using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Decimo_Projeto;
using System.Threading;

namespace oitavo_projeto_C_
{

    [DataContract]

    internal class dadosDoUsuario
    {
        //atributos da aplicação
        [DataMember]
        private List<cadastroDoUsuario> cadastrarUsuario;
        private string localDosDados;

        private Mutex mutexArquivo;
        private Mutex mutexLista;
        private bool baseDisponivel;

        public void adicionarIndividuo(cadastroDoUsuario cUsuario)
        {
            mutexLista.WaitOne();
                cadastrarUsuario.Add(cUsuario);
            mutexLista.ReleaseMutex();
            new Thread (()=>
            {
                baseDisponivel = false;
                mutexArquivo.WaitOne();
                Serializador.serializando(localDosDados, this);
                mutexArquivo.ReleaseMutex();
                baseDisponivel = true;
            }).Start ();
        }
        public List<cadastroDoUsuario> procurarUsuarioTelefone(string pUsuario)
        {
            mutexLista.WaitOne ();
            List<cadastroDoUsuario> listaUsuarioTemporaria = cadastrarUsuario.Where(x => x.usuarioTelefone == pUsuario).ToList();
            mutexLista.ReleaseMutex ();
            if (listaUsuarioTemporaria.Count > 0)
            {
                return listaUsuarioTemporaria;
            }
            else
            {
                return null;
            }
        }

        public List<cadastroDoUsuario> removerUsuarioTelefone(string pUsuario)
        {
            mutexLista.WaitOne ();
            List<cadastroDoUsuario> listaUsuarioTemporaria = cadastrarUsuario.Where(x => x.usuarioTelefone == pUsuario).ToList();
            mutexLista.ReleaseMutex ();
            if (listaUsuarioTemporaria.Count > 0)
            {
                foreach (cadastroDoUsuario usuario in listaUsuarioTemporaria)
                {
                    mutexLista.WaitOne();
                    cadastrarUsuario.Remove(usuario);
                    mutexLista.ReleaseMutex();
                }
                new Thread(() =>
                {
                    baseDisponivel = false;
                    mutexArquivo.WaitOne();
                    Serializador.serializando(localDosDados, this);
                    mutexArquivo.ReleaseMutex();
                    baseDisponivel = true;
                }).Start();
                return listaUsuarioTemporaria;
            }
            else
                return null;
        }

        public bool disponibiliadeDaBase()
        {
            return baseDisponivel;
        }

        //construtor dos dados do usuário

        public dadosDoUsuario(string localDosDados)
        {
            mutexLista = new Mutex();
            mutexArquivo = new Mutex();
            baseDisponivel = true;

             this.localDosDados = localDosDados;

            new Thread(() =>
            {
                baseDisponivel = false;
                mutexArquivo.WaitOne();
                dadosDoUsuario dadosDoUsuarioTemporaria = Serializador.desserializar(localDosDados);
                mutexArquivo.ReleaseMutex();
                    mutexLista.WaitOne();
                if (dadosDoUsuarioTemporaria != null)
                    cadastrarUsuario = dadosDoUsuarioTemporaria.cadastrarUsuario;
                else
                    cadastrarUsuario = new List<cadastroDoUsuario>();
                mutexLista.ReleaseMutex();
                baseDisponivel = true;
            }).Start();
        }
    }
}
