using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Mozo.Comun.Implement.Log;

public class ELog
{
    public static void Save(ProblemDetails problem)
    {
        var fecha = DateTime.Now.ToString("yyyyMMdd");
        var hora = DateTime.Now.ToString("HH:mm:ss");
        if (!Directory.Exists(string.Concat(Directory.GetCurrentDirectory(), "/log")))
            Directory.CreateDirectory(string.Concat(Directory.GetCurrentDirectory(), "/log"));
        var path = string.Concat(Directory.GetCurrentDirectory(), "/log/", fecha, ".txt");
        var sw = new StreamWriter(path, true);

        sw.WriteLine(problem.Title + " " + hora);
        sw.WriteLine("Status: " + problem.Status);
        sw.WriteLine("Instance: " + problem.Instance);
        sw.WriteLine("Type: " + problem.Type);
        sw.WriteLine("Detail: " + problem.Detail);
        sw.Flush();
        sw.Close();
    }
}