window.onload = function () {
    var fileupload = document.getElementById("FL");
    var filePath = document.getElementById("spnFilePath");
    var button = document.getElementById("btnSubmit");
    var buttonUpload = document.getElementById("btnUpload");
    button.onclick = function () {
        fileupload.click();
    };
    fileupload.onchange = function () {
        //var fileName = fileupload.value.split('\\')[fileupload.value.split('\\').length - 1];
        if (fileupload.files.length == 0) {
            buttonUpload.disabled = true;
        }
        else {
            buttonUpload.disabled = false;
        }
        var fileName = $(this).val().split("\\").pop();
        filePath.innerHTML = "<b>Selected File: </b>" + fileName;
        var x = fileValidation();
        if (x) {
            buttonUpload.disabled = false;
        }
        else {
            buttonUpload.disabled = true;
        }
    };
};

function fileValidation() {
    let header;
    var fileInput = document.getElementById('FL');
    var filePath = fileInput.value;
    var fileExtension = filePath.split(".").pop();
    var fileAllowedExtension = document.getElementById('FileEx').value;
    var fileAllowedSize = document.getElementById('FileSize').value;
    var fileTypeError = document.getElementById('FileNotAllowedMess').value;
    var FileSizeError = document.getElementById('FileSizeMessage').value;
    // var x=true;

    // Allowing file type
    

    if (!fileAllowedExtension.includes(fileExtension)) {
        header = document.querySelector("#spnFilePath").innerText = "";
        header = document.querySelector("#spnFileName");
        header.innerText = fileTypeError;
        // x = false;
        fileInput.value = '';
        return false;
    }
    else {
        header = document.querySelector("#spnFileName").innerText = "";
    }
    if (FL.files.length > 0) {
        for (const i = 0; i <= FL.files.length - 1; i++) {
            const size = (fileAllowedSize * 1024 * 1024)
            const fsize = FL.files.item(i).size;
            
            const file = Math.round((fsize / 1024));

            if (fsize >= size) {
                header = document.querySelector("#spnFilePath").innerText = "";
                header = document.querySelector("#spnFileName").innerText = FileSizeError;
                //     x = false;
                fileInput.value = '';
                return false;
            } else {
                header = document.querySelector("#spnFileName");
                header.innerText = "";
            }
        } fileInput.value = '';
    }
    return true;
}