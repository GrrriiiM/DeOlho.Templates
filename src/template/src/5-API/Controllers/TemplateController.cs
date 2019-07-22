using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DeOlho.Templates.DataContract.TemplateAggregate;
using DeOlho.Templates.DataContract.TemplateAggregate.Commands;
using DeOlho.Templates.DataContract.TemplateAggregate.Models;
using DeOlho.Templates.DataContract.TemplateAggregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeOlho.Templates.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class TemplateController : Controller, ITemplateService
    {

        readonly IMediator _mediator;

        public TemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public Task CreateTemplate(CreateTemplateCommand command)
        {
            return _mediator.Send(command);
        }

        [HttpGet("")]
        public Task<IEnumerable<TemplateModel>> ListTemplate(ListTemplateQuery query)
        {
            return _mediator.Send(query);
        }
    }
}