---
title: "(Backslash) (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "SQL Server (starting with 2008)"
f1_keywords: 
  - "\\_TSQL"
  - "\\"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backwhack"
  - "backslash"
  - "excape character"
  - "hack character"
  - "\\ (backslash)"
  - "backslant"
  - "bash"
  - "reverse slant"
  - "slosh"
  - "reversed virgule"
  - "line continuation character"
  - "reverse solidus"
ms.assetid: c97fbb20-3d12-4d0b-9b52-62a229bc83c0
caps.latest.revision: 22
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# SQL Server Utilities Statements - Backslash
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides commands that are not [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, but are recognized by the **sqlcmd** and **osql** utilities and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Code Editor. These commands can be used to facilitate the readability and execution of batches and scripts.  
  
\  breaks a long string constant into two or more lines for readability.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
<first section of string> \  
<continued section of string>  
```  
  
## Arguments  
 \<first section of string>  
 Is the start of a string.  
  
 \<continued section of string>  
 Is the continuation of a string.  
  
## Remarks  
 This command returns the first and continued sections of the string as one string, without the backslash.  
  
 The backslash is not a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. It is a command that is recognized by the **sqlcmd** and **osql** utilities and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Code Editor.  
  
## Examples  
 The following example uses a backslash and a carriage return to split the string into two lines.  
  
```  
SELECT 'abc\  
def' AS ColumnResult;  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```  
 ColumnResult  
 ------------  
 abcdef
 ```    
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Built-in Functions &#40;Transact-SQL&#41;](~/t-sql/functions/functions.md)   
 [Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/operators-transact-sql.md)   
 [&#40;Divide&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-transact-sql.md)   
 [&#40;Divide EQUALS&#41; &#40;Transact-SQL&#41;](../../t-sql/language-elements/divide-equals-transact-sql.md)   
 [Compound Operators &#40;Transact-SQL&#41;](../../t-sql/language-elements/compound-operators-transact-sql.md)  
  
  
