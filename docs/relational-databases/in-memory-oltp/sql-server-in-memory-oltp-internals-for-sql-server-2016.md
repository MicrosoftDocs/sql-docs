---
title: "SQL Server In-Memory OLTP Internals for SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "09/14/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: b14da361-a6b8-4d85-b196-7f2f13650f44
author: "jodebrui"
ms.author: "jodebrui"
manager: craigg
---
# SQL Server In-Memory OLTP Internals for SQL Server 2016
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

**Summary:** In-memory OLTP, frequently referred to by its codename "Hekaton", was introduced in SQL Server 2014.
This powerful technology allows you to take advantage of large amounts of memory and many dozens of
cores to increase performance for OLTP operations by up to 30 to 40 times! SQL Server 2016 is continuing
the investment in In-memory OLTP by removing many of the limitations found in SQL Server 2014, and
enhancing internal processing algorithms so that In-memory OLTP can provide even greater
improvements. This paper describes the implementation of SQL Server 2016's In-memory OLTP
technology as of SQL Server 2016 RTM. Using In-memory OLTP, tables can be declared as 'memory
optimized' to enable In-Memory OLTP's capabilities. Memory-optimized tables are fully transactional and
can be accessed using Transact-SQL. Transact-SQL stored procedures, triggers and scalar UDFs can be
compiled to machine code for further performance improvements on memory-optimized tables. The
engine is designed for high concurrency with no blocking.    
  
**Writer:** Kalen Delaney  
  
**Technical Reviewers:** Sunil Agarwal and Jos de Bruijn  
  
**Published:** June, 2016  
  
**Applies to:** SQL Server 2016  
  
To review the document, please download the [SQL Server In-Memory OLTP Internals for SQL Server 2016](https://download.microsoft.com/download/8/3/6/8360731A-A27C-4684-BC88-FC7B5849A133/SQL_Server_2016_In_Memory_OLTP_White_Paper.pdf) document.   
