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

            public struct DadosDeUsuario_S
            {
                public string Nome;
                public DateTime DataDeNascimento;
                public string NomeDaRua;
                public uint NumeroDaCasa;
                public string NumeroDocumento;
            }
            public enum Resultado_E
            {
                Sucesso = 0,
                Sair = 1,
                Excecao = 2
            }
            //métodos:
            public static void ImprimeNoConsole(string mensagem)
            {
                Console.WriteLine(mensagem);
            }
            public static Resultado_E PegaString(ref string minhaString, string mensagemNoMenu)
            {
                string temp = "";

                Resultado_E retorno;
                do
                {
                    Console.WriteLine(mensagemNoMenu);
                    temp = Console.ReadLine();
                    if (temp == string.Empty)
                    {
                        ImprimeNoConsole("Nenhum nome digitado!!");
                        Console.ReadKey();
                    }
                    Console.Clear();
                } while (string.IsNullOrEmpty(temp));

                if (temp == "s" || temp == "S")
                    retorno = Resultado_E.Sair;
                else
                {
                    minhaString = temp;
                    retorno = Resultado_E.Sucesso;
                }
                Console.Clear();
                return retorno;
            }
            public static Resultado_E PegaData(ref DateTime data, string mensagemNoMenu)
            {
                Resultado_E retorno;
                do
                {
                    try
                    {
                        Console.WriteLine(mensagemNoMenu);
                        string temp = Console.ReadLine();
                        if (temp == "s" || temp == "S")

                            retorno = Resultado_E.Sair;
                        else
                        {
                            data = Convert.ToDateTime(temp);
                            retorno = Resultado_E.Sucesso;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exceção: " + ex.Message);
                        ImprimeNoConsole("Presione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        retorno = Resultado_E.Excecao;
                    }

                } while (retorno == Resultado_E.Excecao);
                Console.Clear();
                return retorno;
            }
            public static Resultado_E PegaNumero_Uint32(ref uint numeroCasa, string mensagemNoMenu)
            {
                Resultado_E retorno;
                do
                {
                    try
                    {
                        Console.WriteLine(mensagemNoMenu);
                        string temp = Console.ReadLine();
                        if (temp == "s" || temp == "S")

                            retorno = Resultado_E.Sair;
                        else
                        {
                            numeroCasa = Convert.ToUInt32(temp);
                            retorno = Resultado_E.Sucesso;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exeção: " + ex.Message);
                        ImprimeNoConsole("Presione qualquer tecla para continuar");
                        Console.ReadKey();
                        Console.Clear();
                        retorno = Resultado_E.Excecao;
                    }

                } while (retorno == Resultado_E.Excecao);
                Console.Clear();
                return retorno;
            }
            public static Resultado_E CadastraUsuario(ref List<DadosDeUsuario_S> ListaUsuario_M)
            {
                DadosDeUsuario_S cadastroDoUsuario;
                cadastroDoUsuario.Nome = "";
                cadastroDoUsuario.DataDeNascimento = new DateTime();
                cadastroDoUsuario.NomeDaRua = "";
                cadastroDoUsuario.NumeroDaCasa = 0;
                cadastroDoUsuario.NumeroDocumento = "";

                if (PegaString(ref cadastroDoUsuario.Nome, "Digite o nome completo ou S para sair") == Resultado_E.Sair)
                    return Resultado_E.Sair;
                if (PegaData(ref cadastroDoUsuario.DataDeNascimento, "Digite a data de nascimento no formato DD/MM/YYYY ou S para sair") == Resultado_E.Sair)
                    return Resultado_E.Sair;
                if (PegaString(ref cadastroDoUsuario.NomeDaRua, "Digite o nome da rua ou S para sair") == Resultado_E.Sair)
                    return Resultado_E.Sair;
                if (PegaNumero_Uint32(ref cadastroDoUsuario.NumeroDaCasa, "Digite o número da casa ou S para sair") == Resultado_E.Sair)
                    return Resultado_E.Sair;
                if (PegaString(ref cadastroDoUsuario.NumeroDocumento, "Digite o número do documento ou S para sair") == Resultado_E.Sair)
                    return Resultado_E.Sair;
                    ListaUsuario_M.Add(cadastroDoUsuario);
                    GravaDados(caminhoDados, ListaUsuario_M);
                return Resultado_E.Sucesso;
            }
            public static void GravaDados(string caminho, List<DadosDeUsuario_S> ListaUsuarios)
            {
                try
                {
                    string exportaCadastro = "";
                    foreach (DadosDeUsuario_S dadosCadastro in ListaUsuarios)
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
            public static void CarregaDados(string caminho, ref List<DadosDeUsuario_S> ListaUsuarios)
            {
                try
                {
                    if (File.Exists(caminho))
                    {
                        string[] conteudoRead = File.ReadAllLines(caminho);
                        DadosDeUsuario_S dadosDoUsuario;
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
            public static void BuscaUserDoc(List<DadosDeUsuario_S> ListaUsuarios)
            {
                string temp = "";
                do
                {
                    ImprimeNoConsole("Digite o número do documento para buscar o usúario ou S para sair");
                    temp = Console.ReadLine();
                    if (temp == string.Empty)
                    {
                        ImprimeNoConsole("Nenhum documento digitado!!");
                        Console.ReadKey();
                    }
                    Console.Clear();
                } while (string.IsNullOrEmpty(temp));
                Console.Clear();

                if (temp.ToLower() == "s")
                    return;
                else
                {
                    List<DadosDeUsuario_S> ListaUsuariosTemp = ListaUsuarios.Where( user => user.NumeroDocumento == temp).ToList();
                    if (ListaUsuariosTemp.Count > 0)
                    {
                        ImprimeNoConsole("DOCUMENTO_ENCONTRADO: "+ ListaUsuariosTemp.Count);
                        foreach(DadosDeUsuario_S userExiste in ListaUsuariosTemp)
                        {
                            ImprimeNoConsole(tgNome + userExiste.Nome.ToUpper());
                            ImprimeNoConsole(tgDtNascimento + userExiste.DataDeNascimento.ToString("dd/MM/yyyy"));
                            ImprimeNoConsole(tgNumeroDocumento + userExiste.NumeroDocumento);
                            ImprimeNoConsole(tgNomeDaRua + userExiste.NomeDaRua);
                            ImprimeNoConsole(tgNumeroCasa + userExiste.NumeroDaCasa);
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        ImprimeNoConsole("Nenhum usúario encontrado com o documento: "+ temp);
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
        }
            public static void ExcluiUserDoc(string caminho, ref List<DadosDeUsuario_S> ListaUsuarios)
            {
                string temp = "";
                bool ExcluidoSucesso = false;
                int result = 0;
                do
                {
                    ImprimeNoConsole("Para Excluir um usúario digite o número do documento ou S para sair");
                    temp = Console.ReadLine();
                    if (temp == string.Empty)
                    {
                      
                        ImprimeNoConsole("Nenhum documento digitado!!");
                           
                        Console.ReadKey();
                    }
                    Console.Clear();
                } while (int.TryParse(temp, out result));
                if (temp.ToLower() == "s")
                    return;
                else
                {
                    List<DadosDeUsuario_S> ListaUsuariosTemp =  ListaUsuarios.Where(user => user.NumeroDocumento == temp).ToList();
                    if (ListaUsuariosTemp.Count > 0)
                    { foreach(DadosDeUsuario_S excluir in ListaUsuariosTemp)
                        {
                            ListaUsuarios.Remove(excluir);
                            ExcluidoSucesso = true;
                        }
                    if (ExcluidoSucesso)
                        {
                            GravaDados(caminho, ListaUsuarios);
                            foreach (DadosDeUsuario_S userExcluido in ListaUsuariosTemp)
                            {
                                ImprimeNoConsole("Usuário: " + tgNome + userExcluido.Nome.ToUpper());
                                ImprimeNoConsole(tgNumeroDocumento + userExcluido.NumeroDocumento.ToUpper() +
                                                "\n\rFoi Excluído com sucesso!".ToUpper());
                            }
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        ImprimeNoConsole("Nenhum usúario encontrado com o documento: " + temp);
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            static void Main(string[] args)
            {
                List<DadosDeUsuario_S> ListaUsuarios = new List<DadosDeUsuario_S>();
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
                    ImprimeNoConsole("Digite C para cadastrar um novo usuário");
                    ImprimeNoConsole("Digite B para buscar um usuário");
                    ImprimeNoConsole("Digite E para excluir um usuário");
                    ImprimeNoConsole("Digite S para sair");
                    opcaoEscolhida = Console.ReadKey(true).KeyChar.ToString().ToLower();
                    Console.Clear();
                    
                    if (opcaoEscolhida == "s")
                    {
                        ImprimeNoConsole("Fim do programa");
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
                        BuscaUserDoc( ListaUsuarios);
                    }
                    else if (opcaoEscolhida == "e")
                    {
                        //excluir
                        ExcluiUserDoc(caminhoDados, ref ListaUsuarios);
                    }
                    else
                    {
                        ImprimeNoConsole("Opção Invalida!");
                        Console.Clear();
                    }
                } while (opcaoEscolhida != "s");
            }
        }
    }
}
