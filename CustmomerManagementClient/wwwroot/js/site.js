// wwwroot/js/site.js

// Set up the request verification token for HTMX requests
document.addEventListener('htmx:configRequest', function (event) {
    var tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    if (tokenElement) {
        event.detail.headers['RequestVerificationToken'] = tokenElement.value;
    }
});

// Handle post-HTMX swap events to re-initialize Bootstrap components and validation
document.addEventListener('htmx:afterSwap', function (event) {
    // Initialize Bootstrap modal components if needed
    if (event.detail.target.id === 'modalContainer') {
        var modalElement = document.getElementById('confirmationModal');
        if (modalElement) {
            var confirmationModal = new bootstrap.Modal(modalElement);
            confirmationModal.show();
        }
    }

    // Re-apply validation styles
    applyBootstrapValidation();
});

// Show the loading spinner before the request is sent
document.addEventListener('htmx:beforeRequest', function () {
    console.log('Showing spinner...');
    showSpinner();
});

// Hide the loading spinner after the request is completed
document.addEventListener('htmx:afterRequest', function (event) {
    hideSpinner();

    if (event.detail.successful) {
        // Check if the request was triggered by the confirmation modal form
        var modalElement = event.detail.elt.closest('#confirmationModal');
        if (modalElement) {
            closeModal(); // Close the modal after a successful form submission
        }
    }
});

// Function to show the loading spinner
function showSpinner() {
    var spinnerOverlay = document.getElementById('spinnerOverlay');
    if (spinnerOverlay) {
        spinnerOverlay.classList.remove('d-none');
        console.log('Spinner shown.');
    } else {
        console.log('Spinner element not found!');
    }
}

// Function to hide the loading spinner
function hideSpinner() {
    var spinnerOverlay = document.getElementById('spinnerOverlay');
    if (spinnerOverlay) {
        spinnerOverlay.classList.add('d-none');
        console.log('Spinner hidden.');
    } else {
        console.log('Spinner element not found!');
    }
}

// Close the modal with a specific ID
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

// Apply Bootstrap validation styles to forms
function applyBootstrapValidation() {
    var forms = document.querySelectorAll('.needs-validation');

    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            // Prevent default submission
            event.preventDefault();
            event.stopPropagation();

            // If form is valid, trigger HTMX submission manually
            if (form.checkValidity()) {
                htmx.trigger(form, 'submit');
            }

            // Add validation styles
            form.classList.add('was-validated');
        }, false);
    });
}

// Initialize validation on page load
document.addEventListener('DOMContentLoaded', function () {
    applyBootstrapValidation();
});
