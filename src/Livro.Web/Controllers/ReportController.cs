using FastReport;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public class ReportController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public ReportController(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public IActionResult Index()
    {
        var webReport = new WebReport();
        var data = LoadDataFromView();
        var reportPath = Path.Combine(_environment.WebRootPath, "reports", "Report.frx");

        if (!System.IO.File.Exists(reportPath))
        {
           // Console.WriteLine("O arquivo de relatório não foi encontrado.");
            return NotFound("O arquivo de relatório não foi encontrado.");
        }

        webReport.Report.Load(reportPath);
        webReport.Report.RegisterData(data, "viewLivros");
        webReport.Report.GetDataSource("viewLivros").Enabled = true;

        ViewBag.WebReport = webReport;

        return View(webReport);
    }

    private DataTable LoadDataFromView()
    {
        var dataTable = new DataTable("viewLivros");
        string connectionString = _configuration.GetConnectionString("Sql");

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("SELECT Autor, Titulo, Assunto, Editora, Edicao, Ano FROM viewLivros", connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }
        }

        return dataTable;
    }
}
