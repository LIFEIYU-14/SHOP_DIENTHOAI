using System;
using System.Runtime.Serialization;

namespace SHOP_DIENTHOAI.Areas.Admin.Controllers
{
    [Serializable]
    internal class dtUpdateConcurrencyException : Exception
    {
        public dtUpdateConcurrencyException()
        {
        }

        public dtUpdateConcurrencyException(string message) : base(message)
        {
        }

        public dtUpdateConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected dtUpdateConcurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}