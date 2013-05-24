﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using AutoMapper;
using DDD.Light.Messaging;
using DDD.Light.Realtor.API.Commands.Realtor;
using DDD.Light.Realtor.API.Queries;
using DDD.Light.Realtor.REST.API.Resources;

namespace DDD.Light.Realtor.REST.API.Controllers
{
    public class RealtorController : ApiController
    {
        private readonly IListings _listings;

        public RealtorController(IListings listings)
        {
            _listings = listings;
        }

        [POST("api/realtor/listings")]
        public HttpResponseMessage PostListing([FromBody]RealtorListing realtorListing)
        {
            var postListing = Mapper.Map<RealtorListing, PostListing>(realtorListing);
            postListing.ListingId = Guid.NewGuid();
            try
            {
                CommandBus.Instance.Dispatch(postListing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created, postListing);
        }
        
        [GET("api/realtor/listings")]
        public HttpResponseMessage GetListings()
        {
            try
            {
                 var listings = _listings.All();
                 return Request.CreateResponse(HttpStatusCode.OK, listings);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
            
        }
    }
}