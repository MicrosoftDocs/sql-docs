---
title: "Render to HTML (Report Builder)"
description: In Report Builder, the HTML rendering extension renders paginated reports in HTML format. It can produce full HTML pages or fragments to embed in other pages.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/15/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Render to HTML (Report Builder)

  The HTML rendering extension renders a paginated report in HTML format. The rendering extension can also produce fully formed HTML pages or fragments of HTML to embed in other HTML pages. All HTML is generated with UTF-8 encoding.

The HTML rendering extension is the default rendering extension for reports that are viewed in a browser, including when run in the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] web portal. The HTML rendering extension can render HTML as a fragment or as a full HTML document. If the HTML is a fragment, the **HEAD**, **HTML**, and **BODY** tags of the HTML document are removed. Only the contents of the **BODY** tag are rendered. This result is useful for embedding the HTML in the HTML produced by another application.

In some scenarios, report parameters can be used to launch script injection attacks when rendering reports to HTML. For more information about securing reports, see [Secure reports and resources](../../reporting-services/security/secure-reports-and-resources.md).

For more information about browsers, see [Browser support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## <a id="RenderingMHTML"></a> Render in MHTML

The HTML rendering extension can also render reports in MHTML (MIME Encapsulation of Aggregate HTML Documents). MHTML extends HTML to embed encoded objects, such as images, in the HTML document. Using the MHTML rendering extension, you can embed resources such as images, documents, or other binary files as MIME structures within the report HTML, into a single file. MHTML reports are also useful for embedding within e-mail messages because all resources are included with the report. Although it's actually the HTML rendering extension that renders MHTML, this functionality might also be referred to as the MHTML rendering extension.

## <a id="BrowserSupport"></a> Browser support

This rendering extension supports the following browser versions:

- Internet Explorer 5.5 and later

- Firefox 1.5 and later

- Safari 3.0 and later

Due to cross browser considerations, the rendered report might vary slightly from browser to browser. For example, the text box contains a property called WritingMode. This property isn't supported in Firefox.

## <a id="HTMLSpecificRenderingRules"></a> HTML-specific rendering rules

The following HTML-specific rules are applied when rendering:

- The renderer builds an HTML table structure to contain all of the items in each **ReportItems** collection, if there's more than one.

- Every item within the table structure occupies a single cell.

- Empty cells are collapsed together as much as possible to reduce the size of the HTML.

- A row of empty cells is added to the top edge and another column to the left edge to improve the speed at which browsers can render the table.

- Table rows or columns that contain no items, just gaps between items, are given fixed widths and heights.

- All other rows and columns are allowed to grow depending on the size of each report item.

- All coordinates and report item sizes are converted to millimeters. All other sizes, including style properties, retain their original units. Size and position differences smaller than 0.2 mm are treated as 0 mm.

## <a id="Interactivity"></a> Interactivity

Some interactive elements are supported in HTML. The following section is a description of specific behaviors.

### Show and hide

A report item whose visibility can be toggled is rendered with a +/- toggle image and is selectable. When the item is selected, a callback to the server takes place in order to re-render the output with the changed show or hide state.

### Document map

Document map labels are rendered and can be navigated to by using the document map in the viewer control. For omitted data region headers, labels are rendered on the first child cell. If there's no child cell present, the label is rendered on the child that precedes it.

### Bookmarks

Bookmark links are rendered and appear as hyperlinks. Bookmark targets are rendered and can be navigated to by selecting the bookmark links. When a bookmark link is selected, the report goes to the first occurrence of the target bookmark label. Then, when possible, the browser is scrolled so that the bookmark link is at the top of the window. HTML anchor (\<a>) tags are used to mark bookmark targets.

### Interactive sort

If a text box has user sort defined, the HTML rendering extension renders the sort icons in the text box to the right of its contents. If a report contains any text box where user sort is defined, JavaScript is rendered. The JavaScript causes a postback to the server when the sort image is selected.

### Hyperlinks and Drillthrough

Hyperlinks and drillthrough links are rendered as hyperlinks on report items using the HTML anchor (`<a>`) tags around the item on which they're defined.

### Search

The Search feature allows users to search for a string of text within the report.

More search and find functionality is provided by the ReportViewer Web Forms control.

## <a id="FontsOnClient"></a> Fonts on the client computer

When a custom font is used within the report, the computer that is used to view the report (the client computer) must have the custom font installed for the report to display correctly. If the font isn't installed on the client computer, the report displays a system default font instead of the custom font.

## <a id="DeviceInfo"></a> Device information settings

You can change some default settings for this renderer, including which mode to render in, by changing the device information settings. For more information, see [HTML device information settings](../../reporting-services/html-device-information-settings.md).

## Related content

- [Pagination in Reporting Services (Report Builder)](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)
- [Renderer behaviors (Report Builder)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)
- [Interactive functionality for different report rendering extensions (Report Builder)](../../reporting-services/report-builder/interactive-functionality-different-report-rendering-extensions.md)
- [Render report items (Report Builder)](../../reporting-services/report-design/rendering-report-items-report-builder-and-ssrs.md)
- [Tables, matrices, and lists (Report Builder)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
