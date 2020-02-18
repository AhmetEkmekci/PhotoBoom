using PhotoBoom.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoBoom.Models
{
    public class PhotoModel
    {
        public PhotoModel()
        {
            Id = Guid.NewGuid().ToString();
            CraeteDate = DateTime.Now;
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public List<string> TagList
        {
            get
            {
                return Tags?.StringToList();
            }
            set
            {
                Tags = value?.ListToString();
            }
        }
        public DateTime CraeteDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}