using MarkerMetro.Unity.WinLegacy.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkerMetro.Unity.WinLegacy.Net
{
    public class WebRequest : System.Net.WebRequest
    {
        readonly System.Net.WebRequest _actual;

        private WebRequest(System.Net.WebRequest actaul)
        {
            if (actaul == null)
                throw new ArgumentNullException("actaul", "actaul is null.");

            _actual = actaul;
        }

        public new NameValueCollection Headers
        {
            get
            {   // TODO: This will not work as setting values should set values in collection below:
                return new WebHeaderCollection(_actual.Headers);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // TODO: Add Headers somehow?
        new public static WebRequest Create(string requestUriString)
        {
            var actual = System.Net.WebRequest.Create(requestUriString);

            return new WebRequest(actual);
        }

        new public static WebRequest Create(Uri requestUri)
        {
            var actual = System.Net.WebRequest.Create(requestUri);

            return new WebRequest(actual);
        }

#if NETFX_CORE || SILVERLIGHT
        int _timeout;   // TODO: Set default value
#endif

#if SILVERLIGHT
        public virtual int Timeout
#else
        public override int Timeout
#endif
        {
            get
            {
#if NETFX_CORE || SILVERLIGHT
                return _timeout;
#else
                return base.Timeout;
#endif
            }
            set
            {
                if (value < -1)
                    throw new ArgumentOutOfRangeException("value");

#if NETFX_CORE || SILVERLIGHT
                // TODO: check range
                _timeout = value;
#else
                base.Timeout = value;
#endif
            }
        }
    }
}
