using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP01_SW2.Negocio;
using TP01_SW2.Repository;

namespace ExemploAula03
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

        }
        
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Book/{index:int}/Name", GetBookName);
            builder.MapRoute("Book/{index:int}/Info", GetBookInfo);
            builder.MapRoute("Book/{index:int}/Authors", GetBookAuthorsName);
            builder.MapRoute("Book/{index:int}", GetBookHTML);
            var rotas = builder.Build();

            app.UseRouter(rotas);
        }
        
        public Task GetBookName(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repoCSV = new BookRepositoryCSV();

            var book = _repoCSV.Books[bookIndex];

            return context.Response.WriteAsync(book.Name);
        }

        public Task GetBookInfo(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repoCSV = new BookRepositoryCSV();

            var book = _repoCSV.Books[bookIndex];

            return context.Response.WriteAsync(book.ToString());
        }

        public Task GetBookAuthorsName(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repoCSV = new BookRepositoryCSV();

            var book = _repoCSV.Books[bookIndex];

            return context.Response.WriteAsync(book.getAuthorNames());

        }
        public Task GetBookHTML(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repoCSV = new BookRepositoryCSV();
            var book = _repoCSV.Books[bookIndex];

            var htmlFIle = LoadHTMLFile("Book");

            htmlFIle = htmlFIle.Replace("#BOOK-NAME#", book.Name);
            foreach (Author author in book.Authors)
            {
                htmlFIle = htmlFIle.Replace("#AUTHOR#", $"<li>{author.Name}</li>#AUTHOR#");
            }
            htmlFIle = htmlFIle.Replace("#AUTHOR#", "");

            return context.Response.WriteAsync(htmlFIle);
        }

        private string LoadHTMLFile(string fileName)
        {
            var fullPath = $@"C:\Users\Nikolas Caldeira\Documents\ADS\671\2ª & 3ª -Sistema Web II\TP01_SW2-master\TP01_SW2-master\TP01_SW2\HTML\{fileName}.html";
            using (var arquivo = File.OpenText(fullPath))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
