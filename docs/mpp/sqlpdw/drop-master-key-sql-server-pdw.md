---
title: "DROP MASTER KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 73392137-48b2-47c9-9d29-b7aa052e107e
caps.latest.revision: 6
author: BarbKess
---
# DROP MASTER KEY (SQL Server PDW)
Removes the master key. Must be executed in the master database. SQL Server PDW uses the master key to protect a certificate, which is used by transparent data encryption. For more information, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DROP MASTER KEY  
```  
  
## Arguments  
This statement takes no arguments.  
  
## Remarks  
The drop will fail if a certificate is protected by the master key.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
The following example removes the master key.  
  
```  
USE master;  
DROP MASTER KEY;  
GO  
```  
  
## See Also  
[CREATE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-master-key-sql-server-pdw.md)  
[ALTER MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/alter-master-key-sql-server-pdw.md)  
[OPEN MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/open-master-key-sql-server-pdw.md)  
[CLOSE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/close-master-key-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md)  
  
