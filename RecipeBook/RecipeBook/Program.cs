using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using RecipeBook.BL;
using RecipeBook.BL.Interfaces;
using RecipeBook.DL;
using RecipeBook.Models.Configurations;
using RecipeBook.Validators;

namespace RecipeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<MongoDbConfiguration>(
                builder.Configuration
                    .GetSection(nameof(MongoDbConfiguration)));

            builder.Services
                .RegisterRepositories()
                .RegisterServices();

            builder.Services.AddMapster();

            builder.Services.AddControllers();

            builder.Services.AddValidatorsFromAssemblyContaining<TestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<RecipeValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<IngredientValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            app.MapHealthChecks("/healthz");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
