---
title: "MSSQLSERVER_41333"
description: "MSSQLSERVER_41333"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "41333 (Database Engine error)"
---
# MSSQLSERVER_41333
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41333|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|CROSS_CONTAINER_ISOLATION_FAILURE|  
|Message Text|The following transactions must access memory optimized tables and natively compiled stored procedures under snapshot isolation: RepeatableRead transactions, Serializable transactions, and transactions that access tables that are not memory optimized in RepeatableRead or Serializable isolation.|  
  
## Explanation  
There are restrictions against the user of the higher isolation levels between disk based transactions and XTP transactions.  
  
## User Action  
Don't attempt high level isolation operations on memory-optimized tables (and natively compiled procedures) and disk based tables.  
  
For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
  
## See Also  
[In-Memory OLTP &#40;In-Memory Optimization&#41;](~/relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
