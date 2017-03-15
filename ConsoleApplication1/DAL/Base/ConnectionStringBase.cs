using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace DAL.Base
{
    public abstract class ConnectionStringBase
    {
        public abstract DbConnectionStringBuilder GetDbConnectionStringBuilder();
    }
}
