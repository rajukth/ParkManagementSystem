using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ParkManagementSystem.Web.Helpers;

[HtmlTargetElement("image-upload-for")]
    public class ImageUploadTagHelper : TagHelper
    {
        public ModelExpression For { get; set; }
        public string Src { get; set; } = "/admin/assets/img/no-image.png";
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var fieldName = For.Name;
            var inputId = fieldName.Replace(".", "_") + "_Input";
            var previewId = fieldName.Replace(".", "_") + "_Preview";

            output.TagName = "div";
            output.Attributes.Add("class", "image-upload-wrapper");
            
            var html = $@"
<div class='card h-100 w-100 position-relative p-2' id='{fieldName}_Card'>

    <!-- Hidden input -->
    <input type='file' id='{inputId}' name='{fieldName}' class='d-none' />

    <!-- Image Preview -->
    <img id='{previewId}'
         src='{Src}'
         class='img-thumbnail'
         style='width:100%; height:calc(100% - 60px); object-fit:contain; display:{(string.IsNullOrEmpty(Src) ? "none" : "block")};' />

    <!-- Bottom buttons -->
    <div class='d-flex gap-1 w-100 position-absolute bottom-0 start-0 p-2' id='{fieldName}_Buttons'>
        <button type='button' class='btn btn-danger flex-fill'
                onclick=""document.getElementById('{inputId}').value=''; 
                         document.getElementById('{previewId}').src='{Src}'; 
                         document.getElementById('{previewId}').style.display='{(string.IsNullOrEmpty(Src) ? "none" : "block")}';"">
            <span class='fa fa-times'></span>
        </button>

        <button type='button' class='btn btn-primary flex-fill'
                onclick=""document.getElementById('{inputId}').click()"">
            <span class='fa fa-upload'></span>
        </button>
    </div>
</div>

<script>
    document.getElementById('{inputId}').addEventListener('change', function (e) {{
        const file = e.target.files[0];
        const preview = document.getElementById('{previewId}');
        if (file) {{
            preview.src = URL.createObjectURL(file);
            preview.style.display = 'block';
        }}

        const event = new CustomEvent('imageUploadChanged', {{
            detail: {{
                inputId: '{inputId}',
                previewId: '{previewId}',
                fileName: file ? file.name : null
            }}
        }});
        document.dispatchEvent(event);
    }});
</script>
";


            

            output.Content.SetHtmlContent(html);
        }
    }