---
author: MashaMSFT
ms.author: mathoma
ms.date: 09/15/2026
ms.service: sql
ms.topic: include
---
> [!NOTE]
> Self-installed SQL Server instances fail to start when you place `tempdb` on the local temp disk for Azure VM images with uninitialized ephemeral disks, such as the **FXmdsv2**. Deploy a SQL Server image through Azure Marketplace, use a different VM series, or use the [Azure VM ephemeral NVMe storage script](https://github.com/Azure-Samples/azuresandbox/tree/main/extras/scripts/vm-mssql-win/NVMe) to initialize drives before SQL Server starts. To learn more about the issue and see a list of affected VMs, review [SQL Server failures](/troubleshoot/sql/azure-sql/sql-deployment-fails-drive-not-ready).