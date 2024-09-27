---
title: "Export a paginated report to a CSV file"
description: In Report Builder, the CSV rendering extension renders paginated reports to a plain text format that is readable and compatible with many applications.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Export a paginated report to a CSV file (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The Comma-Separated Value (CSV) rendering extension renders paginated reports as a flattened representation of data from a report in a standardized, plain-text format that is easily readable and exchangeable with many applications.

The CSV rendering extension uses a string character delimiter to separate fields and rows, with the string character delimiter configurable to be a character other than a comma. The resulting file can be opened in a spreadsheet program like [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] or used as an import format for other programs. The exported report becomes a .csv file, and returns a MIME type of **text/csv**.

If you want to work with data related to charts, data bars, sparklines, gauges, and indicators in [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)], export the report to a CSV file, and then open the file in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Excel.

See [Export reports (Report Builder and SSRS)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md) for details on how to export to CSV format.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## <a id="CSVRendering"></a> CSV rendering

When rendered using the default settings, a CSV report has the following characteristics:

- The default field delimiter string is a comma (`,`).

    > [!NOTE]  
    >  You can change the field delimiter to any character that you want, including Tab, by changing the device information settings. For more information, see [CSV device information settings](../../reporting-services/csv-device-information-settings.md).

- The record delimiter string is the carriage return and line feed (`<cr><lf>`).

- The text qualifier string is a quotation mark (`"`).

     The CSV renderer doesn't add qualifiers around all text strings. Text qualifiers are added only when the value contains the delimiter character or when the value has a line break.

- If the text contains an embedded delimiter string or qualifier string, the text qualifier is placed around the text, and the embedded qualifier strings are doubled.

- Formatting and layout are ignored.

The following items are ignored during rendering:

- Page header

- Page footer

- Custom report items

- Line

- Image

- Rectangle

- Automatic subtotals

The remaining report items are sorted, from top to bottom, then left to right. Each item is then rendered to a column. If the report has nested data items like lists or tables, the parent items are repeated in each record.

The following table indicates the appearance of report items when rendered:

| Item | Rendering behavior |
| --- | --- |
| Text box | Renders the contents of the text box. In default mode, items are formatted based on the item's formatting properties. In compliant mode, device information settings can change formatting. For more information about CSV rendering modes, see later in this article. |
| Table | Renders by expanding the table and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns don't have column or row headings. Drillthrough reports aren't supported. |
| Matrix | Renders by expanding the matrix and creating a row and column for each row and column at the lowest level of detail. Subtotal rows and columns don't have column or row headings. |
| List | Renders a record for each detail row or instance in the list. |
| Subreport | The parent item is repeated for each instance of the contents. |
| Chart | Renders by creating a row for each chart value and member labels. Labels from series and categories in hierarchies are flattened and included in the row for a chart value. |
| Data bar | Renders like a chart. Typically, a data bar doesn't include hierarchies or labels. |
| Sparkline | Renders like a chart. Typically, a sparkline doesn't include hierarchies or labels. |
| Gauge | Renders as a single record with the minimum and maximum values of the linear scale, start and end values of the range, and the value of the pointer. |
| Indicator | Renders as a single record with the active state name, available states, and the data value. |
| Map | Renders a row with the labels and values for each map member of a map layer.<br /><br />When the map has multiple layers, the values in the rows vary depending on whether the map layers use the same or different map data regions. If multiple map layers use the same data region, the rows contain data from all layers. |

### Hierarchical and grouped data

Hierarchical and grouped data must be flattened in order to be represented in the CSV format.

The rendering extension flattens the report into a tree structure that represents the nested groups within the data region. To flatten the report:

- A row hierarchy is flattened before a column hierarchy.

- Columns are ordered as follows: text boxes in body order left-to-right, top-to-bottom followed by data regions ordered left-to-right, top-to-bottom.

- Within a data region, the columns are ordered as follows: corner members, row hierarchy members, column hierarchy members, and then cells.

- Peer data regions are data regions or dynamic groups that share a common data region or dynamic ancestor. Peer data is identified by branching of the flattened tree.

For more information, see [Tables, matrices, and lists (Report Builder and SSRS)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).

## <a id="RenderingModes"></a> Renderer modes

The CSV rendering extension can operate in two modes: one is optimized for Excel and the other is optimized for non-Microsoft applications that require strict CSV compliance with the CSV specification in RFC 4180. Depending on which mode you use, peer data regions are handled differently.

### Default mode

The default mode is optimized for Excel. When rendered in default mode, the report is rendered as a CSV file with multiple sections of CSV-rendered data. Each peer data region is empty line delimited. Peer data regions within the report body are rendered as separate blocks of data within the CSV file. The result is a CSV file in which:

- Individual text boxes within the report body are rendered once as the first block of data within the CSV file.

- Each top-level peer data region in the report body is rendered in its own data block.

- Nested data regions are rendered diagonally into the same data block.

#### Format

Numeric values are rendered in their formatted state. Excel can recognize formatted numeric values, such as currency, percentage and date, and format the cells appropriately when importing the CSV file.

### Compliant mode

Compliant mode is optimized for non-Microsoft applications.

#### Data Regions

Only the first row of the file contains the column headers and each row has the same number of columns.

#### Format

Values are unformatted.

## <a id="Interactivity"></a> Interactivity

This renderer doesn't support interactivity in the generated CSV formats. The following interactive elements aren't rendered:

- Hyperlinks

- Show or Hide

- Document Map

- Drillthrough or clickthrough links

- End user sort

- Fixes headers

- Bookmarks

## <a id="DeviceInfo"></a> Device information settings

You can change some default settings for this renderer. These settings include which mode to render in, which characters to use as delimiters, and which characters to use as the text qualifier default string. You can change these settings by modifying the device information settings. For more information, see [CSV device information settings](../../reporting-services/csv-device-information-settings.md).

## Related content

- [Pagination in Reporting Services (Report Builder  and SSRS)](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)
- [Renderer behaviors (Report Builder  and SSRS)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)
- [Interactive functionality for different report rendering extensions (Report Builder and SSRS)](../../reporting-services/report-builder/interactive-functionality-different-report-rendering-extensions.md)
- [Render report items (Report Builder and SSRS)](../../reporting-services/report-design/rendering-report-items-report-builder-and-ssrs.md)
- [Tables, matrices, and lists (Report Builder and SSRS)](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
