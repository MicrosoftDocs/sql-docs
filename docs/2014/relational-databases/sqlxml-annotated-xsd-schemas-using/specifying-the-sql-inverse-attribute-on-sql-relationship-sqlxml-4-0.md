---
title: "Specifying the sql:inverse Attribute on sql:relationship (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "sql:relationship"
  - "bulk load [SQLXML]"
  - "inverse attribute"
  - "relationships [SQLXML], inverse orders"
  - "relationship annotation"
  - "XSD schemas [SQLXML], relationships"
  - "annotated XSD schemas, relationships"
  - "updategrams [SQLXML], relationships"
  - "sql:inverse"
ms.assetid: 08904cbd-9c86-493d-90c3-f5e1d13ce59d
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specifying the sql:inverse Attribute on sql:relationship (SQLXML 4.0)
  The `sql:inverse` attribute is useful only when the XSD schema is used for either bulk load or by an updategram. The `sql:inverse` attribute can be specified on the **\<sql:relationship>** element. In updategrams, the updategram logic interprets the schema in determining the tables and columns that are updated by the updategram operation. The parent-child relationships that are specified in the schema determine the order in which the records are modified (inserted or deleted).  
  
 If you have an XSD schema in which the parent-child relationship is specified in the inverse order of the primary-key/foreign-key relationship between the corresponding database columns, the insert or delete updategram operation will fail because of the primary-key/foreign-key violation. In such cases, the `sql:inverse` attribute is specified (`sql:inverse="true"`) in the **\<sql:relationship>** element, and the updategram logic inverses its interpretation of the parent-child relationship specified in the schema.  
  
 The `sql:inverse` attribute takes a Boolean value (0=false, 1=true). The acceptable values are 0, 1, true, and false.  
  
 For a working sample using the `sql:inverse` annotation, see [Specifying an Annotated Mapping Schema in an Updategram](../sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
## See Also  
 [Specifying Relationships Using sql:relationship &#40;SQLXML 4.0&#41;](specifying-relationships-using-sql-relationship-sqlxml-4-0.md)  
  
  
