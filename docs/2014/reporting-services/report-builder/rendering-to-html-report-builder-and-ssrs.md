---
title: "Rendering to HTML (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "04/26/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: cf559b0a-499a-4d74-b520-b382b87e0b17
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Rendering to HTML (Report Builder and SSRS)
  The HTML rendering extension renders a report in HTML format. The rendering extension can also produce fully formed HTML pages or fragments of HTML to embed in other HTML pages. All HTML is generated with UTF-8 encoding.  
  
 The HTML rendering extension is the default rendering extension for reports that are viewed in a browser, including when run in Report Manager.  
  
 The HTML rendering extension is the default rendering extension for reports that are viewed in a browser, including when run in Report Manager. The HTML rendering extension can render HTML as a fragment or as a full HTML document. If the HTML is a fragment, the `HEAD`, `HTML`, and `BODY` tags of the HTML document are removed. Only the contents of the `BODY` tag are rendered. This is useful for embedding the HTML in the HTML produced by another application.  
  
 In some scenarios, report parameters can be used to launch script injection attacks when rendering reports to HTML. For more information about securing reports, see [Secure Reports and Resources](../security/secure-reports-and-resources.md).  
  
 For more information about browsers, see [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../browser-support-for-reporting-services-and-power-view.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="RenderingMHTML"></a> Rendering in MHTML  
 The HTML rendering extension can also render reports in MHTML (MIME Encapsulation of Aggregate HTML Documents). MHTML extends HTML to embed encoded objects, such as images, in the HTML document. Using the MHTML rendering extension, you can embed resources such as images, documents, or other binary files as MIME structures within the report HTML, into a single file. MHTML reports are also useful for embedding within e-mail messages because all resources are included with the report. Although it is actually the HTML rendering extension that renders MHTML, this functionality may also be referred to as the MHTML rendering extension.  
  
##  <a name="BrowserSupport"></a> Browser Support  
 This rendering extension supports the following browser versions:  
  
-   Internet Explorer 5.5 and later  
  
-   Firefox 1.5 and later  
  
-   Safari 3.0 and later  
  
 Due to cross browser considerations, the rendered report may vary slightly from browser to browser. For example, the text box contains a property called WritingMode. This property is not supported in Firefox.  
  
##  <a name="HTMLSpecificRenderingRules"></a> HTML-Specific Rendering Rules  
 The following HTML-specific rules are applied when rendering:  
  
-   The renderer builds an HTML table structure to contain all of the items in each `ReportItems` collection, if there is more than one.  
  
-   Every item within the table structure occupies a single cell.  
  
-   Empty cells are collapsed together as much as possible to reduce the size of the HTML.  
  
-   A row of empty cells is added to the top edge and another column to the left edge to improve the speed at which browsers can render the table.  
  
-   Table rows or columns that contain no items, just gaps between items, are given fixed widths and heights.  
  
-   All other rows and columns are allowed to grow depending on the size of each report item.  
  
-   All coordinates and report item sizes are converted to millimeters. All other sizes, including style properties, retain their original units. Size and position differences smaller than .2mm are treated as 0mm.  
  
##  <a name="Interactivity"></a> Interactivity  
 Some interactive elements are supported in HTML. The following is a description of specific behaviors.  
  
### Show and Hide  
 A report item whose visibility can be toggled is rendered with a +/- toggle image and is clickable. When the item is clicked, a call back to the server takes place in order to re-render the output with the changed show or hide state.  
  
### Document Map  
 Document map labels are rendered and can be navigated to by using the document map in the viewer control. For omitted data region headers, labels are rendered on the first child cell. If there is no child cell present, the label is rendered on the child that precedes it.  
  
### Bookmarks  
 Bookmark links are rendered and appear as hyperlinks. Bookmark targets are rendered and can be navigated to by clicking the bookmark links. When a bookmark link is clicked, the report goes to the first occurrence of the target bookmark label and, when possible, the browser is scrolled so that the bookmark link is at the top of the window. HTML anchor (\<a>) tags are used to mark bookmark targets.  
  
### Interactive Sorting  
 If a text box has user sort defined, the HTML rendering extension renders the sort icons in the text box to the right of its contents. If a report contains any text box where user sort is defined, JavaScript is rendered that causes a postback to the server when the sort image is clicked.  
  
### Hyperlinks and Drillthrough  
 Hyperlinks and drillthrough links are rendered as hyperlinks on report items using the HTML anchor (\<a>) tags around the item on which they are defined.  
  
### Search  
 The Search feature allows users to search for a string of text within the report.  
  
 Additional search and find functionality is provided by the ReportViewer Web Forms control.  
  
##  <a name="DeviceInfo"></a> Device Information Settings  
 You can change some default settings for this renderer, including which mode to render in, by changing the device information settings. For more information, see [HTML Device Information Settings](../html-device-information-settings.md).  

## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../report-design/rendering-behaviors-report-builder-and-ssrs.md)   
 [Interactive Functionality for Different Report Rendering Extensions &#40;Report Builder and SSRS&#41;](interactive-functionality-different-report-rendering-extensions.md)   
 [Rendering Report Items &#40;Report Builder and SSRS&#41;](../report-design/rendering-report-items-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)  
