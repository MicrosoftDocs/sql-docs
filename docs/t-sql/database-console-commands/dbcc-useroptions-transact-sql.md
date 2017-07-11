---
title: "DBCC USEROPTIONS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC USEROPTIONS"
  - "DBCC_USEROPTIONS_TSQL"
  - "USEROPTIONS_TSQL"
  - "USEROPTIONS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DBCC USEROPTIONS statement"
  - "active SET options"
  - "SET statement, active SET options"
ms.assetid: 565ab112-7af1-4c18-a579-07cdb332f539
caps.latest.revision: 41
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# DBCC USEROPTIONS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the SET options active (set) for the current connection.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DBCC USEROPTIONS  
[ WITH NO_INFOMSGS ]  
```  
  
## Arguments  
 NO_INFOMSGS  
 Suppresses all informational messages that have severity levels from 0 through 10.  
  
## Result Sets  
 DBCC USEROPTIONS returns a column for the name of the SET option and a column for the value of the option (values and entries may vary):  
  
 `Set Option                   Value`  
  
 `---------------------------- ---------------------------`  
  
 `textsize                     64512`  
  
 `language                     us_english`  
  
 `dateformat                   mdy`  
  
 `datefirst                    7`  
  
 `lock_timeout                 -1`  
  
 `quoted_identifier            SET`  
  
 `arithabort                   SET`  
  
 `ansi_null_dflt_on            SET`  
  
 `ansi_warnings                SET`  
  
 `ansi_padding                 SET`  
  
 `ansi_nulls                   SET`  
  
 `concat_null_yields_null      SET`  
  
 `isolation level              read committed`  
  
 `(13 row(s) affected)`  
  
 `DBCC execution completed. If DBCC printed error messages, contact your system administrator.`  
  
 `Permissions`  
  
## Remarks  
 DBCC USEROPTIONS reports an isolation level of 'read committed snapshot' when the database option READ_COMMITTED_SNAPSHOT is set to ON and the transaction isolation level is set to 'read committed'. The actual isolation level is read committed.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example returns the active SET options for the current connection.  
  
```tsql  
DBCC USEROPTIONS;  
```  
  
## See Also  
 [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [SET TRANSACTION ISOLATION LEVEL &#40;Transact-SQL&#41;](../../t-sql/statements/set-transaction-isolation-level-transact-sql.md)  
  
  