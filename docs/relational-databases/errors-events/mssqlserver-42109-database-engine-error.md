---
title: "MSSQLSERVER_42109"
description: "MSSQLSERVER_42109"
author: azaricstefan
ms.author: stefanazaric
ms.date: "08/05/2021"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "42109 (Database Engine error)"
---
# MSSQLSERVER_42109

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|42109|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|GATEWAY_SQL_POOL_CONNECT_ACTIVATION_TIMEOUT|  
|Message Text|The SQL pool is warming up. Please try again.|  
  
## Explanation  

Synapse SQL pool is warming up and will be available soon. This error is transitive and should succeed with next connection.
  
## User Action  

Retry connecting to the Synapse SQL pool.
