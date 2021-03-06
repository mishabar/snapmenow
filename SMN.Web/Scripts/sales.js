﻿function updatePrice(id, loggedIn, interval) {
    $.get("/Sales/Price/" + id, {}, function (response) {
        if ($("div.sale-details").data("status") != response.status) {
            document.location.reload();
        } else {
            $("#snapsCount").html((response.snaps == 1 ? "1 item was" : response.snaps +" items were") + " snapped!");
            if (loggedIn){
                $(".btn-snapme").html("Snap Me Now");
            }
        }
        setTimeout(updatePrice, interval, id, loggedIn, interval);
        $(response.price.match(/\d+/)).each(function (i, v) { currentPrice[i] = parseInt(v) });
    });
}

function updatePrices(interval) {
    $.get("/Sales/Prices/Active", {}, function (response) {
        var ids = [];
        for (var i = 0; i < response.length; i++) {
            $("btn-snap[data-id=" + response[i].id + "]").html("Enter Sale (" + response[i].price + ")");
            ids.push(response[i].id);
        }
        $(".active-sale").each(function (idx, item) {
            var index = ids.indexOf($(item).data("id"));
            if (index == -1) {
                $(item).remove();
            } else { ids.splice(index, 1); }
        });
        if (ids.length > 0) { document.location.reload(); }
        setTimeout(updatePrices, interval, interval);
    });
}

$(".btn-fbshare").on("click", function () {
    FB.ui({
        method: 'share',
        href: $(this).data("href"),
    }, function (response) { });
});