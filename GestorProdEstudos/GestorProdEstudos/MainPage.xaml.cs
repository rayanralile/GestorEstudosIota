using GestorProdEstudos.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestorProdEstudos
{
    public partial class MainPage : MasterDetailPage
    {
        private NavigationPage _topicosPage = null;
        private NavigationPage _notificationsPage = null;
        private NavigationPage _settingsPage = null;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (_topicosPage is null)
            {
                _topicosPage = new NavigationPage(new TopicosView());
                Detail = _topicosPage;
            }
            else
                Detail = _topicosPage;
            // Abaixo ele verifica se já existe configuração. Se não, ele já atribui aqui.
            SetupInicial();

            base.OnAppearing(); 
        }

        private void SetupInicial()
        {
            if (!Application.Current.Properties.ContainsKey("HoraNotificacao"))
                Application.Current.Properties["HoraNotificacao"] = DateTime.Today.AddHours(12);
            if (!Application.Current.Properties.ContainsKey("RevisaoUm"))
                Application.Current.Properties["RevisaoUm"] = 1;
            if (!Application.Current.Properties.ContainsKey("RevisaoDois"))
                Application.Current.Properties["RevisaoDois"] = 7;
            if (!Application.Current.Properties.ContainsKey("RevisaoTres"))
                Application.Current.Properties["RevisaoTres"] = 15;
            if (!Application.Current.Properties.ContainsKey("RevisaoQuatro"))
                Application.Current.Properties["RevisaoQuatro"] = 30;
            if (!Application.Current.Properties.ContainsKey("RevisaoExtraQtd"))
                Application.Current.Properties["RevisaoExtraQtd"] = 0;
        }

        private void Topicos_Tapped(object sender, EventArgs e)
        {
            if (_topicosPage is null)
            {
                _topicosPage = new NavigationPage(new TopicosView());
                Detail = _topicosPage;
            }
            else
                Detail = _topicosPage;
            IsPresented = false;
        }

        private void Notifications_Tapped(object sender, EventArgs e)
        {
            if (_notificationsPage is null)
            {
                _notificationsPage = new NavigationPage(new NotificationsView());
                Detail = _notificationsPage;
            }
            else
                Detail = _notificationsPage;
            IsPresented = false;
        }

        private void Settings_Tapped(object sender, EventArgs e)
        {
            if (_settingsPage is null)
            {
                _settingsPage = new NavigationPage(new SettingsView());
                Detail = _settingsPage;
            }
            else
                Detail = _settingsPage;
            IsPresented = false;
        }
    }
}
