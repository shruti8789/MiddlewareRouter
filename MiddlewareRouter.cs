using System.Collections.Generic;

namespace MiddlewareRouterNS
{
    public interface IRouter
    {
        void AddRoute(string path, string result);
        string CallRoute(string path);
    }

    public class MiddlewareRouter
    {
        private readonly RouterFactory _routerFactory;

        public MiddlewareRouter()
        {
            _routerFactory = new RouterFactory();
        }

        public void AddRoute(string path, string result)
        {
            _routerFactory.GetRouter(path).AddRoute(path, result);
        }

        public string CallRoute(string path)
        {
            var allRouters = _routerFactory.GetAllRouters();
            foreach (var router in allRouters)
            {
                var value = router.CallRoute(path);
                if (value != string.Empty)
                    return value;
            }
            return string.Empty;
        }
    }
}

/*
 *  Time Complexity
➤ AddRoute(path, result)
ExactMatchRouter:
Uses a Dictionary<string, string> → O(1) insertion.

WildcardRouter:

Adds an entry to a Dictionary<string, (Regex, string)> → O(1)

But compiles a Regex → O(R) where R = length of pattern

✅ Overall:

Exact match: O(1)

Wildcard match: O(R) (regex compile time)

➤ CallRoute(path)
MiddlewareRouter calls both:

ExactMatchRouter.CallRoute(path) → O(1) (dictionary lookup)

WildcardRouter.CallRoute(path) → Iterates through all wildcard routes

For M wildcard routes, each calling Regex.IsMatch(path)

✅ Wildcard matching:

Worst-case time = O(M * P)
where:

M = number of wildcard routes

P = average complexity of regex matching (varies with pattern; typically linear but can be worse in pathological cases)

✅ Overall:
CallRoute(path) → O(1 + M * P)
Often, this is dominated by wildcard matching.


2. Space Complexity
ExactMatchRouter:

Stores path-result pairs in dictionary → O(E), where E = exact routes

WildcardRouter:

Stores compiled Regex + result per route → O(W + Σ|pattern|), where W = wildcard routes

Regex compilation memory overhead → small per regex, but adds up

✅ Overall:

Space = O(N) for storing routes (exact + wildcard)

⚡ Summary Table
Operation	Time Complexity	Space Complexity
AddRoute (exact)	O(1)	O(1) per route
AddRoute (wildcard)	O(R) (regex compile)	O(1) per route + regex overhead
CallRoute	O(1 + M * P)	O(N) total storage

🧠 Optimization Tips (if needed)
Use tries or prefix trees for faster path matching

Separate wildcard routes by pattern types (e.g., /foo/*, /bar/{id}) into buckets

Cache frequent matches
*/
