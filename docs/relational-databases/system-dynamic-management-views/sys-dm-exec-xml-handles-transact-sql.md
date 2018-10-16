---
title: "sys.dm_exec_xml_handles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_exec_xml_handles"
  - "dm_exec_xml_handles_TSQL"
  - "sys.dm_exec_xml_handles_TSQL"
  - "sys.dm_exec_xml_handles"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_xml_handles dynamic management function"
ms.assetid: a873ce0f-6955-417a-96a1-b2ef11a83633
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_xml_handles (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]

  Returns information about active handles that have been opened by **sp_xml_preparedocument**.  
  
## Syntax  
  
```  
  
dm_exec_xml_handles (session_id | 0 )  
```  
  
## Arguments  
 *session_id* | 0,  
 ID of the session. If *session_id* is specified, this function returns information about XML handles in the specified session.  
  
 If 0 is specified, the function returns information about all XML handles for all sessions.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**session_id**|**int**|Session ID of the session that holds this XML document handle.|  
|**document_id**|**int**|XML document handle ID returned by **sp_xml_preparedocument**.|  
|**namespace_document_id**|**int**|Internal handle ID used for the associated namespace document that has been passed as the third parameter to **sp_xml_preparedocument**. NULL if there is no namespace document.|  
|**sql_handle**|**varbinary(64)**|Handle to the text of the SQL code where the handle has been defined.|  
|**statement_start_offset**|**int**|Number of characters into the currently executing batch or stored procedure at which the **sp_xml_preparedocument** call occurs. Can be used together with the **sql_handle**, the **statement_end_offset**, and the **sys.dm_exec_sql_text** dynamic management function to retrieve the currently executing statement for the request.|  
|**statement_end_offset**|**int**|Number of characters into the currently executing batch or stored procedure at which the **sp_xml_preparedocument** call occurs. Can be used together with the **sql_handle**, the **statement_start_offset**, and the **sys.dm_exec_sql_text** dynamic management function to retrieve the currently executing statement for the request.|  
|**creation_time**|**datetime**|Timestamp when **sp_xml_preparedocument** was called.|  
|**original_document_size_bytes**|**bigint**|Size of the unparsed XML document in bytes.|  
|**original_namespace_document_size_bytes**|**bigint**|Size of the unparsed XML namespace document, in bytes. NULL if there is no namespace document.|  
|**num_openxml_calls**|**bigint**|Number of OPENXML calls with this document handle.|  
|**row_count**|**bigint**|Number of rows returned by all previous OPENXML calls for this document handle.|  
|**dormant_duration_ms**|**bigint**|Milliseconds since the last OPENXML call. If OPENXML has not been called, returns milliseconds since the **sp_xml_preparedocumen**t call.|  
  
## Remarks  
 The lifetime of **sql_handles** used to retrieve the SQL text that executed a call to **sp_xml_preparedocument** outlives the cached plan used to execute the query. If the query text is not available in the cache, the data cannot be retrieved using the information provided in the function result. This can occur if you are running many large batches.  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server to see all sessions or session IDs that are not owned by the caller. A caller can always see the data for his or her own current session ID.      
  
## Examples  
 The following example selects all active handles.  
  
```  
SELECT * FROM sys.dm_exec_xml_handles(0);  
```  
  
## See Also  
 <br>[Dynamic Management Views and Functions (Transact-SQL)](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
 <br>[Execution Related Dynamic Management Views and Functions (Transact-SQL)](../../relational-databases/system-dynamic-management-views/execution-related-dynamic-management-views-and-functions-transact-sql.md)
 <br>[sp_xml_preparedocument (Transact-SQL)](../system-stored-procedures/sp-xml-preparedocument-transact-sql.md)
 <br>[sp_xml_removedocument (Transact-SQL)](../system-stored-procedures/sp-xml-removedocument-transact-sql.md)


 
  
  
