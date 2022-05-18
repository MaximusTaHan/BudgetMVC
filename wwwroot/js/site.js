
$("#openCategoryModalBtn").on("click", function () {
    $("#addCategoryModal").modal("show");
});

$("#openTransactionModalBtn").on("click", function () {
    $("#addTransactionModal").modal("show");
});

$("#manageCategoriesBtn").on("click", function () {
    $("#categories").removeClass("d-none");
    $("#backToTransactionsBtn").removeClass("d-none");
    $("#records").addClass("d-none");
    $("#openTransactionModalBtn").addClass("d-none");
    $("#openCategoryModalBtn").removeClass("d-none");
    $("#manageCategoriesBtn").addClass("d-none");
    $("#filter-area").addClass("d-none");
});

$("#backToTransactionsBtn").on("click", function () {
    $("#categories").addClass("d-none");
    $("#backToTransactionsBtn").addClass("d-none");
    $("#records").removeClass("d-none");
    $("#openTransactionModalBtn").removeClass("d-none");
    $("#openCategoryModalBtn").addClass("d-none");
    $("#manageCategoriesBtn").removeClass("d-none");
    $("#filter-area").removeClass("d-none");
});