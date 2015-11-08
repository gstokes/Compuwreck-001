using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compuwreck_001.Models;

namespace Compuwreck_001.DAL
{
    public interface IPageRepository: IDisposable 
    {
        IEnumerable<Page> GetAllPages();
        IEnumerable<Page> GetTop5Pages();
        Page GetPageById(int id);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(int id);
        void Save();
    }
}