using FordAPI.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FordAPI.Utils
{
    public class EnviarEmail
    {
        public static string EnviarEmailAoSorteado(Sorteios sorteado)
        {
            MailMessage objEmail = new MailMessage();

            //rementente do email
            objEmail.From = new MailAddress("ford.milanexpress@gmail.com");

            //destinatário(s) do email(s)
            objEmail.To.Add(sorteado.Funcionarios.Email);

            //prioridade do email
            objEmail.Priority = MailPriority.High;

            //utilize true pra ativar html no conteúdo do email, ou false, para somente texto
            objEmail.IsBodyHtml = true;

            //Assunto do email
            objEmail.Subject = "Sorteio da Ford";

            //corpo do email a ser enviado
            objEmail.Body =
                "<img src=https://logodownload.org/wp-content/uploads/2014/02/ford-logo-1.png alt=Smiley face height=50 width=100><br/>"
                + "<hr style=height: 2px; border: none; color:#000; background-color:#000; margin-top: 0px; margin-bottom: 0px;/><br/>"
                + "<h1>Olá " + sorteado.Funcionarios.NomeCompleto + "</h1><br/>"
                + "<b>Parabéns!</b><br/>"
                + "Você foi sorteado no feirão realizado dia " + sorteado.Veiculos.Eventos.start + " e foi o ganhador do <b>Lote " + sorteado.Item + "</b>, descrito abaixo:<br/><br/>"
                + ("<b>" + sorteado.Veiculos.Marca + "/" + sorteado.Veiculos.Modelo + " " + sorteado.Veiculos.Carroceria + " " + sorteado.Veiculos.Potencia + " " + sorteado.Veiculos.Combustivel + " " + sorteado.Veiculos.Portas + "P " + "ANO " + sorteado.Veiculos.Ano + "</b><br/>"
                + "<b>VALOR:<font color='red'> " + "R$ " + sorteado.Veiculos.Valor + "</b></font><br/><br/>"
                + "" + sorteado.Veiculos.Marca + " / " + sorteado.Veiculos.Modelo + " " + sorteado.Veiculos.Carroceria + " " + sorteado.Veiculos.Potencia + " " + sorteado.Veiculos.Combustivel + " " + sorteado.Veiculos.Portas + "P " + "ANO " + sorteado.Veiculos.Ano + " KM: " + sorteado.Veiculos.Quilometragem + "</b><br/>"
                + "FINAL DA PLACA: " + sorteado.Veiculos.FinalDaPlaca + "<br/>"
                + "OPCIONAIS: " + sorteado.Veiculos.Opcionais + "<br/><br/>"
                + "** DESPESAS APURADAS POR CONTA DO COMPRADOR ->>" + sorteado.Veiculos.ValorDoIPVA + " INCLUSO O IPVA 2018" + "<br/>"
                + "OBS: O VEÍCULO DEVERA SER TRANSFERIDO OBRIGATORIAMENTE PELO DESPACHANTE CREDENCIADO, VER CONDIÇÕES NR 16 A 10 <br/>"
                + "(QUILOMETRAGEM VEREFICADA NO HODOMETRO) <br/>"
                + "LOCAL: <br>"
                + "*DESPESAS DE ASSESSORIA/ LOGIST.QUE CORRERA P/ CONTA DO COMPRADOR ->>").ToUpper();

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
            objSmtp.Credentials = new NetworkCredential("ford.milanexpress@gmail.com", "ford@123");

            //envia o email
            objSmtp.Send(objEmail);

            return ("E-mail Enviado com sucesso ao vencedor do sorteio!");
        }
    }
}