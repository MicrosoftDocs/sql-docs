---
description: "MSSQLSERVER_17083"
title: "MSSQLSERVER_17083 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17083 (Database Engine error)"
ms.assetid: 6c83737d-0531-4fd9-88f6-2da5a150532d
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17083
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|17083|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|P3_HEKATON_NOT_ATOMIC|  
|Message Text|The body of a natively compiled stored procedure must be an ATOMIC block.|  
  
## Explanation  
The body of a natively compiled stored procedure did not have an ATOMIC block.  
  
## User Action  
A natively compiled stored procedure must contain an ATOMIC block. For example:  
  
```  
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE= N'us_english')  
```  
  
For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
