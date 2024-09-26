---
title: "Export paginated reports (Report Builder)"
description: You can export a Reporting Services report to another file format, like PowerPoint or PDF, using Report Builder, Report Designer, or the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---

# Export paginated reports (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can export a paginated report to another file format, such as PowerPoint, Image, PDF, [Accessible PDF](/power-bi/report-server/rendering-extension-support), [!INCLUDE[ofprword](../../includes/ofprword-md.md)], or [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)]. Or, you can export the report by generating an Atom service document, listing the Atom-compliant data feeds available from the report. You can export your report from Report Builder, Report Designer ([!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]), or the report server.

Export a report to do the following actions:

- **Work with the report data in another application.** For example, you can export your report to Excel and then continue to work with the data in Excel.

- **Print the report in a different format.** For example, you can export the report to the PDF file format and then print it.

- **Save a copy of the report as another file type.** For example, you can export a report to Word and save it, creating a copy of the report.

- **Use report data as data feeds in applications.** For example, you can generate Atom-compliant data feeds that Power Pivot or Power BI can consume, and then work with the data in Power Pivot or Power BI. For more information, see [Generate data feeds from a report](../../reporting-services/report-builder/generate-data-feeds-from-a-report-report-builder-and-ssrs.md)

- Rendering the report on the report server is useful when you set up subscriptions or when you deliver your reports via e-mail. Or, you can choose to save a report that is available on the report server. For more information, see [Subscriptions and delivery (Reporting Services)](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md).

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides many rendering extensions, supporting exports of reports to common file formats. The rendering extensions support file formats with soft breaks (for example, Word or Excel), hard-page breaks (for example, PDF or TIFF), or data only (for example, CSV or Atom compliant XML).

Report pagination might be affected when you export a report to a different format. When you preview a report, you  see the report as the HTML rendering extension renders it, which follows soft-page break rules. When you export a report to a different file format, such as Adobe Acrobat (PDF), pagination is based on the physical page size, which follows hard-page break rules. You can also separate pages with logical page breaks that you add to a report. But, the actual length of a page varies based on the renderer type that you use. To change the pagination of your report, you must understand the pagination behavior of the rendering extension you choose. You might need to adjust the design of your report layout for this rendering extension. For more information, see, [Page layout and rendering](../../reporting-services/report-design/page-layout-and-rendering-report-builder-and-ssrs.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## <a id="bkmk_export_from_rb"></a> Export a report from Report Builder

1. Run or Preview the report.

1. On the ribbon, select **Export**.

     :::image type="content" source="media/export-reports-report-builder-and-ssrs/ssrb-export.png" alt-text="Screenshot of the Export button in Report Builder.":::

1. Select the format that you want to use.

     The **Save As** dialog opens. By default, the file name is that of the report that you exported. Optionally, you can change the file name.

## <a id="bkmk_export_from_rm"></a> Export a report from the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal

1. From the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal **Home** page, navigate to the report that you want to export.

1. Select the report to render and preview the report.

1. On the Report Viewer toolbar, select the **Export** list arrow.

     :::image type="content" source="media/export-reports-report-builder-and-ssrs/ssrsportal-export.png" alt-text="Screenshot showing the Reporting Services web portal Export list.":::

1. Select the format that you want to use.

1. Select **Export**. A dialog appears asking you if you want to open or save the file.

1. To view the report in the selected export format, select **Open**.

     \- or -

     To immediately save the report in the selected export format, select **Save**.

     By using the application associated with the format that you chose, the report is either displayed or saved. If you select **Save**, you're prompted for a location where you can save your report.

## <a id="bkmk_export_from_sharepoint"></a> Export a report from a SharePoint library

1. Preview the report.

1. On the toolbar, select **Actions**, point to **Export**, and then select the format that you want to use.

     The **File Download** dialog opens.

1. To view the report in the selected export format, select **Open**.

     \- or -

     To immediately save the report in the selected export format, select **Save**.

     Using the application that is associated with the format that you chose, the report is either displayed or saved. If you select **Save**, you're prompted for a location where you can save your report.

     Optionally, change the file name of the exported report.

     > [!NOTE]  
     >  If the program can't open the report in the format that you chose because you don't have a program associated with this file type, you are prompted to save the exported report or to find a program online to open the report.

## <a id="RendererTypes"></a> Render extension types

There are three types of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] rendering extensions:

- **Data renderer extensions**: Data rendering extensions strip all formatting and layout information from the report and display only the data. The resulting file can be used to import the raw report data into another file type. For example, you can import another file type such as Excel, another database, an XML data message, or a custom application. Data renderers don't support page breaks.

     The following data rendering extensions are supported: CSV, XML, and Atom.

- **Soft page-break renderer extensions**: Soft page-break rendering extensions maintain the report layout and formatting. The resulting file is optimized for screen-based viewing and delivery, such as on a Web page or in the **ReportViewer** controls.

     The following soft page-break rendering extensions are supported: [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Word, and Web archive (MHTML).

- **Hard page-break rendering extensions**: Hard page-break renderer extensions maintain the report layout and formatting. The resulting file is optimized for a consistent printing experience, or to view the report online in a book format.

     The following hard page-break rendering extensions are supported: TIFF and PDF.

## <a id="ExportFormats"></a> Formats you can export while viewing reports

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides rendering extensions that render reports in different formats. You should optimize the report design for your chosen file format.  The following table lists the formats you can export from the user interface.  There are other formats you can use with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions or if you're exporting from URL access. For more information, see the [Other ways of exporting reports](#OtherWaysExportingReports) section in this article.

| Format | Rendering Extension Type | Description |
| --- | --- | --- |
| Acrobat (PDF) file | Hard page-break | The PDF rendering extension renders a report to files that can be opened in Adobe Acrobat and other non-Microsoft PDF viewers that support PDF 1.3. Although PDF 1.3 is compatible with Adobe Acrobat 4.0 and later, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports Adobe Acrobat 6 or later. The rendering extension doesn't require Adobe software to render the report. However, PDF viewers such as Adobe Acrobat are required for viewing or printing a report in PDF format.<br /><br />For more information, see [Export to a PDF file](../../reporting-services/report-builder/exporting-to-a-pdf-file-report-builder-and-ssrs.md). |
| Atom | Data | The Atom rendering extension generates Atom-compliant data feeds from reports. The data feeds are readable and exchangeable with applications such as Power Pivot and Power BI, both of which can consume Atom-compliant data feeds.<br /><br />The output is an Atom service document that lists the data feeds available from a report. At least one data feed is created for each data region in a report. Depending on the type of data region and the data that the data region displays, multiple data feeds might be generated.<br /><br />For more information, see [Generate data feeds from reports](../../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md). |
| CSV | Data | The Comma-Separated Value (CSV) rendering extension renders reports as a flattened representation of data from a report in a standardized, plain-text format that is easily readable and exchangeable with many applications.<br /><br />For more information, see [Export to a CSV file](../../reporting-services/report-builder/exporting-to-a-csv-file-report-builder-and-ssrs.md). |
| EXCELOPENXML | Soft page-break | Displayed as **Excel** in the export menus when reviewing reports. The Excel rendering extension renders a report as an Excel document (.xlsx) that is compatible with [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2013. For more information, see [Export to Microsoft Excel](../../reporting-services/report-builder/exporting-to-microsoft-excel-report-builder-and-ssrs.md). |
| PowerPoint | Hard page-break | The PowerPoint rendering extension renders a report as a PowerPoint document (.pptx) that is compatible with PowerPoint 2013. |
| TIFF file | Hard page-break | The Image rendering extension renders a report to a bitmap or metafile. By default, the Image rendering extension produces a TIFF file of the report, which can be viewed in multiple pages. When the client receives the image, it can be displayed in an image viewer and printed.<br /><br />The Image rendering extension can generate files in any of the formats supported by GDI+: BMP, EMF, EMFPlus, GIF, JPEG, PNG, and TIFF.<br /><br />For more information, see [Export to an image file](../../reporting-services/report-builder/exporting-to-an-image-file-report-builder-and-ssrs.md). |
| Web archive | Soft page-break | The HTML rendering extension renders a report in HTML format. The rendering extension can also produce fully formed HTML pages or fragments of HTML to embed in other HTML pages. All HTML is generated with UTF-8 encoding.<br /><br />The HTML rendering extension is the default rendering extension for reports that are previewed in Report Builder and viewed in a browser, including when run in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal.<br /><br />For more information, see [Render to HTML](../../reporting-services/report-builder/rendering-to-html-report-builder-and-ssrs.md). |
| WORDOPENXML | Soft page-break | Displayed as **Word** in the export menu when viewing reports. The Word rendering extension renders a report as a Word document (.docx) that is compatible with [!INCLUDE[ofprword](../../includes/ofprword-md.md)] 2013. For more information, see [Export to Microsoft Word](../../reporting-services/report-builder/exporting-to-microsoft-word-report-builder-and-ssrs.md). |
| XML | Data | The XML rendering extension returns a report in XML format. The schema for the report XML is specific to the report, and contains data only. The XML rendering extension doesn't render layout information and it doesn't maintain pagination. The XML generated by this extension can be imported into a database, used as an XML data message, or sent to a custom application.<br /><br />For more information, see [Export to XML](../../reporting-services/report-builder/exporting-to-xml-report-builder-and-ssrs.md). |

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides other features to help you create reports that work well in other formats. Page breaks on tablix data regions (table, matrix, and list), groups, and rectangles give you better control of report pagination. Report pages, delimited by page breaks, can have different page names and reset page numbering. By using expressions, the page names and page numbers can be dynamically updated when the report is run. For more information, see [Pagination in Reporting Services](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md).

In addition, you can use the RenderFormat built-in global to conditionally apply different report layouts for different renderers. For more information, see [Built-in globals and users references](../../reporting-services/report-design/built-in-collections-built-in-globals-and-users-references-report-builder.md)

## <a id="GeneratingDataFeedsFromReport"></a> Generate data feeds from a report

To generate data feeds from a report, run the report in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, and then select the **Generate Data Feed** icon on the web portal toolbar. You're prompted to choose whether to save or open the file. If you chose **Open**, the Atom service document opens in the application associated with the .atomsvc file extension. If you chose **Save**, the document is saved as an .atomsvc file. By default, the name of the file is the name of the report. You can change the name to one that's more meaningful.

You save the Atom service document to your computer. Later you can upload it to a report server or another server to make it available for others to use. For more information, see [Generate data feeds from reports](../../reporting-services/report-builder/generating-data-feeds-from-reports-report-builder-and-ssrs.md) and [Generate data feeds from a report](../../reporting-services/report-builder/generate-data-feeds-from-a-report-report-builder-and-ssrs.md).

## <a id="Troubleshooting"></a> Troubleshoot exported reports

Sometimes your reports look different or don't work the way you want after you export them to a different format. This result occurs because certain rules and limitations might apply to the renderer. You can address many limitations by considering them when you create the report. You might need to use a slightly different layout in your report, carefully align items within the report, confine report footers to a single line of text, and so forth.

### Arabic numbers and dates

If your report contains Unicode text with Arabic numbers or dates in Arabic, the dates and numbers *don't* render correctly. This issue occurs when you export the report to any of the following formats or print the report.

- PDF
- Word
- Excel
- Image/TIFF

If you export the report to HTML, the dates and numbers *do* render correctly.

### Export reports with embedded or external images

When you export a paginated report in PowerPoint format, if the report has an embedded or external image in the background, the image doesn't come up in the exported file. The same is true when you export to other formats like Word.

## <a id="OtherWaysExportingReports"></a> Other ways of exporting reports

Exporting a report is an on-demand task that you perform when the report is open in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal or Report Builder. If you want to automate an export operation, create a subscription that delivers the report to a shared folder. For example, you can export a report to a shared folder as a specific file type on a recurring schedule. For more information, see [File share delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md).

Reports previewed in the reporting tools or opened in a browser application such as the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal are always first rendered in HTML. You can't specify a different rendering extension as the default for viewing. You can, however, create a subscription that produces a report in the rendering format you want for subsequent delivery to an e-mail inbox or shared folder. For more information, see [Create and manage subscriptions for native mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md) and [Create, modify, and delete data-driven subscriptions](../../reporting-services/subscriptions/create-modify-and-delete-data-driven-subscriptions.md).

You can also access a report through a URL that specifies a rendering extension as a URL parameter and render the report directly to the specified format without rendering it in HTML first. The following example renders a report in Excel format:

```
https://<Report Server Name>/reportserver?/Sales/YearlySalesSummary&rs:Format=Excel&rs:Command=Render
```

and the following example renders a PowerPoint report from a named instance:

```
https://<Report Server Name/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx
```

For more information, see [Export a report by using URL access](../../reporting-services/export-a-report-using-url-access.md).

## Related content

- [Control page breaks, headings, columns, and rows (Report Builder and SSRS)](../../reporting-services/report-design/controlling-page-breaks-headings-columns-and-rows-report-builder-and-ssrs.md)
- [Find, view, and manage reports (Report Builder and SSRS )](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
- [Print reports (Report Builder and SSRS)](../../reporting-services/report-builder/print-reports-report-builder-and-ssrs.md)
- [Save reports (Report Builder)](../../reporting-services/report-builder/saving-reports-report-builder.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
