using App15.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App15.Controller;
using Xamarin.Forms;
using App15.Helpers;

namespace App15.Data
{
  public class CoworkerStorage
  {
    virtual public Coworker LoadCoworker()
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");

      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        return connection.Table<Coworker>().FirstOrDefault();
      }
    }
    virtual public void SaveCoworker(Coworker coworker)
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");

      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        if (connection.Table<Coworker>().Count() > 0)
        {
          throw new InvalidOperationException("There already exist an Coworker in the database!");
        }
        connection.Insert(coworker);
      }
    }
    virtual public void DeleteCoworker(Coworker coworker)
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");
//      using (SQLiteConnection connection = new SQLiteConnection(AppConfigurationController.DBPath))
      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        if (connection.Table<Coworker>().Count() <= 0)
        {
          throw new InvalidOperationException("There exist no Coworker in the database");
        }

        connection.Delete(coworker);
      }
    }

  }
}
