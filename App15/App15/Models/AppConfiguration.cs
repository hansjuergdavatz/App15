using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Models
{
  public class AppConfiguration
  {
    public const int AppConfigurationId = 1234;

    public AppConfiguration()
    {
      //There should be only one AppConfiguration, so every object needs the same primary key.
      Id = AppConfigurationId;
    }

    [PrimaryKey]
    public int Id { get; private set; }
    public string ServerURL { get; set; }
    public string OrderType { get; set; }
  }
}
