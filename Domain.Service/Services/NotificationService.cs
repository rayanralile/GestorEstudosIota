using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Domain.Service.Services
{
    public class NotificationService : INotificationService
    {
        public INotificationRepository NotificationRepository => DependencyService.Get<INotificationRepository>();
        public async Task<bool> Create(NotificationModel notification)
        {
            return await NotificationRepository.Create(notification);
        }

        public bool CreateLocalNotify(NotificationModel topico)
        {
            // throw new NotImplementedException();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            return await NotificationRepository.Delete(id);
        }

        public bool DeleteLocalNotify(NotificationModel topico)
        {
            return true;
        }

        public async Task<List<NotificationModel>> GetAll()
        {
            return await NotificationRepository.GetAll();
        }

        public NotificationModel GetById(int id)
        {
            return NotificationRepository.GetById(id);
        }
    }
}
