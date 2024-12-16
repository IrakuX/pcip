using entities.entities;
using entities.interfaces;
using entities.models;

namespace core.services
{
    public class UserService : IUserService
    {
        private readonly IWorkRepository _unit;

        public UserService(IWorkRepository unit)
        {
            this._unit = unit;
        }

        public async Task<ResponseModel<Usuario>> GetAsync(int usuarioId)
        {
            ResponseModel<Usuario> response = new ResponseModel<Usuario>();

            try
            {
                var usuario = await _unit.Usuario.GetByIdAsync(usuarioId);
                if (usuario.data != LanguageExt.Option<Usuario>.None)
                {
                    return new ResponseModel<Usuario>()
                    {
                        data = (Usuario)usuario.data,
                        resultado = true
                    };
                }
                else
                {
                    return new ResponseModel<Usuario>()
                    {
                        resultado = false,
                        error = new ErrorModel()
                        {
                            errorCodigo = 000,
                            errorMensaje = "Usuario no encontrado."
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<Usuario>()
                {
                    resultado = false,
                    error = new ErrorModel()
                    {
                        error = ex,
                        errorCodigo = 000,
                        errorMensaje = ex.Message
                    }
                };
            }
        }
    }
}