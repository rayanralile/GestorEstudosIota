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
    public class TopicoService : ITopicoService
    {
        public ITopicoRepository TopicoRepository => DependencyService.Get<ITopicoRepository>();
        public INotificationService NotificationService => DependencyService.Get<INotificationService>();

        //Aqui é o conjunto de variáveis do setup
        private int _diasRevisao1;
        private int _diasRevisao2;
        private int _diasRevisao3;
        private int _diasRevisao4;
        private int _diasPos4;
        private int _horas;
        private int _minutos;

        public async Task<bool> Create(TopicoModel topico)
        {
            PreencherSetup();
            var isDone = await TopicoRepository.Create(topico);
            if (isDone)
            {
                //Bloco da primeira revisão (a mais importante e única obrigatória)
                var notification1 = ConvertTopicoToNotify(topico);
                notification1.DateTimeNotify = topico
                    .DataCadastro
                    .Date
                    .AddDays(_diasRevisao1)
                    .AddHours(_horas)
                    .AddMinutes(_minutos);
                await NotificationService.Create(notification1);
                NotificationService.CreateLocalNotify(notification1);
                //Bloco da segunda revisão
                if(_diasRevisao2 != 0)
                {
                    var notification2 = ConvertTopicoToNotify(topico);
                    notification2.DateTimeNotify = topico
                        .DataCadastro
                        .Date
                        .AddDays(_diasRevisao2)
                        .AddHours(_horas)
                        .AddMinutes(_minutos);
                    await NotificationService.Create(notification2);
                    NotificationService.CreateLocalNotify(notification2);
                }
                //Bloco da terceira revisão
                if (_diasRevisao3 != 0)
                {
                    var notification3 = ConvertTopicoToNotify(topico);
                    notification3.DateTimeNotify = topico
                        .DataCadastro
                        .Date
                        .AddDays(_diasRevisao3)
                        .AddHours(_horas)
                        .AddMinutes(_minutos);
                    await NotificationService.Create(notification3);
                    NotificationService.CreateLocalNotify(notification3);
                }
                //Bloco da quarta revisão
                if (_diasRevisao4 != 0)
                {
                    var notification4 = ConvertTopicoToNotify(topico);
                    notification4.DateTimeNotify = topico
                        .DataCadastro
                        .Date
                        .AddDays(_diasRevisao4)
                        .AddHours(_horas)
                        .AddMinutes(_minutos);
                    await NotificationService.Create(notification4);
                    NotificationService.CreateLocalNotify(notification4);
                }
                //Bloco da revisão infinita (definida nas configurações)
                for (int i = 1; i <= _diasPos4; i++)
                {
                    var notif = ConvertTopicoToNotify(topico);
                    notif.DateTimeNotify = topico
                    .DataCadastro
                    .Date
                    .AddDays(_diasRevisao4*(i+1))
                    .AddHours(_horas)
                    .AddMinutes(_minutos);
                    await NotificationService.Create(notif);
                    NotificationService.CreateLocalNotify(notif);
                }
                return true;
            }
            else
                return false;
        }

        public async Task<bool> Delete(int id)
        {
            bool retorno = false;
            var isDone = await TopicoRepository.Delete(id);
            if (isDone)
            {
                var allNotif = await NotificationService.GetAll();
                allNotif = allNotif.Where(x => x.TopicoId == id).ToList();
                foreach (var item in allNotif)
                {
                    var isDeleted = await NotificationService.Delete(item.Id);
                    var isNotifDel = NotificationService.DeleteLocalNotify(item);
                    if (!isDeleted || !isNotifDel)
                        return retorno;
                }
                retorno = true;
            }
            return retorno;

        }

        public async Task<List<TopicoModel>> GetAll()
        {
            return await TopicoRepository.GetAll();
        }

        public TopicoModel GetById(int id)
        {
            return TopicoRepository.GetById(id);
        }

        public async Task<bool> Update(TopicoModel topico)
        {
            return await TopicoRepository.Update(topico);
        }
        private void PreencherSetup()
        {
            var dataHora = DateTime.Parse(Application.Current.Properties["HoraNotificacao"].ToString());
            _diasRevisao1 = int.Parse(Application.Current.Properties["RevisaoUm"].ToString());
            _diasRevisao2 = int.Parse(Application.Current.Properties["RevisaoDois"].ToString());
            _diasRevisao3 = int.Parse(Application.Current.Properties["RevisaoTres"].ToString());
            _diasRevisao4 = int.Parse(Application.Current.Properties["RevisaoQuatro"].ToString());
            _diasPos4 = int.Parse(Application.Current.Properties["RevisaoExtraQtd"].ToString());
            _horas = dataHora.Hour;
            _minutos = dataHora.Minute;
        }
        private NotificationModel ConvertTopicoToNotify(TopicoModel topico)
        {
            return new NotificationModel 
            {
                Titulo = topico.Titulo,
                TopicoId = topico.Id
            };
        }
    }
}
