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
                account.Status = true;

                _repository.Create(account);

                Success("Account Created");
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Title = "Create User";
            Attention("Data Invalid");
            return View(accountInputModel);
        }

        public ActionResult Resset(int id)
        {
            var account = _repository.First<Account>(x => x.Id == id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }
            var accountResetInModel = _mappingEngine.Map<Account, AccountResetInModel>(account);
            ViewBag.Title = "Reset Password";
            return View(accountResetInModel);
        }

        [HttpPost]
        public ActionResult Resset(AccountResetInModel accountResetInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var accountOld = _repository.GetById<Account>(accountResetInModel.Id);
                    if(accountOld.Password != accountResetInModel.Password)
                    {
                        Attention("Password incorrect");
                        return View(accountResetInModel);
                    }
                    if(accountResetInModel.PasswordNew != accountResetInModel.Password2)
                    {
                        Attention("New Password not match with Confirm Password");
                        return View(accountResetInModel);
                    }
                    var account = _mappingEngine.Map<AccountResetInModel, Account>(accountResetInModel);

                    account.Name = accountOld.Name;
                    account.Genre = accountOld.Genre;
                    account.Age = accountOld.Age;
                    account.Password = accountResetInModel.PasswordNew;

                    _repository.Clear();
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


        public ActionResult Edit(int id)
        {
            var account = _repository.First<Account>(x => x.Id == id);
            if (account == null)
            {
                return RedirectToAction("Index");
            }
            var accountEditModel = _mappingEngine.Map<Account, AccountEditModel>(account);
            ViewBag.Title = "Edit Account";
            return View(accountEditModel);
        }

        [HttpPost]
        public ActionResult Edit(AccountEditModel accountEditModel)
        {
            if (ModelState.IsValid)
            {
                var account = _mappingEngine.Map<AccountEditModel, Account>(accountEditModel);
                var accountOld = _repository.GetById<Account>(accountEditModel.Id);

                account.Password = accountOld.Password;
                account.Status = true;

                _repository.Clear();
                _repository.Update(account);

                Success("Account Edited");
                return RedirectToAction("Index", "Dashboard");
            }
            ViewBag.Title = "Edit Account";
            Attention("Data Invalid");
            return View(accountEditModel);
        }

        public ActionResult VerifyPasswordMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}