# Custom Route Paths 

By default, the path of the generated routes are of the form `/{typeName}/{methodName}`. For our `IMusicStore` type, the method `createAlbum` will have the route `/IMusicStore/createAlbum`. You can override this behaviour by providing your own route builder. For example, to prefix your route with `/api` you can use the following:

```fs
/// Prefix routes with /api/
let routeBuilder (typeName: string) (methodName: string) = 
    sprintf "/api/%s/%s" typeName methodName

// now use it with remoting builder
let webApp = 
    Remoting.createApi()
    |> Remoting.withRouteBuilder routeBuilder
    |> Remoting.fromValue musicStore 
```
Ofcourse, the routes must match both on client and server, so we override the behaviour on the client too:
```fs
// Assuming the function routeBuilder is shared between the client and server
// musicStore : IMusicStore
let musicStore : IMusicStore = 
  Remoting.createApi()
  |> Remoting.withRouteBuilder routeBuilder 
  |> Remoting.buildProxy<IMusicStore> 
```
One last thing you need to consider if you are using `webpack-dev-server` is that you need to delegate the requests that start with `/api` from the developement server to the F# backend using the following configuration:
```js
devServer: {
  contentBase: resolve('./public'),
  port: 8080,
  proxy: {
    // delegate the requests prefixed with /api/
    '/api/*': {
      target: "http://localhost:8083",
      changeOrigin: true
    }
}
```