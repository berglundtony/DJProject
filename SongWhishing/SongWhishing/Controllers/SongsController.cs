using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SongWishing.Controllers
{
    public class SongsController : Controller
    {
        private WishingSongEntities db = new WishingSongEntities();



        // GET: Songs
        //string sortOrder, string currentFilter, string searchName, int? page
        public ActionResult Index(string sortOrder, string currentFilter, string searchName, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ArtistSortParm = string.IsNullOrEmpty(sortOrder) ? "artist_desc" : "";
            ViewBag.SongSortParm = sortOrder == "Låt" ? "song_desc" : "Låt";
            ViewBag.RealeastYearSortParm = sortOrder == "Utgivningsår" ? "releasedyear_desc" : "Utgivningsår";

            if (searchName != null)
            {
                page = 1;
            }
            else
            {
                searchName = currentFilter;
            }

            ViewBag.CurrentFilter = searchName;

            var songs = from s in db.Songs
                        select s;


            if (!string.IsNullOrEmpty(searchName))
            {
                songs = songs.Where(s => s.Artist.Contains(searchName) || s.Låtnamn.Contains(searchName));
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            //Sorting

            IPagedList<Song> songsToReturn = null;


            switch (sortOrder)
            {
                case "artist_desc":
                    songsToReturn = songs.OrderByDescending(s => s.Artist).ToPagedList(pageNumber, pageSize);
                    break;
                case "Låt":
                    songsToReturn = songs.OrderBy(s => s.Låtnamn).ToPagedList(pageNumber, pageSize);
                    break;
                case "song_desc":
                    songsToReturn = songs.OrderByDescending(s => s.Låtnamn).ToPagedList(pageNumber, pageSize);
                    break;
                case "Utgivningsår":
                    songsToReturn = songs.OrderBy(s => s.Utgivningsår).ToPagedList(pageNumber, pageSize);
                    break;
                case "releasedyear_desc":
                    songsToReturn = songs.OrderByDescending(s => s.Utgivningsår).ToPagedList(pageNumber, pageSize);
                    break;
                default:
                    songsToReturn = songs.OrderBy(s => s.IDSong).ToPagedList(pageNumber, pageSize);
                    break;
            }

            return View(songsToReturn);
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDSong,Artist,Låtnamn,BPM,Genre_1,Genre_2,Skivnamn,Utgivningsår,Importdatum,Omit")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDSong,Artist,Låtnamn,BPM,Genre_1,Genre_2,Skivnamn,Utgivningsår,Importdatum,Omit")] Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult SearchForArtistOrSong(string searchName)
        //{
        //    var artist = 

        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
