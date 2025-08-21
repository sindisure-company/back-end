using System.ComponentModel.DataAnnotations;

namespace ApiSindisure.Domain.ViewModel.FileUpload
{
    public class FileUploadViewModel
    {
        public class Request
        {        
            [Required]   
            public IFormFile File { get; set; }

            [Required]           
            public string? FileName { get; set; }

            [Required]        
            public string? FilePath { get; set; }
        }

        public class Download
        {           

            [Required]        
            public string? FilePath { get; set; }
        }

        public class Delete
        {     

            [Required]        
            public string? FilePath { get; set; }
        }

        public class Response
        {
            [Required]
            public string? FileResponse { get; set; }

            [Required]
            public string? FileName { get; set; }

            [Required]
            public string? FilePath { get; set; }

            [Required]
            public string? PublicUrl { get; set; }
        }        
    }
}

