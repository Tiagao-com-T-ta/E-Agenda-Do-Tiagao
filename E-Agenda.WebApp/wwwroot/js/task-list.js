function toggleDropdown(button) {
    const menu = button.nextElementSibling;
    const isVisible = menu.style.display === "block";

    document.querySelectorAll(".dropdown-menu").forEach(m => m.style.display = "none");
    menu.style.display = isVisible ? "none" : "block";

    if (!isVisible) {
        document.addEventListener("click", function handler(e) {
            if (!button.parentElement.contains(e.target)) {
                menu.style.display = "none";
                document.removeEventListener("click", handler);
            }
        });
    }
}
