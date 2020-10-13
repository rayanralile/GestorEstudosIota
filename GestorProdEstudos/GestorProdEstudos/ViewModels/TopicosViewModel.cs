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
    public class TopicosViewModel : INotifyPropertyChanged
    {
        
        private bool _isLoading;
        public ITopicoService TopicoService => DependencyService.Get<ITopicoService>();
        public ObservableCollection<TopicoModel> Topicos { get; }
        public TopicosViewModel()
        {
            Topicos = new ObservableCollection<TopicoModel>();
            IsLoading = true;
        }
        public async Task GetAllTopicos()
        {
            IsLoading = true;
            Topicos.Clear();
            var topicos = await TopicoService
                .GetAll();
            foreach (var item in topicos)
            {
                Topicos.Add(item);
            }
            IsLoading = false;
        }
        public async Task<bool> Delete(int id)
        {
            if (await TopicoService.Delete(id))
            {
                var topicoDelete = Topicos.Where(x => x.Id == id).SingleOrDefault();
                Topicos.Remove(topicoDelete);
                return true;
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
        //public async Task MockCagadao()
        //{
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        await TopicoService.Create(new TopicoModel
        //        {
        //            Titulo = $"Tópico {i}",
        //            Descricao = $"Descrição {i}",
        //            DataCadastro = DateTime.Parse($"08/{i}/2020")
        //        });
        //    }
        //}
        internal void UpdateFromSalvar(TopicoModel topico)
        {
            var novosTopicos = Topicos.ToList();
            novosTopicos.Remove(novosTopicos.Where(x => x.Id == topico.Id).SingleOrDefault());
            novosTopicos.Add(topico);
            novosTopicos = novosTopicos.OrderByDescending(x => x.DataCadastro).ToList();
            Topicos.Clear();
            foreach (var item in novosTopicos)
            {
                Topicos.Add(item);
            }

        }
        internal void AddSalvar(TopicoModel topico)
        {
            var novosTopicos = Topicos.ToList();
            novosTopicos.Add(topico);
            novosTopicos = novosTopicos.OrderByDescending(x => x.DataCadastro).ToList();
            Topicos.Clear();
            foreach (var item in novosTopicos)
            {
                Topicos.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
