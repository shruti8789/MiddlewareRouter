using System.Collections.Generic;

namespace MiddlewareRouterNS
{
    public class ExactMatchRouter : IRouter
    {
        //private static ExactMatchRouter _instance;
        //private static readonly object _lock = new object();
        private static readonly Lazy<ExactMatchRouter> _lazyInstance =
            new Lazy<ExactMatchRouter>(() => new ExactMatchRouter());

        private readonly Dictionary<string, string> _pathMap;

        private ExactMatchRouter()
        {
            _pathMap = new Dictionary<string, string>();
        }

        public static ExactMatchRouter Instance => _lazyInstance.Value;

        //public static ExactMatchRouter Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lock)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new ExactMatchRouter();
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        public void AddRoute(string path, string result)
        {
            _pathMap[path] = result;
        }

        public string CallRoute(string path)
        {
            return _pathMap.TryGetValue(path, out string? result) ? result : string.Empty;
        }
    }
}
