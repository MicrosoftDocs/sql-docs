---
title: "Interactive functionality - different report rendering extensions"
description: Reporting Services allows you to interact with a paginated report at runtime. This article describes how the interactive features work in specific formats.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Interactive functionality - different report rendering extensions

  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides features for interacting with a paginated report at run time. Not all of the report rendering formats support the full range of interactive features. Use the following table to understand how each interactive feature works in specific formats.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## Interactive features in different output formats

Paginated reports that are rendered to XML, CSV, or Image formats don't support interactive features.

Reports that you view in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal, SharePoint Web parts, or in a browser are rendered in HTML. HTML and Windows Forms are the only report output formats that fully support all interactive features. Alternate HTML formats (such as MHTML) support many interactive features. If exceptions for MHTML exist, they're noted in the following table.

### Document maps

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | Interactive document map provides a navigation pane of hierarchical links that can be used to navigate to different sections of a report. |
| PDF | PDF renders a document map as the Bookmarks pane. All items in the document map are listed one after the other down the pane. It includes a hierarchy of links. If a page range is specified, only those bookmarks that are rendered exist in the hierarchy. |
| Excel | Excel renders a document map as the first worksheet in the workbook. It includes a hierarchy of links. When the link in the document map is selected, the appropriate target cell in the respective worksheet is opened. |
| Word | Word renders the document map as Table of Contents labels. |
| Other (for example, TIFF, XML, and CSV) | Not available in MHTML, XML, CSV, or Image. |

### Drillthrough links to other reports

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | Users select on data values in the report to view related data in another report. |
| PDF | Drillthrough links aren't available in PDF. Consider using hyperlinks for PDF reports that link to other pages. |
| Excel | Drillthrough links are rendered in Excel.<br /><br />The link becomes a hyperlink pointing to the report referenced by the drillthrough link. Selecting the link opens a report in a browser window. |
| Word | Drillthrough links are rendered in Word.<br /><br />The link becomes a hyperlink pointing to the report referenced by the drillthrough link. Selecting the link opens a report in a browser window. |
| Other | Not available in XML, CSV, or Image. |

### Toggle items within a report

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | Users select expand and collapse icons to view sections of a report. |
| PDF | The report server exports the current show or hide state of the report to PDF. Interactive toggling isn't supported |
| Excel | Drilldown links and items that can be toggled are rendered as collapsible outlines in Excel. You can expand and collapse sections of the report in Excel. For more information about Excel-imposed limitations, see [Exporting to Microsoft Excel (Report Builder)](../../reporting-services/report-builder/exporting-to-microsoft-excel-report-builder-and-ssrs.md). |
| Word | The report server exports the current show or hide state of the report to PDF. Interactive toggling isn't supported |
| Other | Not available in MHTML, XML, or CSV. When you export to an Image format, the report server exports the current show or hide stated of the report to PDF. Interactive toggling isn't supported. |

### Interactive sorting

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | For tabular reports, users select sort arrows on column to change how the data is sorted. |
| PDF | Not available in PDF. |
| Excel | Not available in Excel. |
| Word | Not available in Word. |
| Other | Not available in MHTML, XML, CSV, or Image. |

### Hyperlinks to external Web content or images

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | Users select links to open external Web pages in a new browser window. |
| PDF | The PDF rendering extension renders the hyperlinks. When a user selects on a hyperlink, the linked pages are opened in the browser. |
| Excel | Hyperlinks are rendered in Excel. |
| Word | Hyperlinks are rendered in Word. |
| Other | Hyperlinks aren't available in MHTML, XML, CSV, or Image.<br /><br />For MHTML, and Image, external images are rendered as a static picture. |

### Bookmark or anchor

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | Users select links to navigate to another section of the same report. |
| PDF | Not available in PDF. |
| Excel | Bookmarks are rendered in Excel.<br /><br />The bookmark becomes a hyperlink pointing to the name of the report item. |
| Word | Bookmarks are rendered in Word.<br /><br />The bookmark becomes a hyperlink pointing to the bookmarked report item. Only 40 characters of a bookmark or anchor name are converted when the report is exported, which can lead to duplicate bookmark or anchor names. Spaces are converted to underscores (_). |
| Other | Not available in XML, CSV, or Image. |

### Prompted parameters obtained at run time

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | A parameter input area appears at the top of the report. This area is part of the HTML Viewer used to display reports in a browser. |
| PDF | The report server exports the report to PDF using the parameter values currently in effect for the report. |
| Excel | The report server exports the report to Excel using the parameter values currently in effect for the report. |
| Word | The report server exports the report to Word using the parameter values currently in effect for the report. |
| Other | The report server exports the report to other formats using the parameter values currently in effect for the report. |

### Filters applied at run time

| Export Option | Support Information |
| --- | --- |
| Preview/Report Viewer, HTML | The filtered data is displayed to the user at run time. |
| PDF | The report server exports the report to PDF using filtered data in the current report. |
| Excel | The report server exports the report to Excel using filtered data in the current report. |
| Word | The report server exports the report to Word using filtered data in the current report. |
| Other | The report server exports the report to other formats using filtered data in the current report. |

## Related content

- [Export reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Interactive sort, document maps, and links (Report Builder)](../../reporting-services/report-design/interactive-sort-document-maps-and-links-report-builder-and-ssrs.md)
- [Tables, matrices, and lists (Report Builder)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Charts (Report Builder)](../../reporting-services/report-design/charts-report-builder-and-ssrs.md)
