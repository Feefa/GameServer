(function ($)
{
    function roleSelectClick()
    {
        $(".role-card").hide();
        $(".role-card." + this.className).show();
        $(".role-card-button-container input[name='roleId']").val(this.className.substring(4));
    }

    function cancelButtonClick()
    {
        $(".role-card").hide();
    }

    function documentReady()
    {
        $(".role-card").hide();
        $(".role-select a").click(roleSelectClick)
        $(".role-card-button-container button[name='cancel-role-select']").click(cancelButtonClick);
        var roleId = $(".role-card-button-container input[name='roleId']").val();

        if (roleId > 0)
        {
            $(".role-card.role" + roleId).show();
        }
    }

    $(document).ready(documentReady);
}
)(jQuery);