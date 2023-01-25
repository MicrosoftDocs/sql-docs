---
title: "Example: Querying XMLType Columns"
description: View an example of how to query columns of the xml data type.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, querying XML example"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Query XMLType columns

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following query includes columns of **xml** type. The query retrieves product model ID, name, and manufacturing steps at the first location from the `Instructions` column of the **xml** type.

## Example

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name,
   Instructions.query('
declare namespace MI="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";
/MI:root/MI:Location[1]/MI:step')
FROM Production.ProductModel
FOR XML RAW ('ProductModelData')
GO
```

The following is the result. The table stores manufacturing instructions for only some product models. The manufacturing steps are returned as subelements of the `<ProductModelData>` element in the result.

```xml
<ProductModelData ProductModelID="5" Name="HL Mountain Frame" />
<ProductModelData ProductModelID="6" Name="HL Road Frame" />
<ProductModelData ProductModelID="7" Name="HL Touring Frame">
    <MI:step> ... </MI:step>
    <MI:step> ... </MI:step>
</ProductModelData>
```

If the query specifies a column name for the XML returned by the XQuery, as specified in the following `SELECT` statement, the manufacturing steps are wrapped in the element that has the specified name.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name,
   Instructions.query('
declare namespace MI="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";
/MI:root/MI:Location[1]/MI:step') as ManuSteps
FROM Production.ProductModel
FOR XML RAW ('ProductModelData');
GO
```

This is the result:

```xml
<ProductModelData ProductModelID="5" Name="HL Mountain Frame" />
<ProductModelData ProductModelID="6" Name="HL Road Frame" />
<ProductModelData ProductModelID="7" Name="HL Touring Frame">
  <ManuSteps>
    <MI:step ... </MI:step>
    <MI:step ... </MI:step>
  </ManuSteps>
</ProductModelData>
```

The following query specifies the `ELEMENTS` directive. Therefore, the result returned is element-centric. The `XSINIL` option specified with the `ELEMENTS` directive returns the `<ManuSteps>` elements, even if the corresponding column in the rowset is NULL.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name,
   Instructions.query('
declare namespace MI="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions"
   /MI:root/MI:Location[1]/MI:step
') as ManuSteps
FROM Production.ProductModel
FOR XML RAW ('ProductModelData'), root('MyRoot'), ELEMENTS XSINIL
GO
```

This is the result:

```xml
<MyRoot xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
   ...
  <ProductModelData>
    <ProductModelID>6</ProductModelID>
    <Name>HL Road Frame</Name>
    <ManuSteps xsi:nil="true" />
  </ProductModelData>
  <ProductModelData>
    <ProductModelID>7</ProductModelID>
    <Name>HL Touring Frame</Name>
    <ManuSteps>
      <MI:step ... </MI:step>
      <MI:step ...</MI:step>
       ...
    </ManuSteps>
  </ProductModelData>
</MyRoot>
```

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
