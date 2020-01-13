﻿using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
    {
    public class GigsController : Controller
        {

        private readonly ApplicationDbContext _context;

        public GigsController()
            {
            _context = new ApplicationDbContext();
            }

        protected override void Dispose(bool disposing)
            {
            if (disposing)
                _context.Dispose();
            }

        // GET: Gigs/create
        [Authorize]
        public ActionResult Create()
            {
            var viewModel = new GigFormViewModel
                {
                Genres = _context.Genres.ToList()
                };

            return View(viewModel);
            }

        [Authorize, HttpPost]
        public ActionResult Create(GigFormViewModel viewModel)
            {
            var gig = new Gig
                {
                ArtistId = User.Identity.GetUserId(),
                DateTime = Convert.ToDateTime($"{viewModel.Date} {viewModel.Time}"),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
                };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
            }

        }
    }