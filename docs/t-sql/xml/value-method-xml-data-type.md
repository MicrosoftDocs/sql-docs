---
title: value() method (xml data type)
description: The value method performs an XQuery against XML and returns a scalar SQL type value.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
helpviewer_keywords:
  - "value method"
  - "value() method"
dev_langs:
  - "TSQL"
---
# value() method (xml data type)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Performs an XQuery against XML and returns a value of SQL type. This method returns a scalar value.

You typically use this method to extract a value from an XML instance stored in an **xml** type column, parameter, or variable. In this way, you can specify `SELECT` queries that combine or compare XML data with data in non-XML columns.

## Syntax

```syntaxsql
value ( XQuery , SQLType )
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *XQuery*

The *XQuery* expression, a string literal, that retrieves data inside the XML instance. The XQuery must return at most one value. Otherwise, an error is returned.

#### *SQLType*

The preferred SQL type, a string literal, to be returned. The return type of this method matches the *SQLType* parameter. *SQLType* can be a user-defined SQL data type.

> [!NOTE]  
> *SQLType* can't be one of the following data types: **xml**, **image**, **text**, **ntext**, **sql_variant**, or a common language runtime (CLR) user-defined type.

The `value()` method uses the [!INCLUDE [tsql](../../includes/tsql-md.md)] `CONVERT` operator implicitly. `value()` tries to convert the result of the XQuery expression, the serialized string representation, from XML Schema Definition (XSD) type to the corresponding SQL type specified by [!INCLUDE [tsql](../../includes/tsql-md.md)] conversion. For more information about type casting rules for `CONVERT`, see [CAST and CONVERT](../functions/cast-and-convert-transact-sql.md).

For performance reasons, you can use `exist()` with `sql:column()` instead of using the `value()` method in a predicate, to compare with a relational value. This `exist()` example is shown later in this article.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use the value() method against an XML type variable

In the following example, an XML instance is stored in a variable of **xml** type. The `value()` method retrieves the `ProductID` attribute value from the XML. The value is then assigned to an **int** variable.

```sql
DECLARE @myDoc XML;
DECLARE @ProdID INT;

SET @myDoc = '<Root>
<ProductDescription ProductID="1" ProductName="Road Bike">
<Features>
  <Warranty>1 year parts and labor</Warranty>
  <Maintenance>3 year parts and labor extended maintenance is available</Maintenance>
</Features>
</ProductDescription>
</Root>';

SET @ProdID = @myDoc.value('(/Root/ProductDescription/@ProductID)[1]', 'int');
SELECT @ProdID;
```

A value of `1` is returned as a result.

Although there's only one `ProductID` attribute in the XML instance, the static typing rules require you to explicitly specify that the path expression returns a singleton. Therefore, the `[1]` is added to the end of the path expression. For more information about static typing, see [XQuery and Static Typing](../../xquery/xquery-and-static-typing.md).

### B. Use the value() method to retrieve an integer value from an XML type column

The following query is specified against an **xml** type column (`CatalogDescription`) in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. The query retrieves `ProductModelID` attribute values from each XML instance stored in the column.

```sql
SELECT CatalogDescription.value(
    'declare namespace PD="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
       (/PD:ProductDescription/@ProductModelID)[1]', 'int') AS Result
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL
ORDER BY Result DESC;
```

Note from the previous query:

- The `namespace` keyword is used to define a namespace prefix.

- Per static typing requirements, `[1]` is added at the end of the path expression in the `value()` method to explicitly indicate that the path expression returns a singleton.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
35
34
28
25
23
19
```

### C. Use the value() method to retrieve a string value from an XML type column

The following query is specified against the **xml** type column (`CatalogDescription`) in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. The query retrieves `ProductModelName` attribute values from each XML instance stored in the column.

```sql
SELECT CatalogDescription.value(
    'declare namespace PD="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
       (/PD:ProductDescription/@ProductModelName)[1]', 'varchar(50)') AS Result
FROM Production.ProductModel
WHERE CatalogDescription IS NOT NULL
ORDER BY Result DESC;
```

Note from the previous query:

- The `namespace` keyword is used to define a namespace prefix.

- Per static typing requirements, `[1]` is added at the end of the path expression in the `value()` method to explicitly indicate that the path expression returns a singleton.

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
Touring-2000
Touring-1000
Road-450
Road-150
Mountain-500
Mountain 100
```

### D. Use the value() and exist() methods to retrieve values from an XML type column

The following example shows using both the `value()` method and the [exist() method](exist-method-xml-data-type.md) of the **xml** data type. The `value()` method is used to retrieve `ProductModelID` attribute values from the XML. The `exist()` method in the `WHERE` clause is used to filter the rows from the table.

The query retrieves product model IDs from XML instances that include warranty information (the `<Warranty>` element) as one of the features. The condition in the `WHERE` clause uses the `exist()` method to retrieve only the rows satisfying this condition.

```sql
SELECT CatalogDescription.value(
    'declare namespace PD="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
           (/PD:ProductDescription/@ProductModelID)[1]', 'int') AS Result
FROM Production.ProductModel
WHERE CatalogDescription.exist(
    'declare namespace PD="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelDescription";
     declare namespace wm="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain";

     /PD:ProductDescription/PD:Features/wm:Warranty') = 1;
```

Note from the previous query:

- The `CatalogDescription` column is a typed XML column. This means that it has a schema collection associated with it. In the [Modules and Prologs - XQuery Prolog](../../xquery/modules-and-prologs-xquery-prolog.md), the namespace declaration is used to define the prefix that is used later in the query body.

- If the `exist()` method returns `1` (true), it indicates that the XML instance includes the `<Warranty>` child element as one of the features.

- The `value()` method in the `SELECT` clause then retrieves the `ProductModelID` attribute values as integers.

Here's the partial result:

```output
19
23
...
```

### E. Use the exist() method instead of the value() method

For performance reasons, instead of using the `value()` method in a predicate to compare with a relational value, use `exist()` with `sql:column()`. For example:

```sql
CREATE TABLE T (c1 INT, c2 VARCHAR(10), c3 XML);
GO

SELECT c1, c2, c3
FROM T
WHERE c3.value('(/root/@a)[1]', 'integer') = c1;
GO
```

This code can be rewritten as follows:

```sql
SELECT c1, c2, c3
FROM T
WHERE c3.exist('/root[@a=sql:column("c1")]') = 1;
GO
```

## Related content

- [Add namespaces to queries using WITH XMLNAMESPACES](../../relational-databases/xml/add-namespaces-to-queries-with-with-xmlnamespaces.md)
- [Compare typed XML to untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)
- [Create instances of XML data](../../relational-databases/xml/create-instances-of-xml-data.md)
- [xml Data Type Methods](xml-data-type-methods.md)
- [XML Data Modification Language (XML DML)](xml-data-modification-language-xml-dml.md)
