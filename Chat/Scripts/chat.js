// Carrega as Mensagens da Base e mostra na tela.

var recebeMensagens = function () {
    $.get("ChatApi/Receive", function (resultado) {
        debug = resultado;
        
        if (resultado.length > 0) {
            for (var i = 0; i < resultado.length; i++) {

                if ($("#" + resultado[i].NomeConversa).length == 0) {
                    $("#conversas").append("<div id=\"" + resultado[i].NomeConversa + "\">" + resultado[i].NomeConversa + "</div>");
                    $("#" + resultado[i].NomeConversa).append("<div id=\"" + resultado[i].NomeConversa + "mensagens\"></div>");
                   
                }

                if ($("#" + resultado[i].MensagemId + "mensagem").length == 0) {
                    $("#" + resultado[i].NomeConversa + "mensagens").append("<div id=\"" + resultado[i].MensagemId + "mensagem\">" + resultado[i].Autor + " disse às " + resultado[i].Hora + ":" + resultado[i].Conteudo + "</div>");
                }
            }
        }
    });

};





var mostraLogados = function () {
  $.get("ChatApi/Logados", function (resultado) {
        for (var i = 0; i < resultado.length; i++) {
            $("#logados").append("<button id="+ resultado[i].Nome +" class="+" card-panel center-align" +" value="+resultado[i].Nome+">"+ resultado[i].Nome+ "</button>")
            $("#"+resultado[i].Nome).css("background-color", getRandomColor());
        }
        
    });
  
}

$("#enviar_btn").click(function(){
    $.post('/ChatApi/Send',"conv="+ $("#inputConversa").val() +"&"+"mensagem="+ $("#conteudo").val()
        
    );

    recebeMensagens();
    
})
    


    
// Teste de busca pelo nome do usuário 
$(document).ready(function () {
    recebeMensagens();
    mostraLogados();
    
   setInterval(recebeMensagens, 1000);
    
}
);

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++ ) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}