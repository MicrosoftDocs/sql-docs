---
title: "Extensible Key Management Using Azure Key Vault (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "07/22/2016"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Extensible Key Management with key vault"
  - "Transparent Data Encryption, using EKM and key vault"
  - "EKM, with key vault"
  - "TDE, EKM and key vault"
  - "Key Management with key vault"
  - "SQL Server Connector, about"
ms.assetid: 3efdc48a-8064-4ea6-a828-3fbf758ef97c
author: aliceku
ms.author: aliceku
manager: craigg
---
# Extensible Key Management Using Azure Key Vault (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Azure Key Vault enables [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption to use the Azure Key Vault service as an [Extensible Key Management &#40;EKM&#41;](../../../relational-databases/security/encryption/extensible-key-management-ekm.md) provider to protect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption keys.  
  
 This topic describes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] connector. Additional information is available in [Setup Steps for Extensible Key Management Using the Azure Key Vault](../../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md), [Use SQL Server Connector with SQL Encryption Features](../../../relational-databases/security/encryption/use-sql-server-connector-with-sql-encryption-features.md), and [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md).  
  
##  <a name="Uses"></a> What is Extensible Key Management (EKM) and Why Use it?  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides several types of encryption that help protect sensitive data, including [Transparent Data Encryption &#40;TDE&#41;](../../../relational-databases/security/encryption/transparent-data-encryption.md), [Column Level Encryption](../../../t-sql/functions/cryptographic-functions-transact-sql.md) (CLE), and [Backup Encryption](../../../relational-databases/backup-restore/backup-encryption.md). In all of these cases, in this traditional key hierarchy, the data is encrypted using a symmetric data encryption key (DEK). The symmetric data encryption key is further protected by encrypting it with a hierarchy of keys stored in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Instead of this model, the alternative is the EKM Provider Model. Using the EKM provider architecture enables [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to protect the data encryption keys by using an asymmetric key stored outside of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] in an external cryptographic provider. This model adds an additional layer of security and separates the management of keys and data.  
   
 The following image compares the traditional service-manage key hierarchy with the Azure Key Vault system.  
  
 ![ekm-key-hierarchy-traditional](../../../relational-databases/security/encryption/media/ekm-key-hierarchy-traditional.png "ekm-key-hierarchy-traditional")  
  
   
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector serves as a bridge between [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and Azure Key Vault, so [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can leverage the scalability, high performance, and highly availability of the Azure Key Vault service. The following image represents how the key hierarchy works in the EKM provider architecture with Azure Key Vault and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector.  
  
  Azure Key Vault can be used with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installations on [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Azure Virtual Machines and for on-premises servers. The key vault service also provides the option to use tightly controlled and monitored Hardware Security Modules (HSMs) for a higher level of protection for asymmetric encryption keys. For more information about the key vault, see [Azure Key Vault](https://go.microsoft.com/fwlink/?LinkId=521401).  
  
 The following image summarizes the process flow of EKM using the key vault. (The process step numbers in the image are not meant to match the setup step numbers that follow the image.)  
  
 ![SQL Server EKM using the Azure Key Vault](../../../relational-databases/security/encryption/media/ekm-using-azure-key-vault.png "SQL Server EKM using the Azure Key Vault")  

> [!NOTE]  
>  Versions 1.0.0.440 and older have been replaced and are no longer supported in production environments. Upgrade to version 1.0.1.0 or later by visiting the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=45344) and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md) page under "Upgrade of SQL Server Connector."
  
 For the next step, see [Setup Steps for Extensible Key Management Using the Azure Key Vault](../../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).  
  
 For use scenarios, see [Use SQL Server Connector with SQL Encryption Features](../../../relational-databases/security/encryption/use-sql-server-connector-with-sql-encryption-features.md).  
  
## See Also  
 [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md)  
  
  
