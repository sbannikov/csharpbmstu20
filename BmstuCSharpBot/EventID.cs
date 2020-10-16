using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmstuCSharpBot
{
    /// <summary>
    /// Идентификаторы событий для журнала событий Windows
    /// </summary>
    public enum EventID
    {
        None = 0,
        StartService,
        StopService,
        PauseService,
        ContinueService
    }
}
