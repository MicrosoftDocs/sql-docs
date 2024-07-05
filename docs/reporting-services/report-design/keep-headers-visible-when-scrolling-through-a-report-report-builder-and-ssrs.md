---
title: Keep headers visible when scrolling through a paginated report in Report Builder
description: Learn how to freeze the row or column headings in Report Builder to prevent row and column labels from scrolling out of view after rendering a paginated report.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/27/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to freeze row or column headings so that I can keep useful day visible on my rendered reports.
---
# Keep headers visible when scrolling through a paginated report in Report Builder

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can prevent row and column labels from scrolling out of view after rendering a paginated report by freezing the row or column headings.
  
How you control the rows and columns depends on whether you have a table or a matrix. If you have a table, you configure static members (row and column headings) to remain visible. If you have a matrix, you configure row and column group headers to remain visible.  
  
If you export the report to Excel, the header doesn't freeze automatically. You can freeze the pane in Excel. For more information, see the **Page Headers and Footers** section of [Export a paginated report to Microsoft Excel (Report Builder)](../../reporting-services/report-builder/exporting-to-microsoft-excel-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> Even if a table has row and column groups, you can't keep those group headers visible while scrolling  
  
The following image shows a table:

:::image type="content" source="../../reporting-services/report-design/media/table.png" alt-text="Screenshot of an empty table in Report Builder.":::
  
The following image shows a matrix:

:::image type="content" source="../../reporting-services/report-design/media/matrix.png" alt-text="Screenshot of a matrix in Report Builder highlighting row and column groups.":::
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Keep matrix group headers visible while scrolling  
  
1. Right-click the row, column, or corner handle of a tablix data region, and then select **Tablix Properties**.  
  
1. On the **General** tab, under **Row Headers** or **Column Headers**, select **Keep header visible while scrolling**.  

    :::image type="content" source="../../reporting-services/report-design/media/header-should-remain-visible-while-scrolling.png" alt-text="Screenshot of the Tablix Properties dialog box highlighting the options to select Keep header visible while scrolling.":::

1. Select **OK**.
  
## Keep a static tablix member (row or column) visible while scrolling  
  
1. On the design surface, select anywhere in the table to display static members, and groups, in the grouping pane.  

    :::image type="content" source="../../reporting-services/report-design/media/grouppane-updated.png" alt-text="Screenshot of Report Builder showing a matrix with groups shown in the Grouping pane.":::

    The **Row Groups** pane displays the hierarchical static and dynamic members for the row groups hierarchy, and the **Column Groups** pane shows a similar display for the column groups hierarchy.  
  
1. On the right side of the **Grouping** pane, select the dropdown, and then select **Advanced Mode**.  
  
1. Select the row or column static member that you want to remain visible while scrolling. The **Properties** pane displays the **Tablix Member** properties.  

    :::image type="content" source="../../reporting-services/report-design/media/grouppane-tablixmember-updated.png" alt-text="Screenshot of Report Builder highligting a group in the Grouping pane and its FixedData property in the Properties pane" lightbox="../../reporting-services/report-design/media/grouppane-tablixmember-updated.png":::
  
1. In the Properties pane, set **FixedData** to **True**.  
  
1. Repeat this step for as many adjacent members as you want to keep visible while scrolling.  
  
1. To preview your report, select **Run**.  
  
As you page down or across the report, the static tablix members remain in view.  
  
## Related content

- [Tablix data region in a paginated report (Report Builder)](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)
- [Find, view, and manage reports (Report Builder and SSRS)](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
- [Export paginated reports (Report Builder)](../../reporting-services/report-builder/export-reports-report-builder-and-ssrs.md)
