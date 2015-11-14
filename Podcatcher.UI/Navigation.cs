using Podcatcher.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Podcatcher.UI
{
    public static class Navigation
    {

        public static void GoTo(this Page page, string pageName, object data)
        {
            var factory = new ModelFactory();
            var param = factory.Serialise(data);
            GoTo(page, pageName, param);
        }

        public static void GoTo(this Page page, string pageName, string data)
        {
            var uriStr = string.Format("{0}?data={1}", pageName, data);
            var uri = new Uri(uriStr, UriKind.Relative);
            page.NavigationService.Navigate(uri);
        }

        public static T GetParam<T>(this Page page)
        {
            string value;
            var queries = GetParams(page);
            if(queries.TryGetValue("data", out value))
            {
                var factory = new ModelFactory();
                var result = factory.Deserialise<T>(value);
                return result;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public static Dictionary<string, string> GetParams(this Page page)
        {
            var queries = new Dictionary<string, string>();
            var qSplits = page.NavigationService.CurrentSource.OriginalString.Split('?');
            if (qSplits.Length > 1)
            {
                var query = string.Join(string.Empty, qSplits.Skip(1));

                var pairs = query.Split('&');
                foreach (var pair in pairs)
                {
                    var splits = pair.Split('=');
                    string value = "";
                    if (splits.Length > 1)
                    {
                        value = string.Join(string.Empty, splits.Skip(1));
                    }

                    queries.Add(splits.First(), value);
                }
            }
            return queries;
        }
    }
}
