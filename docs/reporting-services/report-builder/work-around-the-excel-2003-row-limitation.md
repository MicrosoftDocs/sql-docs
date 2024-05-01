---
title: "Work around the Excel 2003 row limitation"
description: You can work around the Excel 2003 row limitation when you export paginated reports to Excel by forcing a page break after some rows.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/16/2017
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Work around the Excel 2003 row limitation

  This article explains how to work around the Excel 2003 row limitation when you export paginated reports to Excel. The workaround is for a report that contains only a table.

> [!IMPORTANT]  
> The [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] 2003 (.xls) rendering extension is deprecated. For more information, see [Deprecated features in SQL Server Reporting Services in SQL Server 2016](../../reporting-services/deprecated-features-in-sql-server-reporting-services-ssrs.md).

Excel 2003 supports a maximum of 65,536 rows per worksheet. You can work around this limitation by forcing an explicit page break after some rows. The Excel renderer creates a new worksheet for each explicit page break.

### Create an explicit page break

1. Open the report in [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] or the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] web portal.

1. Right-click the Data row in the table, and then select **Add Group**. Choose **Parent Group** to add an outer table group.

     :::image type="content" source="media/work-around-the-excel-2003-row-limitation/datarow-selectparentgroup.png" alt-text="Screenshot of screen where you select the Parent Group.":::

1. Enter the following formula in the **Group by** expression box, and then select **OK** to add the parent group.

     `=Int((RowNumber(Nothing)-1)/65000)`

     The formula assigns a number to each set of 65,000 rows in the dataset. When a page break is defined for the group, the expression results in a page break every 65,000 rows.

     Adding the outer table group adds a group column to the report.

1. Delete the group column by right-clicking on the column header, selecting **Delete Columns**, choosing **Delete columns only**, and then selecting **OK**.

     :::image type="content" source="media/work-around-the-excel-2003-row-limitation/groupcolumn-delete-updated.png" alt-text="Screenshot of deleting a group column.":::

1. Right-click **Group 1** in the **Row Groups** section, and then select **Group Properties**.

     :::image type="content" source="media/work-around-the-excel-2003-row-limitation/groupproperties-updated.png" alt-text="Screenshot of the screen where you view group properties.":::

1. On the **Sorting** page of the **Group Properties** dialog, select the default sorting option and select **Delete**.

     :::image type="content" source="media/work-around-the-excel-2003-row-limitation/groupproperties-sorting-updated.png" alt-text="Screenshot of deleting default sorting.":::

1. On the **Page Breaks** page, choose **Between each instance of a group** and then select **OK**.

     :::image type="content" source="media/work-around-the-excel-2003-row-limitation/groupproperties-pagebreaks-updated.png" alt-text="Screenshot of the screen where you set page breaks.":::

1. Save the report. When you export it to Excel, it exports to multiple worksheets and each worksheet contains a maximum of 65,000 rows.
