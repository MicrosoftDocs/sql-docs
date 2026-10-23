---
title: SQL Server In-Memory OLTP Internals
description: Learn about the implementation of SQL Server In-memory OLTP technology, which declares tables as memory optimized to enable In-Memory OLTP capabilities.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, ryanston
ms.date: 06/04/2026
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: concept-article
---
# SQL Server In-Memory OLTP internals

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## In-Memory OLTP

In-Memory OLTP helps you take advantage of large amounts of memory and many dozens of cores to increase performance for OLTP operations by up to 30 to 40 times. This article describes the implementation of SQL Server 2016's In-memory OLTP technology as of SQL Server 2016 RTM.

Use In-Memory OLTP to declare memory optimized tables, which are fully transactional and accessible by using Transact-SQL. Compile Transact-SQL stored procedures, triggers, and scalar UDFs to machine code for further performance improvements on memory-optimized tables. The engine is designed for high concurrency with no blocking.

## Download whitepaper

**Writer**: Kalen Delaney

**Technical Reviewers**: Sunil Agarwal and Jos de Bruijn

**Published**: June 2016

**Download**: [SQL Server In-Memory OLTP Internals for SQL Server 2016](https://download.microsoft.com/download/8/3/6/8360731A-A27C-4684-BC88-FC7B5849A133/SQL_Server_2016_In_Memory_OLTP_White_Paper.pdf).

## Related content

- [In-Memory OLTP overview and usage scenarios](overview-and-usage-scenarios.md)
- [Introduction to Memory-Optimized Tables](introduction-to-memory-optimized-tables.md)
- [Requirements for using memory-optimized tables](requirements-for-using-memory-optimized-tables.md)
