using System;
using System.Collections.Generic;
using System.Linq;

namespace MostActiveCookie
{
    public class CookieJar
    {
        //group cookies by date as well as track how many times they appeared on that date (aka their activity)
        private readonly IDictionary<long, IDictionary<string, int>> _cookiesByDate;

        public CookieJar()
        {
            _cookiesByDate = new Dictionary<long, IDictionary<string, int>>();
        }

        public void AddCookie(DateTimeOffset cookieDate, string cookie)
        {
            var newDate = cookieDate.Date.Ticks;
            
            if (!_cookiesByDate.ContainsKey(newDate))
            {
                _cookiesByDate.Add(
                    newDate,
                    new Dictionary<string, int>
                    {
                        {cookie, 1}
                    });
                return;
            }
            
            var cookies = _cookiesByDate[newDate];
            if (cookies.ContainsKey(cookie))
            {
                cookies[cookie]++;
            }
            else
            {
                cookies.Add(cookie,1);
            }
        }

        public IList<string> FindMostActiveFor(DateTimeOffset dateToFindBy)
        {
            var dateKey = dateToFindBy.Date.Ticks;
            
            if (!_cookiesByDate.ContainsKey(dateKey))
            {
                return new List<string>();
            }

            // group cookies by the amount of times they appeared (aka their activity)
            var cookiesByActivity = new Dictionary<int, IList<string>>();
            
            var cookies = _cookiesByDate[dateKey];
            foreach (var (cookie, cookieActivity) in cookies)
            {
                if (cookiesByActivity.ContainsKey(cookieActivity))
                {
                    cookiesByActivity[cookieActivity].Add(cookie);
                }
                else
                {
                    cookiesByActivity.Add(
                        cookieActivity,
                        new List<string> {cookie});
                }
            }

            return cookiesByActivity
                .OrderByDescending(activity => activity.Key)
                .First()
                .Value;
        }
    }
}