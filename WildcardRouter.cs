using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiddlewareRouterNS
{
    internal class PatternWithValue
    {
        public Regex RegexPattern { get; }
        public string Result { get; }

        public PatternWithValue(Regex pattern, string value)
        {
            RegexPattern = pattern;
            Result = value;
        }
    }

    public class WildcardRouter : IRouter
    {
        //private static WildcardRouter _instance;
        //private static readonly object _lock = new object();
        private static readonly Lazy<WildcardRouter> _lazyInstance =
            new Lazy<WildcardRouter>(() => new WildcardRouter());

        private readonly List<PatternWithValue> _patterns;

        private WildcardRouter()
        {
            _patterns = new List<PatternWithValue>();
        }

        public static WildcardRouter Instance => _lazyInstance.Value;
        //public static WildcardRouter Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lock)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new WildcardRouter();
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        public void AddRoute(string path, string result)
        {
            // Convert wildcard and path parameters into regex
            string regexPattern = "^" + Regex.Replace(path, @"\*", "[^/]+");
            regexPattern = Regex.Replace(regexPattern, @"\{[^/]+\}", "([^/]+)") + "$";
            var regex = new Regex(regexPattern, RegexOptions.Compiled);

            // Remove any existing pattern with same regex (to allow updates)
            _patterns.RemoveAll(p => p.RegexPattern.ToString() == regex.ToString());

            _patterns.Add(new PatternWithValue(regex, result));
        }

        public string CallRoute(string path)
        {
            foreach (var pattern in _patterns)
            {
                //Console.WriteLine(pattern.RegexPattern + "  " + pattern.Result);
                if (pattern.RegexPattern.IsMatch(path))
                {
                    return pattern.Result;
                }
            }
            return string.Empty;
        }
    }
}

/*
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiddlewareRouterNS
{
    internal class PatternWithValue
    {
        public Regex RegexPattern { get; }
        public string Result { get; }

        public PatternWithValue(Regex pattern, string value)
        {
            RegexPattern = pattern;
            Result = value;
        }
    }

    public class WildcardRouter : IRouter
    {
        //private static WildcardRouter _instance;
        //private static readonly object _lock = new object();
        private static readonly Lazy<WildcardRouter> _lazyInstance =
            new Lazy<WildcardRouter>(() => new WildcardRouter());

        private readonly Dictionary<string, PatternWithValue> _patternMap;

        private WildcardRouter()
        {
            _patternMap = new Dictionary<string, PatternWithValue>();
        }

        public static WildcardRouter Instance => _lazyInstance.Value;
        //public static WildcardRouter Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lock)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new WildcardRouter();
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        public void AddRoute(string path, string result)
        {
            // Convert wildcard and path parameter patterns into regex
            string regexPattern = "^" + Regex.Replace(path, @"\*", "[^/]+");
            regexPattern = Regex.Replace(regexPattern, @"\{[^/]+\}", "([^/]+)") + "$";
            var regex = new Regex(regexPattern, RegexOptions.Compiled);
            _patternMap[path] = new PatternWithValue(regex, result);
        }

        //public void AddRoute(string path, string result)
        //{
        //    var regexPattern = Regex.Escape(path)
        //        .Replace("\\*", "[^/]+")
        //        .Replace("\\{[^/]+\\}", "([^/]+)");
        //    // Optionally anchor: ^ and $
        //    var regex = new Regex("^" + regexPattern + "$", RegexOptions.Compiled);
        //    _patternMap[path] = new PatternWithValue(regex, result);
        //}

        public string CallRoute(string path)
        {
            foreach (var entry in _patternMap)
            {
                if (entry.Value.RegexPattern.IsMatch(path))
                {
                    return entry.Value.Result;
                }
            }
            return null;
        }
    }
}
*/