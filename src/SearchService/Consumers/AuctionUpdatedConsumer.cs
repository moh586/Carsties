using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;

public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine("==========> Consuming auction Updating" + context.Message.Id);

        var item = _mapper.Map<Item>(context.Message);
        var result = await DB.Update<Item>()
        .MatchID(item.ID)
        .ModifyWith(item)
        .ExecuteAsync();

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionUpdated), "Problem Updating in Mobgo DB");
        }
    }
}
