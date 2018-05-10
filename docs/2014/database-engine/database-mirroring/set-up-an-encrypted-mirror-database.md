---
title: "Set Up an Encrypted Mirror Database | Microsoft Docs"
ms.custom: ""
ms.date: "06/23/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "cryptography [SQL Server], database mirroring"
  - "encryption [SQL Server], database mirroring"
  - "database master key [SQL Server], database mirroring"
  - "mirror database [SQL Server]"
  - "database mirroring [SQL Server], security"
ms.assetid: 7329a575-be29-46e0-abc6-1344db37920c
caps.latest.revision: 22
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Set Up an Encrypted Mirror Database
  To enable automatic decryption of the database master key of a mirror database, you must provide the password used to encrypt the master key to the mirror server instance. [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions include mechanisms to transfer the password. Use **sp_control_dbmasterkey_password** to create a credential for the database master key before you start database mirroring. You must repeat this process for every database that will be mirrored. For more information, see [sp_control_dbmasterkey_password &#40;Transact-SQL&#41;](~/sql/relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql?view=sql-server-2016).  
  
> [!CAUTION]  
>  Do not enable failover decryption of a database that must remain inaccessible to **sa** and other highly privileged server principals. You can configure a database so that its key hierarchy cannot be decrypted by the service master key. This option is supported as a defense-in-depth for databases that contain information that should not be accessible to **sa** or other highly privileged server principals. Enabling failover decryption of such a database removes this defense-in-depth, enabling **sa** and other highly privileged server principals to decrypt the database.  
  
## See Also  
 [sp_control_dbmasterkey_password &#40;Transact-SQL&#41; 3](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql?view=sql-server-2016)   
 [sp_control_dbmasterkey_password &#40;Transact-SQL&#41; 4](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41; 5](/sql/t-sql/statements/create-master-key-transact-sql?view=sql-server-2016)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41; 1](~/sql/t-sql/statements/create-master-key-transact-sql?view=sql-server-2016)   
 [CREATE MASTER KEY &#40;Transact-SQL&#41; 2](../../../t-sql/statements/create-master-key-transact-sql.md)   
 [ALTER MASTER KEY &#40;Transact-SQL&#41;](~/sql/t-sql/statements/alter-master-key-transact-sql)   
 [Encryption Hierarchy](../../relational-databases/security/encryption/encryption-hierarchy.md)   
 [Setting Up Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)  
  
  