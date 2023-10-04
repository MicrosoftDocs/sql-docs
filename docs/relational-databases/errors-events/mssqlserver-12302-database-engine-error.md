---
title: "MSSQLSERVER_12302"
description: "MSSQLSERVER_12302"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "12302 (Database Engine error)"
---
# MSSQLSERVER_12302
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|12302|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_UNSUPPORTED_COMPUTED_COLUMNS|  
|Message Text|Updating columns that are part of the PRIMARY KEY constraint is not supported with '*construct*'.|  
  
## User Action  
Do not update columns that are part of the primary key constraint.  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
