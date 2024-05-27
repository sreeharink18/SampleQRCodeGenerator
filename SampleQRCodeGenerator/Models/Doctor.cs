using QRCoder;

namespace SampleQRCodeGenerator.Models
{
    public class Doctor
    {
        public Doctor(int docId, string docName)
        {
            DocId = docId;
            Name = docName;
        }

        public int DocId { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{DocId} {Name}";  // Combine DocId and Name in a single string
        }

      
    }
}
