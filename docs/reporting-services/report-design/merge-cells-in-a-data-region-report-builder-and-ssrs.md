---
title: "Merge cells in a data region in a paginated report"
description: Discover how to merge cells in a data region in a paginated report to combine cells, improve data region appearance, or provide spanning labels for column and row groups in Report Builder.
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom:
  - updatefrequency5
---
# Merge cells in a data region in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In a paginated report, you can merge cells in a data region to combine cells, improve data region appearance, or provide spanning labels for column groups and row groups.  
  
You can only merge cells within each area of a data region: corner, column headers, group definition (or row headers), and body. You can't merge cells that cross area boundaries. For example, you can't merge a cell in the data region corner area with a cell in the row group area. Read more about [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To merge cells in a data region  
  
1.  In the data region on the report design surface, click the first cell to merge. Holding the left mouse button down, drag vertically or horizontally to select adjacent cells. The selected cells are highlighted.  
  
2.  Right-click the selected cells and select **Merge Cells**. The selected cells are combined into a single cell.  
  
3.  Repeat steps 1 and 2 to merge other adjacent cells in a data region.  
  
## Related content

- [Tablix data region in a paginated report (Report Builder)](tablix-data-region-report-builder-and-ssrs.md)
- [Tables in paginated reports (Report Builder)](tables-report-builder-and-ssrs.md)
- [Create a matrix in a paginated report (Report Builder)](create-a-matrix-report-builder-and-ssrs.md)
- [Create invoices and forms with lists in a paginated report (Report Builder)](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)
- [Tables, matrices, and lists in Report Builder paginated reports](tables-matrices-and-lists-report-builder-and-ssrs.md)
