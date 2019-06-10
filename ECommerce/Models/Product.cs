using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Models
{
    public enum Genders
    {
        Men, Woman, Kids
    }

    public enum Categorys
    {
        Boats, Boots, Brogues, Derby, Flip_Flops, Oxford, Sandals, Sneakers, Loafers, Monk, Ballerina, Mules, Pumps, Espadrilles, Crib, PreWalker
    }

    public enum Colors
    {
        Black, Blue, Brown, Gold, Green, Grey, Metalic, Multicolour, Neutrals, Orange, Pink, Purple, Red, Silver, White, Yellow
    }

    public enum Materials
    {
        leather, Rubber, Suede, Polyester, Canvas, Cotton, Neoprene, Polyurethane, Sequin, Skin, Crystal, Silk, PVC, Jute
    }

    public class Compositions
    {
        public Materials Outer { get; set; }
        public int OuterPercentage { get; set; }

        public Materials Lining { get; set; }
        public int LiningPercentage { get; set; }

        public Materials Sole { get; set; }
        public int SolePercentage { get; set; }
    }

    public class Product
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Brand { get; set; }

        public string BrandBackstory { get; set; }
        public Genders Gender { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Image { get; set; }

        [Required]
        public string ImageDetails { get; set; }

        [Required]
        public string ImageDetails2 { get; set; }

        public Categorys Category { get; set; }

        public Colors Color { get; set; }

        public Compositions Composition { get; set; }
    }


}