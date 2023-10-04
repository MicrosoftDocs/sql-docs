---
title: "MSSQLSERVER_7920"
description: "MSSQLSERVER_7920"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "7920 (Database Engine error)"
---
# MSSQLSERVER_7920
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|7920|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_SUMMARY_ENTRIES|  
|Message Text|Processed ENTRY_COUNT entries in system catalog for database ID D_ID.|  
  
## Explanation  
This is an informational message returned by all DBCC CHECK commands except DBCC CHECKALLOC. The value returned is the total number of rowsets checked.  
  
## User Action  
None  
  
