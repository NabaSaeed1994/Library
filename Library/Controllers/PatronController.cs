using System.Collections.Generic;
using System.Linq;

using Library.Models.Patron;

using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    public class PatronController : Controller
    {
        private  IPatron _patron;

        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();

            var patronModels = allPatrons
                .Select(p => new PatronDetailModel
                {
                    Id = p.Id,
                    LastName = p.LastName ?? "No First Name Provided",
                    FirstName = p.FirstName ?? "No Last Name Provided",
                    LibraryCardId = p.LibraryCard.Id,
                    OverdueFees = p.LibraryCard.Fees,
                    HomeLibraryBranch = p.HomeLibraryBranch.Name
                }).ToList();

            var model = new PatronIndexModel
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var patron = _patron.Get(id);

            var model = new PatronDetailModel
            {
                Id = patron.Id,
                LastName = patron.LastName ?? "No Last Name Provided",
                FirstName = patron.FirstName ?? "No First Name Provided",
                Address = patron.Address ?? "No Address Provided",
                HomeLibraryBranch = patron.HomeLibraryBranch?.Name ?? "No Home Library",
                MemberSince = patron.LibraryCard.Created,
                OverdueFees = patron.LibraryCard.Fees,
                LibraryCardId = patron.LibraryCard.Id,
                Telephone=patron.TelephoneNumber,
                //Telephone = string.IsNullOrEmpty(patron.TelephoneNumber) ? "No Telephone Number Provided" : patron.TelephoneNumber,
                AssetsCheckedOut = _patron.GetCheckouts(id).ToList() ?? new List<Checkout>(),
                CheckoutHistory = _patron.GetCheckoutHistory(id),
                Holds = _patron.GetHolds(id)
            };

            return View(model);
        }
    }
}