---
title: "Create and manage hierarchies in Analysis Services tabular models | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create and manage hierarchies 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Hierarchies can be created and managed in the model designer, in Diagram View. To view the model designer in Diagram View, in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Model** menu, then point to **Model View**, and then click **Diagram View**.  
  
 This article includes the following tasks:  
  
-   [Create a Hierarchy](#bkmk_create)  
  
-   [Edit a Hierarchy](#bkmk_edit)  
  
-   [Delete a Hierarchy](#bkmk_delete)  
  
##  <a name="bkmk_create"></a> Create a hierarchy  
 You can create a hierarchy by using the columns and table context menu. When you create a hierarchy, a new parent level displays with selected columns as child levels.  
  
#### To create a hierarchy from the context menu  
  
1.  In the model designer (Diagram View), in a table window, right-click on a column, and then click **Create Hierarchy**.  
  
     To select multiple columns, click each column, then right-click to open the context menu, and then click **Create Hierarchy**.  
  
     A parent hierarchy level is created at the bottom of the table window and the selected columns are copied under the hierarchy as child levels.  
  
2.  Type a name for the hierarchy.  
  
 You can drag additional columns into your hierarchy's parent level, which copies the columns. Drop the child level to place it where you want it to appear in the hierarchy.  
  
> [!NOTE]  
>  The Create Hierarchy command is disabled in the context menu if you multi-select a measure along with one or more columns or you select columns from multiple tables.  
  
##  <a name="bkmk_edit"></a> Edit a hierarchy  
 You can rename a hierarchy, rename a child level, change the order of the child levels, add additional columns as child levels, remove a child level from a hierarchy, show the source name of a child level (the column name), and hide a child level if it has the same name as the hierarchy parent level.  
  
#### To change the name of a hierarchy or child level  
  
1.  Right-click the hierarchy parent level or a child level, and the click **Rename**.  
  
2.  Type a new name or edit the existing name.  
  
#### To change the order of a child level in a hierarchy  
  
-   Click and drag a child level into a new position in the hierarchy.  
  
-   Or, right-click a child level of the hierarchy, and then click Move Up to move the level up in the list, or click Move Down to move the level down in the list.  
  
-   Or, click a child level to select it, and then press Alt + Up Arrow to move the level up, or press Alt+Down Arrow to move the level down in the list.  
  
#### To add another child level to a hierarchy  
  
-   Click and drag a column onto the parent level or into a specific location of the hierarchy. The column is copied as a child level of the hierarchy.  
  
-   Or, right-click a column, point to **Add to Hierarchy**, and then click the hierarchy.  
  
> [!NOTE]  
>  You can add a hidden column (a column that is hidden from reports) as a child level to the hierarchy. The child level is not hidden.  
  
#### To remove a child level from a hierarchy  
  
-   Right-click a child level, and then click **Remove from Hierarchy**.  
  
-   Or, click a child level, and then press **Delete**.  
  
> [!NOTE]  
>  If you rename a hierarchy child level, it no longer shares the same name as the column that it is copied from. Use the **Show Source Name** command to see which column it was copied from.  
  
#### To show a source name  
  
-   Right-click a hierarchy child level, and then click **Show Source Name**. The name of the column that it was copied from appears.  
  
##  <a name="bkmk_delete"></a> Delete a Hierarchy  
  
#### To delete a hierarchy and remove its child levels  
  
-   Right-click the parent hierarchy level, and then click Delete Hierarchy.  
  
-   Or, click the parent hierarchy level, and then press Delete. This also removes all the child levels.  
  
## See Also  
 [Tabular Model Designer](../../analysis-services/tabular-models/tabular-model-designer-ssas.md)   
 [Hierarchies](../../analysis-services/tabular-models/hierarchies-ssas-tabular.md)   
 [Measures](../../analysis-services/tabular-models/measures-ssas-tabular.md)  
  
  
