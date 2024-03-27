using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingBooks.Shared.Dtos
{
    public record PageResult<TRecords>(TRecords[] Records, int TotalCount)
    {
    }
}
