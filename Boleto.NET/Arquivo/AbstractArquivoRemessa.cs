using System;
using System.IO;

namespace BoletoNet
{
    public abstract class AbstractArquivoRemessa : IArquivoRemessa
    {
        public event EventHandler<LinhaDeArquivoGeradaArgs> LinhaDeArquivoGerada;

        #region Vari�veis

        private Boletos _boletos;
        private Cedente _cedente;
        private IBanco _banco;
        private string _numeroConvenio;
        private int _numeroArquivoRemessa;
        private TipoArquivo _tipoArquivo;
        private IArquivoRemessa _arquivoRemessa;

        #endregion Vari�veis

        #region Construtores

        protected AbstractArquivoRemessa()
        {
        }

        public AbstractArquivoRemessa(TipoArquivo tipoArquivo)
        {
            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    _arquivoRemessa = new ArquivoRemessaCNAB240();
                    _arquivoRemessa.LinhaDeArquivoGerada += new EventHandler<LinhaDeArquivoGeradaArgs>(_arquivoRemessa_LinhaDeArquivoGerada);
                    break;

                case TipoArquivo.CNAB400:
                    _arquivoRemessa = new ArquivoRemessaCNAB400();
                    _arquivoRemessa.LinhaDeArquivoGerada += new EventHandler<LinhaDeArquivoGeradaArgs>(_arquivoRemessa_LinhaDeArquivoGerada);
                    break;

                default:
                    throw new NotImplementedException("Arquivo n�o implementado.");
            }
        }

        private void _arquivoRemessa_LinhaDeArquivoGerada(object sender, LinhaDeArquivoGeradaArgs e)
        {
            OnLinhaGerada(e.Boleto, e.Linha, e.TipoLinha);
        }

        #endregion Construtores

        #region Propriedades

        /// <summary>
        /// N�mero do conv�nio - Apenas alguns bancos trabalham com esse conceito.
        /// </summary>
        public virtual string NumeroConvenio
        {
            get { return _numeroConvenio; }
            set { _numeroConvenio = value; }
        }

        /// <summary>
        /// N�mero do arquivo de remessa
        /// </summary>
        public virtual int NumeroArquivoRemessa
        {
            get { return _numeroArquivoRemessa; }
            set { _numeroArquivoRemessa = value; }
        }

        /// <summary>
        /// Boletos
        /// </summary>
        public virtual Boletos Boletos
        {
            get { return _boletos; }
            protected set { _boletos = value; }
        }

        /// <summary>
        /// Cedente
        /// </summary>
        public virtual Cedente Cedente
        {
            get { return _cedente; }
            protected set { _cedente = value; }
        }

        /// <summary>
        /// Banco
        /// </summary>
        public virtual IBanco Banco
        {
            get { return _banco; }
            protected set { _banco = value; }
        }

        /// <summary>
        /// TipoArquivo
        /// </summary>
        public virtual TipoArquivo TipoArquivo
        {
            get { return _tipoArquivo; }
            protected set { _tipoArquivo = value; }
        }

        #endregion Propriedades

        #region M�todos

        /// <summary>
        /// Gera o arquivo de remessa
        /// </summary>
        public virtual void GerarArquivoRemessa(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, Stream arquivo, int numeroArquivoRemessa)
        {
            _banco = banco;
            _cedente = cedente;
            _boletos = boletos;
            _numeroConvenio = numeroConvenio;
            _numeroArquivoRemessa = numeroArquivoRemessa;
            _arquivoRemessa.GerarArquivoRemessa(numeroConvenio, banco, cedente, boletos, arquivo, numeroArquivoRemessa);
        }

        #endregion M�todos

        #region Disparadores de Eventos

        public virtual void OnLinhaGerada(Boleto boleto, string linha, EnumTipodeLinha tipoLinha)
        {
            try
            {
                if (this.LinhaDeArquivoGerada != null)
                    this.LinhaDeArquivoGerada(this, new LinhaDeArquivoGeradaArgs(boleto, linha, tipoLinha));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar evento.", ex);
            }
        }

        #endregion Disparadores de Eventos
    }
}