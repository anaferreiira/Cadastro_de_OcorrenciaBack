using AutoMapper;
using Signa.GrupoOcorrencia.Api1.Data.Repository;
using Signa.GrupoOcorrencia.Api1.Domain.Entities;
using Signa.GrupoOcorrencia.Api1.Domain.Models;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using System.Collections.Generic;

namespace Signa.GrupoOcorrencia.Api1.Business
{
    public class CadOcorrenciaBL
    {

        private readonly CadOcorrenciaDAO SQL;
        private readonly IMapper mapa;
        private readonly CadOcorrenciaDAO _CadDAO;

        public CadOcorrenciaBL(CadOcorrenciaDAO Cad, IMapper mapa)
        {

            this.mapa = mapa;
            _CadDAO = Cad;

        }

        public TipoOcorrenciaModel CadastrarOcorrencia(TipoOcorrenciaModel Cad)
        {
            var cadastro = mapa.Map<TipoOcorrenciaEntity>(Cad);
            var cadastroFeito = new TipoOcorrenciaEntity();
            if (Cad.tab_status_tracking_id.IsZeroOrNull())
            {
                cadastroFeito = _CadDAO.CadastrarOcorrencia(cadastro);
            }
            else
            {
                cadastroFeito = _CadDAO.AlteracaoOcorrencia(cadastro);
            }
            return mapa.Map<TipoOcorrenciaModel>(cadastroFeito);

        }

        public void DeleteOcorrencia(int id)
        {
                if (!id.IsZeroOrNull())
            {
                _CadDAO.DeleteOcorrencia(id);
            }
            else
            {
                throw new SignaSqlNotFoundException("Nenhuma pessoa encontrada com esse id");
            }
        }

        public IEnumerable<TipoOcorrenciaModel> DescricaoGrupo(string DescricaoGrupo)
        {
            var results = _CadDAO.DescricaoGrupo(DescricaoGrupo);

            if (results.IsNullOrEmpty())
            {
                throw new SignaSqlNotFoundException("Nenhum nome encontrado");
            }

            return mapa.Map<IEnumerable<TipoOcorrenciaModel>>(results);
        }



    }
}
