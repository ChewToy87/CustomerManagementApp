// wwwroot/js/site.js

document.addEventListener('htmx:configRequest', function (event) {
    var tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenElement) {
        event.detail.headers['RequestVerificationToken'] = tokenElement.value;
    }
});

document.addEventListener('htmx:afterSwap', function (event) {
    // Initialize any Bootstrap components if needed
    if (event.detail.target.id === 'modalContainer') {
        var modalElement = document.getElementById('confirmationModal');
        if (modalElement) {
            var confirmationModal = new bootstrap.Modal(modalElement);
            confirmationModal.show();
        }
    }

    // Apply validation styles
    applyBootstrapValidation();
});

function closeModal() {
    var modal = document.getElementById('confirmationModal');
    var modalInstance = bootstrap.Modal.getInstance(modal);
    modalInstance.hide();
}

function applyBootstrapValidation() {
    var forms = document.querySelectorAll('.needs-validation');

    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                // Prevent form submission
                event.preventDefault();
                event.stopPropagation();

                if (form.checkValidity()) {
                    // Manually trigger htmx submission
                    htmx.trigger(form, 'submit');
                }

                form.classList.add('was-validated');
            }, false);
        });
}

// Initialize validation on page load
document.addEventListener('DOMContentLoaded', function () {
    applyBootstrapValidation();
});
