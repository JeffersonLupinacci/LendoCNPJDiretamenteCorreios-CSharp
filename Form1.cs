using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LeituraWeb
{
    public partial class Form1 : Form
    {
        ConsultaCnpjReceita ConsultaCnpj = new ConsultaCnpjReceita();
        CadastroCNPJ cad = new CadastroCNPJ();

        public Form1(){
            InitializeComponent();
        }

        #region BOTAO CONSULTA DE DADOS
        private void button2_Click(object sender, EventArgs e){
            Cursor cursor;
            cursor = this.Cursor;           

            try{
                this.Cursor = Cursors.WaitCursor;
                ConsultaCnpj.Consulta(edCNPJ.Text, edCAPTCHA.Text);                
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            if (ConsultaCnpj.ErroDetectado != null){
                MessageBox.Show(ConsultaCnpj.ErroDetectado);
            } else {
                try{
                    ConsultaCnpj.PreencheFicha(cad);
                    memoEdit2.Text = ConsultaCnpj.RetornoEmHtml();
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
            this.Cursor = cursor;
        }
        #endregion

        #region BOTAO RECUPERA IMAGEM
        private void button1_Click(object sender, EventArgs e){
            edCAPTCHA.Text = "";
            Cursor cursor;
            cursor = this.Cursor;           
            this.Cursor = Cursors.WaitCursor;
            Bitmap bit = ConsultaCnpj.RecuperaCaptcha();
            if (bit != null)
                pictureBox1.Image = bit;            
            else 
                MessageBox.Show("Não foi possível recuperar a imagem de validação do site da Receita Federal");
            this.Cursor = cursor;
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            string tmp = memoEdit2.Text.ToString();
            memoEdit1.Text += RecuperaColunaValor(tmp, Coluna.DataSituacaoCadastral);            
        }

        private enum Coluna { 
            RazaoSocial = 0,
            NomeFantasia,
            
            AtividadeEconomicaPrimaria,
            AtividadeEconomicaSecundaria,
            
            NumeroDaInscricao,
            MatrizFilial,
            NaturezaJuridica,

            SituacaoCadastral,
            DataSituacaoCadastral,
            MotivoSituacaoCadastral,

            EnderecoLogradouro,
            EnderecoNumero,
            EnderecoComplemento,
            EnderecoCEP,
            EnderecoBairro,
            EnderecoCidade,
            EnderecoEstado

            
        };

        /*

        
         * 
        <!-- Início Linha SITUAÇÃO ESPECIAL -->
        <!-- Fim Linha SITUACAO ESPECIAL-->
         * 
        <!-- Início Linha PORTE DA EMPRESA -->
        <!-- Fim Linha PORTE DA EMPRESA -->       
         */

        private String RecuperaColunaValor(String pattern, Coluna col)
        {
            String S = pattern.Replace("\n", "").Replace("\t", "").Replace("\r", "");           
            
            switch (col) {
                case Coluna.RazaoSocial:{
                    S = StringEntreString(S, "<!-- Início Linha NOME EMPRESARIAL -->", "<!-- Fim Linha NOME EMPRESARIAL -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.NomeFantasia:{
                    S = StringEntreString(S, "<!-- Início Linha ESTABELECIMENTO -->", "<!-- Fim Linha ESTABELECIMENTO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.NaturezaJuridica: {
                    S = StringEntreString(S, "<!-- Início Linha NATUREZA JURÍDICA -->", "<!-- Fim Linha NATUREZA JURÍDICA -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.AtividadeEconomicaPrimaria:{
                    S = StringEntreString(S, "<!-- Início Linha ATIVIDADE ECONOMICA -->", "<!-- Fim Linha ATIVIDADE ECONOMICA -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }
                case Coluna.AtividadeEconomicaSecundaria: {
                    S = StringEntreString(S, "<!-- Início Linha ATIVIDADE ECONOMICA SECUNDARIA-->", "<!-- Fim Linha ATIVIDADE ECONOMICA SECUNDARIA -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }        
                case Coluna.NumeroDaInscricao:{
                    S = StringEntreString(S, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");                                    
                    return S.Trim() ; 
                }
                case Coluna.MatrizFilial: {
                    S = StringEntreString(S, "<!-- Início Linha NÚMERO DE INSCRIÇÃO -->", "<!-- Fim Linha NÚMERO DE INSCRIÇÃO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }
                case Coluna.EnderecoLogradouro:
                    {
                    S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }
                case Coluna.EnderecoNumero: {
                    S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }
                case Coluna.EnderecoComplemento:{
                    S = StringEntreString(S, "<!-- Início Linha LOGRADOURO -->", "<!-- Fim Linha LOGRADOURO -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }                
                case Coluna.EnderecoCEP: {
                    S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                    S = StringEntreString(S, "<tr>", "</tr>");                    
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim(); 
                }
                case Coluna.EnderecoBairro:{
                    S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.EnderecoCidade:{
                    S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.EnderecoEstado:{
                    S = StringEntreString(S, "<!-- Início Linha CEP -->", "<!-- Fim Linha CEP -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringSaltaString(S, "</b>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.SituacaoCadastral:{
                    S = StringEntreString(S, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
                case Coluna.DataSituacaoCadastral:{
                    S = StringEntreString(S, "<!-- Início Linha SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha SITUACAO CADASTRAL -->");
                    S = StringEntreString(S, "<tr>", "</tr>");
                    S = StringSaltaString(S, "</b>");
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }                             
                case Coluna.MotivoSituacaoCadastral:{
                    S = StringEntreString(S, "<!-- Início Linha MOTIVO DE SITUAÇÃO CADASTRAL -->", "<!-- Fim Linha MOTIVO DE SITUAÇÃO CADASTRAL -->");
                    S = StringEntreString(S, "<tr>", "</tr>");                    
                    S = StringEntreString(S, "<b>", "</b>");
                    return S.Trim();
                }                             
                    
        

                default: { 
                    return S; }
            }                      
        }
        
        private String StringEntreString(String Str, String StrInicio, String StrFinal) {
            int Ini;
            int Fim;
            int Diff;
            Ini = Str.IndexOf(StrInicio);
            Fim = Str.IndexOf(StrFinal);
            if (Ini > 0) Ini = Ini + StrInicio.Length;            
            if (Fim > 0) Fim = Fim + StrFinal.Length;
            Diff = ((Fim - Ini) - StrFinal.Length);
            if ((Fim > Ini) && (Diff > 0))
                return Str.Substring(Ini, Diff);
            else
                return "";           
        }
    
        private String StringSaltaString(String Str, String StrInicio) {
            int Ini;           
            Ini = Str.IndexOf(StrInicio);            
            if (Ini > 0){
                Ini = Ini + StrInicio.Length;            
                return Str.Substring(Ini);
            }else
                return Str;           
        }

        public string StringPrimeiraLetraMaiuscula(String Str){
            string StrResult = "";
            if (Str.Length > 0){
                StrResult += Str.Substring(0, 1).ToUpper();
                StrResult += Str.Substring(1, Str.Length - 1).ToLower();
            }
            return StrResult;
        }   
    }
}
    
