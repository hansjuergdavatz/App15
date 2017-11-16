using App15.Data;
using App15.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App15.Web
{
    public class RestService : IRestService
    {
        HttpClient client;

        public List<WorkingTime> Items { get; private set; }

        public RestService()
        {
            CoworkerStorage coStore = new CoworkerStorage();
            Coworker savedCoworker = coStore.LoadCoworker();
            if (savedCoworker == null)
                return;

            SetClient(savedCoworker.LoginId, savedCoworker.Password);

        }
        public RestService(string loginId, string password)
        {
            SetClient(loginId, password);
        }
        private void SetClient(string loginId, string password)
        {
            var authData = string.Format("{0}:{1}", loginId, password);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        }

        // Zeiterfassung
        public async Task<Coworker> GetCoworkerAsync(string loginName)
        {
            Coworker coworker = null;

            // http://localhost:63491/api/coworker?loginName=h.davatz%40gmx.ch
            string resource = String.Format("{0}coworker?loginname={1}", Constants.RestUrl, loginName);
            //resource = "http://caprex.ddns.net:5505/api/ping";

            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    coworker = JsonConvert.DeserializeObject<Coworker>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return coworker;
        }
        public async Task<WorkingTime> GetWorkingTimeCoworkerAsync(Coworker coworker)
        {
            WorkingTime workingTime = null;

            // http://localhost:63491/api/workingTime?IdMandant=8dc0122e-5a47-44d5-a0ef-02325c5d7956&IdCoworker=b639dacf-8307-4ef0-9bea-7addcb8bbe62
            string resource = String.Format("{0}workingTime?IdMandant={1}&IdCoworker={2}", Constants.RestUrl, coworker.IdMandant, coworker.Id);
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    workingTime = JsonConvert.DeserializeObject<WorkingTime>(content);
                }
                else
                {
                    WorkingTime wt = new WorkingTime();
                    workingTime = wt;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return workingTime;
        }
        public async Task<List<WorkingTime>> GetWorkingTimeAsync(DateTime date)
        {
            List<WorkingTime> list = null;

            // http://localhost:63491/api/workingTime/list?date=2017-07-12&displayOldRunning=1
            string resource = String.Format("{0}workingTime/list?date={1}&displayOldRunning=1", Constants.RestUrl, date.ToString("yyyy-MM-dd"));
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<WorkingTime>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }
        public async Task<bool> UpdateWorkingTimeAsync(WorkingTime workingTime)
        {

            // RestUrl = http://localhost:63491/api/workingTime

            string resource = String.Format("{0}workingTime", Constants.RestUrl);
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(workingTime);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				WorkingTime successfully saved.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }
        public async Task DeleteWorkingTimeAsync(Guid id)
        {
            // http://localhost:63491/api/workingTime?idWorkingTime=8289714b-9e05-443f-ad62-aca20f5ded00
            string resource = String.Format("{0}workingTime?idWorkingTime={1}", Constants.RestUrl, id);
            var uri = new Uri(string.Format(resource, string.Empty));

            //string resource = String.Format("{0}workingTime", Constants.RestUrl);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				WorkingTime successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        // Leistungserfassung
        public async Task<List<OrderAchievement>> GetNewOrderAchievementAsync(string idOrder, string idAchievement, bool start, bool listDetail)
        {
            // http://localhost:63491/api/orderAchievement/neu?idOrder=00000000-0000-0000-0000-000000000000&idAchievement=00000000-0000-0000-0000-000000000000&start=true&listDetail=true

            List<OrderAchievement> list = null;
            string resource = string.Empty;
            string guidEmpty = "00000000-0000-0000-0000-000000000000";

            if (idAchievement.Length == 0 && idOrder.Length == 0)
                resource = String.Format("{0}orderAchievement/neu?idOrder={1}&idAchievement{2}&start={1}&listDetail={2}", Constants.RestUrl, guidEmpty, guidEmpty, start, listDetail);
            else
                resource = String.Format("{0}orderAchievement/neu?idOrder={1}&idAchievement{2}&start={1}&listDetail={2}", Constants.RestUrl, idOrder, idAchievement, start, listDetail);

            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var cont = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                var response = await client.PutAsync(uri, cont);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<OrderAchievement>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }
        public async Task<List<OrderAchievement>> GetListOrderAchievementAsync(DateTime day, bool listDetail)
        {
            // http://localhost:63491/api/orderAchievement/list/detail?start=2017-11-10
            // http://localhost:63491/api/orderAchievement/list/cumulated?start=2017-11-10

            List<OrderAchievement> list = null;

            string ListTyp = "cumulated";
            if (listDetail)
                ListTyp = "detail";

            string resource = String.Format("{0}orderAchievement/list/{1}?&start={2}", Constants.RestUrl, ListTyp, day.ToString("yyyy-MM-dd"));
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<OrderAchievement>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }
        public async Task<bool> UpdateOrderAchievement(OrderAchievement orderAchievement)
        {

            // RestUrl = http://localhost:63491/api/orderAchievement

            string resource = String.Format("{0}orderAchievement", Constants.RestUrl);
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(orderAchievement);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				OrderAchievement successfully saved.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }
        public async Task<List<Order>> GetOrderList(string search)
        {
            List<Order> list = null;

            // http://localhost:63491/api/order/list/readcount?SearchValue=Manufacture%20Wolf%20SA
            string resource = String.Format("{0}order/list?SearchValue={1}", Constants.RestUrl, search);
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Order>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }
        public async Task<List<Achievement>> GetAchievementList(string search)
        {
            List<Achievement> list = null;

            // http://localhost:63491/api/order/list/readcount?SearchValue=Manufacture%20Wolf%20SA
            string resource = String.Format("{0}achivement/list?SearchValue={1}", Constants.RestUrl, search);
            var uri = new Uri(string.Format(resource, string.Empty));

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    list = JsonConvert.DeserializeObject<List<Achievement>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return list;
        }

    }
}
