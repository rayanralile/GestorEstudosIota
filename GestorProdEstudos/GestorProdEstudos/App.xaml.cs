using Domain.Model;
using Domain.Service.Services;
using GestorProdEstudos.Views;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SQLiteConf;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestorProdEstudos
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Início da injeção de dependências
            DependencyService.Register<TopicoRepository>();
            DependencyService.Register<TopicoService>();
            DependencyService.Register<NotificationRepository>();
            DependencyService.Register<NotificationService>();
            // Abaixo uma injeção que analisa qual plataforma estou para injetar certo
            if (Device.RuntimePlatform == Device.iOS)
                GlobalConstants.Platform = "iOS";
            else if (Device.RuntimePlatform == Device.Android)
                GlobalConstants.Platform = "Android";
            else
            {
                Console.WriteLine("ERROR - NÃO RECONHECEU SISTEMA VÁLIDO. USAREI DO ANDROID PRA TENTAR ME SAFAR");
                GlobalConstants.Platform = "Unknown";
            }
            // Fim da injeção de dependências

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
