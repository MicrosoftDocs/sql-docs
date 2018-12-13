---
title: "View a Stored XML Schema Collection | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: conceptual
helpviewer_keywords: 
  - "schema collections [SQL Server], viewing"
  - "XML schemas [SQL Server], viewing"
  - "CREATE XML SCHEMA COLLECTION statement"
  - "xml_schema_namespace function"
  - "XML schema collections [SQL Server], viewing"
  - "displaying XML schema collections"
  - "viewing XML schema collections"
ms.assetid: e38031af-22df-4cd9-a14e-e316b822f91b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# View a Stored XML Schema Collection
  After you import an XML schema collection by using [CREATE XML SCHEMA COLLECTION](/sql/t-sql/statements/create-xml-schema-collection-transact-sql), the schema components are stored in the metadata. You can use the [xml_schema_namespace](/sql/t-sql/xml/xml-schema-namespace)intrinsic function to reconstruct the XML schema collection. This function returns an `xml` data type instance.  
  
 For example, the following query retrieves an XML schema collection (`ProductDescriptionSchemaCollection`) from the production relational schema in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database.  
  
```  
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection')  
GO  
```  
  
 If you want to see only one schema from the XML schema collection, you can specify XQuery against the `xml` type result that is returned by `xml_schema_namespace`.  
  
```  
SELECT xml_schema_namespace(N'RelationalSchemaName',N'XmlSchemaCollectionName').query('  
/xs:schema[@targetNamespace="TargetNameSpace"]  
')  
GO  
```  
  
 For example, the following query retrieves product warranty and maintenance XML schema information from the `ProductDescriptionSchemaCollection` XML schema collection.  
  
```  
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection').query('  
/xs:schema[@targetNamespace="https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain"]  
')  
GO  
```  
  
 You can also pass the optional target namespace as the third parameter to the `xml_schema_namespace` function to retrieve specific schema from the collection, as shown in the following query:  
  
```  
SELECT xml_schema_namespace(N'Production',N'ProductDescriptionSchemaCollection', N'https://schemas.microsoft.com/sqlserver/2004/07/adventure-works/ProductModelWarrAndMain')  
GO  
```  
  
 When you create an XML schema collection by using CREATE XML SCHEMA COLLECTION in the database, the statement stores the schema components in the metadata. Note that only the schema components that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] understands are stored. Any comments, annotations, or non-XSD attributes are not stored. Therefore, the schema reconstructed by **xml_schema_namespace** is functionally equivalent to the original schema, but it will not necessarily look the same. For example, you will not see the same prefixes you had in the original schema. The schema returned by **xml_schema_namespace** uses **t** as the prefix for the target namespace and **ns1**, **ns2**, and so on, for other namespaces.  
  
 If you want to retain an identical copy of the XML schemas, you should save your XML schema in a file or in a database table in an `xml` type column.  
  
 The [sys.xml_schema_collections](/sql/relational-databases/system-catalog-views/sys-xml-schema-collections-transact-sql) catalog view also returns information about XML schema collections. This information includes the name of the collection, the creation date, and the owner of the collection.  
  
## See Also  
 [XML Schema Collections &#40;SQL Server&#41;](xml-schema-collections-sql-server.md)  
  
  
