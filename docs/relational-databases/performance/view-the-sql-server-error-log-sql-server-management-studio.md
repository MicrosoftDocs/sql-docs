---
title: "View the SQL Server error log (SSMS)"
description: Learn about the SQL Server error log, which contains user-defined events and certain system events you can use for troubleshooting.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/19/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "viewing logs"
  - "displaying logs"
  - "errors [SQL Server], logs"
  - "logs [SQL Server], SQL Server error logs"
  - "logs [SQL Server], viewing"
---
# View the SQL Server error log in SQL Server Management Studio (SSMS)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log contains user-defined events and certain system events you can use for troubleshooting. 

## View the logs

1. In SQL Server Management Studio, select **Object Explorer**. To open **Object Explorer**, select `F8`. Or on the top menu, select **View**, and then select **Object Explorer**:
    
    :::image type="content" source="media/view-the-sql-server-error-log-sql-server-management-studio/object-explorer.png" alt-text="Screenshot of the Object Explorer in the SSMS menu.":::

1. In **Object Explorer**, connect to an instance of SQL Server, and then expand that instance.
  
1. Find and expand the **Management** section (assuming you have permissions to see it).

1. Right-click **SQL Server Logs**, select **View**, and then choose **SQL Server Log**.

    :::image type="content" source="media/view-the-sql-server-error-log-sql-server-management-studio/view-sqlserver-log-ssms.png" alt-text="View the SQL Server Log in SSMS.":::
 
1. The **Log File Viewer** appears (it might take a moment) with a list of logs for you to view.

## Related content

- [Configure SQL Server Error Logs](../../database-engine/configure-windows/scm-services-configure-sql-server-error-logs.md)
- [sp_readerrorlog](../system-stored-procedures/sp-readerrorlog-transact-sql.md)
