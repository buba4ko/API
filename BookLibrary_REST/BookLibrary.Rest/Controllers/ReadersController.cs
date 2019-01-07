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
    /// <summary>
    /// Controller for the Reader functionality
    /// </summary>
    [RoutePrefix("api/readers")]
    public class ReadersController : ApiController
    {
        /// <summary>
        /// Get all Readers in the library
        /// </summary>
        /// <returns>an IList with all readers</returns>
        /// <response code="200">OK</response>
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

        /// <summary>
        /// Get information for one reader
        /// </summary>
        /// <param name="readerID">The readerID in teh DB</param>
        /// <returns>information for one reader</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
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

        /// <summary>
        /// Updates information for an existing reader
        /// </summary>
        /// <param name="reader">information for the new Reader. The ID should not be set.</param>
        /// <returns>Code 204 if success or error message</returns>
        /// <response code="204">NoContent</response>
        /// <response code="404">NotFound</response>
        /// <response code="400">BadRequest</response>
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

        /// <summary>
        /// Create a new reader
        /// </summary>
        /// <param name="reader">information for the new Reader. The ID should not be set.</param>
        /// <returns>the newly created Reader object</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
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


        /// <summary>
        /// Deletes reader from the library
        /// </summary>
        /// <param name="readerID">The ID of the reader</param>
        /// <returns>Code 204 if success or error message</returns>
        /// <response code="204">NoContent</response>
        /// <response code="404">NotFound</response>
        /// <response code="400">BadRequest</response>
        [HttpDelete]
        [Route("{readerID:int}")]
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
