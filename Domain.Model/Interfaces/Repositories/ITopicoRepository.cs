using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface ITopicoRepository
    {
        Task<bool> Create(TopicoModel topico);
        Task<bool> Update(TopicoModel topico);
        Task<bool> Delete(int id);
        Task<List<TopicoModel>> GetAll();
        TopicoModel GetById(int id);

    }
}
