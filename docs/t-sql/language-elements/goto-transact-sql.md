---
title: "GOTO (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "GOTO"
  - "GOTO_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "skipping statements"
  - "Transact-SQL statements, skipping"
  - "labels [SQL Server]"
  - "statements [SQL Server], skipping"
  - "GOTO statement"
ms.assetid: 589b6f8e-dc80-416f-9e74-48bed5337f58
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# GOTO (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Alters the flow of execution to a label. The [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or statements that follow GOTO are skipped and processing continues at the label. GOTO statements and labels can be used anywhere within a procedure, batch, or statement block. GOTO statements can be nested.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
Define the label:   
label:   
Alter the execution:  
GOTO label   
```  
  
## Arguments  
 *label*  
 Is the point after which processing starts if a GOTO is targeted to that label. Labels must follow the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). A label can be used as a commenting method whether GOTO is used.  
  
## Remarks  
 GOTO can exist within conditional control-of-flow statements, statement blocks, or procedures, but it cannot go to a label outside the batch. GOTO branching can go to a label defined before or after GOTO.  
  
## Permissions  
 GOTO permissions default to any valid user.  
  
## Examples  
 The following example shows how to use `GOTO` as a branch mechanism.  
  
```  
DECLARE @Counter int;  
SET @Counter = 1;  
WHILE @Counter < 10  
BEGIN   
    SELECT @Counter  
    SET @Counter = @Counter + 1  
    IF @Counter = 4 GOTO Branch_One --Jumps to the first branch.  
    IF @Counter = 5 GOTO Branch_Two  --This will never execute.  
END  
Branch_One:  
    SELECT 'Jumping To Branch One.'  
    GOTO Branch_Three; --This will prevent Branch_Two from executing.  
Branch_Two:  
    SELECT 'Jumping To Branch Two.'  
Branch_Three:  
    SELECT 'Jumping To Branch Three.';  
```  
  
## See Also  
 [Control-of-Flow Language &#40;Transact-SQL&#41;](~/t-sql/language-elements/control-of-flow.md)   
 [BEGIN...END &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-end-transact-sql.md)   
 [BREAK &#40;Transact-SQL&#41;](../../t-sql/language-elements/break-transact-sql.md)   
 [CONTINUE &#40;Transact-SQL&#41;](../../t-sql/language-elements/continue-transact-sql.md)   
 [IF...ELSE &#40;Transact-SQL&#41;](../../t-sql/language-elements/if-else-transact-sql.md)   
 [WAITFOR &#40;Transact-SQL&#41;](../../t-sql/language-elements/waitfor-transact-sql.md)   
 [WHILE &#40;Transact-SQL&#41;](../../t-sql/language-elements/while-transact-sql.md)  
  
  
