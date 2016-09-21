---
title: "SET QUOTED_IDENTIFIER (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: efaa6883-04b0-4189-85ff-2bb6ec2c631c
caps.latest.revision: 7
author: BarbKess
---
# SET QUOTED_IDENTIFIER (SQL Server PDW)
Causes SQL Server PDW to follow the ISO rules regarding quotation mark delimiting identifiers and literal strings. Use this setting to allow identifiers to be reserved keywords or to contain characters that are not allowed by the syntax rules for identifiers.  
  
In this release, the QUOTED_IDENTIFIER setting is always ON.  
  
For more information, see the [SET QUOTED_IDENTIFIER (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174393(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET QUOTED_IDENTIFIER ON  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
When SET QUOTED_IDENTIFIER is ON:  
  
1.  Identifiers can be delimited by double quotation marks.  
  
2.  Literal strings must be delimited by single quotation marks. Literal strings cannot be delimited by double quotation marks.  
  
3.  If a single quotation mark (**'**) is part of the literal string, it can be represented by two single quotation marks (**"**).  
  
4.  All strings delimited by double quotation marks are interpreted as object identifiers  
  
5.  Quoted identifiers do not have to follow the SQL rules for identifiers. They can be reserved keywords and can include characters not generally allowed in Transact\-SQL identifiers.  
  
SET QUOTED_IDENTIFIER is set at parse time. Setting at parse time means that if the SET statement is present in the batch or stored procedure, it takes effect, regardless of whether code execution actually reaches that point; and the SET statement takes effect before any statements are executed.  
  
Using brackets, **[** and **]**, to delimit identifiers is not affected by the QUOTED_IDENTIFIER setting.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
