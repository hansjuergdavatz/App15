using App15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App15.Web
{
    public interface IRestService
    {
        Task<Coworker> GetCoworkerAsync(string loginName);

        Task<WorkingTime> GetWorkingTimeCoworkerAsync(Coworker user);
        Task<List<WorkingTime>> GetWorkingTimeAsync(DateTime date);
        Task<bool> UpdateWorkingTimeAsync(WorkingTime item);
        Task DeleteWorkingTimeAsync(Guid id);

        // Leistungserfassung
        Task<List<OrderAchievement>> GetNewOrderAchievementAsync(string idOrder, string idAchievement, bool start, bool listDetail);
        Task<List<OrderAchievement>> GetListOrderAchievementAsync(DateTime day, bool listDetail);
        Task<bool> UpdateOrderAchievement(OrderAchievement item);
        Task<bool> DeleteOrderAchievement(OrderAchievement item);

        Task<List<Order>> GetOrderList(string search);
        Task<List<Achievement>> GetAchievementList(string search);

    }
}
