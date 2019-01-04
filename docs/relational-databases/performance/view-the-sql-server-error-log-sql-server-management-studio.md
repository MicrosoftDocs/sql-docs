---
title: "View the SQL Server error log (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "09/29/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing logs"
  - "displaying logs"
  - "errors [SQL Server], logs"
  - "logs [SQL Server], SQL Server error logs"
  - "logs [SQL Server], viewing"
ms.assetid: 55f468ba-146c-4ab3-95cd-d35d051afd12
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# View the SQL Server error log (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains user-defined events and certain system events you can use for troubleshooting. 

## View the logs

1. In SQL Server Management Studio, select **Object Explorer**. To open **Object Explorer**, select F8. Or on the top menu, select **View**, and then select **Object Explorer**:
    
    ![Object_Explorer](../../relational-databases/performance/media/object-explorer.png) 

2. In **Object Explorer**, connect to an instance of SQL Server, and then expand that instance.
  
3. Find and expand the **Management** section (assuming you have permissions to see it).

4. Right-click **SQL Server Logs**, select **View**, and then choose **SQL Server Log**.

    ![View_SQLServer_Log_SSMS](../../relational-databases/performance/media/view-sqlserver-log-ssms.png) 
 
5. The **Log File Viewer** appears (it might take a moment) with a list of logs for you to view.
  
  ## See also
  For more information, see [MSSQLTips.com's](https://www.mssqltips.com/) helpful post [Identify location of the SQL Server Error Log file](https://www.mssqltips.com/sqlservertip/2506/identify-location-of-the-sql-server-error-log-file/).

