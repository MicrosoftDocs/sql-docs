---
title: Integrate Key Vault with SQL Server on Windows VMs in Azure (Resource Manager)
description: Learn how to automate the configuration of SQL Server encryption for use with Azure Key Vault. This topic explains how to use Azure Key Vault Integration with SQL virtual machines created with Resource Manager.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma, vanto
ms.date: 09/03/2024
ms.service: virtual-machines-sql
ms.subservice: security
ms.topic: how-to
tags: azure-service-management
---
# Configure Azure Key Vault integration for SQL Server on Azure VMs (Resource Manager)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

There are multiple SQL Server encryption features, such as [transparent data encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption), [column level encryption (CLE)](/sql/t-sql/functions/cryptographic-functions-transact-sql), and [backup encryption](/sql/relational-databases/backup-restore/backup-encryption). These forms of encryption require you to manage and store the cryptographic keys you use for encryption. The Azure Key Vault service is designed to improve the security and management of these keys in a secure and highly available location. The [SQL Server Connector](https://www.microsoft.com/download/details.aspx?id=45344) enables SQL Server to use these keys from Azure Key Vault and [Azure Key Vault Managed Hardware Security Module (HSM)](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault#optional---configure-an-azure-key-vault-managed-hsm-hardware-security-module).

If you are running SQL Server on-premises, there are steps you can follow to [access Azure Key Vault from your on-premises SQL Server instance](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). The same steps apply for SQL Server on Azure VMs, but you can save time by using the *Azure Key Vault Integration* feature.

> [!NOTE]  
> The Azure Key Vault integration is available only for the Enterprise, Developer, and Evaluation Editions of SQL Server. Starting with SQL Server 2019, Standard edition is also supported.

When this feature is enabled, it automatically installs the SQL Server Connector, configures the EKM provider to access Azure Key Vault, and creates the credential to allow you to access your vault. If you looked at the steps in the previously mentioned on-premises documentation, you can see that this feature automates steps 3, 4, and 5 (up to 5.4 to create the credential). Make sure that the key vault has already been created ([step 2](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault#step-2-create-a-key-vault)). From there, the entire setup of your SQL Server VM is automated. Once this feature has completed this setup, you can execute Transact-SQL (T-SQL) statements to begin encrypting your databases or backups as you normally would.

> [!NOTE]  
> You can also configure Key Vault integration by using a template. For more information, see [Azure quickstart template for Azure Key Vault integration](https://github.com/Azure/azure-quickstart-templates/tree/master/quickstarts/microsoft.compute/vm-sql-existing-keyvault-update).
>
> SQL Server Connector version 1.0.4.0 is installed on the SQL Server VM through the [SQL infrastructure as a service (IaaS) extension](sql-server-iaas-agent-extension-automate-management.md). Upgrading the SQL IaaS Agent extension will not update the provider version. Consider manually upgrading the SQL Server Connector version if needed (for example, when using an Azure Key Vault Managed HSM, which needs at least version [15.0.2000.440](https://www.microsoft.com/en-us/download/details.aspx?id=45344)).

## Enable and configure Key Vault integration

You can enable Key Vault integration during provisioning or configure it for existing VMs.

### New VMs

If you are provisioning a new SQL virtual machine with Resource Manager, the Azure portal provides a way to enable Azure Key Vault integration.

:::image type="content" source="media/azure-key-vault-integration-configure/azure-sql-arm-akv.png" alt-text="Screenshot of creating a SQL Server on Azure VM with Azure Key Vault Integration in the Azure portal." lightbox="media/azure-key-vault-integration-configure/azure-sql-arm-akv.png":::

For a detailed walkthrough of provisioning, see [Provision SQL Server on Azure VM (Azure portal)](create-sql-vm-portal.md).

### Existing VMs

For existing SQL virtual machines, open your [SQL virtual machines resource](manage-sql-vm-portal.md#access-the-resource), under **Security**, select **Security Configuration**. Select **Enable** to enable **Azure Key Vault integration**.

The following screenshot shows how to enable Azure Key Vault in the portal for an existing SQL Server on Azure VM:

:::image type="content" source="media/azure-key-vault-integration-configure/azure-sql-rm-akv-existing-vms.png" alt-text="Screenshot of an existing SQL Server on Azure VM Key Vault integration settings in the Azure portal.":::

When you're finished, select the **Apply** button on the bottom of the **Security** page to save your changes.

> [!NOTE]  
> The credential name we created here will be mapped to a SQL login later. This allows the SQL login to access the key vault.

Continue with step 5.5 from [Set up SQL Server TDE Extensible Key Management by using Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault?view=sql-server-ver16&branch=pr-en-us-31589&tabs=portal#step-5-configure-sql-server) to complete the EKM setup.

## Related content

- [Security considerations for SQL Server on Azure Virtual Machines](security-considerations-best-practices.md)
