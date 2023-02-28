---
title: "MSSQLSERVER_611"
description: "MSSQLSERVER_611"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "611 (Database Engine error)"
---
# MSSQLSERVER_611
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|611|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|VAR_SIZE_TOO_BIG|  
|Message Text|Cannot insert or update a row because total variable column size, including overhead, is %d bytes more than the limit.|  
  
## Explanation  
The maximum variable column size is more than that allowed by the schema. Error 611 is returned when the variable column is over the limit in the fixed case when vardecimal storage format is enabled, or when the variable column size is over the limit allowed in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] for a compressed data record.  
  
## User Action  
Reduce the size of the record.  
  
