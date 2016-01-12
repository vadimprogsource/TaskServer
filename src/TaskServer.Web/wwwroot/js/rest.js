function Rest()
{
}


Rest.executeHttp = function (method, uri, body, lambda)
{
    var httpClient = new HttpClient(method, uri);

    httpClient.onReceive = function (x)
    {
        var obj = JSON.parse(x);
        lambda.call(obj);
    }

    if (body)
    {
        body = JSON.stringify(body);
    }

    httpClient.executeRequest("application/json",body);
}



Rest.GET = function (uri, lambda)
{
    return Rest.executeHttp("GET", uri, null, lambda);
}

Rest.POST = function (uri, body, lambda)
{
    return Rest.executeHttp("POST", uri, body, lambda);
}

Rest.PUT = function (uri, body, lambda)
{
    return Rest.executeHttp("PUT", uri, body, lambda);
}

Rest.DELETE = function (uri, lambda)
{
    return Rest.executeHttp("DELETE", uri, null, lambda);
}

