using SongWishing.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SongWishing.Controllers
{
    public class WishesController : Controller
    {
        private WishingSongEntities db = new WishingSongEntities();
        private readonly TextRepository _repository;

        public WishesController()
        {
            _repository = new TextRepository();
        }


        // GET: Requests
        public ActionResult Index(string message)
        {
            return View(db.Requests.ToList());
        }


        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Wishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Wishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Artist,Låt,Avsändare")] Request request, Song song)
        {
            if (ModelState.IsValid)
            {
                //Here we store the usersession and save it to the database by the request
                Session["_UserSession"] = System.Web.HttpContext.Current.Session.SessionID;
                request.SessionID = Session["_UserSession"].ToString();
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
        }

        public JsonResult getAjaxResult(string term)
        {

            //string searchResult = string.Empty;

            var _songList = new List<SearchSongViewModel>();

            if (!string.IsNullOrEmpty(term))
            {
                var songs = (from s in db.Songs
                             where s.Artist.StartsWith(term) || s.Låtnamn.StartsWith(term)
                             orderby s.Artist
                             //orderby s.Låtnamn
                             select new { s.Artist, s.Låtnamn }).Take(30).Distinct().ToList();
                //select new { s.Artist, s.Låtnamn }).Distinct().Take(5).ToList();

                foreach (var s in songs)
                {
                    var songobjects = new SearchSongViewModel
                    {
                        Artist = s.Artist,
                        Song = s.Låtnamn
                    };
                    _songList.Add(songobjects);
                }
            }
            return Json(_songList,
              JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArtists(string term)
        {
            var _artistList = new List<SearchArtistViewModel>();


            if (!string.IsNullOrEmpty(term))
            {
                var artists = (from s in db.Songs
                               where s.Artist.StartsWith(term)
                               orderby s.Artist
                               select new { s.Artist }).Take(5).Distinct().ToList();



                foreach (var s in artists)
                {
                    var songobjects = new SearchArtistViewModel
                    {
                        Artist = s.Artist,
                    };
                    _artistList.Add(songobjects);
                }
            }
            return Json(_artistList,
              JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSongs(string artist, string term)
        {
            var _songList = new List<SearchArtistViewModel>();
            var artistName = artist;

            if (!string.IsNullOrEmpty(term))
            {
                var songs = (from s in db.Songs
                             where s.Låtnamn.StartsWith(term)
                             orderby s.Låtnamn
                             select new { s.Låtnamn }).Take(5).Distinct().ToList();



                foreach (var s in songs)
                {
                    var songobjects = new SearchArtistViewModel
                    {
                        Song = s.Låtnamn,
                    };
                    _songList.Add(songobjects);
                }
            }
            return Json(_songList,
              JsonRequestBehavior.AllowGet);
        }

        // GET: Wishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Wishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Artist,Låt,Avsändare")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Wishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request wish = db.Requests.Find(id);
            if (wish == null)
            {
                return HttpNotFound();
            }
            return View(wish);
        }

        // POST: Wishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request wish = db.Requests.Find(id);
            db.Requests.Remove(wish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Here we handle the message the DJ want to send to the customer
        /// that have had wished a special song he or she wants to hear.
        /// The request id store which request the message will belong to.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The request that belongs to the IDRequest.</returns>
        public ActionResult Find(string term)
        {
            MessageViewModel[] texts = _repository.FindTexts(term);
            var projection = from text in texts
                             select new
                             {
                                 id = text.Id,
                                 label = text.DisplayName,
                                 value = text.DisplayName
                             };
            return Json(projection.ToList(),
                JsonRequestBehavior.AllowGet);

        }
        public ActionResult MessageToCustomer(int? id)
        {
            Request request = db.Requests.Find(id);

            /*
             Here we get the customer or wishers SessionID
             and store it in the model.
            */

            Session["_UserSession"] = System.Web.HttpContext.Current.Session.SessionID;
            string userSession = Session["_UserSession"] as string;


            var model = new MessageViewModel
            {
                Request = request,
                SessionID = userSession,
                IDRequest = id,
                Avsändare = request.Avsändare
            };


            /* 
            All the values that the models requires are stored in TempData["MessageToCustomer"]
            so we can use it in the neaxt ActionResult Message.
            */

            TempData["MessageToCustomer"] = model;
            return View(model);

        }


        [HttpPost]
        public ActionResult Message(string message, string sessionid)
        {
            MessageViewModel requestdata = TempData["MessageToCustomer"] as MessageViewModel;

            if (!string.IsNullOrEmpty(sessionid) && requestdata != null)
            {
                int? id = requestdata.IDRequest;
                Request request = db.Requests.Find(id);
                bool noPlayBoolProp = false;
                var hasSuccess = (TempData["NotPlay"] != null) == true;

                if (hasSuccess)
                {
                    string boolValue = TempData["NotPlay"].ToString();

                    if (boolValue == "true")
                    {
                        noPlayBoolProp = true;
                    }
                    noPlayBoolProp = true;
                }

                var model = new MessageViewModel
                {
                    Message = message,
                    SessionID = sessionid,
                    IDRequest = id,
                    Avsändare = requestdata.Avsändare,
                    Låt = request.Låt,
                    Artist = request.Artist,
                    WillNotPlayBoolProperty = noPlayBoolProp
                };


                model.allRequests = db.Requests.ToList();
                return View(model);
            }
            else
            {
                var model = new MessageViewModel { };
                model.allRequests = db.Requests.ToList();
                return View(model);
            }
        }


        public string Play()
        {
            TempData["NotPlay"] = "false";
            return "Jag kommer att spela den låten ikväll !";
        }

        public string NotPlay()
        {
            TempData["NotPlay"] = "true";
            return "Jag har inte den låten i mitt skivarkiv och kommer tyvärr INTE att spela låten ikväll.";
        }

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
