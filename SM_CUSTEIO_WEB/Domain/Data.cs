using SM_CUSTEIO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SM_CUSTEIO_WEB.Domain
{
    public class Data
    {
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();
            
            menu.Add(new Navbar { Id = 1, nameOption = "Empresa", controller = "Empresa", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 2, nameOption = "Produto", controller = "Produto", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            menu.Add(new Navbar { Id = 3, nameOption = "Material", controller = "Material", action = "Index", imageClass = "fa fa-dashboard fa-fw", status = true, isParent = false, parentId = 0 });
            
            return menu.ToList();
        }
    }
}