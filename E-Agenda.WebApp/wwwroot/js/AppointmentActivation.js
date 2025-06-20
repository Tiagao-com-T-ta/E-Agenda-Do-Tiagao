function updateFields() {
    const Presencial = document.getElementById('Presencial').checked;
    const locationDiv = document.getElementById('locationField');
    const linkDiv = document.getElementById('linkField');

    if (Presencial) {
        locationDiv.classList.remove('d-none');
        linkDiv.classList.add('d-none');
    } else {
        locationDiv.classList.add('d-none');
        linkDiv.classList.remove('d-none');
    }
}


window.onload = updateFields;
