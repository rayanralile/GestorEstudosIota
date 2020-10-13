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
    public partial class NotificationsView : ContentPage
    {
    //    private bool _isDataLoaded;
        public NotificationsView()
        {
            InitializeComponent();
            BindingContext = new NotificationsViewModel();
        }
        protected override void OnAppearing()
        {
            //if (_isDataLoaded)
            //    return;
            //_isDataLoaded = true;
            LoadData();
            base.OnAppearing();
        }

        private async void LoadData()
        {
            await (BindingContext as NotificationsViewModel).GetAllNotifications();
            listView.ItemsSource = (BindingContext as NotificationsViewModel).Notifications;
        }

        private async void NotificationExcluir_Clicked(object sender, EventArgs e)
        {
            var notification = (sender as Button).CommandParameter as NotificationModel;
            bool decision = await DisplayAlert("Apagar Notificação", "Tem certeza que " +
                $"deseja apagar {notification.Titulo}?", "Sim", "Não");
            if (decision)
            {
                bool isDeleted = await (BindingContext as NotificationsViewModel).Delete(notification.Id);
                if (!isDeleted)
                    await DisplayAlert("Erro de Banco de Dados", "Não foi possível apagar. Tente reiniciar a aplicação", "Ok");
            }
        }

    }
}