---
description: "MSSQLSERVER_42109"
title: "MSSQLSERVER_42109 | Microsoft Docs"
ms.custom: ""
ms.date: "08/05/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "42109 (Database Engine error)"
ms.assetid: 5e5acb07-16ca-4329-8210-cd2bab0c904f
author: azaricstefan
ms.author: stefanazaric
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
