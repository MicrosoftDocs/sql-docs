---
title: "Print a report (Report Builder)"
description: You can view and print a report from a browser, the Reporting Services web portal, or any application that you use to view an exported report.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Print a report (Report Builder)

  After you save a report to a report server, you can view and print the report from a browser, the Reporting Services web portal, or any application that you use to view an exported report. Before saving a report, you can print it when you preview it.

When you print a report, you can specify the size of the paper to use. The size of the paper determines the number of pages in a report and which report data fits on each page. Paper size affects only reports that are rendered with hard page-break renders: PDF, Image, and Print. Setting the paper size has no effect on other renderers. For more information, see [Renderer behaviors (Report Builder)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md).

From the report viewer toolbar in the Reporting Services web portal or in preview in Report Builder, you can export a report to a hard page-break renderer. Or, you can select the Print button to print a copy of the report. You might need to set the paper size or other page setup properties. Use the **Report Properties** dialog to change page setup properties, including paper size.

You can specify print page margins in two different locations. Those locations are in design mode and in run mode.

- **Design mode**: When you set page margins in design mode, these settings are saved in the report definition when you save the report.

- **Run mode**: When you set page margins in run mode, this information isn't saved in the report definition. The next time you print the report, you get the settings from the report definition, unless you indicate your print margins again.

    > [!NOTE]  
    >  Print margins are not displayed in design or run modes. There is no relationship between the design surface area and the print area of your report. To see print margins, in run mode, select Print Layout on the **Run** tab on the Ribbon.

For more information about report paging, see [Pagination in Reporting Services (Report Builder)](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md).

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]

### Print a report in Report Builder

1. Open a report.

1. On the Home tab, select **Run**.

1. (optional) Select **Print Layout** to see how the report looks when it prints.

1. (optional) Select **Page Setup** to set paper, orientation, and margins.

    > [!NOTE]  
    >  The default values for these come from the report properties, which are set in Design view. The values you set here in the **Page Setup** dialog are for this session only. When you close this report and reopen it, it will have the default values again.

1. Select **Print**.

1. In the **Print** dialog, select a printer, and specify other printing options.

### Print a report from a web browser application

1. In the Reporting Services web portal, navigate to the report that you want to print. Open the report.

1. On the toolbar at the top of the report, select **Print**.

    > [!NOTE]  
    >  The first time you print an HTML report, the report server prompts you to install an ActiveX control used for printing. You must install and configure the control to print.

1. In the **Print** dialog, select a printer, and then select **Print**.

### Print a report from other applications

1. In the Reporting Services web portal, navigate to the report that you want to print. Open the report.

1. On the toolbar at the top of the report, select a rendering format, and then select **Export**. The report opens in a viewer application that corresponds to the rendering format.

     For example, if you select PDF, the report opens in Adobe Acrobat Reader.

1. On the **File** menu in that program, select **Print**.

### Change paper size

1. Right-click outside of the report body and select **Report Properties**.

1. In **Page Setup**, select a value from the **Paper Size** list. Each option populates the **Width** and **Height** properties. You can also specify a custom size by typing numeric values in the **Width** and **Height** boxes. Select **OK**.

    > [!NOTE]  
    >  Size values have a default unit based on the user's locale settings. To designate a different unit, type a physical unit designator such as cm, mm, pt, or pc after the numeric value.

### Set page margins in design mode

- Right-click the blue area around the design surface, select **Report Properties**, and then select the **Page Setup** page.

### Set page margins in run mode

- Select **Page Setup** on the **Run** tab.

## Related content

- [Print reports (Report Builder)](../../reporting-services/report-builder/print-reports-report-builder-and-ssrs.md)
- [Export reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
- [Report Properties dialog, page setup (Report Builder)](/previous-versions/sql/sql-server-2016/dd220640(v=sql.130))
- [Report design view (Report Builder)](../../reporting-services/report-builder/report-design-view-report-builder.md)
