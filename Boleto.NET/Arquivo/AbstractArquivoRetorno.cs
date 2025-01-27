using System;
using System.IO;

namespace BoletoNet
{
    public abstract class AbstractArquivoRetorno
    {
        public event EventHandler<LinhaDeArquivoLidaArgs> LinhaDeArquivoLida;

        #region Vari�veis

        private IBanco _banco;
        private TipoArquivo _tipoArquivo;
        private DetalheRetorno _detalheRetorno;
        private IArquivoRetorno _arquivoRetorno;

        #endregion Vari�veis

        #region Construtores

        protected AbstractArquivoRetorno()
        {
        }

        public AbstractArquivoRetorno(TipoArquivo tipoArquivo)
        {
            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    _arquivoRetorno = new ArquivoRetornoCNAB240();
                    _arquivoRetorno.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>(ArquivoRemessa_LinhaDeArquivoLidaCNAB240);
                    break;

                case TipoArquivo.CNAB400:
                    _arquivoRetorno = new ArquivoRetornoCNAB400();
                    _arquivoRetorno.LinhaDeArquivoLida += new EventHandler<LinhaDeArquivoLidaArgs>(ArquivoRemessa_LinhaDeArquivoLidaCNAB400);
                    break;

                default:
                    throw new NotImplementedException("Arquivo n�o implementado.");
            }
        }

        private void ArquivoRemessa_LinhaDeArquivoLidaCNAB240(object sender, LinhaDeArquivoLidaArgs e)
        {
            OnLinhaLida(e.Detalhe as DetalheRetornoCNAB240, e.Linha, e.TipoLinha);
        }

        private void ArquivoRemessa_LinhaDeArquivoLidaCNAB400(object sender, LinhaDeArquivoLidaArgs e)
        {
            OnLinhaLida(e.Detalhe as DetalheRetorno, e.Linha);
        }

        #endregion Construtores

        #region Propriedades

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

        /// <summary>
        /// Detalhe do arquivo retorno
        /// </summary>
        public virtual DetalheRetorno DetalheRetorno
        {
            get { return _detalheRetorno; }
            protected set { _detalheRetorno = value; }
        }

        #endregion Propriedades

        #region M�todos

        /// <summary>
        /// Gera o arquivo de remessa
        /// </summary>
        public virtual void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            _banco = banco;
            _arquivoRetorno.LerArquivoRetorno(banco, arquivo);
        }

        #endregion M�todos

        #region Disparadores de Eventos

        public virtual void OnLinhaLida(DetalheRetornoCNAB240 detalheRetornoCNAB240, string linha, EnumTipodeLinhaLida tipoLinha)
        {
            try
            {
                if (this.LinhaDeArquivoLida != null)
                    this.LinhaDeArquivoLida(this, new LinhaDeArquivoLidaArgs(detalheRetornoCNAB240, linha, tipoLinha));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar evento.", ex);
            }
        }

        public virtual void OnLinhaLida(DetalheRetorno detalheRetorno, string linha)
        {
            try
            {
                if (this.LinhaDeArquivoLida != null)
                    this.LinhaDeArquivoLida(this, new LinhaDeArquivoLidaArgs(detalheRetorno, linha));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar evento.", ex);
            }
        }

        #endregion Disparadores de Eventos
    }
}