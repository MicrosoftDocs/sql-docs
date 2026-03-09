---
title: SQL Projects XML Schema Collections
description: "Automatic generation of XML schema collections in SQL database projects."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 03/03/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: concept-article
ai-usage: ai-assisted
ms.collection:
  - data-tools
---

# XML schema collections in SQL projects

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

SQL database projects support automatic generation of XML schema collection objects from XSD (XML Schema Definition) files. When you include an XSD file in your project with the appropriate metadata, the build process creates a `CREATE XML SCHEMA COLLECTION` statement that you can use to enforce XML data validation in your database.

XML schema collections provide typed XML columns in SQL Server, enabling the database engine to validate XML data against defined schemas and optimize query performance.

## Configure XSD files in the project

To generate an XML schema collection from an XSD file, add a `Build` item to your project file (`.sqlproj`) with two required metadata elements:

- **RelationalSchema**: The database schema where the XML schema collection is created (for example, `dbo`)
- **XMLSchemaCollectionName**: The name of the resulting XML schema collection object in the database

The following example shows how to configure an XSD file in a SQL project:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build">
  <Sdk Name="Microsoft.Build.Sql" Version="2.1.0" />
  <PropertyGroup>
    <Name>MyDatabase</Name>
    <DSP>Microsoft.Data.Tools.Schema.Sql.Sql170DatabaseSchemaProvider</DSP>
    <ModelCollation>1033, CI</ModelCollation>
  </PropertyGroup>
  <ItemGroup>
    <Build Include="OrderSchema.xsd">
      <RelationalSchema>dbo</RelationalSchema>
      <XMLSchemaCollectionName>OrderSchemaCollection</XMLSchemaCollectionName>
    </Build>
  </ItemGroup>
</Project>
```

## XSD file example

The XSD file defines the structure that XML data must follow. The following example defines a schema for an `Order` element containing one or more `Item` elements:

```xml
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="Order">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="Item" type="xs:string" maxOccurs="unbounded"/>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>
```

When the project builds, this XSD file generates a `CREATE XML SCHEMA COLLECTION` statement that creates the `OrderSchemaCollection` object in the `dbo` schema.

## Use typed XML columns

After the XML schema collection is defined, reference it in table definitions to create typed XML columns. Typed XML columns validate data against the schema and improve query performance.

The following example creates a table with a typed XML column that uses the generated schema collection:

```sql
CREATE TABLE dbo.Orders (
    Id INT PRIMARY KEY,
    OrderData XML(dbo.OrderSchemaCollection)
);
```

When you insert data into the `OrderData` column, SQL Server validates the XML against the `OrderSchemaCollection` schema. Invalid XML that doesn't conform to the schema is rejected.

## Related content

- [XML schema collections (SQL Server)](../../../relational-databases/xml/xml-schema-collections-sql-server.md)
- [XML data (SQL Server)](../../../relational-databases/xml/xml-data-sql-server.md)
- [Project properties](project-properties.md)