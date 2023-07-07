var page = 0,
    inCallback = false,
    maxPageCount=0

var scrollHandler = function () {
    //if (maxPageCount >= page && $(document).scrollTop() == $(document).height() - $(window).height()){
    if (maxPageCount > page && $(window).scrollTop() + $(window).height() > $(".infinite-scroll").height()) {
        loadPersonData(url);
    }
}

function loadPersonData(url) {
    if (page > -1 && !inCallback) {
        inCallback = true;
        page++;
        //$("div#loading").show();

        $.ajax({
            type: 'GET',
            url: url+"?pageNum="+page,
            //data: "pageNum=" + page,
            success: function (data, textstatus) {
                if (data != '') {
                    $("table.infinite-scroll > tbody").append(data);
                    $("table.infinite-scroll > tbody > tr:even").addClass("alt-row-class");
                    $("table.infinite-scroll > tbody > tr:odd").removeClass("alt-row-class");
                }
                else {
                    page = -1;
                }

                inCallback = false;
                //$("div#loading").hide();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        });
    }
}