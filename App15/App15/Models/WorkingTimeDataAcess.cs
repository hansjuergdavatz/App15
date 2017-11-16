using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using App15.Helpers;

namespace App15.Models
{
  public class WorkingTimeDataAcess
  {
    private SQLiteConnection database;
    private static object collisionLock = new object();

    public ObservableCollection<WorkingTime> WorkingTimes { get; set; }

    public WorkingTimeDataAcess()
    {
      string s = DependencyService.Get<IFileHelper>().GetLocalFilePath("TimedocSQLite.db3");
      database = new SQLiteConnection(s);

      //database = DependencyService.Get<IDatabaseConnection>().DbConnection();
      this.WorkingTimes = new ObservableCollection<WorkingTime>(database.Table<WorkingTime>());
    }

  }
}
