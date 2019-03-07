---
title: "Undo Checkouts | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "VisualStudio.SourcControl.UndoCheckDialog"
helpviewer_keywords: 
  - "checking out files"
  - "checkout source controls [SQL Server]"
  - "undoing checkouts"
ms.assetid: a6596b20-3aa5-4dc4-a4c5-3649f1f5a20e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Undo Checkouts
  You can use the **Undo Checkout** command to cancel an existing checkout. This is particularly useful when you have modified and saved a file, and then later need to roll back your changes.  
  
 Depending on the options you set in the **Undo Checkout Advanced Options** dialog box, the Studio environment either leaves the working copy of the item on your local disk or replaces it with the latest source-controlled version. If someone has modified the item outside the context of the source control system, the retrieved version may not be the latest one.  
  
### To undo a checkout  
  
1.  In Solution Explorer, select the project.  
  
2.  On the **File** menu, point to **Source Control**, and then click **Undo Checkout**.  
  
3.  In the **Undo Checkout** dialog box, select the appropriate options, and then click the **Undo Checkout** button.  
  
     **Columns**  
     Identify the columns to display and the order in which they are displayed.  
  
     **Flat View**  
     Display the items as flat lists under their source control connection.  
  
     **Name**  
     Displays the names of the items for which to undo the checkout. Items appear with the check boxes next to them selected. If you do not want to undo the checkout of an item, clear its check box.  
  
     **Options**  
     Displays source control plug-in-specific undo checkout options when the arrow to the right of the button is clicked.  
  
     **Sort**  
     Sort the order of the display columns.  
  
     **Tree View**  
     Display the folder and item hierarchy for items on which you are reversing the checkout.  
  
     **Undo Checkout**  
     Revert the checkout, discarding any changes to the checked-out file.  
  
## See Also  
 [Check Out Files](../../2014/database-engine/check-out-files.md)   
 [Manage Checkouts](../../2014/database-engine/manage-checkouts.md)  
  
  
