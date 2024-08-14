---
title: What's New for SQL Server 2017 on Linux
description: In this article, learn about the major features and services available for SQL Server 2017 running on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-whats-new
  - linux-related-content
---

# What's new for SQL Server 2017 on Linux

[!INCLUDE [sqlserver2017-linux](../includes/applies-to-version/sqlserver2017-linux.md)]

This article describes the major features and services available for [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] running on Linux.

In addition to these capabilities in this article, cumulative updates (CUs) are released at regular intervals. These cumulative updates provide many improvements and fixes. For detailed information about the latest CU release, see <https://aka.ms/sql2017cu>. For package downloads and known issues, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

## Ubuntu 18.04 support

Ubuntu 18.04 is now supported in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 20 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md).

## Red Hat Enterprise Linux 8 support

Red Hat Enterprise Linux (RHEL) 8 is now supported in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] CU 20 and later versions. For more information, see [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md).

## SQL Server Database Engine

- Enabled the core [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Database Engine capabilities.
- Support for native Linux paths.
- IPV6 support.
- Support for database files on NFS.
- Enabled [Transport Layer Security](sql-server-linux-encrypted-connections.md) (TLS) encryption.
- Enabled [Active Directory Authentication](sql-server-linux-active-directory-authentication.md).
- [Availability Groups functionality](sql-server-linux-availability-group-overview.md) for high availability.
- [Full-text Search](sql-server-linux-setup-full-text-search.md) support.

## SQL Server Agent

- Enabled [SQL Server Agent](sql-server-linux-setup-sql-agent.md) support for the following tasks:
  - [Transact-SQL jobs](sql-server-linux-run-sql-server-agent-job.md)
  - [DB mail](sql-server-linux-db-mail-sql-agent.md)
  - [Log shipping](sql-server-linux-use-log-shipping.md)

## SQL Server Integration Services (SSIS)

- Ability to run SSIS packages on Linux. For more information, see [Configure SQL Server Integration Services on Linux with ssis-conf](sql-server-linux-configure-ssis.md).

## Other improvements

- Command-line configuration tool, [mssql-conf](sql-server-linux-configure-mssql-conf.md).
- Unattended installation support with [environment variables](sql-server-linux-configure-environment-variables.md).
- Cross-platform [SQL Server extension for Visual Studio Code](../tools/visual-studio-code/sql-server-develop-use-vscode.md).
- Cross-platform script generator, [mssql-scripter](https://github.com/Microsoft/sql-xplat-cli/blob/dev/doc/usage_guide.md).
- Cross-platform Dynamic Management View (DMV) monitor, [DBFS tool](https://github.com/Microsoft/dbfs).

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-connect-docker.md)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)
- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [What's new in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
