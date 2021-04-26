using System;
using MostActiveCookie;
using Xunit;

namespace Tests
{
    public class CookieJarTests
    {
        private readonly CookieJar _cookieJar;

        public CookieJarTests()
        {
            _cookieJar = new CookieJar();
        }

        [Fact]
        public void Return_Empty_When_Date_Has_No_Cookies()
        {
            var mostActiveCookies = _cookieJar.FindMostActiveFor(DateTimeOffset.Now);
            
            Assert.Empty(mostActiveCookies);
        }

        [Fact]
        public void Return_one_cookie_When_there_is_only_one_cookie_for_the_date()
        {
            var cookieValue = "123";
            var cookieDate = DateTimeOffset.Now;
            _cookieJar.AddCookie(cookieDate, cookieValue);
            
            var mostActiveCookies = _cookieJar.FindMostActiveFor(cookieDate);

            Assert.Single(mostActiveCookies);
            Assert.Equal(cookieValue, mostActiveCookies[0]);
        }

        [Fact]
        public void Return_one_cookie_When_that_cookie_was_the_most_active_for_the_date()
        {
            var cookieValue1 = "123";
            var cookieDate = DateTimeOffset.Now;
            
            _cookieJar.AddCookie(cookieDate, cookieValue1);
            _cookieJar.AddCookie(cookieDate.AddMinutes(3), cookieValue1);
            _cookieJar.AddCookie(cookieDate.AddMinutes(6), cookieValue1);

            var cookieValue2 = "456";
            _cookieJar.AddCookie(cookieDate.AddMinutes(1), cookieValue2);
            _cookieJar.AddCookie(cookieDate.AddMinutes(2), cookieValue2);
            
            var cookieValue3 = "789";
            _cookieJar.AddCookie(cookieDate.AddDays(1), cookieValue3);

            var mostActiveCookies = _cookieJar.FindMostActiveFor(cookieDate);

            Assert.Single(mostActiveCookies);
            Assert.Equal(cookieValue1, mostActiveCookies[0]);
        }

        [Fact]
        public void Return_more_than_one_cookie_When_all_cookies_of_the_date_had_same_activity()
        {
            var cookieDate = DateTimeOffset.Now;
            
            var cookieValue1 = "123";
            _cookieJar.AddCookie(cookieDate, cookieValue1);
            
            var cookieValue2 = "456";
            _cookieJar.AddCookie(cookieDate, cookieValue2);
            
            var cookieValue3 = "789";
            _cookieJar.AddCookie(cookieDate, cookieValue3);
            
            var mostActiveCookies = _cookieJar.FindMostActiveFor(cookieDate);
            
            Assert.Equal(3, mostActiveCookies.Count);
            Assert.Equal(cookieValue1, mostActiveCookies[0]);
            Assert.Equal(cookieValue2, mostActiveCookies[1]);
            Assert.Equal(cookieValue3, mostActiveCookies[2]);
        }
    }
}