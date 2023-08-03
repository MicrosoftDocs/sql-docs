---
title: "Use XML in Computed Columns"
description: View examples of how to use XML instances and XML columns with computed columns in SQL.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/29/2022
ms.service: sql
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "computed columns, XML"
  - "XML [SQL Server], computed columns"
---
# Use XML in computed columns

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

XML instances can appear as a source for a computed column, or as a type of computed column. The examples in this article show how to use XML with computed columns.

## Creating computed columns from XML columns

In the following `CREATE TABLE` statement, an `xml` type column (`col2`) is computed from `col1`:

```sql
CREATE TABLE T ( col1 varchar(max), col2 AS CAST(col1 AS xml) );
```

The **xml** data type can also appear as a source in creating a computed column, as shown in the following `CREATE TABLE` statement:

```sql
CREATE TABLE T ( col1 xml, col2 as cast(col1 as varchar(1000) ));
```

You can create a computed column by extracting a value from an `xml` type column as shown in the following example. Because the **xml** data type methods can't be used directly in creating computed columns, the example first defines a function (`my_udf`) that returns a value from an XML instance. The function wraps the `value()` method of the `xml` type. The function name is then specified in the `CREATE TABLE` statement for the computed column.

> [!NOTE]  
> If you want to create a *persisted* computed column based on this function, the function itself must be *deterministic*. For more information, see [Deterministic and nondeterministic functions](../user-defined-functions/deterministic-and-nondeterministic-functions.md).

```sql
CREATE FUNCTION my_udf(@var xml) returns int
AS BEGIN
RETURN @var.value('(/ProductDescription/@ProductModelID)[1]' , 'int')
END;
GO
-- Use the function in CREATE TABLE.
CREATE TABLE T (col1 xml, col2 as dbo.my_udf(col1) );
GO
-- Try adding a row.
INSERT INTO T values('<ProductDescription ProductModelID="1" />');
GO
-- Verify results.
SELECT col2, col1
FROM T;
```

As in the previous example, the following example defines a function to return an **xml** type instance for a computed column. Inside the function, the `query()` method of the **xml** data type retrieves a value from an `xml` type parameter.

```sql
CREATE FUNCTION my_udf(@var xml)
  RETURNS xml AS
BEGIN
   RETURN @var.query('ProductDescription/Features')
END;
```

In the following `CREATE TABLE` statement, `Col2` is a computed column that uses the XML data (`<Features>` element) that is returned by the function:

```sql
CREATE TABLE T (Col1 xml, Col2 as dbo.my_udf(Col1) );
-- Insert a row in table T.
INSERT INTO T VALUES('
<ProductDescription ProductModelID="1" >
  <Features>
    <Feature1>description</Feature1>
    <Feature2>description</Feature2>
  </Features>
</ProductDescription>');
-- Verify the results.
SELECT *
FROM T;
```

### See also

- [Promote Frequently Used XML Values with Computed Columns](../../relational-databases/xml/promote-frequently-used-xml-values-with-computed-columns.md)  
