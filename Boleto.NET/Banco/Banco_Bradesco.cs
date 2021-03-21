using Microsoft.VisualBasic;
using System;
using System.Web.UI;

[assembly: WebResource("BoletoNet.Imagens.237.jpg", "image/jpg")]

namespace BoletoNet
{
    /// <author>
    /// Eduardo Frare
    /// Stiven
    /// </author>
    internal class Banco_Bradesco : AbstractBanco, IBanco
    {
        private string _dacNossoNumero = string.Empty;
        private int _dacBoleto = 0;

        /// <summary>
        /// Classe responsavel em criar os campos do Banco Banco_Bradesco.
        /// </summary>
        internal Banco_Bradesco()
        {
            this.Codigo = 237;
            this.Digito = 2;
            this.Nome = "Bradesco";
        }

        #region IBanco Members

        /// <summary>
        /// A linha digit�vel ser� composta por cinco campos:
        ///      1� campo
        ///          composto pelo c�digo de Banco, c�digo da moeda, as cinco primeiras posi��es do campo
        ///          livre e o d�gito verificador deste campo;
        ///      2� campo
        ///          composto pelas posi��es 6� a 15� do campo livre e o d�gito verificador deste campo;
        ///      3� campo
        ///          composto pelas posi��es 16� a 25� do campo livre e o d�gito verificador deste campo;
        ///      4� campo
        ///          composto pelo d�gito verificador do c�digo de barras, ou seja, a 5� posi��o do c�digo de
        ///          barras;
        ///      5� campo
        ///          Composto pelo fator de vencimento com 4(quatro) caracteres e o valor do documento com 10(dez) caracteres, sem separadores e sem edi��o.
        ///
        /// </summary>
        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            //BBBMC.CCCCD1 CCCCC.CCCCCD2 CCCCC.CCCCCD3 D4 FFFFVVVVVVVVVV

            #region Campo 1

            string Grupo1 = string.Empty;

            string BBB = boleto.CodigoBarra.Codigo.Substring(0, 3);
            string M = boleto.CodigoBarra.Codigo.Substring(3, 1);
            string CCCCC = boleto.CodigoBarra.Codigo.Substring(19, 5);
            string D1 = Mod10(BBB + M + CCCCC).ToString();

            Grupo1 = string.Format("{0}{1}{2}.{3}{4} ", BBB, M, CCCCC.Substring(0, 1), CCCCC.Substring(1, 4), D1);

            #endregion Campo 1

            #region Campo 2

            string Grupo2 = string.Empty;

            string CCCCCCCCCC2 = boleto.CodigoBarra.Codigo.Substring(24, 10);
            string D2 = Mod10(CCCCCCCCCC2).ToString();

            Grupo2 = string.Format("{0}.{1}{2} ", CCCCCCCCCC2.Substring(0, 5), CCCCCCCCCC2.Substring(5, 5), D2);

            #endregion Campo 2

            #region Campo 3

            string Grupo3 = string.Empty;

            string CCCCCCCCCC3 = boleto.CodigoBarra.Codigo.Substring(34, 10);
            string D3 = Mod10(CCCCCCCCCC3).ToString();

            Grupo3 = string.Format("{0}.{1}{2} ", CCCCCCCCCC3.Substring(0, 5), CCCCCCCCCC3.Substring(5, 5), D3);

            #endregion Campo 3

            #region Campo 4

            string Grupo4 = string.Empty;

            string D4 = _dacBoleto.ToString();

            Grupo4 = string.Format("{0} ", D4);

            #endregion Campo 4

            #region Campo 5

            string Grupo5 = string.Empty;

            //string FFFF = boleto.CodigoBarra.Codigo.Substring(5, 4);//FatorVencimento(boleto).ToString() ;
            string FFFF = FatorVencimento(boleto).ToString();

            //if (boleto.Carteira == "06" && !Utils.DataValida(boleto.DataVencimento))
            //    FFFF = "0000";

            string VVVVVVVVVV = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);

            //if (Utils.ToInt64(VVVVVVVVVV) == 0)
            //    VVVVVVVVVV = "000";

            Grupo5 = string.Format("{0}{1}", FFFF, VVVVVVVVVV);

            #endregion Campo 5

            boleto.CodigoBarra.LinhaDigitavel = Grupo1 + Grupo2 + Grupo3 + Grupo4 + Grupo5;
        }

        /// <summary>
        ///
        ///   *******
        ///
        ///	O c�digo de barra para cobran�a cont�m 44 posi��es dispostas da seguinte forma:
        ///    01 a 03 - 3 - Identifica��o  do  Banco
        ///    04 a 04 - 1 - C�digo da Moeda
        ///    05 a 05 � 1 - D�gito verificador do C�digo de Barras
        ///    06 a 09 - 4 - Fator de vencimento
        ///    10 a 19 - 10 - Valor
        ///    20 a 44 � 25 - Campo Livre
        ///
        ///   *******
        ///
        /// </summary>
        ///
        public override void FormataCodigoBarra(Boleto boleto)
        {
            string valorBoleto = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            if (boleto.Carteira == "02" || boleto.Carteira == "03" || boleto.Carteira == "09")
            {
                boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", Codigo.ToString(), boleto.Moeda,
                FatorVencimento(boleto), valorBoleto, FormataCampoLivre(boleto));
            }
            else if (boleto.Carteira == "06")
            {
                if (boleto.ValorBoleto == 0)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}0000{2}{3}", Codigo.ToString(), boleto.Moeda,
                        valorBoleto, FormataCampoLivre(boleto));
                }
                else
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}", Codigo.ToString(), boleto.Moeda,
                        FatorVencimento(boleto), valorBoleto, FormataCampoLivre(boleto));
                }
            }

            _dacBoleto = Mod11(boleto.CodigoBarra.Codigo, 9);

            boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + _dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
        }

        ///<summary>
        /// Campo Livre
        ///    20 a 23 -  4 - Ag�ncia Cedente (Sem o digito verificador,completar com zeros a esquerda quandonecess�rio)
        ///    24 a 25 -  2 - Carteira
        ///    26 a 36 - 11 - N�mero do Nosso N�mero(Sem o digito verificador)
        ///    37 a 43 -  7 - Conta do Cedente (Sem o digito verificador,completar com zeros a esquerda quando necess�rio)
        ///    44 a 44	- 1 - Zero
        ///</summary>
        public string FormataCampoLivre(Boleto boleto)
        {
            string FormataCampoLivre = string.Format("{0}{1}{2}{3}{4}", boleto.Cedente.ContaBancaria.Agencia, boleto.Carteira,
                                            boleto.NossoNumero, boleto.Cedente.ContaBancaria.Conta, "0");

            return FormataCampoLivre;
        }

        public override void FormataNumeroDocumento(Boleto boleto)
        {
            throw new NotImplementedException("Fun��o ainda n�o implementada.");
        }

        public override void FormataNossoNumero(Boleto boleto)
        {
            boleto.NossoNumero = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero,
                                            Mod11Bradesco(boleto.Carteira + boleto.NossoNumero, 7));
        }

        public override void ValidaBoleto(Boleto boleto)
        {
            if (boleto.Carteira != "02" && boleto.Carteira != "03" && boleto.Carteira != "06" && boleto.Carteira != "09")
                throw new NotImplementedException("Carteira n�o implementada. Carteiras implementadas 02, 03, 06, 09.");

            //O valor � obrigat�rio para a carteira 03
            if (boleto.Carteira == "03")
            {
                if (boleto.ValorBoleto == 0)
                    throw new NotImplementedException("Para a carteira 03, o valor do boleto n�o pode ser igual a zero");
            }

            //O valor � obrigat�rio para a carteira 09
            if (boleto.Carteira == "09")
            {
                if (boleto.ValorBoleto == 0)
                    throw new NotImplementedException("Para a carteira 09, o valor do boleto n�o pode ser igual a zero");
            }
            //else if (boleto.Carteira == "06")
            //{
            //    boleto.ValorBoleto = 0;
            //}

            //Verifica se o nosso n�mero � v�lido
            if (boleto.NossoNumero.Length > 11)
                throw new NotImplementedException("A quantidade de d�gitos do nosso n�mero, s�o 11 n�meros.");
            else if (boleto.NossoNumero.Length < 11)
                boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);

            //Verificar se a Agencia esta correta
            if (boleto.Cedente.ContaBancaria.Agencia.Length > 4)
                throw new NotImplementedException("A quantidade de d�gitos da Ag�ncia " + boleto.Cedente.ContaBancaria.Agencia + ", s�o de 4 n�meros.");
            else if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
                boleto.Cedente.ContaBancaria.Agencia = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);

            //Verificar se a Conta esta correta
            if (boleto.Cedente.ContaBancaria.Conta.Length > 7)
                throw new NotImplementedException("A quantidade de d�gitos da Conta " + boleto.Cedente.ContaBancaria.Conta + ", s�o de 04 n�meros.");
            else if (boleto.Cedente.ContaBancaria.Conta.Length < 7)
                boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 7);

            //Atribui o nome do banco ao local de pagamento
            boleto.LocalPagamento += Nome + "";

            //Verifica se data do processamento � valida
            if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataProcessamento = DateTime.Now;

            //Verifica se data do documento � valida
            if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
                boleto.DataDocumento = DateTime.Now;

            boleto.QuantidadeMoeda = 0;

            //Atribui o nome do banco ao local de pagamento
            boleto.LocalPagamento = "PAG�VEL PREFERENCIALMENTE NAS AG�NCIAS DO BRADESCO";

            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }

        #endregion IBanco Members

        /// <summary>
        /// Verifica o tipo de ocorr�ncia para o arquivo remessa
        /// </summary>
        public string Ocorrencia(string codigo)
        {
            switch (codigo)
            {
                case "02":
                    return "02-Entrada Confirmada";

                case "03":
                    return "03-Entrada Rejeitada";

                case "06":
                    return "06-Liquida��o  normal";

                case "09":
                    return "09-Baixado Automaticamente via Arquivo";

                case "10":
                    return "10-Baixado conforme instru��es da Ag�ncia";

                case "11":
                    return "11-Em Ser - Arquivo de T�tulos pendentes";

                case "12":
                    return "12-Abatimento Concedido";

                case "13":
                    return "13-Abatimento Cancelado";

                case "14":
                    return "14-Vencimento Alterado";

                case "15":
                    return "15-Liquida��o em Cart�rio";

                case "17":
                    return "17-Liquida��o ap�s baixa ou T�tulo n�o registrado";

                case "18":
                    return "18-Acerto de Deposit�ria";

                case "19":
                    return "19-Confirma��o Recebimento Instru��o de Protesto";

                case "20":
                    return "20-Confirma��o Recebimento Instru��o Susta��o de Protesto";

                case "21":
                    return "21-Acerto do Controle do Participante";

                case "23":
                    return "22-Entrada do T�tulo em Cart�rio";

                case "24":
                    return "23-Entrada rejeitada por CEP Irregular";

                case "27":
                    return "27-Baixa Rejeitada";

                case "28":
                    return "28-D�bito de tarifas/custas";

                case "30":
                    return "30-Altera��o de Outros Dados Rejeitados";

                case "32":
                    return "32-Instru��o Rejeitada";

                case "33":
                    return "33-Confirma��o Pedido Altera��o Outros Dados";

                case "34":
                    return "34-Retirado de Cart�rio e Manuten��o Carteira";

                case "35":
                    return "35-Desagendamento ) d�bito autom�tico";

                case "68":
                    return "68-Acerto dos dados ) rateio de Cr�dito";

                case "69":
                    return "69-Cancelamento dos dados ) rateio";

                default:
                    return "";
            }
        }

        /// <summary>
        /// Verifica o c�digo do motivo da rejei��o informada pelo banco
        /// </summary>
        public string MotivoRejeicao(string codigo)
        {
            switch (codigo)
            {
                case "02":
                    return "02-C�digo do registro detalhe inv�lido";

                case "03":
                    return "03-C�digo da ocorr�ncia inv�lida";

                case "04":
                    return "04-C�digo de ocorr�ncia n�o permitida para a carteira";

                case "05":
                    return "05-C�digo de ocorr�ncia n�o num�rico";

                case "07":
                    return "07-Ag�ncia/conta/Digito - Inv�lido";

                case "08":
                    return "08-Nosso n�mero inv�lido";

                case "09":
                    return "09-Nosso n�mero duplicado";

                case "10":
                    return "10-Carteira inv�lida";

                case "16":
                    return "16-Data de vencimento inv�lida";

                case "18":
                    return "18-Vencimento fora do prazo de opera��o";

                case "20":
                    return "19-Valor do T�tulo inv�lido";

                case "21":
                    return "21-Esp�cie do T�tulo inv�lida";

                case "22":
                    return "22-Esp�cie n�o permitida para a carteira";

                case "24":
                    return "24-Data de emiss�o inv�lida";

                case "38":
                    return "38-Prazo para protesto inv�lido";

                case "44":
                    return "44-Ag�ncia Cedente n�o prevista";

                case "50":
                    return "50-CEP irregular - Banco Correspondente";

                case "63":
                    return "63-Entrada para T�tulo j� cadastrado";

                case "68":
                    return "68-D�bito n�o agendado - erro nos dados de remessa";

                case "69":
                    return "69-D�bito n�o agendado - Sacado n�o consta no cadastro de autorizante";

                case "70":
                    return "70-D�bito n�o agendado - Cedente n�o autorizado pelo Sacado";

                case "71":
                    return "71-D�bito n�o agendado - Cedente n�o participa da modalidade de d�bito autom�tico";

                case "72":
                    return "72-D�bito n�o agendado - C�digo de moeda diferente de R$";

                case "73":
                    return "73-D�bito n�o agendado - Data de vencimento inv�lida";

                case "74":
                    return "74-D�bito n�o agendado - Conforme seu pedido, T�tulo n�o registrado";

                case "75":
                    return "75-D�bito n�o agendado - Tipo de n�mero de inscri��o do debitado inv�lido";

                default:
                    return "";
            }
        }

        private string Mod11Bradesco(string seq, int b)
        {
            #region Trecho do manual layout_cobranca_port.pdf do BRADESCO

            /*
            Para o c�lculo do d�gito, ser� necess�rio acrescentar o n�mero da carteira � esquerda antes do Nosso N�mero,
            e aplicar o m�dulo 11, com base 7.
            Multiplicar cada algarismo que comp�e o n�mero pelo seu respectivo multiplicador (PESO).
            Os multiplicadores(PESOS) variam de 2 a 7.
            O primeiro d�gito da direita para a esquerda dever� ser multiplicado por 2, o segundo por 3 e assim sucessivamente.

              Carteira   Nosso Numero
                ______   _________________________________________
                1    9   0   0   0   0   0   0   0   0   0   0   2
                x    x   x   x   x   x   x   x   x   x   x   x   x
                2    7   6   5   4   3   2   7   6   5   4   3   2
                =    =   =   =   =   =   =   =   =   =   =   =   =
                2 + 63 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 0 + 4 = 69

            O total da soma dever� ser dividido por 11: 69 / 11 = 6 tendo como resto = 3
            A diferen�a entre o divisor e o resto, ser� o d�gito de autoconfer�ncia: 11 - 3 = 8 (d�gito de auto-confer�ncia)

            Se o resto da divis�o for �1�, desprezar o c�lculo de subtra��o e considerar o d�gito como �P�.
            Se o resto da divis�o for �0�, desprezar o c�lculo de subtra��o e considerar o d�gito como �0�.
            */

            #endregion Trecho do manual layout_cobranca_port.pdf do BRADESCO

            /* Vari�veis
             * -------------
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            int s = 0, p = 2;

            for (int i = seq.Length; i > 0; i--)
            {
                s = s + (Convert.ToInt32(Microsoft.VisualBasic.Strings.Mid(seq, i, 1)) * p);
                if (p == b)
                    p = 2;
                else
                    p = p + 1;
            }

            int r = (s % 11);

            if (r == 0)
                return "0";
            else if (r == 1)
                return "P";
            else
                return (11 - r).ToString();
        }

        public override DetalheRetorno LerDetalheRetornoCNAB400(string registro)
        {
            try
            {
                DetalheRetorno detalhe = new DetalheRetorno(registro);

                //Tipo de Inscri��o Empresa
                detalhe.CodigoInscricao = Utils.ToInt32(registro.Substring(1, 2));
                //N� Inscri��o da Empresa
                detalhe.NumeroInscricao = registro.Substring(3, 14);

                //Identifica��o da Empresa Cedente no Banco
                detalhe.Agencia = Utils.ToInt32(registro.Substring(24, 6));
                detalhe.Conta = Utils.ToInt32(registro.Substring(30, 7));
                detalhe.DACConta = Utils.ToInt32(registro.Substring(36, 1));

                //N� Controle do Participante
                detalhe.NumeroControle = registro.Substring(37, 25);
                //Identifica��o do T�tulo no Banco
                detalhe.NossoNumero = registro.Substring(70, 12);
                detalhe.DACNossoNumero = Utils.ToInt32(registro.Substring(81, 1));
                //Carteira
                detalhe.Carteira = registro.Substring(107, 1);
                //Identifica��o de Ocorr�ncia
                detalhe.CodigoOcorrencia = Utils.ToInt32(registro.Substring(108, 2));

                //N�mero do Documento
                detalhe.NumeroDocumento = registro.Substring(116, 10);
                //Identifica��o do T�tulo no Banco
                detalhe.IdentificacaoTitulo = registro.Substring(126, 20);

                //Valor do T�tulo
                double valorTitulo = Convert.ToInt64(registro.Substring(152, 13));
                detalhe.ValorTitulo = valorTitulo / 100;
                //Banco Cobrador
                detalhe.CodigoBanco = Utils.ToInt32(registro.Substring(165, 3));
                //Ag�ncia Cobradora
                detalhe.AgenciaCobradora = Utils.ToInt32(registro.Substring(168, 5));
                //Esp�cie do T�tulo
                detalhe.Especie = Utils.ToInt32(registro.Substring(173, 2));
                //Despesas de cobran�a para os C�digos de Ocorr�ncia (Valor Despesa)
                double valorDespesa = Convert.ToUInt64(registro.Substring(175, 13));
                detalhe.ValorDespesa = valorDespesa / 100;
                //Outras despesas Custas de Protesto (Valor Outras Despesas)
                double valorOutrasDespesas = Convert.ToUInt64(registro.Substring(188, 13));
                detalhe.ValorOutrasDespesas = valorOutrasDespesas / 100;
                // IOF
                double iof = Convert.ToUInt64(registro.Substring(214, 13));
                detalhe.IOF = iof / 100;
                //Abatimento Concedido sobre o T�tulo (Valor Abatimento Concedido)
                double valorAbatimento = Convert.ToUInt64(registro.Substring(227, 13));
                detalhe.ValorAbatimento = valorAbatimento / 100;
                //Desconto Concedido (Valor Desconto Concedido)
                double valorDesconto = Convert.ToUInt64(registro.Substring(240, 13));
                detalhe.Descontos = valorDesconto / 100;
                //Valor Pago
                double valorPago = Convert.ToUInt64(registro.Substring(253, 13));
                detalhe.ValorPago = valorPago / 100;
                //Juros Mora
                double jurosMora = Convert.ToUInt64(registro.Substring(266, 13));
                detalhe.JurosMora = jurosMora / 100;
                //Outros Cr�ditos
                double outrosCreditos = Convert.ToUInt64(registro.Substring(279, 13));
                detalhe.OutrosCreditos = outrosCreditos / 100;
                //Motivo do C�digo de Ocorr�ncia 19 (Confirma��o de Instru��o de Protesto)
                detalhe.MotivoCodigoOcorrencia = registro.Substring(294, 1);

                //Data Ocorr�ncia no Banco
                int dataOcorrencia = Utils.ToInt32(registro.Substring(110, 6));
                detalhe.DataOcorrencia = Utils.ToDateTime(dataOcorrencia.ToString("##-##-##"));
                //Data Vencimento do T�tulo
                int dataVencimento = Utils.ToInt32(registro.Substring(146, 6));
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                // Data do Cr�dito
                int dataCredito = Utils.ToInt32(registro.Substring(295, 6));
                detalhe.DataCredito = Utils.ToDateTime(dataCredito.ToString("##-##-##"));

                //Origem Pagamento
                detalhe.OrigemPagamento = registro.Substring(301, 3);

                //Motivos das Rejei��es para os C�digos de Ocorr�ncia
                detalhe.MotivosRejeicao = registro.Substring(318, 10);
                //N�mero do Cart�rio
                detalhe.NumeroCartorio = Utils.ToInt32(registro.Substring(365, 2));
                //N�mero do Protocolo
                detalhe.NumeroProtocolo = registro.Substring(365, 2);
                //Nome do Sacado
                detalhe.NomeSacado = "";

                detalhe.NumeroSequencial = Utils.ToInt32(registro.Substring(394, 6));

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }
    }
}