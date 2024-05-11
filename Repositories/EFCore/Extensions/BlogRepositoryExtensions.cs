using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
{
    public static class BlogRepositoryExtensions
    {
        public static IQueryable<Blog> Search(this IQueryable<Blog> blogs,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return blogs;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return blogs
                .Where(b => b.Title
                .ToLower()
                .Contains(searchTerm));
        }

        public static IQueryable<Blog> Sort(this IQueryable<Blog> blogs,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return blogs.OrderBy(b => b.Id);

            // Global metod Type verilerek cagrilir
            var orderQuery = OrderQueryBuilder
                .CreateOrderQuery<Blog>(orderByQueryString);

            if (orderQuery is null)
                return blogs.OrderBy(b => b.Id);

            return blogs.OrderBy(orderQuery);

        }
    }
}
