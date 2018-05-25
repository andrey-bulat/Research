using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Staff.Model.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
