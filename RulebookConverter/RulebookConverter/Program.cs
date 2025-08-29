using RulebookConversionLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Services;

namespace ConsoleUI
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var dateTimePostfix = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var dbPath = $@"{Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\"))}sqLite\\rulesData_{dateTimePostfix}.db";
            
            var conversionService = new ConversionService(dbPath);
            conversionService.ConvertAllRulebooks();
        }
    }
}
