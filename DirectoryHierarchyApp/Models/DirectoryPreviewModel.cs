using System.ComponentModel.DataAnnotations;

namespace DirectoryHierarchyApp.Models
{
    public class DirectoryPreviewModel
    {
        [Display(AutoGenerateField = false)]
        public int Id { get; set; }

        [Display(Name = "Directory path")]
        public string DirectoryPath { get; set; }

        [Display(Name = "Directory name")]
        public string DirectoryName { get; set; }
    }
}