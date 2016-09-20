---
title: "CREATE MASTER KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5cdbd5d4-fc61-4ed4-9bfd-25f6bb1187fa
caps.latest.revision: 9
author: BarbKess
---
# CREATE MASTER KEY (SQL Server PDW)
Creates a database master key. This statement must be executed in the master database.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE MASTER KEY ENCRYPTION BY PASSWORD ='password'  
```  
  
## Arguments  
PASSWORD ='*password*'  
Is the password that is used to encrypt the master key in the database. *password* must meet the SQL Server PDW password policy.  
  
> [!IMPORTANT]  
> Communication traffic between components inside the SQL Server PDW is not encrypted. High privileged users could potentially install network sniffing software to obtain the DMK password. Mitigate this risk by controlling physical access to the SQL Server PDW.  
  
## Remarks  
The database master key is a symmetric key used to protect the database encryption key during transparent data encryption. For more information about TDE, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md). When it is created, the master key is encrypted by using the AES_256 algorithm and a user-supplied password. To enable the automatic decryption of the master key, a copy of the key is encrypted by using the service master key and stored in in the master database. This default can be changed by using the `DROP ENCRYPTION BY SERVICE MASTER KEY` option of [ALTER MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-master-key-sql-server-pdw.md). A master key that is not encrypted by the service master key must be opened by using the [OPEN MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/open-master-key-sql-server-pdw.md) statement and a password.  
  
The is_master_key_encrypted_by_server column of the sys.databases catalog view in master indicates whether the database master key is encrypted by the service master key.  
  
Information about the database master key is visible in the sys.symmetric_keys catalog view.  
  
The service master key and database master keys are protected by using the AES-256 algorithm.  
  
## Permissions  
Requires **CONTROL SERVER**.  
  
## Examples  
The following example creates a master key. The key is encrypted using the password `23987hxJ#KL95234nl0zBe`.  
  
```  
USE master;  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '23987hxJ#KL95234nl0zBe';  
GO  
```  
  
## See Also  
[ALTER MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/alter-master-key-sql-server-pdw.md)  
[DROP MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-master-key-sql-server-pdw.md)  
[OPEN MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/open-master-key-sql-server-pdw.md)  
[CLOSE MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/close-master-key-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-databases-sql-server-pdw.md)  
  
