using System.IO;

namespace AsposeBug
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ImportLicense();
                var wrdf = new Aspose.Words.Document("./input.html");
                string Dir = "./output/";
                if (Directory.Exists(Dir))
                    Directory.Delete(Dir, true);

                wrdf.Save(Dir + "output.pdf", Aspose.Words.SaveFormat.Pdf);
                wrdf.Save(Dir + "output.jpeg", Aspose.Words.SaveFormat.Jpeg);
                wrdf.Save(Dir + "output.html", Aspose.Words.SaveFormat.Html);
                wrdf.Save(Dir + "output.docx", Aspose.Words.SaveFormat.Docx);
            }
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
