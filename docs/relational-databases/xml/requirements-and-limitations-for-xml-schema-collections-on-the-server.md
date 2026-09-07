---
title: "Requirements and limitations (XML schema collections)"
description: Learn about the requirements and limitations for modifying your XML schema collections in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 05/05/2022
ms.service: sql
ms.subservice: xml
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "identifiers [XML schema collections]"
  - "XML schema collections [SQL Server], limitations"
  - "substitution groups [XML in SQL Server]"
  - "XML schema collections [SQL Server], guidelines"
  - "lax validation"
  - "enumeration facets [XML in SQL Server]"
  - "decimal precision [XML in SQL Server]"
  - "repeated XML schema collection values"
  - "schema collections [SQL Server], limitations"
  - "time zones [XML in SQL Server]"
  - "precision decimals [XML in SQL Server]"
  - "schema collections [SQL Server], guidelines"
  - "lexical representation"
---
# Requirements and limitations for XML schema collections on the server

[!INCLUDE [SQL Server Azure SQL Database FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The XML schema definition language (XSD) validation has some limitations regarding SQL columns that use the **xml** data type. The following table provides details about those limitations and guidelines for modifying your XSD schema so it can work with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The articles in this section provide additional information about specific limitations and guidance for working with them.

|Item|Limitation|
|----------|----------------|
|**minOccurs** and **maxOccurs**|The values for **minOccurs** and **maxOccurs** attributes must fit into 4-byte integers. Schemas that don't conform are rejected by the server.|
|**\<xsd:choice>**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects schemas that have an **\<xsd:choice>** particle without children, unless the particle is defined with a **minOccurs** attribute value of zero.|
|**\<xsd:include>**|Currently, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support this element. XML schemas that include this element are rejected by the server.<br /><br /> As a solution, XML schemas that include the **\<xsd:include>** directive can be preprocessed to copy and merge the contents of any included schemas into a single schema for upload to the server. For more information, see [Preprocess a Schema to Merge Included Schemas](../../relational-databases/xml/preprocess-a-schema-to-merge-included-schemas.md).|
|**\<xsd:key>**, **\<xsd:keyref>**, and **\<xsd:unique>**|Currently, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support these XSD-based constraints for enforcing uniqueness or establishing keys and key references. XML schemas that contain these elements can't be registered.|
|**\<xsd:redefine>**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support this element. For information about another way to update schemas, see [The &#60;xsd:redefine&#62; Element](../../relational-databases/xml/the-xsd-redefine-element.md).|
|**\<xsd:simpleType>** values|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only supports millisecond precision for simple types that have second components other than **xs:time** and **xs:dateTime**, and 100-nanosecond precision for **xs:time** and **xs:dateTime**. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] puts limitations on all recognized XSD simple type enumerations.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support using the "NaN" value in **\<xsd:simpleType>** declarations.<br /><br /> For more information, see [Values for &#60;xsd:simpleType&#62; Declarations](../../relational-databases/xml/values-for-xsd-simpletype-declarations.md).|
|**xsi:schemaLocation** and **xsi:noNamespaceSchemaLocation**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ignores these attributes if they are present in the XML instance data inserted into a column or variable of **xml** data type.|
|**xs:QName**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support types derived from **xs:QName** that use an XML Schema restriction element.<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support union types with **xs:QName** as a member element.<br /><br /> For more information, see [The xs:QName Type](../../relational-databases/xml/the-xs-qname-type.md).|
|Adding members to an existing substitution group|You can't add members to an existing substitution group in an XML schema collection. A substitution group in an XML schema is restricted in that the head element and all its member elements must be defined in the same {CREATE &#124; ALTER} XML SCHEMA COLLECTION statement.|
|Canonical forms and pattern restrictions|The canonical representation of a value can't violate the pattern restriction for its type. For more information, see [Canonical Forms and Pattern Restrictions](../../relational-databases/xml/canonical-forms-and-pattern-restrictions.md).|
|Enumeration facets|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support XML schemas with types that have pattern facets or enumerations that violate those facets.|
|Facet length|The **length**, **minLength**, and **maxLength** facets are stored as a **long** type. This type is a 32-bit type. Therefore, the range of acceptable values for these values is 2^31.|
|ID attribute|Each XML schema component can have an ID attribute on it. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] enforces uniqueness for **\<xsd:attribute>** declarations of **ID** type, but doesn't store these values. The scope for enforcement of uniqueness is the {CREATE &#124; ALTER} XML SCHEMA COLLECTION statement.|
|ID type|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support elements of type **xs:ID**, **xs:IDREF**, or **xs:IDREFS**. A schema may not declare elements of this type, or elements derived by restriction or extension from this type.|
|Local namespace|The local namespace has to be explicitly specified for the **\<xsd:any>** element. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects schemas that use an empty string ("") as a value for the namespace attribute. Instead, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires the explicit use of "##local" to indicate an unqualified element or attribute as the instance of the wildcard character.|
|Mixed type and simple content|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support restricting a mixed type to a simple content. For more information, see [Mixed Type and Simple Content](../../relational-databases/xml/mixed-type-and-simple-content.md).|
|NOTATION type|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support the NOTATION type.|
|Out-of-memory conditions|When working with large XML schema collections, an out-of-memory condition might occur. For solutions to this problem, see [Large XML Schema Collections and Out-of-Memory Conditions](../../relational-databases/xml/large-xml-schema-collections-and-out-of-memory-conditions.md).|
|Repeated values|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] rejects schemas in which the block or final attribute has repeated values such as "restriction restriction" and "extension extension".|
|Schema component identifiers|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] limits identifiers of schema components to a maximum length of 1000 Unicode characters. Also,  surrogate character pairs within identifiers are not supported.|
|Time zone information|In [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions, time zone information is fully supported for **xs:date**, **xs:time**, and **xs:dateTime** values for XML Schema validation. With [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] backward-compatibility mode, time zone information is always normalized to Coordinated Universal Time (Greenwich Mean Time). For elements of **dateTime** type, the server converts the time provided to GMT by using the offset value ("-05:00") and returning the corresponding GMT time.|
|Union types|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support restrictions from union types.|
|Variable precision decimals|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support variable precision decimals. The **xs:decimal** type represents arbitrary precision decimal numbers. Minimally conforming XML processors must support decimal numbers with a minimum of `totalDigits=18`. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports `totalDigits=38,` but limits the fractional digits to 10. All **xs:decimal** instanced values are represented internally by the server by using the SQL type numeric (38, 10).|

## Related content

- [XML data (SQL Server)](xml-data-sql-server.md)
- [Compare typed XML to untyped XML](compare-typed-xml-to-untyped-xml.md)
- [Grant permissions on an XML schema collection](grant-permissions-on-an-xml-schema-collection.md)
- [Unique particle attribution constraint](unique-particle-attribution-constraint.md)
- [XML schema collections (SQL Server)](xml-schema-collections-sql-server.md)
- [Canonical forms and pattern restrictions](canonical-forms-and-pattern-restrictions.md)
- [Wildcard components and content validation](wildcard-components-and-content-validation.md)
- [The &lt;xsd:redefine&gt; element](the-xsd-redefine-element.md)
- [The xs:QName type](the-xs-qname-type.md)
- [Values for &lt;xsd:simpleType&gt; declarations](values-for-xsd-simpletype-declarations.md)
- [Enumeration facets](enumeration-facets.md)
- [Mixed type and simple content](mixed-type-and-simple-content.md)
- [Large XML schema collections and out-of-memory conditions](large-xml-schema-collections-and-out-of-memory-conditions.md)
- [Non-Deterministic content models](non-deterministic-content-models.md)
