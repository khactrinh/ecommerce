// using Ecommerce.Application.Common.Models;
//
// namespace Ecommerce.Application.Common.Extensions;
//
// using System;
// using System.Linq;
//
// public static class PagedResultExtensions
// {
//     public static PagedResult<TDestination> Map<TSource, TDestination>(
//         this PagedResult<TSource> source,
//         Func<TSource, TDestination> mapFunc)
//     {
//         return new PagedResult<TDestination>
//         {
//             Items = source.Items.Select(mapFunc).ToList(),
//             Page = source.Page,
//             PageSize = source.PageSize,
//             Total = source.Total
//         };
//     }
// }