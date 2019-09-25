namespace DirectoryHierarchyApp.Models
{
    using System.Data.Entity;
    using System.Linq;

    public partial class DirectoriesDataModel : DbContext
    {
        public DirectoriesDataModel()
            : base("name=DirectoriesDataModel")
        {
            InitDirectories();
        }

        private void InitDirectories()
        {
            if (!this.Directories.Any())
            {
                Directory root = new Directory()
                {
                    Name = "root",
                    Path = ""
                };
                this.Directories.Add(root);
                root.ParentDirectories.Add(root);
                this.SaveChanges();
            }
        }

        public virtual DbSet<Directory> Directories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Directory>()
                .HasMany(e => e.ParentDirectories)
                .WithMany(e => e.ChildDirectories)
                .Map(m => m.ToTable("Relations").MapLeftKey("ChildId").MapRightKey("ParentId"));
        }
    }
}
