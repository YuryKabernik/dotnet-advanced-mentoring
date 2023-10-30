using System.Data.Common;
using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts.Interfaces;
using Dapper;

namespace CatalogService.DataAccess;

public class CategoryRepository : IRepository<Category>
{
    private readonly DbConnection dbConnection;

    public CategoryRepository(DbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task Add(Category category)
    {
        string sql = @"
            INSERT INTO Categories (Name, Image, Parent)
            VALUES(@Name, @Image, @Parent)";

        await dbConnection.ExecuteAsync(sql, category);
    }

    public async Task Delete(int id)
    {
        var sqlQuery = "DELETE FROM Categories WHERE Id = @id";
        await dbConnection.ExecuteAsync(sqlQuery, new { id });
    }

    public async Task<Category?> Get(int id)
    {
        string sql = "SELECT * FROM Categories WHERE Id = @id";
        Category? category = await dbConnection.QueryFirstOrDefaultAsync<Category>(sql, new { id });

        return category;
    }

    public async Task<IEnumerable<Category>> List()
    {
        string sql = "SELECT * FROM Categories";
        IEnumerable<Category> items = await dbConnection.QueryAsync<Category>(sql);

        return items;
    }

    public async Task Update(int id, Category newCategory)
    {
        var sqlQuery = @"
            UPDATE
                Items
            SET
                Name = @Name,
                Image = @Image,
                Parent = @Parent,
            WHERE
                Id = @Id";

        await dbConnection.ExecuteAsync(sqlQuery, newCategory);
    }
}
