---
title: "Add a page break to a paginated report | Microsoft Docs"
description: Discover how to add a page break to rectangles, data regions, or groups within data regions in Report Builder to control the information on each page.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 3846cd48-2787-47e9-b13b-7fc45a205f68
author: maggiesMSFT
ms.author: maggies
---
# Add a page break to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can add a page break to rectangles, data regions, or groups within data regions in paginated reports, to control the amount of information on each page. Adding page breaks can improve the performance of published reports because only the items on each page have to be processed as you view the report. When the whole report is a single page, all items must be processed before you can view the report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a page break to a data region  
  
1.  On the design surface, right-click the corner handle of the data region and then click **Tablix Properties**.  
  
2.  On the **General** tab, under **Page break options**, select one of the following options:  
  
    -   **Add a page break before**. Select this option when you want to add a page break before the table.  
  
    -   **Add a page break after**. Select this option when you want to add a page break after the table.  
  
    -   **Fit table on one page if possible**. Select this option when you want the data to stay on one page.  
  
### To add a page break to a rectangle  
  
1.  On the design surface, right-click the rectangle where you want to add a page break, and then click **Rectangle Properties**.  
  
2.  On the **General** tab, under **Page break options**, select one of the following options:  
  
    -   **Add a page break before**. Select this option when you want to add a page break before the rectangle.  
  
    -   **Add a page break after**. Select this option when you want to add a page break after the rectangle.  
  
    -   **Omit border on page break**. Select this option when you do not want a border on the page break.  
  
    -   **Keep contents together on a single page, if possible**. Select this option when you want contents inside the rectangle to stay on one page.  
  
### To add a page break to a row group in a table, matrix, or list  
  
1.  In the Grouping pane, right-click a row group, and then click **Group Properties**.  
  
    > [!NOTE]  
    >  Page breaks are ignored on column groups.  
  
2.  On the **Page Breaks** tab, select **Between each instance of a group** to add a page break between each instance of a group in the table.  
  
3.  Optionally, select **Also at the start of a group** or **Also at the end of a group** to specify that a page break be added when a group starts or ends in the table.  
  
## See Also  
 [Pagination in Reporting Services &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/pagination-in-reporting-services-report-builder-and-ssrs.md)   
 [Rendering Behaviors &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)   
 [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)  
  
  
