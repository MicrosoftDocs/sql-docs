---
title: "Example: Renaming the &lt;row&gt; Element"
description: View an example of renaming an XML row element by specifying an optional argument to RAW mode in the FOR XML clause.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, renaming <row> example"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Rename the &lt;row&gt; element

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

For each row in the result set, the RAW mode generates an element `<row>`. You can optionally specify another name for this element by specifying an optional argument to the RAW mode, as shown in this query. The query returns a `<ProductModel>` element for each row in the rowset.

## Example

```sql
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID = 122
FOR XML RAW ('ProductModel'), ELEMENTS;
GO
```

This is the result. Because the `ELEMENTS` directive is added in the query, the result is element-centric.

```xml
<ProductModel>
  <ProductModelID>122</ProductModelID>
  <Name>All-Purpose Bike Stand</Name>
</ProductModel>
```

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
