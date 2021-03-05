using System;
using System.Collections;
using System.IO;
using Aspose.Words;
using Aspose.Words.Fonts;
using System.Linq;

namespace AsposeBug
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ImportLicense();
                var callback = new FontSubstitutionWarningCollector();
                var fontSettings = new FontSettings();
                var loadOptions = new LoadOptions { FontSettings = fontSettings };
                var wrdf = new Aspose.Words.Document("./input.html", loadOptions);
                wrdf.FontSettings.SubstitutionSettings.FontConfigSubstitution.Enabled = false;
                wrdf.WarningCallback = callback;
                var Dir = "./output/";
                if (Directory.Exists(Dir))
                    Directory.Delete(Dir, true);

                Aspose.Words.Saving.PdfSaveOptions pdfSaveOptions = new Aspose.Words.Saving.PdfSaveOptions{
                    FontEmbeddingMode = Aspose.Words.Saving.PdfFontEmbeddingMode.EmbedAll
                };
                wrdf.Save(Dir + "output.pdf", SaveFormat.Pdf);
                wrdf.Save(Dir + "output.jpeg", Aspose.Words.SaveFormat.Jpeg);
                wrdf.Save(Dir + "output.html", Aspose.Words.SaveFormat.Html);
                wrdf.Save(Dir + "output.docx", Aspose.Words.SaveFormat.Docx);

                if(callback.FontSubstitutionWarnings.Any())
                    Console.WriteLine("Font warning exists");
            }
        }
 
        private class FontSubstitutionWarningCollector : IWarningCallback
        {
            /// <summary>
            /// Called every time a warning occurs during loading/saving.
            /// </summary>
            public void Warning(WarningInfo info)
            {
                if (info.WarningType == WarningType.FontSubstitution)
                    FontSubstitutionWarnings.Warning(info);
            }

            public WarningInfoCollection FontSubstitutionWarnings = new WarningInfoCollection();
        }

        public static void ImportLicense()
        {
            var asposeLicenseValue = File.ReadAllText("./license.txt");

            if (string.IsNullOrEmpty(asposeLicenseValue))
                return;

            using var stream = new MemoryStream();

            using var write = new StreamWriter(stream);

            write.Write(asposeLicenseValue);
            write.Flush();
            stream.Position = 0;

            var docLicense = new Aspose.Words.License();
            docLicense.SetLicense(stream);

            stream.Position = 0;
            var pdfLicense = new Aspose.Pdf.License();
            pdfLicense.SetLicense(stream);

            stream.Position = 0;
            var imagingLicense = new Aspose.Imaging.License();
            imagingLicense.SetLicense(stream);

            stream.Position = 0;
            var htmlLicense = new Aspose.Html.License();
            htmlLicense.SetLicense(stream);
        }
    }
}
