$(function () {
  initPoints();

  function initPoints() {
    $.ajax({
      method: 'GET',
      url: '/api/path/points',
      dataType: 'json',
      success: function (items) {
        console.log(items);
      }
    });
  }
});