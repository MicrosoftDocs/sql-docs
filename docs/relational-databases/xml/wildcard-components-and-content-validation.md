---
title: "Wildcard Components and Content Validation"
description: Learn how the XML Element and Attribute wildcard components are used to increase flexibility in what is allowed to appear in a content model.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "wildcard components [XML]"
  - "content validation [XML]"
author: MikeRayMSFT
ms.author: mikeray
---
# Wildcard components and content validation

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Wildcard components are used to increase flexibility in what is allowed to appear in a content model. These components are supported in the XSD language in the following ways:

- Element wildcard components. These are represented by the `<xsd:any>` element.

- Attribute wildcard components. These are represented by the `<xsd:anyAttribute>` element.

Both wildcard character elements, `<xsd:any>` and `<xsd:anyAttribute>`, support the use of a `processContents` attribute. This lets you specify a value that indicates how XML applications handle the validation of document content associated with these wildcard character elements. These are the different values and their effect:

- The **strict** value specifies that the contents are fully validated.

- The **skip** value specifies that the contents aren't validated.

- The **lax** value specifies that only elements and attributes for which schema definitions are available are validated.

## Lax validation and xs:anyType elements

The XML Schema specification uses **lax** validation for elements of the **anyType** type. Because [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] didn't support lax validation, strict validation was applied for elements of the **anyType**. Beginning with [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], lax validation is supported. Content of elements of type **anyType** will be validated using lax validation.

The following example illustrates the lax validation. The schema element `e` is of the **anyType** type. The example creates typed **xml** variables and illustrates the lax validation of the element of the **anyType** type.

```sql
CREATE XML SCHEMA COLLECTION SC AS '
<schema xmlns="http://www.w3.org/2001/XMLSchema"
        targetNamespace="http://ns">
   <element name="e" type="anyType"/>
   <element name="a" type="byte"/>
   <element name="b" type="string"/>
</schema>';
GO
```

The following example succeeds, because the validation of `<e>` is successful:

```sql
DECLARE @var XML(SC);
SET @var = '<e xmlns="http://ns"><a>1</a><b>data</b></e>';
GO
```

The following example succeeds. The instance is accepted, even though no element `<c>` is defined in the schema:

```sql
DECLARE @var XML(SC);
SET @var = '<e xmlns="http://ns"><a>1</a><c>Wrong</c><b>data</b></e>';
GO
```

The XML instance in the following example is rejected, because the definition of the `<a>` element doesn't allow a string value.

```sql
DECLARE @var XML(SC);
SET @var = '<e xmlns="http://ns"><a>Wrong</a><b>data</b></e>';
SELECT @var;
GO
```

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
