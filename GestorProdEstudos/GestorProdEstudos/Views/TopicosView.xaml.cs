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
    public partial class TopicosView : ContentPage
    {
        private bool _isDataLoaded;
        public TopicosView()
        {
            InitializeComponent();
            BindingContext = new TopicosViewModel();
        }
        protected override void OnAppearing()
        {
            if (_isDataLoaded)
                return;
            _isDataLoaded = true;
            LoadData();
            base.OnAppearing();
        }

        private async void LoadData()
        {
            await (BindingContext as TopicosViewModel).GetAllTopicos();
            listView.ItemsSource = (BindingContext as TopicosViewModel).Topicos;
            // Agora vou preencher a lista de notificações - ela é estática
            // está localizada no NotificationRepository, mas é chamada a partir
            // da View Model (NotificationsViewModel.cs)

            var notificationsVM = new NotificationsViewModel();
            await notificationsVM.GetAllNotifications();
        }

        private async void TopicoExcluir_Clicked(object sender, EventArgs e)
        {
            var topico = (sender as Button).CommandParameter as TopicoModel;
            bool decision = await DisplayAlert("Apagar Tópico", "Tem certeza que " +
                $"deseja apagar {topico.Titulo}?", "Sim", "Não");
            if (decision)
            {
                bool isDeleted = await (BindingContext as TopicosViewModel).Delete(topico.Id);
                if (!isDeleted)
                    await DisplayAlert("Erro de Banco de Dados", "Não foi possível apagar. Tente reiniciar a aplicação","Ok");
            }
        }

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (listView.SelectedItem == null)
                return;

            var selectedTopico = e.SelectedItem as TopicoModel;

            listView.SelectedItem = null;
            var pageVM = new TopicosEditViewModel(selectedTopico);
            pageVM.TopicoUpdated += (source, topico) =>
            {
                selectedTopico.Id = topico.Id;
                selectedTopico.Titulo = topico.Titulo;
                selectedTopico.DataCadastro = topico.DataCadastro;
                (BindingContext as TopicosViewModel).UpdateFromSalvar(selectedTopico);
            };
            await Navigation.PushAsync(new TopicosEditView(pageVM));
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            var pageVM = new TopicosEditViewModel(new TopicoModel{ DataCadastro = DateTime.Now });
            pageVM.TopicoAdded += (source, topico) =>
            {
                (BindingContext as TopicosViewModel).AddSalvar(topico);
            };
            await Navigation.PushAsync(new TopicosEditView(pageVM));
        }
    }
}