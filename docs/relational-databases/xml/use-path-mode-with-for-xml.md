---
title: "Use PATH Mode with FOR XML"
description: Learn how to use PATH mode with nested FOR XML queries and the TYPE directive to write less complex queries that return xml type instances.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 05/09/2024
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "PATH FOR XML mode"
  - "characters [SQL Server], XML"
  - "aliases [SQL Server], XML"
  - "FOR XML clause, PATH mode"
  - "FOR XML PATH mode"
  - "column names [SQL Server]"
  - "XPath queries [SQL Server]"
---
# Use PATH mode with FOR XML

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

As described in [FOR XML (SQL Server)](for-xml-sql-server.md), the `PATH` mode provides a simpler way to mix elements and attributes. `PATH` mode is also a simpler way to introduce more nesting for representing complex properties. You can use `FOR XML EXPLICIT` mode queries to construct such XML from a rowset, but the `PATH` mode provides a simpler alternative to the potentially cumbersome `EXPLICIT` mode queries. `PATH` mode, together with the ability to write nested `FOR XML` queries and the `TYPE` directive to return **xml** type instances, allows you to write queries with less complexity.

In `PATH` mode, column names or column aliases are treated as XPath expressions. These expressions indicate how the values are being mapped to XML. Each XPath expression is a relative XPath that provides the item type. Types include the attribute, element, scalar value, and the name and hierarchy of the node that is generated, relative to the row element.

This section describes mapping columns in a rowset under various conditions, and provides examples.

## In this section

- [Columns without a name](columns-without-a-name.md)
- [Columns with a name](columns-with-a-name.md)
- [Columns with a name specified as a wildcard character](columns-with-a-name-specified-as-a-wildcard-character.md)
- [Columns with the name of an XPath node test](columns-with-the-name-of-an-xpath-node-test.md)
- [Column names with the path specified as data()](column-names-with-the-path-specified-as-data.md)
- [Columns that contain a null value by default](columns-that-contain-a-null-value-by-default.md)
- [Namespace support in PATH mode](namespace-support-in-path-mode.md)
- [Examples: Use PATH mode](examples-using-path-mode.md)

## Related content

- [Add namespaces to queries using WITH XMLNAMESPACES](add-namespaces-to-queries-with-with-xmlnamespaces.md)
- [SELECT (Transact-SQL)](../../t-sql/queries/select-transact-sql.md)
- [FOR XML (SQL Server)](for-xml-sql-server.md)
