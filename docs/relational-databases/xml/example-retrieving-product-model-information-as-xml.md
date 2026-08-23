---
title: "Example: Retrieving Product Model Information as XML"
description: View an example of how to retrieve product model information as XML by using RAW mode with the FOR XML clause.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "RAW mode, retrieving XML information example"
---
# Example: Retrieve product model information as XML

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The following query returns product model information. `RAW` mode is specified in the `FOR XML` clause.

## Example

```sql
USE AdventureWorks2022;
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
USE AdventureWorks2022;
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
USE AdventureWorks2022;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, TYPE;
GO
```

## Related content

- [Use RAW mode with FOR XML](use-raw-mode-with-for-xml.md)
