using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yazley_watch_boutique;

namespace Yazley_watch_boutique.Controllers
{
    public class productController : Controller
    {
        private yazleyEntities db = new yazleyEntities();

        // GET: product
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult FeedbackList()
        {
            var list = db.Feedbacks.ToList();
            return View(list);
        }
        public ActionResult OrderList()
        {
            var list = db.Orders.ToList();
            return View(list);
        }

        public ActionResult AddBrand()
        {
            return View();

        }

        public ActionResult createGuide()
        {
            return View();

        }

        public ActionResult CreateBoutique()
        {
            return View();

        }


        public ActionResult CreateCalibre()
        {
            return View();

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBrand(brand brand)
        {
            if (ModelState.IsValid)
            {
                db.brands.Add(brand);
                await db.SaveChangesAsync();
                return RedirectToAction("ListBrand");
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBoutique(boutique boutique)
        {
            if (ModelState.IsValid)
            {
                db.boutiques.Add(boutique);
                await db.SaveChangesAsync();
                return RedirectToAction("ListBoutique");
            }
            return View(boutique);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> createGuide(thewatchguide thewatchguide)
        {
            if (ModelState.IsValid)
            {
                db.thewatchguides.Add(thewatchguide);
                await db.SaveChangesAsync();
                return RedirectToAction("ListGuide");
            }
            return View(thewatchguide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBoutique(boutique boutique)
        {
            if (ModelState.IsValid)
            {
                db.boutiques.Add(boutique);
                await db.SaveChangesAsync();
                return RedirectToAction("ListBoutique");
            }
            return View(boutique);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCalibre(calibre calibre)
        {
            if (ModelState.IsValid)
            {
                db.calibres.Add(calibre);
                await db.SaveChangesAsync();
                return RedirectToAction("ListCalibre");
            }
            return View(calibre);
        }

        public ActionResult AddProduct(product model)
        {
            db.products.Add(model);
            db.SaveChanges();
            ViewBag.Message = "New product record inserted successfully...";
            return View();
        }

        public ActionResult display()
        {
            var listofdata = db.products.ToList();
            return View(listofdata);
        }

        public ActionResult ListBrand()
        {
            var listofdata = db.brands.ToList();
            return View(listofdata);
        }

        public ActionResult ListBoutique()
        {
            var listofdata = db.boutiques.ToList();
            return View(listofdata);
        }

        public ActionResult ListCalibre()
        {
            var listofdata = db.calibres.ToList();
            return View(listofdata);
        }

        public ActionResult ListGuide()
        {
            var listofdata = db.thewatchguides.ToList();
            return View(listofdata);
        }


        // GET: brand/Details/5
        public async Task<ActionResult> DetailsBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brand brand = await db.brands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        public async Task<ActionResult> DetailsGuide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thewatchguide thewatchguide = await db.thewatchguides.FindAsync(id);
            if (thewatchguide == null)
            {
                return HttpNotFound();
            }
            return View(thewatchguide);
        }


        // GET: brand/Details/5
        public async Task<ActionResult> DetailsCalibre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calibre calibre = await db.calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View(calibre);
        }

        // GET: product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public async Task<ActionResult> EditBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brand brand = await db.brands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBrand(brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListBoutique");
            }
            return View(brand);
        }


        public async Task<ActionResult> EditBoutique(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            boutique boutique = await db.boutiques.FindAsync(id);
            if (boutique == null)
            {
                return HttpNotFound();
            }
            return View(boutique);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBoutique(boutique boutique)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boutique).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListBoutique");
            }
            return View(boutique);
        }

        public async Task<ActionResult> EditGuide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thewatchguide ListGuide = await db.thewatchguides.FindAsync(id);
            if (ListGuide == null)
            {
                return HttpNotFound();
            }
            return View(ListGuide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGuide(thewatchguide thewatchguide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thewatchguide).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListGuide");
            }
            return View(thewatchguide);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCalibre(calibre calibre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calibre).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("ListCalibre");
            }
            return View(calibre);
        }

        public async Task<ActionResult> EditCalibre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calibre calibre = await db.calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View();
        }


        // GET: product/Create
        public ActionResult Create()
        {
            ViewBag.brand = new SelectList(db.brands, "brandName", "brandName");
            ViewBag.calibreId = new SelectList(db.calibres, "calibreId", "brand");
            return View();
        }


        // POST: product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "productId,model_no,productName,productImage,collection,brand,price,axis,feature1,feature2,feature3,feature4,calibreId,case_shape,case_material,case_size,lug_width,glass_material,luminocity,dial_color,indexes,strap_material,strap_color,clasp_type,EAN,gender,water_resistence,country_origin,warrenty_period,color,material,category,size,description")] product product)
        {

            if (ModelState.IsValid)
            {
                db.products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.brand = new SelectList(db.brands, "brandName", "brandImage", product.brand);
            ViewBag.calibreId = new SelectList(db.calibres, "calibreId", "brand", product.calibreId);


            return View(product);
        }

      

        // GET: product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand = new SelectList(db.brands, "brandName", "brandImage", product.brand);
            ViewBag.calibreId = new SelectList(db.calibres, "calibreId", "brand", product.calibreId);
            return View(product);
        }

        // POST: product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "productId,model_no,productName,productImage,collection,brand,price,axis,feature1,feature2,feature3,feature4,calibreId,case_shape,case_material,case_size,lug_width,glass_material,luminocity,dial_color,indexes,strap_material,strap_color,clasp_type,EAN,gender,water_resistence,country_origin,warrenty_period,color,material,category,size,description")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.brand = new SelectList(db.brands, "brandName", "brandImage", product.brand);
            ViewBag.calibreId = new SelectList(db.calibres, "calibreId", "brand", product.calibreId);
            return View(product);
        }

        // GET: product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = await db.products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public async Task<ActionResult> DeleteBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            brand brand = await db.brands.FindAsync(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
       
        [HttpPost, ActionName("DeleteBrand")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBrandConfirmed(int id)
        {
            brand brand = await db.brands.FindAsync(id);
            db.brands.Remove(brand);
            await db.SaveChangesAsync();
            return RedirectToAction("ListBrand");
        }


        public async Task<ActionResult> DeleteGuide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            thewatchguide thewatchguide = await db.thewatchguides.FindAsync(id);
            if (thewatchguide == null)
            {
                return HttpNotFound();
            }
            return View(thewatchguide);
        }

        public async Task<ActionResult> DeleteCalibre(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            calibre calibre = await db.calibres.FindAsync(id);
            if (calibre == null)
            {
                return HttpNotFound();
            }
            return View(calibre);
        }

        // POST: product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            product product = await db.products.FindAsync(id);
            db.products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCalibreConfirmed(int id)
        {
            calibre calibre = await db.calibres.FindAsync(id);
            db.calibres.Remove(calibre);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteGuideConfirmed(int id)
        {
            thewatchguide thewatchguide = await db.thewatchguides.FindAsync(id);
         
            db.thewatchguides.Remove(thewatchguide);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult ListInquiry()
        {
            var inquiries = db.Inquiries.Include(i => i.product1);
            return View(inquiries.ToList());
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
