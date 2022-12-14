---
description: "MSSQLSERVER_1461"
title: "MSSQLSERVER_1461 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1461 (Database Engine error)"
ms.assetid: fce10907-4753-441b-b624-f28e00ed7520
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1461
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1461|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_SAFETY_MISMATCH|  
|Message Text|Different database mirroring safety levels among servers were detected for database "%.*ls". The FULL safety level will be used.|  
  
## Explanation  
Mirroring connections were broken when the transaction safety level was modified because the transaction safety settings were inconsistent on the principal and mirror databases. The default safety setting of full-transaction safety will be used. The session will run in high-safety mode.  
  
## User Action  
To set transaction safety off, rerun the ALTER DATABASE *database_name* SET PARTNER SAFETY OFF statement on the principal database.  
  
