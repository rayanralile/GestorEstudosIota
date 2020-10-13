using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
