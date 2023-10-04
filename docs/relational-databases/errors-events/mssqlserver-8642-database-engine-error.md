---
title: "MSSQLSERVER_8642"
description: "MSSQLSERVER_8642"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8642 (Database Engine error)"
---
# MSSQLSERVER_8642
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8642|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|EXCHNGSTART_ERR|  
|Message Text|The query processor could not start the necessary thread resources for parallel query execution.|  
  
## Explanation  
Thread resources are low in the server.  
  
## User Action  
Reduce load on the server, and run the query again.  
  
