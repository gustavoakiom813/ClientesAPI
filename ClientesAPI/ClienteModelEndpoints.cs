using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Models;

public static class ClienteModelEndpoints
{
    public static void MapClienteModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/ClienteModel").WithTags(nameof(ClienteModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Clientes.ToListAsync();
        })
        .WithName("GetAllClienteModels");

        group.MapGet("/{id}", async Task<Results<Ok<ClienteModel>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is ClienteModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetClienteModelById");

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, ClienteModel clientemodel, AppDbContext db) =>
        {
            var affected = await db.Clientes
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                //.SetProperty(m => m.Id, clientemodel.Id)
                .SetProperty(m => m.Nome, clientemodel.Nome)
                .SetProperty(m => m.CPF, clientemodel.CPF)
                .SetProperty(m => m.Email, clientemodel.Email)
                .SetProperty(m => m.DataNascimento, clientemodel.DataNascimento)
                .SetProperty(m => m.DataCriacao, clientemodel.DataCriacao)
        );

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateClienteModel");

        group.MapPost("/", async (ClienteModel clientemodel, AppDbContext db) =>
        {
            db.Clientes.Add(clientemodel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/ClienteModel/{clientemodel.Id}",clientemodel);
        })
        .WithName("CreateClienteModel");

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.Clientes
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();

            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteClienteModel");
    }
}
