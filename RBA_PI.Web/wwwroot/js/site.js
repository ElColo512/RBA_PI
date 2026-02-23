// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*----------------------------------------Mensaje Toast----------------------------------------*/
document.addEventListener("DOMContentLoaded", function () {
    if (window.toastMessage && window.toastMessage.trim() !== "") {
        var toastEl = document.getElementById('toastMensaje');
        if (toastEl) {
            var toast = new bootstrap.Toast(toastEl);
            toast.show();
        }
    }
});

function showToast(message, type = 'success') {
    var toastEl = document.getElementById('toastMensaje');
    if (!toastEl) return;
    var toastBody = toastEl.querySelector('.toast-body');
    toastBody.textContent = message;

    toastEl.classList.remove(
        'bg-success',
        'bg-danger',
        'bg-warning',
        'bg-info',
        'bg-black'
    );

    const typeClassMap = {
        success: 'bg-success',
        danger: 'bg-danger',
        warning: 'bg-warning',
        info: 'bg-info',
        black: 'bg-black'
    };

    toastEl.classList.add(typeClassMap[type] ?? 'bg-success');

    const toast = bootstrap.Toast.getOrCreateInstance(toastEl);
    toast.show();
}

/*---------------------------------------Cambiar Estado----------------------------------------*/
async function postJson(url, data, {
    onSuccess = null,
    onError = null,
    successType = 'success',
    errorType = 'danger'
} = {}) {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken':
                    document.querySelector('input[name="__RequestVerificationToken"]')?.value
            },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        if (!response.ok || result.success === false) {
            const message = result?.message || "Error en la operación.";
            showToast(message, errorType);
            onError?.(result);
            return;
        }

        showToast(result.message, successType);
        onSuccess?.(result);

    } catch (err) {
        showToast("Error inesperado al comunicarse con el servidor.", errorType);
        onError?.(err);
    }
}

/*--------------------------------------Campos Numericos---------------------------------------*/
(function () {
    function allowNumericInput(input) {
        input.addEventListener('keydown', e => {
            const allowed = /[0-9.,]/.test(e.key) || ['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab', 'Enter'].includes(e.key);

            if (!allowed) {
                e.preventDefault();
            }

            if ((e.key === '.' || e.key === ',') && /[.,]/.test(input.value)) {
                e.preventDefault();
            }
        });

        input.addEventListener('input', () => {
            const raw = input.value.replace(',', '.');
            const number = Number(raw);

            if (!isNaN(number)) {
                input.dataset.raw = number;
            }
        });

        input.addEventListener('blur', () => formatNumeric(input));
    }

    function formatNumeric(input) {
        if (!input.value) return;

        const raw = input.value.replace(',', '.');

        const number = Number(raw);

        if (isNaN(number)) {
            input.value = '';
            input.dataset.raw = '';
            return;
        }

        input.dataset.raw = number.toFixed(2);

        input.value = number.toLocaleString('es-AR', {minimumFractionDigits: 2, maximumFractionDigits: 2});
    }

    function initNumericInputs(root = document) {
        root.querySelectorAll('input.js-numeric').forEach(input => {
            if (!input.dataset.numericInitialized) {
                input.dataset.numericInitialized = 'true';
                allowNumericInput(input);
            }
        });
    }

    // DOM listo
    document.addEventListener('DOMContentLoaded', () => {
        initNumericInputs();
    });

    // expongo función por si hay AJAX / partial reload
    window.initNumericInputs = initNumericInputs;
})();

function initIntegerInputs(root = document) {
    root.querySelectorAll('input.js-integer').forEach(input => {
        if (input.dataset.integerInitialized) return;

        input.dataset.integerInitialized = 'true';

        input.addEventListener('keydown', e => {
            if (
                !/[0-9]/.test(e.key) &&
                !['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(e.key)
            ) {
                e.preventDefault();
            }
        });

        input.addEventListener('blur', () => {
            if (!input.value) input.value = '1';
        });
    });
}

/*---------------------------------------Vista Preparar----------------------------------------*/
document.addEventListener("DOMContentLoaded", () => {

    const selects = document.querySelectorAll(".js-imputar-select");

    selects.forEach(select => {

        const targetId = select.dataset.target;
        const input = document.getElementById(targetId);

        if (!input) return;

        const toggleInput = () => {
            if (select.value) {
                input.disabled = false;
            } else {
                input.disabled = true;
                input.value = "";
            }
        };

        // Estado inicial
        toggleInput();

        // Evento change
        select.addEventListener("change", toggleInput);
    });
});


$(document).on('submit', '#procesarForm', function (e) {
    e.preventDefault();

    const $form = $(this);

    $.ajax({
        url: $form.attr('action'),
        type: 'POST',
        data: $form.serialize(),
        success: function (response) {
            if (response.ok) {
                window.location.href = response.redirectUrl;
            } else {
                showToast(response.message, 'danger');
            }
        },
        error: function () {
            showToast('Error inesperado al procesar.', 'danger');
        }
    });
});

/*-------------------------------------Buscador en Selects-------------------------------------*/
$(function () {
    if ($.fn.select2) {
        $('.select2').select2({
            placeholder: '(Seleccione)',
            width: '100%'
        });
    }
});