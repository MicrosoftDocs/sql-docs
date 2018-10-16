---
title: "View a List of Modified Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "VisualStudio.SourceControl.CheckinWindow"
helpviewer_keywords: 
  - "listing modified files"
  - "modified files list [SQL Server]"
  - "checking in files"
ms.assetid: 1b053719-8500-4300-a169-fffca5801dd0
author: mashamsft
ms.author: mathoma
manager: craigg
---
# View a List of Modified Files
  You can use the **Pending Checkins** window to display at all times a list of the checked-out files in the current solution, and to check in these files with a single button click.  
  
### To view a list of modified files  
  
1.  On the **View** menu, click **Pending Checkins**.  
  
2.  To check in the selected files, click **Check In**. Alternatively, you can dock the **Pending Checkins** window on the right side of the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment so that you can check in the files when you are finished working.  
  
     **Check In**  
     Checks in the solution.  
  
     **Comments**  
     Associates a plain text comment with the pending check in. A comment is created and associated with each version of a project, and stored in the source control database.  
  
     **Options**  
     Specifies the actions that source control should take when you click the **Check In** button.  
  
    -   **Keep All Checked Out**  
  
         Specifies that your changes should be written to the source control provider but that the files should remain checked out.  
  
     **Sort**  
     Specifies the sort order for the columns displayed in the Items list.  
  
     **Columns**  
     Displays a list of the columns you can add to the window's Items list.  
  
     **Tree View**  
     Displays the folder and file hierarchy for the solution or project you are checking in.  
  
     **Flat View**  
     Displays the files you are checking in as flat lists under their source control connection.  
  
     **Compare Versions**  
     Opens the Visual SourceSafe **Difference Options** dialog box, which compares a selected file in your development environment project to any other selected file and shows you the differences, if any.  
  
     **Undo checkout**  
     Reverses the checkout of all items selected in the **Pending Checkins** window.  
  
     **Name**  
     Displays a list of the items available for check in. Items appear with the check box next to them selected. If you do not want to check in a particular item, clear the check box next to it.  
  
## See Also  
 [Manage Checkins](../../2014/database-engine/manage-checkins.md)   
 [Manage Checkouts](../../2014/database-engine/manage-checkouts.md)  
  
  
