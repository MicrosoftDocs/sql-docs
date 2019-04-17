---
title: "Service Master Key | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: vanto
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
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Service Master Key is the root of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption hierarchy. It is generated automatically the first time it is needed to encrypt another key. By default, the Service Master Key is encrypted using the Windows data protection API and using the local machine key. The Service Master Key can only be opened by the Windows service account under which it was created or by a principal with access to both the service account name and its password.  
  
 Regenerating or restoring the Service Master Key involves decrypting and re-encrypting the complete encryption hierarchy. Unless the key has been compromised, this resource-intensive operation should be scheduled during a period of low demand.  
  
 [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] uses the AES encryption algorithm to protect the service master key (SMK) and the database master key (DMK). AES is a newer encryption algorithm than 3DES used in earlier versions. After upgrading an instance of the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] to [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] the SMK and DMK should be regenerated in order to upgrade the master keys to AES. For more information about regenerating the SMK, see [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-service-master-key-transact-sql.md) and [ALTER MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-master-key-transact-sql.md).  
  
## Best Practice  
 Back up the Service Master Key and store the backed up copy in a secure, off-site location.  
  
## Related Tasks  
 [BACKUP SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/backup-service-master-key-transact-sql.md)  
  
 [RESTORE SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/restore-service-master-key-transact-sql.md)  
  
 [ALTER SERVICE MASTER KEY &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-service-master-key-transact-sql.md)  
  
## See Also  
 [Encryption Hierarchy](../../../relational-databases/security/encryption/encryption-hierarchy.md)  
  
  
