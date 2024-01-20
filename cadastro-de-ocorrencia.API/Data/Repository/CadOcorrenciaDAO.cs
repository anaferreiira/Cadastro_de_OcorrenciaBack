using Dapper;
using Signa.GrupoOcorrencia.Api1.Domain.Entities;
using Signa.Library.Core.Data.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace Signa.GrupoOcorrencia.Api1.Data.Repository
{
    public class CadOcorrenciaDAO : RepositoryBase
    {
        //Usuando a SP INC para efetua a gravação de um grupo de ocorrência

        public TipoOcorrenciaEntity CadastrarOcorrencia(TipoOcorrenciaEntity Ocorrencia)
        {

            var sql = "SP_ECR_INC_STATUS_TRACKING_API01";
            var param = new
            {
                desc_status_tracking = Ocorrencia.desc_status_tracking,
                desc_status_tracking_longa = Ocorrencia.desc_status_tracking_longa,
                ordem_status = Ocorrencia.ordem_status,
                usuario_incl = Ocorrencia.usuario_incl,
                tipo_status_tracking = Ocorrencia.tipo_status_tracking,

            };
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<TipoOcorrenciaEntity>(sql, param, commandType: CommandType.StoredProcedure);
            }
                   }
        //Usuando a SP ALT para efetua a alteração de um grupo de ocorrência

        public TipoOcorrenciaEntity AlteracaoOcorrencia(TipoOcorrenciaEntity cad)
        {
            var sql = "SP_ECR_ALT_STATUS_TRACKING_API01";
            var param = new
            {
                tab_status_tracking = cad.tab_status_tracking,
                desc_status_tracking = cad.desc_status_tracking,
                tipo_status_tracking = cad.tipo_status_tracking,
                desc_status_tracking_longa = cad.desc_status_tracking_longa,
                tab_status_tracking_id = cad.tab_status_tracking_id

            };
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<TipoOcorrenciaEntity>(sql, param, commandType: CommandType.StoredProcedure);

            }
        }

        //Usuando o UPDATE para efetua a exclusão de um grupo de ocorrência

        public void DeleteOcorrencia(int id)
        {
            var sql = "UPDATE TAB_STATUS_TRACKING SET TAB_STATUS_ID = 2 WHERE TAB_STATUS_TRACKING_ID = @TAB_STATUS_TRACKING_ID";
            var param = new
            {
                TAB_STATUS_TRACKING_ID = id,
            };
            using (var db = Connection)
            {
                db.Execute(sql, param); 
            }
        }
        //Usuando o SELECT  para trazer os nomes dos grupos de ocorrências

        public IEnumerable<TipoOcorrenciaEntity> DescricaoGrupo(string cad)
        {
            var sql = "SELECT tab_status_tracking_id, Desc_status_tracking_longa,desc_status_tracking  From TAB_STATUS_TRACKING  Where TAB_STATUS_TRACKING.Desc_status_tracking LIKE @nome AND TAB_STATUS_ID = 1;";

            var param = new
            {
                nome = cad + "%"
            };

            using (var db = Connection)
            {
                return db.Query<TipoOcorrenciaEntity>(sql, param);

            }
        }


    }
}

