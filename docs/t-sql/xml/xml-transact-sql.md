---
title: xml (Transact-SQL)
description: xml (Transact-SQL)
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "xml_TSQL"
  - "xml"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xml data type [SQL Server], about xml data type"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: ""
ms.custom: ""
ms.date: "07/26/2017"
---

# xml (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

It's the data type that stores XML data. You can store **xml** instances in a column, or a variable of **xml** type.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```sql
xml [ ( [ CONTENT | DOCUMENT ] xml_schema_collection ) ]
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

CONTENT  
Restricts the **xml** instance to be a well-formed XML fragment. The XML data can contain multiple zero or more elements at the top level. Text nodes are also allowed at the top level.  
  
This is the default behavior.  
  
DOCUMENT  
Restricts the **xml** instance to be a well-formed XML document. The XML data must have one and only one root element. Text nodes aren't allowed at the top level.  
  
*xml_schema_collection*  
Is the name of an XML schema collection. To create a typed **xml** column or variable, you can optionally specify the XML schema collection name. For more information about typed and untyped XML, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
## Remarks

The stored representation of **xml** data type instances can't exceed 2 gigabytes (GB) in size.  
  
The CONTENT and DOCUMENT facets apply only to typed XML. For more information, see [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md).  
  
## Examples  
  
```sql
USE AdventureWorks;  
GO  
DECLARE @DemographicData XML (Person.IndividualSurveySchemaCollection);  
SET @DemographicData = (SELECT TOP 1 Demographics FROM Person.Person);  
SELECT @DemographicData;  
GO  
```  
  
## See Also

- [Data Type Conversion &#40;Database Engine&#41;](../../t-sql/data-types/data-type-conversion-database-engine.md)
- [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
- [xml Data Type Methods](../../t-sql/xml/xml-data-type-methods.md)
- [XQuery Language Reference &#40;SQL Server&#41;](../../xquery/xquery-language-reference-sql-server.md)