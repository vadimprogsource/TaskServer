function HttpClient(method, uri)
{
    this.method   = method;
    this.uri      = uri;
}


HttpClient.createRequest = function ()
{
    if (window.XMLHttpRequest)
    {
        return new XMLHttpRequest();
    }


    try
    {
        return new ActiveXObject('Msxml2.XMLHTTP');
    }
    catch (e)
    {
        try
        {
            return new ActiveXObject("Microsoft.XMLHTTP");
        }
        catch (e)
        {
            return null;
        }

    }
}

HttpClient.prototype.onReceive = function (response)
{
    alert(response);
}


HttpClient.prototype.onError = function (request)
{
    alert("ERROR:"+request.status);
}


HttpClient.prototype.executeRequest = function (contentType, requestBody)
{
    var request = HttpClient.createRequest();

    request.open(this.method, this.uri, true);

    request.setRequestHeader('Content-Type', contentType);

    var clientPtr = this;


    request.onreadystatechange = function ()
    {
        if (request.readyState == 4)
        {
            if (request.status == 200)
            {
                clientPtr.onReceive.call(clientPtr, request.response);
                return;
            }

            clientPtr.onError.call(clientPtr, request);
        }
    }


    request.send(requestBody);

}




