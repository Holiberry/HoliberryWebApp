﻿using AutoMapper;

namespace Holiberry.Api.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
