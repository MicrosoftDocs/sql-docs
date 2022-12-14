---
description: "MSSQLSERVER_2538"
title: "MSSQLSERVER_2538 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "2538 (Database Engine error)"
ms.assetid: 0a0f7d79-f1ba-4749-8804-fb660cca3492
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_2538
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|2538|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_ALLOCATION_SUMMARY_PER_FILE|  
|Message Text|File FILE. Number of extents = EXTENTS, used pages = USED_PAGES, reserved pages = RESERVED_PAGES.|  
  
## Explanation  
This information is part of the output from the DBCC CHECKALLOC command. This information is the per-file summary of allocated extents, used pages, and reserved pages for the specified database.  
  
## User Action  
None  
  
