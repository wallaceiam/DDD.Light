﻿using System;
using CQRS.Light.Core;
using DDD.Light.Realtor.Domain.Event.Offer;
using CQRS.Light.Contracts;

namespace DDD.Light.Realtor.Domain.Model.Offer
{
    // aggregate root
    public class Offer : AggregateRoot
    {
        public Offer(IAggregateBus aggregateBus)
            :base(aggregateBus)
        {

        }
        public Guid BuyerId { get; set; }
        public Guid ProspectId { get; set; }
        public Guid ListingId { get; set; }
        public decimal Price { get; set; }
        public DateTime OfferedOn { get; set; }
        public IOfferReply OfferReply { get; set; }

        public void Accept()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferAcceptance{ RepliedOn = DateTime.UtcNow };
            EventBus.Instance.PublishAsync(GetType(), Id, new Accepted { Offer = this });
        }
        
        public void Reject()
        {
            if (Id == null) throw new Exception("Offer does not have Id");
            OfferReply = new OfferDenial{ RepliedOn = DateTime.UtcNow };
            EventBus.Instance.PublishAsync(GetType(), Id, new Rejected { Offer = this });
        }
    }
}