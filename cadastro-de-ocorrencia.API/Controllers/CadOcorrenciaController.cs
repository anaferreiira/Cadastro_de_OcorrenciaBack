using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.GrupoOcorrencia.Api1.Business;
using Signa.GrupoOcorrencia.Api1.Domain.Models;
using System;
using System.Collections.Generic;

namespace Signa.GrupoOcorrencia.Api1.Controllers
{
    [ApiController]
    [Produces("application/json")]
   /// [Authorize("Bearer")]
    public class CadOcorrenciaController : Controller
    {
        private readonly CadOcorrenciaBL CadOcorrenciaBL;
        public CadOcorrenciaController(CadOcorrenciaBL CadOcorrenciaBL)
        {
            this.CadOcorrenciaBL = CadOcorrenciaBL;
        }
        /// <summary>
                /// Cadastra Grupo de ocorrencia novo
                /// </summary>

        [HttpPost]
        [Route("CadastrarOcorrencia")]
        public ActionResult<TipoOcorrenciaModel> CadastrarOcorrencia(TipoOcorrenciaModel Cad)
        {
            try
            {
                return Ok(CadOcorrenciaBL.CadastrarOcorrencia(Cad));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
                /// Deleta Grupo de ocorrencia
                /// </summary>
        [HttpDelete]
        [Route("DeleteOcorrencia/{id}")]
        public ActionResult DeleteOcorrencia(int id)
        {
            CadOcorrenciaBL.DeleteOcorrencia(id);
            return Ok();
        }

        /// <summary>
                /// Lista de Grupo de ocorrencia
                /// </summary>

        [HttpGet]
        [Route("DescricaoGrupo")]
        public ActionResult<IEnumerable<TipoOcorrenciaModel>> DescricaoGrupo(string DescricaoGrupo) => Ok(CadOcorrenciaBL.DescricaoGrupo(DescricaoGrupo));
    }
}
