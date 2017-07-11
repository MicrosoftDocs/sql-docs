---
title: "sp_cursorunprepare (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cursorunprepare_TSQL"
  - "sp_cursorunprepare"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cursorunprepare"
ms.assetid: b46d4813-c4a9-4f9d-9979-2b5082ecf06a
caps.latest.revision: 8
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sp_cursorunprepare (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Discards the execution plan developed in the sp_cursorprepare stored procedure. sp_cursorunprepare is invoked by specifying ID = 6 in a tabular data stream (TDS) packet.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_cursorunprepare handle  
```  
  
## Arguments  
 *handle*  
 Is the *handle* value that is returned by sp_cursorprepare when the statement is prepared.  
  
## See Also  
 [sp_cursorprepare &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorprepare-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  