using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace BeamApp
{
    abstract class Controller
    {
        protected Uri m_uri = null;
        protected HttpClient m_httpClient = new HttpClient();
    }
}
