using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập tên Sản Phẩm ")]
		public string Name { get; set; }
		
        public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả Sản Phẩm ")]
		public string Description { get; set; }
		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập giá Sản Phẩm ")]
		public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel CateGory { get; set; }
        public BrandModel Brand { get; set; }

        public string Image { get; set; }
    }
}
