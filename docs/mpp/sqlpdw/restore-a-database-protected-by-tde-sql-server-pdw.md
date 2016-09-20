---
title: "Restore a Database Protected by TDE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ffb681ca-8598-4614-b06c-660376333fc3
caps.latest.revision: 4
author: BarbKess
---
# Restore a Database Protected by TDE (SQL Server PDW)
Use the following steps to restore a database that is encrypted by using transparent data encryption.  
  
The **Using Transparent Data Encryption** section of the topic [Transparent Data Encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/transparent-data-encryption-sql-server-pdw.md) contains example code to enable TDE on the `AdventureWorksPDW2012` database. The following code continues that example, by creating a backup of the database on the original SQL Server PDW, and then restoring the certificate and the database on a new SQL Server PDW.  
  
The first step is to create a backup of the source database.  
  
```  
BACKUP DATABASE AdventureWorksPDW2012   
TO DISK = '\\SECURE_SERVER\Backups\AdventureWorksPDW2012';  
```  
  
Prepare the new SQL Server PDW for TDE by creating a master key, enabling encryption, and creating a network credential.  
  
```  
USE master;  
GO  
  
-- Create a database master key in the master database  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<UseStrongPasswordHere>';  
GO  
  
-- Enable encryption for PDW  
EXEC sp_pdw_database_encryption 1;  
GO  
  
EXEC sp_pdw_add_network_credentials 'SECURE_SERVER', '<domain>\<Windows_user>', '<password>';  
```  
  
The last two steps recreate the certificate by using the backups from the original SQL Server PDW. Use the password that you used when you created the backup of the certificate.  
  
```  
-- Create certificate in master  
CREATE CERTIFICATE MyServerCert  
    FROM FILE = '\\SECURE_SERVER\cert\MyServerCert.cer'   
    WITH PRIVATE KEY (FILE = '\\SECURE_SERVER\cert\MyServerCert.key',   
    DECRYPTION BY PASSWORD = '<password>');  
  
RESTORE DATABASE AdventureWorksPDW2012   
    FROM DISK = '\\SECURE_SERVER\Backups\AdventureWorksPDW2012';  
```  
  
## See Also  
[BACKUP DATABASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/backup-database-sql-server-pdw.md)  
[CREATE MASTER KEY &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-master-key-sql-server-pdw.md)  
[sp_pdw_add_network_credentials &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-add-network-credentials-sql-server-pdw.md)  
[sp_pdw_database_encryption &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sp-pdw-database-encryption-sql-server-pdw.md)  
[CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/create-certificate-sql-server-pdw.md)  
[RESTORE DATABASE &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/restore-database-sql-server-pdw.md)  
  
