using System.Data;

namespace Signa.GrupoOcorrencia.Api1.Domain.Entities
{
    public class TipoOcorrenciaEntity
    {
        public int tab_status_tracking_id { get; set; }
        public string tipo_status_tracking { get; set; }
        public string desc_status_tracking_longa { get; set; }
        public string Desc_Longa { get; set; }
        public int ordem_status { get; set; }
        public string tab_status_tracking { get; set; }
        public string desc_status_tracking { get; set; }
        public int Empresa_Id { get; set; }
        public string Msg_Ret { get; set; }
        public int usuario_incl { get; set; }
        // DOC: Todos os parametros que a API precisa para fazer as suas ações
        // Gravar, excluir, update, select
    }
}
