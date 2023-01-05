---
title: What's New for SQL Server 2019 on Linux
description: This article highlights what's new for SQL Server 2019 on Linux.
author: VanMSFT
ms.author: vanto
ms.date: 11/22/2021
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.custom:
  - intro-whats-new
---

# What's new for SQL Server 2019 on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes the major features and services available for SQL Server 2019 running on Linux. For package downloads and known issues, see the [Release notes](sql-server-linux-release-notes-2019.md).

## SLES 15 supported

Starting with SQL Server 2019 CU14, SLES 15 is now supported. Check out our Quickstart on [Installing SQL Server and creating a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md).

## Ubuntu 18.04 and 20.04 supported

- Starting with SQL Server 2019 CU3, Ubuntu 18.04 is now supported. 
- Starting with SQL Server 2019 CU10, Ubuntu 20.04 is now supported. 
- Check out our Quickstart on [Installing SQL Server and creating a database on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true).

## RHEL 8 supported

Starting with SQL Server 2019 CU1, RHEL 8 is now supported. Check out our Quickstart on [Installing SQL Server and creating a database on Red Hat](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true).

## Updates

The updates have been made in SQL Server 2019 on Linux:

| New feature or update | Details |
|:-----|:-----|
|Replication support |[SQL Server Replication on Linux](sql-server-linux-replication.md)
|Support for the Microsoft Distributed Transaction Coordinator (MSDTC) |[How to configure MSDTC on Linux](sql-server-linux-configure-msdtc.md) |
|OpenLDAP support for third-party Active Directory providers |[Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md) |
|Machine Learning on Linux |[Configure Machine Learning on Linux](sql-server-linux-setup-machine-learning.md) |
|`tempdb` improvements | By default, a new installation of SQL Server on Linux creates multiple `tempdb` data files based on the number of logical cores (with up to 8 data files). This does not apply to in-place minor or major version upgrades. Each `tempdb` file is 8 MB with an auto growth of 64 MB. This behavior is similar to the default SQL Server installation on Windows. |
| PolyBase on Linux | [Install PolyBase](../relational-databases/polybase/polybase-linux-setup.md) on Linux for non-Hadoop connectors.<br/><br/>[PolyBase type mapping](../relational-databases/polybase/polybase-type-mapping.md). |
| Change Data Capture (CDC) support | Change Data Capture (CDC) is now supported on Linux for SQL Server 2019. |
| Microsoft Container Registry | The [Microsoft Container Registry](https://azure.microsoft.com/blog/microsoft-syndicates-container-catalog/) now replaces Docker Hub for new official Microsoft container images, including [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)]. |
| Non-root containers | [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)] introduces the ability to create safer containers by starting the [!INCLUDE[sql-server](../includes/ssnoversion-md.md)] process as a non-root user by default. See [build and run SQL Server containers as a non-root user](./sql-server-linux-docker-container-security.md#buildnonrootcontainer) for more details. |

## Next steps

To install SQL Server on Linux, use one of the following tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md?view=sql-server-linux-ver15&preserve-view=true)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true)
- [Run on Docker](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true)
- [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.yml). To see other improvements introduced in SQL Server 2019, see [What's New in SQL Server 2019](../sql-server/what-s-new-in-sql-server-2019.md?preserve-view=true&view=sql-server-ver15).

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
