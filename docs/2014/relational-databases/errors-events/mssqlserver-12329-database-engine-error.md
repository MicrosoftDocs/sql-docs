---
title: "MSSQLSERVER_12329 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "12329 (Database Engine error)"
ms.assetid: 43f90287-36d5-46c2-ac91-a37202dcf6d3
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_12329
    
## Details  
  
|||  
|-|-|  
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
  
  
