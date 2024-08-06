using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class CateGoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage ="Yêu cầu nhập tên Danh mục ")] 
        public string Name { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Danh mục ")]
		public string Description { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
