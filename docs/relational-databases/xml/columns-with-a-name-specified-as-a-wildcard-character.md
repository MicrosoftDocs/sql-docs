---
title: "Specify a column with a wildcard character (SQLXML)"
description: Learn how column names that are specified as a wildcard character affect the results of an XQuery.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "names [SQL Server], columns with"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Columns with a name specified as a wildcard character

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

If the column name specified is a wildcard character (\*), the content of that column is inserted as if there's no column name specified. If this column is a non-**xml** type column, the column content is inserted as a text node, as shown in the following example:

```sql
USE AdventureWorks2012;
GO
SELECT E.BusinessEntityID "@EmpID",
       FirstName "*",
       MiddleName "*",
       LastName "*"
FROM   HumanResources.Employee AS E
  INNER JOIN Person.Person AS P
    ON E.BusinessEntityID = P.BusinessEntityID
WHERE E.BusinessEntityID=1
FOR XML PATH;
```

This is the result:

```xml
<row EmpID="1">KenJSánchez</row>
```

If the column is of **xml** type, the corresponding XML tree is inserted. For example, the following query specifies "*" for the column name that contains the XML returned by the XQuery against the Instructions column.

```sql
SELECT
       ProductModelID,
       Name,
       Instructions.query('declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"
                /MI:root/MI:Location
              ') as "*"
FROM Production.ProductModel
WHERE ProductModelID=7
FOR XML PATH;
```

This is the result. The XML returned by XQuery is inserted without a wrapping element.

```xml
<row>
  <ProductModelID>7</ProductModelID>
  <Name>HL Touring Frame</Name>
  <MI:Location LocationID="10">...</MI:Location>
  <MI:Location LocationID="20">...</MI:Location>
  ...
</row>
```

## See also

- [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)
