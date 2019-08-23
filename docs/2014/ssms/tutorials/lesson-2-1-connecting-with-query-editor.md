---
title: "Connecting with Query Editor | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 48725f54-a7b6-4b79-948e-965c1fe4eef1
author: stevestein
ms.author: sstein
manager: craigg
---
# Connecting with Query Editor
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] permits you to write or edit code while disconnected from the server. This can be useful when the server is not available or when you want to conserve scarce server or network resources. You can also change the connection of Query Editor to a new instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without opening a new Query Editor window or retyping your code.  
  
## Coding Offline  
  
#### To write code offline and then connect to different servers  
  
1.  On the [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] toolbar, click **Database Engine Query** to open the Query Editor.  
  
2.  In the **Connect to Database Engine** dialog box, click **Cancel**. The Query Editor opens, and the title bar for the Query Editor indicates that you are not connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
3.  In the code pane, type the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statements:  
  
    ```  
    SELECT * FROM Production.Product;  
    GO  
    ```  
  
     At this point you can connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by clicking **Connect**, **Execute**, **Parse**, or **Display Estimated Execution Plan**, all of which are available from either the **Query** menu, the Query Editor toolbar, or from the shortcut menu when you right-click in the Query Editor window. For this practice, we'll use the toolbar.  
  
4.  On the toolbar, click the **Execute** button to open the **Connect to Database Engine** dialog box.  
  
5.  In the **Server name** text box, type your server name, and then click **Options**.  
  
6.  On the **Connection Properties** tab, in the **Connect to database** list, browse the server to select [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] and then click **Connect**.  
  
7.  To open another Query Editor window with the same connection, on the toolbar click **New Query**.  
  
8.  To change connections, right-click in the Query Editor window, point to **Connection**, and then click **Change Connection**.  
  
9. In the **Connect to SQL Server** dialog box, select another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if available, and then click **Connect**.  
  
 This new feature of Query Editor enables you to easily run the same code on several servers. This may be useful for maintenance actions involving similar servers.  
  
## Next Task in Lesson  
 [Adding Indentation](lesson-2-2-adding-indentation.md)  
  
  
