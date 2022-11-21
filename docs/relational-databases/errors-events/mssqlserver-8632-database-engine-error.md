---
description: "MSSQLSERVER_8632"
title: MSSQLSERVER_8632
ms.custom: ""
ms.date: 10/27/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "8632 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_8632
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|8632|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|QUERY_EXPRESSION_TOO_COMPLEX|
|Message Text|Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them.|

## Explanation

Error 8632 is raised when you run a query in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that contains a large number of identifiers and constants in a single expression. An error message like the following is reported to the user:

> Server: Msg 8632, Level 17, State 2, Line 1  
Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them.

## Cause

This issue occurs because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] limits the number of identifiers and constants that can be contained in a single expression of a query. This limit is 65,535. For example, the following query only has one expression:

```sql
select a, b + c, d + e
```

This expression retrieves all five columns, calculates the addition operators, and sends three projected results to the client.

The test for the number of identifiers and constants is performed after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] expands all referenced identifiers and constants. For example, the following items may be expanded:

- The asterisk (*) in the select list
- A view
- A computed column definition

If the number after the expansion exceeds the limit, the query cannot run.

## User action

To work around this issue, rewrite your query. Reference fewer identifiers and constants in the largest expression in the query. You must make sure that the number of identifiers and constants in each expression of the query does not exceed the limit. To do this, you may have to break down a query into more than one single query. Then, create a temporary intermediate result.
