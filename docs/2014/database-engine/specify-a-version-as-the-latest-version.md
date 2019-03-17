---
title: "Specify a Version as the Latest Version | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "version control services [SQL Server], latest version"
  - "latest file version specified"
  - "file versions [SQL Server]"
ms.assetid: 407dffb1-3ecf-461e-835d-124781f26ee7
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Specify a Version as the Latest Version
  When you check a file into source control, the version you check in becomes the latest version; users who check out or retrieve the latest version receive local copies of the item most recently checked in.  
  
 However, in some situations, you may want to designate an earlier version of an item as the latest version. For example, you check out a file, modify the file, and then check the file in. You later decide to discard your modifications. Because you have checked in the item, undoing your original checkout is not an option. In this situation, you can designate the version you originally checked out as the latest version of the item.  
  
 You can designate the latest version by:  
  
-   **Pinning a version**. When you pin a file version, versions that are more recent than the pinned version are not deleted. In addition, you can unpin a file that you have previously pinned. When you do this, the most recently checked in version of the file becomes the latest version. However, you cannot check out a pinned file.  
  
-   **Rolling back to a specified version**. When you roll back to a version, all versions more recent than the one to which you have rolled back are deleted from source control. You can then check out the latest remaining version.  
  
### To pin a version  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], open the solution.  
  
2.  In Solution Explorer, select the file that you want to specify as the latest version.  
  
3.  On the **File** menu, point to **Source Control** and click **ViewHistory**.  
  
4.  In the **History of** \<file> dialog box, select the version that you want to specify as the latest, and click **Pin**.  
  
     A pin symbol appears next to the version you have selected, indicating that it is the current file version. If you have a different version loaded in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], you are prompted to reload the file.  
  
### To roll back to a version  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], open the solution.  
  
2.  In Solution Explorer, select the item that you want to specify as the latest version.  
  
3.  On the **File** menu, point to **Source Control** and click **History**.  
  
4.  In the **History Options** dialog box, click **OK** to display the **History of File** dialog box.  
  
5.  In the **History of File** box, select the version you want to specify as the latest version, and click **Rollback**.  
  
     A message appears notifying you that all versions subsequent to the one you have selected will be deleted.  
  
6.  Click **Yes** to roll back to the selected version.  
  
## See Also  
 [Manage Checkins](../../2014/database-engine/manage-checkins.md)   
 [Check In Files](../../2014/database-engine/check-in-files.md)  
  
  
