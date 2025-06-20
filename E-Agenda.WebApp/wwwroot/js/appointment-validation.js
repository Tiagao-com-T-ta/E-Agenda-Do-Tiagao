$(function () {
    function toggleRequiredFields() {
        var type = $("input[name='Type']:checked").val();
        if (type === "Presencial") {
            $("#Location").attr("required", "required");
            $("#Link").removeAttr("required");
        } else if (type === "Online") {
            $("#Link").attr("required", "required");
            $("#Location").removeAttr("required");
        } else {
            $("#Location").removeAttr("required");
            $("#Link").removeAttr("required");
        }
    }

    $("input[name='Type']").change(toggleRequiredFields);
    toggleRequiredFields(); 
});