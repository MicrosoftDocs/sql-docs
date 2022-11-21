---
description: "MSSQLSERVER_3989"
title: MSSQLSERVER_3989
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3989 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_3989
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|3989|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|XACT_UNSUPPORT_PARALLEL_TRAN3|
|Message Text|New request is not allowed to start because it should come with valid transaction descriptor.|

## Explanation

This error occurs when you execute a distributed query that joins multiple tables hosted by remote instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] while the `XACT_ABORT` session setting is **ON**. An error message similar to the following is reported to the user:

> Msg 3989, Level 16, State 1, Line #  
New request is not allowed to start because it should come with valid transaction descriptor.

## Cause

There are some design limitations in the way [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] handles distributed queries (DQs) when the following conditions are true:

- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] joins multiple tables of one remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source.
- The session that is issuing the query is not enlisted in a distributed transaction.

In this situation, an attempt to run the query may raise either of the two errors that are mentioned in the **Explanation** section.

## User action

To work around the issue, enclose the distributed query in a 'begin distributed transaction' statement:

```sql
BEGIN DISTRIBUTED TRANSACTION
/*The actual Distributed Query goes next, outside of comments*/
COMMIT TRANSACTION
```
