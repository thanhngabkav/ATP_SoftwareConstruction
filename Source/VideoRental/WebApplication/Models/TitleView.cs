using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TitleView
    {
        public TitleView(int titleID, string titleName, string tag, string image, int quantity)
        {
            this.titleID = titleID;
            this.titleName = titleName;
            this.tag = tag;
            this.image = image;
            this.quantity = quantity;
        }

        [Display(Name = "ID")]
        public int titleID { set; get; }
        [Display(Name = "Title")]
        public string titleName { set; get; }
        [Display(Name = "Tag")]
        public string tag { set; get; }
        public string image { set; get; }
        [Display(Name = "Quantity")]
        public int quantity { set; get; }
        public bool IsChosen = false;
    }
}