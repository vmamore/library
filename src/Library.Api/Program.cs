static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

try
{
    CreateHostBuilder(args).Build().Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
