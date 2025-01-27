namespace BoletoNet
{
    public interface IBanco
    {
        /// <summary>
        /// Formata o c�digo de barras
        /// </summary>
        void FormataCodigoBarra(Boleto boleto);

        /// <summary>
        /// Formata a linha digital
        /// </summary>
        void FormataLinhaDigitavel(Boleto boleto);

        /// <summary>
        /// Formata o nosso n�mero
        /// </summary>
        void FormataNossoNumero(Boleto boleto);

        /// <summary>
        /// Formata o n�mero do documento, alguns bancos exige uma formata��o. Tipo: 123-4
        /// </summary>
        void FormataNumeroDocumento(Boleto boleto);

        /// <summary>
        /// Respons�vel pela valida��o de todos os dados referente ao banco, que ser�o usados no boleto
        /// </summary>
        void ValidaBoleto(Boleto boleto);

        /// <summary>
        /// Gera o header do arquivo de remessa
        /// </summary>
        string GerarHeaderRemessa(string numeroConvenio, Cedente cendente, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa
        /// </summary>
        string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera o header de arquivo do arquivo de remessa
        /// </summary>
        string GerarHeaderRemessa(Cedente cendente, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera o Trailer do arquivo de remessa
        /// </summary>
        string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera o header de lote do arquivo de remessa
        /// </summary>
        string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cendente, int numeroArquivoRemessa);

        /// <summary>
        /// Gera o header de lote do arquivo de remessa
        /// </summary>
        string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cendente, int numeroArquivoRemessa, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa - SEGMENTO P
        /// </summary>
        string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio);

        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa - SEGMENTO P
        /// </summary>
        string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio, Cedente cedente);

        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa - SEGMENTO Q
        /// </summary>
        string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera os registros de detalhe do arquivo de remessa - SEGMENTO R
        /// </summary>
        string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo);

        /// <summary>
        /// Gera o Trailer de arquivo do arquivo de remessa
        /// </summary>
        string GerarTrailerArquivoRemessa(int numeroRegistro);

        /// <summary>
        /// Gera o Trailer de lote do arquivo de remessa
        /// </summary>
        string GerarTrailerLoteRemessa(int numeroRegistro);

        DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro);

        DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro);

        DetalheRetorno LerDetalheRetornoCNAB400(string registro);

        Cedente Cedente { get; }
        int Codigo { get; set; }
        string Nome { get; }
        int Digito { get; }
    }
}