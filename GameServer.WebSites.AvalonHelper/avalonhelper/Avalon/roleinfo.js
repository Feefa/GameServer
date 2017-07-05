(function ($)
{
    function roleSelectClick()
    {
        $(".role-card").hide();
        $(".role-card." + this.className).show();
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
    }

    $(document).ready(documentReady);
}
)(jQuery);