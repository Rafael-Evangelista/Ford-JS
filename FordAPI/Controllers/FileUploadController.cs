using FordAPI.App_Data;
using FordAPI.Models;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FordAPI.Controllers
{
    public class FileUploadController : ApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private FordEntities _db = new FordEntities();

        [HttpPost]
        [Route("FilesUpload")]
        public IList<Veiculos> FileUpload()
        {
            dynamic fileUpload = "";
            dynamic path = "";

            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    fileUpload = HttpContext.Current.Request.Files[0];
                    path = Path.Combine(
                        HttpContext.Current.Server.MapPath("~/UploadDeVeiculos"),
                        fileUpload.FileName
                        );
                }

                if (fileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    fileUpload.SaveAs(path);
                }

            }
            catch (Exception)
            {

                throw;
            }


            return InserirVeiculos(path.ToString());
        }

        [HttpPost]
        [Route("UploadDeVeiculos")]
        public IList<Veiculos> InserirVeiculos(string path)
        {

            FileInfo file = new FileInfo(Path.Combine(path));
            string FileName = file.Name.Remove(file.Name.Length - 5, 5);

            byte[] arquivo = System.IO.File.ReadAllBytes(path);

            try
            {
                //Inserir Arquivo no banco de dados
                var FileUpload = new FilesUpload();
                FileUpload.Arquivo = arquivo;
                FileUpload.Data = Convert.ToDateTime(FileName);
                _db.FilesUpload.Add(FileUpload);
                _db.SaveChanges();


                using (ExcelPackage package = new ExcelPackage(file))
                {
                    //Guid do Id do Evento
                    Guid IdEvento = new Guid();

                    string ConvertGarantia = string.Empty;

                    ExcelWorksheet workSheet = package.Workbook.Worksheets["ImportarVeiculos"];
                    int totalRows = workSheet.Dimension.Rows;

                    List<Veiculos> ExcelList = new List<Veiculos>();

                    var eventos = _db.Eventos.ToList();

                    foreach (var item in eventos)
                    {
                        if (item.start == FileUpload.Data)
                        {
                            IdEvento = item.Id;
                        }
                    }

                    for (int i = 2; i <= totalRows; i++)
                    {
                        if (workSheet.Cells[i, 19].Value.ToString() == "Sim")
                        {
                            workSheet.Cells[i, 19].Value = true;
                        }
                        else
                        {
                            workSheet.Cells[i, 19].Value = false;
                        }

                        ExcelList.Add(new Veiculos
                        {
                            Id = Guid.NewGuid(),
                            IdEvento = IdEvento,
                            Lote = Convert.ToInt32(workSheet.Cells[i, 1].Value),
                            Modelo = workSheet.Cells[i, 2].Value.ToString(),
                            Marca = workSheet.Cells[i, 3].Value.ToString(),
                            Ano = Convert.ToInt32(workSheet.Cells[i, 4].Value),
                            Valor = Convert.ToDecimal(workSheet.Cells[i, 5].Value),
                            Descricao = workSheet.Cells[i, 6].Value.ToString(),
                            Quilometragem = Convert.ToDecimal(workSheet.Cells[i, 7].Value),
                            Cambio = workSheet.Cells[i, 8].Value.ToString(),
                            Carroceria = workSheet.Cells[i, 9].Value.ToString(),
                            Combustivel = workSheet.Cells[i, 10].Value.ToString(),
                            ValorDoIPVA = Convert.ToDecimal(workSheet.Cells[i, 11].Value),
                            FinalDaPlaca = Convert.ToInt32(workSheet.Cells[i, 12].Value),
                            Opcionais = workSheet.Cells[i, 13].Value.ToString(),
                            Potencia = workSheet.Cells[i, 14].Value.ToString(),
                            Portas = Convert.ToInt32(workSheet.Cells[i, 15].Value),
                            Imagem = workSheet.Cells[i, 1].Value.ToString() + ".png",
                            InclusoNaCompra = workSheet.Cells[i, 16].Value.ToString(),
                            Placa = workSheet.Cells[i, 17].Value.ToString(),
                            Cor = workSheet.Cells[i, 18].Value.ToString(),
                            Garantia = Convert.ToBoolean(workSheet.Cells[i, 19].Value)
                        });
                    }

                    _db.Veiculos.AddRange(ExcelList);
                    _db.SaveChanges();

                    File.Delete(path);

                    return ExcelList;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

