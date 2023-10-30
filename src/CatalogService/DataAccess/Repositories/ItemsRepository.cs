using System.Data.Common;
using CatalogService.BusinessLogic.Entities;
using CatalogService.Contracts.Interfaces;
using Dapper;

namespace CatalogService.DataAccess;

public class ItemsRepository : IRepository<Item>
{
    private readonly DbConnection dbConnection;

    public ItemsRepository(DbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task Add(Item item)
    {
        string sql = @"
            INSERT INTO Items
                (Name, Description, Image, Category, Price, Amount)
            VALUES(@Name, @Description, @Image, @Category, @Price, @Amount)";

        await dbConnection.ExecuteAsync(sql, item);
    }

    public async Task Delete(int id)
    {
        var sqlQuery = "DELETE FROM Items WHERE Id = @id";
        await dbConnection.ExecuteAsync(sqlQuery, new { id });
    }

    public async Task<Item?> Get(int id)
    {
        string sql = "SELECT * FROM Items WHERE Id = @id";
        Item? item = await dbConnection.QueryFirstOrDefaultAsync<Item>(sql, new { id });

        return item;
    }

    public async Task<IEnumerable<Item>> List()
    {
        string sql = "SELECT * FROM Items";
        IEnumerable<Item> items = await dbConnection.QueryAsync<Item>(sql);

        return items;
    }

    public async Task Update(int id, Item newItem)
    {
        var sqlQuery = @"
            UPDATE
                Items
            SET
                Name = @Name,
                Description = @Description,
                Image = @Image,
                Category = @Category,
                Price = @Price,
                Amount = @Amount
            WHERE
                Id = @Id";

        await dbConnection.ExecuteAsync(sqlQuery, newItem);
    }
}
