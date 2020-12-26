using System.Runtime.Serialization;

namespace ServiceContracts
{
    [DataContract]
    public class FileOperationsException
    {
        [DataMember]
        public string Message { get; set; }

        public FileOperationsException(string message)
        {
            Message = message;
        }
    }
}