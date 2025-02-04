﻿using System;
using System.Collections.Generic;
using CadastrosBasicos.ManipularBanco;

namespace CadastrosBasicos
{
    public class Fornecedor
    {
        public LeituraCliente leituraCliente = new LeituraCliente();
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimaCompra { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }

        public Fornecedor()
        {

        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura, char situacao)
        {
            CNPJ = cnpj;
            RazaoSocial = rSocial;
            DataAbertura = dAbertura;
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public Fornecedor(string cnpj, string rSocial, DateTime dAbertura, DateTime uCompra, DateTime dCadastro, char situacao)
        {
            CNPJ = cnpj;
            RazaoSocial = rSocial;
            DataAbertura = dAbertura;
            UltimaCompra = DateTime.Now;
            DataCadastro = DateTime.Now;
            Situacao = situacao;
        }
        public void Navegar()
        {
            Console.WriteLine("============== Fornecedores ==============");
            List<Fornecedor> lista = new LeituraFornecedor().TrazerFornecedores();
            int opcao = 0, posicao = 0;
            if (lista.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Ainda nao tem nenhum fornecedor cadastrado");
                Console.WriteLine("Pressione enter para continuar");
                Console.ReadKey();
                return;
            }
            bool flag = false;
            do
            {
                Console.Clear();
                Console.WriteLine("============== Fornecedores ==============");

                if (opcao == 0)
                {
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 1)
                {
                    if (posicao != lista.Count - 1)
                        posicao++;

                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 2)
                {
                    if (posicao != 0)
                        posicao--;

                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 3)
                {
                    posicao = 0;
                    Console.WriteLine(lista[posicao].ToString());
                }
                else if (opcao == 4)
                {
                    posicao = lista.Count - 1;
                    Console.WriteLine(lista[posicao].ToString());
                }

                Console.WriteLine(@"
1. Proximo 
2. Anterior
3. Primeiro
4. Ultimo
0. Voltar para menu anterior.
");
                do
                {
                    flag = int.TryParse(Console.ReadLine(), out opcao);
                } while (flag != true);

            } while (opcao != 0);

            Console.Clear();
            Console.WriteLine("Ainda nao tem nenhum fornecedor cadastrado");
            Console.WriteLine("Pressione enter para continuar");
            Console.ReadKey();
        }
        public void Localizar()
        {
            Console.WriteLine("Insira o CNPJ para localizar: ");
            string cnpj = Console.ReadLine();

            Fornecedor fornecedor = new LeituraFornecedor().ProcurarFornecedor(cnpj);

            if (fornecedor.CNPJ != null)
            {
                Console.WriteLine(fornecedor.ToString());
            }
            else
                Console.WriteLine("Nenhum cadastrado foi encontrado!");
            Console.WriteLine("Pressione enter para voltar ao menu.");
            Console.ReadKey();
        }
        public void BloqueiaFornecedor()
        {
            Fornecedor fornecedor;
            Console.WriteLine("Insira o CNPJ para bloqueio: ");
            string cnpj = Console.ReadLine();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (new LeituraFornecedor().ProcurarCNPJBloqueado(cnpj))
            {
                bool flag = false;
                int opcao;
                Console.WriteLine("Fornecedor ja esta bloqueado");
                Console.WriteLine("Deseja desbloqueado ? [1 - Sim/ 2 - Nao]");

                do
                {
                    flag = int.TryParse(Console.ReadLine(), out opcao);
                } while (flag != true);

                if (opcao == 1)
                {
                    new EscritaFornecedor().AlterarSituacaoDoFornecedor(cnpj, 'A');
                }
            }
            else
            {
                if (Validacoes.ValidarCnpj(cnpj))
                {
                    fornecedor = new LeituraFornecedor().ProcurarFornecedor(cnpj);
                    if (fornecedor.CNPJ != null)
                    {
                        new EscritaFornecedor().AlterarSituacaoDoFornecedor(fornecedor.CNPJ, 'I');
                        Console.WriteLine("CNPJ bloqueado!");
                    }
                }
                else
                    Console.WriteLine("CNPJ incorreto!");
            }
        }
        public string RetornaArquivo()
        {
            return CNPJ + RazaoSocial + DataAbertura.ToString("dd/MM/yyyy") + UltimaCompra.ToString("dd/MM/yyyy") + DataCadastro.ToString("dd/MM/yyyy") + Situacao;
        }
        public Fornecedor Editar()
        {
            Fornecedor fornecedor;
            Console.WriteLine("Somente algumas informacoes podem ser alterada como (Razao social/situacao), caso nao queira alterar alguma informacao pressione enter!");
            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();

            fornecedor = new LeituraFornecedor().ProcurarFornecedor(cnpj);
            if (fornecedor.CNPJ != null)
            {
                Console.WriteLine("Razao social: ");
                string nome = Console.ReadLine().Trim();
                Console.WriteLine("Situacao [A - Ativo/ I - inativo]: ");
                bool flagSituacao = char.TryParse(Console.ReadLine().ToString().ToUpper(), out char situacao);

                fornecedor.RazaoSocial = nome == "" ? fornecedor.RazaoSocial : nome;

                fornecedor.Situacao = flagSituacao == false ? fornecedor.Situacao : situacao;

                new EscritaFornecedor().EditarFornecedor(fornecedor);
            }
            return fornecedor;
        }
        public void FornecedorBloqueado()
        {
            Console.WriteLine("Insira o CNPJ para pesquisa: ");
            string cnpj = Console.ReadLine();
            bool flag = new LeituraFornecedor().ProcurarCNPJBloqueado(cnpj);

            if (flag)
            {
                Fornecedor fornecedor = new LeituraFornecedor().ProcurarFornecedor(cnpj);
                Console.WriteLine(fornecedor.ToString());
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Fornecedor bloqueado nao encontrado");
                Console.ReadKey();
            }

        }
        public override string ToString()
        {
            return $"CNPJ: {CNPJ}\nRSocial: {RazaoSocial.Trim()}\nData de Abertura da empresa: {DataAbertura.ToString("dd/MM/yyyy")}\nUltima Compra: {UltimaCompra.ToString("dd/MM/yyyy")}\nData de Cadastro: {DataCadastro.ToString("dd/MM/yyyy")}\nSituacao: {Situacao}";
        }
    }
}
