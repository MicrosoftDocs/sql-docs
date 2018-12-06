---
title: "Delete a Column (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 703db83b-e554-450e-813e-23ad08c1cdad
author: minewiskan
ms.author: owend
manager: craigg
---
# Delete a Column (SSAS Tabular)
  This topic describes how to delete a column from a tabular model table.  
  
## Delete a Model Table Column  
  
> [!NOTE]  
>  Deleting a column from a model table does not delete the column from a partition query definition. If the column you are deleting is part of a partition, you must manually delete the column from the partition query definition. Failure to delete the column from the partition query definition will cause the column to be queried and data returned, but not populated to the model table, during processing operations. For more information, see [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md).  
  
#### To delete a model table column  
  
-   In the model designer, in the table that contains the column you want to delete, right-click on the column, and then click **Delete**.  
  
#### To delete a model table column by using the Table Properties dialog box  
  
1.  In the model designer, click on the table which contains the column you want to delete, then click the **Table** menu, and then click  **Table Properties**.  
  
2.  In **Column names from**, select **Model** (*to use model table column names, if different from source*).  
  
3.  In the **Edit Table Properties** dialog box, in the table preview window, uncheck the column you want to delete, and then click **OK**.  
  
## See Also  
 [Add Columns to a Table &#40;SSAS Tabular&#41;](add-columns-to-a-table-ssas-tabular.md)   
 [Partitions &#40;SSAS Tabular&#41;](partitions-ssas-tabular.md)  
  
  
