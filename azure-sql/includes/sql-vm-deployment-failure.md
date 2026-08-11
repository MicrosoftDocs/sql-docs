---
author: MashaMSFT
ms.author: mathoma
ms.date: 10/07/2025
ms.service: sql
ms.topic: include
---
> [!WARNING]
> Placing `tempdb` on the local temp disk isn't supported for Azure VM images with uninitialized ephemeral disks, such as the **FXmdsv2**. This issue only affects Azure Virtual Machines with the new NVMe interface that also has local ephemeral storage. These deployments through the Azure portal might fail, and SQL Server can fail to start. Either use a different VM series, or place `tempdb` on non-ephemeral storage both when you deploy the SQL Server image through the Azure portal, and when you install SQL Server manually. To learn more about the issue and see a list of impacted VMs, review [VM deployment and SQL Server failures](/troubleshoot/sql/azure-sql/sql-deployment-fails-drive-not-ready).