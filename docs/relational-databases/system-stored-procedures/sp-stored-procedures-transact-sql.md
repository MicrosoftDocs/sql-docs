---
description: "sp_stored_procedures (Transact-SQL)"
title: "sp_stored_procedures (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_stored_procedures_TSQL"
  - "sp_stored_procedures"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_stored_procedures"
ms.assetid: fe52dd83-000a-4665-83fb-7a0024193dec
author: markingmyname
ms.author: maghan
---
# sp_stored_procedures (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a list of stored procedures in the current environment.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_stored_procedures [ [ @sp_name = ] 'name' ]   
    [ , [ @sp_owner = ] 'schema']   
    [ , [ @sp_qualifier = ] 'qualifier' ]  
    [ , [@fUsePattern = ] 'fUsePattern' ]  
```  
  
## Arguments  
`[ @sp_name = ] 'name'`
 Is the name of the procedure used to return catalog information. *name* is **nvarchar(390)**, with a default of NULL. Wildcard pattern matching is supported.  
  
`[ @sp_owner = ] 'schema'`
 Is the name of the schema to which the procedure belongs. *schema* is **nvarchar(384)**, with a default of NULL. Wildcard pattern matching is supported. If *owner* is not specified, the default procedure visibility rules of the underlying DBMS apply.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if the current schema contains a procedure with the specified name, that procedure is returned. If a nonqualified stored procedure is specified, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] searches for the procedure in the following order:  
  
-   The **sys** schema of the current database.  
  
-   The caller's default schema if executed in a batch or in dynamic SQL; or, if the non-qualified procedure name appears inside the body of another procedure definition, the schema containing this other procedure is searched next.  
  
-   The **dbo** schema in the current database.  
  
`[ @qualifier = ] 'qualifier'`
 Is the name of the procedure qualifier. *qualifier* is **sysname**, with a default of NULL. Various DBMS products support three-part naming for tables in the form (_qualifier_**.**_schema_**.**_name_. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], *qualifier* represents the database name. In some products, it represents the server name of the database environment of the table.  
  
`[ @fUsePattern = ] 'fUsePattern'`
 Determines whether the underscore (_), percent (%), or brackets [ ]) are interpreted as wildcard characters. *fUsePattern* is **bit**, with a default of 1.  
  
 **0** = Pattern matching is off.  
  
 **1** = Pattern matching is on.  
  
## Return Code Values  
 None  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**PROCEDURE_QUALIFIER**|**sysname**|Procedure qualifier name. This column can be NULL.|  
|**PROCEDURE_OWNER**|**sysname**|Procedure owner name. This column always returns a value.|  
|**PROCEDURE_NAME**|**nvarchar(134)**|Procedure name. This column always returns a value.|  
|**NUM_INPUT_PARAMS**|**int**|Reserved for future use.|  
|**NUM_OUTPUT_PARAMS**|**int**|Reserved for future use.|  
|**NUM_RESULT_SETS**|**int**|Reserved for future use.|  
|**REMARKS**|**varchar(254)**|Description of the procedure. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not return a value for this column.|  
|**PROCEDURE_TYPE**|**smallint**|Procedure type. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] always returns 2.0. This value can be one of the following:<br /><br /> 0 = SQL_PT_UNKNOWN<br /><br /> 1 = SQL_PT_PROCEDURE<br /><br /> 2 = SQL_PT_FUNCTION|  
  
## Remarks  
 For maximum interoperability, the gateway client should assume only SQL standard pattern matching (the percent (%) and underscore (_) wildcard characters).  
  
 The permission information about execute access to a specific stored procedure for the current user is not necessarily checked; therefore, access is not guaranteed. Note that only three-part naming is used. This means that only local stored procedures, not remote stored procedures (which require four-part naming), are returned when they are executed against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the server attribute ACCESSIBLE_SPROC is Y in the result set for **sp_server_info**, only stored procedures that can be executed by the current user are returned.  
  
 **sp_stored_procedures** is equivalent to **SQLProcedures** in ODBC. The results returned are ordered by **PROCEDURE_QUALIFIER**, **PROCEDURE_OWNER**, and **PROCEDURE_NAME**.  
  
## Permissions  
 Requires SELECT permission on the schema.  
  
## Examples  
  
### A. Returning all stored procedures in the current database  
 The following example returns all stored procedures in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_stored_procedures;  
```  
  
### B. Returning a single stored procedure  
 The following example returns a result set for the `uspLogError` stored procedure.  
  
```  
USE AdventureWorks2012;  
GO  
sp_stored_procedures N'uspLogError', N'dbo', N'AdventureWorks2012', 1;  
```  
  
## See Also  
 [Catalog Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/catalog-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
