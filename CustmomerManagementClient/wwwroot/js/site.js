document.addEventListener('htmx:configRequest', function (event) {
    var tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenElement) {
        event.detail.headers['RequestVerificationToken'] = tokenElement.value;
    }
});

document.addEventListener('htmx:afterSwap', function (event) {
    if (event.detail.target.id === 'modalContainer') {
        var modalElement = document.getElementById('confirmationModal');
        if (modalElement) {
            var confirmationModal = new bootstrap.Modal(modalElement);
            confirmationModal.show();
        }
    }

    applyBootstrapValidation();
});

document.addEventListener('htmx:beforeRequest', function () {
    console.log('Showing spinner...');
    showSpinner();
});

document.addEventListener('htmx:afterRequest', function (event) {
    hideSpinner();

    if (event.detail.successful) {
        var modalElement = event.detail.elt.closest('#confirmationModal');
        if (modalElement) {
            closeModal(); 
        }
    }
});

function showSpinner() {
    var spinnerOverlay = document.getElementById('spinnerOverlay');
    if (spinnerOverlay) {
        spinnerOverlay.classList.remove('d-none');
        console.log('Spinner shown.');
    } else {
        console.log('Spinner element not found!');
    }
}

function hideSpinner() {
    var spinnerOverlay = document.getElementById('spinnerOverlay');
    if (spinnerOverlay) {
        spinnerOverlay.classList.add('d-none');
        console.log('Spinner hidden.');
    } else {
        console.log('Spinner element not found!');
    }
}

function closeModal() {
    var modalElement = document.getElementById('confirmationModal');
    if (modalElement) {
        var modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
            console.log('Modal closed.');
        }
        hideSpinner();
    }
}

function applyBootstrapValidation() {
    var forms = document.querySelectorAll('.needs-validation');

    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity()) {
                htmx.trigger(form, 'submit');
            }

            form.classList.add('was-validated');
        }, false);
    });
}

document.addEventListener('DOMContentLoaded', function () {
    applyBootstrapValidation();
});
