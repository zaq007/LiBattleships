using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LiBattleship.Query
{
    public interface IQuery<T>
    {
        IQueryable<T> AsQueriable();

        IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
