---
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 10/29/2025
ms.topic: include
ms.custom:
  - ignite-2023
---

Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] doesn't currently support the following configurations:

- Windows Server 2012 or older versions of Windows Server. They don't have the minimum required versions of TLS to securely authenticate to Azure.
- Windows Server 2012 R2 is supported for DPS because it supports TLS 1.2. Windows Server 2012 R2 doesn't support the telemetry endpoint. Therefore features such as performance dashboard, migration assessment, and others aren't supported.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] running in containers.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] editions: Business Intelligence.
- Private Link connections to the Azure Arc data processing service at the `<region>.arcdataservices.com` endpoint used for inventory and usage upload.
- [!INCLUDE [sql2008-md](../../../includes/sql2008-md.md)], [!INCLUDE [sql2008r2-md](../../../includes/sql2008r2-md.md)], and older versions.
- Installing the Arc agent and [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] extension can't be done as part of sysprep image creation.
- Multiple instances of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] installed on the same host operating system with the same instance name.
- [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] in Azure Virtual Machines.
- An Always On availability group where one or more replicas is on a failover cluster instance.
- SQL Server Reporting Services (SharePoint Mode).
- [DBCC CLONEDATABASE (Transact-SQL)](../../../t-sql/database-console-commands/dbcc-clonedatabase-transact-sql.md) throws error on the default installation of the Azure extension for SQL Server. To run the `DBCC CLONEDATABASE`, the Azure extension must be run in [least privilege mode](../configure-least-privilege.md).
- SQL Server instance names containing a `#` symbol aren't supported. For a complete list of naming rules and restrictions, review [naming rules and restrictions](/azure/azure-resource-manager/management/resource-name-rules).
- Database and availability group names with trailing whitespace on instances using binary collations (BIN/BIN2). These objects are skipped by the extension with a warning. On non-binary collations (the default), trailing whitespace is automatically trimmed and the objects are managed normally.
