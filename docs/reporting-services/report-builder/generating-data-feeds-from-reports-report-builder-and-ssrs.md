---
title: "Generating Data Feeds from Reports (Report Builder and SSRS) | Microsoft Docs"
ms.date: 05/30/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-builder


ms.topic: conceptual
ms.assetid: 4e00789f-6967-42e5-b2b4-03181fdb1e2c
author: markingmyname
ms.author: maghan
---

# Generating Data Feeds from Reports (Report Builder and SSRS)

  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Atom rendering extension generates an Atom service document that lists the data feeds available from a paginated report and the data feeds from the data regions in a report. You use this extension to generate Atom-compliant data feeds that are readable and exchangeable with applications that can consume data feeds generated from reports. For example, you can use the Atom rendering extension to generated data feeds that you can then use in Power Pivot or Power BI.  
  
 The Atom service document lists at least one data feed for each data region in a report. Depending on the type of data region and the data that the data region displays, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] might generate multiple data feeds from a data region. For example, a matrix or chart can provide multiple data feeds. When the Atom rendering extension creates the Atom service document, a unique identifier is created for each data feed and you use the identifier in the URL to access the content of the data feed.  
  
 The way that the Atom rendering extension generates data for a data feed is similar to how the Comma-Separated Value (CSV) rendering extension renders data to a CSV file. Like a CSV file, a data feed is a flattened representation of the report data. For example, a table with a row group that sums the sales within a group repeats the sum in every data row and there is no separate row that contains only the sum.  
  
 You can generate Atom service documents and data feeds using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, Report Server, or a SharePoint site that is integrated with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 Atom applies to a pair of related standards. The Atom service document conforms to the RFC 5023 Atom publishing protocol specification and the data feeds conform to the RFC 4287 Atom syndication format protocol specification.  
  
 The following sections provide additional information about how to use the Atom rendering extension:  
  
 [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="ReportDataAsDataFeeds"></a> Reports as Data Feeds  
 You can export a production report as a data feed or you can create a report whose primary purpose is provide data, in the form of data feeds, to applications. Using reports as a data feed gives you an additional way to provide data to applications when the data is not easy to access through client data providers, or when you prefer to hide the complexity of the data source and make it simpler to use the data. Another benefit of using report data as a data feed is that you can use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features such as security, scheduling, and report snapshots to manage the reports that provide data feeds.  
  
 To get the most from the Atom rendering extension, you should understand how the report is rendered into data feeds. If you are using existing reports, being able to predict what the data feeds the reports will generate is useful; if you are writing report specifically for use as data feeds, being able to include the data and fine tune the report layout to maximize the usefulness of the data feeds is valuable.  
  
 For more information, see [Generate Data Feeds from a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-builder/generate-data-feeds-from-a-report-report-builder-and-ssrs.md).  
  
  
##  <a name="AtomServiceDocument"></a> Atom Service Document (.atomsvc file)  
 An Atom service document specifies a connection to one or more data feeds. At a minimum, the connection is a simple URL to the data service that produces the feed.  
  
 When you render report data by using the Atom rendering extension, the Atom service document lists the data feeds available for a report. The document lists at least one data feed for each data region in the report. Tables and gauges generate only one data feed each, but matrices, lists, and charts might generated multiple depending on the data they display.  
  
 The following diagram shows a report that uses two tables and a chart.  
  
 ![RS_Atom_TableAndChartDataFeeds](../../reporting-services/report-builder/media/rs-atom-tableandchartdatafeeds.gif "RS_Atom_TableAndChartDataFeeds")  
  
 The Atom service document generated from this report includes three data feeds, one for each table and one for the chart.  
  
 The matrix data regions might have more than one data feed, depending on the structure of the matrix. The following diagram shows a report that uses a matrix that generates two data feeds.  
  
 ![RS_Atom_PeerDynamicColumns](../../reporting-services/report-builder/media/rs-atom-peerdynamiccolumns.gif "RS_Atom_PeerDynamicColumns")  
  
 The Atom service document generated from this report includes two data feeds, one for each of the dynamic peer columns: Territory and Year. The following diagram shows the content of each data feed.  
  
 ![RS_Atom_PeerDynamicDataFeeds](../../reporting-services/report-builder/media/rs-atom-peerdynamicdatafeeds.gif "RS_Atom_PeerDynamicDataFeeds")  
  
  
##  <a name="DataFeeds"></a> Data Feeds  
 The data feed is an XML file that has a consistent tabular format that does not change over time and variable data that can be different each time the report is run. The data feeds generated by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are in the same format as those generated by  that ADO.NET Data Services.  
  
 A data feed contains two sections: header and data. The Atom specification defines the elements in each section. The header includes information such as the character encoding schema to use with the data feeds.  
  
### Header Section  
 The following XML code shows the header section of a data feed.  
  
 `<?xml version="1.0" encoding="utf-8" standalone="yes"?><feed xmlns:d="https://schemas.microsoft.com/ado/2007/08/dataservices" xmlns:m="https://schemas.microsoft.com/ado/2007/08/dataservices/metadata" xmlns="http://www.w3.org/2005/Atom">`  
  
 `<title type="text"></title>`  
  
 `<id>uuid:1795992c-a6f3-40ec-9243-fbfd0b1a5be3;id=166321</id>`  
  
 `<updated>2009-05-08T23:09:58Z</updated>`  
  
### Data Section  
 The data section of the data feeds contains one \<**entry**> element for each row in the flattened rowset generated by the Atom rendering extension.  
  
 The following diagram shows a report that uses groups and totals.  
  
 ![RS_Atom_ProductSalesSummaryCircledValues](../../reporting-services/report-builder/media/rs-atom-productsalessummarycircledvalues.gif "RS_Atom_ProductSalesSummaryCircledValues")  
  
 The following XML shows an \<**entry**> element from that report in a data feed. Notice that the \<**entry**> element includes the totals of the sales and orders for the group and the totals of sales and orders for all the groups. The \<**entry**> element includes all values on the report.  
  
 `<entry><id>uuid:1795992c-a6f3-40ec-9243-fbfd0b1a5be3;id=166322</id><title type="text"></title><updated>2009-05-08T23:09:58Z</updated><author /><content type="application/xml"><m:properties>`  
  
 `<d:ProductCategory_Value>Accessories</d:ProductCategory_Value>`  
  
 `<d:OrderYear_Value m:type="Edm.Int32">2001</d:OrderYear_Value>`  
  
 `<d:SumLineTotal_Value m:type="Edm.Decimal">20235.364608</d:SumLineTotal_Value>`  
  
 `<d:SumOrderQty_Value m:type="Edm.Int32">1003</d:SumOrderQty_Value>`  
  
 `<d:SumLineTotal_Total_2_1 m:type="Edm.Decimal">1272072.883926</d:SumLineTotal_Total_2_1>`  
  
 `<d:SumOrderQty_Total_2_1 m:type="Edm.Double">61932</d:SumOrderQty_Total_2_1>`  
  
 `<d:SumLineTotal_Total_2_2 m:type="Edm.Decimal">109846381.399888</d:SumLineTotal_Total_2_2>`  
  
 `<d:SumOrderQty_Total_2_2 m:type="Edm.Double">274914</d:SumOrderQty_Total_2_2></m:properties></content>`  
  
 `</entry>`  
  
### Working with Data Feeds  
 All data feeds generated by the report include the report items that are in scope of the parent of the data region that generate the data feeds. . Imagine a report that has several tables and a chart. Text boxes in the report body provides descriptive text of each data region. Every entry in every data feed that the report generates includes the value of the text box. For example, if the text is "Chart displays monthly sales averages by sales region", all three data feeds would include this text on each row.  
  
 If the report layout includes hierarchical data relationships, such as nested data regions, those relationships are included in the flattened rowset of report data.  
  
 The data rows for nested data regions are typically wide, especially if the nested tables and matrices include groups and totals. You might find it useful to export the report to a data feed and view the data feed to verify that the data generated is what you expected.  
  
 When the Atom rendering extension creates the Atom service document, a unique identifier is created for the data feed and you use the identifier in the URL to view the content of the data feed. The sample Atom service document, shown above, includes the URL `https://ServerName/ReportServer?%2fProduct+Sales+Summary&rs%3aCommand=Render&rs%3aFormat=ATOM&rc%3aDataFeed=xAx0x1`. The URL identifies the report (Product Sales Summary), the Atom rendering format (ATOM), and the name of the data feed (xAx0x1).  
  
 Report item names default to the report definition language (RDL) element names of the report items and often they are not intuitive or easy to remember. For example, the default name of the first matrix placed in a report is Tablix 1. The data feeds use these names.  
  
 To make the data feed easier to work with, you can use the DataElementName property of the data region to provide friendly names. If you provide a value for DataElementName the data feed subelement \<**d**> will use is it instead of the default data region name. For example, if the default name of a data regions is Tablix1 and DataElementName set SalesByTerritoryYear then the \<**d**> in the data feed uses SalesByTerritoryYear. If the data regions has two data feeds like the matrix report described above, the names used in the data feeds are SalesByTerritoryYear _Territory and SalesByTerritoryYear _Year.  
  
 If you compare the data shown on the report and the data in the data feed, you might notice some differences. Reports often shows formatted numeric and time/date data whereas the data feed contains unformatted data.  
  
 A data feed is saved with the .atom file name extension. You can use a text or XML editor such as Notepad or XML Editor to view the file structure and content.  
  
  
##  <a name="FlatteningReportData"></a> Flattening Report Data  
 The Atom renderer provides report data as flattened rowsets in an XML format. The rules for flattening data tables are the same to those of the CSV renderer with a few exceptions:  
  
-   Items in scope are flattened to the detail level. Unlike the CSV renderer, the text boxes at the top level appear in each entry written to the data feed.  
  
-   Report parameter values are rendered on each row of the output.  
  
 Hierarchical and grouped data must be flattened in order to be represented in the Atom-compliant format. The rendering extension flattens the report into a tree structure that represents the nested groups within the data region. To flatten the report:  
  
-   A row hierarchy is flattened before a column hierarchy.  
  
-   Members of the row hierarchy are rendered to the data feed before members of the column hierarchy.  
  
-   Columns are ordered as follows: text boxes in body order left-to-right, top-to-bottom followed by data regions ordered left-to-right, top-to-bottom.  
  
-   Within a data region, the columns are ordered as follows: corner members, row hierarchy members, column hierarchy members, and then cells.  
  
-   Peer data regions are data regions or dynamic groups that share a common data region or dynamic ancestor. Peer data is identified by branching of the flattened tree.  
  
 For more information, see [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).  
  
  
##  <a name="AtomRendering"></a> Atom Rendering Rules  
 The Atom rendering extension ignores the following information when rendering a data feed:  
  
-   Formatting and layout  
  
-   Page header  
  
-   Page footer  
  
-   Custom report items  
  
-   Rectangles  
  
-   Lines  
  
-   Images  
  
-   Automatic subtotals  
  
 The remaining report items are sorted, from top to bottom, and then left to right. Each item is then rendered to a column. If the report has nested data items like lists or tables, the parent items are repeated on each row.  
  
 The following table indicates the appearance of report items when rendered:  
  
|Item|Rendering behavior|  
|----------|------------------------|  
|Table|Renders by expanding the table and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns do not have column or row headings. Drillthrough reports are not supported.|  
|Matrix|Renders by expanding the matrix and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns do not have column or row headings.|  
|List|Renders a record for each detail row or instance in the list.|  
|Subreport|The parent item is repeated for each instance of the contents.|  
|Chart|Renders a record with all chart labels for each chart value. Labels from series and categories in hierarchies are flattened and included in the row for a chart value.|  
|Data bar|Renders like a chart. Typically, a data bar does not include hierarchies or labels.|  
|Sparkline|Renders like a chart. Typically, a sparkline does not include hierarchies or labels.|  
|Gauge|Renders as a single record with the minimum and maximum values of the linear scale, start and end values of the range, and the value of the pointer.|  
|Indicator|Renders as a single record with the active state name, available states, and the data value.|  
|Map|Generates a data feed for each map data region. If multiple map layers use the same data region, the data feed includes all of them. The data feed includes a record with the labels and values for each map member of the map layer.|  
  
  
##  <a name="DeviceInfo"></a> Device Information Settings  
 You can change some default settings for this renderer, including the encoding schema to use. For more information, see [ATOM Device Information Settings](../../reporting-services/atom-device-information-settings.md).  

## Next steps

[Exporting to a CSV File](../../reporting-services/report-builder/exporting-to-a-csv-file-report-builder-and-ssrs.md)   
[Export Reports](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
