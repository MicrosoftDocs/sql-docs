---
title: "sp_execute (Transact-SQL)"
description: "sp_execute (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cursor_execute"
  - "sp_cursor_execute_TSQL"
helpviewer_keywords:
  - "sp_execute"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_execute (Transact-SQL)
[!INCLUDE [sql-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdbmi-asa-pdw.md)]

  Executes a prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statement using a specified handle and optional parameter value. sp_execute is invoked by specifying ID =12 in a tabular data stream (TDS) packet.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server, Azure Synapse Analytics, Parallel Data Warehouse  
  
sp_execute handle OUTPUT  
    [,bound_param  ]  [,...n ]  ]  
```  
  
## Arguments  
 *handle*  
 Is the *handle* value returned by sp_prepare. *handle* is a required parameter that calls for **int** input value.  
  
 *bound_param*  
 Signifies the use of additional parameters. *bound_param* is a required parameter that calls for input values of any data type to signify additional parameters for the procedure.  
  
> [!NOTE]  
>  *bound_param* must match the declarations made by the sp_prepare*params* value and can be in the form *@name = value* or *value*.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [sp_prepare &#40;Transact SQL&#41;](../../relational-databases/system-stored-procedures/sp-prepare-transact-sql.md)  
  
  
