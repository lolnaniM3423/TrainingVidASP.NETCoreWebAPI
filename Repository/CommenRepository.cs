using System.Data;
using Dapper;
using trnvid.Helpers;
using trnvid.Interfaces;
using trnvid.Models;

namespace trnvid.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IDbConnection _db;
        public CommentRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            var sql = "sp_CreateComment";
            var parameters = new
            {
                p_Title = commentModel.Title,
                p_Content = commentModel.Content,
                p_StockId = commentModel.StockId,
                p_AppUserId = commentModel.AppUserId
            };
            
            // Use Dapper's multi-mapping to map both Comment and AppUser
            var newComment = await _db.QueryAsync<Comment, AppUser, Comment>(
                sql,
                (comment, appUser) => 
                {
                    comment.AppUser = appUser;
                    return comment;
                },
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id" // Split the result set at the first 'Id' of the AppUser table
            );
            return newComment.Single();
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await GetByIdAsync(id);
            if (commentModel == null)
            {
                return null;
            }

            var sql = "sp_DeleteComment";
            await _db.ExecuteAsync(sql, new { p_Id = id }, commandType: CommandType.StoredProcedure);
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var sql = "sp_GetAllComments";
            var parameters = new 
            {
                p_Symbol = queryObject.Symbol,
                p_IsDescending = queryObject.IsDecsending
            };

            var comments = await _db.QueryAsync<Comment, AppUser, Comment>(
                sql,
                (comment, appUser) => 
                {
                    comment.AppUser = appUser;
                    return comment;
                },
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id"
            );
            return comments.ToList();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var sql = "sp_GetCommentById";
            var comments = await _db.QueryAsync<Comment, AppUser, Comment>(
                sql,
                (comment, appUser) => 
                {
                    comment.AppUser = appUser;
                    return comment;
                },
                new { p_Id = id },
                commandType: CommandType.StoredProcedure,
                splitOn: "Id"
            );
            return comments.FirstOrDefault();
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var sql = "sp_UpdateComment";
            var parameters = new
            {
                p_Id = id,
                p_Title = commentModel.Title,
                p_Content = commentModel.Content
            };

            var updatedComments = await _db.QueryAsync<Comment, AppUser, Comment>(
                sql,
                (comment, appUser) =>
                {
                    comment.AppUser = appUser;
                    return comment;
                },
                parameters,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id"
            );
            return updatedComments.FirstOrDefault();
        }
    }
}