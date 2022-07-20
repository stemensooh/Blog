using BLOGCORE.APPLICATION.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOGCORE.UI.API.POST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaService _categoria;

        public CategoriaController(ICategoriaService categoria)
        {
            _categoria = categoria;
        }

        public async Task<IActionResult> Get()
        {
            return Ok(await _categoria.GetAll());
        }
    }
}
