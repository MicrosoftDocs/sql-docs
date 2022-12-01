---
title: "Example: Specifying the ID and IDREF Directives"
description: View an example of how to specify the ID and IDREF directives in an SQL query.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "IDREF directive"
  - "ID directive"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Specify the ID and IDREF directives

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This example is almost the same as the [Specifying the ELEMENTXSINIL Directive](../../relational-databases/xml/example-specifying-the-elementxsinil-directive.md) example. The only difference is that the query specifies the **ID** and **IDREF** directives. These directives overwrite the types of the **SalesPersonID** attribute in the `<OrderHeader>` and `<OrderDetail>` elements, and form intra-document links. You need the schema to see the overwritten types. Therefore, the query specifies the **XMLDATA** option in the FOR XML clause to retrieve the schema.

```sql
USE AdventureWorks2012;
GO
SELECT  1 as Tag,
        0 as Parent,
        SalesOrderID  as [OrderHeader!1!SalesOrderID!id],
        OrderDate     as [OrderHeader!1!OrderDate],
        CustomerID    as [OrderHeader!1!CustomerID],
        NULL          as [SalesPerson!2!SalesPersonID],
        NULL          as [OrderDetail!3!SalesOrderID!idref],
        NULL          as [OrderDetail!3!LineTotal],
        NULL          as [OrderDetail!3!ProductID],
        NULL          as [OrderDetail!3!OrderQty]
FROM   Sales.SalesOrderHeader
WHERE  SalesOrderID IN (43659, 43661)

UNION ALL
SELECT 2 as Tag,
       1 as Parent,
        SalesOrderID,
        NULL,
        NULL,
        SalesPersonID,
        NULL,
        NULL,
        NULL,
        NULL
FROM   Sales.SalesOrderHeader
WHERE  SalesOrderID IN (43659, 43661)

UNION ALL
SELECT 3 as Tag,
       1 as Parent,
        SOD.SalesOrderID,
        NULL,
        NULL,
        SalesPersonID,
        SOH.SalesOrderID,
        LineTotal,
        ProductID,
        OrderQty
FROM    Sales.SalesOrderHeader SOH,
        Sales.SalesOrderDetail SOD
WHERE   SOH.SalesOrderID = SOD.SalesOrderID
AND     (SOH.SalesOrderID=43659 or SOH.SalesOrderID=43661)
ORDER BY [OrderHeader!1!SalesOrderID!id],
         [SalesPerson!2!SalesPersonID],
         [OrderDetail!3!SalesOrderID!idref],
         [OrderDetail!3!LineTotal]

FOR XML EXPLICIT, XMLDATA;
```

This is the partial result. In the schema, the **ID** and **IDREF** directives have overwritten the data types of the **SalesOrderID** attribute in the `<OrderHeader>` and `<OrderDetail>` elements. If you remove these directives, the schema returns original types of these attributes.

```xml
<Schema
       name="Schema1"
       xmlns="urn:schemas-microsoft-com:xml-data"
       xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <ElementType name="OrderHeader" content="mixed" model="open">
    <AttributeType name="SalesOrderID" dt:type="id" />
    <AttributeType name="OrderDate" dt:type="dateTime" />
    <AttributeType name="CustomerID" dt:type="i4" />
    <attribute type="SalesOrderID" />
    <attribute type="OrderDate" />
    <attribute type="CustomerID" />
  </ElementType>
  <ElementType name="SalesPerson" content="mixed" model="open">
    <AttributeType name="SalesPersonID" dt:type="i4" />
    <attribute type="SalesPersonID" />
  </ElementType>
  <ElementType name="OrderDetail" content="mixed" model="open">
    <AttributeType name="SalesOrderID" dt:type="idref" />
    <AttributeType name="LineTotal" dt:type="number" />
    <AttributeType name="ProductID" dt:type="i4" />
    <AttributeType name="OrderQty" dt:type="i2" />
    <attribute type="SalesOrderID" />
    <attribute type="LineTotal" />
    <attribute type="ProductID" />
    <attribute type="OrderQty" />
  </ElementType>
</Schema>
<OrderHeader
       xmlns="x-schema:#Schema1"
       SalesOrderID="43659"
       OrderDate="2001-07-01T00:00:00"
       CustomerID="676">
  <SalesPerson SalesPersonID="279" />
  <OrderDetail
         SalesOrderID="43659"
         LineTotal="10.373000"
         ProductID="712"
         OrderQty="2" />
  ...
</OrderHeader>
...
```

## See also

- [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)
