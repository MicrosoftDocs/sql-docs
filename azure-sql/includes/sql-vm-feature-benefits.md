---
title: Feature benefits for the SQL Server on Azure VM
description: Feature benefits unlocked by registering your SQL Server on Azure VM with the SQL IaaS Agent extension
author: MashaMSFT
ms.author: mathoma
ms.topic: include
---

| Feature | Description |
| --- | --- |
| **Azure portal management** | Unlocks [management in the portal](../virtual-machines/windows/manage-sql-vm-portal.md), so that you can view all of your SQL Server VMs in one place, and enable or disable SQL specific features directly from the portal. <br /> <br /> Included with basic registration.  |  
| **Automated backup** |Automates the scheduling of backups for all databases for either the default instance or a [properly installed named instance](../virtual-machines/windows/frequently-asked-questions-faq.yml#can-i-use-a-named-instance-of-sql-server-with-the-iaas-extension-) of SQL Server on the VM. For more information, see [Automated backup for SQL Server in Azure virtual machines (Resource Manager)](../virtual-machines/windows/automated-backup-sql-2014.md). <br /> <br /> Requires SQL IaaS Agent extension. |
| **Automated patching** |Configures a maintenance window during which important Windows and SQL Server security updates to your VM can take place, so you can avoid updates during peak times for your workload. For more information, see [Automated patching for SQL Server in Azure virtual machines (Resource Manager)](../virtual-machines/windows/automated-patching.md). <br /> <br /> Requires SQL IaaS Agent extension. |
| **Azure Key Vault integration** |Enables you to automatically install and configure Azure Key Vault on your SQL Server VM. For more information, see [Configure Azure Key Vault integration for SQL Server on Azure Virtual Machines (Resource Manager)](../virtual-machines/windows/azure-key-vault-integration-configure.md). <br /> <br /> Requires SQL IaaS Agent extension. |
| **Azure Update Manager integration** | Since Automated Patching doesn't install Cumulative Updates for SQL Server, you can replace it by integrating [Azure Update Manager](../virtual-machines/azure-update-manager-sql-vm.md) into your [SQL virtual machines](../virtual-machines/windows/manage-sql-vm-portal.md) resource, which adds improved patching functionality for your SQL Server VMs.  <br /> <br /> Requires SQL IaaS Agent extension.  | 
| **Configure tempdb** | You can [configure your tempdb](../virtual-machines/windows/manage-sql-vm-portal.md#storage-configuration) directly from the Azure portal, such as specifying the number of files, their initial size, their location, and the autogrowth ratio. Restart your SQL Server service for the changes to take effect. <br /> <br /> Requires SQL IaaS Agent extension.  | 
| **Defender for Cloud portal integration** | If you've enabled [Microsoft Defender for SQL](/azure/defender-for-cloud/defender-for-sql-usage), then you can view Defender for Cloud recommendations directly in the [SQL virtual machines](../virtual-machines/windows/manage-sql-vm-portal.md) resource of the Azure portal. See [Security best practices](../virtual-machines/windows/security-considerations-best-practices.md) to learn more. <br /> <br /> Requires SQL IaaS Agent extension. |
| **Extended security updates** | Automatically receive security updates for your SQL Server on Azure VMs, up to three years after extended [SQL Server lifecycle support](/lifecycle/products/?terms=sql%20server) ends. | 
| **Flexible licensing** | Save on cost by [seamlessly transitioning](../virtual-machines/windows/licensing-model-azure-hybrid-benefit-ahb-change.md) from the bring-your-own-license (also known as the Azure Hybrid Benefit) to the pay-as-you-go licensing model and back again. <br /> <br /> Included with basic registration. | 
| **Flexible version / edition** | If you decide to change the [version](../virtual-machines/windows/change-sql-server-version.md) or [edition](../virtual-machines/windows/change-sql-server-edition.md) of SQL Server, you can update the metadata within the Azure portal without having to redeploy the entire SQL Server VM.  <br /> <br /> Included with basic registration.   | 
| **Microsoft Entra authentication** | Enhance the security of your SQL Server VM by using [Microsoft Entra ID for authentication](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md) to your SQL Server VM. <br /> <br /> Requires SQL IaaS Agent extension. | 
| **SQL best practices assessment** | Enables you to assess the health of your SQL Server VMs by using configuration best practices. For more information, see [SQL best practices assessment](../virtual-machines/windows/sql-assessment-for-sql-vm.md). <br /> <br /> Requires SQL IaaS Agent extension. | 
| **View disk utilization in portal** | Allows you to view a graphical representation of the disk utilization of your SQL data files in the Azure portal. <br /> <br /> Requires SQL IaaS Agent extension. | 


