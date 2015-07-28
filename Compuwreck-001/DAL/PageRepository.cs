using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL
{
    public class PageRepository : IPageRepository{
        private readonly CompuwreckEntities _db;

        public PageRepository(CompuwreckEntities db){
            _db = db;
        }

        public IEnumerable<Page> GetAllPages(){
            var allPages = _db.Pages.Where(p => p.Published == true);
            return allPages;
        }

        public Page GetPageById(int id){
            var page = _db.Pages.Find(id);
            return page;
        }

        public void InsertPage(Page page){
            _db.Pages.Add(page);
        }

        public void UpdatePage(Page page){
            _db.Entry(page).State = EntityState.Modified;
        }

        public void DeletePage(int id) {
            Page page = _db.Pages.Find(id);
        }

        public void Save(){
            _db.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose(){
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}