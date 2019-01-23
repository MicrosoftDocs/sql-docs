---
title: "Delete relationships in Analysis Services tabular models | Microsoft Docs"
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
# Delete relationships 
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  You can delete existing relationships by using the model designer in Diagram View or by using the Manage Relationships dialog box. For information about how relationships are used in tabular models, see [Relationships](../../analysis-services/tabular-models/relationships-ssas-tabular.md).  
  
## Considerations for deleting relationships  
 Keep the following issues in mind when deciding whether to delete a relationship:  
  
-   There is no way to undo the deletion of a relationship. You can re-create the relationship, but this action requires a complete recalculation of formulas in the model. Therefore, always check first before deleting a relationship that is used in formulas.  
  
-   Deleting a relationship between two tables can cause errors in formulas that reference these tables.  
  
-   The Data Analysis Expression (DAX) RELATED function uses the relationships between tables to look up related values in another table. It will return different results after the relationship is deleted. For more information, see the RELATED Function (DAX).  
  
-   In addition to changing PivotTable and formula results, both the creation and deletion of relationships will cause the workbook to be recalculated, which can take some time.  
  
## Delete Relationships  
  
#### To delete a relationship by using Diagram View  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click the **Model** menu, then point to **Model View**, and then click **Diagram View**.  
  
2.  Right-click a relationship line between two tables, and then click **Delete**.  
  
#### To delete a relationship by using the Manage Relationships dialog box  
  
1.  In [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)], click the **Table** menu, and then click **Manage Relationships**.  
  
2.  In the **Manage Relationships** dialog box, select one or more relationships from the list.  
  
     To select multiple relationships, hold down CTRL while you click each relationship.  
  
3.  Click **Delete Relationship**.  
  
4.  In the **Manage Relationships** dialog box, click **Close**.  
  
## See Also  
 [Relationships](../../analysis-services/tabular-models/relationships-ssas-tabular.md)   
 [Create a relationship](../../analysis-services/tabular-models/create-a-relationship-between-two-tables-ssas-tabular.md)  
  
  
