---
title: Extensible Key Management Using Azure Key Vault
description: Use the SQL Server Connector for Extensible Key Management with Azure Key Vault for SQL Server.
author: jaszymas
ms.author: jaszymas
ms.reviewer: vanto, randolphwest
ms.date: 10/06/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - sfi-image-nochange
helpviewer_keywords:
  - "Extensible Key Management with key vault"
  - "Transparent Data Encryption, using EKM and key vault"
  - "EKM, with key vault"
  - "TDE, EKM and key vault"
  - "Key Management with key vault"
  - "SQL Server Connector, about"
---
# Extensible Key Management using Azure Key Vault (SQL Server)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector for Azure Key Vault enables [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption to use the Azure Key Vault service as an [Extensible Key Management (EKM)](extensible-key-management-ekm.md) provider to protect [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption keys.

This article describes the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] connector. More information is available in:

- [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](setup-steps-for-extensible-key-management-using-the-azure-key-vault.md)
- [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md)
- [SQL Server Connector Maintenance & Troubleshooting](sql-server-connector-maintenance-troubleshooting.md)

<a id="Uses"></a>

## What is Extensible Key Management (EKM) and why use it?

[!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] provides several types of encryption that help protect sensitive data, including [Transparent data encryption (TDE)](transparent-data-encryption.md), [Encrypt a Column of Data](encrypt-a-column-of-data.md) (CLE), and [Backup encryption](../../backup-restore/backup-encryption.md). In all of these cases, in this traditional key hierarchy, the data is encrypted using a symmetric data encryption key (DEK). The symmetric data encryption key is further protected by encrypting it with a hierarchy of keys stored in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

Instead of this model, the alternative is the EKM Provider Model. Using the EKM provider architecture enables [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to protect the data encryption keys by using an asymmetric key stored outside of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] in an external cryptographic provider. This model adds an additional layer of security and separates the management of keys and data.

The following image compares the traditional service-manage key hierarchy with the Azure Key Vault system.

:::image type="content" source="media/ekm-key-hierarchy-traditional.png" alt-text="Diagram that compares the traditional service-manage key hierarchy with the Azure Key Vault system." lightbox="media/ekm-key-hierarchy-traditional.png":::

The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector serves as a bridge between [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] and Azure Key Vault, so [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] can use the scalability, high performance, and high availability of the Azure Key Vault service. The following image represents how the key hierarchy works in the EKM provider architecture with Azure Key Vault and [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector.

Azure Key Vault can be used with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] installations on Azure Virtual Machines and for on-premises servers. The key vault service also provides the option to use tightly controlled and monitored Hardware Security Modules (HSMs) for a higher level of protection for asymmetric encryption keys. For more information about the key vault, see [Azure Key Vault](/azure/key-vault/general/basic-concepts).

> [!NOTE]  
> Only Azure Key Vault and Azure Key Vault Managed HSM are supported. Azure Cloud HSM isn't supported.

The following image summarizes the process flow of EKM using the key vault. (The process step numbers in the image aren't meant to match the setup step numbers that follow the image.)

:::image type="content" source="media/ekm-using-azure-key-vault.png" alt-text="Screenshot of SQL Server EKM using the Azure Key Vault." lightbox="media/ekm-using-azure-key-vault.png":::

> [!NOTE]  
> Versions 1.0.0.440 and older are no longer supported in production environments. Upgrade to version 1.0.1.0 or a later version by visiting the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=45344) and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](sql-server-connector-maintenance-troubleshooting.md) page under "Upgrade of SQL Server Connector."

For the next step, see [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).

For use scenarios, see [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md).

## Related content

- [SQL Server Connector maintenance and troubleshooting](sql-server-connector-maintenance-troubleshooting.md)
