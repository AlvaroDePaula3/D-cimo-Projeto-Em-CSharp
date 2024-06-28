using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace oitavo_projeto_C_
{
    [DataContract] 
    internal class cadastroDoUsuario
    {
        [DataMember]
        private string nomeDoUsuario;
        [DataMember]
        private UInt32 idadeDoUsuario;
        [DataMember]
        private DateTime dataDeNascimento;
        [DataMember]
        private string nomeDaMae;
        [DataMember]
        private string numeroDoTelefone;

        public string usuarioNome
        {
            get { return nomeDoUsuario; }
            set { nomeDoUsuario = value; }
        }
        public UInt32 usuarioIdade
        {
            get { return idadeDoUsuario; }
            set { idadeDoUsuario = value; }
        }
        public DateTime usuarioNascimento
        {
            get { return dataDeNascimento; }
            set { dataDeNascimento = value; }
        }
        public string usuarioMae
        {
            get { return nomeDaMae; }
            set { nomeDaMae = value; }
        }

        public string usuarioTelefone
        {
            get { return numeroDoTelefone; }
            set { numeroDoTelefone = value; }
        }

        public cadastroDoUsuario(string nomeDoUsuario, uint idadeDoUsuario, DateTime dataDeNascimento, string nomeDaMae, string numeroDeTelefone)
        {
            this.nomeDoUsuario = nomeDoUsuario;
            this.idadeDoUsuario = idadeDoUsuario;
            this.dataDeNascimento = dataDeNascimento;
            this.nomeDaMae = nomeDaMae;
            this.numeroDoTelefone = numeroDeTelefone;

        }
    }
}

