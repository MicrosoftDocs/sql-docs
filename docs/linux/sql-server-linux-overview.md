---
title: Overview of SQL Server on Linux
description: This article describes how SQL Server runs on Linux and provides information on how to learn more.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/29/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

::: moniker range="= sql-server-2017 || = sql-server-linux-2017"
Starting with [!INCLUDE [sssql17-md](../includes/sssql17-md.md)], SQL Server runs on Linux. It's the same SQL Server database engine, with many similar features and services regardless of your operating system.

> [!TIP]  
> [SQL Server 2019](sql-server-linux-overview.md?view=sql-server-ver15&preserve-view=true) is available! To find out what's new for Linux in the latest release, see [What's new in SQL Server 2019 for Linux](sql-server-linux-whats-new-2019.md?view=sql-server-ver15&preserve-view=true).
::: moniker-end

::: moniker range="= sql-server-ver15 || = sql-server-linux-ver15"
[!INCLUDE [sssql19-md](../includes/sssql19-md.md)] runs on Linux. It's the same SQL Server database engine, with many similar features and services regardless of your operating system. To find out more about this release, see [What's new in SQL Server 2019 for Linux](sql-server-linux-whats-new-2019.md).

> [!TIP]  
> [SQL Server 2022](sql-server-linux-overview.md?view=sql-server-ver16&preserve-view=true) is available! To find out what's new for Linux in the latest release, see [What's new in SQL Server 2022](../sql-server/what-s-new-in-sql-server-2022.md).
::: moniker-end

::: moniker range=">= sql-server-ver16 || >= sql-server-linux-ver16"
[!INCLUDE [sssql22-md](../includes/sssql22-md.md)] runs on Linux. It's the same SQL Server database engine, with many similar features and services regardless of your operating system. To find out more about this release, see [What's new in SQL Server 2022](../sql-server/what-s-new-in-sql-server-2022.md).

::: moniker-end

## Install

To get started, install SQL Server on Linux using one of the following quickstarts:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Install containers for SQL Server on Linux](quickstart-install-connect-docker.md)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)

### Container images

The SQL Server container images are published and available on the Microsoft Container Registry (MCR), and also cataloged at the following locations, based on the operating system image that was used when creating the container image:

- For RHEL-based SQL Server container images, see [SQL Server Red Hat Containers](https://catalog.redhat.com/software/containers/mssql/rhel/server/61f2f612f385723914ed60bc).
- For Ubuntu-based SQL Server images, see [SQL Server on Docker Hub](https://hub.docker.com/_/microsoft-mssql-server).

> [!NOTE]  
> Containers will only be published to MCR for the *most recent* Linux distributions. If you create your own custom SQL Server container image for an older supported distribution, it will still be supported. For more information, see [Upcoming updates to SQL Server container images on Microsoft Artifact Registry aka (MCR)](https://techcommunity.microsoft.com/t5/sql-server-blog/upcoming-updates-to-sql-server-container-images-on-microsoft/ba-p/3573013).

## Connect

After installation, connect to the SQL Server instance on your Linux machine. You can connect locally or remotely and with various tools and drivers. The quickstarts demonstrate how to use the [sqlcmd](sql-server-linux-setup-tools.md) command-line tool. Other tools include the following:

| Tool | Tutorial |
| --- | --- |
| Visual Studio Code (VS Code) | [Use VS Code with SQL Server on Linux](../tools/visual-studio-code/sql-server-develop-use-vscode.md) |
| SQL Server Management Studio (SSMS) | [Use SSMS on Windows to connect to SQL Server on Linux](sql-server-linux-manage-ssms.md) |
| SQL Server Data Tools (SSDT) | [Use SSDT with SQL Server on Linux](sql-server-linux-develop-use-ssdt.md) |

## Explore

Starting with [!INCLUDE [sssql17-md](../includes/sssql17-md.md)], [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] has the same underlying database engine on all supported platforms, including Linux and containers. Therefore, many existing features and capabilities operate the same way. This area of the documentation exposes some of these features from a Linux perspective. It also calls out areas that have unique requirements on Linux.

If you're already familiar with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, review the release notes for general guidelines and known issues for this release:

- [SQL Server 2017 release notes](sql-server-linux-release-notes-2017.md)
- [SQL Server 2019 release notes](sql-server-linux-release-notes-2019.md)
- [SQL Server 2022 release notes](sql-server-linux-release-notes-2022.md)

Then look at what's new:

- [What's new for SQL Server 2017](sql-server-linux-whats-new.md)
- [What's new for SQL Server 2019 on Linux](../sql-server/what-s-new-in-sql-server-2019.md#sql-server-on-linux)
- [What's new in SQL Server 2022](../sql-server/what-s-new-in-sql-server-2022.md)

> [!TIP]  
> For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.yml).

[!INCLUDE[Get Help Options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
