---
title: "ALTER MASTER KEY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 725b1083-2c66-4332-a1bc-a46314b0930b
caps.latest.revision: 9
author: BarbKess
---
# ALTER MASTER KEY (SQL Server PDW)
Changes the properties of a database master key.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ALTER MASTER KEY <alter_option>  
  
<alter_option> ::=  
    <regenerate_option> | <encryption_option>  
  
<regenerate_option> ::=  
    [ FORCE ] REGENERATE WITH ENCRYPTION BY PASSWORD ='password'<encryption_option> ::=  
    ADD ENCRYPTION BY SERVICE MASTER KEY   
    |   
    DROP ENCRYPTION BY SERVICE MASTER KEY  
```  
  
## Arguments  
PASSWORD ='*password*'  
Specifies a password with which to encrypt the database master key. *password* must meet the SQL Server PDW password policy.  
  
> [!IMPORTANT]  
> Communication traffic between components inside the SQL Server PDW is not encrypted. High privileged users could potentially install network sniffing software to obtain the DMK password. Mitigate this risk by controlling physical access to the SQL Server PDW.  
  
## Remarks  
The REGENERATE option re-creates the database master key and all the keys it protects. The keys are first decrypted with the old master key, and then encrypted with the new master key. This resource-intensive operation should be scheduled during a period of low demand, unless the master key has been compromised.  
  
SQL Server PDW uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions.  
  
When the **FORCE** option is used, key regeneration will continue even if the master key is unavailable or the server cannot decrypt all the encrypted private keys. Use the **FORCE** option only if the master key is irretrievable or if decryption fails. Information that is encrypted only by an irretrievable key will be lost.  
  
The `DROP ENCRYPTION BY SERVICE MASTER KEY` option removes the encryption of the database master key by the service master key.  
  
`ADD ENCRYPTION BY SERVICE MASTER KEY` causes a copy of the master key to be encrypted using the service master key and stored in both the current database and in master.  
  
## Permissions  
Requires **CONTROL SERVER** permission.  
  
## Examples  
The following example creates a new database master key for `AdventureWorksPDW2012` and re-encrypts the keys below it in the encryption hierarchy.  
  
```  
USE master;  
ALTER MASTER KEY REGENERATE WITH ENCRYPTION BY PASSWORD = 'dsjdkflJ435907NnmM#sX003';  
GO  
```  
  
## See Also  
[CREATE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/create-master-key-sql-server-pdw.md)  
[DROP MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/drop-master-key-sql-server-pdw.md)  
[OPEN MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/open-master-key-sql-server-pdw.md)  
[CLOSE MASTER KEY &#40;SQL Server PDW&#41;](../sqlpdw/close-master-key-sql-server-pdw.md)  
[sys.databases &#40;SQL Server PDW&#41;](../sqlpdw/sys-databases-sql-server-pdw.md)  
  
