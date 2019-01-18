---
title: "Check In Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "VisualStudio.SourceControl.CheckInDialog"
helpviewer_keywords: 
  - "checking in files"
ms.assetid: 0657a387-8411-4406-bef9-d262a5bfa269
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Check In Files
  To make source-controlled files that you have modified available to other users, you must check the files into source control. When you check in a file, the version you check in is written to the source control provider and becomes the latest version of the file.  
  
 You can use the **Check In** command to check in files. If you use this command to check in a solution or project, all the files in the solution or project are also checked in. However, checking in an individual source code file does not result in the check-in of the project or solution to which it belongs.  
  
### To check in a file  
  
1.  In Solution Explorer, right-click the file to check in, and then click **Check In**.  
  
2.  If the **Check In** dialog box appears, select the appropriate options, and then click **OK**.  
  
     **Check In**  
     Check in all the selected items.  
  
     **Columns**  
     Identify the columns to display and the order in which they are displayed.  
  
     **Comments**  
     Add a comment to associate with the check-in operation.  
  
     **Don't show Check in dialog box when checking in items**  
     Suppress the dialog box during check-in operations.  
  
     **Flat View**  
     Display the files you are checking in as flat lists under their source control connection.  
  
     **Name**  
     Displays the names of the items to check in. Items appear with the check boxes next to them selected. If you do not want to check in a particular item, clear its check box.  
  
     **Options**  
     Displays source control plug-in-specific check-in options when the arrow to the right of the button is clicked.  
  
     **Sort**  
     Sort the order of the display columns.  
  
     **Tree View**  
     Display the folder and file hierarchy for the items you are checking in.  
  
 If the file you have checked in is not part of a shared checkout, the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] environment checks in the file immediately. Otherwise, it may prompt you to merge your version with versions created by other users.  
  
## See Also  
 [View a List of Modified Files](../../2014/database-engine/view-a-list-of-modified-files.md)   
 [Manage Checkins](../../2014/database-engine/manage-checkins.md)  
  
  
