﻿using MatchOdds.Domain;
using MatchOdds.Domain.Models.Match;
using MatchOdds.Domain.Models.Odd;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MatchOdds.Api.Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    public class OddController : BaseController
    {
        //repository pattern

        public OddController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        // GET: api/odds
        [HttpGet]
        public async Task<ActionResult<IList<OddModel>>> Get()
        {
            try
            {
                var odds = await _unitOfWork.OddRepositoryServiceService.GetAllMatchOdds();
                return Ok(odds);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // GET api/odds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OddModel>> Get(int id)
        {
            try
            {
                var odd = await _unitOfWork.OddRepositoryServiceService.GetMatchOddById(id);
                return Ok(odd);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/odds
        [HttpPost]
        public async Task<ActionResult<OddModel>> Post([FromBody] OddAddModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var odd = await _unitOfWork.OddRepositoryServiceService.AddMatchOdd(model);
                return Ok(odd);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/odds/1
        [HttpPut("{id}")]
        public async Task<ActionResult<OddModel>> Put(int id, [FromBody] OddUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var odd = await _unitOfWork.OddRepositoryServiceService.GetMatchOddById(id);
                if (odd == null)
                {
                    return NotFound();
                }

                if (model.ID == 0)
                {
                    model.ID = id;
                }

                var updatedMatch = await _unitOfWork.OddRepositoryServiceService.UpdateMatchOdd(model);
                return Ok(updatedMatch);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/odds/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var odd = await _unitOfWork.OddRepositoryServiceService.GetMatchOddById(id);
                if (odd == null)
                {
                    return NotFound();
                }

                var deleted = await _unitOfWork.OddRepositoryServiceService.DeleteMatchOdd(id);
                if (deleted)
                {
                    return Ok();
                }
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
