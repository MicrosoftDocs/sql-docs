---
title: "SQLGetTypeInfo (Visual FoxPro ODBC Driver)"
description: "SQLGetTypeInfo (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLGetTypeInfo function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLGetTypeInfo (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Level 1.

## Remarks

Returns information about the data types supported by a data source. The driver returns the information in a SQL result set. The following table lists ODBC data types and the corresponding Visual FoxPro data type.

| ODBC type | Visual FoxPro type |
| --- | --- |
| `SQL_BIGINT` | Not supported. There's no 64-bit Visual FoxPro type. |
| `SQL_BIT` | Logical |
| `SQL_CHAR` | Character |
| `SQL_DATE` | Date |
| `SQL_DECIMAL` | Numeric |
| `SQL_DOUBLE` | Double |
| `SQL_FLOAT` | Double |
| `SQL_INTEGER` | Integer |
| `SQL_LONGVARBINARY` | Memo (Binary) |
| `SQL_LONGVARCHAR` | Memo |
| `SQL_NUMERIC` | Numeric <sup>1</sup>, Currency, Float |
| `SQL_REAL` | Double |
| `SQL_SMALLINT` | Integer |
| `SQL_TIME` | Not supported. There's no Visual FoxPro `time` type. |
| `SQL_TIMESTAMP` | DateTime |
| `SQL_TINYINT` | Integer |
| `SQL_VARBINARY` | Memo (Binary) <sup>1</sup>, General |
| `SQL_VARCHAR` | Character |

<sup>1</sup> Default type

For more information about Visual FoxPro data types, see [CREATE TABLE](create-table-sql-command.md). For more information about this function, see [SQLGetTypeInfo Function](../reference/syntax/sqlgettypeinfo-function.md) in the *ODBC Programmer's Reference*.
