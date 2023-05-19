function mostrarDetalles(id, plantilla) {
    window.location = '/'+plantilla +'/Details/' + id;
}

function cambiarPaginaPrincipal() {
    window.location = '/Home/Index';
}

function cambiarPagina(pagina) {
    //window.location.href = 'DecoApp/'+pagina + '/Index';
    window.location.href = '/'+pagina + '/Index';
}

function crearNueva() {
    var url = window.location.href.split("/");
    var pagina = url[3].replace("'", "")
    window.location.href = '/' + pagina + '/Create';
}

function editDetalle() {
    $("#edit").toggle();
}


function borrar() {
    console.log("si");
    var num = window.location.href.split('/')
    var idd = parseInt(num[num.length - 1])
    var pagina = num[num.length - 3].replace('"', '')
    var url = "/" + pagina +"/DeleteConfirmed/" + idd;
    $("#dialog").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Borrar": function () {
                window.location.href = url;
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    }).prev(".ui-dialog-titlebar").css("background", "#8E2727").css("border", "2px solid black");
    
}




function addTareaLista() {
    //$(".tarea").clone().appendTo("#tareasFacturas");
    var tarea = document.querySelector(".tarea")
    var clon = tarea.cloneNode(true);
    var contenedor = document.getElementById("tareasFacturas")
    contenedor.appendChild(clon)
}







function autocompletarCliente( model, input, inputHide) {
    console.log("Si")

    $(input).autocomplete({
       
        source: function (request, response) {
            console.log("Si2")
                $.ajax({
                    url: "/Home/Get"+model+"/",
                    type: "POST",
                    data: { nom: $(input).val() },
                    success: function (data) {
                        console.log(data)
                        response($.map(data, function (item) {
                            console.log(item)
                            return item;
                        }))
                    }
                })
            },
            select: function (e, i) {
                console.log(i)
                $(inputHide).val(i.item.val)
            }
            //minLength:1
        });

}
function autocompletarEstado(input) {
    console.log("Si")

    $(input).autocomplete({

        source: function (request, response) {
            console.log("Si2")
            $.ajax({
                url: "/Home/GetEstado/",
                type: "POST",
                data: { nom: $(input).val() },
                success: function (data) {
                    console.log(data)
                    response($.map(data, function (item) {
                        console.log(item)
                        return item;
                    }))
                }
            })
        },
        select: function (e, i) {
            console.log(i)
            $(input).next("input").val(i.item.val)
            //$(inputHide)
        }
        //minLength:1
    });

}




//PAGGINA CLIENTE VER RELACIONADO
function mostrarFacturas(id) {
            $.ajax({
                url: "/Home/MostrarFactura/",
                type: "POST",
                data: { id: id },
                success: function (data) {
                    console.log(data)
                    var contenedor = document.getElementById("clienteFacturas")
                    if ($(contenedor).html() == "") { 
                    for (var i = 0; i < data.length; i++){
                        var cont = document.createElement("div")
                        cont.className = "contCliente"
 
                        var nom = document.createElement("label")
                        nom.className = "labelEdit"
                        nom.textContent=data[i].id
                        var inf = document.createElement("label")
                        inf.className="inputEdit"
                        inf.textContent = data[i].nombreFactura
                        cont.appendChild(nom)
                        cont.appendChild(inf)
                        contenedor.appendChild(cont)
                        cont.addEventListener("click", function (e) {
                            e.preventDefault();
                            mostrarDetalles(nom.textContent, 'Facturas')
                        })
                        }
                        $("#clienteFacturas").toggle();
                    }
                    $("#clienteFacturas").toggle();
                }
            })
};
function mostrarCitas(id) {
    $.ajax({
        url: "/Home/MostrarCitas/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            console.log(data)
            var contenedor = document.getElementById("clienteCitas")
            console.log("si")
            if ($(contenedor).html() == "") {
                console.log("si2")
                for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    console.log("si3")
                    var cont = document.createElement("div")
                    cont.className = "contCliente"

                    var nom = document.createElement("label")
                    nom.className = "labelEdit"
                    nom.textContent = data[i].id
                    var inf = document.createElement("label")
                    inf.className = "inputEdit"
                    inf.textContent = data[i].fecha
                    cont.appendChild(nom)
                    cont.appendChild(inf)
                    contenedor.appendChild(cont)
                    cont.addEventListener("click", function (e) {
                        e.preventDefault();
                        mostrarDetalles(nom.textContent, 'Citas')
                    })
                }
                $("#clienteCitas").toggle();
            }
            $("#clienteCitas").toggle();
        }
    })
};
function mostrarObras(id) {
    $.ajax({
        url: "/Home/MostrarObras/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            console.log(data)
            var contenedor = document.getElementById("clienteObras")
            if ($(contenedor).html() == "") {
                for (var i = 0; i < data.length; i++) {
                    var cont = document.createElement("div")
                    cont.className = "contCliente"

                    var nom = document.createElement("label")
                    nom.className = "labelEdit"
                    nom.textContent = data[i].id
                    var inf = document.createElement("label")
                    inf.className = "inputEdit"
                    inf.textContent = data[i].direccion
                    cont.appendChild(nom)
                    cont.appendChild(inf)
                    contenedor.appendChild(cont)
                    cont.addEventListener("click", function (e) {
                        e.preventDefault();
                        mostrarDetalles(nom.textContent, 'Obras')
                    })
                }
                $("#clienteObras").toggle();
            }
            $("#clienteObras").toggle();
        }
    })
};






//GUARDAR TAREAS
function editTarea(id) {
                $("#tareasFacturas").toggle();
        }

function editTarea2(id) {
    $.ajax({
        url: "/Home/MostrarTareas/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            var contenedor = document.getElementById("tareasFacturas")
            while (contenedor.firstChild) { contenedor.removeChild(contenedor.firstChild) }
            //$(contenedor).html("");
            for (var i = 0; i < data.length; i++) {
                    console.log(data[i])
                    var cont = document.createElement("div")
                cont.className = "tarea"
                cont.id = data[i].id
                cont.addEventListener("click", function (e) {
                    e.preventDefault();
                    borrarTarea(cont.id, id)
                })
                    
                    var des = document.createElement("label")
                    des.className = "labelEdit"
                    des.textContent = "Descripcion"
                    var desInp = document.createElement("input")
                    desInp.className = "descripcion InputEdit"
                    desInp.name = "Descripcion"
                desInp.type = "text"
                desInp.disabled=true
                    desInp.value = data[i].descripcion

                    var pre = document.createElement("label")
                    pre.className = "labelEdit"
                    pre.textContent = "Precio"
                    var preInp = document.createElement("input")
                    preInp.className = "precio InputEdit numE"
                    preInp.name = "Precio"
                preInp.type = "number"
                preInp.disabled = true
                preInp.value = data[i].precio

                    var can = document.createElement("label")
                    can.className = "labelEdit"
                    can.textContent = "Cantidad"
                    var canInp = document.createElement("input")
                    canInp.className = "cantidad InputEdit numE"
                    canInp.name = "Cantidad"
                canInp.type = "number"
                canInp.disabled = true
                canInp.value = data[i].cantidad

                    var d = document.createElement("label")
                    d.className = "labelEdit"
                    d.textContent = "Descuento"
                    var dInp = document.createElement("input")
                    dInp.className = "descuento InputEdit numE"
                    dInp.name = "Descuento"
                dInp.type = "number"
                dInp.disabled = true
                dInp.value = data[i].descuento



                var es = document.createElement("label")
                es.className = "labelEdit"
                es.textContent = "Estado"
                var esInp = document.createElement("input")
                esInp.className = "InputEdit"
                esInp.name = "est2"
                esInp.id = "est2"
                esInp.type = "text"
                esInp.disabled = true
                //esInp.value = data[i].estado.Nombre
                var esInp2 = document.createElement("input")
                esInp2.type = "hidden"
                esInp2.name = "Estado2"
                esInp2.id = "Estado2"
                esInp2.value = data[i].idEstado

                    cont.appendChild(des)
                    cont.appendChild(desInp)
                    cont.appendChild(pre)
                    cont.appendChild(preInp)
                    cont.appendChild(can)
                    cont.appendChild(canInp)
                    cont.appendChild(d)
                    cont.appendChild(dInp)
                    cont.appendChild(es)
                    cont.appendChild(esInp)
                    cont.appendChild(esInp2)


                    contenedor.appendChild(cont)
            }
    $("#tareasFacturas").toggle();
}
    })
}





//ANADIR CAMPO DE TAREA EN FACTURAS- ANADIR TAREA
function addTareaLista() {
   /* var contenedor = document.getElementById("tareasFacturas")
    var cont = document.createElement("div")
    cont.className = "tarea"

    var des = document.createElement("label")
    des.className = "labelEdit"
    des.textContent = "Descripcion"
    var desInp = document.createElement("input")
    desInp.className = "descripcion InputEdit"
    desInp.name = "Descripcion"
    desInp.type = "text"

    var pre = document.createElement("label")
    pre.className = "labelEdit"
    pre.textContent = "Precio"
    var preInp = document.createElement("input")
    preInp.className = "precio InputEdit numE"
    preInp.name = "Precio"
    preInp.type = "number"

    var can = document.createElement("label")
    can.className = "labelEdit"
    can.textContent = "Cantidad"
    var canInp = document.createElement("input")
    canInp.className = "cantidad InputEdit numE"
    canInp.name = "Cantidad"
    canInp.type = "number"

    var d = document.createElement("label")
    d.className = "labelEdit"
    d.textContent = "Descuento"
    var dInp = document.createElement("input")
    dInp.className = "descuento InputEdit numE"
    dInp.name = "Descuento"
    dInp.type = "number"

    var es = document.createElement("label")
    es.className = "labelEdit"
    es.textContent = "Estado"
    var esInp = document.createElement("input")
    esInp.className = "estado InputEdit"
    esInp.name = "Estado"
    esInp.type = "text"

    var es = document.createElement("label")
    es.className = "labelEdit"
    es.textContent = "Estado"
    var esInp = document.createElement("input")
    esInp.className = "InputEdit"
    esInp.name = "est"
    esInp.id = "est"
    esInp.type = "text"
    var esInp2 = document.createElement("input")
    esInp2.type = "hidden"
    esInp2.name = "Estado"
    esInp2.id = "Estado"
    esInp.addEventListener("click", function (e) {
        e.preventDefault()
        autocompletarCliente('Estado', '#est', '#Estado')
    })



    cont.appendChild(des)
    cont.appendChild(desInp)
    cont.appendChild(pre)
    cont.appendChild(preInp)
    cont.appendChild(can)
    cont.appendChild(canInp)
    cont.appendChild(d)
    cont.appendChild(dInp)
    cont.appendChild(es)
    cont.appendChild(esInp)
    cont.appendChild(esInp2)
    //contenedor.append(cont)*/
    var tarea = document.querySelector(".tarea")
    var clon = tarea.cloneNode(true);
    for (var i = 0; i < clon.childNodes.length; i++) {
        clon.childNodes[i].value=''
    }
    //var contenedor = document.getElementById("tareasFacturas")
    $("#addTarea").before(clon)
}

//GUARDAR TAREAS EN FACTURAS- ANADIR TAREA
function guardarTarea() {
    var listaDescripcion = document.querySelectorAll(".descripcion")
    var listaPrecio = document.querySelectorAll(".precio")
    var listaCantidad = document.querySelectorAll(".cantidad")
    var listaDescuento = document.querySelectorAll(".descuento")
    var listaEstado = document.querySelectorAll("#estadoT")
    console.log(listaEstado[0])
    console.log(listaEstado[0].value)
    var descripcion = [];
    var descuento = [];
    var cantidad = [];
    var precio = [];
    var estado = [];
    for (var i = 0; i < listaDescripcion.length; i++) {

        descripcion.push(listaDescripcion[i].value);
        descuento.push(listaDescuento[i].value);
        cantidad.push(listaCantidad[i].value);
        precio.push(listaPrecio[i].value);
        estado.push(listaEstado[i].value);
    }
    console.log(descripcion)
    $.ajax({
        url: "/Facturas/Tarea/",
        type: "POST",
        data: { id: $("#Id").val(), descripcion: descripcion, cantidad: cantidad, precio: precio, descuento: descuento, estado: estado },
        //success: function (data) {console.log(data) }
    })
}


//Borrar tarea al pulsar encima
function borrarTarea(id, idd) {
    /*$.ajax({
        url: "/Facturas/DeleteTarea/",
        type: "POST",
        data: { id: id, id2: idd},
        success: function (data) { console.log(data) }
    })*/
                /*alertify.set({ buttonReverse: true, labels: { ok: "Borrar", cancel: "Cancelar"} });
                alertify.confirm("Se borrara la siguiente tarea<strong><%=Model.UnidadPesoActiva %></strong>"+lista, function (e) {
                    if (e) {
							var url = "/Facturas/DeleteTarea?id=" + id+"&id2="+idd;
    window.location.href = url;
                    }
                });*/
    /*if (confirm('Some message')) {
        alert('Thanks for confirming');
    } else {
        alert('Why did you press cancel? You should have confirmed');
    }*/
    $("#dialogF").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Borrar": function () {
                var url = "/Facturas/DeleteTarea?id=" + id + "&id2=" + idd;
                window.location.href = url;
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function imprimir(id, plantilla) {
    /*$.ajax({
        url: "/Facturas/DeleteTarea/",
        type: "POST",
        data: { id: id, id2: idd},
        success: function (data) { console.log(data) }
    })*/
    var url = "/Facturas/ImprimirFactura?id=" + id+"&plantilla="+plantilla;
    window.location.href = url;
}



//MOSTRAR CONTRASENA
function pswd() {
    $(".passwd").toggleClass("passwdMostrar");
}

//MOSTRAR DESPLEGABLE BOTON
function mostrarDesplegable() {
    $(".contenedorDesplegable").toggle();
}


//CALENDARIO
function crearMes(dia) {
    var contenedor = document.getElementById("contenedorMes")
    while (contenedor.firstChild) {
        contenedor.removeChild(contenedor.firstChild)
    }
    var mes = new Date(2023, dia, 0).getDate();
    for (var i = 1; i <= mes; i++) {
        var div = document.createElement("div");
        var aux = "";
        if (i < 10) {
            aux = "0" + i;
        } else {
            aux=i
        }
        div.setAttribute("id", aux);
        div.className = "dia";
        div.textContent = i;
        contenedor.appendChild(div)
    }
    console.log(mes)
}
function obrasCalendario(mes) {
    var texto = document.querySelector(".mesActual")
    var mes = texto.getAttribute("value");
    $.ajax({
        url: "/Home/ObrasCalendario/",
        type: "POST",
        data: { mes: mes },
        success: function (data) {
            console.log(data)
            for (var i = 0; i < data.length; i++) {
                //var j = data[i].id;
                var fecha = data[i].fechaInicio.split("-");
                var dia = fecha[2].substring(0, 2)

                var cont = document.getElementById(dia)

                var label = document.createElement("label");
                label.textContent = "Obra: " + data[i].direccion;
                label.className = 'labelCalendario ObraCalendario';
                label.id = data[i].id;
                var j = data[i].id;
                label.addEventListener("click", function (data) {
                    mostrarDetalles(j, "Obras");
                });
                cont.appendChild(label);
            }
        }
    })
}

function citasCalendario(mes) {
    var texto = document.querySelector(".mesActual")
    var mes = texto.getAttribute("value");
    $.ajax({
        url: "/Home/CitasCalendario/",
        type: "POST",
        data: { mes: mes },
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                console.log(data[i].fecha)
                var fecha = data[i].fecha.split("-");
                var dia = fecha[2].substring(0, 2)
                var cont = document.getElementById(dia)
                var label = document.createElement("label");
                label.textContent = "Cita: " + data[i].id;
                label.className = 'labelCalendario CitaCalendario';
                label.id = data[i].id;
                var j = data[i].id;
                label.addEventListener("click", function(data){
                    mostrarDetalles(j, "Citas");
            });
                cont.appendChild(label);
            }

        }
    })

}

var listaMes = ["", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"]
function CambiarMesMas() {
    var texto = document.querySelector(".mesActual")
    var mes = texto.getAttribute("value");
    mes++
    texto.setAttribute("value", mes);
    texto.textContent = listaMes[mes]
    crearMes(mes)
    obrasCalendario(mes)
    citasCalendario(mes)
}

function CambiarMesMenos() {
    var texto = document.querySelector(".mesActual")
    var mes = texto.getAttribute("value");
    mes--
    texto.setAttribute("value", mes);
    texto.textContent = listaMes[mes]
    crearMes(mes)
    obrasCalendario(mes)
    citasCalendario(mes)
}




