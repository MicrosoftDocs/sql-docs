---
description: "Remove or Delete an Item or Project"
title: "Remove or Delete an Item or Project"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting project items"
  - "projects [SQL Server Management Studio], item removal"
  - "removing project items"
ms.assetid: 3fd92434-70f5-466e-bef0-7e0fd73ddb1c
author: "markingmyname"
ms.author: "maghan"
---
# Remove or Delete an Item or Project
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Project items in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] projects are Queries, Connections, and Miscellaneous files. You can remove project queries and miscellaneous files from your solution without erasing the files from storage. Remove a project or item when it is not useful in the current solution but you want to include it in another solution.  
  
### To remove a project item  
  
1.  In Solution Explorer, select the project item you want to remove.  
  
2.  On the **Edit** menu, click **Remove**.  
  
3.  On the confirmation dialog, click **Remove** to remove the item from the project.  
  
A removed item still exists on the file system. Therefore, you can add a removed item to its original solution or to another solution.  
  
#### To remove a project  
  
1.  In Solution Explorer, select the project you want to remove.  
  
2.  On the **Edit** menu, click **Remove**.  
  
3.  On the confirmation dialog, click **OK**, to remove the project from the solution.  
  
You can delete a project permanently, but you first need to remove any references to the project from SQL Server Management Studio solutions, and then use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Explorer to permanently delete the associated files from storage.  
  
#### To delete a project  
  
1.  In Solution Explorer, remove the project you want to delete from the solution.  
  
2.  In Windows Explorer, locate and select the files associated with the project or item you want to delete.  
  
3.  On the **File** menu, click **Delete**.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Add New Items to a Project](../../ssms/solution/add-new-items-to-a-project.md)  
[Add Existing Items to a Project](../../ssms/solution/add-existing-items-to-a-project.md)  
  
