﻿using DDD.Light.Messaging;
using DDD.Light.Realtor.Core.Domain.Model.Buyer;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Application.EventHandlers.Buyer
{
    public class PromotedToRepeatBuyerHandler : EventHandler<Core.Domain.Events.Buyer.PromotedToRepeatBuyer>
    {
        private readonly IRepository<IBuyer> _buyerRepo;

        public PromotedToRepeatBuyerHandler(IRepository<IBuyer> buyerRepo)
        {
            _buyerRepo = buyerRepo;
        }

        public override void Handle(Core.Domain.Events.Buyer.PromotedToRepeatBuyer @event)
        {
            _buyerRepo.Save(@event.Buyer);
        }
    }
}