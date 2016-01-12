function Binder(elementOrBind)
{
    function addBind(root,item)
    {
        if (root.$binding)
        {
            var x = root.$binding[item.dataset.bind];

            if (x)
            {
                if (!Array.isArray(x))
                {
                    x = [x];
                }

                x.push(item);
                root.$binding[item.dataset.bind] = x;
                return;
            }

        }
        else
        {
            root.$binding = {};
        }

        root.$binding[item.dataset.bind] = item;

    }


    if (typeof elementOrBind === "string")
    {
        this.rootElement = document.querySelector("[data-bind='" + elementOrBind + "']");
    }
    else
    {
        this.rootElement = elementOrBind;
    }

    this.rootElement.$binder = this;


    for (var query = this.rootElement.querySelectorAll("[data-bind]"), len = query.length, i = 0,x; i < len; i++)
    {
        x = query[i];

        for (var parent = x.parentNode; parent; parent = parent.parentNode)
        {
            if (parent.dataset && parent.dataset.bind)
            {
                addBind(parent, x);
                break;
            }
        }

    }

    elementOrName = null;

}


Binder.getDataSource = function (element)
{
    for (var x = element; x; x = x.parentNode)
    {
        if (x.$dataSource)
        {
            return x.$dataSource;
        }
    }
}

Binder.getBinder = function (elementOrBind)
{
    if (typeof elementOrBind === "string")
    {
        elementOrBind = document.querySelector("[data-bind='" + elementOrBind + "']");
    }

    if (!elementOrBind.$binder)
    {
        new Binder(elementOrBind);
    }

    return elementOrBind.$binder;
}



Binder.prototype.bindToObject = function (dataSource, element)
{


    if (!element)
    {
        element = this.rootElement;
    }

   
    if (!element)
    {
        return;
    }

    element.$dataSource = dataSource;

    for (var x in dataSource)
    {
        var obj = dataSource[x];


        var item = element.$binding[x];

        if (!item)
        {
            continue;
        }


        if (Array.isArray(obj))
        {
            this.bindToArray(obj, item);
            continue;
        }


        if (typeof obj === typeof {})
        {

            if (Array.isArray(item))
            {
                for (var i = 0 ; i < item.length; i++)
                {
                    this.bindToObject(obj, item[i]);
                }
                continue;
            }


            this.bindToObject(obj, item);
            continue;
        }


        if (item.tagName == "INPUT")
        {
            item.value = obj;
            continue;
        }

        if (item.tagName == "SELECT")
        {
            item.value = obj;
            continue;
        }




        item.innerHTML = obj;
   
    }

    return element;
}


    
Binder.prototype.bindToArray = function (dataSource, element)
{

        function appendSiblingClone(element)
        {
            clonedElement             = element.cloneNode(true);
            clonedElement.$dataSource = element.$dataSource;

            if(element.parentNode.$childClones)
            {
                element.parentNode.$childClones.push(clonedElement);
            }
            else
            {
                element.parentNode.$childClones = [clonedElement];
            }

            element.parentNode.appendChild(clonedElement);
        }

        function clearSiblingClones(element)
        {
            if (element.parentNode.$childClones)
            {
                for (var len = element.parentNode.$childClones.length, i = 0; i < len; i++)
                {
                    element.parentNode.removeChild(element.parentNode.$childClones[i]);
                }

                delete element.parentNode.$childClones;
            }
        }

        if (!element)
        {
            element = this.rootElement;
        }

        clearSiblingClones(element);

        for (var len = dataSource.length, i=0; i < len; i++)
        {
            appendSiblingClone(this.bindToObject(dataSource[i], element));
        }

        element.style.display = 'none';

}


Binder.prototype.getDelta = function ()
{
    var delta = {};

    for (var x in this.dataSource)
    {
        var obj = this.dataSource[x];

        if(obj && obj.hasOwnProperty('newValue'))
        {
            delta[x] = obj.newValue;
        }
    }

    return delta;
}

Binder.prototype.getData = function ()
{
    var data = {};

    for (var x in this.dataSource)
    {
        var obj = this.dataSource[x];

        if (!obj || obj.IsReadOnly)
        {
            continue;
        }


        if (obj.hasOwnProperty('newValue'))
        {
            data[x] = obj.newValue;
            continue;
        }

        data[x] = obj.Value;
    }

    return data;

}


Binder.prototype.updateValue = function (name, value)
{
    var obj = this.dataSource[name];

    if(obj && !obj.IsReadOnly)
    {
        obj.newValue = value;
    }
}

Binder.prototype.bindToForm = function (dataSource,onError,onSuccess)
{

    this.dataSource = dataSource;

    for (var x in dataSource)
    {
        var control = this.rootElement.$binding[x];
        var obj     = dataSource[x];


        if (!control)
        {
            continue;
        }

        if (obj.IsReadOnly)
        {
            control.disable = "disable";
        }

        if (onError && obj.IsError)
        {
            onError.call(control);
        }
        
        if (onSuccess && !obj.IsError)
        {
            onSuccess.call(control);
        }



        control.$binder = this;

        switch (control.tagName)
        {
            case 'INPUT':
            {

                if (control.type == "checkbox"||control.type=="radio")
                {
                    control.checked = obj.Value;
                    control.onchange = function () { this.$binder.updateValue(this.dataset.bind, this.checked); }
                    break;
                }

                control.value = "";

                if (obj.Value)
                {
                    control.value = obj.Value;
                }



                control.onchange = function () { this.$binder.updateValue(this.dataset.bind, this.value); }
                break;
            }

            case 'TEXTAREA':
            {
                control.innerHTML = "";

                 if (obj.Value)
                 {
                        control.innerHTML = obj.Value;
                 }

                control.onchange = function () { this.$binder.updateValue(this.dataset.bind, this.innerHTML); }
                break;
            }
           
            case 'SELECT':
            {
                function isEqual(x, y)
                {
                    if (x && y)
                    {
                        for (var i in x)
                        {
                            if (x[i] != y[i])
                            {
                                return false;
                            }
                        }

                        return true;
                    }

                }
                

                while (control.length)
                {
                   control.remove(0);
                }

                if (obj.ValidValues)
                {

                    for (var i = 0, len = obj.ValidValues.length; i < len; i++)
                    {
                        var x = obj.ValidValues[i];

                        var option       = document.createElement("option");
                        option.value     = x.Id;
                        option.innerHTML = x.Name;
                        option.$dataValue = x;

                        if (isEqual(obj.Value,x))
                        {
                            option.selected = "selected";
                        }

                        control.add(option);
                    }

                    control.onchange = function () { this.$binder.updateValue(this.dataset.bind, this.options[this.selectedIndex].$dataValue); }
                    break;

                }

                var singleOption = document.createElement("option");

                singleOption.value     = obj.Value;
                singleOption.innerHTML = obj.Value.Name;

                control.add(singleOption);
                break;
            }
        }

    }
}


