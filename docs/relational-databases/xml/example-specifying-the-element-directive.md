---
title: "Example: Specifying the ELEMENT Directive"
description: View an example of how to specify the ELEMENT directive in an SQL query to generate element-centric XML.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "ELEMENT directive"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Specify the ELEMENT directive

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This retrieves employee information and generates element-centric XML as shown in the following:

```xml
<Employee EmpID=...>
  <Name>
    <FName>...</FName>
    <LName>...</LName>
  </Name>
</Employee>
```

The query remains the same, except you add the `ELEMENT` directive in the column names. Therefore, instead of attributes, the `<FName>` and `<LName>` element children are added to the `<Name>` element. Because the `Employee!1!EmpID` column doesn't specify the `ELEMENT` directive, `EmpID` is added as the attribute of the `<Employee>` element.

```sql
SELECT 1    as Tag,
       NULL as Parent,
       E.BusinessEntityID as [Employee!1!EmpID],
       NULL       as [Name!2!FName!ELEMENT],
       NULL       as [Name!2!LName!ELEMENT]
FROM   HumanResources.Employee AS E
INNER JOIN Person.Person AS P
ON  E.BusinessEntityID = P.BusinessEntityID
UNION ALL
SELECT 2 as Tag,
       1 as Parent,
       E.BusinessEntityID,
       FirstName,
       LastName
FROM   HumanResources.Employee AS E
INNER JOIN Person.Person AS P
ON  E.BusinessEntityID = P.BusinessEntityID
ORDER BY [Employee!1!EmpID],[Name!2!FName!ELEMENT]
FOR XML EXPLICIT;
```

This is the partial result.

```xml
<Employee EmpID="1">
  <Name>
    <FName>Ken</FName>
    <LName>Sánchez</LName>
  </Name>
</Employee>
<Employee EmpID="2">
  <Name>
    <FName>Terri</FName>
    <LName>Duffy</LName>
  </Name>
</Employee>
...
```

## See also

- [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)
