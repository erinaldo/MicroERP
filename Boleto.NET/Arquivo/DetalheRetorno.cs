using System;

namespace BoletoNet
{
    public class DetalheRetorno
    {
        #region Vari�veis

        private int _codigoInscricao = 0;
        private string _numeroInscricao = string.Empty;
        private int _conta = 0;
        private int _codigoBanco = 0;
        private int _dacConta = 0;
        private string _usoEmpresa = string.Empty;
        private int _dacNossoNumero = 0;
        private string _carteira = string.Empty;
        private int _codigoOcorrencia = 0;
        private int _confirmacaoNossoNumero = 0;
        private double _valorTitulo = 0;
        private int _agenciaCobradora = 0;
        private int _dacAgenciaCobradora = 0;
        private int _especie = 0;
        private double _tarifaCobranca = 0;
        private double _valorAbatimento = 0;
        private double _valorPrincipal = 0;
        private double _jurosMora = 0;
        private DateTime _dataCredito = new DateTime(1, 1, 1);
        private int _instrucaoCancelada = 0;
        private string _nomeSacado = string.Empty;
        private string _erros = string.Empty;
        private string _codigoLiquidacao = string.Empty;
        private int _numeroSequencial = 0;
        private string _registro = string.Empty;
        private double _valorDespesa = 0;
        private double _valorOutrasDespesas = 0;
        private string _origemPagamento = string.Empty;
        private string _motivoCodigoOcorrencia = string.Empty;
        private string _identificacaoTitulo = string.Empty;
        private string _motivosRejeicao = string.Empty;
        private int _numeroCartorio = 0;
        private string _numeroProtocolo = string.Empty;
        private string _numeroControle = string.Empty;

        #region Propriedades BRB

        private int _identificacaoDoRegistro = 0;
        private int _tipoInscricao = 0;
        private string _cgcCpf = string.Empty;
        private int _contaCorrente = 0;
        private string _nossoNumero = string.Empty;
        private string _seuNumero = string.Empty;
        private int _instrucao = 0;
        private DateTime _dataOcorrencia = new DateTime(1, 1, 1);
        private string _numeroDocumento = string.Empty;
        private int _codigoRateio = 0;
        private DateTime _dataVencimento = new DateTime(1, 1, 1);
        private double _valoTitulo = 0;
        private int _bancoCobrador = 0;
        private int _agencia = 0;
        private string _especieTitulo = string.Empty;
        private double _despeasaDeCobranca = 0;
        private double _outrasDespesas = 0;
        private double _juros = 0;
        private double _iof = 0;
        private double _abatimentos = 0;
        private double _descontos = 0;
        private double _valorPago = 0;
        private double _outrosDebitos = 0;
        private double _outrosCreditos = 0;
        private DateTime _dataLiquidacao = new DateTime(1, 1, 1);
        private int _sequencial = 0;

        #endregion Propriedades BRB

        #endregion Vari�veis

        #region Construtores

        public DetalheRetorno()
        {
        }

        public DetalheRetorno(string registro)
        {
            _registro = registro;
        }

        #endregion Construtores

        #region Propriedades

        public int CodigoInscricao
        {
            get { return _codigoInscricao; }
            set { _codigoInscricao = value; }
        }

        public string NumeroInscricao
        {
            get { return _numeroInscricao; }
            set { _numeroInscricao = value; }
        }

        public int Agencia
        {
            get { return _agencia; }
            set { _agencia = value; }
        }

        public int Conta
        {
            get { return _conta; }
            set { _conta = value; }
        }

        public int DACConta
        {
            get { return _dacConta; }
            set { _dacConta = value; }
        }

        public string UsoEmpresa
        {
            get { return _usoEmpresa; }
            set { _usoEmpresa = value; }
        }

        public string NossoNumero
        {
            get { return _nossoNumero; }
            set { _nossoNumero = value; }
        }

        public int DACNossoNumero
        {
            get { return _dacNossoNumero; }
            set { _dacNossoNumero = value; }
        }

        public string Carteira
        {
            get { return _carteira; }
            set { _carteira = value; }
        }

        public int CodigoOcorrencia
        {
            get { return _codigoOcorrencia; }
            set { _codigoOcorrencia = value; }
        }

        public DateTime DataOcorrencia
        {
            get { return _dataOcorrencia; }
            set { _dataOcorrencia = value; }
        }

        public string NumeroDocumento
        {
            get { return _numeroDocumento; }
            set { _numeroDocumento = value; }
        }

        public int ConfirmacaoNossoNumero
        {
            get { return _confirmacaoNossoNumero; }
            set { _confirmacaoNossoNumero = value; }
        }

        public DateTime DataVencimento
        {
            get { return _dataVencimento; }
            set { _dataVencimento = value; }
        }

        public double ValorTitulo
        {
            get { return _valorTitulo; }
            set { _valorTitulo = value; }
        }

        public int CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        public int AgenciaCobradora
        {
            get { return _agenciaCobradora; }
            set { _agenciaCobradora = value; }
        }

        public int DACAgenciaCobradora
        {
            get { return _dacAgenciaCobradora; }
            set { _dacAgenciaCobradora = value; }
        }

        public int Especie
        {
            get { return _especie; }
            set { _especie = value; }
        }

        public double TarifaCobranca
        {
            get { return _tarifaCobranca; }
            set { _tarifaCobranca = value; }
        }

        public double IOF
        {
            get { return _iof; }
            set { _iof = value; }
        }

        public double ValorAbatimento
        {
            get { return _valorAbatimento; }
            set { _valorAbatimento = value; }
        }

        public double Descontos
        {
            get { return _descontos; }
            set { _descontos = value; }
        }

        public double ValorPrincipal
        {
            get { return _valorPrincipal; }
            set { _valorPrincipal = value; }
        }

        public double JurosMora
        {
            get { return _jurosMora; }
            set { _jurosMora = value; }
        }

        public double OutrosCreditos
        {
            get { return _outrosCreditos; }
            set { _outrosCreditos = value; }
        }

        public double OutrosDebitos
        {
            get { return _outrosDebitos; }
            set { _outrosDebitos = value; }
        }

        public DateTime DataCredito
        {
            get { return _dataCredito; }
            set { _dataCredito = value; }
        }

        public int InstrucaoCancelada
        {
            get { return _instrucaoCancelada; }
            set { _instrucaoCancelada = value; }
        }

        public string NomeSacado
        {
            get { return _nomeSacado; }
            set { _nomeSacado = value; }
        }

        public string Erros
        {
            get { return _erros; }
            set { _erros = value; }
        }

        public string CodigoLiquidacao
        {
            get { return _codigoLiquidacao; }
            set { _codigoLiquidacao = value; }
        }

        public int NumeroSequencial
        {
            get { return _numeroSequencial; }
            set { _numeroSequencial = value; }
        }

        public string Registro
        {
            get { return _registro; }
        }

        public double ValorDespesa
        {
            get { return _valorDespesa; }
            set { _valorDespesa = value; }
        }

        public double ValorOutrasDespesas
        {
            get { return _valorOutrasDespesas; }
            set { _valorOutrasDespesas = value; }
        }

        public double ValorPago
        {
            get { return _valorPago; }
            set { _valorPago = value; }
        }

        public string MotivoCodigoOcorrencia
        {
            get { return _motivoCodigoOcorrencia; }
            set { _motivoCodigoOcorrencia = value; }
        }

        public string OrigemPagamento
        {
            get { return _origemPagamento; }
            set { _origemPagamento = value; }
        }

        public string IdentificacaoTitulo
        {
            get { return _identificacaoTitulo; }
            set { _identificacaoTitulo = value; }
        }

        public string MotivosRejeicao
        {
            get { return _motivosRejeicao; }
            set { _motivosRejeicao = value; }
        }

        public string NumeroProtocolo
        {
            get { return _numeroProtocolo; }
            set { _numeroProtocolo = value; }
        }

        public int NumeroCartorio
        {
            get { return _numeroCartorio; }
            set { _numeroCartorio = value; }
        }

        public string NumeroControle
        {
            get { return _numeroControle; }
            set { _numeroControle = value; }
        }

        public int IdentificacaoDoRegistro
        {
            get { return _identificacaoDoRegistro; }
            set { _identificacaoDoRegistro = value; }
        }

        public int TipoInscricao
        {
            get { return _tipoInscricao; }
            set { _tipoInscricao = value; }
        }

        public string CgcCpf
        {
            get { return _cgcCpf; }
            set { _cgcCpf = value; }
        }

        public int ContaCorrente
        {
            get { return _contaCorrente; }
            set { _contaCorrente = value; }
        }

        public string SeuNumero
        {
            get { return _seuNumero; }
            set { _seuNumero = value; }
        }

        public int Instrucao
        {
            get { return _instrucao; }
            set { _instrucao = value; }
        }

        public int CodigoRateio
        {
            get { return _codigoRateio; }
            set { _codigoRateio = value; }
        }

        public double ValoTitulo
        {
            get { return _valoTitulo; }
            set { _valoTitulo = value; }
        }

        public int BancoCobrador
        {
            get { return _bancoCobrador; }
            set { _bancoCobrador = value; }
        }

        public string EspecieTitulo
        {
            get { return _especieTitulo; }
            set { _especieTitulo = value; }
        }

        public double DespeasaDeCobranca
        {
            get { return _despeasaDeCobranca; }
            set { _despeasaDeCobranca = value; }
        }

        public double OutrasDespesas
        {
            get { return _outrasDespesas; }
            set { _outrasDespesas = value; }
        }

        public double Juros
        {
            get { return _juros; }
            set { _juros = value; }
        }

        public double Abatimentos
        {
            get { return _abatimentos; }
            set { _abatimentos = value; }
        }

        public DateTime DataLiquidacao
        {
            get { return _dataLiquidacao; }
            set { _dataLiquidacao = value; }
        }

        public int Sequencial
        {
            get { return _sequencial; }
            set { _sequencial = value; }
        }

        #endregion Propriedades

        #region M�todos de Inst�ncia

        public static string PrimeiroCaracter(string retorno)
        {
            try
            {
                return retorno.Substring(0, 1);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao desmembrar registro.", ex);
            }
        }

        #endregion M�todos de Inst�ncia
    }
}