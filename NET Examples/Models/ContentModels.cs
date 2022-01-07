using Models.Backend;
using Models.ModelAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Schooldesk.Models
{

    [TableName("Pages")]
    public class ContentPage : IDatabaseModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        [ColumnName("FriendlyName")]
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public string PageName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsDefault { get; set; } = false;

    }
    public class ContentPageHtmlComponents
    {
        public long PageId { get; set; }
        public string HtmlComponentsJson { get; set; }
    }

    public class DefaultComponent
    {
        public long Id { get; set; }
        public string FriendlyName { get; set; }
        public string CategoryName { get; set; }
        public string ComponentJson { get; set; }
        public string ImagePath { get; set; }
    }
}