using App15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Controller
{
  public class AppConfigurationController
  {
    public const string DB_NAME = "TimedocDatabase";
    //DBPath is the only thing we do not save in AppConfiguration (makes not much sense to save the DB-Path in the DB)
    public static string DBPath { get; set; }

    AppConfiguration appConfiguration;


    public bool IsAppConfigurationAvailable
    {
      get
      {
        return appConfiguration != null;
      }
    }

    public string ServerURL
    {
      get
      {
        if (IsAppConfigurationAvailable)
        {
          return appConfiguration.ServerURL;
        }
        else
        {
          return null;
        }
      }

      set
      {
        if (IsAppConfigurationAvailable)
        {
          appConfiguration.ServerURL = value;
        }
        else
        {
          appConfiguration = new AppConfiguration()
          {
            ServerURL = value
          };
        }
      }
    }

    public AppConfigurationController() { }
  }
}
