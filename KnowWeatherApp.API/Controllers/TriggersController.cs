using KnowWeatherApp.Common.Interfaces;
using KnowWeatherApp.Contracts.Models.Triggers;
using KnowWeatherApp.Domain.Entities;
using KnowWeatherApp.Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowWeatherApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TriggersController : ControllerBase
    {
        private readonly ITriggerRepository triggerRepository;
        private readonly ICurrentUserHelper currentUserHelper;

        public TriggersController(
            ITriggerRepository triggerRepository,
            ICurrentUserHelper currentUserHelper)
        {
            this.triggerRepository = triggerRepository;
            this.currentUserHelper = currentUserHelper;
        }

        /// <summary>
        /// Get users triggers.
        /// </summary>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTriggersAsync(CancellationToken cancel)
        {
            var triggers = await this.triggerRepository.GetUsersTriggers(this.currentUserHelper.UserId, cancel);
            return Ok(triggers.AsQueryable().ProjectToType<TriggerSummaryDto>());
        }

        /// <summary>
        /// Create a trigger
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTriggerAsync(TriggerEditDto model, CancellationToken cancel)
        {
            var entity = model.Adapt<Trigger>();
            entity.Created = DateTime.UtcNow;
            entity.Modified = DateTime.UtcNow;
            entity.UserId = this.currentUserHelper.UserId;

            this.triggerRepository.Create(entity);
            await this.triggerRepository.SaveChangesAsync(cancel);

            return CreatedAtAction(nameof(GetTriggersAsync), model);
        }

        [HttpPut("{triggerId}")]
        public async Task<IActionResult> UpdateTriggerAsync(int triggerId, [FromBody]TriggerEditDto model, CancellationToken cancel)
        {
            var trigger = (await this.triggerRepository
                    .FindByCondition(x => x.TriggerId == triggerId && x.UserId == this.currentUserHelper.UserId, cancel))
                    .FirstOrDefault();      

            if (trigger == null)
            {
                return BadRequest("City doesn't exist");
            }

            model.Adapt(trigger);
            trigger.Modified = DateTime.UtcNow;
            trigger.UserId = this.currentUserHelper.UserId;

            this.triggerRepository.Update(trigger);

            await this.triggerRepository.SaveChangesAsync(cancel);

            return Ok();
            
        }


        /// <summary>
        /// Removes user trigger by trigger id
        /// </summary>
        /// <param name="triggerId"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        [HttpDelete("{triggerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveTrigger(int triggerId, CancellationToken cancel)
        {
            var triggers = await this.triggerRepository
                                    .FindByCondition(x => x.UserId == this.currentUserHelper.UserId && x.TriggerId == triggerId, cancel);
            
            var trigger = triggers.FirstOrDefault();
            if (trigger == null)
            {
                return BadRequest();
            }

            this.triggerRepository.Delete(trigger);
            await this.triggerRepository.SaveChangesAsync(cancel);

            return NoContent();
        }
    }
}
