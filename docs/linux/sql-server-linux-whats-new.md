---
title: What's New for SQL Server 2017 on Linux | Microsoft Docs
description: This article highlights what's new for SQL Server 2017 on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 02/21/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 456b6f31-6b97-4e31-80ab-b40151ec4868
---
# What's new for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article describes the major features and services available for SQL Server 2017 running on Linux.

SQL Server 2019 preview has been released. This article does not cover SQL Server 2019 preview releases. To learn about SQL Server 2019 preview, see [What's new in SQL Server 2019 preview for Linux](../sql-server/what-s-new-in-sql-server-ver15.md?view=sql-server-ver15#sql-server-on-linux).

> [!NOTE]
> In addition to these capabilities in this article, cumulative updates are released at regular intervals after the GA release. These cumulative updates provide many improvements and fixes. For information about the latest CU release, see [https://aka.ms/sql2017cu](https://aka.ms/sql2017cu). For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes.md).

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
- Cross-platform [Visual Studio Code mssql-server extension](sql-server-linux-develop-use-vscode.md).
- Cross-platform script generator, [mssql-scripter](https://github.com/Microsoft/sql-xplat-cli/blob/dev/doc/usage_guide.md).
- Cross-platform Dynamic Management View (DMV) monitor, [DBFS tool](https://github.com/Microsoft/dbfs).

## Next steps

To install SQL Server on Linux, use one of the following tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=%2fsql%2flinux%2ftoc.json)

To see other improvements introduced in SQL Server 2017, see [What's New in SQL Server 2017](../sql-server/what-s-new-in-sql-server-2017.md).

> [!TIP]
> For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.md).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
