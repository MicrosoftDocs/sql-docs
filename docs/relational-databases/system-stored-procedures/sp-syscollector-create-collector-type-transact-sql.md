---
title: "sp_syscollector_create_collector_type (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_create_collector_type"
  - "sp_syscollector_create_collector_type_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_syscollector_create_collector_type"
  - "data collector [SQL Server], stored procedures"
ms.assetid: 568e9119-b9b0-4284-9cef-3878c691de5f
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_create_collector_type (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a collector type for the data collector. A collector type is a logical wrapper around the [!INCLUDE[ssIS](../../includes/ssis-md.md)] packages that provide the actual mechanism for collecting data and uploading it to the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_create_collector_type   
    [ [@collector_type_uid = ] 'collector_type_uid' OUTPUT ]  
    , [ @name = ] 'name'  
    , [ [ @parameter_schema = ] 'parameter_schema' ]  
    , [ [ @parameter_formatter = ] 'parameter_formatter' ]  
    , [ @collection_package_id = ] 'collection_package_id'  
    , [ @upload_package_id = ] 'upload_package_id'  
```  
  
## Arguments  
 [ @collector_type_uid = ] '*collector_type_uid*'  
 Is the GUID for the collector type. *collector_type_uid* is **uniqueidentifier** and if it is NULL it will be automatically created and returned as OUTPUT.  
  
 [ @name = ] '*name*'  
 Is the name of the collector type. *name* is **sysname** and must be specified.  
  
 [ @parameter_schema = ] '*parameter_schema*'  
 Is the XML schema for this collector type. *parameter_schema* is **xml** with a default of NULL.  
  
 [ @parameter_formatter = ] '*parameter_formatter*'  
 Is the template to use to transform the XML for use in the collection set property page. *parameter_formatter* is **xml** with a default of NULL.  
  
 [@collection_package_id = ] *collection_package_id*  
 Is a local unique identifier that points to the [!INCLUDE[ssIS](../../includes/ssis-md.md)] collection package used by the collection set. *collection_package_id* is **uniqueidentifer** and is required.  
  
 [@upload_package_id = ] *upload_package_id*  
 Is a local unique identifier that points to the [!INCLUDE[ssIS](../../includes/ssis-md.md)] upload package used by the collection set. *upload_package_id* is **uniqueidentifier** and is required.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Example  
 This creates the Generic T-SQL Query collector type.  
  
```  
EXEC sp_syscollector_create_collector_type  
@collector_type_uid = '302E93D1-3424-4be7-AA8E-84813ECF2419',  
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
  
  
