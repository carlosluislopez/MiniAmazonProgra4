using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class SalesController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public SalesController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        public  ActionResult Index()
        {
            var datosAView = _repository.Query<Sale>(x=> x.Id == x.Id);
            var sale = datosAView.Project().To<SalesInputModel>();

            ViewBag.Title = "Sales";
            return View(sale);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Sale";

            return View(new SalesInputModel());
        }

        [HttpPost]
        public ActionResult Create(SalesInputModel salesInputModel)
        {
            if (ModelState.IsValid)
            {
                var sale = _mappingEngine.Map<SalesInputModel, Sale>(salesInputModel);
                sale.CreateDateTime = System.DateTime.Now;

                var account = _repository.GetById<Account>(salesInputModel.IdAccount);
                var category = _repository.GetById<Category>(salesInputModel.IdCategory);

                sale.Account = account;
                sale.Category = category;

                _repository.Create(sale);

                Success("Sale Created");
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Create Sale";
            Attention("Data Invalid");
            return View(salesInputModel);
        }

        public ActionResult Edit(int id)
        {
            var sale = _repository.First<Sale>(x => x.Id == id);
            if (sale == null)
            {
                return RedirectToAction("Index");
            }
            var salesInputModel = _mappingEngine.Map<Sale, SalesInputModel>(sale);

            ViewBag.Title = "Edit Sale";
            return View(salesInputModel);
        }

        [HttpPost]
        public ActionResult Edit(SalesInputModel salesInputModel)
        {
            if (ModelState.IsValid)
            {
                var sale = _mappingEngine.Map<SalesInputModel, Sale>(salesInputModel);

                var account = _repository.GetById<Account>(salesInputModel.IdAccount);
                var category = _repository.GetById<Category>(salesInputModel.IdCategory);

                sale.Account = account;
                sale.Category = category;

                _repository.Update(sale);
                Success("Registro editado");

                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit Sale";
            return View();
        }

    }
}