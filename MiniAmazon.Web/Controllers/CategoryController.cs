using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class CategoryController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public CategoryController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        //
        // GET: /Category/
        public ActionResult Index()
        {
            var datosAView = _repository.Query<Category>(x => x.Status == true);
            var category = datosAView.Project().To<CategoryInputModel>();

            ViewBag.Title = "Categorys";
            return View(category);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Category";
            return View(new CategoryInputModel());
        }

        [HttpPost]
        public ActionResult Create(CategoryInputModel categoryInputModel)
        {
            if(ModelState.IsValid)
            {
                var category = _mappingEngine.Map<CategoryInputModel, Category>(categoryInputModel);
                category.Status = true;
                _repository.Create(category);
                Success("Registro creado");

                return RedirectToAction("Index");
            }
            ViewBag.Title = "Create Category";
            return View(categoryInputModel);
        }

        public ActionResult Edit(int id)
        {
            var category = _repository.First<Category>(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var categoryInputModel = _mappingEngine.Map<Category, CategoryInputModel>(category);
            ViewBag.Title = "Edit Category";
            return View(categoryInputModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryInputModel categoryInputModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mappingEngine.Map<CategoryInputModel, Category>(categoryInputModel);
                category.Status = true;
                _repository.Update(category);
                Success("Registro editado");
                
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit Category";
            return View(categoryInputModel);
        }

        public ActionResult Delete(int id)
        {
            var category = _repository.First<Category>(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var categoryInputModel = _mappingEngine.Map<Category, CategoryInputModel>(category);
            ViewBag.Title = "Delete Category";
            return View(categoryInputModel);
        }

        [HttpPost]
        public ActionResult Delete(CategoryInputModel categoryInputModel)
        {
            if (ModelState.IsValid)
            {
                var category = _mappingEngine.Map<CategoryInputModel, Category>(categoryInputModel);
                category.Status = false;
                _repository.Update(category);
                Success("Registro eliminado");

                return RedirectToAction("Index");
            }
            ViewBag.Title = "Delete Category";
            return View(categoryInputModel);
        }

        public  ActionResult Details(int id)
        {
            var category = _repository.First<Category>(x => x.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            var categoryInputModel = _mappingEngine.Map<Category, CategoryInputModel>(category);
            ViewBag.Title = "Details Category";
            return View(categoryInputModel);
        }
    }
}
