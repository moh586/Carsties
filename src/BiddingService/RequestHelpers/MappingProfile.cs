using AutoMapper;
using BiddingService.DTOs;
using BiddingService.Models;
using Contracts.Bids;

namespace SearchService;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Bid, BidDto>();
        CreateMap<Bid, BidPlaced>();
    }
}
