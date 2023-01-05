---
description: "Guidelines for Using xml Data Type Methods"
title: Guidelines for Using xml Data Type Methods
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs:
  - "TSQL"
helpviewer_keywords:
  - "xml data type [SQL Server], methods"
  - "methods [XML in SQL Server]"
ms.assetid: 1a483aa1-42de-4c88-a4b8-c518def3d496
author: MikeRayMSFT
ms.author: mikeray
---

# Guidelines for Using xml Data Type Methods

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This topic describes guidelines for using the **xml** data type methods.

## The PRINT Statement

The **xml** data type methods cannot be used in the PRINT statement as shown in the following example. The **xml** data type methods are treated as subqueries, and subqueries are not allowed in the PRINT statement. As a result, the following example returns an error:

```sql
DECLARE @x XML
SET @x = '<root>Hello</root>'
PRINT @x.value('/root[1]', 'varchar(20)') -- will not work because this is treated as a subquery (select top 1 col from table)
```

A solution is to first assign the result of the **value()** method to a variable of **xml** type and then specify the variable in the query.

```sql
DECLARE @x XML
DECLARE @c VARCHAR(max)
SET @x = '<root>Hello</root>'
SET @c = @x.value('/root[1]', 'VARCHAR(11)')
PRINT @c
```

## The GROUP BY Clause

The **xml** data type methods are treated internally as subqueries. Because GROUP BY requires a scalar and does not allow aggregates and subqueries, you cannot specify the **xml** data type methods in the GROUP BY clause. A solution is to call a user-defined function that uses XML methods inside of it.

## Reporting Errors

When reporting errors, **xml** data type methods raise a single error in the following format:

```
Msg errorNumber, Level levelNumber, State stateNumber:
XQuery [database.table.method]: description_of_error
```

For example:

```
Msg 2396, Level 16, State 1:
XQuery [xmldb_test.xmlcol.query()]: Attribute may not appear outside of an element
```

## Singleton Checks

Location steps, function parameters, and operators that require singletons will return an error if the compiler cannot determine whether a singleton is guaranteed at run time. This problem occurs frequently with untyped data. For example, the lookup of an attribute requires a singleton parent element. An ordinal that selects a single parent node is sufficient. The evaluation of a **node()**-**value()** combination to extract attribute values may not require the ordinal specification. This is shown in the next example.

### Example: Known Singleton

In this example, the **nodes()** method generates a separate row for each `<book>` element. The **value()** method that is evaluated on a `<book>` node extracts the value of `@genre` and, being an attribute, is a singleton.

```sql
SELECT nref.value('@genre', 'VARCHAR(max)') LastName
FROM T CROSS APPLY xCol.nodes('//book') AS R(nref)
```

XML schema is used for type checking of typed XML. If a node is specified as a singleton in the XML schema, the compiler uses that information and no error occurs. Otherwise, an ordinal that selects a single node is required. In particular, the use of descendant-or-self axis (//) axis, such as in `/book//title`, looses singleton cardinality inference for the `<title>` element, even if the XML schema specifies it to be so. Therefore, you should rewrite it as `(/book//title)[1]`.

It is important to remain aware of the difference between `//first-name[1]` and `(//first-name)[1]` for type checking. The former returns a sequence of `<first-name>` nodes in which each node is the leftmost `<first-name>` node among its siblings. The latter returns the first singleton `<first-name>` node in document order in the XML instance.

### Example: Using value()

The following query on an untyped XML column results in a static, compilation error.This is because **value()** expects a singleton node as the first argument and the compiler cannot determine whether only one `<last-name>` node will occur at run time:

```sql
SELECT xCol.value('//author/last-name', 'NVARCHAR(50)') LastName
FROM T
```

Following is a solution that you could consider:

```sql
SELECT xCol.value('//author/last-name[1]', 'NVARCHAR(50)') LastName
FROM T
```

However, this solution does not solve the error, because multiple `<author>` nodes may occur in each XML instance. The following rewrite works:

```sql
SELECT xCol.value('(//author/last-name/text())[1]', 'NVARCHAR(50)') LastName
FROM T
```

This query returns the value of the first `<last-name>` element in each XML instance.

## See Also

- [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)
