using System.Runtime.Serialization;

namespace ServiceContracts
{
    [DataContract]

	public class Files
	{
        [DataMember]
		public string FileName { get; set; }

        public Files(string fileName)
        {
            FileName = fileName;
        }
    }
}
