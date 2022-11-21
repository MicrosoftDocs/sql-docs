---
description: "sp_prepexec (Transact-SQL)"
title: "sp_prepexec (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_cursor_prepexec"
  - "sp_cursor_prepexec_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_prepexec"
ms.assetid: f9141850-a62b-43bf-8e46-b2f92b75ca56
author: markingmyname
ms.author: maghan
---
# sp_prepexec (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Prepares and executes a parameterized [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. sp_prepexec combines the functions of sp_prepare and sp_execute. This action is invoked by ID =13 in a tabular data stream (TDS) packet.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_prepexec handle OUTPUT, params , stmt  
    [ , bound param ] [ ,...n]]  
```  
  
## Arguments  
 *handle*  
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-generated *handle* identifier. *handle* is a required parameter with an **int** return value.  
  
 *params*  
 Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value. Input a NULL value if the statement is not parameterized.  
  
 *stmt*  
 Defines the cursor result set. The *stmt* parameter is required and calls for an **ntext**, **nchar**, or **nvarchar** input value.  
  
 *bound_param*  
 Signifies the optional use of additional parameters. *bound_param* calls for an input value of any data type to designate the additional parameters in use.  
  
## Examples  
 The following example prepares and executes a simple statement:  
  
```  
Declare @Out int;  
EXEC sp_prepexec @Out output,   
    N'@P1 nvarchar(128), @P2 nvarchar(100)',  
    N'SELECT database_id, name  
      FROM sys.databases  
      WHERE name=@P1 AND state_desc = @P2',   
          @P1 = 'tempdb', @P2 = 'ONLINE';   
EXEC sp_unprepare @Out;  
```  
  
## See also  
 [sp_prepare &#40;Transact SQL&#41;](../../relational-databases/system-stored-procedures/sp-prepare-transact-sql.md)   
 [sp_execute &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
