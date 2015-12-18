// Carrega as Mensagens da Base e mostra na tela.
var debug;
var selected;

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


$("#enviar_btn").click(function(){
    $.post('/ChatApi/Send',"conv="+ "igor" +"&"+"mensagem="+ $("#conteudo").val()
        
    );

    recebeMensagens();
    console.log($("#conteudo").val());
})
    

//Fazer requisição à cada 1 segundo
//setInterval(ajax, 1000);

 
// Falta receber o nome do usuário a que se deseja enviar a mensagem 



    
// Teste de busca pelo nome do usuário 
$(document).ready(function () {
    recebeMensagens();
}
);

