---
title: "View the SQL Server Error Log (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "viewing logs"
  - "displaying logs"
  - "errors [SQL Server], logs"
  - "logs [SQL Server], SQL Server error logs"
  - "logs [SQL Server], viewing"
ms.assetid: 55f468ba-146c-4ab3-95cd-d35d051afd12
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# View the SQL Server Error Log (SQL Server Management Studio)
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains user-defined events and certain system events you will want for troubleshooting. 
  

  ## How to view the logs
1.  In SSMS, select **Object Explorer**

>To **open** Object Explorer: Keyboard shortcuy is **F8**. Or, on the top menu, click View/Object Explorer 
![Object_explorer](../../relational-databases/performance/media/object-explorer.png) 


2.  In **Object Explorer**, connect to an instance of the SQL Server and then expand that instance.
  
3.  Find and expand the **Management** section (Assuming you have permissions to see it).

4.  Right-click on **SQL Server Logs**, select View, and choose **View SQL Server Log**.
 ![View_SQLServer_Log_SSMS](../../relational-databases/performance/media/view-sqlserver-log-ssms.png) 
 
5.  The Log File Viewer will appear (It might take a minute) with a list of logs for you to view.
  
6. Several people have recommended [MSSQLTips.com's](https://www.mssqltips.com/) helpful post [Identify location of the SQL Server Error Log file](https://www.mssqltips.com/sqlservertip/2506/identify-location-of-the-sql-server-error-log-file/). They have a lot of terrific information - be sure to check them out!
  
  
