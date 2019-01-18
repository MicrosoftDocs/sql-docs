---
title: "Exporting Reports (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10437"
ms.assetid: a2bab8c1-505d-4da3-b1db-ea0ae13b2336
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Exporting Reports (Report Builder and SSRS)
  After you run a report, you can export it to another format, such as Excel or PDF, or export the report by generating an Atom service document, listing the Atom-compliant data feeds available from the report.  
  
 Export a report to do the following:  
  
-   Work with the report data in another application. For example, you can export your report to Excel and then continue to work with the data in Excel.  
  
-   Print the report in a different format. For example, you can export the report to the PDF file format and then print it.  
  
-   Save a copy of the report as another file type. For example, you can export a report to Word and save it, creating a copy of the report.  
  
-   Use report data as data feeds in applications. For example, you can generate Atom-compliant data feeds that the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] client can consume, and then work with the data in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)].  
  
 The export option is available on the report viewer toolbar in Report Manager, which appears at the top of every report when you view a report on the report server, and on the ribbon in Report Builder when you preview a report. The data feed option is available only in Report Manager.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides many rendering extensions, supporting exports of reports to common file formats. The rendering extensions support file formats with soft breaks (for example, Word or Excel), hard-page breaks (for example, PDF or TIFF), or data only (for example, CSV or Atom compliant XML).  
  
 To quickly get started with exporting reports and generating Atom-compliant data feeds from reports, see [Export a Report as Another File Type &#40;Report Builder and SSRS&#41;](../export-a-report-as-another-file-type-report-builder-and-ssrs.md) and [Generate Data Feeds from a Report &#40;Report Builder and SSRS&#41;](generate-data-feeds-from-a-report-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="RendererTypes"></a> Rendering Extension Types  
 There are three types of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] rendering extensions:  
  
-   **Data renderer extensions** Data rendering extensions strip all formatting and layout information from the report and display only the data. The resulting file can be used to import the raw report data into another file type, such as Excel, another database, an XML data message, or a custom application. Data renderers do not support page breaks.  
  
     The following data rendering extensions are supported: CSV, XML, and Atom.  
  
-   **Soft page-break renderer extensions** Soft page-break rendering extensions maintain the report layout and formatting. The resulting file is optimized for screen-based viewing and delivery, such as on a Web page or in the **ReportViewer** controls.  
  
     The following soft page-break rendering extensions are supported: [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Word, and Web archive (MHTML).  
  
-   **Hard page-break rendering extensions** Hard page-break renderer extensions maintain the report layout and formatting. The resulting file is optimized for a consistent printing experience, or to view the report online in a book format.  
  
     The following hard page-break rendering extensions are supported: TIFF and PDF.  
  
##  <a name="ExportFormats"></a> Export Formats  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides rendering extensions that render reports in different formats. If you plan to use this feature, you should optimize the report design for your chosen file format. The topic about each rendering extension provides detailed information about how the report is rendered to that format.  
  
 The following table lists the available formats.  
  
|Format|Rendering Extension Type|Description|  
|------------|------------------------------|-----------------|  
|CSV|Data|The Comma-Separated Value (CSV) rendering extension renders reports as a flattened representation of data from a report in a standardized, plain-text format that is easily readable and exchangeable with many applications.<br /><br /> For more information, see [Exporting to a CSV File &#40;Report Builder and SSRS&#41;](exporting-to-a-csv-file-report-builder-and-ssrs.md).|  
|Excel|Soft page-break|The Excel rendering extension renders a report as an Excel document that is compatible with [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2007-2010 as well as [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2003 with the Microsoft Office Compatibility Pack for Word, Excel, and PowerPoint installed. The report is exported to an Excel worksheet with some layout and original design elements stripped out. Properties of the report and groups within the report can be set to enable the naming of worksheet tabs upon export to Excel. The file name extension of files generated by this renderer is xlsx.<br /><br /> For more information, see [Exporting to Microsoft Excel &#40;Report Builder and SSRS&#41;](exporting-to-microsoft-excel-report-builder-and-ssrs.md).<br /><br /> Note: The Excel 2003 rendering extension that renders to the native format of [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2003 is available in some reporting scenarios.|  
|Word|Soft page-break|The Word rendering extension renders a report as a Word document that is compatible with [!INCLUDE[ofprword](../../includes/ofprword-md.md)] 2007-2010 as well as [!INCLUDE[ofprword](../../includes/ofprword-md.md)] 2003 with the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Office Compatibility Pack for Word, Excel, and PowerPoint installed. After the report is exported to a Word document, you can change the contents of your report and design document-style reports such as mailing labels, purchase orders, or form letters. The file name extension of files generated by this renderer is docx.<br /><br /> For more information, see [Exporting to Microsoft Word &#40;Report Builder and SSRS&#41;](exporting-to-microsoft-word-report-builder-and-ssrs.md).<br /><br /> Note: The Word 2003 rendering extension that renders to the native format of [!INCLUDE[ofprword](../../includes/ofprword-md.md)] 2003 is available in some reporting scenarios.|  
|Web archive|Soft page-break|The HTML rendering extension renders a report in HTML format. The rendering extension can also produce fully formed HTML pages or fragments of HTML to embed in other HTML pages. All HTML is generated with UTF-8 encoding.<br /><br /> The HTML rendering extension is the default rendering extension for reports that are previewed in Report Builder and viewed in a browser, including when run in Report Manager.<br /><br /> For more information, see [Rendering to HTML &#40;Report Builder and SSRS&#41;](rendering-to-html-report-builder-and-ssrs.md).|  
|Acrobat (PDF) file|Hard page-break|The PDF rendering extension renders a report to files that can be opened in Adobe Acrobat and other third-party PDF viewers that support PDF 1.3. Although PDF 1.3 is compatible with Adobe Acrobat 4.0 and later, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports Adobe Acrobat 6 or later. The rendering extension does not require Adobe software to render the report. However, PDF viewers such as Adobe Acrobat are required for viewing or printing a report in PDF format.<br /><br /> For more information, see [Exporting to a PDF File &#40;Report Builder and SSRS&#41;](exporting-to-a-pdf-file-report-builder-and-ssrs.md).|  
|TIFF file|Hard page-break|The Image rendering extension renders a report to a bitmap or metafile. By default, the Image rendering extension produces a TIFF file of the report, which can be viewed in multiple pages. When the client receives the image, it can be displayed in an image viewer and printed.<br /><br /> The Image rendering extension can generate files in any of the formats supported by [!INCLUDE[ndptecgdiplus](../../includes/ndptecgdiplus-md.md)]: BMP, EMF, EMFPlus, GIF, JPEG, PNG, and TIFF.<br /><br /> For more information, see [Exporting to an Image File &#40;Report Builder and SSRS&#41;](exporting-to-an-image-file-report-builder-and-ssrs.md).|  
|XML|Data|The XML rendering extension returns a report in XML format. The schema for the report XML is specific to the report, and contains data only. Layout information is not rendered and pagination is not maintained by the XML rendering extension. The XML generated by this extension can be imported into a database, used as an XML data message, or sent to a custom application.<br /><br /> For more information, see [Exporting to XML &#40;Report Builder and SSRS&#41;](exporting-to-xml-report-builder-and-ssrs.md).|  
|Atom|Data|The Atom rendering extension generates Atom-compliant data feeds from reports. The data feeds are readable and exchangeable with applications such as the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] client that can consume Atom-compliant data feeds.<br /><br /> The output is an Atom service document that lists the data feeds available from a report. At least one data feed is created for each data region in a report. Depending on the type of data region and the data that the data region displays, multiple data feeds might be generated.<br /><br /> For more information, see [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](generating-data-feeds-from-reports-report-builder-and-ssrs.md).|  
  
##  <a name="ExportingReport"></a> Exporting a Report  
 To export a report, run the report in Report Manager or Report Builder and then select a format from the Export drop-down list. You are prompted to choose whether to save or open the file. If you chose **Open**, the report opens in the application associated with the rendering format you chose. (For example, when you select **Excel** the report opens in Excel). If you chose **Save**, the report is saved. For example, if you are exporting to Excel, the report is saved as an .xls file. The file associations defined for the local computer determine which application is used for a particular rendering format. For more information, see [Export a Report as Another File Type &#40;Report Builder and SSRS&#41;](../export-a-report-as-another-file-type-report-builder-and-ssrs.md).  
  
 The report server exports the report as it is in the current user session. If someone publishes an updated version of the report while you have the report open or the data that the report displays changes, the exported report is not updated.  
  
 Report pagination might be affected when you export a report to a different format. When you preview a report, you are viewing the report as it is rendered by the HTML rendering extension, which follows soft-page break rules. When you export a report to a different file format, such as Adobe Acrobat (PDF), pagination is based on the physical page size, which follows hard-page break rules. Pages can also be separated by logical page breaks that you add to a report, but the actual length of a page varies based on the renderer type that you use. To change the pagination of your report, you must understand the pagination behavior of the rendering extension you choose. You might need to adjust the design of your report layout for this rendering extension. For more information see, [Page Layout and Rendering &#40;Report Builder and SSRS&#41;](../report-design/page-layout-and-rendering-report-builder-and-ssrs.md).  
  
##  <a name="GeneratingDataFeedsFromReport"></a> Generating Data Feeds From a Report  
 To generate data feeds from a report, run the report in Report Manager, and then click the **Generate Data Feed** icon on the Report Manager toolbar. You are prompted to choose whether to save or open the file. If you chose **Open**, the Atom service document opens in the application that is associated with the .atomsvc file extension. If you chose **Save**, the document is saved as an .atomsvc file. By default, the name of the file is the name of the report. You can change the name to one that is more meaningful.  
  
 You save the Atom service document to your computer. Later you can upload it to a report server or another server to make it available for others to use. For more information, see [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](generating-data-feeds-from-reports-report-builder-and-ssrs.md) and [Generate Data Feeds from a Report &#40;Report Builder and SSRS&#41;](generate-data-feeds-from-a-report-report-builder-and-ssrs.md).  
  
##  <a name="Troubleshooting"></a> Troubleshooting Exported Reports  
 Sometimes your reports look different or do not work the way you want after you export them to a different format. This occurs because certain rules and limitations might apply to the renderer. You can address many limitations by considering them when you create the report. You might need to use a slightly different layout in your report, carefully align items within the report, confine report footers to a single line of text, and so forth.  
  
 If your report contains Unicode text with Arabic numbers, or contains dates in Arabic, the dates and numbers do not render correctly when you export the report to any of the following formats or print the report.  
  
-   PDF  
  
-   Word  
  
-   Excel  
  
-   Image/TIFF  
  
 If you export the report to HTML, the dates and numbers render correctly.  
  
 The topics about specific renderers describe how report items and data regions are rendered as well as the limitations and solutions for each renderer.  
  
-   [Exporting to a CSV File &#40;Report Builder and SSRS&#41;](exporting-to-a-csv-file-report-builder-and-ssrs.md)  
  
-   [Exporting to Microsoft Excel &#40;Report Builder and SSRS&#41;](exporting-to-microsoft-excel-report-builder-and-ssrs.md)  
  
-   [Exporting to Microsoft Word &#40;Report Builder and SSRS&#41;](exporting-to-microsoft-word-report-builder-and-ssrs.md)  
  
-   [Rendering to HTML &#40;Report Builder and SSRS&#41;](rendering-to-html-report-builder-and-ssrs.md)  
  
-   [Exporting to a PDF File &#40;Report Builder and SSRS&#41;](exporting-to-a-pdf-file-report-builder-and-ssrs.md)  
  
-   [Exporting to an Image File &#40;Report Builder and SSRS&#41;](exporting-to-an-image-file-report-builder-and-ssrs.md)  
  
-   [Exporting to XML &#40;Report Builder and SSRS&#41;](exporting-to-xml-report-builder-and-ssrs.md)  
  
-   [Generating Data Feeds from Reports &#40;Report Builder and SSRS&#41;](generating-data-feeds-from-reports-report-builder-and-ssrs.md)  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides additional features to help you create reports that work well in other formats. Page breaks on tablix data regions (table, matrix, and list), groups, and rectangles give you better control of report pagination. Report pages, delimited by page breaks, can have different page names and reset page numbering. By using expressions, the page names and page numbers can be dynamically updated when the report is run. For more information, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../report-design/pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
 In addition, you can use the RenderFormat built-in global to conditionally apply different report layouts for different renderers. For more information, see [Built-in Globals and Users References &#40;Report Builder and SSRS&#41;](../report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md).  
  
##  <a name="OtherWaysExportingReports"></a> Other Ways of Exporting Reports  
 Exporting a report is an on-demand task that you perform when the report is open in Report Manager or Report Builder. If you want to automate an export operation (for example, to export a report to a shared folder as a specific file type on a recurring schedule), create a subscription that delivers the report to a shared folder. For more information, see [File Share Delivery in Reporting Services](../subscriptions/file-share-delivery-in-reporting-services.md).  
  
 Reports previewed in the reporting tools or opened in a browser application such as Report Manager are always first rendered in HTML. You cannot specify a different rendering extension as the default for viewing. You can, however, create a subscription that produces a report in the rendering format you want for subsequent delivery to an e-mail inbox or shared folder. For more information, see [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](../subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md) and [Create, Modify, and Delete a Data-Driven Subscription](../subscriptions/data-driven-subscriptions.md).  
  
 You can also access a report through a URL that specifies a rendering extension as a URL parameter and render the report directly to the specified format without rendering it in HTML first. The following example renders a report in Excel format:  
  
```  
http://<Server Name>/reportserver?/Sales/YearlySalesSummary&rs:Format=Excel&rs:Command=Render  
```  
  
 For more information, see [Export a Report Using URL Access](../export-a-report-using-url-access.md).  
  
## See Also  
 [Controlling Page Breaks, Headings, Columns, and Rows &#40;Report Builder and SSRS&#41;](../report-design/controlling-page-breaks-headings-columns-and-rows-report-builder-and-ssrs.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](finding-viewing-and-managing-reports-report-builder-and-ssrs.md)   
 [Print Reports &#40;Report Builder and SSRS&#41;](print-reports-report-builder-and-ssrs.md)   
 [Saving Reports &#40;Report Builder&#41;](saving-reports-report-builder.md)  
  
  
