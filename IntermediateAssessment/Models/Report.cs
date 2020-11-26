using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntermediateAssessment.Models
{
    /// <summary>
    /// Данные для отчета
    /// </summary>
    public class Report
    {
        public List<Storage.Assessment> Assessments;
        public List<Group> Groups;

        public Report(List<Storage.Assessment> alist, List<Group> glist)
        {
            Assessments = alist;
            Groups = glist;
        }
    }
}