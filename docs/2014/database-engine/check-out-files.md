---
title: "Check Out Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "Visual Studio.SourceControl.CheckOutDialog"
helpviewer_keywords: 
  - "checking out files"
ms.assetid: cc033727-51bb-4b58-a12b-8977ce61ff56
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Check Out Files
  Unless you have configured the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] environment to permit checked-in files to be edited, you must check out a file before you can modify it. When you check out a file, a copy of the file version is copied to your local disk, and the read-only attribute of the file is removed.  
  
 You can check files out either exclusively or in shared mode. When you check out a file exclusively, no other user can check out the file until you check it in. When you check out a file in shared mode, other users can check out and modify the file, and when you check it in, you may need to merge the version you have checked out with the versions created by other users.  
  
 Use the **Check Out** command to check out source-controlled projects and files. If you use this command to check out a solution or project, all the files in the solution or project are also checked out. However, checking out an individual source code file does not result in the check out of the project or solution to which it belongs.  
  
> [!NOTE]  
>  If the [!INCLUDE[msCoName](../includes/msconame-md.md)] Visual SourceSafe database for your project is configured to allow multiple checkouts, and you want to check out a file exclusively, you must clear the **Allow multiple checkouts** option in the **Advanced Check Out Options** dialog box before checking out the file. You must restart the [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] for this setting to take effect.  
  
### To check out a file  
  
1.  In Solution Explorer, select the project or file.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Check Out for Edit**.  
  
3.  If the **Check Out for Edit** dialog box is displayed, select the items you want, and click **Check Out**. If you have configured the [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] environment not to display the **Check Out** dialog box, the items selected in Solution Explorer and any children they might have are checked out immediately.  
  
     **Check Out**  
     Check out all the selected items.  
  
     **Columns**  
     Identify the columns to display and the order in which they are displayed.  
  
     **Comments**  
     Specify a comment to associate with the checkout operation.  
  
     **Don't Show Check Out dialog box when checking out items**  
     Suppress the dialog box during checkout operations.  
  
     **Flat View**  
     Display the items you are checking out as flat lists under their source control connection.  
  
     **Edit**  
     Modify an item without checking it out. The **Edit** button appears only if you have [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] configured to support the editing of checked-in files.  
  
     **Name**  
     Displays the names of the items available for checkout. Items that are selected appear with check boxes next to them. If you do not want to check out a particular item, clear its check box.  
  
     **Options**  
     Displays source control plug-in-specific checkout options when the arrow to the right of the button is clicked.  
  
     **Sort**  
     Sort the order of the displayed columns.  
  
     **Tree View**  
     Display the folder and file hierarchy for the item you are checking out.  
  
## See Also  
 [Edit Checked-In Files](../../2014/database-engine/edit-checked-in-files.md)   
 [Automatically Check Out Files Upon Edit](../../2014/database-engine/automatically-check-out-files-upon-edit.md)   
 [Undo Checkouts](../../2014/database-engine/undo-checkouts.md)   
 [Manage Checkouts](../../2014/database-engine/manage-checkouts.md)  
  
  
