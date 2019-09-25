using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DirectoryHierarchyApp.Models;

namespace DirectoryHierarchyApp.Controllers
{
    public class DirectoryController : Controller
    {
        private DirectoriesDataModel db = new DirectoriesDataModel();

        // GET: Directory
        public ActionResult Index(string id)
        {
            Directory dir = null;
            if (string.IsNullOrEmpty(id))
            {
                dir = db.Directories.First();
            }
            else
            {
                id = id.TrimEnd('/');
                dir = db.Directories.Where(dr => (dr.Path + dr.Name) == id).First();
            }
            if (dir != null)
            {
                Directory parent = dir.ParentDirectories.First();
                DirectoryViewModel view = new DirectoryViewModel()
                {
                    Id = dir.Id,
                    ParentId = parent.Id,
                    DirectoryName = dir.Name,
                    DirectoryPath = dir.Path
                };
                List<DirectoryPreviewModel> subs = new List<DirectoryPreviewModel>();

                foreach (var sub in dir.ChildDirectories)
                {
                    if (sub.Id != dir.Id)
                    {
                        subs.Add(
                            new DirectoryPreviewModel()
                            {
                                Id = sub.Id,
                                DirectoryPath = sub.Path,
                                DirectoryName = sub.Name
                            }
                            );
                    }
                }
                view.Subdirectories = subs;
                return View(view);
            }
            else
            {
                return View();
            }
        }

        // GET: Directory/Create
        public ActionResult Create(int? id)
        {
            Directory dir = null;
            if (id == null)
            {
                dir = db.Directories.First();
            }
            else
            {
                dir = db.Directories.Where(dr => dr.Id == id).First();
            }
            return View(dir);
        }

        // POST: Directory/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                string dirName = form["directoryName"];
                string dirPath = form["directoryPath"];
                int dirParentId = int.Parse(form["directoryParent"]);

                Directory newDir = new Directory()
                {
                    Name = dirName,
                    Path = dirPath
                };

                db.Directories.Add(newDir);
                db.SaveChanges();
                var parent = db.Directories.Where(pr => pr.Id == dirParentId).First();
                newDir.ParentDirectories.Add(parent);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Directory/Edit/5
        public ActionResult Rename(int? id)
        {
            Directory dir = null;
            try
            {
                dir = db.Directories.Where(dr => dr.Id == id).First();
                return View(dir);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Directory/Edit/5
        [HttpPost]
        public ActionResult Rename(FormCollection form)
        {
            try
            {
                string dirName = form["directoryName"];
                int dirId = int.Parse(form["directoryId"]);

                Directory dir = db.Directories.Where(dr => dr.Id == dirId).First();

                var childs = db.Directories.Where(child => child.Path.StartsWith(dir.Path + dir.Name));

                foreach(var child in childs)
                {
                    child.Path = child.Path.Replace(dir.Name, dirName);
                }

                dir.Name = dirName;


                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Directory/Delete/5
        public ActionResult Delete(int? id)
        {
            Directory dir = null;
            try
            {
                dir = db.Directories.Where(dr => dr.Id == id).First();
                string parent = dir.Path;
                dir.ParentDirectories.Clear();
                
                var childs = db.Directories.Where(child => child.Path.StartsWith(dir.Path + dir.Name));
                foreach(var child in childs)
                {
                    child.ParentDirectories.Clear();
                }
                db.Directories.RemoveRange(childs);

                db.Directories.Remove(dir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
