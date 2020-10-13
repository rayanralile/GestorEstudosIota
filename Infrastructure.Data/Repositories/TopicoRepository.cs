using Domain.Model;
using Domain.Model.Interfaces;
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
    public class TopicoRepository : ITopicoRepository
    {
        private static List<TopicoModel> _topicos = new List<TopicoModel>();
        private SQLiteAsyncConnection _conn;
        public TopicoRepository()
        {
            switch (GlobalConstants.Platform)
            {
                case "Android": _conn = new SQLiteAndroid().GetConnection(); break;
                case "iOS": _conn = new SQLiteiOS().GetConnection(); break;
                default:
                    _conn = new SQLiteAndroid().GetConnection(); break;
            }
        }

        public async Task<bool> Create(TopicoModel topico)
        {
            try
            {
                await _conn.InsertAsync(topico);
                _topicos.Add(topico);
                OrdenarTopicos();
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
                var topicoDelete = GetById(id);
                await _conn.DeleteAsync(topicoDelete);
                _topicos.Remove(topicoDelete);
                OrdenarTopicos();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<TopicoModel>> GetAll()
        {
            await _conn.CreateTableAsync<TopicoModel>();
            if (!_topicos.Any())
            {
                var topicos = await _conn.Table<TopicoModel>()
                    .OrderByDescending(x => x.DataCadastro)
                    .ToListAsync();
                _topicos = topicos;
            }
            return _topicos;
        }

        public TopicoModel GetById(int id)
        {
            return _topicos
                .Where(x => x.Id == id)
                .SingleOrDefault();
        }

        public async Task<bool> Update(TopicoModel topico)
        {
            try
            {
                var topicoOriginal = GetById(topico.Id);
                await _conn.UpdateAsync(topico);
                _topicos.Remove(topicoOriginal);
                _topicos.Add(topico);
                OrdenarTopicos();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void OrdenarTopicos()
        {
            _topicos = _topicos.OrderByDescending(x => x.DataCadastro).ToList();
        }
    }
}
