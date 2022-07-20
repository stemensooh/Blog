
declare let $: any;

export function IniciarLoading() {
    // $('body').append('<div class="overlay"><div class="opacity center"><div class="lds-roller"><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div></div></div>');
    // $('.overlay').fadeIn(300);
};
export function DetenerLoading() {

    $('.overlay').fadeOut(300, function () {
        $('.overlay').remove();
    });
};