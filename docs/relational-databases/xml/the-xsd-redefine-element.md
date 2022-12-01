---
title: "The &lt;xsd:redefine&gt; Element"
description: Learn about support for the W3C XSD redefine element and how to update an XML schema or its components.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "xsd:redefine element"
author: MikeRayMSFT
ms.author: mikeray
---
# The &lt;xsd:redefine&gt; element

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The W3C XSD **redefine** element provides support for redefining schema components. However, support for this directive is potentially costly to performance and also requires that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] revalidate all instances of the **xml** data type associated with the redefined schema. Therefore, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't support this element. XML schemas that include the **\<xsd:redefine>** element are rejected by the server.

To update a schema or its components, you can do the following instead:

1. Create a new XML Schema collection with the modified schema components.

1. Retype all **xml** data types (XML DT) that use the XML Schema collection to be redefined to use the new XML Schema collection instead. To do this, use the ALTER COLUMN option of the ALTER TABLE command for retyping columns, or change the XML Schema collection constraints on variables or parameters.

1. Drop the old version of the XML Schema collection.

## See also

- [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)
