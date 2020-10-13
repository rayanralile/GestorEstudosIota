using Domain.Model.Models;
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
    public partial class TopicosEditView : ContentPage
    {
        public TopicosEditView(TopicosEditViewModel topico)
        {
            InitializeComponent();
            if (topico is null)
                throw new ArgumentException(nameof(topico));
            BindingContext = topico;
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            (BindingContext as TopicosEditViewModel).Salvar();
            await Navigation.PopAsync();
        }
    }
}