using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestorProdEstudos.ViewModels
{
    public class NotificationsViewModel : INotifyPropertyChanged
    {

        private bool _isLoading;
        public INotificationService NotificationService => DependencyService.Get<INotificationService>();
        public ObservableCollection<NotificationModel> Notifications { get; }
        public NotificationsViewModel()
        {
            Notifications = new ObservableCollection<NotificationModel>();
            IsLoading = true;
        }
        public async Task GetAllNotifications()
        {
            IsLoading = true;
            Notifications.Clear();
            var topicos = await NotificationService
                .GetAll();
            foreach (var item in topicos)
            {
                Notifications.Add(item);
            }
            IsLoading = false;
        }
        public async Task<bool> Delete(int id)
        {
            if (await NotificationService.Delete(id))
            {
                var notificationDelete = Notifications.Where(x => x.Id == id).SingleOrDefault();
                Notifications.Remove(notificationDelete);
                if (NotificationService.DeleteLocalNotify(notificationDelete))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
