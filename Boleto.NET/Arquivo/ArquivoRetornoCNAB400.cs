using System;
using System.Collections.Generic;
using System.IO;

namespace BoletoNet
{
    public class ArquivoRetornoCNAB400 : AbstractArquivoRetorno, IArquivoRetorno
    {
        private List<DetalheRetorno> _listaDetalhe = new List<DetalheRetorno>();

        public List<DetalheRetorno> ListaDetalhe
        {
            get { return _listaDetalhe; }
            set { _listaDetalhe = value; }
        }

        #region Construtores

        public ArquivoRetornoCNAB400()
        {
            this.TipoArquivo = TipoArquivo.CNAB400;
        }

        #endregion Construtores

        #region M�todos de inst�ncia

        public override void LerArquivoRetorno(IBanco banco, Stream arquivo)
        {
            try
            {
                StreamReader stream = new StreamReader(arquivo);
                string linha = "";

                // Lendo o arquivo
                linha = stream.ReadLine();

                // Pr�xima linha (DETALHE)
                linha = stream.ReadLine();

                while (DetalheRetorno.PrimeiroCaracter(linha) == "1")
                {
                    DetalheRetorno detalhe = banco.LerDetalheRetornoCNAB400(linha);
                    ListaDetalhe.Add(detalhe);
                    OnLinhaLida(detalhe, linha);
                    linha = stream.ReadLine();
                }

                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler arquivo.", ex);
            }
        }

        #endregion M�todos de inst�ncia
    }
}