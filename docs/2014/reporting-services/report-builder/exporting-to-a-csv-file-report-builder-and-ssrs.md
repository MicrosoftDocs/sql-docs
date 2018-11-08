---
title: "Exporting to a CSV File (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 68ec746e-8c82-47f5-8c3d-dbe403a441e5
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Exporting to a CSV File (Report Builder and SSRS)
  The Comma-Separated Value (CSV) rendering extension renders reports as a flattened representation of data from a report in a standardized, plain-text format that is easily readable and exchangeable with many applications.  
  
 The CSV rendering extension uses a string character delimiter to separate fields and rows, with the string character delimiter configurable to be a character other than a comma. The resulting file can be opened in a spreadsheet program like [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] or used as an import format for other programs. The exported report becomes a .csv file, and returns a MIME type of `text/csv`.  
  
 If you want to work with data related to charts, data bars, sparklines, gauges, and indicators in [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)], export the report to a CSV file, and then open the file in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="CSVRendering"></a> CSV Rendering  
 When rendered using the default settings, a CSV report has the following characteristics:  
  
-   The default field delimiter string is a comma (,).  
  
    > [!NOTE]  
    >  You can change the field delimiter to any character that you want, including TAB, by changing the device information settings. For more information, see [CSV Device Information Settings](../csv-device-information-settings.md).  
  
-   The record delimiter string is the carriage return and line feed (\<cr>\<lf>).  
  
-   The text qualifier string is a quotation mark (").  
  
     The CSV renderer does not add qualifiers around all text strings. Text qualifiers are added only when the value contains the delimiter character or when the value has a line break.  
  
-   If the text contains an embedded delimiter string or qualifier string, the text qualifier is placed around the text, and the embedded qualifier strings are doubled.  
  
-   Formatting and layout are ignored.  
  
 The following items are ignored during rendering:  
  
-   Page header  
  
-   Page footer  
  
-   Custom report items  
  
-   Line  
  
-   Image  
  
-   Rectangle  
  
-   Automatic subtotals  
  
 The remaining report items are sorted, from top to bottom, then left to right. Each item is then rendered to a column. If the report has nested data items like lists or tables, the parent items are repeated in each record.  
  
 The following table indicates the appearance of report items when rendered:  
  
|Item|Rendering behavior|  
|----------|------------------------|  
|Text box|Renders the contents of the text box. In default mode, items are formatted based on the item's formatting properties. In compliant mode, formatting can be changed by device information settings. For more information about CSV rendering modes, see below.|  
|Table|Renders by expanding the table and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns do not have column or row headings. Drillthrough reports are not supported.|  
|Matrix|Renders by expanding the matrix and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns do not have column or row headings.|  
|List|Renders a record for each detail row or instance in the list.|  
|Subreport|The parent item is repeated for each instance of the contents.|  
|Chart|Renders by creating a row for each chart value and member labels. Labels from series and categories in hierarchies are flattened and included in the row for a chart value.|  
|Data bar|Renders like a chart. Typically, a data bar does not include hierarchies or labels.|  
|Sparkline|Renders like a chart. Typically, a sparkline does not  do not include hierarchies or labels.|  
|Gauge|Renders as a single record with the minimum and maximum values of the linear scale, start and end values of the range, and the value of the pointer.|  
|Indicator|Renders as a single record with the active state name, available states, and the data value.|  
|Map|Renders a row with the labels and values for each map member of a map layer.<br /><br /> If the map has multiple layers the values in the rows varies depending on whether the map layers use the same or different map data regions. If multiple map layers use the same data region, the rows contain data from all layers.|  
  
### Hierarchical and Grouped Data  
 Hierarchical and grouped data must be flattened in order to be represented in the CSV format.  
  
 The rendering extension flattens the report into a tree structure that represents the nested groups within the data region. To flatten the report:  
  
-   A row hierarchy is flattened before a column hierarchy.  
  
-   Columns are ordered as follows: text boxes in body order left-to-right, top-to-bottom followed by data regions ordered left-to-right, top-to-bottom.  
  
-   Within a data region, the columns are ordered as follows: corner members, row hierarchy members, column hierarchy members, and then cells.  
  
-   Peer data regions are data regions or dynamic groups that share a common data region or dynamic ancestor. Peer data is identified by branching of the flattened tree.  
  
 For more information, see [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md).  
  
 
  
##  <a name="RenderingModes"></a> Renderer Modes  
 The CSV rendering extension can operate in two modes: one is optimized for Excel and the other is optimized for third-party applications that require strict CSV compliance to the CSV specification in RFC 4180. Depending on which mode you use, peer data regions are handled differently.  
  
### Default Mode  
 The default mode is optimized for Excel. When rendered in default mode, the report is rendered as a CSV file with multiple sections of CSV-rendered data. Each peer data region is delimited by an empty line. Peer data regions within the report body are rendered as separate blocks of data within the CSV file. The result is a CSV file in which:  
  
-   Individual text boxes within the report body are rendered once as the first block of data within the CSV file.  
  
-   Each top-level peer data region in the report body is rendered in its own data block.  
  
-   Nested data regions are rendered diagonally into the same data block.  
  
#### Formatting  
 Numeric values are rendered in their formatted state. Excel can recognize formatted numeric values, such as currency, percentage and date, and format the cells appropriately when importing the CSV file.  
  
### Compliant Mode  
 Compliant mode is optimized for third-party applications.  
  
#### Data Regions  
 Only the first row of the file contains the column headers and each row has the same number of columns.  
  
#### Formatting  
 Values are unformatted.  
  
##  <a name="Interactivity"></a> Interactivity  
 Interactivity is not supported by either CSV formats generated by this renderer. The following interactive elements are not rendered:  
  
-   Hyperlinks  
  
-   Show or Hide  
  
-   Document Map  
  
-   Drillthrough or clickthrough links  
  
-   End user sort  
  
-   Fixes headers  
  
-   Bookmarks  
  

  
##  <a name="DeviceInfo"></a> Device Information Settings  
 You can change some default settings for this renderer, including which mode to render in, which characters to use as delimiters and which characters to use as the text qualifier default string, by changing the device information settings. For more information, see [CSV Device Information Settings](../csv-device-information-settings.md).  
  
  
  
## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../report-design/rendering-behaviors-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](interactive-functionality-different-report-rendering-extensions.md)   
 [Rendering Report Items &#40;Report Builder and SSRS&#41;](../report-design/rendering-report-items-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)  
  
  
