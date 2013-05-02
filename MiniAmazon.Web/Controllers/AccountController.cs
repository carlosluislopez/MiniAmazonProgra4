using System;
using System.Web.Mvc;
using AutoMapper;
using MiniAmazon.Domain;
using MiniAmazon.Domain.Entities;
using MiniAmazon.Web.Models;

namespace MiniAmazon.Web.Controllers
{
    public class AccountController : BootstrapBaseController
    {
        private readonly IRepository _repository;
        private readonly IMappingEngine _mappingEngine;

        public AccountController(IRepository repository, IMappingEngine mappingEngine)
        {
            _repository = repository;
            _mappingEngine = mappingEngine;
        }

        public ActionResult SignIn()
        {
            ViewBag.Title = "SignIn";
            return View(new AccountSignInModel());
        }

        [HttpPost]
        public ActionResult SignIn(AccountSignInModel accountSignInModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Title = "SignIn";
                Error("Datos invalidos");
                return RedirectToAction("SignIn");
            }
            var account =
                _repository.First<Account>(
                    x => x.Email == accountSignInModel.Email && x.Password == accountSignInModel.Password);

            if (account!=null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            Attention("User and/or Pass incorrect");
            return View(accountSignInModel);
        }


        public ActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create User";
            return View(new AccountInputModel());
        }

        [HttpPost]
        public ActionResult Create(AccountInputModel accountInputModel)
        {
            if(ModelState.IsValid)
            {
                var account = _mappingEngine.Map<AccountInputModel, Account>(accountInputModel);
                
                _repository.Create(account);


                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Title = "Create User";
            return View(accountInputModel);
        }

        public ActionResult Resset(int id)
        {
            var account = _repository.First<Category>(x => x.Id == id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }
            var categoryInputModel = _mappingEngine.Map<Category, CategoryInputModel>(account);
            //Probando GITs
            ViewBag.Title = "Edit Category2";
            return View(categoryInputModel);
            return View(new AccountResetInModel());
        }

        [HttpPost]
        public ActionResult Resset(AccountResetInModel accountResetInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(accountResetInModel.Password != accountResetInModel.Password2)
                    {
                        //CODIGO
                    }
                    var account = _mappingEngine.Map<AccountResetInModel, Account>(accountResetInModel);
                    var accountOld = _repository.GetById<Account>(accountResetInModel.Id);

                    account.Name = accountOld.Name;
                    account.Genre = accountOld.Genre;
                    account.Age = accountOld.Age;

                    _repository.Update(account);
                    Success("Password Reset");

                    return RedirectToAction("Index", "Dashboard");

                }
                Attention("Data Invalid");
            }catch(Exception ex)
            {
                Error(ex.Message);
            }
            ViewBag.Title = "Reset Password";
            return View(accountResetInModel);
        }
    }
}