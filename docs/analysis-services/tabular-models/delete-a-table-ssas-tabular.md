---
title: "Delete a Table (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: be4ed45f-fde3-466c-9869-49bd3ddb505e
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "Inactive"
---
# Delete a Table (SSAS Tabular)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  In the model designer, you can delete tables in your model workspace database that you no longer need. Deleting a table does not affect the original source data, only the data that you imported and were working with. You cannot undo the deletion of a table.  
  
### To delete a table  
  
-   Right-click the tab at the bottom of the model designer for the table that you want to delete, and then click **Delete**.  
  
## Considerations when Deleting Tables  
  
-   When you delete a table, the entire tab that the table was on is deleted.  
  
-   If any measures were associated with that table, the definition of the measure will also be deleted.  
  
-   If you created any calculated columns using that table, columns in that table are also deleted; any calculated columns in other tables that use columns from the deleted table will display an error.  
  
## See Also  
 [Tables and Columns &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/tables-and-columns-ssas-tabular.md)  
  
  
