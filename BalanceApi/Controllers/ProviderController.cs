
using System;
using System.Collections.Generic;
using BalanceApi.Controllers.Views;
using BalanceApi.Model.Domain;
using BalanceApi.Services;
using BalanceApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BalanceApi.Controllers {

    [Route("/api/provider")]
    public class ProviderController: BaseController {

        private readonly ILogger _logger;
        private readonly ProviderService _service;

        private readonly IModelValidator<Provider> _validator;

        public ProviderController(ILogger<ProviderController> logger, ProviderService service,
            IModelValidator<Provider> validator)
        {
            _service = service;
            _logger = logger;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            if (result.IsSuccess()) return Ok(result.GetPayload());

            _logger.LogError("Unable to get the providers due: {0}", result.GetFailure());
            return ForFailure(result.GetFailure());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var result = _service.GetById(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());

            var provider = result.GetPayload();
            if (provider != null) return Ok(provider);

            return NotFound();
        }

        [HttpGet("country/{country}")]
        public IActionResult GetByCountry(string country)
        {
            var result = _service.GetByCountry(country);

            return result.IsSuccess() ? Ok(result.GetPayload()) : ForFailure(result.GetFailure());
        }

        [HttpPost]
        public IActionResult New([FromBody] AddProvider newProvider)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var provider = new Provider() { Name = newProvider.Name, Country = newProvider.Country };
            var result = _service.New(provider);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var p = result.GetPayload();
            if (p != null)
            {
                return Created("New provider", p);
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Provider provider) {
            var validation = _validator.Validate(provider);

            if (validation.HasFailed()) return BadRequest(validation.GetErrors());

            var r = _service.Update(provider);

            if (r.HasErrors()) return ForFailure(r.GetFailure());

            var p = r.GetPayload();
            if (p != null) return Created("Update Provider", p);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _service.Delete(id);

            if (result.HasErrors()) return ForFailure(result.GetFailure());
            
            var rows = result.GetPayload();
            if (rows > 0) return Accepted(rows);

            return NotFound();
        }
    }
}