---
description: "sp_cursorexecute (Transact-SQL)"
title: "sp_cursorexecute (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cursorexecute"
  - "sp_cursorexecute_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cursor_execute"
ms.assetid: 6a204229-0a53-4617-a57e-93d4afbb71ac
author: markingmyname
ms.author: maghan
---
# sp_cursorexecute (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Creates and populates a cursor based upon the execution plan created by sp_cursorprepare. This procedure, coupled with sp_cursorprepare, has the same function as sp_cursoropen, but is split into two phases. sp_cursorexecute is invoked by specifying ID =4 in a tabular data stream (TDS) packet.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_cursorexecute prepared_handle, cursor  
    [ , scrollopt[ OUTPUT ]  
    [ , ccopt[ OUTPUT ]  
    [ ,rowcount OUTPUT [ ,bound param][,...n]]]]]  
```  
  
## Arguments  
 *prepared_handle*  
 Is the prepared statement *handle* value returned by sp_cursorprepare. *prepared_handle* is a required parameter that calls for an **int** input value.  
  
 *cursor*  
 Is the SQL Server-generated cursor identifier. *cursor* is a required parameter that must be supplied on all subsequent procedures which act upon the cursor, such as sp_cursorfetch  
  
 *scrollopt*  
 Scroll option. *scrollopt* is an optional parameter that requires an **int** input value. The sp_cursorexecute*scrollopt* parameter has the same value options as those for sp_cursoropen.  
  
> [!NOTE]  
>  The PARAMETERIZED_STMT value is not supported.  
  
> [!IMPORTANT]  
>  If a *scrollopt* value is not specified, the default value is KEYSET regardless of *scrollopt* value specified in sp_cursorprepare.  
  
 *ccopt*  
 Currency control option. *ccopt* is an optional parameter that requires an **int** input value. The sp_cursorexecute*ccopt* parameter has the same value options as those for sp_cursoropen.  
  
> [!IMPORTANT]  
>  If a *ccopt* value is not specified, the default value is OPTIMISTIC regardless of *ccopt* value specified in sp_cursorprepare.  
  
 *rowcount*  
 Is an optional parameter that signifies the number of fetch buffer rows to use with AUTO_FETCH. The default is 20 rows. *rowcount* behaves differently when assigned as an input value versus a return value.  
  
|As input value|As return value|  
|--------------------|---------------------|  
|When AUTO_FETCH is specified with FAST_FORWARD cursors *rowcount* represents the number of rows to place into the fetch buffer.|Represents the number of rows in the result set. When the *scrollopt* AUTO_FETCH value is specified, *rowcount* returns the number of rows that were fetched into the fetch buffer.|  
  
 *bound_param*  
 Signifies the optional use of additional parameters.  
  
> [!NOTE]  
>  Any parameters after the fifth are passed along to the statement plan as input parameters.  
  
## Code Return Value  
 *rowcount* may return the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|-1|Number of rows unknown.|  
|-n|An asynchronous population is in effect.|  
  
## Remarks  
  
## scrollopt and ccopt Parameters  
 *scrollopt* and *ccopt* are useful when the cached plans are preempted for the server cache, meaning that the prepared handle identifying the statement must be recompiled. The *scrollopt* and *ccopt* parameter values must match the values sent in the original request to sp_cursorprepare.  
  
> [!NOTE]  
>  PARAMETERIZED_STMT should not be assigned to *scrollopt*.  
  
 Failure to provide matching values will result in recompilation of the plans, negating the prepare and execute operations.  
  
## RPC and TDS considerations  
 The RPC RETURN_METADATA input flag can be set to 1 to request that cursor select list metadata be returned in the TDS stream.  
  
## See Also  
 [sp_cursoropen &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursoropen-transact-sql.md)   
 [sp_cursorfetch &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorfetch-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
