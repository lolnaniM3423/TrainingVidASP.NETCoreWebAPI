using System.Data;
using Dapper;
using trnvid.Dtos.Stock;
using trnvid.Helpers;
using trnvid.Interfaces;
using trnvid.Models;

namespace trnvid.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly IDbConnection _db;
        public StockRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            var sql = "sp_CreateStock";
            var parameters = new
            {
                p_Symbol = stockModel.Symbol,
                p_CompanyName = stockModel.CompanyName,
                p_Purchase = stockModel.Purchase,
                p_LastDiv = stockModel.LastDiv,
                p_Industry = stockModel.Industry,
                p_MarketCap = stockModel.MarketCap
            };

            var newStock = await _db.QuerySingleAsync<Stock>(sql, parameters, commandType: CommandType.StoredProcedure);
            return newStock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await GetByIdAsync(id);
            if (stockModel == null)
            {
                return null;
            }

            var sql = "sp_DeleteStock";
            await _db.ExecuteAsync(sql, new { p_Id = id }, commandType: CommandType.StoredProcedure);
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var sql = "sp_GetAllStocks";
            var parameters = new
            {
                p_CompanyName = query.CompanyName,
                p_Symbol = query.Symbol,
                p_SortBy = query.SortBy,
                p_IsDescending = query.IsDecsending,
                p_Skip = (query.PageNumber - 1) * query.PageSize,
                p_PageSize = query.PageSize
            };

            var stocks = await _db.QueryAsync<Stock>(sql, parameters, commandType: CommandType.StoredProcedure);
            return stocks.ToList();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var sql = "sp_GetStockById";
            
            using (var multi = await _db.QueryMultipleAsync(sql, new { p_Id = id }, commandType: CommandType.StoredProcedure))
            {
                var stock = await multi.ReadSingleOrDefaultAsync<Stock>();
                if (stock != null)
                {
                    stock.Comments = (await multi.ReadAsync<Comment>()).ToList();
                }
                return stock;
            }
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            var sql = "sp_GetStockBySymbol";
            return await _db.QuerySingleOrDefaultAsync<Stock>(sql, new { p_Symbol = symbol }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> StockExists(int id)
        {
            var sql = "sp_StockExists";
            return await _db.ExecuteScalarAsync<bool>(sql, new { p_Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var sql = "sp_UpdateStock";
            var parameters = new
            {
                p_Id = id,
                p_Symbol = stockDto.Symbol,
                p_CompanyName = stockDto.CompanyName,
                p_Purchase = stockDto.Purchase,
                p_LastDiv = stockDto.LastDiv,
                p_Industry = stockDto.Industry,
                p_MarketCap = stockDto.MarketCap
            };
            
            return await _db.QuerySingleOrDefaultAsync<Stock>(sql, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}