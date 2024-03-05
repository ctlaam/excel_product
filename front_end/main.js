document.addEventListener("DOMContentLoaded", function () {
    initFunction();
});

function initFunction() {
    const productCode = document.getElementById("codeproduct");
    const productName = document.getElementById("nameproduct");
    const timeProduct = document.getElementById("datetime");
    const description = document.getElementById("description");
    document.getElementById("form-product").addEventListener("submit", function (event) {
        event.preventDefault();
        !productCode.value ? productCode.classList.add("is-invalid") : ""
        !productName.value ? productName.classList.add("is-invalid") : ""
        !timeProduct.value ? timeProduct.classList.add("is-invalid") : ""
        if (!productCode.value.trim() || !productName.value.trim() || !timeProduct.value) {
            alert("Vui lòng nhập đầy đủ thông tin");
            return;
        }

        let formData = {
            productCode: productCode.value,
            productName: productName.value,
            timeProduct: timeProduct.value,
            description: description.value
        }
        fetch("http://localhost:5241/api/Products/insert", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    alert("Thêm sản phẩm thành công");
                    resetForm();
                }
            })
            .catch(error => {
                alert("Có lỗi xảy ra, vui lòng thử lại sau");
            });
    });
    function resetForm() {
        productCode.value = "";
        productName.value = "";
        timeProduct.value = "";
        description.value = "";
        productCode.classList.remove("is-invalid");
        productName.classList.remove("is-invalid");
        timeProduct.classList.remove("is-invalid");
    }
}
