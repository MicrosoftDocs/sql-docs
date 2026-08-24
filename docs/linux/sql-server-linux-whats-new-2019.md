---
title: "What's New for SQL Server 2019 on Linux"
description: In this article, learn about the major features and services available for SQL Server 2019 running on Linux.
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
---

# What's new for SQL Server 2019 on Linux

[!INCLUDE [sqlserver2019-linux](../includes/applies-to-version/sqlserver2019-linux.md)]

This article describes the major features and services available for [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] running on Linux.

In addition to these capabilities in this article, cumulative updates (CUs) are released at regular intervals. These cumulative updates provide many improvements and fixes. For detailed information about the latest CU release, see [SQL Server 2019 build versions](/troubleshoot/sql/releases/sqlserver-2019/build-versions). For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes-2019.md).

## Red Hat Enterprise Linux 8 support

Red Hat Enterprise Linux (RHEL) 8 is supported in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 1 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true).

## SUSE Linux Enterprise Server 15 support

SUSE Linux Enterprise Server (SLES) 15 is supported in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 14 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md?view=sql-server-linux-ver15&preserve-view=true).

## Ubuntu 18.04 and 20.04 support

Ubuntu 18.04 is supported in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 3 and later versions.

Ubuntu 20.04 is supported in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] CU 10 and later versions.

For more information, see [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true).

## Updates

The following updates are available in [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on Linux:

| New feature or update | Details |
| --- | --- |
| Replication support | [SQL Server replication on Linux](replication/overview.md) |
| Support for the Microsoft Distributed Transaction Coordinator (MSDTC) | [How to configure the Microsoft Distributed Transaction Coordinator (MSDTC) on Linux](configure/distributed-transactions.md) |
| OpenLDAP support for third-party Active Directory providers | [Tutorial: Use Active Directory authentication with SQL Server on Linux](security/authentication/active-directory-tutorial.md) |
| Machine Learning on Linux | [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](install-upgrade/setup-machine-learning.md) |
| `tempdb` improvements | By default, a new installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux creates multiple `tempdb` data files based on the number of logical cores (with up to eight data files). This setting doesn't apply to in-place minor or major version upgrades. Each `tempdb` file is 8 MB with an auto growth of 64 MB. This behavior is similar to the default [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] installation on Windows. |
| PolyBase on Linux | [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md) on Linux for non-Hadoop connectors.<br /><br />[Type mapping with PolyBase](../relational-databases/polybase/polybase-type-mapping.md). |
| Change Data Capture (CDC) support | Change Data Capture (CDC) is supported on Linux for [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]. |
| Microsoft Container Registry | The [Microsoft Container Registry](https://azure.microsoft.com/blog/microsoft-syndicates-container-catalog/) replaces Docker Hub for official Microsoft container images, including [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| Non-root containers | [!INCLUDE [sql-server-2019](../includes/sssql19-md.md)] introduces the ability to create safer containers by starting the [!INCLUDE [sql-server](../includes/ssnoversion-md.md)] process as a non-root user by default. For more information, see [Build and run SQL Server containers as a non-root user](sql-server-linux-docker-container-security.md#buildnonrootcontainer). |

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](install-upgrade/quickstart-install-red-hat.md?view=sql-server-linux-ver15&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](install-upgrade/quickstart-install-suse.md?view=sql-server-linux-ver15&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on Ubuntu](install-upgrade/quickstart-install-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true)
- [Quickstart: Run SQL Server Linux container images with Docker](install-upgrade/quickstart-install-docker.md?view=sql-server-linux-ver15&preserve-view=true)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [What's new in SQL Server 2019](../sql-server/what-s-new-in-sql-server-2019.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
