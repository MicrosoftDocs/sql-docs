---
title: "sp_syscollector_update_collector_type (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_update_collector_type_TSQL"
  - "sp_syscollector_update_collector_type"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_update_collector_type"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 3c414dfd-d9ca-4320-81aa-949465b967bf
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_update_collector_type (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Updates a collector type for a collection item. Given the name and GUID of a collector type, updates the collector type configuration, including the collection and upload package, the parameter schema, and the parameter formatter schema.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_update_collector_type [ @collector_type_uid = ] 'collector_type_uid' OUTPUT  
          , [ @name = ] 'name'  
          , [ @parameter_schema = ] 'parameter_schema'  
          , [ @collection_package_id = ] collection_package_id  
          , [ @upload_package_id = ] upload_package_id  
```  
  
## Arguments  
 [ **@collector_type_uid =** ] **'***collector_type_uid***'**  
 Is the GUID for the collector type. *collector_type_uid* is **uniqueidentifier**, and if it is NULL it will be automatically created and returned as OUTPUT.  
  
 [ **@name =** ] **'***name***'**  
 Is the name of the collector type. *name* is **sysname** and must be specified.  
  
 [ **@parameter_schema =** ] **'***parameter_schema***'**  
 Is the XML schema for this collector type. *parameter_schema* is **xml** and may be required by certain collector types. If it is not required, this argument can be NULL.  
  
 [ **@collection_package_id =** ] *collection_package_id*  
 Is a local unique identifier that points to the [!INCLUDE[ssIS](../../includes/ssis-md.md)] collection package used by the collection set. *collection_package_id* is **uniqueidentifer** and is required. To obtain the value for *collection_package_id*, query the dbo.syscollector_collector_types system view in the msdb database.  
  
 [ **@upload_package_id =** ] *upload_package_id*  
 Is a local unique identifier that points to the [!INCLUDE[ssIS](../../includes/ssis-md.md)] upload package used by the collection set. *upload_package_id* is **uniqueidentifier** and is required. To obtain the value for *upload_package_id*, query the dbo.syscollector_collector_types system view in the msdb database.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Permissions  
 Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role.  
  
## Example  
 This example updates the Generic T-SQL Query collector type. (In the example, the default schema for the Generic T-SQL Query collector type is used.)  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_update_collector_type  
@collector_type_uid = '302E93D1-3424-4BE7-AA8E-84813ECF2419',  
@name = 'Generic T-SQL Query Collector Type',  
@parameter_schema = '<?xml version="1.0" encoding="utf-8"?>  
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" targetNamespace="DataCollectorType">  
  <xs:element name="TSQLQueryCollector">  
<xs:complexType>  
  <xs:sequence>  
<xs:element name="Query" minOccurs="1" maxOccurs="unbounded">  
  <xs:complexType>  
<xs:sequence>  
  <xs:element name="Value" type="xs:string" />  
  <xs:element name="OutputTable" type="xs:string" />  
</xs:sequence>  
  </xs:complexType>  
</xs:element>  
<xs:element name="Databases" minOccurs="0" maxOccurs="1">  
  <xs:complexType>  
<xs:sequence>  
  <xs:element name="Database" minOccurs="0" maxOccurs="unbounded" type="xs:string" />  
</xs:sequence>  
<xs:attribute name="UseSystemDatabases" type="xs:boolean" use="optional" />  
<xs:attribute name="UseUserDatabases" type="xs:boolean" use="optional" />  
  </xs:complexType>  
</xs:element>  
  </xs:sequence>  
</xs:complexType>  
  </xs:element>  
</xs:schema>',  
@collection_package_id = '292B1476-0F46-4490-A9B7-6DB724DE3C0B',  
@upload_package_id = '6EB73801-39CF-489C-B682-497350C939F0';  
GO  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
