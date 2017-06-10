function checkValid() {

    var tenngnhan = document.getElementById('tenngnhan');
    var sdt = document.getElementById('sodt');
    var diachi = document.getElementById("diaChi");

    var messtenng = document.getElementById('messtenng');
    var messsdt = document.getElementById('messsdt');
    var messdiachi = document.getElementById('messdiachi');

    var buttonsubmit = document.getElementById('btnXacNhan');

    var goodColor = "#66cc66";
    var badColor = "#ff6666";
    
    var tenngnhanTrim = $.trim(tenngnhan.value);
    

    if ($.trim(tenngnhan.value).length == 0) {
        tenngnhan.style.backgroundColor = badColor;
        messtenng.style.color = badColor;
        messtenng.innerHTML = "Họ tên người nhận không được trống!"
        $('#btnXacNhan').prop('disabled', true);
    }
    else {
        tenngnhan.style.backgroundColor = goodColor;
        messtenng.style.color = goodColor;
        messtenng.innerHTML = "OK!";
        $('#btnXacNhan').prop('disabled', false);
    }


    if (sdt == null || sdt.value.length < 10) {
        sdt.style.backgroundColor = badColor;
        messsdt.style.color = badColor;
        messsdt.innerHTML = "Số điện thoại không được trống hoặc không đủ"
        $('#btnXacNhan').prop('disabled', true);
    }
    else {

        if (isNaN(messsdt.value)) {
            sdt.style.backgroundColor = goodColor;
            messsdt.style.color = goodColor;
            messsdt.innerHTML = "OK!";
            $('#btnXacNhan').prop('disabled', false);
        }
        else {
            sdt.style.backgroundColor = badColor;
            messsdt.style.color = badColor;
            messsdt.innerHTML = "Số điện thoại không đúng định dạng"
            $('#btnXacNhan').prop('disabled', true);
        }

        
    }


    if (diachi == null || diachi.value.length == 0) {
        diachi.style.backgroundColor = badColor;
        messdiachi.style.color = badColor;
        messdiachi.innerHTML = "Địa chỉ không được trống!"
        $('#btnXacNhan').prop('disabled', true);
    }
    else {
        diachi.style.backgroundColor = goodColor;
        messdiachi.style.color = goodColor;
        messdiachi.innerHTML = "OK!";
        $('#btnXacNhan').prop('disabled', false);
    }

    
}