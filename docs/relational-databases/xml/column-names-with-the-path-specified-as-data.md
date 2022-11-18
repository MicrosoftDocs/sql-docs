---
title: "Column Names with the Path Specified as data()"
description: Learn about XML queries containing column names with the path specified as data().
ms.custom: "fresh2019may"
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "names [SQL Server], columns with"
author: MikeRayMSFT
ms.author: mikeray
---
# Column names with the path specified as data()

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

If the path specified as column name is `data()`, the value is treated as an atomic value in the generated XML. A space character is added to the XML if the next item in the serialization is also an atomic value. This is useful when you're creating list typed element and attribute values. The following query retrieves the product model ID, name, and list of products in that product model.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID       AS "@ProductModelID",
       Name                 AS "@ProductModelName",
      (SELECT ProductID AS "data()"
       FROM   Production.Product
       WHERE  Production.Product.ProductModelID =
              Production.ProductModel.ProductModelID
      FOR XML PATH (''))    AS "@ProductIDs"
FROM  Production.ProductModel
WHERE ProductModelID = 7
FOR XML PATH('ProductModelData');
```

The nested SELECT retrieves a list of product IDs. It specifies "data()" as the column name for product IDs. Because PATH mode specifies an empty string for the row element name, there's no row element generated. Instead, the values are returned as assigned to a ProductIDs attribute of the `<ProductModelData>` row element of the parent SELECT. This is the result:

```xml
<ProductModelData
  ProductModelID = "7"
  ProductModelName="HL Touring Frame"
  ProductIDs="885 887 888 889 890 891 892 893"
/>
```

## See also

- [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)
