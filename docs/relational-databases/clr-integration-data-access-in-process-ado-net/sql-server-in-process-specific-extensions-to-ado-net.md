---
title: "SQL Server In-Process Extensions to ADO.NET"
description: Links to articles about the four main functional extensions to ADO.NET that are specifically for in-process use.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "data access [CLR integration]"
  - "ADO.NET [CLR integration]"
  - "common language runtime [SQL Server], ADO.NET"
  - "database objects [CLR integration], in-process extensions"
  - "in-process data access providers [CLR integration]"
  - "extensions [CLR integration]"
---
# SQL Server in-process specific extensions to ADO.NET

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

There are four main functional extensions to ADO.NET that are specifically for in-process use. The following articles will cover these extensions in detail.

## In this section

| Article | Description |
| --- | --- |
| [SqlContext object](sqlcontext-object.md) | This class provides access to the other extensions by abstracting the context of a caller of a SQL Server routine that executes managed code in-process. |
| [SqlPipe object](sqlpipe-object.md) | This class contains routines to send tabular results and messages to the client. |
| [SqlTriggerContext object](sqltriggercontext-object.md) | This class provides information on the context in which a trigger is run. |
| [SqlDataRecord object](sqldatarecord-object.md) | The `SqlDataRecord` class represents a single row of data, along with its related metadata, and allows stored procedures to return custom result sets to the client. |
