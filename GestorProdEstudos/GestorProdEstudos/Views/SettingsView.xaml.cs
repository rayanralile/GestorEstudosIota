using GestorProdEstudos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestorProdEstudos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView()
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            bool isDone = await (BindingContext as SettingsViewModel).SalvarAlteracoes();
            if (isDone)
            {
                await DisplayAlert("Salvo!", "Alterações salvas com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            }
            else
                await DisplayAlert("Erro ao salvar", "Verifique se todos os campos estão preenchidos. A primeira revisão é obrigatória e deve conter um número maior que zero.", "Vou revisar!");
        }
    }
}