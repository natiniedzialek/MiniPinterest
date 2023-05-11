let uploadElement = document.getElementById('imageUpload');
let imageDisplayElement = document.getElementById('imageDisplay');

async function uploadImage(e) {
    let data = new FormData();
    data.append('file', e.target.files[0]);

    await fetch('/api/images', {
        method: 'POST',
        headers: {
            'Accept': '*/*'
        },
        body: data
    }).then(response => response.json())
        .then(result => {
            // Set the image URL in the form data
            let formElement = document.querySelector('form');
            let imageUrlInput = document.createElement('input');
            imageUrlInput.type = 'hidden';
            imageUrlInput.name = 'ImageUrl';
            imageUrlInput.value = result.imageURL;
            formElement.appendChild(imageUrlInput);

            // Display the uploaded image
            imageDisplayElement.src = result.imageURL;
            imageDisplayElement.style.display = 'block';
        });
}

uploadElement.addEventListener('change', uploadImage);