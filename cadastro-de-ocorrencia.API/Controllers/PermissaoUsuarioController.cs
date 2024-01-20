using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.GrupoOcorrencia.Api1.Business;
using Signa.GrupoOcorrencia.Api1.Domain.Models;
using Signa.Library.Core.Aspnet.Domain.Models;
using System.Collections.Generic;

namespace Signa.GrupoOcorrencia.Api1.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class PermissaoUsuarioController : Controller
    {
        private readonly PermissaoUsuarioBL _permissaoUsuarioBLL;

        public PermissaoUsuarioController(PermissaoUsuarioBL permissaoUsuarioBLL)
        {
            _permissaoUsuarioBLL = permissaoUsuarioBLL;
        }

        /// <summary>
        /// Busca permissão do usuário logado na base de dados
        /// </summary>
        /// <response code="200">Permissões cadastradas na base de dados</response>
        /// <response code="404">Nenhuma permissão encontrada na base de dados</response>
        [HttpGet]
        [Route("permissao")]
        public ActionResult<PermissaoUsuarioModel> Get() => Ok(_permissaoUsuarioBLL.Get());


    }
}
