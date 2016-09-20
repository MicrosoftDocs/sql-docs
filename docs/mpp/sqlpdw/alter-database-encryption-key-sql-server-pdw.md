---
title: "ALTER DATABASE ENCRYPTION KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a6a2e995-3cd5-465f-9d81-c5d9b9ce56d5
caps.latest.revision: 11
author: BarbKess
---
# ALTER DATABASE ENCRYPTION KEY (SQL Server PDW)
Alters an encryption key and certificate that is used for transparently encrypting a database. For more information about transparent database encryption, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
> [!WARNING]  
> Regenerating the database encryption key requires decrypting and then re-encrypting the entire database. Changing the certificate occurs quickly because it only triggers re-encryption of the DEK that is protected by a certificate.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER DATABASE ENCRYPTION KEY  
    {  
      {  
        REGENERATE WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
        [ ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name ]  
      }  
      |  
      ENCRYPTION BY SERVER   CERTIFICATE Encryptor_Name    
    }  
[ ; ]  
```  
  
## Arguments  
REGENERATE WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
Specifies the encryption algorithm that is used for the encryption key.  
  
ENCRYPTION BY SERVER CERTIFICATE *Encryptor_Name*  
Specifies the name of the certificate used to encrypt the database encryption key.  
  
ENCRYPTION BY SERVER ASYMMETRIC KEY is   not supported in this version of SQL Server PDW.  
  
## Remarks  
The certificate that is used to encrypt the database encryption key must be located in the master database.  
  
The database encryption key does not have to be regenerated when a database owner (dbo) is changed.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
The following example alters the database encryption key to use the `AES_256` algorithm.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER DATABASE ENCRYPTION KEY  
REGENERATE WITH ALGORITHM = AES_256;  
GO  
```  
  
## See Also  
[Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md)  
[CREATE DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-database-encryption-key-sql-server-pdw.md)  
[DROP DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/drop-database-encryption-key-sql-server-pdw.md)  
[sys.dm_pdw_nodes_database_encryption_keys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-nodes-database-encryption-keys-sql-server-pdw.md)  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
  
