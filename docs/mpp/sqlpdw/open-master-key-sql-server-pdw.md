---
title: "OPEN MASTER KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8f140de7-3c9f-4b16-b9fc-dbe12a0616e3
caps.latest.revision: 9
author: BarbKess
---
# OPEN MASTER KEY (SQL Server PDW)
Opens the database master key in the master database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OPEN MASTER KEY DECRYPTION BY PASSWORD ='password' [ ; ]  
```  
  
## Arguments  
'*password*'  
The password with which the database master key was encrypted.  
  
> [!IMPORTANT]  
> Communication traffic between components inside the SQL Server PDW is not encrypted. High privileged users could potentially install network sniffing software to obtain the DMK password. Mitigate this risk by controlling physical access to the SQL Server PDW.  
  
## Remarks  
If the database master key was encrypted with the service master key, it will be automatically opened when it is needed for decryption or encryption. In this case, it is not necessary to use the `OPEN MASTER KEY` statement.  
  
When a database is first attached or restored to a new instance of SQL Server PDW, a copy of the database master key (encrypted by the service master key) is not yet stored in the server. You must use the `OPEN MASTER KEY` statement to decrypt the database master key (DMK). Once the DMK has been decrypted, you have the option of enabling automatic decryption in the future by using the `ALTER MASTER KEY REGENERATE` statement to provision the server with a copy of the DMK encrypted with the service master key (SMK).  
  
You can exclude the database master key from automatic key management by using the `ALTER MASTER KEY` statement with the `DROP ENCRYPTION BY SERVICE MASTER KEY` option. Afterward, you must explicitly open the database master key with a password.  
  
If a transaction in which the database master key was explicitly opened is rolled back, the key will remain open.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
The following example opens the database master, which has been encrypted with a password.  
  
```  
USE master;  
OPEN MASTER KEY DECRYPTION BY PASSWORD = '43987hkhj4325tsku7';  
GO  
CLOSE MASTER KEY;  
GO  
```  
  
## See Also  
[CREATE MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-master-key-sql-server-pdw.md)  
[ALTER MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-master-key-sql-server-pdw.md)  
[DROP MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-master-key-sql-server-pdw.md)  
[CLOSE MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/close-master-key-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-databases-sql-server-pdw.md)  
  
