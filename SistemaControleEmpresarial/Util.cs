using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaControleEmpresarial.Controllers
{
    public class Util : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public static bool ValidarCPF(string CPF)
        {
            if (CPF.Length < 14)
            {
                return false;
            }
            return true;
        }

        public static string CriptografarSenha(string senha)
        {
            StringBuilder senhaCriptografada = new StringBuilder();
            MD5 md5Hash = MD5.Create();

            // Criptografa o valor passado
            byte[] valorCriptografado = md5Hash.ComputeHash(Encoding.Default.GetBytes(senha));

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                senhaCriptografada.Append(valorCriptografado[i].ToString("x2"));
            }

            return senhaCriptografada.ToString();
        }

        // Verifica se o valor é compativel com o hash passado
        public static bool VerificarSenhaInformada(string valor, string valorCriptografado)
        {
            // Criptografamos o valor passado como parâmetro
            // utilizando o mesmo método citado no artigo anterior
            string NovoValorCriptografado = CriptografarSenha(valor);

            // Criamos uma StringComparer para compararmos os dois hashs gerados
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            //Se os valores forem iguais, retorna true, ou seja, o valor
            // passado corresponde ao Hash
            if (comparer.Compare(NovoValorCriptografado, valorCriptografado) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}