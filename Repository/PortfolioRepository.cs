using System.Data;
using Dapper;
using trnvid.Interfaces;
using trnvid.Models;

namespace trnvid.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly IDbConnection _db;
        public PortfolioRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            var sql = "sp_CreatePortfolio";
            var parameters = new
            {
                p_AppUserId = portfolio.AppUserId,
                p_StockId = portfolio.StockId
            };

            // Since Portfolios table only has keys, we execute the insert
            // and return the original model, as there's nothing new to fetch from the DB.
            var affectedRows = await _db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            
            // We assume success if 1 row was affected.
            return affectedRows > 0 ? portfolio : null;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var sql = "sp_DeletePortfolio";
            var parameters = new 
            {
                p_AppUserId = appUser.Id,
                p_StockSymbol = symbol
            };

            // The stored procedure will handle the deletion. 
            // We execute it and assume success if rows were affected.
            // Note: Returning the deleted model is tricky here. The controller might need a small adjustment
            // to just expect an OK status instead of the deleted object.
            var affectedRows = await _db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            // This part is a simplification. A more complex SP would be needed to return the deleted model.
            if (affectedRows > 0)
            {
                // Returning a dummy object to satisfy the interface.
                // In a real scenario, you might change the method to return bool.
                return new Portfolio { AppUserId = appUser.Id }; 
            }
            return null;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            var sql = "sp_GetUserPortfolio";
            var parameters = new { p_AppUserId = user.Id };

            var portfolio = await _db.QueryAsync<Stock>(sql, parameters, commandType: CommandType.StoredProcedure);
            return portfolio.ToList();
        }
    }
}