using BlazingBooks.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBooks.Mobile.Services
{
    public class CommonService : ICommonService
    {
        public bool IsWeb => false;

        public bool IsMobile => true;
    }
}
