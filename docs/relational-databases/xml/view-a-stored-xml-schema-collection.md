---
title: "View a Stored XML Schema Collection"
description: Learn how to view a stored XML schema collection by using the XQuery function xml_schema_namespace().
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "schema collections [SQL Server], viewing"
  - "XML schemas [SQL Server], viewing"
  - "CREATE XML SCHEMA COLLECTION statement"
  - "xml_schema_namespace function"
  - "XML schema collections [SQL Server], viewing"
  - "displaying XML schema collections"
  - "viewing XML schema collections"
author: MikeRayMSFT
ms.author: mikeray
---
# View a stored XML schema collection

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

After you import an XML schema collection by using [CREATE XML SCHEMA COLLECTION](../../t-sql/statements/create-xml-schema-collection-transact-sql.md), the schema components are stored in the metadata. You can use the [xml_schema_namespace](../../t-sql/xml/xml-schema-namespace.md)intrinsic function to reconstruct the XML schema collection. This function returns an **xml** data type instance.

For example, the following query retrieves an XML schema collection (`ProductDescriptionSchemaCollection`) from the production relational schema in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.

```sql
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection');
GO
```

If you want to see only one schema from the XML schema collection, you can specify XQuery against the **xml** type result that is returned by `xml_schema_namespace`.

```sql
SELECT xml_schema_namespace(N'RelationalSchemaName',N'XmlSchemaCollectionName').query('
/xs:schema[@targetNamespace="TargetNameSpace"]
');
GO
```

For example, the following query retrieves product warranty and maintenance XML schema information from the `ProductDescriptionSchemaCollection` XML schema collection.

```sql
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection').query('
/xs:schema[@targetNamespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain"]
');
GO
```

You can also pass the optional target namespace as the third parameter to the `xml_schema_namespace` function to retrieve specific schema from the collection, as shown in the following query:

```sql
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection', N'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain');
GO
```

When you create an XML schema collection by using CREATE XML SCHEMA COLLECTION in the database, the statement stores the schema components in the metadata. Only the schema components that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] understands are stored. Any comments, annotations, or non-XSD attributes aren't stored. Therefore, the schema reconstructed by **xml_schema_namespace** is functionally equivalent to the original schema, but it will not necessarily look the same. For example, you won't see the same prefixes you had in the original schema. The schema returned by **xml_schema_namespace** uses **t** as the prefix for the target namespace and **ns1**, **ns2**, and so on, for other namespaces.

If you want to retain an identical copy of the XML schemas, you should save your XML schema in a file or in a database table in an **xml** type column.

The [sys.xml_schema_collections](../../relational-databases/system-catalog-views/sys-xml-schema-collections-transact-sql.md) catalog view also returns information about XML schema collections. This information includes the name of the collection, the creation date, and the owner of the collection.

## See also

- [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md)
