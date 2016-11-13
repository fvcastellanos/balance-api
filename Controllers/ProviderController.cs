
using System;
using Microsoft.AspNetCore.Mvc;
using BalanceApi.Services;
using Microsoft.Extensions.Logging;
using BalanceApi.Model.Domain;
using System.Collections.Generic;

namespace BalanceApi.Controllers {

    [Route("/api/provider")]
    public class ProviderController: BaseController {

        private ILogger logger;
        private ProviderService Service;

        public ProviderController(ILogger<ProviderController> logger, ProviderService Service) {
            this.Service = Service;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll() {
            Result<Exception, List<Provider>> result = Service.GetAll();
            if(result.isSuccess()) {
                return Ok(result.GetPayload());
            } else {
                logger.LogError("Unable to get the providers due: {0}", result.GetFailure());
                return ForException(result.GetFailure());
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id) {
            Result<Exception, Provider> result = Service.GetById(id);
            if(result.isSuccess()) {
                Provider provider = result.GetPayload();
                if(provider != null) {
                    return Ok(provider);
                } else {
                    return NotFound();
                }
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpGet("country/{country}")]
        public IActionResult GetByCountry(string country) {
            Result<Exception, List<Provider>> result = Service.GetByCountry(country);
            if(result.isSuccess()) {
                return Ok(result.GetPayload());
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpPost]
        public IActionResult New([FromBody] Provider provider) {
            Result<Exception, Provider> result = Service.New(provider);
            if(result.isSuccess()) {
                Provider p = result.GetPayload();
                if(p != null ) {
                    return Created("New provider", p);
                } else {
                    return BadRequest();
                }
            } else {
                return ForException(result.GetFailure());
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] Provider provider) {
            return Ok();
        }
    }
}