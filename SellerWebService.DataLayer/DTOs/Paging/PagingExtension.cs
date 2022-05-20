﻿namespace SellerWebService.DataLayer.DTOs.Paging
{
    public static class PagingExtension
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, BasePaging paging)
        {
            return  query.Skip(paging.SkipEntities).Take(paging.TakeEntities);
        }
    }
}
