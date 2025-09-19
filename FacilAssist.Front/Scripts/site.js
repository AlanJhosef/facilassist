function showToast(message, type, duration) {
    const toastContainer = $('.toast-container');
    const isSuccess = type === 'success';

    const toastHtml = `
            <div class="toast align-items-center ${isSuccess ? 'toast-success' : 'toast-error'}" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="d-flex">
                    <div class="toast-body">
                        ${message}
                    </div>
                    <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
            </div>
        `;

    const toastElement = $(toastHtml);
    toastContainer.append(toastElement);

    const bsToast = new bootstrap.Toast(toastElement[0], {
        autohide: true,
        delay: duration
    });
    bsToast.show();
}