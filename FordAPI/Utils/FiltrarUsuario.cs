using FordAPI.App_Data;
using FordAPI.Models;
using System.Linq;

namespace FordAPI.Utils
{
    public class FiltrarUsuario
    {
        private static FordEntities db = new FordEntities();
        public static string FiltrandoUsuarioPorId(string userName)
        {
            int Id = 0;
            var funcionarios = db.Funcionarios.ToList();

            foreach (var item in funcionarios)
            {
                if (item.Login == userName)
                {
                    Id = item.Id;
                }
            }
            Funcionarios funcionario = db.Funcionarios.Find(Id);

            if (funcionario == null)
            {
                return ("Usuário não encontrado");
            }

            return funcionario.Id.ToString();
        }

    }
}