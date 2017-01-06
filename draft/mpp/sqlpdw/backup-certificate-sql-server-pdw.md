---
title: "BACKUP CERTIFICATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 877fead0-895a-4a41-81c6-eaa7a1aaa964
caps.latest.revision: 8
author: BarbKess
---
# BACKUP CERTIFICATE (SQL Server PDW)
Exports a certificate to a file. Can only be executed in the master database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
BACKUP CERTIFICATE certname TO FILE ='path_to_file'  
      WITH PRIVATE KEY   
      (   
        FILE ='path_to_private_key_file',  
        ENCRYPTION BY PASSWORD ='encryption_password'   
      )  
```  
  
## Arguments  
*path_to_file*  
Specifies the complete path, including file name, of the file in which the certificate is to be saved. This must be a UNC path to a network location.  
  
*path_to_private_key_file*  
Specifies the complete path, including file name, of the file in which the private key is to be saved. This must be a UNC path to a network location.  
  
*encryption_password*  
Is the password that is used to encrypt the private key before writing the key to the backup file. The password is subject to complexity checks.  
  
## Remarks  
When you back up the private key to a file, encryption is required. The password used to protect the backed up certificate is not the same password that is used to encrypt the private key of the certificate.  
  
To restore a backed up certificate, use the [CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md) statement.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
  
### A. Exporting a certificate and a private key  
In the following example, the private key of the certificate that is backed up will be encrypted with the password `997jkhUbhk$w4ez0876hKHJH5gh`.  
  
```  
BACKUP CERTIFICATE sales05 TO FILE = '\\ServerA7\storedcerts\sales05cert'  
    WITH PRIVATE KEY ( FILE = '\\ServerA7\storedkeys\sales05key' ,   
    ENCRYPTION BY PASSWORD = '997jkhUbhk$w4ez0876hKHJH5gh' );  
GO  
```  
  
## See Also  
[CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md)  
[ALTER CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/alter-certificate-sql-server-pdw.md)  
[DROP CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/drop-certificate-sql-server-pdw.md)  
[sys.certificates &#40;SQL Server PDW&#41;](../sqlpdw/sys-certificates-sql-server-pdw.md)  
  
