---
title: "Copy Items in a Solution | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "copying items"
  - "projects [SQL Server Management Studio], copying"
  - "solutions [SQL Server Management Studio], item copying"
ms.assetid: f95f084e-9f3d-4d15-90b4-1094ab2eda51
caps.latest.revision: 3
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Copy Items in a Solution
You can copy items using Solution Explorer or Windows Explorer.  
  
### To copy items within Solution Explorer  
  
1.  In Solution Explorer, select the item you want to copy.  
  
2.  On the **Edit** menu, click **Copy**.  
  
3.  In Solution Explorer, select the destination project.  
  
4.  On the **Edit** menu, click **Paste**.  
  
> [!NOTE]  
> Connections cannot be copied between projects. When copying a query with an associated connection to another project, its associated connection is not copied to the target project. You must manually create the connection in the target project.  
  
## See Also  
[Solution Explorer](../../ssms/solution/solution-explorer.md)  
[Move Items in a Solution](../../ssms/solution/move-items-in-a-solution.md)  
  
