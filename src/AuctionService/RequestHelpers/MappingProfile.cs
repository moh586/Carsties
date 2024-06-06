using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionService.DTOs.Auctions;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction,AuctionDTO>().IncludeMembers(x=>x.Item);
            CreateMap<Item,AuctionDTO>();
            CreateMap<CreateAuctionDTO,Auction>().ForMember(d=>d.Item,o=>o.MapFrom(s=>s));
            CreateMap<CreateAuctionDTO,Item>();
        }
    }
}