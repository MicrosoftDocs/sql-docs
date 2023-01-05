---
title: "Request schemas as results with XMLDATA & XMLSCHEMA"
description: Learn how to use the XMLDATA and XMLSCHEMA options in RAW mode with the FOR XML clause to request an XML-DATA schema or an XSD schema in the query result.
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "RAW mode, requesting schema example"
  - "RAW mode, with XMLDATA and XMLSCHEMA"
author: MikeRayMSFT
ms.author: mikeray
ms.custom: "seo-lt-2019"
---
# Request schemas as results with XMLDATA and XMLSCHEMA

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The following query returns the XML-DATA schema that describes the document structure.

## Example

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, XMLDATA;
GO
```

This is the result:

```xml
<Schema name="Schema1" xmlns="urn:schemas-microsoft-com:xml-data"
        xmlns:dt="urn:schemas-microsoft-com:datatypes">
  <ElementType name="row" content="empty" model="closed">
    <AttributeType name="ProductModelID" dt:type="i4" />
    <AttributeType name="Name" dt:type="string" />
    <attribute type="ProductModelID" />
    <attribute type="Name" />
  </ElementType>
</Schema>
<row xmlns="x-schema:#Schema1" ProductModelID="122" Name="All-Purpose Bike Stand" />
<row xmlns="x-schema:#Schema1" ProductModelID="119" Name="Bike Wash" />
```

> [!NOTE]
>  The `<Schema>` is declared as a namespace. To avoid namespace collisions when multiple XML-Data schemas are requested in different FOR XML queries, the namespace identifier, `Schema1` in this example, changes with every query execution. The namespace identifier is made up of **Schema**_**n**_ where _**n**_ is an integer.

By specifying the `XMLSCHEMA` option, you can request the XSD schema for the result.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, XMLSCHEMA;
GO
```

This is the result:

```xml
<xsd:schema targetNamespace="urn:schemas-microsoft-com:sql:SqlRowSet1" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="https://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified">
  <xsd:import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="https://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" />
  <xsd:element name="row">
    <xsd:complexType>
      <xsd:attribute name="ProductModelID" type="sqltypes:int" use="required" />
      <xsd:attribute name="Name" use="required">
        <xsd:simpleType sqltypes:sqlTypeAlias="[AdventureWorks].[dbo].[Name]">
          <xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52">
            <xsd:maxLength value="50" />
          </xsd:restriction>
        </xsd:simpleType>
      </xsd:attribute>
    </xsd:complexType>
  </xsd:element>
</xsd:schema>
<row xmlns="urn:schemas-microsoft-com:sql:SqlRowSet1" ProductModelID="122" Name="All-Purpose Bike Stand" />
<row xmlns="urn:schemas-microsoft-com:sql:SqlRowSet1" ProductModelID="119" Name="Bike Wash" />

```

You can specify the target namespace URI as an optional argument to XMLSCHEMA in FOR XML. This returns the specified target namespace in the schema. This target namespace remains the same every time you execute the query. For example, the following modified version of the previous query includes the namespace URI, `'urn:example.com'`, as an argument.

```sql
USE AdventureWorks2012;
GO
SELECT ProductModelID, Name
FROM Production.ProductModel
WHERE ProductModelID IN (122, 119)
FOR XML RAW, XMLSCHEMA ('urn:example.com');
GO
```

This is the result:

```xml
<xsd:schema targetNamespace="urn:example.com" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:sqltypes="https://schemas.microsoft.com/sqlserver/2004/sqltypes" elementFormDefault="qualified">
  <xsd:import namespace="https://schemas.microsoft.com/sqlserver/2004/sqltypes" schemaLocation="https://schemas.microsoft.com/sqlserver/2004/sqltypes/sqltypes.xsd" />
  <xsd:element name="row">
    <xsd:complexType>
      <xsd:attribute name="ProductModelID" type="sqltypes:int" use="required" />
      <xsd:attribute name="Name" use="required">
        <xsd:simpleType sqltypes:sqlTypeAlias="[AdventureWorks].[dbo].[Name]">
          <xsd:restriction base="sqltypes:nvarchar" sqltypes:localeId="1033" sqltypes:sqlCompareOptions="IgnoreCase IgnoreKanaType IgnoreWidth" sqltypes:sqlSortId="52">
            <xsd:maxLength value="50" />
          </xsd:restriction>
        </xsd:simpleType>
      </xsd:attribute>
    </xsd:complexType>
  </xsd:element>
</xsd:schema>
<row xmlns="urn:example.com" ProductModelID="122" Name="All-Purpose Bike Stand" />
<row xmlns="urn:example.com" ProductModelID="119" Name="Bike Wash" />
```

## See also

- [Use RAW Mode with FOR XML](../../relational-databases/xml/use-raw-mode-with-for-xml.md)
