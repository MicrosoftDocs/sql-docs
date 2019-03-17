---
title: "sp_cursorprepexec (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_cursorprepexec_TSQL"
  - "sp_cursorprepexec"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_cursorprepexec"
ms.assetid: 8094fa90-35b5-4cf4-8012-0570cb2ba1e6
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_cursorprepexec (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Compiles a plan for the submitted cursor statement or batch, then creates and populates the cursor. sp_cursorprepexec combines the functions of sp_cursorprepare and sp_cursorexecute. This procedures is invoked by specifying ID = 5 in a tabular data stream (TDS) packet.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_cursorprepexec prepared handle OUTPUT, cursor OUTPUT, params , statement , options  
    [ , scrollopt [ , ccopt [ , rowcount ] ] ]  
```  
  
## Arguments  
 *prepared handle*  
 Is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generated prepared *handle* identifier. *prepared handle* is required and returns **int**.  
  
 *cursor*  
 Is the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] generated cursor identifier. *cursor* is a required parameter that must be supplied on all subsequent procedures which act upon this cursor, e.g. sp_cursorfetch.  
  
 *params*  
 Identifies parameterized statements. The *params* definition of variables is substituted for parameter markers in the statement. *params* is a required parameter that calls for an **ntext**, **nchar**, or **nvarchar** input value.  
  
> [!NOTE]  
>  Use an **ntext** string as the input value when *stmt* is parameterized and the *scrollopt* PARAMETERIZED_STMT value is ON.  
  
 *statement*  
 Defines the cursor result set. The *statement* parameter is required and calls for an **ntext**, **nchar** or **nvarchar** input value.  
  
> [!NOTE]  
>  The rules for specifying the stmt value are the same as those for sp_cursoropen, with the exception that the *stmt* string data type must be **ntext**.  
  
 *options*  
 An optional parameter that returns a description of the cursor result set columns. *options* requires the following **int** input value.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|RETURN_METADATA|  
  
 *scrollopt*  
 Scroll Option. *scrollopt* is an optional parameter that requires one of the following **int** input values.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|KEYSET|  
|0x0002|DYNAMIC|  
|0x0004|FORWARD_ONLY|  
|0x0008|STATIC|  
|0x10|FAST_FORWARD|  
|0x1000|PARAMETERIZED_STMT|  
|0x2000|AUTO_FETCH|  
|0x4000|AUTO_CLOSE|  
|0x8000|CHECK_ACCEPTED_TYPES|  
|0x10000|KEYSET_ACCEPTABLE|  
|0x20000|DYNAMIC_ACCEPTABLE|  
|0x40000|FORWARD_ONLY_ACCEPTABLE|  
|0x80000|STATIC_ACCEPTABLE|  
|0x100000|FAST_FORWARD_ACCEPTABLE|  
  
 Because of the possibility that the requested option is not appropriate for the cursor defined by *\<stmt>*, this parameter serves as both input and output. In such cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns an appropriate type and modifies this value.  
  
 *ccopt*  
 Concurrency control option. *ccopt* is an optional parameter that requires one of the following **int** input values.  
  
|Value|Description|  
|-----------|-----------------|  
|0x0001|READ_ONLY|  
|0x0002|SCROLL_LOCKS (previously known as LOCKCC)|  
|0x0004|**OPTIMISTIC** (previously known as OPTCC)|  
|0x0008|OPTIMISTIC (previously known as OPTCCVAL)|  
|0x2000|ALLOW_DIRECT|  
|0x4000|UPDT_IN_PLACE|  
|0x8000|CHECK_ACCEPTED_OPTS|  
|0x10000|READ_ONLY_ACCEPTABLE|  
|0x20000|SCROLL_LOCKS_ACCEPTABLE|  
|0x40000|OPTIMISTIC_ACCEPTABLE|  
|0x80000|OPTIMISITC_ACCEPTABLE|  
  
 As with *scrollpt*, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can assign a different value than the one requested.  
  
 *rowcount*  
 Is an optional parameter that signifies the number of fetch buffer rows to use with AUTO_FETCH. The default is 20 rows. *rowcount* behaves differently when assigned as an input value versus a return value.  
  
|As input value|As return value|  
|--------------------|---------------------|  
|When AUTO_FETCH is specified with FAST_FORWARD cursors *rowcount* represents the number of rows to place into the fetch buffer.|Represents the number of rows in the result set. When the *scrollopt* AUTO_FETCH value is specified, *rowcount* returns the number of rows that were fetched into the fetch buffer.|  
  
## Return Code Values  
 If *params* returns a NULL value then the statement is not parameterized.  
  
## See Also  
 [sp_cursoropen &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursoropen-transact-sql.md)   
 [sp_cursorexecute &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorexecute-transact-sql.md)   
 [sp_cursorprepare &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorprepare-transact-sql.md)   
 [sp_cursorfetch &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-cursorfetch-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
