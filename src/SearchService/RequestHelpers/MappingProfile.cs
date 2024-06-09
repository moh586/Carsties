﻿using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuctionCreated, Item>();
        CreateMap<AuctionUpdated, Item>();
        CreateMap<AuctionDeleted, Item>();
    }
}
