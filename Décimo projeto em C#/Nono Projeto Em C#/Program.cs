using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oitavo_projeto_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dadosDoUsuario usuarioDados = new dadosDoUsuario("localDoArquivo.xml");
            interfaceDoUsuario usuarioInteface = new interfaceDoUsuario(usuarioDados);
            usuarioInteface.iniciarAplicacao();
        }
    }
}
