---
title: "MSSQLSERVER_3452"
description: "MSSQLSERVER_3452"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3452 (Database Engine error)"
---
# MSSQLSERVER_3452
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3452|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_CHECKIDENTITY|  
|Message Text|Recovery of database '%.*ls' (%d) detected possible identity value inconsistency in table ID %d. Run DBCC CHECKIDENT ('%.\*ls').|  
  
## Explanation  

During an upgrade or while applying a servicing update, the process to recover the identity values in the database found an inconsistency in the metadata.  
  
## User Action

Run DBCC CHECKIDENT.  
  
