﻿using App15.Models;
using App15.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Data
{
  public class RestManager
  {
    IRestService restService;

    public RestManager(IRestService service)
    {
      restService = service;
    }

    public Task<Coworker> GetCoworkerAsync(string name)
    {
      return restService.GetCoworkerAsync(name);
    }

    public Task<WorkingTime> GetWorkingTimeCoworkerAsync(Coworker user)
    {
      return restService.GetWorkingTimeCoworkerAsync(user);
    }

    public Task<List<WorkingTime>> GetWorkingTimeAsync(DateTime date)
    {
      return restService.GetWorkingTimeAsync(date);
    }

    public Task<bool> UpdateWorkingTimeAsync(WorkingTime workingTime)
    {
      return restService.UpdateWorkingTimeAsync(workingTime);
    }
    public Task DeleteWorkingTimeAsync(Guid Id)
    {
      return restService.DeleteWorkingTimeAsync(Id);
    }

    // Leistungserfassung
    public Task<List<OrderAchievement>> GetNewOrderAchievementAsync(string IdOrder, string IdAchievement, bool start, bool listDetail)
    {
      return restService.GetNewOrderAchievementAsync(IdOrder, IdAchievement, start, listDetail);
    }
    public Task<List<OrderAchievement>> StartStopAsync(string IdOrderAchievement, bool start)
    {
      return restService.StartStopAsync(IdOrderAchievement, start);
    }
    public Task<List<OrderAchievement>> GetListOrderAchievementAsync(DateTime day, bool listDetail)
    {
      return restService.GetListOrderAchievementAsync(day, listDetail);
    }
    public Task<bool> UpdateOrderAchievement(OrderAchievement orderAchievement)
    {
      return restService.UpdateOrderAchievement(orderAchievement);
    }
    public Task<bool> DeleteOrderAchievement(OrderAchievement orderAchievement)
    {
      return restService.DeleteOrderAchievement(orderAchievement);
    }

    public Task<List<Order>> GetOrderList(string search)
    {
      return restService.GetOrderList(search);
    }
    public Task<List<Achievement>> GetAchievementList(string idOrder, string search, bool filterPosition)
    {
      return restService.GetAchievementList(idOrder, search, filterPosition);
    }

    public Task<Setting> GetSettingAsync(string name)
    {
      return restService.GetSettingAsync(name);
    }

    public Task<List<CostUnit>> GetCostUnitAsync(string search)
    {
      return restService.GetCostUnitAsync(search);
    }

  }
}
