using SM_CUSTEIO_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SM_CUSTEIO_WEB.Repository
{
    public class BaseRepository
    {

        private DataModel _DataModel;

        public DataModel DataModel
        {
            get 
            
            {
                if (_DataModel == null)
                    _DataModel = new DataModel();
                    return _DataModel;
                 
            }


            set { _DataModel = value; }
        }


    }
}