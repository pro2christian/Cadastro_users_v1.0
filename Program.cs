using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Projeto_para_estudos
{
    class Program
    {

        class CadastroUsers
        {
            static string deLimitdorInicio;
            static string deLimitdorFim;
            static string tgNome;
            static string tgDtNascimento;
            static string tgNomeDaRua;
            static string tgNumeroCasa;
            static string tgNumeroDocumento;
            static string caminhoDados;

            public struct DadosDeUsuario_e
            {
                public string Nome;
                public DateTime DataDeNascimento;
                public string NomeDaRua;
                public uint NumeroDaCasa;
                public string NumeroDocumento;
            }
            public enum Resultado_e
            {
                Sucesso = 0,
                Sair = 1,
                Excecao = 2
            }
            //métodos:
            public static void ImprimeMensagens(string mensagem)
            {
                Console.WriteLine(mensagem);
            }
            public static Resultado_e PegaString(ref string minhaString, string mensagemNoMenu)
            {
                string temp = "";

                Resultado_e retorno;
                do
                {
                    Console.WriteLine(mensagemNoMenu);
                    temp = Console.ReadLine();
                    if (temp == string.Empty)
                    {
                        ImprimeMensagens("Nenhum nome digitado!!");
                        Console.ReadKey();
                    }
                    Console.Clear();
                } while (string.IsNullOrEmpty(temp));

                if (temp == "s" || temp == "S")
                    retorno = Resultado_e.Sair;
                else
                {
                    minhaString = temp;
                    retorno = Resultado_e.Sucesso;
                }
                Console.Clear();
                return retorno;
            }
            public static Resultado_e PegaData(ref DateTime data, string mensagemNoMenu)
            {
                Resultado_e retorno;
                do
                {
                    try
                    {
                        Console.WriteLine(mensagemNoMenu);
                        string temp = Console.ReadLine();
                        if (temp == "s" || temp == "S")

                            retorno = Resultado_e.Sair;
                        else
                        {
                            data = Convert.ToDateTime(temp);
                            retorno = Resultado_e.Sucesso;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exceção: " + ex.Message);
                        ImprimeMensagens("Presione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        retorno = Resultado_e.Excecao;
                    }

                } while (retorno == Resultado_e.Excecao);
                Console.Clear();
                return retorno;
            }
            public static Resultado_e PegaNumeroCasa(ref uint numeroCasa, string mensagemNoMenu)
            {
                Resultado_e retorno;
                do
                {
                    try
                    {
                        Console.WriteLine(mensagemNoMenu);
                        string temp = Console.ReadLine();
                        if (temp == "s" || temp == "S")

                            retorno = Resultado_e.Sair;
                        else
                        {
                            numeroCasa = Convert.ToUInt32(temp);
                            retorno = Resultado_e.Sucesso;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exeção: " + ex.Message);
                        ImprimeMensagens("Presione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        retorno = Resultado_e.Excecao;
                    }

                } while (retorno == Resultado_e.Excecao);
                Console.Clear();
                return retorno;
            }
            public static Resultado_e CadastraUsuario(ref List<DadosDeUsuario_e> ListaUsuario_m)
            {
                DadosDeUsuario_e cadastroDoUsuario;
                cadastroDoUsuario.Nome = "";
                cadastroDoUsuario.DataDeNascimento = new DateTime();
                cadastroDoUsuario.NomeDaRua = "";
                cadastroDoUsuario.NumeroDaCasa = 0;
                cadastroDoUsuario.NumeroDocumento = "";

                if (PegaString(ref cadastroDoUsuario.Nome, "Digite o nome completo ou S para sair") == Resultado_e.Sair)
                    return Resultado_e.Sair;
                if (PegaData(ref cadastroDoUsuario.DataDeNascimento, "Digite a data de nascimento no formato DD/MM/YYYY ou S para sair") == Resultado_e.Sair)
                    return Resultado_e.Sair;
                if (PegaString(ref cadastroDoUsuario.NomeDaRua, "Digite o nome da rua ou S para sair") == Resultado_e.Sair)
                    return Resultado_e.Sair;
                if (PegaNumeroCasa(ref cadastroDoUsuario.NumeroDaCasa, "Digite o número da casa ou S para sair") == Resultado_e.Sair)
                    return Resultado_e.Sair;
                if (PegaString(ref cadastroDoUsuario.NumeroDocumento, "Digite o número do documento ou S para sair") == Resultado_e.Sair)
                    ListaUsuario_m.Add(cadastroDoUsuario);
                    GravaDados(caminhoDados, ListaUsuario_m);
                return Resultado_e.Sucesso;
            }
            public static void GravaDados(string caminho, List<DadosDeUsuario_e> ListaUsuarios)
            {
                try
                {
                    string exportaCadastro = "";
                    foreach (DadosDeUsuario_e dadosCadastro in ListaUsuarios)
                    {
                        exportaCadastro += deLimitdorInicio + "\r\n";
                        exportaCadastro += tgNome + dadosCadastro.Nome + "\r\n";
                        exportaCadastro += tgDtNascimento + dadosCadastro.DataDeNascimento + "\r\n";
                        exportaCadastro += tgNumeroDocumento + dadosCadastro.NumeroDocumento + "\r\n";
                        exportaCadastro += tgNomeDaRua + dadosCadastro.NomeDaRua + "\r\n";
                        exportaCadastro += tgNumeroCasa + dadosCadastro.NumeroDaCasa + "\r\n";
                        exportaCadastro += deLimitdorFim + "\r\n";

                    }
                    File.WriteAllText(caminho, exportaCadastro);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEÇÃO: " + ex.Message);
                }

            }
            public static void CarregaDados(string caminho, ref List<DadosDeUsuario_e> ListaUsuarios)
            {
                try
                {
                    if (File.Exists(caminho))
                    {
                        string[] conteudoRead = File.ReadAllLines(caminho);
                        DadosDeUsuario_e dadosDoUsuario;
                        dadosDoUsuario.Nome = "";
                        dadosDoUsuario.DataDeNascimento = new DateTime();
                        dadosDoUsuario.NomeDaRua = "";
                        dadosDoUsuario.NumeroDaCasa = 0;
                        dadosDoUsuario.NumeroDocumento = "";
                        foreach (string lines in conteudoRead)
                        {
                            if (lines.Contains(deLimitdorInicio))
                                continue;
                            if (lines.Contains(deLimitdorFim))
                                ListaUsuarios.Add(dadosDoUsuario);
                            if (lines.Contains(tgNome))
                                dadosDoUsuario.Nome = lines.Replace(tgNome, "");
                            if (lines.Contains(tgDtNascimento))
                                dadosDoUsuario.DataDeNascimento = Convert.ToDateTime(lines.Replace(tgDtNascimento, ""));
                            if (lines.Contains(tgNumeroDocumento))
                                dadosDoUsuario.NumeroDocumento = Convert.ToString(lines.Replace(tgNumeroDocumento, ""));
                            if (lines.Contains(tgNomeDaRua))
                                dadosDoUsuario.NomeDaRua = lines.Replace(tgNomeDaRua, "");
                            if (lines.Contains(tgNumeroCasa))
                                dadosDoUsuario.NumeroDaCasa = Convert.ToUInt32(lines.Replace(tgNumeroCasa, ""));
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEÇÃO: " + ex.Message);
                }
            }
            
            static void Main(string[] args)
            {
                List<DadosDeUsuario_e> ListaUsuarios = new List<DadosDeUsuario_e>();
                string opcaoEscolhida = "";
                deLimitdorInicio = "          ##### INICIO #####";
                deLimitdorFim = "          ##### FIM #####";
                tgNome = "NOME: ";
                tgDtNascimento = "DATA_DE_NASCIMENTO: ";
                tgNumeroDocumento = "NÚMERO_DO_DOCUMENTO: ";
                tgNomeDaRua = "NOME_DA_RUA: ";
                tgNumeroCasa = "NÚMERO_DA_CASA: ";
                caminhoDados = @"data_source.txt";
                CarregaDados(caminhoDados, ref ListaUsuarios);

                do
                {
                    ImprimeMensagens("Digite C para cadastrar um novo usuário");
                    ImprimeMensagens("Digite B para buscar um usuário");
                    ImprimeMensagens("Digite E para excluir um usuário");
                    ImprimeMensagens("Digite S para sair");
                    opcaoEscolhida = Console.ReadKey(true).KeyChar.ToString().ToLower();

                    Console.Clear();
                    if (opcaoEscolhida == "s")
                    {
                        ImprimeMensagens("Fim do programa");
                        Console.ReadKey();
                    }
                    else if (opcaoEscolhida == "c")
                    {
                        //cadastra usuario
                        CadastraUsuario(ref ListaUsuarios);
                            
                    }
                    else if (opcaoEscolhida == "b")
                    {
                        //buscar
                    }
                    else if (opcaoEscolhida == "e")
                    {
                        //excluir
                    }
                    else
                    {
                        ImprimeMensagens("Opção Invalida!");
                        Console.Clear();
                    }

                } while (opcaoEscolhida != "s");

            }
        }
    }

}
