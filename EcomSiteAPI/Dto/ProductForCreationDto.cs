﻿namespace EcomAPI.Dto
{
    public class ProductForCreationDto
    {
        public string SKU { get; set; }
        public string UPC { get; set; }
        public int ColorID { get; set; }
        public int SizeID { get; set; }
        public int BrandID { get; set; }
        public int PackSize { get; set; }
        public string Description { get; set; }
    }
}
