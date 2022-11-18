---
title: "XML schema collections (SQL Server)"
description: Learn how the XML schema collection stores imported XML schemas to validate XML instances and type the XML data as it is stored in an SQL Server database.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "XSD schemas [SQL Server]"
  - "xml_schema_namespace function"
  - "XML schema collections [SQL Server], about XML schema collections"
  - "metadata [SQL Server], XML schema collections"
  - "queries [XML in SQL Server], XML schema collections"
  - "schema collections [SQL Server]"
  - "schemas [SQL Server], XML"
  - "XML [SQL Server], schema collections"
  - "XML schema collections [SQL Server]"
  - "schema collections [SQL Server], about XML schema collections"
author: MikeRayMSFT
ms.author: mikeray
---
# XML schema collections (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

As described in the [xml &#40;Transact-SQL&#41;](../../t-sql/xml/xml-transact-sql.md) article, SQL Server provides native storage of XML data through the **xml** data type. You can optionally associate XSD schemas with a variable or a column of **xml** type through an XML schema collection. The XML schema collection stores the imported XML schemas and is then used to do the following:

- Validate XML instances

- Type the XML data as it is stored in the database

The XML schema collection is a metadata entity like a table in the database. You can create, modify, and drop them. Schemas specified in a [CREATE XML SCHEMA COLLECTION (Transact-SQL)](../../t-sql/statements/create-xml-schema-collection-transact-sql.md) statement are automatically imported into the newly created XML schema collection object. You can import additional schemas or schema components into an existing collection object in the database by using the [ALTER XML SCHEMA COLLECTION (Transact-SQL)](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md) statement.

As described in the article, [Typed vs. Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md), the XML stored in a column or variable that a schema is associated with is referred to as **typed** XML, because the schema provides the necessary data type information for the instance data. SQL Server uses this type information to optimize data storage.

The query-processing engine also uses the schema for type checking and to optimize queries and data modification.

Also, SQL Server uses the associated XML schema collection, with typed **xml**, to validate the XML instance. If the XML instance complies with the schema, the database allows the instance to be stored in the system with their type information. Otherwise, it rejects the instance.

You can use the intrinsic function XML_SCHEMA_NAMESPACE to retrieve the schema collection that is stored in the database. For more information, see [View a Stored XML Schema Collection](../../relational-databases/xml/view-a-stored-xml-schema-collection.md).

You can also use the XML schema collection to type XML variables, parameters, and columns.

## <a id="ddl"></a> DDL for managing schema collections

You can create XML schema collections in the database and associate them with variables and columns of **xml** type. To manage schema collections in the database, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides the following DDL statements:

- [CREATE XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-xml-schema-collection-transact-sql.md) Imports schema components into a database.

- [ALTER XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md) Modifies the schema components in an existing XML schema collection.

- [DROP XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md) Deletes a complete XML schema collection and all its components.

To use an XML schema collection and the schemas it contains, you must first create the collection and the schemas by using the CREATE XML SCHEMA COLLECTION statement. After the schema collection is created, you can then create variables and columns of **xml** type and associate the schema collection with them. After a schema collection is created, various schema components are stored in the metadata. You can also use the ALTER XML SCHEMA COLLECTION to add more components to the existing schemas or add new schemas to an existing collection.

To drop the schema collection, use the DROP XML SCHEMA COLLECTION statement. This drops all schemas that are contained in the collection and removes the collection object. Before you can drop a schema collection, the conditions described in [DROP XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-xml-schema-collection-transact-sql.md)must be met.

## <a id="components"></a> Understand schema components

When you use the CREATE XML SCHEMA COLLECTION statement, various schema components are imported into the database. Schema components include schema elements, attributes, and type definitions. When you use the DROP XML SCHEMA COLLECTION statement, you remove the complete collection.

CREATE XML SCHEMA COLLECTION saves the schema components into various system tables.

For example, consider the following schema:

```xml
<?xml version="1.0"?>
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"
            targetNamespace="uri:Cust_Orders2"
            xmlns="uri:Cust_Orders2" >
  <xsd:attribute name="SomeAttribute" type="xsd:int" />
  <xsd:complexType name="SomeType" />
  <xsd:complexType name="OrderType" >
    <xsd:sequence>
      <xsd:element name="OrderDate" type="xsd:date" />
      <xsd:element name="RequiredDate" type="xsd:date" />
      <xsd:element name="ShippedDate" type="xsd:date" />
    </xsd:sequence>
    <xsd:attribute name="OrderID" type="xsd:ID" />
    <xsd:attribute name="CustomerID"  />
    <xsd:attribute name="EmployeeID"  />
  </xsd:complexType>
  <xsd:complexType name="CustomerType" >
     <xsd:sequence>
        <xsd:element name="Order" type="OrderType"
                     maxOccurs="unbounded" />
       </xsd:sequence>
      <xsd:attribute name="CustomerID" type="xsd:string" />
      <xsd:attribute name="OrderIDList" type="xsd:IDREFS" />
  </xsd:complexType>
  <xsd:element name="Customer" type="CustomerType" />
</xsd:schema>
```

The previous schema shows the different types of components that can be stored in the database. These include `SomeAttribute`, `SomeType`, `OrderType`, `CustomerType`, `Customer`, `Order`, `CustomerID`, `OrderID`, `OrderDate`, `RequiredDate`, and `ShippedDate`.

### Component categories

The Schema components stored in the database fall into the following categories:

- ELEMENT

- ATTRIBUTE

- TYPE (for simple or complex types)

- ATTRIBUTEGROUP

- MODELGROUP

For example:

- `SomeAttribute` is an ATTRIBUTE component.

- `SomeType`, `OrderType`, and `CustomerType` are TYPE components.

- `Customer` is an ELEMENT component.

When you import a schema into the database, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't store the schema itself. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stores the various individual components. That is, the \<Schema> tag is not stored, only the components that are defined within it are preserved. All schema elements are not preserved. If the \<Schema> tag contains attributes that specify default behavior of its components, these attributes are moved to the schema components within it during the import process, as shown in the following table.

|Attribute name|Behavior|
|--------------------|--------------|
|**attributeFormDefault**|The **form** attribute applied to all attribute declarations in the schema where it is not already present and the value is set to the value of the **attributeFormDefault** attribute.|
|**elementFormDefault**|The **form** attribute applied to all element declarations in the schema where it is not already present and the value is set to the value of the **elementFormDefault** attribute.|
|**blockDefault**|The **block** attribute applied to all element declarations and type definitions where it is not already present and the value is set to the value of the **blockDefault** attribute.|
|**finalDefault**|The **final** attribute applied to all element declarations and type definitions where it is not already present and the value is set to the value of the **finalDefault** attribute.|
|**targetNamespace**|Information about the components that belong to the target namespace is stored in the metadata.|

## <a id="perms"></a> Permissions on an XML schema collection

You must have the necessary permissions to do the following:

- Create/load the XML schema collection

- Modify the XML schema collection

- Drop the XML schema collection

- Use the XML schema collection to type **xml** type columns, variables, and parameters, or use it in table or column constraints

The SQL Server security model allows CONTROL permission on every object. The grantee of this permission obtains all other permissions on the object. The owner of the object also has all the permissions on the object.

The owner and the grantee of the CONTROL permission on an object can grant any permission on the object. A user who is not the owner and doesn't have CONTROL permission can still grant permission on an object when WITH GRANT OPTION is specified. For example, assume User A has REFERENCES permission on XML schema collection S, through WITH GRANT OPTION, but no other permissions on S. User A could grant User B REFERENCES permission on schema collection S.

The security model also allows permissions to create and use XML schema collections or transfer ownership from one user to another. The following articles describe the XML schema collection permissions.

- [Grant Permissions on an XML Schema Collection](../../relational-databases/xml/grant-permissions-on-an-xml-schema-collection.md)

  This article discusses how to grant permissions to create an XML schema collection and how to grant permissions on an XML schema collection object.

- [Revoke Permissions on an XML Schema Collection](../../relational-databases/xml/revoke-permissions-on-an-xml-schema-collection.md)

  This article discusses how revoking permissions can be used to prevent the creation of an XML schema collection and how to revoke permissions on an XML schema collection object.

- [Deny Permissions on an XML Schema Collection](../../relational-databases/xml/deny-permissions-on-an-xml-schema-collection.md)

  This article discusses how to deny permissions to create an XML schema collection and deny permission on an XML schema collection object.

## <a id="info"></a> Get information about XML schemas and schema collections

XML schema collections are enumerated in the catalog view, sys.xml_schema_collections. The XML schema collection `sys` is defined by the system. It contains the predefined namespaces that can be used in all user-defined XML schema collections without having to load them explicitly. This list contains the namespaces for `xml`, `xs`, `xsi`, `fn`, and `xdt`. Two other catalog views are `sys.xml_schema_namespaces`, which enumerates all namespaces within each XML schema collection, and `sys.xml_components`, which enumerates all XML schema components within each XML schema.

The built-in function `XML_SCHEMA_NAMESPACE`, `schemaName, XmlSchemacollectionName, namespace-uri`, yields an **xml** data type instance. This instance contains XML schema fragments for schemas that are contained in an XML schema collection, except the predefined XML schemas.

You can enumerate the contents of an XML schema collection in the following ways:

- Write Transact-SQL queries on the appropriate catalog views for XML schema collections.

- Use the built-in function `XML_SCHEMA_NAMESPACE()`. You can apply **xml** data type methods on the output of this function. However, you can't modify the underlying XML schemas.

These are illustrated in the following examples.

### Examples

#### Enumerate the XML namespaces in an XML schema collection

Use the following query for the XML schema collection `myCollection`:

```sql
SELECT XSN.name
FROM    sys.xml_schema_collections XSC JOIN sys.xml_schema_namespaces XSN
    ON (XSC.xml_collection_id = XSN.xml_collection_id)
WHERE    XSC.name = 'myCollection';
```

#### Enumerate the contents of an XML schema collection

The following statement enumerates the contents of the XML schema collection `myCollection` within the relational schema, `dbo`.

```sql
SELECT XML_SCHEMA_NAMESPACE (N'dbo', N'myCollection');
```

Individual XML schemas within the collection can be obtained as **xml** data type instances by specifying the target namespace as the third argument to **XML_SCHEMA_NAMESPACE()**. This is shown in the following example.

#### Output a specified schema from an XML schema collection

The following statement outputs the XML schema with the *pretend* target namespace `https://www.microsoft.com/was-books` from the XML schema collection `myCollection` within the relational schema, `dbo`.

```sql
SELECT XML_SCHEMA_NAMESPACE (N'dbo', N'myCollection',
N'https://www.microsoft.com/was-books')
```

### Query XML schemas

You can query XML schemas that you have loaded into XML schema collections in the following ways:

- Write Transact-SQL queries on catalog views for XML schema namespaces.

- Create a table that contains an **xml** data type column to store your XML schemas and also load them into the XML type system. You can query the XML column by using the **xml** data type methods. Also, you can build an XML index on this column. However, with this approach, the application must maintain consistency between the XML schemas stored in the XML column and the XML type system. For example, if you drop the XML schema namespace from the XML type system, you also have to drop it from the table in order to preserve consistency.

## See also

- [View a Stored XML Schema Collection](../../relational-databases/xml/view-a-stored-xml-schema-collection.md)
- [Preprocess a Schema to Merge Included Schemas](../../relational-databases/xml/preprocess-a-schema-to-merge-included-schemas.md)
- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
