using System.IO;

namespace AsposeBug
{
    class Program
    {
    private const string Dir = "/Users/savaris/Dev/output/";

    static void Main(string[] args)
    {
            using (MemoryStream ms = new MemoryStream())
            {
                var asposeLicenseValue ="<License> <Data> <LicensedTo>PwC Global Licensing Services Corporation</LicensedTo> <EmailTo>pwcglsc@ca.pwc.com</EmailTo> <LicenseType>Site OEM</LicenseType> <LicenseNote>Limited to 10 developers, unlimited physical locations</LicenseNote> <OrderID>200603141327</OrderID> <UserID>361985</UserID> <OEM>This is a redistributable license</OEM> <Products> <Product>Aspose.Total for .NET</Product> </Products> <EditionType>Enterprise</EditionType> <SerialNumber>253bf0bd-0220-4a3a-8d4c-ea1662a29773</SerialNumber> <SubscriptionExpiry>20210628</SubscriptionExpiry> <LicenseVersion>3.0</LicenseVersion> <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions> </Data> <Signature>K0NBkNtixNqQnEOXx4uPebQno6ZfTesBrmkwO9d8O/HqVgUwLLxhfXzLZLKgBlJJDZtGyaj+wQMmycHY4Q5NlFeC3EP9JrDpKx6l55SUiibGEIUaQ8HMz/ezQXeJVx4R270od/9sxru2HO5yaPyVpDFWWHlpp3iui7uJWTIJzO0=</Signature> </License>";
                
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
                
                using (FileStream file = new FileStream("/Users/savaris/Dev/sample.html", FileMode.Open, FileAccess.Read)) {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }
                
                var wrdf = new Aspose.Words.Document("/Users/savaris/Dev/sample.html");
                wrdf.Save(Dir + "output.docx", Aspose.Words.SaveFormat.Docx);
                wrdf.Save(Dir + "output.pdf", Aspose.Words.SaveFormat.Pdf);
                wrdf.Save(Dir + "output.jpeg", Aspose.Words.SaveFormat.Jpeg);
                wrdf.Save(Dir + "output.html", Aspose.Words.SaveFormat.Html);
            }
        }
    }
}
