---
title: "Delete a Table (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: be4ed45f-fde3-466c-9869-49bd3ddb505e
author: minewiskan
ms.author: owend
manager: craigg
---
# Delete a Table (SSAS Tabular)
  In the model designer, you can delete tables in your model workspace database that you no longer need. Deleting a table does not affect the original source data, only the data that you imported and were working with. You cannot undo the deletion of a table.  
  
### To delete a table  
  
-   Right-click the tab at the bottom of the model designer for the table that you want to delete, and then click **Delete**.  
  
## Considerations when Deleting Tables  
  
-   When you delete a table, the entire tab that the table was on is deleted.  
  
-   If any measures were associated with that table, the definition of the measure will also be deleted.  
  
-   If you created any calculated columns using that table, columns in that table are also deleted; any calculated columns in other tables that use columns from the deleted table will display an error.  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](tables-and-columns-ssas-tabular.md)  
  
  
