---
title: "What's New for SQL Server 2022 on Linux"
description: In this article, learn about the major features and services available for SQL Server 2022 running on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: whats-new
ms.update-cycle: 1825-days
ms.custom:
  - intro-whats-new
  - linux-related-content
  - build-2025
---

# What's new for SQL Server 2022 on Linux

[!INCLUDE [sqlserver2022-linux](../includes/applies-to-version/sqlserver2022-linux.md)]

This article describes the major features and services available for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] running on Linux.

In addition to these capabilities in this article, cumulative updates (CUs) are released at regular intervals. These cumulative updates provide many improvements and fixes. For detailed information about the latest CU release, see [SQL Server 2022 build versions](/troubleshoot/sql/releases/sqlserver-2022/build-versions). For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes-2022.md).

## Custom password policy support

[!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Cumulative Update (CU) 23 provides support for configuring a custom password policy. This feature was backported from [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

For more information, see [Set custom password policy for SQL logins in SQL Server on Linux](security/authentication/custom-password-policy.md).

## Red Hat Enterprise Linux 9 support

Red Hat Enterprise Linux (RHEL) 9 is supported in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] CU 10 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver16&preserve-view=true).

## SUSE Linux Enterprise Server 15 SP4 support

SUSE Linux Enterprise Server (SLES) 15 is supported in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] CU 4 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md?view=sql-server-linux-ver16&preserve-view=true).

## Ubuntu 22.04 support

Ubuntu 22.04 is supported in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] CU 10 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true).

## Updates

The following updates are available in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux:

| New feature or update | Details |
| --- | --- |
| Microsoft Entra Managed Identity | The Microsoft Entra managed identity for SQL Server on Azure Virtual Machines isn't supported on Linux. For more information, see [Improvement: Microsoft Entra managed identity support for backup and restore database operations and for EKM with AKV in SQL Server on Azure VMs](/troubleshoot/sql/releases/sqlserver-2022/microsoft-entra-managed-identity-support-for-backup-restore-database-ekm-akv). |

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](install-upgrade/quickstart-install-red-hat.md?view=sql-server-linux-ver16&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](install-upgrade/quickstart-install-suse.md?view=sql-server-linux-ver16&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on Ubuntu](install-upgrade/quickstart-install-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true)
- [Quickstart: Run SQL Server Linux container images with Docker](install-upgrade/quickstart-install-docker.md?view=sql-server-linux-ver16&preserve-view=true)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [What's new in SQL Server 2022](../sql-server/what-s-new-in-sql-server-2022.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
