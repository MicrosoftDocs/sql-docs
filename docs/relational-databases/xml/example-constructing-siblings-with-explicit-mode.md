---
title: "Example: Constructing Siblings with EXPLICIT Mode"
description: View an example of an SQL query that uses EXPLICIT mode with the FOR XML clause to construct XML siblings.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "EXPLICIT FOR XML mode"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Construct siblings with EXPLICIT mode

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Assume that you want to construct XML that provides sales order information. In this example, `<SalesPerson>` and `<OrderDetail>` elements are siblings. Each Order has one `<OrderHeader>` element, one `<SalesPerson>` element, and one or more `<OrderDetail>` elements.

```xml
<OrderHeader SalesOrderID=... OrderDate=... CustomerID=... >
  <SalesPerson SalesPersonID=... />
  <OrderDetail SalesOrderID=... LineTotal=... ProductID=... OrderQty=... />
  <OrderDetail SalesOrderID=... LineTotal=... ProductID=... OrderQty=.../>
      ...
</OrderHeader>
<OrderHeader ...</OrderHeader>
```

The following EXPLICIT mode query constructs this XML. The query specifies `Tag` values of 1 for the `<OrderHeader>` element, 2 for the `<SalesPerson>` element, and 3 for the `<OrderDetail>` element. Because `<SalesPerson>` and `<OrderDetail>` are siblings, the query specifies the same `Parent` tag value of 1 identifying the `<OrderHeader>` element.

```sql
USE AdventureWorks2012;
GO
SELECT  1 as Tag,
        0 as Parent,
        SalesOrderID  as [OrderHeader!1!SalesOrderID],
        OrderDate     as [OrderHeader!1!OrderDate],
        CustomerID    as [OrderHeader!1!CustomerID],
        NULL          as [SalesPerson!2!SalesPersonID],
        NULL          as [OrderDetail!3!SalesOrderID],
        NULL          as [OrderDetail!3!LineTotal],
        NULL          as [OrderDetail!3!ProductID],
        NULL          as [OrderDetail!3!OrderQty]
FROM   Sales.SalesOrderHeader
WHERE     SalesOrderID=43659 or SalesOrderID=43661
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
WHERE     SalesOrderID=43659 or SalesOrderID=43661
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
FROM    Sales.SalesOrderHeader SOH,Sales.SalesOrderDetail SOD
WHERE   SOH.SalesOrderID = SOD.SalesOrderID
AND     (SOH.SalesOrderID=43659 or SOH.SalesOrderID=43661)
ORDER BY [OrderHeader!1!SalesOrderID], [SalesPerson!2!SalesPersonID],
         [OrderDetail!3!SalesOrderID],[OrderDetail!3!LineTotal]
FOR XML EXPLICIT;
```

This is the partial result:

```xml
<OrderHeader SalesOrderID="43659" OrderDate="2005-07-01T00:00:00" CustomerID="676">
  <SalesPerson SalesPersonID="279" />
  <OrderDetail SalesOrderID="43659" LineTotal="10.373000" ProductID="712" OrderQty="2" />
  <OrderDetail SalesOrderID="43659" LineTotal="28.840400" ProductID="716" OrderQty="1" />
  <OrderDetail SalesOrderID="43659" LineTotal="34.200000" ProductID="709" OrderQty="6" />
    ...
</OrderHeader>
<OrderHeader SalesOrderID="43661" OrderDate="2005-07-01T00:00:00" CustomerID="442">
  <SalesPerson SalesPersonID="282" />
  <OrderDetail SalesOrderID="43661" LineTotal="20.746000" ProductID="712" OrderQty="4" />
  <OrderDetail SalesOrderID="43661" LineTotal="40.373000" ProductID="711" OrderQty="2" />
  ...
</OrderHeader>
```

## See also

- [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)
