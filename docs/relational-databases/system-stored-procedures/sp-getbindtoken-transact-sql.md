---
description: "sp_getbindtoken (Transact-SQL)"
title: "sp_getbindtoken (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_getbindtoken"
  - "sp_getbindtoken_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_getbindtoken"
ms.assetid: 5db87d77-85fa-45a3-a23a-3ea500f9a5ac
author: markingmyname
ms.author: maghan
---
# sp_getbindtoken (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a unique identifier for the transaction. This unique identifier is a string used to bind sessions using sp_bindsession.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use Multiple Active Results Sets (MARS) or distributed transactions instead. For more information, see [Using Multiple Active Result Sets &#40;MARS&#41;](../../relational-databases/native-client/features/using-multiple-active-result-sets-mars.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_getbindtoken [@out_token =] 'return_value' OUTPUT   
```  
  
## Arguments  
 [@out_token= ]'*return_value*'  
 Is the token to use to bind sessions. *return_value* is **varchar(255)** with no default.  
  
## Return Code Values  
 None  
  
## Result Sets  
 None  
  
## Remarks  
 sp_getbindtoken will return a valid token only when the stored procedure is executed inside an active transaction. Otherwise, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will return an error message. For example:  
  
```  
-- Declare a variable to hold the bind token.  
-- No active transaction.  
DECLARE @bind_token varchar(255);  
-- Trying to get the bind token returns an error 3921.  
EXECUTE sp_getbindtoken @bind_token OUTPUT;  
Server: Msg 3921, Level 16, State 1, Procedure sp_getbindtoken, Line 4  
Cannot get a transaction token if there is no transaction active.  
Reissue the statement after a transaction has been started.  
```  
  
 When sp_getbindtoken is used to enlist a distributed transaction connection inside an open transaction, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns the same token. For example:  
  
```  
USE AdventureWorks2012;  
GO  
DECLARE @bind_token varchar(255);  
  
BEGIN TRAN;  
  
EXECUTE sp_getbindtoken @bind_token OUTPUT;  
SELECT @bind_token AS Token;  
  
BEGIN DISTRIBUTED TRAN;  
  
EXECUTE sp_getbindtoken @bind_token OUTPUT;  
SELECT @bind_token AS Token;  
```  
  
 Both `SELECT` statements return the same token:  
  
```  
Token  
-----  
PKb'gN5<9aGEedk_16>8U=5---/5G=--  
(1 row(s_) affected)  
  
Token  
-----  
PKb'gN5<9aGEedk_16>8U=5---/5G=--  
(1 row(s_) affected)  
```  
  
 The bind token can be used with sp_bindsession to bind new sessions to the same transaction. The bind token is only valid locally inside each instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and cannot be shared across multiple instances.  
  
 To obtain and pass a bind token, you must run sp_getbindtoken before executing sp_bindsession for sharing the same lock space. If you obtain a bind token, sp_bindsession runs correctly.  
  
> [!NOTE]  
>  We recommend that you use the srv_getbindtoken Open Data Services application programming interface (API) to obtain a bind token to be used from an extended stored procedure.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example obtains a bind token and displays the bind token name.  
  
```  
DECLARE @bind_token varchar(255);  
BEGIN TRAN;  
EXECUTE sp_getbindtoken @bind_token OUTPUT;  
SELECT @bind_token AS Token;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 `Token`  
  
 `----------------------------------------------------------`  
  
 `\0]---5^PJK51bP<1F<-7U-]ANZ`  
  
## See Also  
 [sp_bindsession &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-bindsession-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [srv_getbindtoken &#40;Extended Stored Procedure API&#41;](../../relational-databases/extended-stored-procedures-reference/srv-getbindtoken-extended-stored-procedure-api.md)  
  
  
