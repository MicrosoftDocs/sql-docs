---
title: "SET LOCK_TIMEOUT (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a4b5f651-0513-41ce-9631-60cde268f0ce
caps.latest.revision: 9
author: BarbKess
---
# SET LOCK_TIMEOUT (SQL Server PDW)
Specifies the number of milliseconds a SQL Server PDW statement waits for a lock to be released. SQL Server PDW always uses the default (-1) behavior for the LOCK_TIMEOUT setting, which is to wait indefinitely for locks to be released.  
  
> [!NOTE]  
> If you set LOCK_TIMEOUT to a value other than -1, SQL Server PDW will ignore the value and continue to wait indefinitely for locks to be released.  
  
For more information, see the [SET LOCK_TIMEOUT (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms189470(v=sql.11).aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET LOCK_TIMEOUT timeout_period;  
```  
  
## Arguments  
*timeout_period*  
Is the number of milliseconds that will pass before SQL Server PDW returns a locking error. The default is -1, which indicates to wait forever with no timeout period. All values, other than -1, are ignored.  
  
## Permissions  
Requires membership in the public role.  
  
## SQL Server Differences  
SQL Server PDW does not support the READPAST locking hint, which is available in SQL Server as an alternative to using the LOCK_TIMEOUT setting.  
  
## Examples  
  
### A. Set the lock timeout to wait forever for a lock to be released.  
The following example sets the lock timeout to wait forever and never expire. This is the default behavior that is already set at the beginning of each connection.  
  
```  
SET LOCK_TIMEOUT -1;  
```  
  
The following example sets the lock time-out period to `1800` milliseconds. In this release, SQL Server PDW will parse the statement successfully, but will ignore the value 1800 and continue to use the default behavior.  
  
```  
SET LOCK_TIMEOUT 1800;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SET Statements &#40;SQL Server PDW&#41;](../sqlpdw/set-statements-sql-server-pdw.md)  
  
