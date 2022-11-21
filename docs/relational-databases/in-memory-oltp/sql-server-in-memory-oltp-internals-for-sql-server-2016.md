---
title: "SQL Server in-memory OLTP internals"
description: Learn about the implementation of SQL Server In-memory OLTP technology, which declares tables as memory optimized to enable In-Memory OLTP capabilities.
author: "kevin-farlee"
ms.author: "kfarlee"
ms.date: "09/14/2016"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.custom: seo-dt-2019
ms.assetid: b14da361-a6b8-4d85-b196-7f2f13650f44
---
# SQL Server In-Memory OLTP Internals for SQL Server 2016
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

**Summary:** In-memory OLTP, frequently referred to by its code name "Hekaton", was introduced in SQL Server 2014.
This powerful technology allows you to take advantage of large amounts of memory and many dozens of
cores to increase performance for OLTP operations by up to 30 to 40 times! SQL Server 2016 is continuing
the investment in In-memory OLTP by removing many of the limitations found in SQL Server 2014, and
enhancing internal processing algorithms so that In-memory OLTP can provide even greater
improvements. This paper describes the implementation of SQL Server 2016's In-memory OLTP
technology as of SQL Server 2016 RTM. Using In-memory OLTP, tables can be declared as 'memory
optimized' to enable In-Memory OLTP's capabilities. Memory-optimized tables are fully transactional and
can be accessed using Transact-SQL. Transact-SQL stored procedures, triggers, and scalar UDFs can be
compiled to machine code for further performance improvements on memory-optimized tables. The
engine is designed for high concurrency with no blocking.    
  
**Writer:** Kalen Delaney  
  
**Technical Reviewers:** Sunil Agarwal and Jos de Bruijn  
  
**Published:** June  2016  
  
**Applies to:** SQL Server 2016  
  
To review the document, download the [SQL Server In-Memory OLTP Internals for SQL Server 2016](https://download.microsoft.com/download/8/3/6/8360731A-A27C-4684-BC88-FC7B5849A133/SQL_Server_2016_In_Memory_OLTP_White_Paper.pdf) document.   
