﻿using System;
using System.Collections.Generic;
using DDD.Light.Repo.Contracts;

namespace DDD.Light.Realtor.Domain.Model
{
    // aggregate root
    public class Prospect : Entity
    {
        public IEnumerable<Guid> ListingIdsViewed { get; set; }
    }
}