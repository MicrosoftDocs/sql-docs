---
title: "Configure and Manage Encryption Keys (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "encryption keys [Reporting Services]"
  - "private keys [Reporting Services]"
  - "cryptography [Reporting Services]"
  - "symmetric keys [Reporting Services]"
  - "encryption [Reporting Services]"
  - "public keys [Reporting Services]"
ms.assetid: 58e61636-88a2-4338-ae5f-3dd210aee887
author: markingmyname
ms.author: maghan
manager: kfile
---
# Configure and Manage Encryption Keys (SSRS Configuration Manager)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses encryption keys to secure credentials and connection information that is stored in a report server database. In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], encryption is supported through a combination of public, private, and symmetric keys that are used to protect sensitive data. The symmetric key is created during report server initialization when you install or configure the report server, and it is used by the report server to encrypt sensitive data that is stored in the report server. Public and private keys are created by the operating system, and they are used to protect the symmetric key. A public and private key pair is created for each report server instance that stores sensitive data in a report server database.  
  
 Managing the encryption keys consists of creating a backup copy of the symmetric key, and knowing when and how to restore, delete, or change the keys. If you migrate a report server installation or configure a scale-out deployment, you must have a backup copy of the symmetric key so that you can apply it to the new installation.  
  
> [!IMPORTANT]  
>  Periodically changing the Reporting Services encryption key is a security best practice. A recommended time to change the key is immediately following a major version upgrade of Reporting Services. Changing the key after an upgrade minimizes additional service interruption caused by changing the Reporting Services encryption key outside of the upgrade cycle.  
  
 To manage symmetric keys, you can use the Reporting Services Configuration tool or the **rskeymgmt** utility. The tools included in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are used to manage the symmetric key only (the public and private keys are managed by the operating system). Both the Reporting Services Configuration tool and the **rskeymgmt** utility support the following tasks:  
  
-   Back up a copy of the symmetric key so that you can use it to recover a report server installation or as part of a planned migration.  
  
-   Restore a previously saved symmetric key to a report server database, allowing a new report server instance to access existing data that it did not originally encrypt.  
  
-   Delete the encrypted data in a report server database in the unlikely event that you can no longer access encrypted data.  
  
-   Re-create symmetric keys and re-encrypt data in the unlikely event that the symmetric key is compromised. As a security best practice, you should recreate the symmetric key periodically (for example, every few months) to protect the report server database from cyber attacks that attempt to decipher the key.  
  
-   Add or remove a report server instance from a report server scale-out deployment where multiple report servers share both a single report server database and the symmetric key that provides reversible encryption for that database.  
  
## In This Section  
 [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-initialize-a-report-server.md)  
 Explains how encryption keys are created.  
  
 [Back Up and Restore Reporting Services Encryption Keys](ssrs-encryption-keys-back-up-and-restore-encryption-keys.md)  
 Explains how to back up encryption keys and restore them to recover or migrate a report server installation.  
  
 [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-store-encrypted-report-server-data.md)  
 Describes encryption on a report server.  
  
 [Delete and Re-create Encryption Keys  &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-delete-and-re-create-encryption-keys.md)  
 Explains how you can replace a symmetric key with a new version, and how to start over if symmetric keys cannot be validated.  
  
 [Add and Remove Encryption Keys for Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](add-and-remove-encryption-keys-for-scale-out-deployment.md)  
 Explains how to add and remove encryption keys to control which report servers are part of a scale-out deployment.  
  
## See Also  
 [Store Encrypted Report Server Data &#40;SSRS Configuration Manager&#41;](ssrs-encryption-keys-store-encrypted-report-server-data.md)  
  
  
