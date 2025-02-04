using System;
using CadastrosBasicos.ManipularBanco;

namespace CadastrosBasicos
{

    public class MenuCadastros
    {
        public static LeituraCliente leituraCliente = new LeituraCliente();
        public static EscritaCliente escritaCliente = new EscritaCliente();

        public static void SubMenu()
        {
            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== CADASTROS ===============");
                Console.WriteLine("1. Clientes / Fornecedores");
                Console.WriteLine("2. Produtos");
                Console.WriteLine("3. Mat�rias-Primas");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch(escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        SubMenuClientesFornecedores();
                        break;

                    case "2":
                        new Produto().Menu();
                        break;

                    case "3":
                        new MPrima().Menu();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Op��o inv�lida");
                        Console.WriteLine("\nPressione ENTER para voltar ao menu");
                        break;
                }

            }while(escolha != "0");

        }

        public static void SubMenuClientesFornecedores()
        {
            string escolha;

            do
            {
                Console.Clear();

                Console.WriteLine("=============== CLIENTES / FORNECEDORES ===============");
                Console.WriteLine("1. Cadastar cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Editar registro de cliente");
                Console.WriteLine("4. Bloquear/Desbloqueia cliente (Inadimplente)");
                Console.WriteLine("5. Localizar cliente");
                Console.WriteLine("6. Localizar cliente bloqueado");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("7. Cadastar fornecedor");
                Console.WriteLine("8. Listar fornecedores");
                Console.WriteLine("9. Editar registro de fornecedor");
                Console.WriteLine("10. Bloquear/Desbloqueia fornecedor");
                Console.WriteLine("11. Localizar fornecedor");
                Console.WriteLine("12. Localizar fornecedor bloqueado");
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine("0. Voltar ao menu anterior");
                Console.Write("\nEscolha: ");

                switch (escolha = Console.ReadLine())
                {
                    case "0":
                        break;

                    case "1":
                        NovoCliente();
                        break;

                    case "2":
                        new Cliente().Navegar();
                        break;

                    case "3":
                        new Cliente().Editar();
                        break;

                    case "4":
                        new Cliente().BloqueiaCadastro();
                        
                        break;

                    case "5":
                        new Cliente().Localizar();
                        break;

                    case "6":
                        new Cliente().ClientesBloqueados();
                        break;

                    case "7":
                        NovoFornecedor();
                        break;

                    case "8":
                        new Fornecedor().Navegar();
                        break;
                    case "9":
                        new Fornecedor().Editar();
                        break;
                    case "10":
                        new Fornecedor().BloqueiaFornecedor();
                        break;
                    case "11":
                        new Fornecedor().Localizar();
                        break;
                    case "12":
                        new Fornecedor().FornecedorBloqueado();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Op��o inv�lida");
                        Console.WriteLine("\n Pressione ENTER para voltar ao menu");
                        break;
                }

            } while (escolha != "0");
        }

        public static void NovoCliente()
        {
            Console.Clear();

            bool flag;

            DateTime dNascimento;

            do
            {
                Console.Write("Data de nascimento: ");
                flag = DateTime.TryParse(Console.ReadLine(), out dNascimento);
            } while (flag != true);
            if (Validacoes.CalculaData(dNascimento))
            {
                RegistrarCliente(dNascimento);
            }
            else
            {
                Console.WriteLine("Menor de 18 anos nao pode ser cadastrado");
                Console.ReadKey();
            }
                
        }

        public static void NovoFornecedor()
        {
            Console.Clear();

            bool flag;

            DateTime dCriacao;

            do
            {
                Console.Write("Data de criacao da empresa:");
                flag = DateTime.TryParse(Console.ReadLine(), out dCriacao);
            } while (flag != true);
            if (Validacoes.CalculaCriacao(dCriacao))
            {
                Fornecedor fornecedor = RegistrarFornecedor(dCriacao);
                new EscritaFornecedor().GravarNovoFornecedor(fornecedor);
            }
            else
            {
                Console.WriteLine("Empresa com menos de 6 meses nao deve ser cadastrada");
                Console.WriteLine("Pressione enter para continuar");
                Console.ReadKey();
            }
        }

        public static Fornecedor RegistrarFornecedor(DateTime dFundacao)
        {
            string rSocial, cnpj;
            char situacao;
            do
            {
                Console.Write("CNPJ: ");
                cnpj = Console.ReadLine();
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            } while (Validacoes.ValidarCnpj(cnpj) == false);
            Fornecedor f = new LeituraFornecedor().ProcurarFornecedor(cnpj);
            if (f.CNPJ == null)
            {
                Console.Write("Razao social: ");
                rSocial = Console.ReadLine().Trim();
                Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                situacao = char.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Fornecedor ja cadastrado");
                Console.WriteLine("Pressione enter para continuar");
                Console.ReadKey();
                return f;
            }
            return new Fornecedor(cnpj, rSocial, dFundacao, situacao);

        }
        public static Cliente RegistrarCliente(DateTime dNascimento)
        {
            string nome, cpf;
            char situacao, sexo;
            do
            {
                Console.Write("CPF: ");
                cpf = Console.ReadLine();
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");

            } while (Validacoes.ValidarCpf(cpf) == false);
            Cliente c = leituraCliente.ProcurarCliente(cpf);

            if (c.CPF == null)
            {
                Console.Write("Nome: ");
                nome = Console.ReadLine().Trim();
                Console.Write("Genero (M - Masculino/ F - Feminino): ");
                sexo = char.Parse(Console.ReadLine());
                Console.Write("Situacao (A - Ativo/ I - Inativo): ");
                situacao = char.Parse(Console.ReadLine());
                escritaCliente.GravarNovoCliente(new Cliente(cpf, nome, dNascimento, sexo, situacao));
            }
            else
            {
                Console.WriteLine("Cliente ja cadastrado!!");
                Console.ReadKey();
                return c;
            }
            return null;
        }
    }
}