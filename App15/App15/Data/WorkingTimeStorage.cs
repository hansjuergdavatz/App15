using App15.Helpers;
using App15.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App15.Data
{
  public class WorkingTimeStorage
  {
    public List<WorkingTime> LoadOfflineWorkingTimes()
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");
      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        return connection.Table<WorkingTime>().OrderBy(i => i.ComeTime).Select(i => i).ToList();
      }
    }

    virtual public WorkingTime LoadWorkingTime(Guid id)
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");

      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        return ( from wt in connection.Table<WorkingTime>() where wt.Id == id select wt).FirstOrDefault();
      }
    }
    virtual public void SaveWorkingTime(WorkingTime workingTime)
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");

      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        if (connection.Table<WorkingTime>().Count() > 0)
        {
          WorkingTime wt = connection.Table<WorkingTime>().Where(m => m.Id == workingTime.Id).FirstOrDefault();
          if (wt != null)
          {
            connection.Delete(wt);
          }
        }
        connection.InsertOrReplace(workingTime);
      }
    }
    virtual public void DeleteWorkingTime(WorkingTime workingTime)
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");
      using (SQLiteConnection connection = new SQLiteConnection(s))
      {
        if (connection.Table<WorkingTime>().Count() > 0)
        {
          connection.Delete(workingTime);
        }

      }
    }

  }
}
