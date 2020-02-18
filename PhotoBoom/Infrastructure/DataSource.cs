using PhotoBoom.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PhotoBoom.Infrastructure
{
    public sealed class DataSource
    {
        private static readonly Lazy<DataSource>
            lazy =
            new Lazy<DataSource>
                (() => new DataSource());

        public static DataSource Instance => lazy.Value;

        private DataSource()
        {
#if(DEBUG)
            //DUMMY MOCK DATA
            PhotoList = new ConcurrentBag<PhotoModel>();
            for (int i = 1; i <= 10; i++)
            {
                PhotoList.Add(new PhotoModel()
                {
                    Id = i.ToString(),
                    CraeteDate = DateTime.Now,
                    IsDeleted = false,
                    TagList = new List<string>() { "testTag1", "testTag2" },
                    Title = $"Test Photo {i}",
                    UserId = "618ffdc1-173b-430e-8dd7-199be5d0a583",
                });
            }  
#endif
        }

        public ConcurrentBag<PhotoModel> PhotoList { get; set; }
    }
}