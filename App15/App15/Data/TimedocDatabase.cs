using App15.Controller;
using App15.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Data
{
  public class TimedocDatabase
  {
    readonly SQLiteAsyncConnection database;

    public TimedocDatabase(string dbPath)
    {
      database = new SQLiteAsyncConnection(dbPath);
      database.CreateTableAsync<Coworker>().Wait();
      database.CreateTableAsync<WorkingTime>().Wait();
    }
    public Task<Coworker> GetCoworker()
    {

      return database.Table<Coworker>().FirstOrDefaultAsync();

    }
    public Task<int> SaveCoworkerAsync(Coworker item)
    {
      if (item.Id != Guid.Empty)
      {
        return database.UpdateAsync(item);
      }
      else
      {
        item.Id = Guid.NewGuid();
        return database.InsertAsync(item);
      }
    }

  }
}
