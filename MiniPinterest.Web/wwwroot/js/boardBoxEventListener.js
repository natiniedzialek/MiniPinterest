document.querySelectorAll('.board-box').forEach(function (boardBox) {
    boardBox.addEventListener('click', function () {
        var url = this.getAttribute('data-url');
        window.location.href = url;
    });
});