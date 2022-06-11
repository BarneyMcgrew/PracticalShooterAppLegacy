using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Enums.Attributes;
using RulebookConversionLibrary.Extensions;
using UglyToad.PdfPig;

namespace RulebookConversionLibrary.Helpers
{
    public static class PdfPigHelper
    {
        private static readonly Regex RegexNewParagraph = new Regex(@"\s{2}", RegexOptions.Compiled);
        private static readonly Regex RegexHeader = new Regex(@".*Edition\s\d+\s", RegexOptions.Compiled);
        private static readonly Regex RegexTableOfContents = new Regex(@"\n(\d+\.?)+\s(\w\s?)+\.{5,}", RegexOptions.Compiled);
        
        [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
        public static string[] ConvertRulesPdfToText(Discipline discipline, Language language)
        {
            var textList = new List<string>();
            
            var rulebookName = discipline.GetAttribute<FilenameAttribute>().FileName;
            
            var binFolder = Directory.GetCurrentDirectory();
            var rootFolder = Path.GetFullPath(Path.Combine(binFolder, @"..\..\..\..\..\"));

            try
            {
                using (var document =
                    PdfDocument.Open($@"{rootFolder}\Rulebooks\Original PDF\{language}-{rulebookName}.pdf"))
                {
                    foreach (var page in document.GetPages())
                    {
                
                        // check page isn't empty using the header - skip if no header
                        if (!RegexHeader.IsMatch(page.Text))
                            continue;
                
                        // check page is table of contents - skip if table of contents
                        if (RegexTableOfContents.IsMatch(page.Text))
                            continue;
                
                        var pageText = RegexHeader.Replace(page.Text, "");

                        var paragraphs = RegexNewParagraph.Split(pageText);

                        foreach (var paragraph in paragraphs)
                        {
                            textList.Add(paragraph);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"!!!Issue with converting rules to text for {discipline} in {language}!!!");
            }
            
            return textList.ToArray();
        }
    }
}
