using System;
using System.Xml.Serialization;

namespace Source.Classes.DTOs
{
    /// <summary>
    /// DataTransferObjectBase is a Layer Supertype for
    /// data transfer objects.
    /// </summary>
    [Serializable()]
    public abstract class DataTransferObjectBase
    {
        internal static readonly string NULL_TOKEN = DateTime.MinValue.Ticks.ToString();

        private string _concurrencyToken = NULL_TOKEN;
        
        /// <summary>
        /// Gets or sets the concurrency token associated with the
        /// retrieval of the data being transferred in this object.
        /// </summary>
        /// <value>The fetch date/timestamp.</value>
        [XmlElement]
        public string ConcurrencyToken
        {
            get { return _concurrencyToken; }
            set { _concurrencyToken = value; }
        }
        
    }
}
