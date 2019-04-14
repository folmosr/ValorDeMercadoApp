using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Core
{
    public sealed class Logger
    {
        private Logger()
        {

        }

        #region Public Fields

        public static readonly string PROCESS_NAME = "Cálculo valor de Mercado APP";

        #endregion Public Fields

        #region Private Fields

        private IUnityContainer _container = new UnityContainer().AddNewExtension<EnterpriseLibraryCoreExtension>();
        ExceptionManager _exceptionHandler = null;
        LogWriter _LogWriter = null;

        private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        #endregion Private Fields

        #region Public Properties

        public ExceptionManager ExceptionHandler
        {
            get
            {
                if (_exceptionHandler == null)
                {
                    _exceptionHandler = _container.Resolve<ExceptionManager>();
                }
                return _exceptionHandler;
            }
        }

        public LogWriter LogWriter
        {
            get
            {
                if (_LogWriter == null)
                {
                    _LogWriter = _container.Resolve<LogWriter>();
                }

                return _LogWriter;
            }
        }

        #endregion Public Properties

    }
}
