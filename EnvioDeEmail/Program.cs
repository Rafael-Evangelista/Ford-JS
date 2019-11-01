using System.Net;
using System.Net.Mail;
using System.Text;

namespace EnvioDeEmail
{
    class Program
    {

        static void Main(string[] args)
        {
            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("rafael.evangelista@milanleiloes.com.br");

            //destinatário(s) do email(s)
            objEmail.To.Add("rafael.evangelista@milanleiloes.com.br");

            //prioridade do email
            objEmail.Priority = MailPriority.High;

            //utilize true pra ativar html no conteúdo do email, ou false, para somente texto
            objEmail.IsBodyHtml = true;

            //Assunto do email
            objEmail.Subject = "Sorteio da Ford";

            //corpo do email a ser enviado
            objEmail.Body =
                "<img src=https://logodownload.org/wp-content/uploads/2014/02/ford-logo-1.png alt=Smiley face height=50 width=90><br/>"
                + "<hr style=height: 2px; border: none; color:#000; background-color:#000; margin-top: 0px; margin-bottom: 0px;/><br/>"
                + "<h1>Olá Rafael Evangelista</h1><br/>"
                + "<b>Parabéns!</b><br/>"
                + "Você foi sorteado no feirão realizado dia <b>10/05/2019</b> e foi o ganhador do <b>item 001</b>, descrito abaixo:<br/><br/>"
                + "<b>FORD/KA HATCH SEL 1.0 FLEX 4P ANO 2014/2015</b><br/>"
                + "<b>VALOR:</b><br/><br/>"
                + "FORD/KA"
                ;

            //codificação do assunto do email para que os caracteres acentuados serem reconhecidos.
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");

            //codificação do corpo do emailpara que os caracteres acentuados serem reconhecidos.
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            //cria o objeto responsável pelo envio do email
            SmtpClient objSmtp = new SmtpClient();

            //endereço do servidor SMTP(para mais detalhes leia abaixo do código)
            objSmtp.Host = "smtp.gmail.com";
            objSmtp.Port = 587;
            objSmtp.EnableSsl = true;

            //para envio de email autenticado, coloque login e senha de seu servidor de email
            objSmtp.Credentials = new NetworkCredential("rafael.evangelista@milanleiloes.com.br", "Dropdead@10");

            //envia o email
            objSmtp.Send(objEmail);
        }
    }
}

