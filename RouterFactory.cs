using System.Collections.Generic;

namespace MiddlewareRouterNS
{
    public class RouterFactory
    {
        public RouterFactory()
        {
        }

        public IRouter GetRouter(string path)
        {
            if (path.Contains("*") || path.Contains("{"))
            {
                return WildcardRouter.Instance;
            }
            else
            {
                return ExactMatchRouter.Instance;
            }
        }

        public List<IRouter> GetAllRouters()
        {
            return new List<IRouter>
            {
                ExactMatchRouter.Instance,
                WildcardRouter.Instance
            };
        }
    }
}
