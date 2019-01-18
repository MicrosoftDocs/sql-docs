---
title: "Delete a table in an Analysis Services tabular model | Microsoft Docs"
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
# Delete a table
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  In the model designer, you can delete tables in your model workspace database that you no longer need. Deleting a table does not affect the original source data, only the data that you imported and were working with. You cannot undo the deletion of a table.  
  
### To delete a table  
  
-   Right-click the tab at the bottom of the model designer for the table that you want to delete, and then click **Delete**.  
  
## Considerations when Deleting Tables  
  
-   When you delete a table, the entire tab that the table was on is deleted.  
  
-   If any measures were associated with that table, the definition of the measure will also be deleted.  
  
-   If you created any calculated columns using that table, columns in that table are also deleted; any calculated columns in other tables that use columns from the deleted table will display an error.  
  
## See also  
 [Tables and columns](../../analysis-services/tabular-models/tables-and-columns-ssas-tabular.md)  
  
  
