---
title: "MSSQLSERVER_8966"
description: "MSSQLSERVER_8966"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "8966 (Database Engine error)"
---
# MSSQLSERVER_8966
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|8966|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC3_FAILED_TO_READ_AND_LATCH_PAGE|  
|Message Text|Unable to read and latch page P_ID with latch type TYPE. OPERATION failed.|  
  
## Explanation  
The page read failed or a latch could not be taken on a PFS or GAM page. There may be latch time-out or other accompanying messages in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
  
## User Action  
Review the SQL error log for accompanying messages and resolve these errors.  
  
