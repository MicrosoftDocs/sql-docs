---
title: "MSSQLSERVER_2539"
description: "MSSQLSERVER_2539"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "2539 (Database Engine error)"
---
# MSSQLSERVER_2539
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2539|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_ALLOCATION_SUMMARY_FOR_DATABASE|  
|Message Text|Total number of extents = EXTENTS, used pages = USED_PAGES, reserved pages = RESERVED_PAGES in this database.|  
  
## Explanation  
This information is part of the output from the DBCC CHECKALLOC command. This information is the summary of allocated extents, used pages, and reserved pages for the specified database.  
  
## User Action  
None  
  
