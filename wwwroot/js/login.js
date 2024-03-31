document.getElementById('loginBtn').addEventListener('click', function () {
    // Xử lý logic khi nút đăng nhập được nhấn
    // Ví dụ: Thêm hoặc loại bỏ các lớp CSS
    var navbar = document.querySelector('a.navbar-brand');
    navbar.classList.toggle('loggedIn');
});