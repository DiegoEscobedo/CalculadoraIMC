using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculadoraIMC.vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ventanaCalcular : ContentPage
    {
        double peso;
        double estatura;
        double IMC;
        int valorGenero;
        int edad;
        double BMR;
        double valorDeActividad;

        public ventanaCalcular()
        {
            InitializeComponent();
        }

        private void CalcularIMC()
        {
            peso = Convert.ToDouble(txtPeso.Text);
            estatura = Convert.ToDouble(txtEstatura.Text);
            edad = Convert.ToInt32(txtEdad.Text);
            IMC = peso / (Math.Pow(estatura, 2));
            lblResultado.Text = IMC.ToString();
            calcularBMR();
            calcularEstado(IMC);
            calcularPGC();
            calcularPI();
            calcularTDEE();
        }

        private void calcularEstado(double resultadoIMC){
            if(resultadoIMC <= 18.50)
            {
                lblEstado.Text = "Peso bajo";
            }
            else if(resultadoIMC <= 24.90)
            {
                lblEstado.Text = "Peso normal";
            }
            else if (resultadoIMC <= 29.90)
            {
                lblEstado.Text = "Pre-obesidad";
            }
            else if (resultadoIMC <= 34.90)
            {
                lblEstado.Text = "Obesidad clase 1";
            }
            else if (resultadoIMC <= 39.90)
            {
                lblEstado.Text = "Obesidad clase 2";
            }
            else
            {
                lblEstado.Text = "Obesidad clase 3 (Estas jodido ya)";
            }
        }

        private void Validar()
        {
            if (!string.IsNullOrEmpty(txtPeso.Text) ||
               !string.IsNullOrEmpty(txtEstatura.Text))
            {
                CalcularIMC();
            }
            else
            {
                DisplayAlert("Error",
                    "Ingresa los valores faltantes", "OK");
            }
        }

        private void btnCalcular_Clicked(object sender, EventArgs e)
        {
            Validar();
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void GenderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string genero = picker.SelectedItem.ToString();
            if (genero.Equals("Masculino"))
            {
                valorGenero = 1;
            }
            else
            {
                valorGenero = 0;
            }
        }

        private void calcularPGC()
        {
            double resultado = 1.2 * IMC + 0.23 * edad - (10.8 * valorGenero) - 5.4;
            lblGc.Text = "%GC: " + resultado.ToString();
        }

        private void calcularPI()
        {
            double estaturacm = estatura * 100;
            if (valorGenero == 1)
            {
                lblPesoIdeal.Text = "Peso ideal: " + ((estaturacm - 100) - ((estaturacm -150)/4)).ToString();
            }
            else
            {
                lblPesoIdeal.Text = "Peso ideal: " + ((estaturacm - 100) - ((estaturacm - 150) / 2.5)).ToString();
            }
        }

        private void calcularBMR()
        {
            BMR = ((estatura * 100) * 6.25) + (peso * 9.99) - (edad* 4.92) + 5;
        }


        private void calcularTDEE()
        {
            double TDEE = BMR * valorDeActividad;
            lblTDEE.Text = "TDEE: " + TDEE.ToString();
        }

        private void actividadPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            string actividad = picker.SelectedItem.ToString();
            if (actividad.Equals("Rara vez"))
            {
                valorDeActividad = 1.2;
            } 
            else if (actividad.Equals("De 1 a 3 dias"))
            {
                valorDeActividad = 1.375;
            }
            else if (actividad.Equals("De 3 a 5 dias"))
            {
                valorDeActividad = 1.55;
            }
            else if (actividad.Equals("De 6 a 7 dias"))
            {
                valorDeActividad = 1.725;
            }
            else if (actividad.Equals("Dos veces al dia"))
            {
                valorDeActividad = 1.9;
            }
        }
    }
}