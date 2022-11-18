---
title: "Generate XML from Rowsets with FOR XML"
description: Learn how to generate an xml data type instance from a rowset by using the TYPE directive with the FOR XML clause.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "FOR XML clause, generating XML from rowsets"
author: MikeRayMSFT
ms.author: mikeray
---
# Generate XML from rowsets with FOR XML

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can generate an **xml** data type instance from a rowset by using FOR XML with the new **TYPE** directive.

The result can be assigned to an **xml** data type column, variable, or parameter. Also, FOR XML can be nested to generate any hierarchical structure. This makes nested FOR XML much more convenient to write than FOR XML EXPLICIT, but it may not perform as well for deep hierarchies. FOR XML also introduces a new PATH mode. This new mode specifies the path in the XML tree where a column's value appears.

The new **FOR XML TYPE** directive can be used to define read-only XML views over relational data with SQL syntax. The view can be queried with SQL statements and embedded XQuery, as shown in the following example. You can also refer to these SQL views in stored procedures.

## Example: SQL view returning generated xml data type

The following SQL view definition creates an XML view over a relational column, pk, and book authors retrieved from an XML column:

```sql
CREATE VIEW V (xmlVal) AS
SELECT pk, xCol.query('/book/author')
FROM   T
FOR XML AUTO, TYPE;
```

The V view contains a single row with a single columnxmlVal of XML type`.` It can be queried like a regular **xml** data type instance. For example, the following query returns the author whose first name is "David":

```sql
SELECT xmlVal.query('//author[first-name = "David"]')
FROM   V;
```

SQL view definitions are similar to XML views that are created by using annotated schemas. However, there are important differences. The SQL view definition is read-only and must be manipulated with embedded XQuery. The XML views are created by using annotated schema. Additionally, the SQL view materializes the XML result before applying the XQuery expression, while the XPath queries on XML views evaluate SQL queries on the underlying tables.

## See also

- [FOR XML &#40;SQL Server&#41;](../../relational-databases/xml/for-xml-sql-server.md)
