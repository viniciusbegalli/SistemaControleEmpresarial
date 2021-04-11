using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaControleEmpresarial
{
    public class Dominio
    {
        public class TipoOperacaoLog
        {
            private TipoOperacaoLog() { }
            public const string Inclusao = "Inclusao";
            public const string Edicao = "Edicao";
            public const string Exclusao = "Exclusao";
        }

        public class SituacaoAjustePonto
        {
            private SituacaoAjustePonto() { }
            public const string Pendente = "Pendente";
            public const string Aprovada = "Aprovada";
            public const string Reprovada = "Reprovada";
        }
        public class SituacaoSolicitacaoJornada
        {
            private SituacaoSolicitacaoJornada() { }
            public const string Pendente = "Pendente";
            public const string Aprovada = "Aprovada";
            public const string Recusada = "Recusada";
        }

        public class SituacaoVaga
        {
            private SituacaoVaga() { }
            public const string Aberta = "Aberta";
            public const string Fechada = "Fechada";
        }

        public class SituacaoChamado
        {
            private SituacaoChamado() { }
            public const string Aberto = "Aberto";
            public const string Fechado = "Fechado";
        }

        public class SituacaoTreinamento
        {
            private SituacaoTreinamento() { }
            public const string Ativo = "Ativo";
            public const string Cancelado = "Cancelado";
        }

        public class FilaChamado
        {
            private FilaChamado() { }
            public const string Analista = "Analista";
            public const string Supervisor = "Supervisor";
            public const string Gerente = "Gerente";
            public const string Administrador = "Administrador";
        }
        public class Prioridade
        {
            private Prioridade() { }
            public const string Baixa = "Baixa";
            public const string Media = "Media";
            public const string Alta = "Alta";
        }

        public class IDPrioridade
        {
            private IDPrioridade() { }
            public const int Baixa = 3;
            public const int Media = 2;
            public const int Alta = 1;
        }
    }
}
