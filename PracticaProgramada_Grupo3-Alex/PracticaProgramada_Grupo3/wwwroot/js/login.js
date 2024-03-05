document.addEventListener('DOMContentLoaded', function () {
    var loginTab = document.getElementById('login-tab');
    var registerTab = document.getElementById('register-tab');
    var loginForm = document.getElementById('login-form');
    var registerForm = document.getElementById('register-form');

    loginTab.addEventListener('click', function () {
        registerForm.classList.remove('form-container__form--active');
        loginForm.classList.add('form-container__form--active');
        registerTab.classList.remove('form-container__tab--active');
        loginTab.classList.add('form-container__tab--active');
    });

    registerTab.addEventListener('click', function () {
        loginForm.classList.remove('form-container__form--active');
        registerForm.classList.add('form-container__form--active');
        loginTab.classList.remove('form-container__tab--active');
        registerTab.classList.add('form-container__tab--active');
    });
});