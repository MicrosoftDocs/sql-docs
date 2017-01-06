---
title: "CREATE DATABASE ENCRYPTION KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f29e7773-31a1-4c6e-8253-ac6f952a9ea6
caps.latest.revision: 9
author: BarbKess
---
# CREATE DATABASE ENCRYPTION KEY (SQL Server PDW)
Creates an encryption key that is used for transparently encrypting a database. For more information about transparent database encryption, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CREATE DATABASE ENCRYPTION KEY  
       WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY }  
   ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name   
[ ; ]  
```  
  
## Arguments  
WITH ALGORITHM = { AES_128 | AES_192 | AES_256 | TRIPLE_DES_3KEY  }  
Specifies the encryption algorithm that is used for the encryption key.  
  
ENCRYPTION BY SERVER CERTIFICATE Encryptor_Name  
Specifies the name of the encryptor used to encrypt the database encryption key.  
  
## Remarks  
A database encryption key is required before a database can be encrypted by using TDE. The certificate that is used to encrypt the database encryption key must be located in the master system database.  
  
Database encryption statements are allowed only on user databases.  
  
The database encryption key cannot be exported from the database. It is available only to the system, to users who have debugging permissions on the server, and to users who have access to the certificates that encrypt and decrypt the database encryption key.  
  
The database encryption key does not have to be regenerated when a database owner (dbo) is changed.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
For additional examples using TDE, see [Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md).  
  
The following example creates a database encryption key by using the `AES_256` algorithm, and protects the private key with a certificate named `MyServerCert`.  
  
```  
USE AdventureWorksPDW2012;  
GO  
CREATE DATABASE ENCRYPTION KEY  
WITH ALGORITHM = AES_256  
ENCRYPTION BY SERVER CERTIFICATE MyServerCert;  
GO  
```  
  
## See Also  
[Transparent Data Encryption &#40;SQL Server PDW&#41;](../sqlpdw/transparent-data-encryption-sql-server-pdw.md)  
[ALTER DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../sqlpdw/alter-database-encryption-key-sql-server-pdw.md)  
[DROP DATABASE ENCRYPTION KEY &#40;SQL Server PDW&#41;](../sqlpdw/drop-database-encryption-key-sql-server-pdw.md)  
[sys.dm_pdw_nodes_database_encryption_keys &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-database-encryption-keys-sql-server-pdw.md)  
[sp_pdw_database_encryption_regenerate_system_keys &#40;SQL Server PDW&#41;](../sqlpdw/sp-pdw-database-encryption-regenerate-system-keys-sql-server-pdw.md)  
  
