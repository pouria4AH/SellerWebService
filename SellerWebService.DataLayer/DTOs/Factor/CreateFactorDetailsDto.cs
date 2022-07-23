namespace SellerWebService.DataLayer.DTOs.Factor
{
    public class CreateFactorDetailsDto
    {

        [Display(Name = "نام")]
        [MaxLength(80, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Name { get; set; }

        [Display(Name = "توضحیات")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        [Display(Name = "نوع شمارش و دسته بندی")]
        [MaxLength(60, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Packaging { get; set; }

        [Display(Name = "تعداد")]
        [Range(0, long.MaxValue,ErrorMessage="این مقدار نمی تواند زیر صفر باشد")]
        public long Count { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Range(0, long.MaxValue,ErrorMessage = "این مقدار نمی تواند زیر صفر باشد")]
        public int Discount { get; set; } = 0;

        [Range(0, long.MaxValue, ErrorMessage = "این مقدار نمی تواند زیر صفر باشد")]
        [Display(Name = "قیمت")]
        public long Price { get; set; }
    }
}
