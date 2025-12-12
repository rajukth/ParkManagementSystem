using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ParkManagementSystem.Web.Helpers
{
    public static class ImagePreviewHtmlHelper
    {
        public static IHtmlContent ImagePreviewFor<TModel>(
            this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, IFormFile>> expression,
            string imageUrl = "",
            string removeFieldName = "RemoveImage") // hidden field for backend
        {
            var fieldName = htmlHelper.NameFor(expression).ToString();

            string inputId = $"file_{Guid.NewGuid():N}";
            string previewId = $"preview_{Guid.NewGuid():N}";
            string removeBtnId = $"remove_{Guid.NewGuid():N}";
            string hiddenRemoveId = $"hidden_{Guid.NewGuid():N}";

            var html = $@"
<div class='image-upload-wrapper'>

    <!-- Hidden file input -->
    <input type='file' 
           id='{inputId}' 
           name='{fieldName}'
           class='d-none' />

    <!-- Button to open file dialog -->
    <button type='button' class='btn btn-primary btn-sm'
            onclick=""document.getElementById('{inputId}').click();"">
        Upload Image
    </button>

    <!-- Remove button -->
    <button type='button'
            id='{removeBtnId}'
            class='btn btn-danger btn-sm ms-2'
            style='display:{(string.IsNullOrEmpty(imageUrl) ? "none" : "inline-block")}'
            onclick=""removeImage_{inputId}();"">
        Remove
    </button>

    <!-- Image preview -->
    <div>
        <img id='{previewId}'
             src='{imageUrl}'
             style='width:150px;height:150px;object-fit:contain;margin-top:8px;display:{(string.IsNullOrEmpty(imageUrl) ? "none" : "block")}'
             class='img-thumbnail' />
    </div>

    <!-- Hidden field to notify backend if removed -->
    <input type='hidden' id='{hiddenRemoveId}' name='{removeFieldName}' value='false' />
</div>

<script>
document.getElementById('{inputId}').addEventListener('change', function (e) {{
    var file = e.target.files[0];
    var preview = document.getElementById('{previewId}');
    var removeBtn = document.getElementById('{removeBtnId}');
    var hiddenRemove = document.getElementById('{hiddenRemoveId}');

    if (file) {{
        var reader = new FileReader();
        reader.onload = function(evt) {{
            preview.src = evt.target.result;
            preview.style.display = 'block';
            removeBtn.style.display = 'inline-block';
            hiddenRemove.value = 'false'; // reset removal state
        }};
        reader.readAsDataURL(file);
    }}
}});

// REMOVE IMAGE FUNCTION
function removeImage_{inputId}() {{
    var preview = document.getElementById('{previewId}');
    var fileInput = document.getElementById('{inputId}');
    var removeBtn = document.getElementById('{removeBtnId}');
    var hiddenRemove = document.getElementById('{hiddenRemoveId}');

    preview.src = '';
    preview.style.display = 'none';
    removeBtn.style.display = 'none';
    fileInput.value = '';        // clears selected file
    hiddenRemove.value = 'true'; // tells backend to delete/reset
}}
</script>";

            return new HtmlString(html);
        }
    }
}
