---
title: "Remove Columns from a Mining Structure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "mining structures [Analysis Services], columns"
  - "removing columns"
  - "deleting columns"
  - "columns [data mining], mining structure columns"
ms.assetid: 41073ffe-9351-416b-9f0c-62634bc213f9
author: minewiskan
ms.author: owend
manager: craigg
---
# Remove Columns from a Mining Structure
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
 [Mining Structure Tasks and How-tos](mining-structure-tasks-and-how-tos.md)  
  
  
