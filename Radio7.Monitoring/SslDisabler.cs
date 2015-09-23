using System;
using System.Net;

namespace Radio7.Monitoring
{
    class SslDisabler : IDisposable
    {
        public SslDisabler(bool disableSslCertificatateValidation)
        {
            try
            {
                // Change SSL checks so that all checks pass
                if (disableSslCertificatateValidation)
                    ServicePointManager.ServerCertificateValidationCallback = (delegate { return true; });
            }
            catch { }
        }

        private bool _disposing;
        public void Dispose()
        {
            if (_disposing) return;

            _disposing = true;

            ServicePointManager.ServerCertificateValidationCallback = null;
        }
    }
}
