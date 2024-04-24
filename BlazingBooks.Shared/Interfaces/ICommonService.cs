using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBooks.Shared.Interfaces
{
    public interface ICommonService
    {
        bool IsWeb { get; }
        bool IsMobile { get; }
    }
}
