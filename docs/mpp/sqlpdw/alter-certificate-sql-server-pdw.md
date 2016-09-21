---
title: "ALTER CERTIFICATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 0ad3ef95-f74b-45b0-967c-62b9d35e6c5f
caps.latest.revision: 10
author: BarbKess
---
# ALTER CERTIFICATE (SQL Server PDW)
Removes the private key used to encrypt a certificate, or adds one if none is present. Can only be executed in the master database.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER CERTIFICATE certificate_name   
{  
    REMOVE PRIVATE KEY  
    |  
    WITH PRIVATE KEY (   
        FILE = '<path_to_private_key>',  
        DECRYPTION BY PASSWORD = '<key password>'  
    )}  
```  
  
## Arguments  
*certificate_name*  
Is the unique name by which the certificate is known in database.  
  
FILE **='***path_to_private_key***'**  
Specifies the complete path, including file name, to the private key. This parameter must be a UNC path to a network location. This file will be accessed within the security context of a credential added by using [sp_pdw_add_network_credential](../sqlpdw/sp-pdw-add-network-credentials-sql-server-pdw.md). When you use this option, you must make sure that the credential account has access to the specified file.  
  
DECRYPTION BY PASSWORD **='***key_password***'**  
Specifies the password that is required to decrypt the private key.  
  
> [!IMPORTANT]  
> Communication traffic between components inside the SQL Server PDW is not encrypted. High privileged users could potentially install network sniffing software to obtain the certificate private and public keys. Mitigate this risk by controlling access to the SQL Server PDW.  
  
REMOVE PRIVATE KEY  
Specifies that the private key should no longer be maintained inside the database.  
  
## Remarks  
The private key must correspond to the public key specified by *certificate_name*.  
  
When the private key of a certificate that already exists in the database is imported from a file, the private key will be automatically protected by the database master key.  
  
The REMOVE PRIVATE KEY option will delete the private key of the certificate from the database. Do not remove the private key of a certificate that protects a symmetric key.  
  
> [!IMPORTANT]  
> Always make an archival copy of a private key before removing it from a database. For more information, see [BACKUP CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/backup-certificate-sql-server-pdw.md).  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
  
### A. Importing a private key for a certificate that is already present in the database  
  
```  
ALTER CERTIFICATE Shipping13   
    WITH PRIVATE KEY (FILE = '\\ServerA7\importedkeys\Shipping13',  
    DECRYPTION BY PASSWORD = 'GDFLKl8^^GGG4000%');  
GO  
```  
  
## See Also  
[CREATE CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/create-certificate-sql-server-pdw.md)  
[DROP CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/drop-certificate-sql-server-pdw.md)  
[BACKUP CERTIFICATE &#40;SQL Server PDW&#41;](../sqlpdw/backup-certificate-sql-server-pdw.md)  
[sys.certificates &#40;SQL Server PDW&#41;](../sqlpdw/sys-certificates-sql-server-pdw.md)  
  
