---
title: "MSSQLSERVER_12329"
description: "MSSQLSERVER_12329"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "12329 (Database Engine error)"
---
# MSSQLSERVER_12329
 [!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|12329|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_UNSUPPORTED_NON_LATIN_CODEPAGE|  
|Message Text|The data types char(n) and varchar(n) using a collation that has a code page other than 1252 are not supported with  *construct*.|  
  
## Explanation  
Do not use the data types char(n) and varchar(n) using a collation that has a code page other than 1252.  
  
## User Action  
One unexpected situation that can generate this error is:  
  
```  
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = 'us_english')  
```  
  
Use this instead:  
  
```  
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE = N'us_english')  
```  
  
