using AutoMapper;
using Signa.GrupoOcorrencia.Api1.Data.Repository;
using Signa.GrupoOcorrencia.Api1.Domain.Models;
using Signa.Library.Core;
using Signa.Library.Core.Aspnet.Helpers;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;

namespace Signa.GrupoOcorrencia.Api1.Business
{
    public class PermissaoUsuarioBL
    {
        private readonly IMapper _mapper;
        private readonly UserProvider _userProvider;
        private readonly PermissaoUsuarioDAO _permissaoUsuarioDAO;
        private readonly int _usuarioId;
        private readonly int _funcaoId;
        private readonly int _grupoUsuarioId;

        public PermissaoUsuarioBL(
            IMapper mapper,
            UserProvider userProvider,
            PermissaoUsuarioDAO permissaoUsuarioDAO
        )
        {
            _mapper = mapper;
            _userProvider = userProvider;
            _permissaoUsuarioDAO = permissaoUsuarioDAO;
            _usuarioId = _userProvider.UserId;
            _funcaoId = Global.FuncaoId;
            _grupoUsuarioId = _userProvider.UserGroupId;
        }


        public PermissaoUsuarioModel Get()
        {
            if (_usuarioId.IsZeroOrNull())
                throw new SignaSqlNotFoundException("Usuário não encontrado");

            var permissoes = _permissaoUsuarioDAO.Get(_usuarioId, _funcaoId);

            if (permissoes == null)
                throw new SignaSqlNotFoundException("Nenhuma permissão encontrada para essa função com este usuário");

            return _mapper.Map<PermissaoUsuarioModel>(permissoes);
        }

    }
}
