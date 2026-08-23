---
title: "Controlling page breaks, headings, columns, and rows in paginated reports"
description: Optimize viewing and printing of your tables, lists, or images in paginated reports with choices for page lengths, columns, headings, and rows in Report Builder.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: concept-article
ms.custom:
  - updatefrequency5
---
# Controlling page breaks, headings, columns, and rows in paginated reports (Report Builder)


[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  A page break divides a paginated report into separate pages for viewing and printing. Page breaks determine how the content is fitted to a report page for optimal viewing when you preview a report or export it to a different file format.  
  
 Adding page breaks also improves the performance of large reports when they are processed. A rendered page is displayed while the rest of the pages are rendered in the background. This allows you to begin viewing the initial pages of the report while waiting for additional pages to become available.  
  
 Page breaks can be added to report items such as a table, matrix, list, chart, gauge, or image. You can also add page breaks to groups in a table, matrix, or list. Page breaks can be added before, after, and between groups. Page breaks between groups are not added to the report by default.  
  
 For more information, see [Display Row and Column Headers on Multiple Pages &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/display-row-and-column-headers-on-multiple-pages-report-builder-and-ssrs.md) and [Keep Headers Visible When Scrolling Through a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/keep-headers-visible-when-scrolling-through-a-report-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Related content

- [Tables, matrices, and lists in Report Builder paginated reports](tables-matrices-and-lists-report-builder-and-ssrs.md)
- [Control the tablix data region display on a paginated report page (Report Builder)](controlling-the-tablix-data-region-display-on-a-report-page.md)
- [Grouping pane in a paginated report (Report Builder)](grouping-pane-report-builder.md)
- [Display headers and footers with a group in a paginated report (Report Builder)](display-headers-and-footers-with-a-group-report-builder-and-ssrs.md)
