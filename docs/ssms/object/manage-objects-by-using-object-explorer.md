---
title: "Manage objects by using Object Explorer"
description: "This article is an overview of managing objects using Object Explorer in SQL Server Management Studio."
author: "markingmyname"
ms.author: "maghan"
ms.date: "09/27/2023"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords:
  - "sql13.SWB.SQLSERVEROBJECTEXPLORER.DHELP"
helpviewer_keywords:
  - "Object Explorer F1 Help"
  - "OE F1 Help"
  - "OE Help"
---

# Manage objects by using Object Explorer

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

You can use Object Explorer in SQL Server Management Studio (SSMS) to manage objects such as databases, tables, and stored procedures.  
  
## Viewing objects in Object Explorer

Object Explorer uses a tree structure to group information into folders. To expand folders, Select the plus sign (+) or double-click the folder. Expand folders to show more detailed information. Right-click folders or objects to perform common tasks. Double-click objects to perform the most common task.  
  
The first time you expand a folder, Object Explorer queries the server for information to populate the tree. You can perform other functions while the tree is populating. While Object Explorer is populating the tree, you can select **Stop** to halt the process. Further actions, such as filtering the list, will only act upon the portion of the folder that was populated, unless you refresh the folder to start population again.  
  
To conserve resources when there are many objects, the folders in the Object Explorer tree don't automatically refresh their list of contents. To refresh the list of objects within a folder, right-click the folder, and then select **Refresh**.  
  
Object Explorer can only display up to 65,536 objects. After you have exceeded 65,536 visible objects, you can't scroll through additional objects in the Object Explorer tree view. To view additional objects in Object Explorer, close nodes that you aren't using or apply filtering to reduce the number of objects.  
  
## Filtering the list of objects in Object Explorer

When a folder contains a large number of objects, it may be difficult to find the object you're looking for. In such cases, use the filter feature of Object Explorer to reduce the list to a smaller size. For example, you may want to find a specific database user or the most recently created table in lists that contain hundreds of objects. Select the folder that you want to filter, and then select the filter button to open the **Filter Settings** dialog box. You can filter the list by name, create date, and sometimes schema, and provide extra filtering operators like **Starts with**, **Contains**, and **Between**.  
  
## Multi-select

Only one object can be selected at a time in Object Explorer. To select multiple items, press **F7** to open the **Object Explorer Details Page**. The **Object Explorer Details Page** supports multi-select.  
  
## Register a server from Object Explorer

When connected to a server, you can easily register the server for future use. In Object Explorer, right-click the server name, and then select **Register**. In the **Register Server** dialog box, specify where in the server group tree you want to place the server. In the **Server name** box, you can replace the server name with a more meaningful server name. For example, you could register server **APSQL02** with a more meaningful name such as "**Accounts Payable**".  
  
## Performing actions on Object Explorer nodes

You perform actions on objects by right-clicking the Object Explorer node representing the object. Each type of object supports a unique set of right-click actions. Some of the types of actions you can perform by using the right-click menus include:  
  
### Open a connected Query Editor

When Object Explorer is connected to a server, you can open a new Code Editor window using the connection settings of Object Explorer. To open a new Code Editor window, right-click the server name in Object Explorer, and then select **New Query**. To open a Code Editor window using a particular database, right-click the database name, and then select **New Query**. When opening a new query for an Analysis Services server, you can select DMX, MDX, or XMLA queries.
  
### Start PowerShell

You can start a PowerShell session by right-clicking most folders and objects in the Object Explorer tree and selecting **Start PowerShell**. This starts a PowerShell session that has the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell support enabled, and the path set to the object you right-clicked in Object Explorer. You can then enter PowerShell commands in an interactive PowerShell environment. For more information, see [SQL Server PowerShell](/powershell/sql-server/sql-server-powershell).  
  
## See also

- [Object Explorer](../../ssms/object/object-explorer.md)  
- [Open and Configure Object Explorer](../../ssms/object/open-and-configure-object-explorer.md)  
- [Connect to an Instance From Object Explorer](../../ssms/object/connect-to-an-instance-from-object-explorer.md)  
- [Object Explorer Details Pane](../../ssms/object/object-explorer-details-pane.md)  
- [Custom Reports in Management Studio](../../ssms/object/custom-reports-in-management-studio.md)  
