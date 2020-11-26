using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using NLog;

namespace IntermediateAssessment.Filters
{
    /// <summary>
    /// Фильтр действия для протоколирования всех действий пользователя
    /// См. также https://professorweb.ru/my/ASP_NET/mvc/level5/5_5.php
    /// </summary>
    public class LoggingAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// Признак разрешения протоколирования
        /// </summary>
        private readonly bool enabled;

        /// <summary>
        /// Протоколирование
        /// </summary>
        private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="enabled">Разрешение протоколирования</param>
        public LoggingAttribute(bool Enabled = true)
        {
            this.enabled = Enabled;
        }

        /// <summary>
        /// Вызывается перед выполнением действия
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                // Проверка на включение протоколирования
                if (!enabled) { return; }

                // Для контроллеров и действий может быть задано описание при помощи атрибута Description

                // Массив описаний действия
                var aa = (DescriptionAttribute[])context.ActionDescriptor.GetCustomAttributes(typeof(DescriptionAttribute), false);

                // Массив описаний контроллера
                var ca = (DescriptionAttribute[])context.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(DescriptionAttribute), false);

                // Описание контроллера или его имя, если описание не задано
                string controller = (ca.Count() > 0) ? ca[0].Description : context.ActionDescriptor.ControllerDescriptor.ControllerName;
                // Ранее было Action = context.HttpContext.Request.AppRelativeCurrentExecutionFilePath
                // Описание действия или его имя, если описание не задано
                string action = (aa.Count() > 0) ? aa[0].Description : context.ActionDescriptor.ActionName;

                // Трассировка запросов
                log.Trace($"{controller}/{action}");
            }
            catch (Exception ex)
            {
                log.Warn(ex);
            }
        }

        /// <summary>
        /// Вызывается после выполнения действия
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Пустая реализация - ничего делать не требуется
        }
    }
}