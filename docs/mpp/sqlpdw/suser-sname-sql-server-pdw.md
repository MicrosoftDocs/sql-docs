---
title: "SUSER_SNAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c332c94b-415e-4d84-b385-7054e7b216f7
caps.latest.revision: 6
author: BarbKess
---
# SUSER_SNAME (SQL Server PDW)
Returns the current login name or the login name associated with a security identification number (SID). For more information, see the [SUSER_SNAME (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms174427.aspx) documentation on MSDN. All features in the MSDN documentation may not be available in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SUSER_SNAME ( [ server_user_sid ] )  
```  
  
## Arguments  
*server_user_sid*  
Is the login security identification number. *server_user_sid*, which is optional, is **varbinary(85)**. *server_user_sid* can be the security identification number of any SQL Server login. If *server_user_sid* is not specified, information about the current user is returned. If the parameter contains the word NULL will return NULL.  
  
## Return Types  
**nvarchar(128)**  
  
## Remarks  
When called without an argument, SUSER_SNAME returns the name of the current security context. When called without an argument within a batch that has switched context by using EXECUTE AS, SUSER_SNAME returns the name of the impersonated context. When called from an impersonated context, ORIGINAL_LOGIN returns the name of the original context.  
  
The **SYSTEM_USER** and **SUSER_NAME()** functions are not supported in SQL Server PDW. Use **SUSER_SNAME()** instead.  
  
## Examples  
  
### A. Using SUSER_SNAME  
The following example returns the login name for the security identification number with a value of `0x01`.  
  
```  
SELECT SUSER_SNAME(0x01);  
GO  
```  
  
### B. Returning the Current Login  
The following example returns the login name of the current login.  
  
```  
SELECT SUSER_SNAME() AS CurrentLogin;  
GO  
```  
  
## See Also  
[sys.server_principals &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-server-principals-sql-server-pdw.md)  
[sys.server_permissions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-server-permissions-sql-server-pdw.md)  
  
