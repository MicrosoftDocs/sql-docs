---
title: "xml_schema_namespace (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "xml_schema_namespace_TSQL"
  - "xml_schema_namespace"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "XML schema collections [SQL Server], reconstructing schemas"
  - "xml_schema_namespace function"
  - "reconstructing schemas"
  - "schemas [SQL Server], XML"
  - "schema collections [SQL Server], reconstructing schemas"
ms.assetid: ee9873d8-dd3a-4bff-a10c-68bbadbdf1a6
author: MightyPen
ms.author: genemi
manager: "craigg"
---
# xml_schema_namespace
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reconstructs all the schemas or a specific schema in the specified XML schema collection. This function returns an **xml** data type instance.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```  
  
xml_schema_namespace( Relational_schema , XML_schema_collection_name , [ Namespace ] )  
```  
  
## Arguments  
 *Relational_schema*  
 Is the relational schema name. *Relational_schema* is **sysname**.  
  
 *XML_schema_collection_name*  
 Is the name of the XML schema collection to reconstruct. *XML_schema_collection_name* is **sysname**.  
  
 *Namespace*  
 Is the namespace URI of the XML schema that you want reconstructed. It is limited to 1,000 characters. If Namespace URI is not provided, the whole XML schema collection is reconstructed. *Namespace* is **nvarchar(4000)**.  
  
## Return Types  
 **xml**  
  
## Remarks  
 When you import XML schema components in the database by using [CREATE XML SCHEMA COLLECTION](../../t-sql/statements/create-xml-schema-collection-transact-sql.md) or [ALTER XML SCHEMA COLLECTION](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md), aspects of the schema used for validation are preserved. Therefore, the reconstructed schema may not be lexically the same as the original schema document. Specifically, comments, white spaces, and annotations are lost; and implicit type information is made explicit. For example, \<xs:element name="e1" /> becomes \<xs:element name="e1" type="xs:anyType"/>. Also, namespace prefixes are not preserved.  
  
 If you specify a namespace parameter, the resulting schema document will contain definitions for all schema components in that namespace, even if they were added in different schema documents or DDL steps, or both.  
  
 You cannot use this function to construct XML schema documents from the **sys.sys** XML schema collection.  
  
## Examples  
 The following example retrieves the  XML schema collection `ProductDescriptionSchemaCollection` from the production relational schema in the `AdventureWorks` database.  
  
```  
USE AdventureWorks;  
GO  
SELECT xml_schema_namespace(N'production',N'ProductDescriptionSchemaCollection');  
GO  
```  
  
## See Also  
 [View a Stored XML Schema Collection](../../relational-databases/xml/view-a-stored-xml-schema-collection.md)   
 [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md)  
  
  
