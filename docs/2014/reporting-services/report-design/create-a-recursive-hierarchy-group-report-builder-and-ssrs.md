---
title: "Create a Recursive Hierarchy Group (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 8b830ba5-4d64-4348-a2b1-76b9338a1462
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create a Recursive Hierarchy Group (Report Builder and SSRS)
  A recursive hierarchy group organizes data from a single report dataset that includes multiple hierarchical levels, such as the report-to structure for manager-employee relationships in an organizational hierarchy.  
  
 Before you can organize data in a table as a recursive hierarchy group, you must have a single dataset that contains all the hierarchical data, You must have separate fields for the item to group and for the item to group by. For example, a dataset where you want to group employees recursively under their manager might contain a name, an employee name, an employee ID, and a manager ID.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To create a recursive hierarchy group  
  
1.  In Design view, add a table, and drag the dataset fields to display. Typically, the field that you want to show as a hierarchy is in the first column.  
  
2.  Right-click anywhere in the table to select it. The Grouping pane displays the details group for the selected table. In the Row Groups pane, right-click **Details**, and then click **Edit Group**. The **Group Properties** dialog box opens.  
  
3.  In **Group expressions**, click **Add**. A new row appears in the grid.  
  
4.  In the **Group on** list, type or select the field to group.  
  
5.  Click **Advanced**.  
  
6.  In the **Recursive Parent** list, enter or select the field to group on.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     Run the report. The report displays the recursive hierarchy group, although there is no indent to show the hierarchy  
  
### To format a recursive hierarchy group with indent levels  
  
1.  Click the text box that contains the field to which you want to add indent levels to display a hierarchy format. The properties for the text box appear in the Properties pane.  
  
    > [!NOTE]  
    >  If you do not see the Properties pane, click **Properties** on the **View** tab.  
  
2.  In the Properties pane, expand the `Padding` node, click **Left**, and from the drop-down list, select **\<Expression...>**.  
  
3.  In the Expression pane, type the following expression:  
  
     `=CStr(2 + (Level()*10)) + "pt"`  
  
     The Padding properties all require a string in the format *nnyy*, where *nn* is a number and *yy* is the unit of measure. The example expression builds a string that uses the `Level` function to increase the size of the padding based on recursion level. For example, a row that has a level of 1 would result in a padding of (2 + (1\*10))=12pt, and a row that has a level of 3 would result in a padding of (2 + (3\*10))=32pt. For information about the `Level` function, see [Level](report-builder-functions-level-function.md).  
  
4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     Run the report. The report displays a hierarchical view of the grouped data.  
  
## See Also  
 [Creating Recursive Hierarchy Groups &#40;Report Builder and SSRS&#41;](creating-recursive-hierarchy-groups-report-builder-and-ssrs.md)   
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Aggregate Functions Reference &#40;Report Builder and SSRS&#41;](report-builder-functions-aggregate-functions-reference.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
