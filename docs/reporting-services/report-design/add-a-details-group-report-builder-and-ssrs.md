---
title: "Add a details group to a paginated report"
description: Find out about adding a details group to an existing tablix data region. Also how to display the detail data for a matrix in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a details group to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

In a paginated report, the detail data from a report dataset is specified as a group with no group expression. Add a detail group to an existing tablix data region when you want to display the detail data for a matrix. You can also add back detail data that you deleted from a table or list, or to add more detail groups. For more information about groups, see [Understand groups &#40;Report Builder&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Add a details group to a tablix data region  
  
1.  On the design surface, select a tablix data region. The **Grouping** pane displays the row and column groups for the selected data region.  
  
1.  In the **Grouping** pane, right-click a group that is an innermost child group. Select **Add Group**, and then choose **Child Group**. The **Tablix Group** dialog opens.  
  
1.  In **Group expression**, leave the expression blank. A details group has no expression.  
  
1.  Select **Show detail data**.  
  
1.  Select **OK**.
  
     A new details group is added as a child group in the Grouping pane, and the row handle for the group you selected in step 1 displays the details group icon. For more information about handles, see [Tablix data region cells, rows, and columns &#40;Report Builder&#41;](../../reporting-services/report-design/tablix-data-region-cells-rows-and-columns-report-builder-and-ssrs.md).  
  
## Related content

- [Add or delete a group in a data region &#40;Report Builder&#41;](../../reporting-services/report-design/add-or-delete-a-group-in-a-data-region-report-builder-and-ssrs.md)
- [Understand groups &#40;Report Builder&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md)
- [Tablix data region &#40;Report Builder&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)
- [Tables &#40;Report Builder&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)
- [Matrices &#40;Report Builder&#41;](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)
- [Lists &#40;Report Builder&#41;](../../reporting-services/report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)
- [Tables, matrices, and lists &#40;Report Builder&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)
