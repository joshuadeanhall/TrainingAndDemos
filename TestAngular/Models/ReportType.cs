using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAngular.Models
{
    public enum ReportType
    {
        /// <summary>
        /// Web Document, HTML 
        ///     WF_Keyword = HTML
        /// </summary>
        Html = 1,
        /// <summary>
        /// Excel Spreadsheet with formattign
        ///     WF_Keyword = EXL2K
        /// </summary>
        Excel = 2,
        /// <summary>
        /// Comma Separated Values
        ///     WF_Keyword = COMT
        /// </summary>
        CSV = 3,
        /// <summary>
        /// XML File        
        ///     WF_Keyword = XML
        /// </summary>
        XML = 4,
        ///// <summary>
        ///// Powerpoint
        /////     WF_Keyword = PPT
        ///// </summary>
        //[EnumMember]
        //[StringValue("PowerPoint")]
        //Powerpoint=5,
        /// <summary>
        /// Adobe PDF
        ///     WF_Keyword = PDF
        /// </summary>
        Pdf = 6,
        /// <summary>
        /// Excel Spreadsheet with formattign
        ///     WF_Keyword = EXL2K
        /// </summary>
        ExcelWithTotals = 5,
        /// <summary>
        /// Excel Spreadsheet with formattign
        ///     WF_Keyword = EXL07
        /// </summary>
        Excel2007 = 7,
        /// <summary>
        /// Excel Spreadsheet with formattign
        ///     WF_Keyword = EXL07
        Excel2007WithTotals = 8
    }
}