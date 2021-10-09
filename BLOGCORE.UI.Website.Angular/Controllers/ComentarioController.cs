using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.Website.Angular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : BaseController
    {
        private readonly IComentarioService _comentarioService;

        public ComentarioController(IComentarioService comentarioService)
        {
            _comentarioService = comentarioService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(long Id)
        {
            var result = await _comentarioService.GetComentariosPostId(Id);

            return Ok(result);
        }
    }
}
