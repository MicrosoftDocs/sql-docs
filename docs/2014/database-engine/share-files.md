---
title: "Share Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "sharing files"
  - "file sharing [SQL Server]"
  - "version control services [SQL Server], file sharing"
ms.assetid: 645f5c0a-e949-4e87-8988-85e4d6128464
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Share Files
  You can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to share items across source control projects. When you share an item, any changes to the item are reflected in every project to which the item is shared.  
  
 Item sharing provides the following advantages to source control users:  
  
-   Makes it unnecessary for each project that uses the shared item to store a separate copy of the item, conserving disk space on both the source control client and server. The source control provider stores the shared item in one central location, and every project to which it is shared stores a pointer to that location.  
  
-   Avoids version incompatibilities. Because every project to which the item is shared uses the same version of the item, you avoid the conflicts that arise when each copy of an item is changing independently within multiple projects.  
  
### To share an item  
  
1.  In Solution Explorer, select either the folder or project in which you want to place the shared files.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Share**.  
  
3.  In the **Share with** dialog box, browse the directory list for the item you want to share, and click that item.  
  
4.  Click **Share**.  
  
## See Also  
 [Source Control Basics](../../2014/database-engine/source-control-basics.md)  
  
  
