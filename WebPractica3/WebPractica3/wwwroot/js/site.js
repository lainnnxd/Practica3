document.addEventListener('DOMContentLoaded', (event) => {
    function cargarSaldo() {
        const select = document.getElementById("Id_Compra");
        const saldo = select.options[select.selectedIndex].dataset.saldo;
        document.getElementById("SaldoAnterior").value = saldo || '';
    }

    const selectElement = document.getElementById("Id_Compra");
    selectElement.addEventListener('change', cargarSaldo);

    (function () {
        'use strict';
        const forms = document.querySelectorAll('.needs-validation');
        Array.from(forms).forEach(function (form) {
            form.addEventListener('submit', function (event) {
                const montoInput = document.getElementById("Monto");
                const saldoAnterior = parseFloat(document.getElementById("SaldoAnterior").value) || 0;
                const monto = parseFloat(montoInput.value) || 0;

                if (monto > saldoAnterior) {
                    event.preventDefault();
                    event.stopPropagation();
                    document.getElementById("MontoInvalidFeedback").style.display = 'block';
                    montoInput.classList.add('is-invalid');
                } else {
                    document.getElementById("MontoInvalidFeedback").style.display = 'none';
                    montoInput.classList.remove('is-invalid');
                }

                if (!form.checkValidity()) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    })();
});

