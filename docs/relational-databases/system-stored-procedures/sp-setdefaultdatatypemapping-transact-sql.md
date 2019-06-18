---
title: "sp_setdefaultdatatypemapping (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_setdefaultdatatypemapping"
  - "sp_setdefaultdatatypemapping_TSQL"
helpviewer_keywords: 
  - "sp_setdefaultdatatypemapping"
ms.assetid: 7394e8ca-4ce1-4e99-a784-205007c2c248
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_setdefaultdatatypemapping (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Marks an existing data type mapping between [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database management system (DBMS) as the default. This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_setdefaultdatatypemapping [ [ @mapping_id = ] mapping_id ]  
    [ , [ @source_dbms = ] 'source_dbms' ]  
    [ , [ @source_version = ] 'source_version' ]  
    [ , [ @source_type = ] 'source_type' ]   
    [ , [ @source_length_min = ] source_length_min ]  
    [ , [ @source_length_max = ] source_length_max ]  
    [ , [ @source_precision_min = ] source_precision_min ]  
    [ , [ @source_precision_max = ] source_precision_max ]  
    [ , [ @source_scale_min = ] source_scale_min ]  
    [ , [ @source_scale_max = ] source_scale_max ]  
    [ , [ @source_nullable = ] source_nullable ]  
    [ , [ @destination_dbms = ] 'destination_dbms' ]  
    [ , [ @destination_version = ] 'destination_version' ]  
    [ , [ @destination_type = ] 'destination_type' ]  
    [ , [ @destination_length = ] destination_length ]  
    [ , [ @destination_precision = ] destination_precision ]  
    [ , [ @destination_scale = ] destination_scale ]  
    [ , [ @destination_nullable = ] source_nullable ]  
```  
  
## Arguments  
`[ @mapping_id = ] mapping_id`
 Identifies an existing data type mapping.  *mapping_id* is **int**, with default value of NULL. If you specify *mapping_id*, then the remaining parameters are not required.  
  
`[ @source_dbms = ] 'source_dbms'`
 Is the name of the DBMS from which the data types are mapped. *source_dbms* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The source is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The source is an Oracle database.|  
|NULL (default)||  
  
 You must specify this parameter if *mapping_id* is NULL.  
  
`[ @source_version = ] 'source_version'`
 Is the version number of the source DBMS. *source_version* is **varchar(10)**, with a default value of NULL.  
  
`[ @source_type = ] 'source_type'`
 Is the data type in the source DBMS. *source_type* is **sysname**. You must specify this parameter if *mapping_id* is NULL.  
  
`[ @source_length_min = ] source_length_min`
 Is the minimum length of the data type in the source DBMS. *source_length_min* is **bigint**, with a default value of NULL.  
  
`[ @source_length_max = ] source_length_max`
 Is the maximum length of the data type in the source DBMS. *source_length_max* is **bigint**, with a default value of NULL.  
  
`[ @source_precision_min = ] source_precision_min`
 Is the minimum precision of the data type in the source DBMS. *source_precision_min* is **bigint**, with a default value of NULL.  
  
`[ @source_precision_max = ] source_precision_max`
 Is the maximum precision of the data type in the source DBMS. *source_precision_max* is **bigint**, with a default value of NULL.  
  
`[ @source_scale_min = ] source_scale_min`
 Is the minimum scale of the data type in the source DBMS. *source_scale_min* is **int**, with a default value of NULL.  
  
`[ @source_scale_max = ] source_scale_max`
 Is the maximum scale of the data type in the source DBMS. *source_scale_max* is **int**, with a default value of NULL.  
  
`[ @source_nullable = ] source_nullable`
 Is if the data type in the source DBMS supports a value of NULL. *source_nullable* is **bit**, with a default value of NULL. **1** means that NULL values are supported.  
  
`[ @destination_dbms = ] 'destination_dbms'`
 Is the name of the destination DBMS. *destination_dbms* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The destination is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The destination is an Oracle database.|  
|**DB2**|The destination is an IBM DB2 database.|  
|**SYBASE**|The destination is a Sybase database.|  
|NULL (default)||  
  
`[ @destination_version = ] 'destination_version'`
 Is the product version of the destination DBMS. *destination_version* is **varchar(10)**, with a default value of NULL.  
  
`[ @destination_type = ] 'destination_type'`
 Is the data type listed in the destination DBMS. *destination_type* is **sysname**, with a default value of NULL.  
  
`[ @destination_length = ] destination_length`
 Is the length of the data type in the destination DBMS. *destination_length* is **bigint**, with a default value of NULL.  
  
`[ @destination_precision = ] destination_precision`
 Is the precision of the data type in the destination DBMS. *destination_precision* is **bigint**, with a default value of NULL.  
  
`[ @destination_scale = ] destination_scale`
 Is the scale of the data type in the destination DBMS. *destination_scale* is **int**, with a default value of NULL.  
  
`[ @destination_nullable = ] destination_nullable`
 Is if the data type in the destination DBMS supports a value of NULL. *destination_nullable* is **bit**, with a default value of NULL. **1** means that NULL values are supported.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_setdefaultdatatypemapping** is used in all types of replication between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DBMS.  
  
 The default data type mappings apply to all replication topologies that include the specified DBMS.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_setdefaultdatatypemapping**.  
  
## See Also  
 [Specify Data Type Mappings for an Oracle Publisher](../../relational-databases/replication/publish/specify-data-type-mappings-for-an-oracle-publisher.md)   
 [sp_getdefaultdatatypemapping &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getdefaultdatatypemapping-transact-sql.md)   
 [sp_helpdatatypemap &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdatatypemap-transact-sql.md)  
  
  
