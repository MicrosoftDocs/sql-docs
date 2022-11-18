---
title: Create XML data type variables and columns
description: Learn how to create columns and variables of the XML data type in SQL Server.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "xml data type [SQL Server], variables"
  - "xml data type [SQL Server], columns"
author: MikeRayMSFT
ms.author: mikeray
---
# Create XML data type variables and columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The **xml** data type is a built-in data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is somewhat similar to other built-in types such as **int** and **varchar**. As with other built-in types, you can use the **xml** data type as a column type when you create a table as a variable type, a parameter type, a function-return type, or in [CAST and CONVERT](../../t-sql/functions/cast-and-convert-transact-sql.md).

## Create columns and variables

To create an `xml` type column as part of a table, use a `CREATE TABLE` statement, as shown in the following example:

```sql
CREATE TABLE T1(Col1 int primary key, Col2 xml);
```

You can use a `DECLARE statement` to create a variable of `xml` type, as the following example shows.

```sql
DECLARE @x xml;
```

Create a typed `xml` variable by specifying an XML schema collection, as shown in the following example.

```sql
DECLARE @x xml (Sales.StoreSurveySchemaCollection)
```

To pass an `xml` type parameter to a stored procedure, use a `CREATE PROCEDURE` statement, as shown in the following example.

```sql
CREATE PROCEDURE SampleProc(@XmlDoc xml) AS ...
```

You can use XQuery to query XML instances stored in columns, parameters, or variables. You can also use the XML Data Manipulation Language (XML DML) to apply updates to the XML instances. Because the XQuery standard didn't define XQuery DML at the time of development, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] introduces [XML Data Modification Language](../../t-sql/xml/xml-data-modification-language-xml-dml.md) extensions to XQuery. These extensions allow you to perform insert, update, and delete operations.

## Assigning defaults

In a table, you can assign a default XML instance to a column of **xml** type. You can provide the default XML in one of two ways: by using an XML constant, or by using an explicit cast to the **xml** type.

To provide the default XML as an XML constant, use syntax as shown in the following example. The string is implicitly CAST to **xml** type.

```sql
CREATE TABLE T (XmlColumn xml default N'<element1/><element2/>')
```

To provide the default XML as an explicit `CAST` to `xml`, use syntax as shown in the following example.

```sql
CREATE TABLE T (XmlColumn xml
                  default CAST(N'<element1/><element2/>' AS xml))
```

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also supports NULL and NOT NULL constraints on columns of **xml** type. For example:

```sql
CREATE TABLE T (XmlColumn xml NOT NULL)
```

## Specify constraints

When you create columns of **xml** type, you can define column-level or table-level constraints. Use constraints in the following situations:

- Your business rules can't be expressed in XML schemas. For example, the delivery address of a flower shop must be within 50 miles of its business location. This can be written as a constraint on the XML column. The constraint may involve **xml** data type methods.

- Your constraint involves other XML or non-XML columns in the table. An example is the enforcement of the ID of a Customer (`/Customer/@CustId`) found in an XML instance to match the value in a relational CustomerID column.

You can specify constraints for typed or untyped **xml** data type columns. However, you can't use the [XML data type methods](../../t-sql/xml/xml-data-type-methods.md) when you specify constraints.

The **xml** data type doesn't support the following column and table constraints:

- PRIMARY KEY/ FOREIGN KEY

- UNIQUE

- COLLATE

  XML provides its own encoding. Collations apply to string types only. The **xml** data type isn't a string type. However, it does have string representation and allows casting to and from string data types.

- RULE

An alternative to using constraints is to create a wrapper, user-defined function to wrap the **xml** data type method and specify user-defined function in the check constraint as shown in the following example.

In the following example, the constraint on `Col2` specifies that each XML instance stored in this column must have a `<ProductDescription>` element that contains a `ProductID` attribute. This constraint is enforced by this user-defined function:

```sql
CREATE FUNCTION my_udf(@var xml) returns bit
AS BEGIN
RETURN @var.exist('/ProductDescription/@ProductID')
END;
GO
```

The `exist()` method of the **xml** data type returns `1` if the `<ProductDescription>` element in the instance contains the `ProductID` attribute. Otherwise, it returns `0`.

Now, you can create a table with a column-level constraint as follows:

```sql
CREATE TABLE T (
    Col1 int primary key,
    Col2 xml check(dbo.my_udf(Col2) = 1));
GO
```

The following insert succeeds:

```sql
INSERT INTO T values(1,'<ProductDescription ProductID="1" />');
```

Because of the constraint, the following insert fails:

```sql
INSERT INTO T values(1,'<Product />');
```

## Same or different table

An **xml** data type column can be created in a table that contains other relational columns, or in a separate table with a foreign key relationship to a main table.

Create an **xml** data type column in the same table when one of the following conditions is true:

- Your application performs data retrieval on the XML column and doesn't require an XML index on the XML column.

- You want to build an XML index on the **xml** data type column and the primary key of the main table is the same as its clustering key. For more information, see [XML Indexes &#40;SQL Server&#41;](../../relational-databases/xml/xml-indexes-sql-server.md).

Create the **xml** data type column in a separate table if the following conditions are true:

- You want to build an XML index on the **xml** data type column, but the primary key of the main table is different from its clustering key, or the main table doesn't have a primary key, or the main table is a heap (no clustering key). This may be true if the main table already exists.

- You don't want table scans to slow down because of the presence of the XML column in the table. This uses space whether it is stored in-row or out-of-row.
