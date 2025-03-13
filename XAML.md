# ContaCalorias

Este é um aplicativo desenvolvido em **.NET MAUI** para calcular o **IMC** e controlar a **ingestão e queima de calorias**.  
Compatível com **Android, iOS e Windows**.

## 📌 Funcionalidades
✅ Cálculo de **IMC**  
✅ Registro de **calorias ingeridas**  
✅ Registro de **calorias gastas**  
✅ Interface responsiva e intuitiva  

## 📦 Código-fonte

```csharp
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace ContaCalorias
{
    public partial class MainPage : ContentPage
    {
        private double totalCaloriasIngeridas = 0;
        private double totalCaloriasEliminadas = 0;

        private Dictionary<string, double> alimentos = new Dictionary<string, double>
        {
            { "Arroz branco", 130 },
            { "Feijão preto", 91 },
            { "Banana", 89 },
            { "Maçã", 52 },
            { "Frango grelhado", 165 },
            { "Pão integral", 252 }
        };

        private Dictionary<string, double> exercicios = new Dictionary<string, double>
        {
            { "Caminhada leve", 5 },
            { "Corrida", 10 },
            { "Natação", 8.33 },
            { "Musculação", 8.33 }
        };

        public MainPage()
        {
            InitializeComponent();
            PreencherPickers();
        }

        private void PreencherPickers()
        {
            foreach (var alimento in alimentos.Keys)
            {
                AlimentosPicker.Items.Add(alimento);
            }

            foreach (var exercicio in exercicios.Keys)
            {
                ExerciciosPicker.Items.Add(exercicio);
            }
        }

        private void CalcularIMC_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(PesoEntry.Text, out double peso) &&
                double.TryParse(AlturaEntry.Text, out double altura))
            {
                double imc = peso / (altura * altura);
                ResultadoIMC.Text = $"Seu IMC é: {imc:F2}";

                if (imc < 18.5)
                    CategoriaIMC.Text = "Abaixo do peso";
                else if (imc < 24.9)
                    CategoriaIMC.Text = "Peso normal";
                else if (imc < 29.9)
                    CategoriaIMC.Text = "Sobrepeso";
                else
                    CategoriaIMC.Text = "Obesidade";
            }
            else
            {
                ResultadoIMC.Text = "Preencha os campos corretamente.";
                CategoriaIMC.Text = "";
            }
        }

        private void AdicionarAlimento_Clicked(object sender, EventArgs e)
        {
            if (AlimentosPicker.SelectedItem != null && 
                double.TryParse(QuantidadeAlimentoEntry.Text, out double quantidade))
            {
                string alimento = AlimentosPicker.SelectedItem.ToString();
                double calorias = (alimentos[alimento] * quantidade) / 100;
                totalCaloriasIngeridas += calorias;
                AtualizarCalorias();
            }
        }

        private void AdicionarExercicio_Clicked(object sender, EventArgs e)
        {
            if (ExerciciosPicker.SelectedItem != null && 
                double.TryParse(TempoExercicioEntry.Text, out double tempo))
            {
                string exercicio = ExerciciosPicker.SelectedItem.ToString();
                double calorias = exercicios[exercicio] * tempo;
                totalCaloriasEliminadas += calorias;
                AtualizarCalorias();
            }
        }

        private void AtualizarCalorias()
        {
            double saldo = totalCaloriasIngeridas - totalCaloriasEliminadas;
            CaloriasIngeridasLabel.Text = $"Calorias Ingeridas: {totalCaloriasIngeridas:F2}";
            CaloriasEliminadasLabel.Text = $"Calorias Eliminadas: {totalCaloriasEliminadas:F2}";
            SaldoCaloriasLabel.Text = $"Saldo de Calorias: {saldo:F2}";
        }
    }
}
