---
title: "Specify XSINIL with the ELEMENTS Directive"
description: View an example that specifies XSINIL with the ELEMENTS directive to generate element-centric XML from the query result.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, specifying XSINIL example"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Example: Specify XSINIL with the ELEMENTS Directive

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following query specifies the `ELEMENTS` directive to generate element-centric XML from the query result.

## Example

```sql
USE AdventureWorks2012;
GO
SELECT ProductID, Name, Color
FROM Production.Product
FOR XML RAW, ELEMENTS;
GO
```

This is the partial result.

```xml
<row>
  <ProductID>1</ProductID>
  <Name>Adjustable Race</Name>
</row>
...
<row>
  <ProductID>317</ProductID>
  <Name>LL Crankarm</Name>
  <Color>Black</Color>
</row>
```

Because the `Color` column has null values for some products, the resulting XML won't generate the corresponding `<Color>` element. By adding the `XSINIL` directive with `ELEMENTS`, you can generate the `<Color>` element even for NULL color values in the result set.

```sql
USE AdventureWorks2012;
GO
SELECT ProductID, Name, Color
FROM Production.Product
FOR XML RAW, ELEMENTS XSINIL;
```

This is the partial result:

```xml
<row xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <ProductID>1</ProductID>
  <Name>Adjustable Race</Name>
  <Color xsi:nil="true" />
</row>
...
<row>
  <ProductID>317</ProductID>
  <Name>LL Crankarm</Name>
  <Color>Black</Color>
</row>
```

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
