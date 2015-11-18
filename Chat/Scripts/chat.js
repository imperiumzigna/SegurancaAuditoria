// Carrega as Mensagens da Base e mostra na tela.
var debug;
var selected;

var ajax = function () {
    $.get("ChatApi/Receive", function (resultado) {
        debug = resultado;
        if (resultado.length > 0) {
            for (var i = 0; i < resultado.length; i++) {

                if ($("#" + resultado[i].NomeConversa).length == 0) {
                    $("#conversas").append("<div id=\"" + resultado[i].NomeConversa + "\">" + resultado[i].NomeConversa + "</div>");
                    $("#" + resultado[i].NomeConversa).append("<div id=\"" + resultado[i].NomeConversa + "mensagens\"></div>");
                    //$("#"+resultado[i].NomeConversa).click(
                    //    function() {
                    //        selected = $("#" + resultado[i].NomeConversa).id;
                    //    }
                    //    );
                }

                if ($("#" + resultado[i].MensagemId + "mensagem").length == 0) {
                    $("#" + resultado[i].NomeConversa + "mensagens").append("<div id=\"" + resultado[i].MensagemId + "mensagem\">" + resultado[i].Autor + " disse às " + resultado[i].Hora + ":" + resultado[i].Conteudo + "</div>");
                }
            }
        }
    });

};

$("#enviar").submit(
    function () {
        $.ajax({
            type: 'POST', // define the type of HTTP verb we want to use (POST for our form)
            url: 'ChatApi/Send', // the url where we want to POST
            data: {
                user: '',
                mensagen: ''
            }, // our data object
            dataType: 'json', // what type of data do we expect back from the server
            encode: true
        })
                  // using the done promise callback
                  .done(function (data) {

                      // log data to the console so we can see
                      console.log(data);

                      // here we will handle errors and validation messages
                  });

        // stop the form from submitting the normal way and refreshing the page
        event.preventDefault();
    });
    
    

//Fazer requisição à cada 1 segundo
setInterval(ajax, 1000);

 
// Falta receber o nome do usuário a que se deseja enviar a mensagem 
var enviar = function () {
    var ajax = $.post("ChatApi/Send", {
        user: "",
        mensagem: ""
    });
}
    
// Teste de busca pelo nome do usuário 
$(document).ready(function () {
    ajax();
    

}
);

