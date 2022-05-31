
$("#openCategoryModalBtn").on("click", function () {
    $("#addCategoryModal").modal("show");
});

$("#openTransactionModalBtn").on("click", function () {
    $("#addTransactionModal").modal("show");
});

$("#addCategoryBtn").click(function () {
    var name = $("#InsertCategory_CategoryName").val();
    var modalSuccess = $("#addCategoryModalSucess");
    var modalForm = $("#addCategoryForm")

    $.ajax({
        url: 'Categories/Create',
        type: 'POST',
        data: {
            name: name
        },
        dataType: 'json',
        success: function (response) {
            Console.log(response);
            modalSuccess.removeClass("d-none");
            modalForm.addClass("d-none");
        }
    });
});

$(".openDeleteCategoryModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    $('#deleteCategoryForm').append(`<input type="hidden" name="id" value="${id}">`)
    $("#deleteCategoryModal").modal("show");
});

$(".openDeleteTransactionModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    $('#deleteTransactionForm').append(`<input type="hidden" name="id" value="${id}">`);
    $("#deleteTransactionModal").modal("show");
});

$(".openUpdateTransactionModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:eq(0)').html();
    var name = $(this).closest('tr').find('td:eq(1)').html();
    var amount = $(this).closest('tr').find('td:eq(2)').html();
    var date = $(this).closest('tr').find('td:eq(3)').html();
    var title = $(this).closest('tr').find('td:eq(4)').html();

    $('#insert-transaction-form #InsertTransaction_TransactionID').val($.trim(id));
    $('#insert-transaction-form #InsertTransaction_TransactionName').val($.trim(name));
    $('#insert-transaction-form #InsertTransaction_Amount').val($.trim(amount));
    $('#insert-transaction-form #InsertTransaction_Date').val($.trim(date));
    $('#insert-transaction-form #InsertTransaction_CategoryID').val($.trim(title));

    $("#addTransactionModal").modal("show");
});

$(".openUpdateCategoryModalBtn").on("click", function () {
    var id = $(this).closest('tr').find('td:first').html();
    var name = $(this).closest('tr').find('td:eq(1)').html();

    $('#insert-category-form #InsertCategory_CategoryId').val($.trim(id));
    $('#insert-category-form #InsertCategory_CategoryName').val($.trim(name));

    $("#addCategoryModal").modal("show");
});

$("#manageCategoriesBtn").on("click", function () {
    $("#categories").removeClass("d-none");
    $("#manageTransactionsBtn").removeClass("d-none");
    $("#records").addClass("d-none");
    $("#openTransactionModalBtn").addClass("d-none");
    $("#openCategoryModalBtn").removeClass("d-none");
    $("#manageCategoriesBtn").addClass("d-none");
    $("#filter-area").addClass("d-none");
});

$("#manageTransactionsBtn").on("click", function () {
    $("#categories").addClass("d-none");
    $("#manageTransactionsBtn").addClass("d-none");
    $("#records").removeClass("d-none");
    $("#openTransactionModalBtn").removeClass("d-none");
    $("#openCategoryModalBtn").addClass("d-none");
    $("#manageCategoriesBtn").removeClass("d-none");
    $("#filter-area").removeClass("d-none");
});