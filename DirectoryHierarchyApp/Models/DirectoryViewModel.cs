using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectoryHierarchyApp.Models
{
    public class DirectoryViewModel
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        [Display(Name = "Directory path")]
        public string DirectoryPath { get; set; }

        [Display(Name = "Directory name")]
        public string DirectoryName { get; set; }

        public List<DirectoryPreviewModel> Subdirectories { get; set; }
    }
}