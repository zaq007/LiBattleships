using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LiBattleship.Query
{
    public class BaseQuery<T> : IQuery<T>
    {
        protected IQueryable<T> _inner;

        public IQueryable<T> AsQueriable()
        {
            return _inner;
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            _inner = _inner.Where(expression);
            return _inner;
        }
    }
}
