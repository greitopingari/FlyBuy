
var loadFile = function (event) {
	var image = document.getElementById('output');
	image.src = URL.createObjectURL(event.target.files[0]);
	image.style.border = "1px solid #ff523b";
};