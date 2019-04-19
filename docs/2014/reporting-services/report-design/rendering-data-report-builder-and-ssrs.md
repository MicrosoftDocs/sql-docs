---
title: "Rendering Data (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a458fdf9-fb2a-4fee-9fbd-b38f36e91753
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Rendering Data (Report Builder and SSRS)
  When using layout renderers, such as HTML, MHTML, Word, Excel, PDF or Image, the data and its organization remains unchanged. When exporting using a data renderer format, such as Comma-Separated Value (CSV) or XML, no visual layout elements are rendered. CSV and XML apply certain rules to the report body and its contents when rendering the report. These rules determine how the data is rendered in these formats.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 You can use data renderers to:  
  
-   Import to a database. CSV is a common format that is easily importable by many database applications including SQL Server and Microsoft Access.  
  
-   Import to Excel. Use the CSV renderer to export your data to Excel without the visual layout. After your data is in Excel, you can take advantage of standard Excel tools such as charts, formulas, and pivot tables.  
  
-   XSLT transformations. An XSLT can be applied to the output of the XML renderer. This server-side transformation is a powerful technique to transform your data to virtually any format.  
  
-   Data exchange/EDI. An external process can request a CSV or XML rendering of a report and consume that data.  
  
 Data renderer formats are controlled by a different set of properties than layout renderers. The following is a list of properties set in the **Properties** pane that apply only to data renderers:  
  
-   The DataElementOutput property controls whether or not a specific item is present in the data when exported.  
  
-   The DataElementName property controls the name of the data element. In CSV, this controls the name of the CSV column header. In XML, this becomes the name of the XML element or attribute for the item.  
  
-   The DataElementStyle property controls, in XML, whether or not the report item is rendered as an element or an attribute.  
  
 The CSV export option saves report data as comma-delimited plain text files, without any formatting. By default, the file uses a comma (,) to delimit fields and rows but this setting is configurable using the Device Information Settings. The resulting file can be opened in a spreadsheet program like Office SharePoint Server or used as an import format for other programs. The .csv file opens in a text editor such as Notepad. If accessed as a URL, the .csv file returns a MIME type of **text/csv**. The files are MIME version 1.0. For more information about rendering your report in the CSV file type, see [Exporting to a CSV File &#40;Report Builder and SSRS&#41;](../report-builder/exporting-to-a-csv-file-report-builder-and-ssrs.md).  
  
 The XML file with report data export option saves a report as an XML file. The XML schema for the report is specific to the report. The report layout information is not saved by the XML export option. The XML generated using this option can be imported into a database, used as an XML data message, or sent to a custom application. For more information about rendering your report in the XML file type, see [Exporting to XML &#40;Report Builder and SSRS&#41;](../report-builder/exporting-to-xml-report-builder-and-ssrs.md).  
  
## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](rendering-behaviors-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](../report-builder/interactive-functionality-different-report-rendering-extensions.md)   
 [Rendering Report Items &#40;Report Builder and SSRS&#41;](rendering-report-items-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)   
 [Reporting Services Device Information Settings](https://go.microsoft.com/fwlink/?LinkId=102515)  
  
  
