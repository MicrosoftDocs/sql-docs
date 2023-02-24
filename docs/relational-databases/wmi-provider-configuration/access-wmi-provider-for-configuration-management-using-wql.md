---
title: "Use WQL to access the WMI Provider"
description: Use this example to see how to run Windows Management Instrumentation Query Language statements for the WMI Provider for Computer Management in SQL Server.
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords: 
  - "query language [WMI]"
  - "WMI Query Language [WMI]"
  - "WQL [WMI]"
  - "WMI Provider for Configuration Management, WQL"
ms.assetid: 26499530-d93b-452b-bbe4-217ef1d11e68
author: markingmyname
ms.author: maghan
---
# Access WMI Provider for Configuration Management using WQL
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This section describes how to execute [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Management Instrumentation Query Language (WQL) statements against the WMI Provider for Computer Management.  
  
 The example uses a WQL editor, WBEMtest.exe, to run WQL queries against the WMI Provider to enumerate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services, network protocols, and aliases.  
  
### Querying services using WBEMtest  
  
1.  From the **Start** menu, click **Run**, and then enter **WBEMtest**.  
  
2.  The WBEMtest.exe dialog appears. Click **Connect**.  
  
3.  In the first text field, type the WMI Provider for Computer Management namespace: root\Microsoft\SqlServer\ComputerManagement11. Click **Connect**.  
  
4.  Click **Query**. Type a query that returns the current services running on the local computer: **SELECT \* FROM SqlService.** Click **Apply**.  
  
5.  Further refine the query by adding **WHERE ServiceName = "MSSQLSERVER"**.  
  
  
