---
description: "MSSQLSERVER_17084"
title: "MSSQLSERVER_17084 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17084 (Database Engine error)"
ms.assetid: e579d104-3307-4edd-8587-b14ecbc02ed9
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17084
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|17084|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|P3_ATOMIC_WITH_MISSING_OPTION|  
|Message Text|The WITH clause of BEGIN ATOMIC statement must specify a value for the option '%ls'.|  
  
## Explanation  
The WITH clause of BEGIN ATOMIC statement did not specify a value for an option.  
  
## User Action  
**ATOMIC** blocks require values for the **WITH** options **TRANSACTION ISOLATION LEVEL** and **LANGUAGE**. For example::  
  
```  
BEGIN ATOMIC WITH (TRANSACTION ISOLATION LEVEL = SNAPSHOT, LANGUAGE= N'us_english')  
```  
  
For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
