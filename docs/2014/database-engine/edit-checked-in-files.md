---
title: "Edit Checked-In Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "modifying checked-in files"
  - "checking in files"
ms.assetid: 560cd19f-ab22-4273-b00c-149993a630e6
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Edit Checked-In Files
  You typically must check out source-controlled files before you can edit them. However, you can configure [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] so that you can modify files you have not checked out. When doing so, your changes are held in memory until you save the files. You will then be prompted to check out the file from source control.  
  
 If you work on a team, allowing checked-in files to be edited is not recommended unless your source control provider supports both local version and server version checkouts. Most providers do not support local version checkouts. If your provider does not support local version checkouts and you edit a checked-in file, you have to merge the in-memory and server versions manually before the file can be checked in. Automatic and provider-assisted merges are unsupported in this situation.  
  
### To edit checked-in files  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  In the **Options** dialog box, expand the **Source Contro**l folder, and then click **Environment**.  
  
3.  Click **Allow checked-in items to be edited**, and then click **OK**.  
  
## See Also  
 [Manage Checkins](../../2014/database-engine/manage-checkins.md)   
 [Manage Checkouts](../../2014/database-engine/manage-checkouts.md)  
  
  
