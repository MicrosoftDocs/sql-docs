---
title: "sp_helpdatatypemap (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_helpdatatypemap"
  - "sp_helpdatatypemap_TSQL"
helpviewer_keywords: 
  - "sp_helpdatatypemap"
ms.assetid: 800c9c65-723e-4961-a63d-327987f129f0
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_helpdatatypemap (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information on the defined data type mappings between [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database management systems (DBMS). This stored procedure is executed at the Distributor on any database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdatatypemap [ @source_dbms = ] 'source_dbms'   
    [ , [ @source_version = ] 'source_version' ]  
    [ , [ @source_type = ] 'source_type' ]   
    [ , [ @destination_dbms = ] 'destination_dbms' ]  
    [ , [ @destination_version = ] 'destination_version' ]  
    [ , [ @destination_type = ] 'destination_type' ]  
    [ , [ @defaults_only = ] defaults_only ]  
```  
  
## Arguments  
 [ **@source_dbms**= ] **'***source_dbms***'**  
 Is the name of the DBMS from which the data types are mapped. *source_dbms* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The source is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The source is an Oracle database.|  
  
 [ **@source_version**= ] **'***source_version***'**  
 Is the product version of the source DBMS. *source_version*is **varchar(10)**, and if not specified, the data type mappings for all versions of the source DBMS are returned. Enables the result set to be filtered by the source version of the DBMS.  
  
 [ **@source_type**= ] **'***source_type***'**  
 Is the data type listed in the source DBMS. *source_type* is **sysname**, and if not specified, mappings for all data types in the source DBMS are returned. Enables the result set to be filtered by data type in the source DBMS.  
  
 [ **@destination_dbms** = ] **'***destination_dbms***'**  
 Is the name of the destination DBMS. *destination_dbms* is **sysname**, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**MSSQLSERVER**|The destination is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|**ORACLE**|The destination is an Oracle database.|  
|**DB2**|The destination is an IBM DB2 database.|  
|**SYBASE**|The destination is a Sybase database.|  
  
 [ **@destination_version**= ] **'***destination_version***'**  
 Is the product version of the destination DBMS. *destination_version*is **varchar(10)**, and if not specified, mappings for all versions of the destination DBMS are returned. Enables the result set to be filtered by the destination version of the DBMS.  
  
 [ **@destination_type**= ] **'***destination_type***'**  
 Is the data type listed in the destination DBMS. *destination_type*is **sysname**, and if not specified, mappings for all data types in the destination DBMS are returned. Enables the result set to be filtered by data type in the destination DBMS.  
  
 [ **@defaults_only**= ] *defaults_only*  
 Is if only the default data type mappings are returned. *defaults_only* is **bit**, with a default of **0**. **1** means that only the default data type mappings are returned. **0** means that the default and any user-defined data type mappings are returned.  
  
## Result Sets  
  
|Column name|Description|  
|-----------------|-----------------|  
|**mapping_id**|Identifies a data type mapping.|  
|**source_dbms**|Is the name and version number of the source DBMS.|  
|**source_type**|Is the data type in the source DBMS.|  
|**destination_dbms**|Is the name of the destination DBMS.|  
|**destination_type**|Is the data type in the destination DBMS.|  
|**is_default**|Is if the mapping is a default or an alternative mapping. A value of **0** indicates that this mapping is user-defined.|  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_helpdatatypemap** defines data type mappings both from non-SQL Server Publishers and from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers to non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.  
  
 When the specified combination of source and destination DBMS is not supported, **sp_helpdatatypemap** returns an empty result set.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role at the Distributor or members of the **db_owner** fixed database role on the distribution database can execute **sp_helpdatatypemap**.  
  
## See Also  
 [sp_getdefaultdatatypemapping &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-getdefaultdatatypemapping-transact-sql.md)   
 [sp_setdefaultdatatypemapping &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-setdefaultdatatypemapping-transact-sql.md)  
  
  
