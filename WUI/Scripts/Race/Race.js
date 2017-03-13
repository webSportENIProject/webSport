$(function () {
    $('#search-ac')
        .autocomplete({
            source: function (request, response) {
                if (request.term.length >= 3) {
                    $.ajax({
                        url: "/Race/Autocomplete",
                        type: "GET",
                        dataType: "json",
                        data: { term: request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.Title, value: item.Title, id: item.Id };
                            }))
                        }
                    });
                }
            }
        });
});