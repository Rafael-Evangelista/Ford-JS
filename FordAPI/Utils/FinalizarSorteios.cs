using FordAPI.App_Data;
using FordAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FordAPI.Utils
{
    public class FinalizarSorteios
    {
        private static FordEntities db = new FordEntities();

        public static void FinalizarSorteio()
        {
            //Lista dos sorteados
            List<Sorteios> sorteios = db.Sorteios.ToList();
            var Sorteados = from t in sorteios
                            where t.Sorteado == true
                            select t;

            //Tabela de backup para armazenas os vencedores
            Sorteados_Bckp sorteados_bckp = new Sorteados_Bckp();

            //Lista dos veiculos
            List<Veiculos> veiculos = db.Veiculos.ToList();

            //Tabela de backup para armazena os veiculos sorteados
            VeiculosSorteados_Bckp veiculosSorteados_Bckp = new VeiculosSorteados_Bckp();

            try
            {
                foreach (var sorteio in Sorteados)
                {
                    if (sorteio.Sorteado == true)
                    {
                        sorteados_bckp.FuncionarioId = sorteio.FuncionarioId;
                        sorteados_bckp.VeiculoId = sorteio.VeiculoId;
                        sorteados_bckp.Sorteado = sorteio.Sorteado;
                        sorteados_bckp.NumeroDaSorte = sorteio.NumeroDaSorte;
                        sorteados_bckp.Item = sorteio.Item;
                        sorteados_bckp.UserName = sorteio.UserName;
                        sorteados_bckp.Data = DateTime.Now;

                        //Adicionando os ganhadores na tabela de backup
                        db.Sorteados_Bckp.Add(sorteados_bckp);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                foreach (var veiculo in veiculos)
                {
                    veiculosSorteados_Bckp.VeiculoId = veiculo.Id;
                    veiculosSorteados_Bckp.IdEvento = veiculo.IdEvento;
                    veiculosSorteados_Bckp.Modelo = veiculo.Modelo;
                    veiculosSorteados_Bckp.Marca = veiculo.Marca;
                    veiculosSorteados_Bckp.Ano = veiculo.Ano;
                    veiculosSorteados_Bckp.Valor = veiculo.Valor;
                    veiculosSorteados_Bckp.Participantes = veiculo.Participantes;
                    veiculosSorteados_Bckp.Descricao = veiculo.Descricao;
                    veiculosSorteados_Bckp.Quilometragem = veiculo.Quilometragem;
                    veiculosSorteados_Bckp.Cambio = veiculo.Cambio;
                    veiculosSorteados_Bckp.Carroceria = veiculo.Carroceria;
                    veiculosSorteados_Bckp.Combustivel = veiculo.Combustivel;
                    veiculosSorteados_Bckp.ValorDoIPVA = veiculo.ValorDoIPVA;
                    veiculosSorteados_Bckp.FinalDaPlaca = veiculo.FinalDaPlaca;
                    veiculosSorteados_Bckp.Opcionais = veiculo.Opcionais;
                    veiculosSorteados_Bckp.Potencia = veiculo.Potencia;
                    veiculosSorteados_Bckp.Portas = veiculo.Portas;
                    veiculosSorteados_Bckp.Imagem = veiculo.Imagem;
                    veiculosSorteados_Bckp.InclusoNaCompra = veiculo.InclusoNaCompra;
                    veiculosSorteados_Bckp.Placa = veiculo.Placa;
                    veiculosSorteados_Bckp.Cor = veiculo.Cor;
                    veiculosSorteados_Bckp.Garantia = veiculo.Garantia;
                    veiculosSorteados_Bckp.DataDoSorteio = DateTime.Now;

                    //Adicionando veiculos na tabela de backup
                    db.VeiculosSorteados_Bckp.Add(veiculosSorteados_Bckp);
                    db.SaveChanges();
                }

                //Removendo a lista de veiculos
                db.Veiculos.RemoveRange(veiculos);
                db.Sorteios.RemoveRange(sorteios);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}