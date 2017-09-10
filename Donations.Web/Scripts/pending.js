$(function () {
    load();

    $("table").on('click', '.btnApprove', function () {
        $.post("/admin/approve", { id: $(this).data("application-id") }, function () {
            load();
        });
    });

    function load() {
        $("table tr:gt(0)").remove();
        $.get("/admin/allPending", function (result) {
            result.forEach(a => $("table").append(`<tr>
            <td>${a.date}</td>
            <td>${a.userName}</td>
            <td>${a.userEmail}</td>
            <td>${a.amount}</td>
            <td>${a.categoryName}</td>
            <td>${a.description}</td>
            <td><button data-application-id="${a.id}" class="btn btn-success btnApprove">Approve</button></td>
        </tr>`))
        });
    };
});