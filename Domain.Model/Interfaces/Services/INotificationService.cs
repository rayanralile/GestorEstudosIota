using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface INotificationService
    {
        // CRUD de notifications é somente ler todos e apagar
        Task<bool> Delete(int id);
        Task<List<NotificationModel>> GetAll();

        Task<bool> Create(NotificationModel notification);
        NotificationModel GetById(int id);
        // Fim do CRUD

        // Regras de negócio
        bool CreateLocalNotify(NotificationModel topico);
        bool DeleteLocalNotify(NotificationModel topico);

    }
}
