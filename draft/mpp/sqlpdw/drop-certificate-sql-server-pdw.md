---
title: "DROP CERTIFICATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f39fd257-b5f8-45df-8f40-c859f6b5f3cc
caps.latest.revision: 10
author: BarbKess
---
# DROP CERTIFICATE (SQL Server PDW)
Removes a certificate from a SQL Server PDW database. Can only be executed in the master database.  
  
> [!IMPORTANT]  
> The encrypting certificate should be retained even if TDE is no longer enabled on the database. Even though the database is not encrypted, parts of the transaction log may still remain protected, and the certificate may be needed for some operations until the full backup of the database is performed.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP CERTIFICATE certificate_name  
```  
  
## Arguments  
*certificate_name*  
Is the unique name by which the certificate is known in the database.  
  
## Remarks  
Certificates can only be dropped if no entities are associated with them.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
The following example drops the certificate `Shipping04`.  
  
```  
USE master;  
DROP CERTIFICATE Shipping04;  
```  
  
## See Also  
[CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md)  
[ALTER CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/alter-certificate-sql-server-pdw.md)  
[BACKUP CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/backup-certificate-sql-server-pdw.md)  
[sys.certificates &#40;SQL Server PDW&#41;](../sqlpdw/sys-certificates-sql-server-pdw.md)  
  
