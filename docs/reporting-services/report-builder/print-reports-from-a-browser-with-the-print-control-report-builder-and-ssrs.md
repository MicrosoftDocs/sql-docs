---
title: "Print reports from a browser with the print control (Report Builder)"
description: To improve the print quality of reports viewed in a browser and to print multiple pages, use the client-side print features in SQL Server Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Print reports from a browser with the print control (Report Builder)

  Although a browser is the most common client application used to view a report, browser print functionality isn't ideal for printing reports. Print functionality in a browser is designed for printing Web pages. Typically, pages that you print from a browser include all of the visual elements that are on a Web page, plus header and footer information that identifies the page or Web site. Printing from a browser prints the contents of the current window. For a multipage report, the browser prints the first page at most, and possibly less if the report page extends beyond the dimensions of a printed page.
You can use the client-side print functionality provided in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] for a couple of reasons. You can use the functionality to improve the print quality of reports that you view in a browser and print multiple pages. Client-side printing provides a standard **Print** dialog box that can be used to select a printer, specify pages and margins, and preview the report before you print. Client-side printing is intended to be used in place of the **Print** command on the browser's **File** menu. When you use client-side printing, the report is printed as it was designed, without the extra elements you see in a Web page printout.

To use client-side printing, you need to install a [!INCLUDE[msCoName](../../includes/msconame-md.md)] ActiveX control. For more information, see [Enable and disable client-side printing for Reporting Services](../../reporting-services/report-server/enable-and-disable-client-side-printing-for-reporting-services.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

## Print options

To configure print properties for your report, in the **Print** dialog, select the **Properties** button. **Paper size** is determined by the default height and width of the report page size as defined in the report definition. The available values are dependent on the printer type and its capabilities. Width and height display default values as determined by the print drivers that are configured on the computer. Changing these values causes the report to print using the new dimensions. Page width and height are each determined by **Orientation**, which is set to either **Portrait** or **Landscape**. The default orientation displayed is dependent on the page width and page height of the report.

> [!NOTE]  
> The **Print** dialog and the default printer settings for width, height, and page orientation are determined by the report definition.

### Print preview

To preview a report, in the **Print** dialog, select the **Preview** button. Selecting preview opens the first page of the report in a separate preview window. More pages are made available as the report is rendered on the report server. A previewed report is rendered in EMF format. You can navigate to the previous or next page until the last page is reached, and the **Next** button is disabled.

### Adjust print margins

You can modify the print margins in the rendered EMF report before printing the report. To modify the margins, in the **Print** dialog, select the **Preview** button. At the top of the preview page, select the **Margins** button. The Margins dialog box is displayed. Configure the top, bottom, right, and left margins as desired. Select **OK**. The dialog closes and the settings are stored for rendering preview and printing.

## Related content

- [Print reports (Report Builder)](../../reporting-services/report-builder/print-reports-report-builder-and-ssrs.md)
- [Print a report (Report Builder)](../../reporting-services/report-builder/print-a-report-report-builder-and-ssrs.md)
