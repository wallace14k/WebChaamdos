function Sucesso(data) {
    Swal.fire(
        'Sucesso!',
        data.msg,
        'success'
    );
}


function Falha(data) {

    Swal.fire({
        icon: 'error',
        title: 'Oops...',        
        text: 'Ocorreu um erro tente mais tarde!',
       
    });
}