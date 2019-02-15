---
title: "sp_get_redirected_publisher (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_get_redirected_publisher_TSQL"
  - "sp_get_redirected_publisher"
ms.assetid: d47a9ab5-f2cc-42a8-8be9-a33895ce44f0
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_get_redirected_publisher (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Used by replication agents to query a distributor to determine whether the original publisher has been redirected.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_get_redirected_publisher   
    [ @original_publisher = ] 'original_publisher',  
    [ @publisher_db = ] 'database_name',   
    [ @bypass_publisher_validation = ] [0 | 1 ]  
```  
  
## Arguments  
 [ **@original_publisher** = ] **'***original_publisher***'**  
 The name of the database being published. *publisher_db* is **sysname**, with no default.  
  
 [ **@publisher_db** = ] **'***publisher_db***'**  
 The name of the database being published. *publisher_db* is **sysname**, with no default.  
  
 [ **@bypass_publisher_validation** = ] [0 | 1 ]  
 Used to bypass validation of the redirected publisher. If 0, validation is performed . If 1, validation is not performed. *bypass_publisher_validation* is **bit**, with a default of 0.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**redirected_publisher**|**sysname**|The name of the publisher after redirection.|  
|**error_number**|**int**|The error number of the validation error.|  
|**error_severity**|**int**|The severity of the validation error.|  
|**error_message**|**nvarchar(4000)**|The text of the validation error message.|  
  
## Remarks  
 *redirected_publisher* returns the current publisher name. Returns null if the publisher and publishing databases have not been redirected using **sp_redirect_publisher**.  
  
 If validation is not requested or if no entry exists for the publisher and the publishing database, *error_number* and *error_severity* return 0 and *error_message* returns null.  
  
 If validation is requested, the validation stored procedure [sp_validate_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-redirected-publisher-transact-sql.md) is called to verify that the target of the redirection is a suitable host for the publishing database. If the validation succeeds, **sp_get_redirected_publisher** returns the redirected publisher name, 0 for the *error_number* and *error_severity* columns, and null in the *error_message* column.  
  
 If validation is requested and fails, the redirected publisher name is returned along with error information.  
  
## Permissions  
 Caller must either be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role for the distribution database, or a member of a publication access list for a defined publication associated with the publisher database.  
  
## See Also  
 [Replication Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)   
 [sp_validate_redirected_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-redirected-publisher-transact-sql.md)   
 [sp_redirect_publisher &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-redirect-publisher-transact-sql.md)   
 [sp_validate_replica_hosts_as_publishers &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-validate-replica-hosts-as-publishers-transact-sql.md)  
  
  
