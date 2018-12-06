---
title: "sys.sp_cdc_generate_wrapper_function (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cdc_generate_wrapper_function_TSQL"
  - "sp_cdc_generate_wrapper_function"
  - "sys.sp_cdc_generate_wrapper_function_TSQL"
  - "sys.sp_cdc_generate_wrapper_function"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_cdc_generate_wrapper_function"
  - "sp_cdc_generate_wrapper_function"
ms.assetid: 85bc086d-8a4e-4949-a23b-bf53044b925c
author: rothja
ms.author: jroth
manager: craigg
---
# sys.sp_cdc_generate_wrapper_function (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Generates scripts to create wrapper functions for the change data capture query functions that are available in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The API that is supported in the generated wrappers enables the query interval to be specified as a datetime interval. This makes the function good for use in many warehousing applications, including those that are developed by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package designers who are using change data capture technology to determine incremental load.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_cdc_generate_wrapper_function  
    [ [ @capture_instance sysname = ] 'capture_instance'  
    [ , [ @closed_high_end_point = ] closed_high_end_pt  
    [ , [ @column_list = ] 'column_list'  
```  
  
```  
  
[ , [ @update_flag_list = ] 'update_flag_list'  
```  
  
## Arguments  
 [ @capture_instance= ] '*capture_instance*'  
 Is the capture instance that scripts are to be generated for. *capture_instance* is **sysname** and has a default value of NULL. If a value is omitted or explicitly set to NULL, wrapper scripts are generated for all capture instances  
  
 [ @closed_high_end_point= ] *high_end_pt_flag*  
 Is the flag bit that indicates whether changes that have a commit time equal to the high endpoint are to be included within the extraction interval by the generated procedure. *high_end_pt_flag* is **bit** and has a default value of 1, which indicates that the endpoint should be included. A value of 0 indicates that all commit times will be strictly less than the high endpoint.  
  
 [ @column_list= ] '*column_list*'  
 Is a list of captured columns to be included in the result set that is returned by the wrapper function. *column_list* is **nvarchar(max)** and has a default value of NULL. When NULL is specified, all captured columns are included.  
  
 [ @update_flag_list= ] '*update_flag_list*'  
 Is a list of included columns for which an update flag is included in the result set that is returned by the wrapper function. *update_flag_list* is **nvarchar(max)** and has a default value of NULL. When NULL is specified, no update flags are included.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Column type|Description|  
|-----------------|-----------------|-----------------|  
|**function_name**|**nvarchar(145)**|Name of the generated function.|  
|**create_script**|**nvarchar(max)**|Is the script that creates the capture-instance wrapper function.|  
  
## Remarks  
 The script that creates the function to wrap the all-changes query for a capture instance is always generated. If the capture instance supports net-changes queries, the script to generate a wrapper for this query is also generatedl.  
  
## Examples  
 The following example show how you can use `sys.sp_cdc_generate_wrapper_function` to create wrappers for all the change data capture functions.  
  
```  
DECLARE @wrapper_functions TABLE (  
    function_name sysname,  
    create_script nvarchar(max));  
  
INSERT INTO @wrapper_functions  
EXEC sys.sp_cdc_generate_wrapper_function;  
  
DECLARE @create_script nvarchar(max);  
DECLARE #hfunctions CURSOR LOCAL fast_forward  
FOR   
    SELECT create_script FROM @wrapper_functions;  
  
OPEN #hfunctions;  
FETCH #hfunctions INTO @create_script;  
WHILE (@@fetch_status <> -1)  
BEGIN  
    EXEC sp_executesql @create_script  
    FETCH #hfunctions INTO @create_script  
END;  
  
CLOSE #hfunctions;  
DEALLOCATE #hfunctions;  
```  
  
## See Also  
 [Change Data Capture Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/change-data-capture-stored-procedures-transact-sql.md)   
 [Change Data Capture &#40;SSIS&#41;](../../integration-services/change-data-capture/change-data-capture-ssis.md)  
  
  
