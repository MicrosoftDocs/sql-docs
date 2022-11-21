---
title: "Columns with a name"
description: Learn about columns with a name in SQL queries and the specific conditions in which rowset columns with a name are mapped to the resulting XML.
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
# Columns with a name

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following are the specific conditions in which rowset columns with a name are mapped, case-sensitive, to the resulting XML:

- The column name starts with an at sign (`@`).

- The column name doesn't start with an at sign (`@`).

- The column name doesn't start with an at sign (`@`) and contains a slash mark (`/`).

- Several columns share the same prefix.

- One column has a different name.

## Column name starts with an at sign (`@`)

If the column name starts with an at sign (`@`) and doesn't contain a slash mark (`/`), an attribute of the `row` element that has the corresponding column value is created. For example, the following query returns a two-column (`@PmId, Name`) rowset. In the resulting XML, a `PmId` attribute is added to the corresponding `row` element and a value of `ProductModelID` is assigned to it.

```sql
SELECT ProductModelID as "@PmId",
       Name
FROM Production.ProductModel
WHERE ProductModelID = 7
FOR XML PATH;
```

This is the result:

```xml
<row PmId="7">
  <Name>HL Touring Frame</Name>
</row>
```

Attributes must come before any other node types, such as element nodes and text nodes, in the same level. The following query will return an error:

```sql
SELECT Name,
       ProductModelID as "@PmId"
FROM Production.ProductModel
WHERE ProductModelID = 7
FOR XML PATH;
```

## Column name doesn't start with an at sign (`@`)

If the column name doesn't start with an at sign (`@`), isn't one of the XPath node tests, and doesn't contain a slash mark (`/`), an XML element that is a subelement of the row element, `row` by default, is created.

The following query specifies the column name, the result. Therefore, a `result` element child is added to the `row` element.

```sql
SELECT 2 + 2 as result
for xml PATH;
```

This is the result:

```xml
<row>
  <result>4</result>
</row>
```

The following query specifies the column name, `ManuWorkCenterInformation`, for the XML returned by the XQuery specified against `Instructions` column of **xml** type. Therefore, a `ManuWorkCenterInformation` element is added as a child of the `row` element.

```sql
SELECT
  ProductModelID,
  Name,
  Instructions.query(
    'declare namespace MI="http://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelManuInstructions";
     /MI:root/MI:Location
    ') as ManuWorkCenterInformation
FROM Production.ProductModel
WHERE ProductModelID=7
FOR XML PATH;
```

This is the result:

```xml
<row>
  <ProductModelID>7</ProductModelID>
  <Name>HL Touring Frame</Name>
  <ManuWorkCenterInformation>
    <MI:Location ...LocationID="10" ...></MI:Location>
    <MI:Location ...LocationID="20" ...></MI:Location>
     ...
  </ManuWorkCenterInformation>
</row>
```

## Column name doesn't start with an at sign (`@`) and contains a slash mark (`/`)

If the column name doesn't start with an at sign (`@`), but contains a slash mark (`/`), the column name indicates an XML hierarchy. For example, if the column name is "Name1/Name2/Name3.../Name***n***", each Name***i*** represents an element name that is nested in the current row element (for i = 1) or that is under the element that has the name Name***i-1***. If Name***n*** starts with `@`, it's mapped to an attribute of Name***n-1*** element.

For example, the following query returns an employee ID and name that are represented as a complex element `EmpName` that contains a `First`, `Middle`, and `Last` name.

```sql
SELECT EmployeeID "@EmpID",
       FirstName  "EmpName/First",
       MiddleName "EmpName/Middle",
       LastName   "EmpName/Last"
FROM   HumanResources.Employee E, Person.Contact C
WHERE  E.EmployeeID = C.ContactID  AND
       E.EmployeeID = 1
FOR XML PATH;
```

The column names are used as a path in constructing XML in the PATH mode. The column name that contains employee ID values, starts with '\@'. Therefore, an attribute, `EmpID`, is added to the `row` element. All other columns include a slash mark (`/`) in the column name that indicates hierarchy. The resulting XML will have the `EmpName` child under the `row` element, and the `EmpName` child will have `First`, `Middle` and `Last` element children.

```xml
<row EmpID="1">
  <EmpName>
    <First>Gustavo</First>
    <Last>Achong</Last>
  </EmpName>
</row>
```

The employee middle name is null and, by default, the null value maps to the absence of the element or attribute. If you want elements generated for the NULL values, you can specify the ELEMENTS directive with XSINIL as shown in this query.

```sql
SELECT EmployeeID "@EmpID",
       FirstName  "EmpName/First",
       MiddleName "EmpName/Middle",
       LastName   "EmpName/Last"
FROM   HumanResources.Employee E, Person.Contact C
WHERE  E.EmployeeID = C.ContactID  AND
       E.EmployeeID = 1
FOR XML PATH, ELEMENTS XSINIL;
```

This is the result:

```xml
<row xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
      EmpID="1">
  <EmpName>
    <First>Gustavo</First>
    <Middle xsi:nil="true" />
    <Last>Achong</Last>
  </EmpName>
</row>
```

By default, the PATH mode generates element-centric XML. Therefore, specifying the ELEMENTS directive in a PATH mode query has no effect. However, as shown in the previous example, the ELEMENTS directive is useful with XSINIL to generate elements for null values.

Besides the ID and name, the following query retrieves an employee address. As per the path in the column names for address columns, an `Address` element child is added to the `row` element and the address details are added as element children of the `Address` element.

```sql
SELECT EmployeeID   "@EmpID",
       FirstName    "EmpName/First",
       MiddleName   "EmpName/Middle",
       LastName     "EmpName/Last",
       AddressLine1 "Address/AddrLine1",
       AddressLine2 "Address/AddrLIne2",
       City         "Address/City"
FROM   HumanResources.Employee E,
       Person.Contact C,
       Person.Address A
WHERE  E.EmployeeID = C.ContactID
AND    E.AddressID = A.AddressID
AND    E.EmployeeID = 1
FOR XML PATH;
```

This is the result:

```xml
<row EmpID="1">
  <EmpName>
    <First>Gustavo</First>
    <Last>Achong</Last>
  </EmpName>
  <Address>
    <AddrLine1>7726 Driftwood Drive</AddrLine1>
    <City>Monroe</City>
  </Address>
</row>
```

## Several columns share the same path prefix

If several subsequent columns share the same path prefix, they're grouped together under the same name. If different namespace prefixes are being used even if they're bound to the same namespace, a path is considered different. In the previous query, the `FirstName`, `MiddleName`, and `LastName` columns share the same `EmpName` prefix. Therefore, they're added as children of the `EmpName` element. This is also the case when you were creating the `Address` element in the previous example.

## One column has a different name

If a column with a different name appears in between, it will break the grouping, as shown in the following modified query. The query breaks the grouping of `FirstName`, `MiddleName`, and `LastName`, as specified in the previous query, by adding address columns in between the `FirstName` and `MiddleName` columns.

```sql
SELECT EmployeeID "@EmpID",
       FirstName "EmpName/First",
       AddressLine1 "Address/AddrLine1",
       AddressLine2 "Address/AddrLIne2",
       City "Address/City",
       MiddleName "EmpName/Middle",
       LastName "EmpName/Last"
FROM   HumanResources.EmployeeAddress E,
       Person.Contact C,
       Person.Address A
WHERE  E.EmployeeID = C.ContactID
AND    E.AddressID = A.AddressID
AND    E.EmployeeID = 1
FOR XML PATH;
```

As a result, the query creates two `EmpName` elements. The first `EmpName` element has the `FirstName` element child and the second `EmpName` element has the `MiddleName` and `LastName` element children.

This is the result:

```xml
<row EmpID="1">
  <EmpName>
    <First>Gustavo</First>
  </EmpName>
  <Address>
    <AddrLine1>7726 Driftwood Drive</AddrLine1>
    <City>Monroe</City>
  </Address>
  <EmpName>
    <Last>Achong</Last>
  </EmpName>
</row>
```

## See also

- [Use PATH Mode with FOR XML](../../relational-databases/xml/use-path-mode-with-for-xml.md)
