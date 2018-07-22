using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            botao.Clicked += BuscarCEP;
        
		}

        private void BuscarCEP(object sender, EventArgs e)
        {
          
            if (isValidCEP(cep.Text.Trim())) {

                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep.Text.Trim());

                    if (end != null)
                    {
                        texto.Text = string.Format("Estado: {0} \n" +
                                                "Cidade: {1} \n" +
                                                "Bairro: {2} \n" +
                                                "Logradouro: {3}",
                        end.uf, end.localidade, end.bairro, end.logradouro);
                    }
                    else
                    {
                        DisplayAlert("Erro", "O endereço não foi encontrado para o CEP informado" + cep.Text.Trim(), "OK");
                    }

                    
                } catch (Exception error)
                {
                    DisplayAlert("Erro Crítico", error.Message, "OK");
                }

               
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valid = true;

            if (cep.Length != 8)
            {
                valid = false;
                DisplayAlert("Erro", "CEP inválido","O CEP deve conter 8 caracteres", "OK");
            }

            int novoCEP = 0;
            if (!int.TryParse(cep,out novoCEP))
            {
                valid = false;
                DisplayAlert("Erro", "CEP inválido", "O CEP deve conter apenas números", "OK");
            }

            return valid;
        }
	}
}
