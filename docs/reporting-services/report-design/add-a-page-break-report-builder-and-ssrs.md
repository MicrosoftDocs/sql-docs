---
title: Add a page break to a Report Builder paginated report
description: Learn how to add a page break to rectangles, data regions, or groups within data regions in Report Builder to control the information on each page.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/19/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to add page break elements in my reports so that I can better control the information on the page.
---
# Add a page break to a Report Builder paginated report

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

You can add a page break to rectangles, data regions, or groups within data regions in paginated reports to control the amount of information on each page. Adding page breaks can improve the performance of published reports, because only the items on each page have to be processed as you view the report. When the whole report is a single page, all items must be processed before you can view the report.  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Add a page break to a data region  
  
1. On the **Design** surface, right-click the corner handle of the data region, and then select **Tablix Properties**.

    :::image type="content" source="../../reporting-services/report-design/media/tablix-properties.png" alt-text="Screenshot of the context menu for a data region highlighting the Tablix Properties option.":::
  
1. On the **General** tab, under **Page break options**, select one of the following options:
  
    - **Add a page break before**: Select this option when you want to add a page break before the table.  
  
    - **Add a page break after**: Select this option when you want to add a page break after the table.  
  
    - **Keep together on one page if possible**: Select this option when you want the data to stay on one page.

:::image type="content" source="../../reporting-services/report-design/media/add-page-break-to-data-region.png" alt-text="Screenshot of the Tablix Properties dialog box on the General tab highlighting the Page break options.":::
  
## Add a page break to a rectangle  
  
1. On the **Design surface**, right-click the rectangle where you want to add a page break, and then select **Rectangle Properties**.  
  
1. On the **General** tab, under **Page break options**, select one of the following options:  
  
    - **Add a page break before**: Select this option when you want to add a page break before the rectangle.  
  
    - **Add a page break after**: Select this option when you want to add a page break after the rectangle.  
  
    - **Omit border on page break**: Select this option when you don't want a border on the page break.  
  
    - **Keep contents together on a single page, if possible**: Select this option when you want contents inside the rectangle to stay on one page.

    :::image type="content" source="../../reporting-services/report-design/media/add-page-break-to-rectangle.png" alt-text="Screenshot of the Rectangle Properties dialog box on the General tab highlighting the Page break options.":::
  
## Add a page break to a row group in a table, matrix, or list  
  
1. In the **Grouping** pane, right-click a row group, and then select **Group Properties**.  
  
    > [!NOTE]  
    > Page breaks are ignored on column groups.  
  
1. On the **Page Breaks** tab, select **Between each instance of a group** to add a page break between each instance of a group in the table.  
  
1. Optionally, select **Also at the start of a group** or **Also at the end of a group** to specify that a page break is added when a group starts or ends in the table.  

    :::image type="content" source="../../reporting-services/report-design/media/add-page-break-to-group.png" alt-text="Screenshot of the Group Properties dialog box on the Page Breaks tab highlighting the Page break options.":::

## Related content  

- [Pagination in paginated reports (Microsoft Report Builder)](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)
- [Rendering behaviors in a paginated report (Report Builder)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)
- [Page headers and footers in a paginated report (Report Builder)](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)  
  