using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestorProdEstudos.ViewModels
{
    public class TopicosEditViewModel : INotifyPropertyChanged
    {
        public event EventHandler<TopicoModel> TopicoAdded;
        public event EventHandler<TopicoModel> TopicoUpdated;
        public event PropertyChangedEventHandler PropertyChanged;

        public ITopicoService TopicoService => DependencyService.Get<ITopicoService>();

        private int _id;
        private string _titulo;
        private string _descricao;
        private DateTime _dataCadastro;

        private TopicoModel _topicoOriginal;
        public TopicosEditViewModel(TopicoModel topicoToView)
        {
            Id = topicoToView.Id;
            Titulo = topicoToView.Titulo;
            Descricao = topicoToView.Descricao;
            DataCadastro = topicoToView.DataCadastro;
            _topicoOriginal = topicoToView;
        }
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                    return;
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                if (_titulo == value)
                    return;
                _titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }
        public string Descricao
        {
            get { return _descricao; }
            set
            {
                if (_descricao == value)
                    return;
                _descricao = value;
                OnPropertyChanged(nameof(Descricao));
            }
        }
        public DateTime DataCadastro
        {
            get { return _dataCadastro; }
            set
            {
                if (_dataCadastro == value)
                    return;
                _dataCadastro = value;
                OnPropertyChanged(nameof(DataCadastro));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async void Salvar()
        {
            var topicoModel = ConvertToModel();
            if (!String.IsNullOrWhiteSpace(this.Titulo) && _topicoOriginal != topicoModel)
            {
                if (this.Id == 0)
                {
                    await TopicoService.Create(topicoModel);
                    TopicoAdded?.Invoke(this, topicoModel);
                }
                else
                {
                    await TopicoService.Update(topicoModel);
                    TopicoUpdated?.Invoke(this, topicoModel);
                }
            }
        }

        private TopicoModel ConvertToModel()
        {
            return new TopicoModel
            {
                Id = this.Id,
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                DataCadastro = this.DataCadastro
            };
        }
    }
}
