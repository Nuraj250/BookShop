document.addEventListener("DOMContentLoaded", function () {
    let container = document.getElementById("container");

    // Log container to verify it exists
    console.log(container);

    // Toggle between sign-in and sign-up
    window.toggle = () => {
        container.classList.toggle("sign-in");
        container.classList.toggle("sign-up");
    };

    // Set initial state to "sign-in" after a short delay
    setTimeout(() => {
        container.classList.add("sign-in");
    }, 200);
});
