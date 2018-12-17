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
    [RoutePrefix("api/readers")]
    public class ReadersController : ApiController
    {
        [HttpGet]
        [Route] 
        public IList<ReaderModel> GetAll()
        {
            ReaderService ReaderService = new ReaderService();
            var allReaders = ReaderService.GetAll()
                .Select(c => new ReaderModel(c))
                .ToList();
            return allReaders;
        }

        [HttpGet]
        [Route("{readerID:int}")]
        public IHttpActionResult GetByID(int? readerID)
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

        [HttpPut]
        [Route]
        public IHttpActionResult Put(ReaderModel reader)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbReader = readerService.GetReaderByID(reader.ID);
                if (dbReader == null)
                    return NotFound();

                reader.CopyValuesToEntity(dbReader);
                readerService.EditReader(dbReader);

                return StatusCode(HttpStatusCode.NoContent); // or use Ok()
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost]
        [Route]
        public IHttpActionResult Post(ReaderModel reader)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbReader = new Reader();

                reader.CopyValuesToEntity(dbReader);
                readerService.AddReader(dbReader);

                // return the newly created Reader
                ReaderModel newReader = new ReaderModel(dbReader);
                return Ok(newReader);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete]
        [Route]
        public IHttpActionResult Delete(int readerID)
        {
            try
            {
                ReaderService readerService = new ReaderService();
                Reader dbReader = readerService.GetReaderByID(readerID);
                if (dbReader == null)
                    return NotFound();

                readerService.DeleteReader(readerID);

                // we decide to return no message for information
                // so the response has empty body with status 204 - success
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}
