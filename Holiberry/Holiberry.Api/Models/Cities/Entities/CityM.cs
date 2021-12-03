﻿using System;
using System.ComponentModel.DataAnnotations;
using Holiberry.Api.Models.Common.Entities;
using NetTopologySuite.Geometries;

namespace Holiberry.Api.Models.Cities
{
    public class CityM : EntityBaseM
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }



        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}