using Domain.Model;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Infrastructure.Data.SQLiteConf;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private static List<NotificationModel> _notifications = new List<NotificationModel>();
        private SQLiteAsyncConnection _conn;
        public NotificationRepository()
        {
            switch (GlobalConstants.Platform)
            {
                case "Android": _conn = new SQLiteAndroid().GetConnection(); break;
                case "iOS": _conn = new SQLiteiOS().GetConnection(); break;
                default:
                    _conn = new SQLiteAndroid().GetConnection(); break;
            }
        }

        public async Task<bool> Create(NotificationModel notification)
        {
            try
            {
                await _conn.InsertAsync(notification);
                _notifications.Add(notification);
                OrdenarNotifications();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var notificationDelete = GetById(id);
                await _conn.DeleteAsync(notificationDelete);
                _notifications.Remove(notificationDelete);
                OrdenarNotifications();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<List<NotificationModel>> GetAll()
        {
            await _conn.CreateTableAsync<NotificationModel>();
            if (!_notifications.Any())
            {
                var notifications = await _conn.Table<NotificationModel>()
                    .OrderBy(x => x.DateTimeNotify)
                    .ToListAsync();
                _notifications = notifications;
            }
            return _notifications;
        }

        public NotificationModel GetById(int id)
        {
            return _notifications
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        private void OrdenarNotifications()
        {
            _notifications = _notifications.OrderByDescending(x => x.DateTimeNotify).ToList();
        }
    }
}
