﻿using Casestudy.DAL;
using Casestudy.DAL.DomainClasses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Casestudy.DAL.DAO
{

    public class BranchDAO
    {
        private AppDbContext _db;
        public BranchDAO(AppDbContext context)
        {
            _db = context;
        }

        public async Task<List<Branch>?> GetThreeClosestBranches(float? lat, float? lon)
        {
            List<Branch>? storeDetails = null;
            try
            {
                var latParam = new SqlParameter("@lat", lat);
                var lonParam = new SqlParameter("@lon", lon);
                var query = _db.Branches?.FromSqlRaw("dbo.pGetThreeClosestBranches @lat, @lon", latParam,
                lonParam);
                storeDetails = await query!.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return storeDetails;
        }

    }
}
