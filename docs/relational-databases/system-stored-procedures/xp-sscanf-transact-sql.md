---
description: "xp_sscanf (Transact-SQL)"
title: "xp_sscanf (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "xp_sscanf_TSQL"
  - "xp_sscanf"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "xp_sscanf"
ms.assetid: 619a9df1-7008-407e-a75a-bc6f851454a8
author: markingmyname
ms.author: maghan
---
# xp_sscanf (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Reads data from the string into the argument locations specified by each format argument.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
xp_sscanf { string OUTPUT , format } [ ,argument [ ,...n ] ]   
```  
  
## Arguments  
 **string**  
 Is the character string to read the argument values from.  
  
 OUTPUT  
 When specified, puts the value of *argument* in the output parameter.  
  
 *format*  
 Is a formatted character string similar to what is supported by the C-language **sscanf** function. Currently, only the %s format argument is supported.  
  
 *argument*  
 Is a **varchar** variable set to the value of the corresponding *format* argument.  
  
 *n*  
 Is a placeholder that indicates that a maximum of 50 arguments can be specified.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 **xp_sscanf** returns the following message:  
  
 `Command(s) completed successfully.`  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example uses `xp_sscanf` to extract two values from a source string based on their positions in the format of the source string.  
  
```  
DECLARE @filename varchar (20), @message varchar (20);  
EXEC xp_sscanf 'sync -b -fproducts10.tmp -rrandom', 'sync -b -f%s -r%s',   
  @filename OUTPUT, @message OUTPUT;  
SELECT @filename, @message;  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
-------------------- --------------------   
products10.tmp        random  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [General Extended Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/general-extended-stored-procedures-transact-sql.md)   
 [xp_sprintf &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/xp-sprintf-transact-sql.md)  
  
  
