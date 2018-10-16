---
title: "Remove Columns from a Mining Structure | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Remove Columns from a Mining Structure
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  You can use Data Mining Designer to remove columns from a mining structure after the structure has already been created. Reasons to remove a mining structure column might include the following:  
  
-   The mining structure contains multiple copies of a column and you want to avoid the use of duplicate data in a model.  
  
-   The data should be protected, but drillthrough has been enabled.  
  
-   The data is unused in modeling and should not be processed.  
  
 Deleting a column from the mining structure does not change the column in the data source view or in the external data; only metadata is deleted. However, when you change the columns used in a mining structure, you must reprocess the structure and any models based on it.  
  
### To remove a column from the mining structure  
  
1.  Select the **Mining Structure** tab in Data Mining Designer.  
  
2.  Expand the tree for the mining structure, to show all the columns.  
  
3.  Right-click the column that you want to delete, and then select **Delete**.  
  
4.  In the **Delete Objects** dialog box, click **OK**.  
  
## See Also  
 [Mining Structure Tasks and How-tos](../../analysis-services/data-mining/mining-structure-tasks-and-how-tos.md)  
  
  
