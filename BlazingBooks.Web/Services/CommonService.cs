using BlazingBooks.Shared.Interfaces;

namespace BlazingBooks.Web.Services
{
    public class CommonService : ICommonService
    {
        public bool IsWeb => true;

        public bool IsMobile => false;
    }
}
