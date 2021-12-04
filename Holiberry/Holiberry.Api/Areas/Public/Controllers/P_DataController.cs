using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Models.Cities;
using Holiberry.Api.Models.Schools;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Public.Controllers
{
    [ApiAuthorize]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [Route("v1/public/data")]
    public class P_DataController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public P_DataController(ApplicationDbContext db)
        {
            _db = db;
        }


        [HttpGet("cities")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCities()
        {
            var data = await _db.Cities.AsNoTracking()
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.PositionDTO
                })
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("schools")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSchools()
        {
            var data = await _db.Schools.AsNoTracking()
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    CityName = a.City.Name,
                    a.PositionDTO,
                    Photo = a.GetPhotoUrl()
                })
                .ToListAsync();

            return Ok(data);
        }


        [HttpGet("scooters")]
        [AllowAnonymous]
        public async Task<IActionResult> GetScooters()
        {
            var locations = new List<(double lat, double lng, bool isBike)>();

            for (int i = 0; i < 50; i++)
            {
                //Simulate parameters
                Random r = new Random();
                var lat = 51.107883;
                var lng = 17.038538;

                Random random = new Random();

                // Convert radius from meters to degrees
                double radiusInDegrees = 2000 / 111000f;

                double u = random.NextDouble();
                double v = random.NextDouble();
                double w = radiusInDegrees * Math.Sqrt(u);
                double t = 2 * Math.PI * v;
                double x = w * Math.Cos(t);
                double y = w * Math.Sin(t);

                // Adjust the x-coordinate for the shrinking of the east-west distances
                double new_x = x / Math.Cos((Math.PI / 180) * lat);

                lng = new_x + lng;
                lat = y + lat;


                bool isBike = i % 3 == 0;

                locations.Add((lat, lng, isBike));
            }

            var scooters = locations.Select(a => new
                {
                    Lat = a.lat,
                    Lng = a.lng,
                    Type = a.isBike ? "bike" : "scooter",

                    PinUrl = a.isBike ? $"{ConfigAPI.WebAppUrl}images/bike.png" : $"{ConfigAPI.WebAppUrl}images/scooter.png",
                    PhotoUrl = a.isBike ? "https://storage.googleapis.com/api_blinkee_images_prod/devices_images/d66d910e8f8354f9e6ff82e106b831dc.png?v=418" : "https://storage.googleapis.com/api_blinkee_images_prod/devices_images/26fb5e1cea0d20a10ac0e810b420da64.png?v=955"
                })
                .ToList();

            return Ok(scooters);
        }













        [HttpPost("schools")]
        public async Task<IActionResult> PostSchools([FromBody] List<SchoolDTO> schools)
        {
            var city = await _db.Cities
                .Where(a => a.Code == "WRO")
                .FirstOrDefaultAsync();
            if(city == null)
            {
                city = new CityM()
                {
                    Code = "WRO",
                    Name = "Wrocław",
                    Lat = 51.107883,
                    Lng = 17.038538,
                };

                await _db.AddAsync(city);
            }


            var schoolsToAdd = new List<SchoolM>();

            foreach (var s in schools)
            {
                double? lat = double.TryParse(s.Coordinates?.Split(",")?.FirstOrDefault(), NumberStyles.Any, new CultureInfo("en"), out var _lat) ? _lat : (double?)null;
                double? lng = double.TryParse(s.Coordinates?.Split(",")?.LastOrDefault(), NumberStyles.Any, new CultureInfo("en"), out var _lng) ? _lng : (double?)null;


                var sch = new SchoolM()
                {
                    City = city,
                    Name = s.Nazwa,
                    NumberRSPO = int.Parse(s.NumerRSPO),
                    Lat = lat,
                    Lng = lng
                };

                schoolsToAdd.Add(sch);
            }

            await _db.AddRangeAsync(schoolsToAdd);
            await _db.SaveChangesAsync();

            return Ok();
        }


    }


    public class SchoolDTO
    {
        [JsonProperty("NumerRSPO")]
        public string NumerRSPO { get; set; }

        [JsonProperty("REGONpodmiotu")]
        public string REGONpodmiotu { get; set; }

        [JsonProperty("NIPpodmiotu")]
        public string NIPpodmiotu { get; set; }

        [JsonProperty("Typ")]
        public string Typ { get; set; }

        [JsonProperty("Nazwa")]
        public string Nazwa { get; set; }

        [JsonProperty("Kodterytorialnywoj")]
        public string Kodterytorialnywoj { get; set; }

        [JsonProperty("Kodterytorialnypowiat")]
        public string Kodterytorialnypowiat { get; set; }

        [JsonProperty("Kodterytorialnygmina")]
        public string Kodterytorialnygmina { get; set; }

        [JsonProperty("Kodterytorialnymiejscowosc")]
        public string Kodterytorialnymiejscowosc { get; set; }

        [JsonProperty("Kodterytorialnyulica")]
        public string Kodterytorialnyulica { get; set; }

        [JsonProperty("Wojew")]
        public string Wojew { get; set; }

        [JsonProperty("Powiat")]
        public string Powiat { get; set; }

        [JsonProperty("Gmina")]
        public string Gmina { get; set; }

        [JsonProperty("Miejscowosc")]
        public string Miejscowosc { get; set; }

        [JsonProperty("Rodzajmiejscowosci")]
        public string Rodzajmiejscowosci { get; set; }

        [JsonProperty("Ulica")]
        public string Ulica { get; set; }

        [JsonProperty("Numerbudynku")]
        public string Numerbudynku { get; set; }

        [JsonProperty("Numerlokalu")]
        public string Numerlokalu { get; set; }

        [JsonProperty("Kodpocztowy")]
        public string Kodpocztowy { get; set; }

        [JsonProperty("Poczta")]
        public string Poczta { get; set; }

        [JsonProperty("adres")]
        public string Adres { get; set; }

        [JsonProperty("Coordinates")]
        public string Coordinates { get; set; }
    }
}
