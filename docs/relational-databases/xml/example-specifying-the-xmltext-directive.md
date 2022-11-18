---
title: "Example: Specifying the XMLTEXT Directive"
description: Learn how to address the unconsumed part of an XML document by specifying the XMLTEXT directive in a SELECT statement using EXPLICIT mode.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "XMLTEXT directive"
author: MikeRayMSFT
ms.author: mikeray
---
# Example: Specify the XMLTEXT directive

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This example illustrates how data in the overflow column is addressed by using the **XMLTEXT** directive in a `SELECT` statement using EXPLICIT mode.

Consider the `Person` table. This table has an `Overflow` column that stores the unconsumed part of the XML document.

```sql
USE tempdb;
GO
CREATE TABLE Person(PersonID varchar(5), PersonName varchar(20), Overflow nvarchar(200));
GO
INSERT INTO Person VALUES
    ('P1','Joe',N'<SomeTag attr1="data">content</SomeTag>')
   ,('P2','Joe',N'<SomeTag attr2="data"/>')
   ,('P3','Joe',N'<SomeTag attr3="data" PersonID="P">content</SomeTag>');
```

This query retrieves columns from the `Person` table. For the `Overflow` column, *AttributeName* isn't specified, but *directive* is set to `XMLTEXT` as part of providing a universal table column name.

```sql
SELECT 1 as Tag, NULL as parent,
       PersonID as [Parent!1!PersonID],
       PersonName as [Parent!1!PersonName],
       Overflow as [Parent!1!!XMLTEXT] -- No AttributeName; XMLTEXT directive
FROM Person
FOR XML EXPLICIT;
```

In the resulting XML document:

- Because *AttributeName* isn't specified for the `Overflow` column and the `xmltext` directive is specified, the attributes in the `<overflow>` element are appended to the attribute list of the enclosing `<Parent>` element.

- Because the `PersonID`attribute in the `<xmltext>` element conflicts with the `PersonID` attribute retrieved on the same element level, the attribute in the `<xmltext>` element is ignored, even if `PersonID` is NULL. Generally, an attribute overrides an attribute of the same name in the overflow.

This is the result:

```xml
<Parent PersonID="P1" PersonName="Joe" attr1="data">content</Parent>
<Parent PersonID="P2" PersonName="Joe" attr2="data"></Parent>
<Parent PersonID="P3" PersonName="Joe" attr3="data">content</Parent>
```

If the overflow data has subelements and the same query is specified, the subelements in the `Overflow` column are added as the subelements of the enclosing `<Parent>` element.

For example, change the data in the `Person` table so that the `Overflow` column now has subelements.

```sql
USE tempdb;
GO
TRUNCATE TABLE Person;
GO
INSERT INTO Person VALUES
    ('P1','Joe',N'<SomeTag attr1="data">content</SomeTag>')
   ,('P2','Joe',N'<SomeTag attr2="data"/>')
    ,('P3','Joe',N'<SomeTag attr3="data" PersonID="P"><name>PersonName</name></SomeTag>');
```

If the same query is executed, the subelements in the `<xmltext>` element are added as subelements of the enclosing `<Parent>` element:

```sql
SELECT 1 as Tag, NULL as parent,
       PersonID as [Parent!1!PersonID],
       PersonName as [Parent!1!PersonName],
       Overflow as [Parent!1!!XMLTEXT] -- no AttributeName, XMLTEXT directive
FROM Person
FOR XML EXPLICIT;
```

This is the result:

```xml
<Parent PersonID="P1" PersonName="Joe" attr1="data">content</Parent>
<Parent PersonID="P2" PersonName="Joe" attr2="data"></Parent>
<Parent PersonID="P3" PersonName="Joe" attr3="data">
<name>PersonName</name>
</Parent>
```

If *AttributeName* is specified with the `xmltext` directive, the attributes of the `<overflow>` element are added as attributes of the subelements of the enclosing `<Parent>` element. The name specified for *AttributeName* becomes the name of the subelement

In this query, *AttributeName*, `<overflow>`, is specified together with the `xmltext` directive*:*

```sql
SELECT 1 as Tag, NULL as parent,
       PersonID as [Parent!1!PersonID],
       PersonName as [Parent!1!PersonName],
       Overflow as [Parent!1!overflow!XMLTEXT] -- Overflow is AttributeName
                      -- XMLTEXT is a directive
FROM Person
FOR XML EXPLICIT;
```

This is the result:

```xml
<Parent PersonID="P1" PersonName="Joe">
<overflow attr1="data">content</overflow>
</Parent>
<Parent PersonID="P2" PersonName="Joe">
<overflow attr2="data" />
</Parent>
<Parent PersonID="P3" PersonName="Joe">
<overflow attr3="data" PersonID="P">
<name>PersonName</name>
</overflow>
</Parent>
```

In this query element, *directive* is specified for `PersonName` attribute. This results in `PersonName` being added as a subelement of the enclosing `<Parent>` element. The attributes of the `<xmltext>` are still appended to the enclosing `<Parent>` element. The contents of the `<overflow>` element, subelements, are prepended to the other subelements of the enclosing `<Parent>` elements.

```sql
SELECT 1      AS Tag, NULL as parent,
       PersonID   AS [Parent!1!PersonID],
       PersonName AS [Parent!1!PersonName!element], -- element directive
       Overflow   AS [Parent!1!!XMLTEXT]
FROM Person
FOR XML EXPLICIT;
```

This is the result:

```xml
<Parent PersonID="P1" attr1="data">content<PersonName>Joe</PersonName>
</Parent>
<Parent PersonID="P2" attr2="data">
<PersonName>Joe</PersonName>
</Parent>
<Parent PersonID="P3" attr3="data">
<name>PersonName</name>
<PersonName>Joe</PersonName>
</Parent>
```

If the `XMLTEXT` column data contains attributes on the root element, these attributes aren't shown in the XML data schema and the MSXML parser doesn't validate the resulting XML document fragment. For example:

```sql
SELECT 1 AS Tag,
       0 AS Parent,
       N'<overflow a="1"/>' AS 'overflow!1!!xmltext'
FOR XML EXPLICIT, xmldata;
```

This is the result. In the returned schema, the overflow attribute `a` is missing from the schema:

```xml
<Schema name="Schema2"
xmlns="urn:schemas-microsoft-com:xml-data"
xmlns:dt="urn:schemas-microsoft-com:datatypes">
<ElementType name="overflow" content="mixed" model="open">`
</ElementType>`
</Schema>`
<overflow xmlns="x-schema:#Schema2" a="1">
</overflow>
```

## See also

- [Use EXPLICIT Mode with FOR XML](../../relational-databases/xml/use-explicit-mode-with-for-xml.md)
