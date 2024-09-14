---
title: Installation guidance for SQL Server on Linux
description: Install, update, and uninstall SQL Server on Linux. This article covers online, offline, and unattended scenarios.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-installation
  - linux-related-content
---
# Installation guidance for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides guidance for installing, updating, and uninstalling [!INCLUDE [sssql17-md](../includes/sssql17-md.md)], [!INCLUDE [sssql19-md](../includes/sssql19-md.md)], and [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux.

For other deployment scenarios, see:

- [Windows](../database-engine/install-windows/install-sql-server.md)
- [Linux containers](sql-server-linux-docker-container-deployment.md)
- [Kubernetes - Big Data Clusters](../big-data-cluster/deploy-get-started.md) ([!INCLUDE [sssql19-md](../includes/sssql19-md.md)] only)

This guide covers several deployment scenarios. If you only need step-by-step installation instructions, jump to one of the quickstarts:

- [Quickstart: Install SQL Server and create a database on Red Hat](quickstart-install-connect-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-connect-docker.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.yml).

[!INCLUDE [support-policy](includes/support-policy.md)]

## <a id="supportedplatforms"></a> Supported platforms

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It's also supported as a container image, which can run on Kubernetes, OpenShift, and Docker Engine on Linux.

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

[!INCLUDE [linux-supported-platforms-2017](includes/linux-supported-platforms-2017.md)]

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

[!INCLUDE [linux-supported-platforms-2019](includes/linux-supported-platforms-2019.md)]

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

[!INCLUDE [linux-supported-platforms-2022](includes/linux-supported-platforms-2022.md)]

::: moniker-end

Microsoft also supports deploying and managing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers by using OpenShift and Kubernetes.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is tested and supported on Linux for the previously listed distributions. If you choose to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on an unsupported operating system, please review the **Support policy** section of the [Technical support policy for Microsoft SQL Server](/troubleshoot/sql/general/support-policy-sql-server) to understand the support implications.

## <a id="system"></a> System requirements

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] has the following system requirements for Linux:

| | Requirement |
| --- | --- |
| **Memory** | 2 GB <sup>1</sup> |
| **File System** | **XFS** or **EXT4** (other file systems, such as **BTRFS**, aren't supported) |
| **Disk space** | 6 GB |
| **Processor speed** | 2 GHz |
| **Processor cores** | 2 cores |
| **Processor type** | x64-compatible only |

<sup>1</sup> 2 GB is the minimum required memory to start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, which accommodates system threads and internal processes. You must take this amount into consideration when setting **[max server memory](../database-engine/configure-windows/server-memory-server-configuration-options.md#max-server-memory)** and **[MemoryLimitMB](sql-server-linux-configure-mssql-conf.md#memorylimit)**.

If you use **Network File System (NFS)** remote shares in production, note the following support requirements:

- Use NFS version **4.2 or higher**. Older versions of NFS don't support required features, such as `fallocate` and sparse file creation, common to modern file systems.
- Locate only the `/var/opt/mssql` directories on the NFS mount. Other files, such as the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] system binaries, aren't supported.

## <a id="repositories"></a> Configure source repositories

When you install or upgrade [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you get the latest version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] from your configured Microsoft repository. The quickstarts use the Cumulative Update **CU** repository for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For more information on repositories and how to configure them, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

## <a id="platforms"></a> Install SQL Server

You can install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux from the command line. For step-by-step instructions, see one of the following quickstarts:

| Platform | Installation quickstarts |
| --- | --- |
| Red Hat Enterprise Linux (RHEL) | [2017](quickstart-install-connect-red-hat.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver16&preserve-view=true) |
| SUSE Linux Enterprise Server (SLES) | [2017](quickstart-install-connect-suse.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-connect-suse.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-connect-suse.md?view=sql-server-linux-ver16&preserve-view=true) |
| Ubuntu | [2017](quickstart-install-connect-ubuntu.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true) |
| Docker | [2017](quickstart-install-connect-docker.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-connect-docker.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-connect-docker.md?view=sql-server-linux-ver16&preserve-view=true) |

You can also run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux in an Azure virtual machine. For more information, see [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json).

After installing, consider making extra configuration changes for optimal performance. For more information, see [Performance best practices and configuration guidelines for SQL Server on Linux](sql-server-linux-performance-best-practices.md).

## <a id="upgrade"></a> Update or upgrade SQL Server

To update the `mssql-server` package to the latest release, use one of the following commands based on your platform:

| Platform | Package update commands |
| --- | --- |
| RHEL | `sudo yum update mssql-server` |
| SLES | `sudo zypper update mssql-server` |
| Ubuntu | `sudo apt-get update`<br />`sudo apt-get install mssql-server` |

These commands download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases aren't affected by this operation.

To upgrade [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], first [change your configured repository](sql-server-linux-change-repo.md) to the desired version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Then use the same `update` command to upgrade your version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. This step is only possible if the upgrade path is supported between the two repositories.

## <a id="rollback"></a> Roll back SQL Server

[!INCLUDE [roll-back-sql-server](includes/roll-back-sql-server.md)]

## <a id="versioncheck"></a> Check installed SQL Server version

To verify your current version and edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, use the following procedure:

1. If not already installed, see [Install the SQL Server command-line tools sqlcmd and bcp on Linux](sql-server-linux-setup-tools.md).

1. Use **sqlcmd** to run a Transact-SQL command that displays your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] version and edition.

   ```bash
   sqlcmd -S localhost -U SA -Q 'select @@VERSION'
   ```

## <a id="uninstall"></a> Uninstall SQL Server

To remove the `mssql-server` package on Linux, use one of the following commands based on your platform:

| Platform | Package removal commands |
| --- | --- |
| RHEL | `sudo yum remove mssql-server` |
| SLES | `sudo zypper remove mssql-server` |
| Ubuntu | `sudo apt-get remove mssql-server` |

Removing the package doesn't delete the generated database files. If you want to delete the database files, use the following command:

```bash
sudo rm -rf /var/opt/mssql/
```

## <a id="unattended"></a> Unattended install

You can perform an unattended installation in the following way:

- Follow the initial steps in the [quickstarts](#platforms) to register the repositories and install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].
- When you run `mssql-conf setup`, set [environment variables](sql-server-linux-configure-environment-variables.md) and use the `-n` (no prompt) option.

The following example configures [!INCLUDE [ssdeveloper-md](../includes/ssdeveloper-md.md)] edition with the `MSSQL_PID` environment variable. It also accepts the EULA (`ACCEPT_EULA`) and sets the SA user password (`MSSQL_SA_PASSWORD`). The `-n` parameter performs an unprompted installation where the configuration values are pulled from the environment variables.

```bash
sudo MSSQL_PID=Developer ACCEPT_EULA=Y MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' /opt/mssql/bin/mssql-conf -n setup
```

You can also create a script that performs other actions. For example, you could install other [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] packages.

For a more detailed sample script, see the following examples:

- [Sample: Unattended SQL Server installation script for Red Hat Enterprise Linux](sample-unattended-install-redhat.md)
- [Sample: Unattended SQL Server installation script for SUSE Linux Enterprise Server](sample-unattended-install-suse.md)
- [Sample: Unattended SQL Server installation script for Ubuntu](sample-unattended-install-ubuntu.md)

## <a id="offline"></a> Offline install

If your Linux machine doesn't have access to the online repositories used in the [quick starts](#platforms), you can download the package files directly. These packages are located in the Microsoft repository, at [https://packages.microsoft.com](https://packages.microsoft.com).

> [!TIP]  
> If you successfully installed with the steps in the quick starts, you don't need to download or manually install the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] package(s). This section is only for the offline scenario.

1. **Download the database engine package for your platform**. Find package download links in the package details section of the [Release notes](sql-server-linux-release-notes-2022.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the database engine package**. Use one of the following commands based on your platform. Replace the package file name in this example with the exact name you downloaded.

   | Platform | Package install command |
   | --- | --- |
   | RHEL | `sudo yum localinstall mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `sudo zypper install mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `sudo dpkg -i mssql-server_versionnumber_amd64.deb` |

   > [!NOTE]  
   > You can also install the RPM packages (RHEL and SLES) with the `rpm -ivh` command, but the commands in the previous table automatically install dependencies if available from approved repositories.

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. On Ubuntu, if you have access to approved repositories containing those dependencies, the easiest solution is to use the `apt-get -f install` command. This command also completes the installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. To manually inspect dependencies, use the following commands:

   | Platform | List dependencies command |
   | --- | --- |
   | RHEL | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `dpkg -I mssql-server_versionnumber_amd64.deb` |

   After you resolve the missing dependencies, you can try installing the `mssql-server` package again.

1. **Complete the SQL Server setup**. Use **mssql-conf** to complete the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

## Licensing and pricing

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is licensed the same for Linux and Windows. For more information about [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] licensing and pricing, see [How to license SQL Server](https://www.microsoft.com/sql-server/sql-server-2022-pricing), and [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server).

## Optional SQL Server features

After installation, you can also install or enable optional [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] features.

- [Install the SQL Server command-line tools sqlcmd and bcp on Linux](sql-server-linux-setup-tools.md)
- [Install SQL Server Agent on Linux](sql-server-linux-setup-sql-agent.md)
- [Install SQL Server Full-Text Search on Linux](sql-server-linux-setup-full-text-search.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning.md)
- [Install SQL Server Integration Services (SSIS) on Linux](sql-server-linux-setup-ssis.md)

[!INCLUDE [Get Help Options](../includes/paragraph-content/get-help-options.md)]

## Related content

- [SQL Server on Linux Frequently Asked Questions (FAQ)](sql-server-linux-faq.yml)
