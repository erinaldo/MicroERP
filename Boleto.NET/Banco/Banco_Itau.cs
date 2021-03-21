using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Web.UI;

[assembly: WebResource("BoletoNet.Imagens.341.jpg", "image/jpg")]

namespace BoletoNet
{
    /// <summary>
    /// Classe referente ao banco Ita�
    /// </summary>
    internal class Banco_Itau : AbstractBanco, IBanco
    {
        #region Vari�veis

        private int _dacBoleto = 0;
        private int _dacNossoNumero = 0;

        #endregion Vari�veis

        #region Construtores

        internal Banco_Itau()
        {
            try
            {
                this.Codigo = 341;
                this.Digito = 7;
                this.Nome = "Ita�";
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }

        #endregion Construtores

        #region M�todos de Inst�ncia

        /// <summary>
        /// Valida��es particulares do banco Ita�
        /// </summary>
        public override void ValidaBoleto(Boleto boleto)
        {
            try
            {
                //Carteiras v�lidas
                int[] cv = new int[] { 175, 176, 178, 109, 198, 107, 122, 142, 143, 196, 126, 131, 146, 150, 169, 157 };//Flavio(fhlviana@hotmail.com) - adicionado a carteira 109
                bool valida = false;

                foreach (int c in cv)
                    if (Utils.ToString(boleto.Carteira) == Utils.ToString(c))
                        valida = true;

                if (!valida)
                {
                    StringBuilder carteirasImplementadas = new StringBuilder(100);

                    carteirasImplementadas.Append(". Carteiras implementadas: ");
                    foreach (int c in cv)
                    {
                        carteirasImplementadas.AppendFormat(" {0}", c);
                    }
                    throw new NotImplementedException("Carteira n�o implementada: " + boleto.Carteira + carteirasImplementadas.ToString());
                }
                //Verifica se o tamanho para o NossoNumero s�o 8 d�gitos
                if (Convert.ToInt32(boleto.NossoNumero).ToString().Length > 8)
                    throw new NotImplementedException("A quantidade de d�gitos do nosso n�mero para a carteira " + boleto.Carteira + ", s�o 8 n�meros.");
                else if (Convert.ToInt32(boleto.NossoNumero).ToString().Length < 8)
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 8);

                //� obrigat�rio o preenchimento do n�mero do documento
                if (boleto.Carteira == "106" || boleto.Carteira == "107"
                        || boleto.Carteira == "122" || boleto.Carteira == "142"
                        || boleto.Carteira == "143" || boleto.Carteira == "195"
                        || boleto.Carteira == "196" || boleto.Carteira == "198"
                        || boleto.Carteira == "157")
                {
                    if (Utils.ToInt32(boleto.NumeroDocumento) == 0)
                        throw new NotImplementedException("O n�mero do documento n�o pode ser igual a zero.");
                }

                //Formato o n�mero do documento
                if (Utils.ToInt32(boleto.NumeroDocumento) > 0)
                    boleto.NumeroDocumento = Utils.FormatCode(boleto.NumeroDocumento, 7);

                // Calcula o DAC do Nosso N�mero a maioria das carteiras
                // agencia/conta/carteira/nosso numero
                if (boleto.Carteira != "126" && boleto.Carteira != "131"
                    && boleto.Carteira != "146" && boleto.Carteira != "150"
                    && boleto.Carteira != "168")
                    _dacNossoNumero = Mod10(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta + boleto.Carteira + boleto.NossoNumero);
                else
                    // Excess�o 126 - 131 - 146 - 150 - 168
                    // carteira/nosso numero
                    _dacNossoNumero = Mod10(boleto.Carteira + boleto.NossoNumero);

                // Calcula o DAC da Conta Corrente
                boleto.Cedente.ContaBancaria.DigitoConta = Mod10(boleto.Cedente.ContaBancaria.Agencia + boleto.Cedente.ContaBancaria.Conta).ToString();
                //Atribui o nome do banco ao local de pagamento
                boleto.LocalPagamento += Nome + ". Ap�s o vencimento, somente no ITA�";

                //Verifica se o nosso n�mero � v�lido
                if (Utils.ToInt64(boleto.NossoNumero) == 0)
                    throw new NotImplementedException("Nosso n�mero inv�lido");

                //Verifica se data do processamento � valida
                if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
                    boleto.DataProcessamento = DateTime.Now;

                //Verifica se data do documento � valida
                if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
                    boleto.DataDocumento = DateTime.Now;

                boleto.FormataCampos();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao validar boletos.", e);
            }
        }

        # endregion

        # region M�todos de formata��o do boleto

        public override void FormataCodigoBarra(Boleto boleto)
        {
            try
            {
                // C�digo de Barras
                //banco & moeda & fator & valor & carteira & nossonumero & dac_nossonumero & agencia & conta & dac_conta & "000"

                string valorBoleto = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
                valorBoleto = Utils.FormatCode(valorBoleto, 10);

                string numeroDocumento = Utils.FormatCode(boleto.NumeroDocumento.ToString(), 7);
                string codigoCedente = Utils.FormatCode(boleto.Cedente.Codigo.ToString(), 5);

                if (boleto.Carteira == "175" || boleto.Carteira == "176" || boleto.Carteira == "178" || boleto.Carteira == "109")//Flavio(fhlviana@hotmail.com) - Adicionado carteira 109
                {
                    boleto.CodigoBarra.Codigo =
                        string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}000", Codigo, boleto.Moeda,
                                      FatorVencimento(boleto), valorBoleto, boleto.Carteira,
                                      boleto.NossoNumero, _dacNossoNumero, boleto.Cedente.ContaBancaria.Agencia,//Flavio(fhlviana@hotmail.com) => Cedente.ContaBancaria.Agencia --> boleto.Cedente.ContaBancaria.Agencia
                                      Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 5), boleto.Cedente.ContaBancaria.DigitoConta);//Flavio(fhlviana@hotmail.com) => Cedente.ContaBancaria.DigitoConta --> boleto.Cedente.ContaBancaria.DigitoConta
                }
                else if (boleto.Carteira == "198" || boleto.Carteira == "107"
                         || boleto.Carteira == "122" || boleto.Carteira == "142"
                         || boleto.Carteira == "143" || boleto.Carteira == "196")
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}0", Codigo, boleto.Moeda,
                        FatorVencimento(boleto), valorBoleto, boleto.Carteira,
                        boleto.NossoNumero, numeroDocumento, codigoCedente,
                        Mod10(boleto.Carteira + boleto.NossoNumero + numeroDocumento + codigoCedente));
                }

                _dacBoleto = Mod11(boleto.CodigoBarra.Codigo, 9, 0);

                boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + _dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar c�digo de barras.", ex);
            }
        }

        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            try
            {
                string numeroDocumento = Utils.FormatCode(boleto.NumeroDocumento.ToString(), 7);
                string codigoCedente = Utils.FormatCode(boleto.Cedente.Codigo.ToString(), 5);

                string AAA = Utils.FormatCode(Codigo.ToString(), 3);
                string B = boleto.Moeda.ToString();
                string CCC = boleto.Carteira.ToString();
                string DD = boleto.NossoNumero.Substring(0, 2);
                string X = Mod10(AAA + B + CCC + DD).ToString();
                string LD = string.Empty; //Linha Digit�vel

                string DDDDDD = boleto.NossoNumero.Substring(2, 6);

                string K = string.Format(" {0} ", _dacBoleto);

                string UUUU = FatorVencimento(boleto).ToString();
                string VVVVVVVVVV = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");

                string C1 = string.Empty;
                string C2 = string.Empty;
                string C3 = string.Empty;
                string C5 = string.Empty;

                #region AAABC.CCDDX

                C1 = string.Format("{0}{1}{2}.", AAA, B, CCC.Substring(0, 1));
                C1 += string.Format("{0}{1}{2} ", CCC.Substring(1, 2), DD, X);

                #endregion AAABC.CCDDX

                #region UUUUVVVVVVVVVV

                VVVVVVVVVV = Utils.FormatCode(VVVVVVVVVV, 10);
                C5 = UUUU + VVVVVVVVVV;

                #endregion UUUUVVVVVVVVVV

                if (boleto.Carteira == "175" || boleto.Carteira == "176" || boleto.Carteira == "178" || boleto.Carteira == "109")//Flavio(fhlviana@hotmail.com) - adicionado carteira 109
                {
                    #region Defini��es

                    /* AAABC.CCDDX.DDDDD.DEFFFY.FGGGG.GGHHHZ.K.UUUUVVVVVVVVVV
              * ------------------------------------------------------
              * Campo 1
              * AAABC.CCDDX
              * AAA - C�digo do Banco
              * B   - Moeda
              * CCC - Carteira
              * DD  - 2 primeiros n�meros Nosso N�mero
              * X   - DAC Campo 1 (AAABC.CCDD) Mod10
              *
              * Campo 2
              * DDDDD.DEFFFY
              * DDDDD.D - Restante Nosso N�mero
              * E       - DAC (Ag�ncia/Conta/Carteira/Nosso N�mero)
              * FFF     - Tr�s primeiros da ag�ncia
              * Y       - DAC Campo 2 (DDDDD.DEFFF) Mod10
              *
              * Campo 3
              * FGGGG.GGHHHZ
              * F       - Restante da Ag�ncia
              * GGGG.GG - N�mero Conta Corrente + DAC
              * HHH     - Zeros (N�o utilizado)
              * Z       - DAC Campo 3
              *
              * Campo 4
              * K       - DAC C�digo de Barras
              *
              * Campo 5
              * UUUUVVVVVVVVVV
              * UUUU       - Fator Vencimento
              * VVVVVVVVVV - Valor do T�tulo
              */

                    #endregion Defini��es

                    #region DDDDD.DEFFFY

                    string E = _dacNossoNumero.ToString();
                    string FFF = boleto.Cedente.ContaBancaria.Agencia.Substring(0, 3);
                    string Y = Mod10(DDDDDD + E + FFF).ToString();

                    C2 = string.Format("{0}.", DDDDDD.Substring(0, 5));
                    C2 += string.Format("{0}{1}{2}{3} ", DDDDDD.Substring(5, 1), E, FFF, Y);

                    #endregion DDDDD.DEFFFY

                    #region FGGGG.GGHHHZ

                    string F = boleto.Cedente.ContaBancaria.Agencia.Substring(3, 1);
                    string GGGGGG = boleto.Cedente.ContaBancaria.Conta + boleto.Cedente.ContaBancaria.DigitoConta;
                    string HHH = "000";
                    string Z = Mod10(F + GGGGGG + HHH).ToString();

                    C3 = string.Format("{0}{1}.{2}{3}{4}", F, GGGGGG.Substring(0, 4), GGGGGG.Substring(4, 2), HHH, Z);

                    #endregion FGGGG.GGHHHZ
                }
                else if (boleto.Carteira == "198" || boleto.Carteira == "107"
                     || boleto.Carteira == "122" || boleto.Carteira == "142"
                     || boleto.Carteira == "143" || boleto.Carteira == "196")
                {
                    #region Defini��es

                    /* AAABC.CCDDX.DDDDD.DEEEEY.EEEFF.FFFGHZ.K.UUUUVVVVVVVVVV
              * ------------------------------------------------------
              * Campo 1 - AAABC.CCDDX
              * AAA - C�digo do Banco
              * B   - Moeda
              * CCC - Carteira
              * DD  - 2 primeiros n�meros Nosso N�mero
              * X   - DAC Campo 1 (AAABC.CCDD) Mod10
              *
              * Campo 2 - DDDDD.DEEEEY
              * DDDDD.D - Restante Nosso N�mero
              * EEEE    - 4 primeiros numeros do n�mero do documento
              * Y       - DAC Campo 2 (DDDDD.DEEEEY) Mod10
              *
              * Campo 3 - EEEFF.FFFGHZ
              * EEE     - Restante do n�mero do documento
              * FFFFF   - C�digo do Cliente
              * G       - DAC (Carteira/Nosso Numero(sem DAC)/Numero Documento/Codigo Cliente)
              * H       - zero
              * Z       - DAC Campo 3
              *
              * Campo 4 - K
              * K       - DAC C�digo de Barras
              *
              * Campo 5 - UUUUVVVVVVVVVV
              * UUUU       - Fator Vencimento
              * VVVVVVVVVV - Valor do T�tulo
              */

                    #endregion Defini��es

                    #region DDDDD.DEEEEY

                    string EEEE = numeroDocumento.Substring(0, 4);
                    string Y = Mod10(DDDDDD + EEEE).ToString();

                    C2 = string.Format("{0}.", DDDDDD.Substring(0, 5));
                    C2 += string.Format("{0}{1}{2} ", DDDDDD.Substring(5, 1), EEEE, Y);

                    #endregion DDDDD.DEEEEY

                    #region EEEFF.FFFGHZ

                    string EEE = numeroDocumento.Substring(4, 3);
                    string FFFFF = codigoCedente;
                    string G = Mod10(boleto.Carteira + boleto.NossoNumero + numeroDocumento + codigoCedente).ToString();
                    string H = "0";
                    string Z = Mod10(EEE + FFFFF + G + H).ToString();
                    C3 = string.Format("{0}{1}.{2}{3}{4}{5}", EEE, FFFFF.Substring(0, 2), FFFFF.Substring(2, 3), G, H, Z);

                    #endregion EEEFF.FFFGHZ
                }
                else if (boleto.Carteira == "126" || boleto.Carteira == "131" || boleto.Carteira == "146" || boleto.Carteira == "150" || boleto.Carteira == "168")
                {
                    throw new NotImplementedException("Fun��o n�o implementada.");
                }

                boleto.CodigoBarra.LinhaDigitavel = C1 + C2 + C3 + K + C5;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar linha digit�vel.", ex);
            }
        }

        public override void FormataNossoNumero(Boleto boleto)
        {
            try
            {
                boleto.NossoNumero = string.Format("{0}/{1}-{2}", boleto.Carteira, boleto.NossoNumero, _dacNossoNumero);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar nosso n�mero", ex);
            }
        }

        public override void FormataNumeroDocumento(Boleto boleto)
        {
            try
            {
                boleto.NumeroDocumento = string.Format("{0}-{1}", boleto.NumeroDocumento, Mod10(boleto.NumeroDocumento));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao formatar n�mero do documento.", ex);
            }
        }

        # endregion

        # region M�todos de gera��o do arquivo remessa CNAB400

        # region HEADER

        /// <summary>
        /// HEADER do arquivo CNAB
        /// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo)
        {
            try
            {
                string _header = " ";

                base.GerarHeaderRemessa("0", cedente, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _header = GerarHeaderRemessaCNAB240();
                        break;

                    case TipoArquivo.CNAB400:
                        _header = GerarHeaderRemessaCNAB400(0, cedente);
                        break;

                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a gera��o do HEADER do arquivo de REMESSA.", ex);
            }
        }

        public string GerarHeaderRemessaCNAB240()
        {
            throw new NotImplementedException("Fun��o n�o implementada.");
        }

        public string GerarHeaderRemessaCNAB400(int numeroConvenio, Cedente cedente)
        {
            try
            {
                string complemento = new string(' ', 294);
                string _header;

                _header = "01REMESSA01COBRANCA       ";
                _header += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 4, 4, '0', 0, true, true, true);
                _header += "00";
                _header += Utils.FitStringLength(cedente.ContaBancaria.Conta, 5, 5, '0', 0, true, true, true);
                _header += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
                _header += "        ";
                _header += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
                _header += "341";
                _header += "BANCO ITAU SA  ";
                _header += DateTime.Now.ToString("ddMMyy");
                _header += complemento;
                _header += "000001";

                _header = Utils.SubstituiCaracteresEspeciais(_header);

                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
            }
        }

        # endregion

        # region DETALHE

        /// <summary>
        /// DETALHE do arquivo CNAB
        /// Gera o DETALHE do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _detalhe = GerarDetalheRemessaCNAB240();
                        break;

                    case TipoArquivo.CNAB400:
                        _detalhe = GerarDetalheRemessaCNAB400(boleto, numeroRegistro, tipoArquivo);
                        break;

                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a gera��o do DETALHE arquivo de REMESSA.", ex);
            }
        }

        public string GerarDetalheRemessaCNAB240()
        {
            throw new NotImplementedException("Fun��o n�o implementada.");
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                // USO DO BANCO - Identifica��o da opera��o no Banco (posi��o 87 a 107)
                string identificaOperacaoBanco = new string(' ', 21);
                string usoBanco = new string(' ', 10);
                string nrDocumento = new string(' ', 25);
                string sNossoNumero = string.Empty;
                string _detalhe;

                _detalhe = "1";

                // Tipo de inscri��o da empresa

                // Normalmente definem o tipo (CPF/CNPJ) e o n�mero de inscri��o do cedente.
                // Se o t�tulo for negociado, dever�o ser utilizados para indicar o CNPJ/CPF do sacador
                // (cedente original), uma vez que os cart�rios exigem essa informa��o para efetiva��o
                // dos protestos. Para este fim, tamb�m poder� ser utilizado o registro tipo �5�.
                // 01 - CPF DO CEDENTE
                // 02 - CNPJ DO CEDENTE
                // 03 - CPF DO SACADOR
                // 04 - CNPJ DO SACADOR
                // O arquivo gerado pelo aplicativo do Banco ITA�, sempre atriubuiu 04 para o tipo de inscri��o da empresa

                if (boleto.Cedente.CPFCNPJ.Length <= 11)
                    _detalhe += "03";
                else
                    _detalhe += "04";
                _detalhe += Utils.FitStringLength(boleto.Sacado.CPFCNPJ.ToString(), 14, 14, '0', 0, true, true, true);
                _detalhe += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia.ToString(), 4, 4, '0', 0, true, true, true);
                _detalhe += "00";
                _detalhe += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta.ToString(), 5, 5, '0', 0, true, true, true);
                _detalhe += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta.ToString(), 1, 1, ' ', 0, true, true, false);
                _detalhe += "    "; // Complemento do registro - 4 posi��es em branco

                // C�digo da instru��o/alega��o a ser cancelada

                // Deve ser preenchido na remessa somente quando utilizados, na posi��o 109-110, os c�digos de ocorr�ncia 35 �
                // Cancelamento de Instru��o e 38 � Cedente n�o concorda com alega��o do sacado. Para os demais c�digos de
                // ocorr�ncia este campo dever� ser preenchido com zeros.
                //Obs.: No arquivo retorno ser� informado o mesmo c�digo da instru��o cancelada, e para o cancelamento de alega��o
                // de sacado n�o h� retorno da informa��o.

                // Por enquanto o objetivo � apenas gerar o arquivo de remessa e n�o utilizar o arquivo para enviar instru��es
                // para t�tulos que j� est�o no banco, portanto o campo ser� preenchido com zeros.
                _detalhe += "0000";

                _detalhe += nrDocumento; // Utils.FitStringLength(boleto.NumeroDocumento, 25, 25, ' ', 0, true, true, false); //Identifica��o do t�tulo na empresa

                sNossoNumero = boleto.NossoNumero.Replace(boleto.Carteira + "/", string.Empty);
                sNossoNumero = sNossoNumero.Substring(0, sNossoNumero.Length - 2);
                _detalhe += Utils.FitStringLength(sNossoNumero, 8, 8, '0', 0, true, true, true);
                // Quantidade de moeda vari�vel - Preencher com zeros se a moeda for REAL
                // O manual do Banco ITA� n�o diz como preencher caso a moeda n�o seja o REAL
                if (boleto.Moeda == 9)
                    _detalhe += "0000000000000";

                _detalhe += Utils.FitStringLength(boleto.Carteira, 3, 3, '0', 0, true, true, true);
                _detalhe += Utils.FitStringLength(identificaOperacaoBanco, 21, 21, ' ', 0, true, true, true);
                // C�digo da carteira
                if (boleto.Moeda == 9)
                    _detalhe += "I"; //O c�digo da carteira s� muda para dois tipos, quando a cobran�a for em d�lar

                _detalhe += "01"; // Identifica��o da ocorr�ncia - 01 REMESSA
                _detalhe += Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, ' ', 0, true, true, false);
                _detalhe += boleto.DataVencimento.ToString("ddMMyy");
                _detalhe += Utils.FitStringLength(Convert.ToString(boleto.ValorBoleto * 100), 13, 13, '0', 0, true, true, true);
                _detalhe += "341";
                _detalhe += "00000"; // Ag�ncia onde o t�tulo ser� cobrado - no arquivo de remessa, preencher com ZEROS

                _detalhe += Utils.FitStringLength(EspecieDocumento.ValidaCodigo(boleto.EspecieDocumento).ToString(), 2, 2, '0', 0, true, true, true);
                _detalhe += "N"; // Identifica��o de t�tulo, Aceito ou N�o aceito

                //A data informada neste campo deve ser a mesma data de emiss�o do t�tulo de cr�dito
                //(Duplicata de Servi�o / Duplicata Mercantil / Nota Fiscal, etc), que deu origem a esta Cobran�a.
                //Existindo diverg�ncia, na exist�ncia de protesto, a documenta��o poder� n�o ser aceita pelo Cart�rio.
                _detalhe += boleto.DataDocumento.ToString("ddMMyy");

                if (boleto.Instrucoes.Count == 0)
                {
                    _detalhe += "    ";
                }
                else
                {
                    if (boleto.Instrucoes.Count >= 1)
                        _detalhe += Utils.FitStringLength(boleto.Instrucoes[0].Codigo.ToString(), 2, 2, '0', 0, true, true, true);

                    if (boleto.Instrucoes.Count >= 2)
                        _detalhe += Utils.FitStringLength(boleto.Instrucoes[1].Codigo.ToString(), 2, 2, '0', 0, true, true, true);
                    else
                        _detalhe += "  ";
                }

                // Juros de 1 dia
                //Se o cliente optar pelo padr�o do Banco Ita� ou solicitar o cadastramento permanente na conta corrente,
                //n�o haver� a necessidade de informar esse valor.
                //Caso seja expresso em moeda vari�vel, dever� ser preenchido com cinco casas decimais.

                _detalhe += "0000000000000";

                // Data limite para desconto
                _detalhe += boleto.DataVencimento.ToString("ddMMyy");
                _detalhe += Utils.FitStringLength(boleto.ValorDesconto.ToString(), 13, 13, '0', 0, true, true, true);
                _detalhe += "0000000000000"; // Valor do IOF
                _detalhe += "0000000000000"; // Valor do Abatimento

                if (boleto.Cedente.CPFCNPJ.Length <= 11)
                    _detalhe += "01";  // CPF
                else
                    _detalhe += "02"; // CNPJ

                _detalhe += Utils.FitStringLength(boleto.Sacado.CPFCNPJ, 14, 14, '0', 0, true, true, true).ToUpper();
                _detalhe += Utils.FitStringLength(boleto.Sacado.Nome, 30, 30, ' ', 0, true, true, false);
                _detalhe += usoBanco;
                _detalhe += Utils.FitStringLength((boleto.Sacado.Endereco.End + " " + boleto.Sacado.Endereco.Numero + " - " + boleto.Sacado.Endereco.Complemento), 40, 40, ' ', 0, true, true, true).ToUpper();
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro, 12, 12, ' ', 0, true, true, false).ToUpper();
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.CEP, 8, 8, ' ', 0, true, true, false).ToUpper(); ;
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade, 15, 15, ' ', 0, true, true, false).ToUpper();
                _detalhe += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
                // SACADOR/AVALISTA
                // Normalmente deve ser preenchido com o nome do sacador/avalista. Alternativamente este campo poder�
                // ter dois outros usos:
                // a) 2o e 3o descontos: para de operar com mais de um desconto(depende de cadastramento pr�vio do
                // indicador 19.0 pelo Banco Ita�, conforme item 5)
                // b) Mensagens ao sacado: se utilizados as instru��es 93 ou 94 (Nota 11), transcrever a mensagem desejada
                _detalhe += Utils.FitStringLength(boleto.Sacado.Nome, 30, 30, ' ', 0, true, true, false).ToUpper();
                _detalhe += "    "; // Complemento do registro
                _detalhe += boleto.DataVencimento.ToString("ddMMyy");
                // PRAZO - Quantidade de DIAS - ver nota 11(A) - depende das instru��es de cobran�a
                _detalhe += "00";
                _detalhe += " "; // Complemento do registro
                _detalhe += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);

                _detalhe = Utils.SubstituiCaracteresEspeciais(_detalhe);

                return _detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
            }
        }

        # endregion DETALHE

        # region TRAILER

        /// <summary>
        /// TRAILER do arquivo CNAB
        /// Gera o TRAILER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _trailer = " ";

                base.GerarTrailerRemessa(numeroRegistro, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _trailer = GerarTrailerRemessa240();
                        break;

                    case TipoArquivo.CNAB400:
                        _trailer = GerarTrailerRemessa400(numeroRegistro);
                        break;

                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _trailer;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public string GerarTrailerRemessa240()
        {
            throw new NotImplementedException("Fun��o n�o implementada.");
        }

        public string GerarTrailerRemessa400(int numeroRegistro)
        {
            try
            {
                string complemento = new string(' ', 393);
                string _trailer;

                _trailer = "9";
                _trailer += complemento;
                _trailer += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true); // N�mero sequencial do registro no arquivo.

                _trailer = Utils.SubstituiCaracteresEspeciais(_trailer);

                return _trailer;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a gera��o do registro TRAILER do arquivo de REMESSA.", ex);
            }
        }

        # endregion

        #endregion M�todos de Inst�ncia
    }
}