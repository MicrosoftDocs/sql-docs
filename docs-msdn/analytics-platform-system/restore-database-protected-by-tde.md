---

title: "Restore a Database Protected by TDE in Parallel Data Warehouse"
description: "Use the following steps to restore a database that is encrypted by using transparent data encryption."

author: "barbkess" 
ms.author: "barbkess"
ms.date: "10/20/2016"
ms.topic: "article"

ms.assetid: ffb681ca-8598-4614-b06c-660376333fc3
caps.latest.revision: 4

---
# Restore a database protected by TDE
Use the following steps to restore a database that is encrypted by using transparent data encryption.  
  
The [Using Transparent Data Encryption](transparent-data-encryption.md#using-tde) example has code to enable TDE on the `AdventureWorksPDW2012` database. The following code continues that example, by creating a backup of the database on the original Analytics Platform System (APS) appliance, and then restoring the certificate and the database on a different appliance.  
  
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
[BACKUP DATABASE](https://msdn.microsoft.com/library/mt631607.aspx)  
[CREATE MASTER KEY](https://msdn.microsoft.com/library/ms174382.aspx) 
[sp_pdw_add_network_credentials](https://msdn.microsoft.com/library/mt204011.aspx)  
[sp_pdw_database_encryption](https://msdn.microsoft.com/library/mt219360.aspx)  
[CREATE CERTIFICATE](https://msdn.microsoft.com/library/ms187798.aspx)  
[RESTORE DATABASE](https://msdn.microsoft.com/library/mt631612.aspx)
  
