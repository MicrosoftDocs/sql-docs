---
title: "USER_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a4165504-b576-4a80-b54a-a4a37f5ec273
caps.latest.revision: 7
author: BarbKess
---
# USER_NAME (SQL Server PDW)
Returns a database user name from a specified identification number.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
USER_NAME ( [ id ] )  
```  
  
## Arguments  
*id*  
The identification number associated with a database user. *id*is **int**. The parentheses are required. When *id* is omitted, the current user in the current context is assumed.  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
If the parameter contains the word NULL will return NULL.  
  
## Examples  
  
### A. Using USER_NAME  
The following example returns the user name for user ID `13`.  
  
```  
SELECT USER_NAME(13);  
```  
  
### B. Using USER_NAME without an ID  
The following example finds the name of the current user without specifying an ID.  
  
```  
SELECT USER_NAME();  
```  
  
Here is the result set for a currently logged-in user.  
  
Here is the result set.  
  
```  
------------------------------   
User7  
```  
  
### C. Using USER_NAME in the WHERE clause  
The following example finds the row in `sysusers` in which the name is equal to the result of applying the system function `USER_NAME` to user identification number `1`.  
  
```  
SELECT name FROM sysusers WHERE name = USER_NAME(1);  
```  
  
Here is the result set.  
  
```  
name                             
------------------------------   
User7  
```  
  
## See Also  
[USER &#40;SQL Server PDW&#41;](../sqlpdw/user-sql-server-pdw.md)  
[SUSER_SNAME &#40;SQL Server PDW&#41;](../sqlpdw/suser-sname-sql-server-pdw.md)  
[HAS_DBACCESS &#40;SQL Server PDW&#41;](../sqlpdw/has-dbaccess-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
[sys.database_permissions &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-permissions-sql-server-pdw.md)  
  
