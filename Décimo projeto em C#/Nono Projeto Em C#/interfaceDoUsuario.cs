using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace oitavo_projeto_C_
{
    internal class interfaceDoUsuario
    {
        public enum saida
        {
            Sucesso = 0,
            Sair = 1,
            Excecao = 2
        }

        public void Exibirmensagem(string Exibirmensagem)
        {
            Console.Write(Exibirmensagem);
            Console.WriteLine("Aperte qualquer coisa pra prosseguir");
            Console.ReadKey(true);
            Console.Clear();
        }

        public saida digitoUsuario(ref string digito, string mensagem)
        {
            saida retornar;
            Console.WriteLine(mensagem);
            string variavelTemporaria = Console.ReadLine();
            if (variavelTemporaria == "d" || variavelTemporaria == "D")
                retornar = saida.Sair;
            else
            {
                digito = variavelTemporaria;
                retornar = saida.Sucesso;
            }
            Console.Clear();
            return retornar;
        }

        //método pra data
        public saida dataUsuario(ref DateTime data, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        data = Convert.ToDateTime(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Exibirmensagem("Exceção: " + ex.Message);
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        //método pra Uint32
        public saida idadeUsuario(ref UInt32 idade, string mensagem)
        {
            saida retornar;
            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string variavelTemporaria = Console.ReadLine();
                    if (variavelTemporaria == "d" || variavelTemporaria == "D")
                        retornar = saida.Sair;
                    else
                    {
                        idade = Convert.ToUInt32(variavelTemporaria);
                        retornar = saida.Sucesso;
                    }
                }
                catch (Exception ex)
                {
                    Exibirmensagem("Exceção: " + ex.Message);
                    retornar = saida.Excecao;
                }
            } while (retornar == saida.Excecao);
            Console.Clear();
            return retornar;
        }

        //atributos 
        dadosDoUsuario baseDadosUsuario;

        //Construtor da aplicação

        public interfaceDoUsuario(dadosDoUsuario parametroDadosDoUsuario)
        {
            baseDadosUsuario = parametroDadosDoUsuario;
        }

        //métodos adicionais
        public void mostrarNaTela(cadastroDoUsuario usuarioCadastrado)
        {
            Console.WriteLine("Nome: " + usuarioCadastrado.usuarioNome);
            Console.WriteLine("Idade: " + usuarioCadastrado.usuarioIdade);
            Console.WriteLine("Data de nascimento: " + usuarioCadastrado.usuarioNascimento);
            Console.WriteLine("Nome da mãe: " + usuarioCadastrado.usuarioMae);
            Console.WriteLine("Número do telefone: " + usuarioCadastrado.usuarioTelefone);
        }

        public void aparecerNoConsole(List<cadastroDoUsuario> parametroListaUsuario)
        {
            foreach (cadastroDoUsuario cadastrado in parametroListaUsuario)
            {
                mostrarNaTela(cadastrado);
            }
        }

        public saida registraUsuario()
        {
            Console.Clear();
            string nome = "";
            UInt32 idade = 0;
            DateTime dataDeNascimento = new DateTime();
            string nomeDaMae = "";
            string numeroDoTelefone = "";

            if (digitoUsuario(ref nome, "Digite seu nome completo ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (idadeUsuario(ref idade, "Digite sua idade ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (dataUsuario(ref dataDeNascimento, "Digite sua data de nascimento no formato (DD/MM/AAAA) ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            if (digitoUsuario(ref nomeDaMae, "Digite o nome da sua mãe ou D para sair") == saida.Sair)
                return saida.Sair;
            if (digitoUsuario(ref numeroDoTelefone, "Digite seu número de telefone ou pressione D para sair") == saida.Sair)
                return saida.Sair;
            cadastroDoUsuario registrarPessoa = new cadastroDoUsuario(nome, idade, dataDeNascimento, nomeDaMae, numeroDoTelefone);
            baseDadosUsuario.adicionarIndividuo(registrarPessoa);

            Console.Clear();
            Console.WriteLine("Adicionando usuário:");
            mostrarNaTela(registrarPessoa);
            Exibirmensagem("");

            return saida.Sucesso;
        }

        public void procurarPessoa()
        {
            Console.Clear();
            Console.WriteLine("Escreva aqui o número do telefone do usuário ou pressione D para sair:");

            string temporaria = Console.ReadLine();
            if (temporaria.ToLower() == "d")
                return;
            List<cadastroDoUsuario> listaDeUsuariosTemporaria = baseDadosUsuario.procurarUsuarioTelefone(temporaria);
            Console.Clear();
            if (listaDeUsuariosTemporaria != null)
            {
                Console.WriteLine(" Usuário(s) com o número de telefone de " + temporaria + " encontrado(s)");
                aparecerNoConsole(listaDeUsuariosTemporaria);
            }
            else
            {
                Console.WriteLine(" Não foi possível encontrar o usuário desejado. Verifique se nenhuma tecla foi pressionada errada e tente novamente. ");
                Exibirmensagem("");
            }
        }

        public void excluirPessoa()
        {
            Console.Clear();
            Console.WriteLine("Escreva aqui o número do telefone do usuário ou pressione D para sair:");

            string temporaria = Console.ReadLine();
            if (temporaria.ToLower() == "d")
                return;
            List<cadastroDoUsuario> listaDeUsuariosTemporaria = baseDadosUsuario.removerUsuarioTelefone(temporaria);
            Console.Clear();
            if (listaDeUsuariosTemporaria != null)
            {
                Console.WriteLine(" Usuário(s) com o número de telefone de " + temporaria + " removido(s)");
                aparecerNoConsole(listaDeUsuariosTemporaria);
            }
            else
            {
                Console.WriteLine(" Não foi possível remover o usuário desejado. Verifique se nenhuma tecla foi pressionada errada e tente novamente. ");
                Exibirmensagem("");
            }
        }
        public void sairDaAplicacao()
        {
            Console.Clear();
            Exibirmensagem("A aplicação será encerrada em breve. ");
        }

        public void opcaoDesconhecida()
        {
            Console.Clear();
            Exibirmensagem("Opção desconhecida. Por favor, verifique a tecla pressionada e tente novamente. ");
        }

        public void iniciarAplicacao()
        {
            string temporaria;
            do
            {
                Console.WriteLine("Digite R para registrar um novo usuário. ");
                Console.WriteLine("Digite P para procurar um usuário já cadastrado. ");
                Console.WriteLine("Digite A para apagar um usuário já cadastrado. ");
                Console.WriteLine("Digite H para mostrar a data e a hora. ");
                Console.WriteLine("Digite D para sair da aplicação. ");
                temporaria = Console.ReadKey(true).KeyChar.ToString().ToLower();
                switch (temporaria)
                {
                    case "r":
                        //registra usuário
                        if (baseDadosUsuario.disponibiliadeDaBase() == false)
                        {
                            Exibirmensagem("Base indisponível, tente novamente em alguns segundos.");
                            break;
                        }
                        registraUsuario();
                        break;
                    case "p":
                        //procura usuário
                        procurarPessoa();
                        break;
                    case "a":
                        //apaga usuário
                        excluirPessoa();
                        break;
                    case "h":
                        //mostra data e hora
                        Exibirmensagem(DateTime.Now.ToString());
                        break;
                    case "d":
                        //sai da aplicação
                        sairDaAplicacao();
                        break;
                    default:
                        opcaoDesconhecida();
                        break;
                }

            } while (temporaria != "d");
        }
    }
}
