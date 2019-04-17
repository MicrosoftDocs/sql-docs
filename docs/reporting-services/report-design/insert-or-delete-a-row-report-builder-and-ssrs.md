---
title: "Insert or Delete a Row (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: b9642af3-b3ae-4f78-b0be-8f96b79fc313
author: markingmyname
ms.author: maghan
---
# Insert or Delete a Row (Report Builder and SSRS)
You can add or delete rows in a tablix data region in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report. The tablix data region can be a table, a matrix, or a list. The following procedures do not apply to the chart and gauge data regions.  
  
 In a tablix data region, you can add rows that are associated with a group (inside a group) or that are not associated with a group (outside a group). A row that is inside a group repeats once per unique group value. For example, a row inside a group based on the value in a data column that contains color names, repeats once per distinct color name. For nested groups, a row can be outside the child group but inside the parent group. In this case, the row repeats once for each unique value in the parent group.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To select a data region so the row and column handles appear  
  
-   In Design view, click the upper left corner of the tablix data region so that column and row handles appear above and next to it.  
  
     For more information about data region areas, see [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md).  
  
## To insert a row in a selected data region  
  
-   Right-click a row handle where you want to insert a row, click **Insert Row**, and then click **Above** or **Below**.  
  
     \- or -  
  
-   Right-click a cell in the data region where you want to insert a row, click **Insert Row**, and then click **Above** or **Below**.  
  
## To delete a row from a selected data region  
  
-   Select the row or rows that you want to delete, right-click the handle for one of the rows you selected, and then click **Delete Rows**.  
  
     \- or -  
  
-   Right-click a cell in the data region where you want to delete a row, and then click **Delete Rows**.  
  
## To insert a row in a group in a selected data region  
  
-   Right-click a row group cell in the row group area of a tablix data region where you want to insert a row, click **Insert Row**, and then click **Above - Outside Group**, **Above - Inside Group**, **Below - Inside Group**, or **Below - Outside Group**.  
  
     A row is added either inside or outside the group represented by the row group cell you clicked on.  
  
## To delete a row from a group in a selected data region  
  
-   Right-click a row group cell in the row group area of a tablix data region where you want to delete a row, and then click **Delete Rows**.  
  
## See Also  
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Understanding Groups &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/understanding-groups-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)     
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
