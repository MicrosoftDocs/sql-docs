---
author: MashaMSFT
ms.author: mathoma
ms.date: 08/26/2024
ms.service: virtual-machines-sql
ms.topic: include
---

### Limited extension support

At this time, SQL Server failover cluster instances on Azure virtual machines registered with the [SQL IaaS Agent extension](../virtual-machines/windows/sql-server-iaas-agent-extension-automate-management.md) only support a limited number of features available through basic registration, and not those that require the agent, such as automated backup, patching, Microsoft Entra authentication and advanced portal management. See the [table of benefits](../virtual-machines/windows/sql-server-iaas-agent-extension-automate-management.md#feature-benefits) to learn more. 

If your SQL Server VM has already been registered with the SQL IaaS Agent extension and you've enabled any features that require the agent, you need to [delete the extension](../virtual-machines/windows/sql-agent-extension-manually-register-single-vm.md#delete-the-extension) from the SQL Server VM by deleting the **SQL virtual machine** resource for the corresponding VMs, and then registering it with the SQL IaaS Agent extension again. When you're deleting the **SQL virtual machine** resource by using the Azure portal, clear the check box next to the correct virtual machine to avoid deleting the virtual machine.

