using FordAPI.App_Data;
using FordAPI.Models;
using FordAPI.Utils;
using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FordAPI
{
    public class ProviderTokenAccess : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication
            (OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials
            (OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (FordEntities bd = new FordEntities())
            {
                var senha = GeradorDeDeHash.Criptografar(context.Password);

                Funcionarios user = bd.Funcionarios.FirstOrDefault(x => x.Login == context.UserName
                                                 && x.Senha == senha);


                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuário não encontrado ou a senha está incorreta.");
                    return;
                }
                var identyUser = new ClaimsIdentity(context.Options.AuthenticationType);
                context.Validated(identyUser);
            }
        }
    };
}