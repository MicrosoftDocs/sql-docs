---
title: "Manage Checkouts | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "source controls [SQL Server Management Studio], checkouts"
  - "checkouts [SQL Server Management Studio]"
  - "checking out files"
ms.assetid: ddd4adba-d432-4005-9cb2-bb9ee3163d8e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Manage Checkouts
  After a file has been added to source control, you must check out the file before you can modify it. When you check a file out of source control, the source control provider creates a copy of the latest version on your local disk and removes the read-only attribute of the file. In some circumstances you might need to edit a file without checking out the file. For more information about editing a file without checking the file out, see [Edit Checked-In Files](../../2014/database-engine/edit-checked-in-files.md).  
  
 You can use [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to check out files manually or automatically. You check out files manually by opening the solution that contains the files in the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment, and then clicking the **Check Out** command. You can check out files automatically if you configure the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment to do so.  
  
 Depending on the options that your administrator sets on your source control provider, you can also check out files in exclusive or shared mode. When you check out a file exclusively, only you can modify it, and no other user can check out the file until you check it in. When you check out a file in shared mode, any number of users can check out the same file. As each user checks in the file, the source control provider attempts to merge the file with the latest server version of the file. If conflicts arise between the version that is being checked in and the latest version, the source control provider prompts the user to resolve the conflicts.  
  
 The following table describes the topics in this section.  
  
|Topic|Description|  
|-----------|-----------------|  
|[Check Out Files](../../2014/database-engine/check-out-files.md)|Provides instructions on how to check out a file so you can modify it.|  
|[Undo Checkouts](../../2014/database-engine/undo-checkouts.md)|Explains how to cancel an existing checkout.|  
|[Automatically Check Out Files Upon Edit](../../2014/database-engine/automatically-check-out-files-upon-edit.md)|Explains how to configure source control to check out a file when you start to edit it.|  
  
## See Also  
 [Manage Checkins](../../2014/database-engine/manage-checkins.md)   
 [Edit Checked-In Files](../../2014/database-engine/edit-checked-in-files.md)  
  
  
