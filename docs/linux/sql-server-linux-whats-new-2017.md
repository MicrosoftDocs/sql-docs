---
title: "What's New for SQL Server 2017 on Linux"
description: In this article, learn about the major features and services available for SQL Server 2017 running on Linux.
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

# What's new for SQL Server 2017 on Linux

[!INCLUDE [sqlserver2017-linux](../includes/applies-to-version/sqlserver2017-linux.md)]

This article describes the major features and services available for [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] running on Linux.

In addition to these capabilities in this article, cumulative updates (CUs) are released at regular intervals. These cumulative updates provide many improvements and fixes. For detailed information about the latest CU release, see [SQL Server 2017 build versions](/troubleshoot/sql/releases/sqlserver-2017/build-versions). For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes-2017.md).

## Red Hat Enterprise Linux 8 support

Red Hat Enterprise Linux (RHEL) 8 is supported in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 20 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md?view=sql-server-linux-2017&preserve-view=true).

## Ubuntu 18.04 support

Ubuntu 18.04 is supported in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 20 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true).

## SQL Server Database Engine

- Enabled the core [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Database Engine capabilities.
- Support for native Linux paths.
- IPv6 support.
- Support for database files on Network File System (NFS).
- Enabled [Transport Layer Security](security/encrypted-connections.md) (TLS) encryption.
- Enabled [Active Directory authentication](security/authentication/active-directory-tutorial.md).
- [Availability groups functionality](business-continuity/availability-groups/overview.md) for high availability.
- [Full-Text Search](install-upgrade/setup-full-text-search.md) support.

## SQL Server Agent

- Enabled [SQL Server Agent](install-upgrade/setup-sql-agent.md) support for the following tasks:
  - [Transact-SQL jobs](sql-server-linux-run-sql-server-agent-job.md)
  - [Database Mail](sql-server-linux-db-mail-sql-agent.md)
  - [Log shipping](business-continuity/use-log-shipping.md)

## SQL Server Integration Services (SSIS)

- Ability to run SSIS packages on Linux. For more information, see [Configure SQL Server Integration Services on Linux with ssis-conf](migrate/configure-ssis.md).

## Other improvements

- Command-line configuration tool, [mssql-conf](configure/mssql-conf.md).
- Unattended installation support with [environment variables](configure/environment-variables.md).
- Cross-platform [MSSQL extension for Visual Studio Code](../tools/visual-studio-code-extensions/mssql/mssql-run-first-query.md).
- Cross-platform script generator, [mssql-scripter](https://github.com/Microsoft/sql-xplat-cli/blob/dev/doc/usage_guide.md).
- Cross-platform Dynamic Management View (DMV) monitor, [DBFS tool](https://github.com/Microsoft/dbfs).

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](install-upgrade/quickstart-install-red-hat.md?view=sql-server-linux-2017&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](install-upgrade/quickstart-install-suse.md?view=sql-server-linux-2017&preserve-view=true)
- [Quickstart: Install SQL Server and create a database on Ubuntu](install-upgrade/quickstart-install-ubuntu.md?view=sql-server-linux-2017&preserve-view=true)
- [Quickstart: Run SQL Server Linux container images with Docker](install-upgrade/quickstart-install-docker.md?view=sql-server-linux-2017&preserve-view=true)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [What's new in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
