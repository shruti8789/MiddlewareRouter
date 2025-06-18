using System;
using MiddlewareRouterNS;

namespace MiddlewareRouterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new router
            MiddlewareRouter router = new MiddlewareRouter();

            // Add some routes
            router.AddRoute("/foo", "foo");
            router.AddRoute("/bar/*/baz", "bar");
            router.AddRoute("/car/{carId}/caz", "path");

            // Handle requests
            Console.WriteLine(router.CallRoute("/foo"));            // Output: foo
            Console.WriteLine(router.CallRoute("/bar/123/baz"));    // Output: bar
            Console.WriteLine(router.CallRoute("/car/456/caz"));    // Output: path

            // Update existing routes
            router.AddRoute("/bar/*/baz", "barNew");
            router.AddRoute("/car/{carId}/caz", "pathNew");

            Console.WriteLine(router.CallRoute("/bar/123/baz"));    // Output: barNew
            Console.WriteLine(router.CallRoute("/car/456/caz"));    // Output: pathNew
        }
    }
}
