using BookLibrary.Business;
using BookLibrary.Entities;
using BookLibrary.Rest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReaderLibrary.Rest.Controllers
{
    public class ReadersController : ApiController
    {
        [HttpGet]
        public IList<ReaderModel> Get()
        {
            ReaderService ReaderService = new ReaderService();
            var allReaders = ReaderService.GetAll()
                .Select(c => new ReaderModel(c))
                .ToList();
            return allReaders;
        }

        [HttpGet]
        [Route("{readerID:int}")]
        public IHttpActionResult Get(int? readerID)
        {
            if (readerID == null)
                return BadRequest("the parameter readerID is empty");

            ReaderService ReaderService = new ReaderService();
            Reader reader = ReaderService.GetReaderByID(readerID.Value);
            if (reader == null)
                return BadRequest($"Could not find Reader with ID: {readerID}");

            ReaderModel apiReader = new ReaderModel(reader);
            return Ok(apiReader);
        }


    }
}
