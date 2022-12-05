---
title: "Shape XML with Nested FOR XML Queries"
description: View an example of using nested FOR XML queries to shape the resulting XML.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "FOR XML query"
  - "queries [XML in SQL Server], nested FOR XML"
  - "XML [SQL Server], FOR XML queries"
author: MikeRayMSFT
ms.author: mikeray
---
# Shape XML with nested FOR XML queries

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following example queries the `Production.Product` table to retrieve the `ListPrice` and `StandardCost` values of a specific product. To make the query interesting, both prices are returned in a `<Price>` element, and each `<Price>` element has a `PriceType` attribute.

## Example

This is the expected shape of the XML:

```xml
<xsd:schema xmlns:schema="urn:schemas-microsoft-com:sql:SqlRowSet2" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="https://schemas.microsoft.com/sqlserver/2004/sqltypes" targetNamespace="urn:schemas-microsoft-com:sql:SqlRowSet2" elementFormDefault="qualified">
  <xsd:import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="https://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" />
  <xsd:element name="Production.Product" type="xsd:anyType" />
</xsd:schema>
<Production.Product xmlns="urn:schemas-microsoft-com:sql:SqlRowSet2" ProductID="520">
  <Price xmlns="" PriceType="ListPrice">133.34</Price>
  <Price xmlns="" PriceType="StandardCost">98.77</Price>
</Production.Product>
```

This is the nested FOR XML query:

```sql
USE AdventureWorks2012;
GO
SELECT Product.ProductID,
          (SELECT 'ListPrice' as PriceType,
                   CAST(CAST(ListPrice as NVARCHAR(40)) as XML)
           FROM    Production.Product Price
           WHERE   Price.ProductID=Product.ProductID
           FOR XML AUTO, TYPE),
          (SELECT  'StandardCost' as PriceType,
                   CAST(CAST(StandardCost as NVARCHAR(40)) as XML)
           FROM    Production.Product Price
           WHERE   Price.ProductID=Product.ProductID
           FOR XML AUTO, TYPE)
FROM Production.Product
WHERE ProductID=520
for XML AUTO, TYPE, XMLSCHEMA;
```

Note the following from the previous query:

- The outer SELECT statement constructs the `<Product>` element that has a **ProductID** attribute and two `<Price>` child elements.

- The two inner SELECT statements construct two `<Price>` elements, each with a **PriceType** attribute and XML that returns the product price.

- The XMLSCHEMA directive in the outer SELECT statement generates the inline XSD schema that describes the shape of the resulting XML.

To make the query interesting, you can write the FOR XML query and then write an XQuery against the result to reshape the XML, as shown in the following query:

```sql
SELECT ProductID,
( SELECT p2.ListPrice, p2.StandardCost
   FROM Production.Product p2
   WHERE Product.ProductID = p2.ProductID
   FOR XML AUTO, ELEMENTS XSINIL, type ).query('
                                   for $p in /p2/*
                                   return
                                    <Price PriceType = "{local-name($p)}">
                                     { data($p) }
                                    </Price>
                                  ')
FROM Production.Product
WHERE ProductID = 520
FOR XML AUTO, TYPE;
```

The previous example uses the `query()` method of the **xml** data type to query the XML returned by the inner FOR XML query and construct the expected result.

This is the result:

```xml
<Production.Product ProductID="520">
  <Price PriceType="ListPrice">133.3400</Price>
  <Price PriceType="StandardCost">98.7700</Price>
</Production.Product>
```

## See also

- [Use Nested FOR XML Queries](../../relational-databases/xml/use-nested-for-xml-queries.md)
