using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        // CRUD de notifications é somente ler todos e apagar
        Task<bool> Delete(int id);
        Task<bool> Create(NotificationModel notification);
        Task<List<NotificationModel>> GetAll();
        NotificationModel GetById(int id);
        // Fim do CRUD

    }
}
