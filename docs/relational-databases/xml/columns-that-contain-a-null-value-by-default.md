---
title: "Columns that Contain a Null Value By Default"
description: Learn how to work with columns that contain a null value by default by using the ELEMENTS XSINIL keyword phrase in SQL.
ms.custom: "fresh2019may"
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "columns [XML in SQL Server], null default value"
author: MikeRayMSFT
ms.author: mikeray
---
# Columns that contain a null value by default

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

By default, a null value in a column maps to the absence of the attribute, node, or element. This default behavior can be overridden by using the ELEMENTS XSINIL keyword phrase. This phrase requests element-centric XML. This means that null values are explicitly indicated in the returned results. These elements will have no value.

The ELEMENTS XSINIL phrase is shown in the following Transact-SQL SELECT example.

```sql
SELECT EmployeeID as "@EmpID",
       FirstName  as "EmpName/First",
       MiddleName as "EmpName/Middle",
       LastName   as "EmpName/Last"
FROM   HumanResources.Employee E, Person.Contact C
WHERE  E.EmployeeID = C.ContactID
  AND  E.EmployeeID=1
FOR XML PATH, ELEMENTS XSINIL;
```

The following shows the result. If XSINIL isn't specified, the `<Middle>` element will be absent.

```xml
<row xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" EmpID="1">
  <EmpName>
    <First>Gustavo</First>
    <Middle xsi:nil="true" />
    <Last>Achong</Last>
  </EmpName>
</row>
```

## See also

- [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)
