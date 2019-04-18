---
title: "Page Layout and Rendering (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: e2358653-35bc-4496-810a-d3ccf02f229f
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Page Layout and Rendering (Report Builder and SSRS)
  When you author reports it is important to understand the behavior of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] renderers to ensure the rendered report looks the way you want, including page layout and page breaks. You likely also want to make sure the rendered report fits on the paper size that you or your organization commonly uses.  
  
 When you view reports in Report Manager or the preview pane of Report Builder or Report Designer, the report in first rendered by the HTML renderer. You can then export the report to different formats such as Excel or Comma Separated File (CSV). The exported report can then be used for further analysis in Excel or as data source for applications that can import and use CSV data files.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a set of renderers for exporting reports to different formats. Each renderer has applies rules when rendering reports. When you export a report to a different file format, especially for renderers such as the Adobe Acrobat (PDF) renderer that uses pagination based on the physical page size, you might need to change the layout of your report to have the exported report look and print correctly after the rendering rules are applied.  
  
 Getting the best results for exported reports is often an iterative process; you author and preview the report in Report Builder or Report Designer, export the report to the preferred format, review the exported report, and then make changes to the report.  
  
 This topic provides information about the Reporting Services rendering extensions and how to work with them.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="PageLayout"></a> Page Layout and Report Items  
 Report items are layout elements that are associated with different types of report data. Table, Matrix, List, Chart, and Gauge are data region report items that each link to a report dataset. When the report is processed, the data region expands across and down the report page to display data. Other report items link to and display a single item. An **Image** report item links to a picture. A **Text Box** report item contains either simple text like a title or an expression that can include references to built-in fields, report parameters, or dataset fields. The **Line** and **Rectangle** report items provide simple graphical elements on the report page. The **Rectangle** can also be a container for other report items. A report can contain subreports.  
  
 With [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can place report items anywhere on the design surface. You can interactively position, expand, and contract the initial shape of the report item using snap lines and resizing handles. You can place data regions with different sets of data, or even the same data in different formats, side-by-side. When you place a report item on the design surface, it has a default size and shape and an initial relationship to all other report items. You can place many report items each other to create more complex report designs. For example, charts or images in table cells, tables in table cells, and multiple images in a rectangle. In addition to providing the organization and look you want in the report, placing report items in containers such as rectangles help control the way the report items are displayed on the report page.  
  
 A report can span multiple pages, with a page header and page footer that are repeated on each page. A report can contain graphical elements such as images and lines, and it can have multiple fonts, colors, and styles, which can be based on expressions.  
  
##  <a name="ReportSections"></a> Report Sections  
 A report consists of three main sections: an optional page header, an optional page footer, and a report body. The report header and footer are not separate sections of the report, but rather are comprised of the report items that are placed at the top and bottom of the report body. The page header and page footer repeat the same content at the top and bottom of each page of the report. You can place images, text boxes, and lines in headers and footers. You can place all types of report items in the report body.  
  
 You can set properties on report items to initially hide or show them on the page. You can set visibility properties on rows or columns or groups for data regions and provide toggle buttons to allow the user to interactively show or hide report data. You can set visibility or initial visibility by using expressions, including expressions based on report parameters.  
  
 When a report is processed, report data is combined with the report layout elements and the combined data is sent to a report renderer. The renderer follows predefined rules for report item expansion and determines how much data fits on each page. To design an easy-to-read report that is optimized for the renderer that you plan to use, you should understand the rules used to control pagination in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For more information, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
##  <a name="RenderingExtensions"></a> Renderers  
 Reporting Services includes a set of renderers, also referred to as rendering extensions, that you can use to export reports to different formats. There are three types of renderers:  
  
-   **Data renderers** Data renderers strip all formatting and layout information from the report and display only the data. The resulting file can be used to import the raw report data into another file type, such as Excel, or another database, an XML data message, or a custom application. The available data renders are: CSV and XML.  
  
    > [!NOTE]  
    >  Although it does not provide direct export to a different format, Atom rendering generates data files from reports.  
  
-   **Soft page-break renderers** Soft page-break renderers maintain the report layout and formatting. The resulting file is optimized for screen-based viewing and delivery, such as on a Web page. The available soft page-break renderers are: [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Word, Web archive (MHTML), and HTML.  
  
-   **Hard page-break renderers** Hard page-break renderers maintain the report layout and formatting. The resulting file is optimized for a consistent printing experience, or to view the report online in a book format. The available hard page-break renderers are supported: TIFF and PDF.  
  
 When you preview  a report in Report Builder or Report Designer or run a report in Report Manager, the report is always first rendered in HTML. After you run the report, you can export it to different file formats. For more information, see [Exporting Reports &#40;Report Builder and SSRS&#41;](../report-builder/export-reports-report-builder-and-ssrs.md).  
  
  
  
##  <a name="RenderingBehaviors"></a> Rendering Behaviors  
 Depending on the renderer you select, certain rules are applied when rendering the report. How report items fit together on a page is determined by the combination of these factors:  
  
-   Rendering rules.  
  
-   The width and height of report items.  
  
-   The size of the report body.  
  
-   The width and height of the page.  
  
-   Renderer-specific support for paging.  
  
 For example, reports rendered to HTML and MHTML formats are optimized for a computer screen-based experience where pages can be various lengths.  
  
 For more information, see [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](rendering-behaviors-report-builder-and-ssrs.md).  
  
  
  
##  <a name="Pagination"></a> Pagination  
 Pagination refers to the number of pages within a report and how report items are arranged on these pages. Pagination in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] varies depending on the rendering extension you use to view and deliver the report and the page break and keep-together options you configure the report to use.  
  
 To successfully design an easy-to-read report for your users that is optimized for the renderer that you plan to use to deliver your report, you need to understand the rules used to control pagination in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Reports exported by using the data and soft page rendering extensions are typically not affected by pagination. When you use a data rendering extension the report is rendered as tabular rowset in an XML or CSV format. To ensure the exported report data is usable you should understand the rules applied to rendered a flattened tabular rowset from a report.  
  
 When you use a soft page rendering extension such as the HTML rendering extension, you might want to know how the report looks printed and also how well it renders using a hard page renderer such as PDF. During the creation or updating of a report you can preview and export it in Report Builder and Report Designer.  
  
 The hard page renderers have the most impact on report layout and physical page size. To learn more, see [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](pagination-in-reporting-services-report-builder-and-ssrs.md).  
  
  
  
##  <a name="HowTo"></a> How-To Topics  
 This section lists procedures that show you, step by step, how to work with pagination in reports.  
  
-   [Add a Page Break &#40;Report Builder and SSRS&#41;](add-a-page-break-report-builder-and-ssrs.md)  
  
-   [Display Row and Column Headers on Multiple Pages &#40;Report Builder and SSRS&#41;](display-row-and-column-headers-on-multiple-pages-report-builder-and-ssrs.md)  
  
-   [Add or Remove a Page Header or Footer &#40;Report Builder and SSRS&#41;](add-or-remove-a-page-header-or-footer-report-builder-and-ssrs.md)  
  
-   [Keep Headers Visible When Scrolling Through a Report &#40;Report Builder and SSRS&#41;](keep-headers-visible-when-scrolling-through-a-report-report-builder-and-ssrs.md)  
  
-   [Display Page Numbers or Other Report Properties &#40;Report Builder and SSRS&#41;](display-page-numbers-or-other-report-properties-report-builder-and-ssrs.md)  
  
-   [Hide a Page Header or Footer on the First or Last Page &#40;Report Builder and SSRS&#41;](hide-a-page-header-or-footer-on-the-first-or-last-page-report-builder-and-ssrs.md)  
  
  
  
##  <a name="InThisSection"></a> In This Section  
 The following topics provide additional information about page layout and rendering.  
  
 [Page Headers and Footers &#40;Report Builder and SSRS&#41;](page-headers-and-footers-report-builder-and-ssrs.md)  
 Provides information about using headers and footers in reports and how to control pagination using them.  
  
 [Controlling Page Breaks, Headings, Columns, and Rows &#40;Report Builder and SSRS&#41;](controlling-page-breaks-headings-columns-and-rows-report-builder-and-ssrs.md)  
 Provides information about using page breaks.  
  
  
  
## See Also  
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](../report-builder/interactive-functionality-different-report-rendering-extensions.md)   
 [Exporting Reports &#40;Report Builder and SSRS&#41;](../report-builder/export-reports-report-builder-and-ssrs.md)  
  
  
