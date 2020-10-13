using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Services
{
    public interface ITopicoService
    {
        // Métodos de CRUD
        Task<bool> Create(TopicoModel topico);
        Task<bool> Update(TopicoModel topico);
        Task<bool> Delete(int id);
        Task<List<TopicoModel>> GetAll();
        TopicoModel GetById(int id);
        // Fim de CRUD

    }
}
