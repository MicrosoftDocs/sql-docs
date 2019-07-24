---
title: "Save Scripts as Projects or Solutions | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 72dfd37f-dbe7-4d1d-bda6-7eb54c7922d3
author: stevestein
ms.author: sstein
manager: craigg
---
# Save Scripts as Projects or Solutions
  Developers familiar with [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio will welcome Solution Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. The scripts that support your business can be grouped into script projects, and the script projects can be managed together as a solution. When scripts are placed in script projects and solutions they can be opened together as a group, or saved together to a source control product such as Visual SourceSafe. Script projects include the connection information for the scripts to execute properly, and can include non-script files such as a supporting text file.  
  
 The following practice creates a short script that queries the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, placed in a script project and solution.  
  
## Using Script Projects and Solutions  
  
#### To create a script project and solution  
  
1.  Open [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], and connect to a server with Object Explorer.  
  
2.  On the **File** menu, point to **New**, and then click **Project**. The **New Project** dialog box opens.  
  
3.  In the **Name** text box, type **StatusCheck**, click **SQL Server Scripts** in **Templates**, and then click **OK** to open a new solution and script project.  
  
4.  In Solution Explorer, right-click **Connections**, and then click **New Connection**. The **Connect to Server** dialog box opens.  
  
5.  In the **Server name** list box, type the name of your server.  
  
6.  Click **Options**, and then click the **Connection Properties** tab.  
  
7.  In the **Connect to database** box, browse the server, select the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database, and then click **Connect**. The connection information including the database is added to the project.  
  
8.  If the Properties window is not displayed, click the new connection in Solution Explorer, and then press F4. The properties for the connection appear, and show information about the connection including the **Initial Database** as [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)].  
  
9. In Solution Explorer, right-click the connection, and then click **New Query**. A new query called **SQLQuery1.sql** is created, connected to the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database on your server, and added to your script project.  
  
10. In Query Editor, type the following query to determine how many work orders have due dates, before the work order starting dates. (You can copy and paste the code from the Tutorial window.)  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT COUNT(WorkOrderID)  
    FROM Production.WorkOrder  
    WHERE DueDate < StartDate;  
  
    ```  
  
    > [!NOTE]  
    >  If you need more room to type your query, press SHIFT+ALT+ENTER, to switch to full-screen mode.  
  
11. In Solution Explorer, right-click **SQLQuery1**, and then click **Rename**. Type **Check Workorders.sql** as the new name for the query and press ENTER.  
  
12. To save your solution and script project, on the **File** menu, click **Save All**.  
  
## Next Task in Lesson  
 [Summary: Solutions and Script Projects](lesson-3-4-summary-solutions-and-script-projects.md)  
  
  
