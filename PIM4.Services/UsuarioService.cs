using System.Threading.Tasks;
using PIM4.Models.Entidades;
using PIM4.Data.Repositorios;
using BCrypt.Net; // Certifique-se de que este pacote está instalado no projeto PIM4.Services
namespace PIM4.Services
{
    /// <summary>
    /// Serviço responsável pelas regras de negócio e lógica de autenticação dos usuários.
    /// </summary>
    public class UsuarioService
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        /// <summary>
        /// Valida as credenciais do usuário para login.
        /// (REQUISITO: SEGURANÇA E CONFORMIDADE)
        /// </summary>
        /// <param name="email">O e-mail (usuário) fornecido.</param>
        /// <param name="senha">A senha sem hash fornecida.</param>
        /// <returns>O objeto Usuario se as credenciais forem válidas, ou null.</returns>
        public async Task<Usuario?> Autenticar(string email, string senha)
        {
            // 1. Buscar usuário pelo e-mail
            var usuario = await _usuarioRepositorio.BuscarPorEmail(email);

            // 2. Verificar se o usuário existe
            if (usuario == null)
            {
                return null;
            }

            // 3. Verificar a senha (utiliza BCrypt para comparar a senha fornecida com o hash)
            // BCrypt.Verify(senhaEmTexto, hashSalvo)
            if (BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            {
                // Senha correta
                return usuario;
            }
            else
            {
                // Senha incorreta
                return null;
            }
        }

        // --- Outros métodos de CRUD iriam aqui ---

        // Método de Criação de Usuário (Exemplo, para garantir que a senha é criptografada ao salvar)
        public async Task<Usuario> CriarUsuario(Usuario novoUsuario, string senha)
        {
            // Aplica o hash antes de salvar
            novoUsuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            return await _usuarioRepositorio.Adicionar(novoUsuario);
        }

        // Método BuscarPorEmail, necessário para o Autenticar
        // Este método deve existir no seu UsuarioRepositorio e retornar o Usuario
        // Se ainda não tiver este método no seu repositório, ele precisa ser adicionado.

        // ... (Adicione outros métodos, como GetById, GetAll, etc.)
    }
}