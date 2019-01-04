---
title: "sp_getdefaultdatatypemapping (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_getdefaultdatatypemapping_TSQL"
  - "sp_getdefaultdatatypemapping"
helpviewer_keywords: 
  - "sp_getdefaultdatatypemapping"
ms.assetid: b8401de1-f135-41d0-ba79-ce8fe1f48c00
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_getdefaultdatatypemapping (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on the default mapping for the specified data type between [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database management system (DBMS). This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_getdefaultdatatypemapping [ @source_dbms = ] 'source_dbms'   
    [ , [ @source_version = ] 'source_version' ]  
        , [ @source_type = ] 'source_type'    
    [ , [ @source_length = ] source_length ]  
    [ , [ @source_precision = ] source_precision ]  
    [ , [ @source_scale = ] source_scale ]  
    [ , [ @source_nullable = ] source_nullable ]  
        , [ @destination_dbms = ] 'destination_dbms'   
    [ , [ @destination_version = ] 'destination_version' ]  
    [ , [ @destination_type = ] 'destination_type' OUTPUT ]  
    [ , [ @destination_length = ] destination_length OUTPUT ]  
    [ , [ @destination_precision = ] destination_precision OUTPUT ]  
    [ , [ @destination_scale = ] destination_scale OUTPUT ]  
    [ , [ @destination_nullable = ] source_nullable OUTPUT ]  
    [ , [ @dataloss = ] dataloss OUTPUT ]  
```  
  
## Arguments  
 [ **@source_dbms**= ] **'**_source_dbms_**'**  
 Is the name of the DBMS from which the data types are mapped. *source_dbms* is **sysname**, and can be one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The source is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The source is an Oracle database.|  
  
 You must specify this parameter.  
  
 [ **@source_version=** ] **'**_source_version_**'**  
 Is the version number of the source DBMS. *source_version* is **varchar(10)**, with a default value of NULL.  
  
 [ **@source_type**= ] **'**_source_type_**'**  
 Is the data type in the source DBMS. *source_type* is **sysname**, with no default.  
  
 [ **@source_length=** ] *source_length*  
 Is the length of the data type in the source DBMS. *source_length* is **bigint**, with a default value of NULL.  
  
 [ **@source_precision=** ] *source_precision*  
 Is the precision of the data type in the source DBMS. *source_precision* is **bigint**, with a default value of NULL.  
  
 [ **@source_scale=** ] *source_scale*  
 Is the scale of the data type in the source DBMS. *source_scale* is **int**, with a default value of NULL.  
  
 [ **@source_nullable=** ] *source_nullable*  
 Is if the data type in the source DBMS supports a value of NULL. *source_nullable* is **bit**, with a default value of **1**, which means that NULL values are supported.  
  
 [ **@destination_dbms** = ] **'**_destination_dbms_**'**  
 Is the name of the destination DBMS. *destination_dbms* is **sysname**, and can be one of the following values:  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The destination is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The destination is an Oracle database.|  
|**DB2**|The destination is an IBM DB2 database.|  
|**SYBASE**|The destination is a Sybase database.|  
  
 You must specify this parameter.  
  
 [ **@destination_version**= ] **'**_destination_version_**'**  
 Is the product version of the destination DBMS. *destination_version* is **varchar(10)**, with a default value of NULL.  
  
 [ **@destination_type**= ] **'**_destination_type_**'** OUTPUT  
 Is the data type listed in the destination DBMS. *destination_type* is **sysname**, with a default value of NULL.  
  
 [ **@destination_length=** ] *destination_length* OUTPUT  
 Is the length of the data type in the destination DBMS. *destination_length* is **bigint**, with a default value of NULL.  
  
 [ **@destination_precision=** ] *destination_precision* OUTPUT  
 Is the precision of the data type in the destination DBMS. *destination_precision* is **bigint**, with a default value of NULL.  
  
 [ **@destination_scale=** ] _destination_scale_**OUTPUT**  
 Is the scale of the data type in the destination DBMS. *destination_scale* is **int**, with a default value of NULL.  
  
 [ **@destination_nullable=** ] _destination_nullable_**OUTPUT**  
 Is if the data type in the destination DBMS supports a value of NULL. *destination_nullable* is **bit**, with a default value of NULL. **1** means that NULL values are supported.  
  
 [ **@dataloss=** ] _dataloss_**OUTPUT**  
 Is if the mapping has the potential for data loss. *dataloss* is **bit**, with a default value of NULL. **1** means that there is a potential for data loss.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_getdefaultdatatypemapping** is used in all types of replication between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] DBMS.  
  
 **sp_getdefaultdatatypemapping** returns the default destination data type that is the closest match to the specified source data type.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role can execute **sp_getdefaultdatatypemapping**.  
  
## See Also  
 [sp_helpdatatypemap &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdatatypemap-transact-sql.md)   
 [sp_setdefaultdatatypemapping &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setdefaultdatatypemapping-transact-sql.md)   
 [Data Type Mapping for Oracle Publishers](../../relational-databases/replication/non-sql/data-type-mapping-for-oracle-publishers.md)   
 [IBM DB2 Subscribers](../../relational-databases/replication/non-sql/ibm-db2-subscribers.md)   
 [Oracle Subscribers](../../relational-databases/replication/non-sql/oracle-subscribers.md)  
  
  
