---
title: "Change Source Control | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
f1_keywords: 
  - "IDD_SCC_CONNECTION_DIALOG"
helpviewer_keywords: 
  - "Change Source Control dialog box"
ms.assetid: e6a5d83c-5809-4c56-907a-73d0c7ccdd7a
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Change Source Control
  Creates and manages the connections and bindings that link a locally saved solution or project to a source control database folder.  
  
## Dialog Box Access  
 In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], select an item in Solution Explorer. On the **File** menu, click **Source Control**, and then **Change Source Control**.  
  
> [!NOTE]  
>  This dialog box is also available by right-clicking the item in Solution Explorer.  
  
## Options  
 **Bind**  
 Associate selected items with a specified source control server location. For example, you can use this button to bind to the last known source control server folder and database. If a recent server folder or database cannot be found, you are prompted to specify another.  
  
 **Browse**  
 Navigate to a new source control server location for the specified item.  
  
 **Columns**  
 Identify columns to display and the order in which they are displayed.  
  
 **Connect**  
 Create a connection between selected items and the source control server.  
  
 **Connected**  
 Displays the connection status of a selected solution or project.  
  
 **Disconnect**  
 Disconnect the local copy of a solution or project on your computer from its master copy in the database. Use this command before disconnecting your computer from the source control server, for example, when working offline on your laptop.  
  
 **OK**  
 Accept changes made in the dialog box.  
  
 **Provider**  
 Displays the name of your source control plug-in.  
  
 **Refresh**  
 Refreshes the connection information for all projects listed in this dialog box.  
  
 **Server Binding**  
 Indicates the item's binding to a source control server.  
  
 **Server Name**  
 Displays the name of the source control server to which the corresponding solution or project is bound.  
  
 **Solution/Project**  
 Displays the name of each solution and project in the current selection.  
  
 **Sort**  
 Sort the order of displayed columns.  
  
 **Status**  
 Identifies the binding and connection status of an item. Possible options are:  
  
|**Option**|**Description**|  
|----------------|---------------------|  
|Valid|The item is correctly bound and connected to the server folder to which it belongs.|  
|Invalid|The item is incorrectly bound to or disconnected from the folder to which it belongs. Use the **Add to Source Control** command instead of **Bind** for this item.|  
|Unknown|The status of the item under source control has not yet been determined.|  
|Not Controlled|The item has not been placed under source control.|  
  
 **Unbind**  
 Display the **Source Control** dialog box to allow you to remove selected items from source control and permanently disassociate the items from their present folders.  
  
## See Also  
 [Solution Explorer Source Control](../../2014/database-engine/solution-explorer-source-control.md)  
  
  
