---
title: What's New for SQL Server 2017 on Linux
description: In this article, learn about the major features and services available for SQL Server 2017 running on Linux.
author: VanMSFT
ms.author: vanto
ms.date: 04/10/2020
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.custom:
  - intro-whats-new
---

# What's new for SQL Server 2017 on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes the major features and services available for SQL Server 2017 running on Linux.

> [!NOTE]
> In addition to these capabilities in this article, cumulative updates are released at regular intervals. These cumulative updates provide many improvements and fixes. For detailed information about the latest CU release, see [https://aka.ms/sql2017cu](https://aka.ms/sql2017cu). For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes-2017.md).

## Ubuntu 18.04 supported

Starting with SQL Server 2017 CU20, Ubuntu 18.04 is now supported. Check out our Quickstart on [Installing SQL Server and creating a database on Ubuntu](quickstart-install-connect-ubuntu.md).

## RHEL 8 supported

Starting with SQL Server 2017 CU20, RHEL 8 is now supported. Check out our Quickstart on [Installing SQL Server and creating a database on Red Hat](quickstart-install-connect-red-hat.md).

## SQL Server Database Engine

- Enabled the core SQL Server Database Engine capabilities.
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
- Cross-platform [Visual Studio Code mssql-server extension](../tools/visual-studio-code/sql-server-develop-use-vscode.md).
- Cross-platform script generator, [mssql-scripter](https://github.com/Microsoft/sql-xplat-cli/blob/dev/doc/usage_guide.md).
- Cross-platform Dynamic Management View (DMV) monitor, [DBFS tool](https://github.com/Microsoft/dbfs).

## Next steps

To install SQL Server on Linux, use one of the following tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.yml). To see other improvements introduced in SQL Server 2017, see [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
