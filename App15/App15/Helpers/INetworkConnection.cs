﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Helpers
{
  public interface INetworkConnection
  {
    bool IsConnected { get; }
    void CheckNetworkConnection();
  }
}
