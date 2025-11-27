// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Function to handle the show/hide password toggle logic
function setupPasswordToggle(inputId, toggleButtonId, toggleIconId) {
    // Get the elements using their IDs
    const passwordInput = document.getElementById(inputId);
    const toggleButton = document.getElementById(toggleButtonId);
    const toggleIcon = document.getElementById(toggleIconId);

    // Only set up the event listener if all elements exist (prevents errors on other pages)
    if (passwordInput && toggleButton && toggleIcon) {
        toggleButton.addEventListener('click', function (e) {
            // 1. Toggle the input type between 'password' and 'text'
            const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
            passwordInput.setAttribute('type', type);

            // 2. Toggle the icon class (bi-eye-slash <-> bi-eye)
            if (type === 'text') {
                toggleIcon.classList.remove('bi-eye-slash');
                toggleIcon.classList.add('bi-eye'); // Show the open eye icon
            } else {
                toggleIcon.classList.remove('bi-eye');
                toggleIcon.classList.add('bi-eye-slash'); // Show the closed eye icon
            }
        });
    }
}

// Initialization calls (Run this logic for both password fields)
// This must be called AFTER the elements have been rendered in the DOM.
// 1. Password field
setupPasswordToggle('passwordInput', 'togglePassword', 'toggleIcon');

// 2. Confirm Password field
setupPasswordToggle('confirmPasswordInput', 'toggleConfirmPassword', 'toggleConfirmIcon');