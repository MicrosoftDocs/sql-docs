---
description: "Move Items in a Solution"
title: "Move Items in a Solution"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "moving items"
  - "solutions [SQL Server Management Studio], item relocation"
  - "relocating items"
ms.assetid: b40a24eb-f528-4e70-b26e-5eaf6e0ed1ed
author: "markingmyname"
ms.author: "maghan"
---
# Move Items in a Solution
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Project items in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] projects are Queries, Connections, and Miscellaneous files. You can move Queries and Miscellaneous Files between projects in Solution Explorer, but Connections cannot be moved.  
  
### To move items in Solution Explorer  
  
1.  In Solution Explorer, select the item you want to move.  
  
2.  On the **Edit** menu, click **Cut**.  
  
3.  In Solution Explorer, select the destination.  
  
4.  On the **Edit** menu, click **Paste**.  
  
You can move items by dragging Query and Miscellaneous files within Solution Explorer. Dragging allows you to see the outcome of the drag operation. Moving queries from one project type to another may cause queries to be considered as miscellaneous files in the target project.  
  
> [!NOTE]  
> Moving a connected query does not move the connection to the target project. The query will lose its connection after it is moved to the target project.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Copy Items in a Solution](../../ssms/solution/copy-items-in-a-solution.md)  
  
