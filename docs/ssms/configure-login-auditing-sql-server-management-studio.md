---
description: "Configure Login Auditing (SQL Server Management Studio)"
title: "Configure Login Auditing (SQL Server Management Studio)"
ms.custom: seo-lt-2019
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "auditing [SQL Server]"
  - "audits [SQL Server], logins"
  - "logins [SQL Server], auditing"
ms.assetid: 16961116-57ac-4eef-8037-791b26ade548
author: "markingmyname"
ms.author: "maghan"
---
# Configure Login Auditing (SQL Server Management Studio)
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]
This topic describes how to configure login auditing in SQL Server to monitor [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] login activity. Login auditing can be configured to write to the error log on the following events.  
  
-   Failed logins  
  
-   Successful logins  
  
-   Both failed and successful logins  
  
You must restart SQL Server before this option will take effect.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To configure login auditing  
  
1.  In SQL Server Management Studio, connect to an instance of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] with Object Explorer.  
  
2.  In Object Explorer, right-click the server name, and then click **Properties**.  
  
3.  On the **Security** page, under **Login** auditing, click the desired option and close the **Server Properties** page.  
  
4.  In Object Explorer, right-click the server name, and then click **Restart**.  
  
