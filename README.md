# MiddlewareRouter


Middleware router -
Implement a middleware router for a web service, which is based on the paths return different strings.
Interface looks like - 
interface Router {
fun addRoute(path: String, result: String) : Unit;
fun callRoute(path: String) : String;
}
Usage:
Router.addRoute(“/bar”, “result”)
Router.callRoute(“/bar”) -> “result”
Other examples:
Router.addRoute("/bar", "result")
Router.callRoute("/bar") -> output : "result"
Router.addRoute("/foo/bar", "result2")
Router.callRoute("/foo/bar") -> output: "result2"
You don't need to implement as it is, you can use other ways of abstraction.

Follow up 
We need to update values of the route, so the value with the latest call to addRoute should be returned when callRoute is invoked
Implementation Note: In hashmap, if we are using Pattern, then map. Contains would not work , so we need to wrap it in a custom class, so that we have exact string of the pattern which we can check against to update the new value for that route

Follow up - Extend the route declarations such that we can have wildcards in the paths.
Router router = new Router();
router.addRoute(“/foo”, “foo”)
router.addRoute(“/bar/*/baz”, “bar”)
Second follow up - Allow the ability to add arguments to the handlers via path parameters.
router.addRoute(“/foo/{id}”, “foo”)
