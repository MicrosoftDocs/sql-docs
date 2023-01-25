---
description: "Choose Search Folders Dialog Box (Visual Studio)"
title: "Choose Search Folders Dialog Box (Visual Studio)"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: ui-reference
ms.assetid: 2eaba888-68b2-4bc1-8f62-e96e710c3db9
author: "markingmyname"
ms.author: "maghan"
---
# Choose Search Folders Dialog Box (Visual Studio)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
Allows you to assemble, save, and revise your own named sets of search folders, and to specify the order in which they are searched. To display this dialog box, select the **Browse (...)** button beside the **Look in** dropdown list on the Find in Files or Replace in Files, Find and Replace Window.  
  
Add folders to the **Selected folders** list, type a name for this folder set in the **Folder set** field, and click **Apply** to save it. This custom search scope can thereafter be chosen by name from the **Look in** dropdown lists in **Find in Files** and **Replace in Files**. To remove a custom folder set from the **Look in** lists, select its name in the **Folder set** field and click **Delete**.  
  
## Options  
The following controls are available to help you assemble, save, and revise your own named sets of search folders.  
  
**Folder set**  
Lists the directories available for search. To create a new folder set, enter a name, add a set of search folders to the **Selected folders** list, and then click **Apply**.  
  
**Apply**  
Save the set of search folders listed in **Selected folders** list as a named folder set. This folder set then can be chosen in the **Look in** field on all tabs of the **Find and Replace** window. Leaves the Choose Search Folders dialog box open.  
  
**Delete**  
Remove the selected folder set from the **Folder set** field, and from the **Look in** field on all tabs of the **Find and Replace** window.  
  
**Available folders**  
Select a drive or folder from this drop-down list to populate the **Folders list**.  
  
**Folders list**  
Lists the drives and folders available within the volume selected in the **Available folders** drop-down list. Double-click to expand any drive or folder listed. Select a folder, or hold down the SHIFT or CONTROL keys to select multiple folders. Click **Add (>)** to include selected folders in the **Selected folders** list.  
  
**Parent**  
Move the selection in the **Folder list** up one level in the folder hierarchy.  
  
**Add (>)**  
Add folders selected in the **Folder list** to the **Selected folder** list.  
  
**Remove (<)**  
Remove selected folders from the **Selected folders** list.  
  
**Selected folders**  
Lists folders added from the **Folders list**. These folders will be included in the named **Folder set**.  
  
**Apply**  
Save the set of search folders listed in **Selected folders** list as a named folder set. This folder set then can be chosen in the **Look in** field on all tabs of the **Find and Replace** window. Closes the Choose Search Folders dialog box.  
