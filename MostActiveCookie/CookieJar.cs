using System;
using System.Collections.Generic;
using System.Linq;

namespace MostActiveCookie
{
    public class CookieJar
    {
        private readonly Dictionary<long, Dictionary<string, int>> _cookies;

        public CookieJar()
        {
            _cookies = new Dictionary<long, Dictionary<string, int>>();
        }

        public List<string> FindMostActiveFor(DateTimeOffset dateToFind)
        {
            if (!_cookies.ContainsKey(dateToFind.Date.Ticks))
            {
                return new List<string>();
            }

            var cookiesOfTheDay = _cookies[dateToFind.Date.Ticks];
            var cookiesGroupedByActivity = new Dictionary<int, List<string>>();
            
            foreach (var cookie in cookiesOfTheDay)
            {
                if (cookiesGroupedByActivity.ContainsKey(cookie.Value))
                {
                    cookiesGroupedByActivity[cookie.Value].Add(cookie.Key);
                }
                else
                {
                    cookiesGroupedByActivity.Add(cookie.Value, new List<string>{cookie.Key});
                }
            }

            return cookiesGroupedByActivity
                .OrderByDescending(cookie => cookie.Key)
                .First()
                .Value;
        }

        public void AddCookie(string cookieValue, DateTimeOffset cookieKey)
        {
            if (!_cookies.ContainsKey(cookieKey.Date.Ticks))
            {
                _cookies.Add(cookieKey.Date.Ticks,new Dictionary<string, int> {{cookieValue, 1}});
                return;
            }
            
            var cookiesOfTheDay = _cookies[cookieKey.Date.Ticks];
            if (cookiesOfTheDay.ContainsKey(cookieValue))
            {
                cookiesOfTheDay[cookieValue]++;
            }
            else
            {
                cookiesOfTheDay.Add(cookieValue,1);
            }
        }
    }
}