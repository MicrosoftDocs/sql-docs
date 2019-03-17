---
title: "Service Master Key | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "service master key [SQL Server]"
  - "service master key [SQL Server], about service master key"
ms.assetid: 85f2095d-2590-4f59-8a29-7e100edd02bb
author: aliceku
ms.author: aliceku
manager: craigg
---
# Service Master Key
  The Service Master Key is the root of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption hierarchy. It is generated automatically the first time it is needed to encrypt another key. By default, the Service Master Key is encrypted using the Windows data protection API and using the local machine key. The Service Master Key can only be opened by the Windows service account under which it was created or by a principal with access to both the service account name and its password.  
  
 Regenerating or restoring the Service Master Key involves decrypting and re-encrypting the complete encryption hierarchy. Unless the key has been compromised, this resource-intensive operation should be scheduled during a period of low demand.  
  
 [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. After upgrading an instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] the SMK and DMK should be regenerated in order to upgrade the master keys to AES. For more information about regenerating the SMK, see [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-service-master-key-transact-sql) and [ALTER MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-master-key-transact-sql).  
  
## Best Practice  
 Back up the Service Master Key and store the backed up copy in a secure, off-site location.  
  
## Related Tasks  
 [BACKUP SERVICE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-service-master-key-transact-sql)  
  
 [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-service-master-key-transact-sql)  
  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-service-master-key-transact-sql)  
  
## See Also  
 [Encryption Hierarchy](encryption-hierarchy.md)  
  
  
