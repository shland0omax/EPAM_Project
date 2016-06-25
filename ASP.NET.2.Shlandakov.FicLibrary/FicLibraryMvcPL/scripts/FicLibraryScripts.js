
function RenewPage() {
    if (response.Success) {
        window.location.reload();
    }
}

function HideCreateButton() {
    var button = document.getElementById("show-comment-form");
    button.style.display = 'none';
}

function CommentActionSuccess(response) {
    if (response.Success) {
        window.location.reload();
    }
}