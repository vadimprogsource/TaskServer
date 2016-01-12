var contentBinder = new Binder(".");
var navBinder     = new Binder("!");

Rest.POST("/", {}, function ()
{
    contentBinder.bindToObject(this);
    navBinder.bindToObject    (this);
});



function ui(){}


ui.applyStatus = function(control)
{
    alert(JSON.stringify(Binder.getDataSource(control)));
    return false;
}

ui.applyPriority = function (control)
{
    alert(JSON.stringify(Binder.getDataSource(control)));
    return false;

}

ui.applyUser = function (control)
{
    alert(JSON.stringify(Binder.getDataSource(control)));
    return false;

}


ui.replaceClass = function (element, className, newClassName)
{
    for (var x = element; x; x = x.parentNode)
    {
        if (x.className == className)
        {
            x.className = newClassName;
            return;
        }
    }
}



ui.onNewTask = function (id)
{
  
    Rest.GET("/create", function ()
    {
        var binder = Binder.getBinder(id);
        binder.bindToForm(this, function () { ui.replaceClass(this, "form-group", "has-error form-group"); }, function () { ui.replaceClass(this, "has-error form-group", "form-group"); });

        $("#" + id).modal();
    });
    

    return false;
}

ui.saveNewTask = function (id)
{

    var binder = Binder.getBinder(id);


    Rest.PUT("/new", binder.getData(), function ()
    {
        if (this.HasError)
        {
            binder.bindToForm(this, function () { ui.replaceClass(this, "form-group", "has-error form-group"); }, function () { ui.replaceClass(this, "has-error form-group", "form-group"); });
        }
    });
}