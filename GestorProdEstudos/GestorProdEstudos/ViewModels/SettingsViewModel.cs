using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GestorProdEstudos.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private DateTime _horaNotificacao;
        private int _revisaoUm;
        private int _revisaoDois;
        private int _revisaoTres;
        private int _revisaoQuatro;
        private int _revisaoExtraQtd;
        public event PropertyChangedEventHandler PropertyChanged;
        public SettingsViewModel()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            HoraNotificacao = DateTime.Parse(Application.Current.Properties["HoraNotificacao"].ToString());
            RevisaoUm = int.Parse(Application.Current.Properties["RevisaoUm"].ToString());
            RevisaoDois = int.Parse(Application.Current.Properties["RevisaoDois"].ToString());
            RevisaoTres = int.Parse(Application.Current.Properties["RevisaoTres"].ToString());
            RevisaoQuatro = int.Parse(Application.Current.Properties["RevisaoQuatro"].ToString());
            RevisaoExtraQtd = int.Parse(Application.Current.Properties["RevisaoExtraQtd"].ToString());
        }

        public DateTime HoraNotificacao
        {
            get { return _horaNotificacao; }
            set
            {
                if (_horaNotificacao == value)
                    return;
                _horaNotificacao = value;
                OnPropertyChanged(nameof(HoraNotificacao));
                OnPropertyChanged(nameof(Hora));
                OnPropertyChanged(nameof(Minuto));
            }
        }
        public int RevisaoUm
        {
            get { return _revisaoUm; }
            set
            {
                if (_revisaoUm == value)
                    return;
                _revisaoUm = value;
                OnPropertyChanged(nameof(RevisaoUm));
            }
        }
        public int RevisaoDois
        {
            get { return _revisaoDois; }
            set
            {
                if (_revisaoDois == value)
                    return;
                _revisaoDois = value;
                OnPropertyChanged(nameof(RevisaoDois));
            }
        }
        public int RevisaoTres
        {
            get { return _revisaoTres; }
            set
            {
                if (_revisaoTres == value)
                    return;
                _revisaoTres = value;
                OnPropertyChanged(nameof(RevisaoTres));
            }
        }
        public int RevisaoQuatro
        {
            get { return _revisaoQuatro; }
            set
            {
                if (_revisaoQuatro == value)
                    return;
                _revisaoQuatro = value;
                OnPropertyChanged(nameof(RevisaoQuatro));
            }
        }
        public int RevisaoExtraQtd
        {
            get { return _revisaoExtraQtd; }
            set
            {
                if (_revisaoExtraQtd == value)
                    return;
                _revisaoExtraQtd = value;
                OnPropertyChanged(nameof(RevisaoExtraQtd));
            }
        }
        public int Hora { get {return HoraNotificacao.Hour; } }
        public int Minuto { get { return HoraNotificacao.Minute; } }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        internal async Task<bool> SalvarAlteracoes()
        {
            if (RevisaoUm <= 0)
                return false;
            if (RevisaoDois < 0)
                return false;
            if (RevisaoTres < 0)
                return false;
            if (RevisaoQuatro < 0)
                return false;
            if (RevisaoExtraQtd < 0)
                return false;

            Application.Current.Properties["HoraNotificacao"] = HoraNotificacao;
            Application.Current.Properties["RevisaoUm"] = RevisaoUm;
            Application.Current.Properties["RevisaoDois"] = RevisaoDois;
            Application.Current.Properties["RevisaoTres"] = RevisaoTres;
            Application.Current.Properties["RevisaoQuatro"] = RevisaoQuatro;
            Application.Current.Properties["RevisaoExtraQtd"] = RevisaoExtraQtd;
            await Application.Current.SavePropertiesAsync();
            NotifyAll();
            return true;
        }

        private void NotifyAll()
        {
            OnPropertyChanged(nameof(HoraNotificacao));
            OnPropertyChanged(nameof(Hora));
            OnPropertyChanged(nameof(Minuto));
            OnPropertyChanged(nameof(RevisaoUm));
            OnPropertyChanged(nameof(RevisaoDois));
            OnPropertyChanged(nameof(RevisaoTres));
            OnPropertyChanged(nameof(RevisaoQuatro));
            OnPropertyChanged(nameof(RevisaoExtraQtd));
        }
    }
}
