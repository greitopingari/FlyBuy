
var loadFile = function (event) {
	var image = document.getElementById('output');
	image.style.display = "block";
	image.src = URL.createObjectURL(event.target.files[0]);
	image.style.border = "1px solid #ff523b";
	
};

function test() {
    var form = $("#form_id").valid();
    var file = $("#file").val();

    if (form && file != '') {
        alertify.success('Product Added');
        setTimeout(function () {
            document.getElementById("form_id").submit()
            return false;
        }, 700);
    }
    else {
        alertify.warning('Warning! Product not added!');

    }
}    