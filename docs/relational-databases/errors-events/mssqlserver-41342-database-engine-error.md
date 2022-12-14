---
description: "MSSQLSERVER_41342"
title: "MSSQLSERVER_41342 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "41342 (Database Engine error)"
ms.assetid: 28270d98-c543-4e7d-b40c-2200e38dce1c
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_41342
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41342|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|HK_HW_NOT_SUPPORTED|  
|Message Text|The model of the processor on the system does not support creating *construct*. This error typically occurs with older processors.|  
  
## Explanation  
Memory-optimized tables require a processor model that supports atomic compare-and-exchange operations on 128-bit values, which require the assembly instruction CMPXCHG16B. Certain older AMD processor models do not support the CMPXCHG16B instruction. Also, certain virtualization environments do not enable this instruction by default.  
  
## User Action  
Upgrade your processor. If your processor supports the instruction and you are running SQL Server in a virtual machine, change the configuration to support the instruction CMPXCHG16B.  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
