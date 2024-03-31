
document.getElementById('loginBtn').addEventListener('click', function () {
    // Xử lý logic khi nút đăng nhập được nhấn
    // Ví dụ: Thêm hoặc loại bỏ các lớp CSS
    var navbar = document.querySelector('a.navbar-brand');
    navbar.classList.toggle('loggedIn');
});

var ctx = document.getElementById('myChart').getContext('2d');
// Dữ liệu cho biểu đồ
var data = {
    labels: ["Peter", "Andrew", "Julie", "Mary", "Dave"],
    datasets: [{
        label: 'Employee',
        data: [2, 6, 4, 5, 3],
        backgroundColor: 'steelblue'
    }]
};
// Tạo biểu đồ cột
var myChart = new Chart(ctx, {
    type: 'bar',
    data: data
});