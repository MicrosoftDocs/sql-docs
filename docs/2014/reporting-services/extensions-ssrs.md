---
title: Extensions
author: markingmyname
ms.author: maghan
manager: kfile
ms.reviewer: ""
ms.prod: reporting-services-2014, sql-server-2014
ms.prod_service: reporting-services-native, reporting-services-sharepoint 
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/14/2018
---

# Extensions for SQL Server Reporting Services (SSRS)

  The report server in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] uses extensions to modularize the types of input or output it accepts for authentication, data processing, report rendering, and report delivery. This makes it easy for existing [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] installations to utilize new software standards in the industry, such as a new authentication scheme, or a custom data source type. The report server supports custom authentication extensions, data processing extensions, report processing extensions, rendering extensions, and delivery extensions, and the extensions that are available to the users are configurable in the RSReportServer.config configuration file. For example, you can limit the export formats the report viewer is allowed to use. A report server requires at least one authentication extension, data processing extension, and rendering extension. Delivery and report processing extensions are optional, but necessary if you want to support report distribution or custom controls.  
  
 This topic describes the extensions that are readily available in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
## Security Extensions

 Security extensions are used to authenticate and authorize users and groups to a report server. The default security extension is based on Windows Authentication. You can also create a custom security extension to replace default security if your deployment model requires a different authentication approach (for example, if you require forms-based authentication for Internet or extranet deployment). Only one security extension can be used in a single [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] installation. You can replace the default Windows Authentication security extension, but you cannot use it alongside a custom security extension.  
  
## Data Processing Extensions

 Data Processing extensions are used to query a data source and return a flattened row set. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] uses different extensions to interact with different types of data sources. You can use the extensions that are included in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], or you can develop your own extensions. Data processing extensions for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], Oracle, [!INCLUDE[SAP_DPE_BW_1](../includes/sap-dpe-bw-1-md.md)], Hyperion Essbase, Teradata, OLE DB, and ODBC data sources are provided. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] can also use any [!INCLUDE[vstecado](../includes/vstecado-md.md)] data provider. Data processing extensions process query requests from the Report Processor component by performing the following tasks:  
  
- Open a connection to a data source.  
  
- Analyze a query and return a list of field names.  
  
- Run a query against the data source and return a rowset.  
  
- Pass parameters to a query, if required.  
  
- Iterate through the rowset and retrieve data.  
  
Some extensions can also perform the following tasks:  
  
- Analyze a query and return a list of parameter names used in the query.  
  
- Analyze a query and return the list of fields used for grouping.  
  
- Analyze a query and return the list of fields used for sorting.  
  
- Provide a user name and password to connect to the data source.  
  
- Pass parameters with multiple values to a query.  
  
- Iterate through rows and retrieve auxiliary metadata.  
  
## Rendering Extensions

 Rendering extensions transform data and layout information from the Report Processor into a device-specific format. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] includes seven rendering extensions: HTML, Excel, CSV, XML, Image, PDF, and [!INCLUDE[msCoName](../includes/msconame-md.md)] Word.  
  
- **HTML Rendering Extension** When you request a report from a report server through a Web browser, the report server uses the HTML rendering extension to render the report. The HTML rendering extension generates all HTML using UTF-8 encoding. For more information, see [Rendering to HTML &#40;Report Builder and SSRS&#41;](report-builder/rendering-to-html-report-builder-and-ssrs.md) and [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../../2014/reporting-services/browser-support-for-reporting-services-and-power-view.md).  
  
- **Excel Rendering Extension** The Excel rendering extension renders reports that can be viewed and modified in [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] 97 or later. This rendering extension creates files in Binary Interchange File Format (BIFF). BIFF is the native file format for Excel data. Reports that are rendered in [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)] support all of the features available for any spreadsheet. For more information, see [Exporting to Microsoft Excel &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-microsoft-excel-report-builder-and-ssrs.md).  
  
- **CSV Rendering Extension** The Comma-Separated Value (CSV) rendering extension renders reports in comma-delimited plain text files, without any formatting. Users can then open these files with a spreadsheet application, such as [!INCLUDE[ofprexcel](../includes/ofprexcel-md.md)], or any other program that reads text files. For more information, see [Exporting to a CSV File &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-a-csv-file-report-builder-and-ssrs.md).  
  
- **XML Rendering Extension** The XML rendering extension renders reports in XML files. These XML files can then be stored or read by other programs. You can also use an XSLT transformation to turn the report into another XML schema for use by another application. The XML generated by the XML rendering extension is UTF-8 encoded. For more information, see [Exporting to XML &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-xml-report-builder-and-ssrs.md).  
  
-   **Image Rendering Extension** The Image rendering extension renders reports to bitmaps or metafiles. The extension can render reports in the following formats: BMP, EMF, GIF, JPEG, PNG, TIFF, and WMF. By default, the image is rendered in TIFF format, which can be displayed with the default image viewer of your operating system (for example, Windows Picture and Fax Viewer). You can send the image to a printer from the viewer. Using the Image rendering extension to render reports ensures that the report looks the same on every client. (When a user views a report in HTML, the appearance of that report can vary depending on the version of the user's browser, the user's browser settings, and the fonts that are available.) The Image rendering extension renders the report on the server, so all users see the same image. Because the report is rendered on the server, all fonts that are used in the report must be installed on the server. For more information, see [Exporting to an Image File &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-an-image-file-report-builder-and-ssrs.md).  
  
- **PDF Rendering Extension** The PDF rendering extension renders reports in PDF files that can be opened and viewed with Adobe Acrobat 6.0 or later. For more information, see [Exporting to a PDF File &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-a-pdf-file-report-builder-and-ssrs.md).  
  
- **Word Rendering Extension** The [!INCLUDE[msCoName](../includes/msconame-md.md)] Word rendering extension renders a report as a Word document that is compatible with [!INCLUDE[msCoName](../includes/msconame-md.md)] Office Word 2000 or later. For more information, see [Exporting to Microsoft Word &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-microsoft-word-report-builder-and-ssrs.md).  
  
## Report Processing Extensions

 Report processing extensions can be added to provide custom report processing for report items that are not included with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. By default, a report server can process tables, charts, matrices, lists, text boxes, images, and all other report items. If you want to add special features to a report that require custom processing during report execution (for example, if you want to embed a [!INCLUDE[msCoName](../includes/msconame-md.md)] MapPoint map), you can create a report processing extension to do so.  
  
## Delivery Extensions

 The background processing application uses delivery extensions to deliver reports to various locations. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] includes an e-mail delivery extension and a file share delivery extension. The e-mail delivery extension sends an e-mail message through Simple Mail Transport Protocol (SMTP) that includes either the report itself or a URL link to the report. Short notices without the URL link or report can also be sent to pagers, phones, or other devices. The file share delivery extension saves reports to a shared folder on your network. You can specify a location, rendering format, and file name, and overwrite options for the file you create. You can use file share delivery for archiving rendered reports and as part of a strategy for working with very large reports. Delivery extensions work in conjunction with subscriptions. When a user creates a subscription, the user chooses one of the available delivery extensions to determine how the report is delivered.