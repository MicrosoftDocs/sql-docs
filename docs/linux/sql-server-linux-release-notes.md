---
title: Release Notes for SQL Server on Linux
description: This article contains the release notes for all supported versions of SQL Server running on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 08/13/2026
ms.service: sql
ms.subservice: linux
ms.topic: release-notes
ms.custom:
  - linux-related-content
  - ignite-2025
---

# Release notes for SQL Server on Linux

The following release notes apply to supported versions of SQL Server running on Linux. This article is separated into tabs for each release. For detailed supportability and known issues, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md). Each release links to a support article describing the changes, in addition to the Linux package downloads.

[!INCLUDE [support-policy](includes/support-policy.md)]

## Supported platforms

### [SQL Server 2025](#tab/sql2025)

[!INCLUDE [linux-supported-platforms-2025](includes/linux-supported-platforms-2025.md)]

### [SQL Server 2022](#tab/sql2022)

[!INCLUDE [linux-supported-platforms-2022](includes/linux-supported-platforms-2022.md)]

### [SQL Server 2019](#tab/sql2019)

[!INCLUDE [linux-supported-platforms-2019](includes/linux-supported-platforms-2019.md)]

### [SQL Server 2017](#tab/sql2017)

[!INCLUDE [linux-supported-platforms-2017](includes/linux-supported-platforms-2017.md)]

---

## Tools

Most existing client tools that target SQL Server can seamlessly target SQL Server running on Linux. Some tools might have a specific version requirement to work well with Linux. For a full list of SQL Server tools, see [SQL tools overview](../tools/overview-sql-tools.md).

## Release and container tag guidance

### [SQL Server 2025](#tab/sql2025)

- Starting with SQL Server 2025, **SUSE Linux Enterprise Server** (SLES) isn't supported.

  Customers using earlier versions of SQL Server on SLES aren't affected, and there are no changes to your support for existing deployments. For more information about version lifecycle policies, see [SQL Server 2022](/lifecycle/products/sql-server-2022), [SQL Server 2019](/lifecycle/products/sql-server-2019), and [SQL Server 2017](/lifecycle/products/sql-server-2017). To upgrade to SQL Server 2025, [back up your databases and restore them](business-continuity/backup-restore/database-backup-restore.md) to a [supported distribution](#supported-platforms).

- Some GDR releases apply only to Windows. These Windows-only GDRs aren't published for Linux, and don't appear in this article.

- Container tags can vary by release. For a list of available tags, see [RHEL](https://mcr.microsoft.com/product/mssql/rhel/server/tags) and [Ubuntu](https://mcr.microsoft.com/product/mssql/server/tags) in the Microsoft Artifact Registry.

### [SQL Server 2022](#tab/sql2022)

- The **mssql-server-is** package isn't supported on SUSE Linux Enterprise Server (SLES). For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

- Some GDR releases apply only to Windows. These Windows-only GDRs aren't published for Linux, and don't appear in this article.

- Container tags can vary by release. For a list of available tags, see [RHEL](https://mcr.microsoft.com/product/mssql/rhel/server/tags) and [Ubuntu](https://mcr.microsoft.com/product/mssql/server/tags) in the Microsoft Artifact Registry.

### [SQL Server 2019](#tab/sql2019)

- The **mssql-server-is** package isn't supported on SUSE Linux Enterprise Server (SLES). For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

- Some GDR releases apply only to Windows. These Windows-only GDRs aren't published for Linux, and don't appear in this article.

- Container tags can vary by release. For a list of available tags, see [RHEL](https://mcr.microsoft.com/product/mssql/rhel/server/tags) and [Ubuntu](https://mcr.microsoft.com/product/mssql/server/tags) in the Microsoft Artifact Registry.

### [SQL Server 2017](#tab/sql2017)

- As of SQL Server 2017 CU 4, SQL Server Agent is no longer installed as a separate package. It's installed with the Database Engine package and must be enabled for use.

- The **mssql-server-is** package isn't supported on SUSE Linux Enterprise Server (SLES). For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md#sql-server-integration-services-ssis).

- Some GDR releases apply only to Windows. These Windows-only GDRs aren't published for Linux, and don't appear in this article.

- Container tags can vary by release. For a list of available tags, see [RHEL](https://mcr.microsoft.com/product/mssql/rhel/server/tags) and [Ubuntu](https://mcr.microsoft.com/product/mssql/server/tags) in the Microsoft Artifact Registry.

---

## Latest releases

The following table shows the most recent release for each supported version of SQL Server on Linux.

| Version | Release | Date | Build | KB article |
| --- | --- | --- | --- | --- |
| SQL Server 2025 | CU 8 | 2026-08-13 | 17.0.4075.5 | [KB5104822](https://support.microsoft.com/help/5104822) |
| SQL Server 2022 | CU 26 | 2026-07-16 | 16.0.4265.3 | [KB5093420](https://support.microsoft.com/help/5093420) |
| SQL Server 2019 | CU 32 GDR | 2026-07-14 | 15.0.4480.2 | [KB5102335](https://support.microsoft.com/help/5102335) |
| SQL Server 2017 | CU 31 GDR | 2026-07-14 | 14.0.3540.1 | [KB5102337](https://support.microsoft.com/help/5102337) |

## Release notes

### [SQL Server 2025](#tab/sql2025)

There are no additional release notes for the latest release of SQL Server 2025.

### [SQL Server 2022](#tab/sql2022)

There are no additional release notes for the latest release of SQL Server 2022.

### [SQL Server 2019](#tab/sql2019)

> [!IMPORTANT]  
> This is the final cumulative update for [!INCLUDE [ssSQL19](../includes/sssql19-md.md)].

### [SQL Server 2017](#tab/sql2017)

The latest GDR release includes the Azure Connect Pack for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)].

> [!IMPORTANT]  
> This is the final cumulative update for [!INCLUDE [ssSQL17](../includes/sssql17-md.md)].

---

<a id="cuinstall"></a>

## How to install updates

### [SQL Server 2025](#tab/sql2025)

When you configure the CU repository (`mssql-server-2025`), you get the latest CU of SQL Server packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](install-upgrade/change-repo-2025.md).

If you update existing SQL Server packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](install-upgrade/setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](install-upgrade/setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](install-upgrade/setup-machine-learning.md)
- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [Install SQL Server Agent on Linux](install-upgrade/setup-sql-agent.md)

### [SQL Server 2022](#tab/sql2022)

When you configure the CU repository (`mssql-server-2022`), you get the latest CU of SQL Server packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](install-upgrade/change-repo-2025.md).

If you update existing SQL Server packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](install-upgrade/setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](install-upgrade/setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](install-upgrade/setup-machine-learning.md)
- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [Install SQL Server Agent on Linux](install-upgrade/setup-sql-agent.md)

### [SQL Server 2019](#tab/sql2019)

When you configure the CU repository (`mssql-server-2019`), you get the latest CU of SQL Server packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](install-upgrade/change-repo-2025.md).

If you update existing SQL Server packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](install-upgrade/setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](install-upgrade/setup-ssis.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](install-upgrade/setup-machine-learning.md)
- [Install PolyBase on Linux](../relational-databases/polybase/polybase-linux-setup.md)
- [Install SQL Server Agent on Linux](install-upgrade/setup-sql-agent.md)

### [SQL Server 2017](#tab/sql2017)

When you configure the CU repository (`mssql-server-2017`), you get the latest CU of SQL Server packages when you perform new installations. If you require Docker container images, see official images for [Microsoft SQL Server on Linux for Docker Engine](https://hub.docker.com/r/microsoft/mssql-server). For more information about repository configuration, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](install-upgrade/change-repo-2025.md).

If you update existing SQL Server packages, run the appropriate update command for each package to get the latest CU. For specific update instructions for each package, see the following installation guides:

- [Install SQL Server package](sql-server-linux-setup.md#upgrade)
- [Install SQL Server Full-Text Search on Linux](install-upgrade/setup-full-text-search.md)
- [Install SQL Server Integration Services (SSIS) on Linux](install-upgrade/setup-ssis.md)
- [Install SQL Server Agent on Linux](install-upgrade/setup-sql-agent.md)

---

## Release history

For the full release history, see [Latest updates and version history for SQL Server on Linux](/troubleshoot/sql/releases/linux/download-and-install-latest-updates-linux).

## Known issues

For more information, see [SQL Server on Linux: Known issues](sql-server-linux-known-issues.md).

## Related content

- [SQL Server on Linux FAQ](sql-server-linux-faq.yml)
- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](install-upgrade/quickstart-install-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](install-upgrade/quickstart-install-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](install-upgrade/quickstart-install-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](install-upgrade/quickstart-install-docker.md)
- [Provision a Linux virtual machine running SQL Server in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart)
- [Quickstart: Run SQL Server in the cloud](install-upgrade/quickstart-install-clouds.md)
