---
title: "Example: Retrieving Product Model Information as XML"
description: View an example of how to retrieve product model information as XML by using RAW mode with the FOR XML clause.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, retrieving XML information example"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Retrieve product model information as XML

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following query returns product model information. `RAW` mode is specified in the `FOR XML` clause.

## Example

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW;
GO
```

This is the partial result:

```xml
<row ProductModelID="122" Name="All-Purpose Bike Stand" />
<row ProductModelID="119" Name="Bike Wash" />
```

You can retrieve element-centric XML by specifying the `ELEMENTS` directive.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, ELEMENTS;
GO
```

This is the result:

```xml
<row>
  <ProductModelID>122</ProductModelID>
  <Name>All-Purpose Bike Stand</Name>
</row>
<row>
  <ProductModelID>119</ProductModelID>
  <Name>Bike Wash</Name>
</row>
```

You can optionally specify the `TYPE` directive to retrieve the results as **xml** type. The `TYPE` directive doesn't change the content of the results. Only the data type of the results is affected.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, TYPE;
GO
```

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
