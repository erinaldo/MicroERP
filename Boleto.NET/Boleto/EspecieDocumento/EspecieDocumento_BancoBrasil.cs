using System;

namespace BoletoNet
{
    #region Enumerado

    public enum EnumEspecieDocumento_BancoBrasil
    {
        Cheque = 1, //CH � CHEQUE
        DuplicataMercantil = 2, //DM � DUPLICATA MERCANTIL
        DuplicataMercantilIndicacao = 3, //DMI � DUPLICATA MERCANTIL P/ INDICA��O
        DuplicataServico = 4, //DS �  DUPLICATA DE SERVI�O
        DuplicataServicoIndicacao = 5, //DSI �  DUPLICATA DE SERVI�O P/ INDICA��O
        DuplicataRural = 6, //DR � DUPLICATA RURAL
        LetraCambio = 7, //LC � LETRA DE CAMBIO
        NotaCreditoComercial = 8, //NCC � NOTA DE CR�DITO COMERCIAL
        NotaCreditoExportacao = 9, //NCE � NOTA DE CR�DITO A EXPORTA��O
        NotaCreditoIndustrial = 10, //NCI � NOTA DE CR�DITO INDUSTRIAL
        NotaCreditoRural = 11, //NCR � NOTA DE CR�DITO RURAL
        NotaPromissoria = 12, //NP � NOTA PROMISS�RIA
        NotaPromissoriaRural = 13, //NPR �NOTA PROMISS�RIA RURAL
        TriplicataMercantil = 14, //TM � TRIPLICATA MERCANTIL
        TriplicataServico = 15, //TS �  TRIPLICATA DE SERVI�O
        NotaSeguro = 16, //NS � NOTA DE SEGURO
        Recibo = 17, //RC � RECIBO
        Fatura = 18, //FAT � FATURA
        NotaDebito = 19, //ND �  NOTA DE D�BITO
        ApoliceSeguro = 20, //AP �  AP�LICE DE SEGURO
        MensalidadeEscolar = 21, //ME � MENSALIDADE ESCOLAR
        ParcelaConsorcio = 22, //PC �  PARCELA DE CONS�RCIO
        Outros = 23 //OUTROS
    }

    #endregion Enumerado

    public class EspecieDocumento_BancoBrasil : AbstractEspecieDocumento, IEspecieDocumento
    {
        #region Construtores

        public EspecieDocumento_BancoBrasil()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public EspecieDocumento_BancoBrasil(int codigo)
        {
            try
            {
                this.carregar(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        #endregion Construtores

        #region Metodos Privados

        private void carregar(int idCodigo)
        {
            try
            {
                this.Banco = new Banco_Brasil();

                switch ((EnumEspecieDocumento_BancoBrasil)idCodigo)
                {
                    case EnumEspecieDocumento_BancoBrasil.Cheque:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.Cheque;
                        this.Especie = "CHEQUE";
                        this.Sigla = "CH";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.DuplicataMercantil:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantil;
                        this.Especie = "DUPLICATA MERCANTIL";
                        this.Sigla = "DM";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.DuplicataMercantilIndicacao:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantilIndicacao;
                        this.Especie = "DUPLICATA MERCANTIL P/ INDICA��O";
                        this.Sigla = "DMI";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.DuplicataServico:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.DuplicataServico;
                        this.Especie = "DUPLICATA DE SERVI�O";
                        this.Sigla = "DS";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.DuplicataServicoIndicacao:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.DuplicataServicoIndicacao;
                        this.Especie = "DUPLICATA DE SERVI�O P/ INDICA��O";
                        this.Sigla = "DSI";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.DuplicataRural:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.DuplicataRural;
                        this.Especie = "DUPLICATA RURAL";
                        this.Sigla = "DR";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.LetraCambio:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.LetraCambio;
                        this.Especie = "LETRA DE CAMBIO";
                        this.Sigla = "LC";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaCreditoComercial:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaCreditoComercial;
                        this.Especie = "NOTA DE CR�DITO COMERCIAL";
                        this.Sigla = "NCC";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaCreditoExportacao:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaCreditoExportacao;
                        this.Especie = "NOTA DE CR�DITO A EXPORTA��O";
                        this.Sigla = "NCE";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaCreditoIndustrial:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaCreditoIndustrial;
                        this.Especie = "NOTA DE CR�DITO INDUSTRIAL";
                        this.Sigla = "NCI";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaCreditoRural:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaCreditoRural;
                        this.Especie = "NOTA DE CR�DITO RURAL";
                        this.Sigla = "NCR";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaPromissoria:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaPromissoria;
                        this.Especie = "NOTA PROMISS�RIA";
                        this.Sigla = "NP";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaPromissoriaRural:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaPromissoriaRural;
                        this.Especie = "NOTA PROMISS�RIA RURAL";
                        this.Sigla = "NPR";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.TriplicataMercantil:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.TriplicataMercantil;
                        this.Especie = "TRIPLICATA MERCANTIL";
                        this.Sigla = "TM";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.TriplicataServico:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.TriplicataServico;
                        this.Especie = "TRIPLICATA DE SERVI�O";
                        this.Sigla = "TS";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaSeguro:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaSeguro;
                        this.Especie = "NOTA DE SEGURO";
                        this.Sigla = "NS";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.Recibo:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.Recibo;
                        this.Especie = "RECIBO";
                        this.Sigla = "RC";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.Fatura:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.Fatura;
                        this.Especie = "FATURA";
                        this.Sigla = "FAT";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.NotaDebito:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.NotaDebito;
                        this.Especie = "NOTA DE D�BITO";
                        this.Sigla = "ND";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.ApoliceSeguro:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.ApoliceSeguro;
                        this.Especie = "AP�LICE DE SEGURO";
                        this.Sigla = "AP";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.MensalidadeEscolar:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.MensalidadeEscolar;
                        this.Especie = "MENSALIDADE ESCOLAR";
                        this.Sigla = "ME";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.ParcelaConsorcio:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.ParcelaConsorcio;
                        this.Especie = "PARCELA DE CONS�RCIO";
                        this.Sigla = "PC";
                        break;

                    case EnumEspecieDocumento_BancoBrasil.Outros:
                        this.Codigo = (int)EnumEspecieDocumento_BancoBrasil.Outros;
                        this.Especie = "OUTROS";
                        this.Sigla = "OUTROS";
                        break;

                    default:
                        this.Codigo = 0;
                        this.Especie = "( Selecione )";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar objeto", ex);
            }
        }

        public static EspeciesDocumento CarregaTodas()
        {
            try
            {
                EspeciesDocumento alEspeciesDocumento = new EspeciesDocumento();

                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Cheque));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantil));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataMercantilIndicacao));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataServico));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataServicoIndicacao));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.DuplicataRural));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.LetraCambio));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoComercial));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoExportacao));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoIndustrial));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaCreditoRural));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaPromissoria));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaPromissoriaRural));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.TriplicataMercantil));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.TriplicataServico));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaSeguro));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Recibo));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Fatura));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.NotaDebito));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.ApoliceSeguro));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.MensalidadeEscolar));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.ParcelaConsorcio));
                alEspeciesDocumento.Add(new EspecieDocumento_BancoBrasil((int)EnumEspecieDocumento_BancoBrasil.Outros));

                return alEspeciesDocumento;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar objetos", ex);
            }
        }

        #endregion Metodos Privados
    }
}