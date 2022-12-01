---
title: "Namespace Support in PATH Mode"
description: Learn about namespace support when using PATH mode to generate XML from a SELECT query.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "PATH FOR XML mode, namespace support"
  - "namespaces [XML in SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Namespace support in PATH mode

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Namespace support in the PATH mode is provided by using WITH NAMESPACES. For example, the following query demonstrates the WITH NAMESPACES syntax to declare a namespace ("a:") that can then be used in the subsequent SELECT statement:

```sql
WITH XMLNAMESPACES('a' as a)
SELECT 1 as 'a:b'
FOR XML PATH;
```

## Examples

These samples illustrate the use of PATH mode in generating XML from a SELECT query. Many of these queries are specified against the bicycle manufacturing instructions XML documents that are stored in the Instructions column of the ProductModel table.

## See also

- [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)
