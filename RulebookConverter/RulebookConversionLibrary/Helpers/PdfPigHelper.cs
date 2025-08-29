using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.Linq;
using System.Drawing.Imaging;
using RulebookConversionLibrary.Enums;
using RulebookConversionLibrary.Enums.Attributes;
using RulebookConversionLibrary.Extensions;
using RulebookConversionLibrary.Models;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Rendering;

namespace RulebookConversionLibrary.Helpers
{
    public static class PdfPigHelper
    {
        private static readonly Regex RegexNewParagraph = new Regex(@"\s{2}", RegexOptions.Compiled);
        private static readonly Regex RegexHeader = new Regex(@".*Edition\s\d+\s", RegexOptions.Compiled);
        private static readonly Regex RegexTableOfContents = new Regex(@"\n(\d+\.?)+\s(\w\s?)+\.{5,}", RegexOptions.Compiled);
        
        [SuppressMessage("ReSharper", "ConvertToUsingDeclaration")]
        public static Dictionary<string, string[]> ConvertRulesPdfToText(Discipline discipline, Language language)
        {
            var rulebookName = discipline.GetAttribute<FilenameAttribute>().FileName;
            var binFolder = Directory.GetCurrentDirectory();
            var rootFolder = Path.GetFullPath(Path.Combine(binFolder, @"..\..\..\..\..\"));
            var pdfFolder = Path.Combine(rootFolder, "Rulebooks", "Original PDF");
            var revisionDict = new Dictionary<string, string[]>();

            // Pattern: {language}-{rulebookName}-{revision}.pdf
            var searchPattern = $"{language}-{rulebookName}-*.pdf";
            var pdfFiles = Directory.GetFiles(pdfFolder, searchPattern);
            var revisionRegex = new Regex($@"{language}-{rulebookName}-(\d{{6}})\.pdf", RegexOptions.Compiled);

            foreach (var pdfFile in pdfFiles)
            {
                var fileName = Path.GetFileName(pdfFile);
                var match = revisionRegex.Match(fileName);
                if (!match.Success) continue;
                var revisionCode = match.Groups[1].Value;
                var textList = new List<string>();
                try
                {
                    using (var document = PdfDocument.Open(pdfFile))
                    {
                        foreach (var page in document.GetPages())
                        {
                            if (!RegexHeader.IsMatch(page.Text))
                                continue;
                            if (RegexTableOfContents.IsMatch(page.Text))
                                continue;
                            var pageText = RegexHeader.Replace(page.Text, "");
                            var paragraphs = RegexNewParagraph.Split(pageText);
                            textList.AddRange(paragraphs);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading PDF {fileName}: {ex.Message}");
                }
                revisionDict[revisionCode] = textList.ToArray();
            }
            return revisionDict;
        }

        public static void SaveAppendixPagesAsImages(Discipline discipline, Language language, string revisionCode, IEnumerable<Models.Appendix> appendices)
        {
            var rulebookName = discipline.GetAttribute<FilenameAttribute>().FileName;
            var binFolder = Directory.GetCurrentDirectory();
            var rootFolder = Path.GetFullPath(Path.Combine(binFolder, @"..\..\..\..\..\"));
            var pdfFolder = Path.Combine(rootFolder, "Rulebooks", "Original PDF");
            var pdfFile = Path.Combine(pdfFolder, $"{language}-{rulebookName}-{revisionCode}.pdf");

            var imageFolder = Path.Combine(rootFolder, "Rulebooks", "AppendixImages", $"{language}-{rulebookName}-{revisionCode}");
            Directory.CreateDirectory(imageFolder);

            var appendixDict = appendices.ToDictionary(a => a.Name, a => a);

            using (var document = PdfDocument.Open(pdfFile))
            {
                var renderer = new PdfRenderer(document);
                foreach (var page in document.GetPages())
                {
                    foreach (var key in appendixDict.Keys.ToList())
                    {
                        var header = $"APPENDIX {key}";
                        if (page.Text.Contains(header))
                        {
                            var fileName = $"{key}.png";
                            var outputPath = Path.Combine(imageFolder, fileName);
                            using (var img = renderer.RenderPage(page.Number, 300))
                            {
                                img.Save(outputPath, ImageFormat.Png);
                            }
                            appendixDict[key].ImageFileName = fileName;
                            appendixDict.Remove(key);
                        }
                    }
                    if (appendixDict.Count == 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
