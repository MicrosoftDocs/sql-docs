---
title: Installation Guidance for SQL Server on Linux
description: Install, update, and uninstall SQL Server on Linux. This article covers online, offline, and unattended scenarios.
author: rwestMSFT
ms.author: randolphwest
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
  - linux-related-content
  - build-2025
  - sfi-ropc-blocked
---
# Installation guidance for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article provides guidance for installing, updating, and uninstalling [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions on Linux.

For other deployment scenarios, see:

- [Windows](../../database-engine/install-windows/install-sql-server.md)
- [Linux containers](../containers/deploy.md)
- [Kubernetes - Big Data Clusters](/previous-versions/sql/big-data-cluster/deploy-get-started) ([!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] only)

This guide covers several deployment scenarios. If you only need step-by-step installation instructions, jump to one of the quickstarts:

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](quickstart-install-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-ubuntu.md)
- [Quickstart: Run SQL Server Linux container images with Docker](quickstart-install-docker.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](../sql-server-linux-faq.yml).

[!INCLUDE [support-policy](../includes/support-policy.md)]

<a id="supportedplatforms"></a>

## Supported platforms

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

[!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It's supported as a container image, which can run on Kubernetes, OpenShift, and Docker Engine on Linux.

[!INCLUDE [linux-supported-platforms-2017](../includes/linux-supported-platforms-2017.md)]

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It's supported as a container image, which can run on Kubernetes, OpenShift, and Docker Engine on Linux.

[!INCLUDE [linux-supported-platforms-2019](../includes/linux-supported-platforms-2019.md)]

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It's supported as a container image, which can run on Kubernetes, OpenShift, and Docker Engine on Linux.

[!INCLUDE [linux-supported-platforms-2022](../includes/linux-supported-platforms-2022.md)]

::: moniker-end

<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] is supported on Red Hat Enterprise Linux (RHEL) and Ubuntu. It's supported as a container image, which can run on Kubernetes, OpenShift, and Docker Engine on Linux.

[!INCLUDE [linux-supported-platforms-2025](../includes/linux-supported-platforms-2025.md)]

::: moniker-end

Microsoft also supports deploying and managing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] containers by using OpenShift and Kubernetes.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is tested and supported on Linux for the previously listed distributions. If you choose to install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on an unsupported operating system, review the **Support policy** section of the [Technical support policy for Microsoft SQL Server](/troubleshoot/sql/general/support-policy-sql-server) to understand the support implications.

<a id="system"></a>

## System requirements

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] has the following system requirements for Linux:

| | Requirement |
| --- | --- |
| **Memory** | 2 GB <sup>1</sup> |
| **File System** | **XFS** or **ext4** (other file systems, such as **BTRFS**, aren't supported) |
| **Disk space** | 6 GB |
| **Processor speed** | 2 GHz |
| **Processor cores** | 2 cores |
| **Processor type** | x64-compatible only |

<sup>1</sup> 2 GB is the minimum required memory to start [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, which accommodates system threads and internal processes. You must take this amount into consideration when setting **[max server memory](../../database-engine/configure-windows/server-memory-server-configuration-options.md#max-server-memory)** and **[MemoryLimitMB](../configure/mssql-conf.md#memorylimit)**.

If you use **Network File System (NFS)** remote shares in production, note the following support requirements:

- Use NFS version **4.2 or higher**. Older versions of NFS don't support required features, such as `fallocate` and sparse file creation, common to modern file systems.
- Locate only the `/var/opt/mssql` directories on the NFS mount. Other files, such as the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] system binaries, aren't supported.

<a id="repositories"></a>

## Configure source repositories

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

When you install or upgrade [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you get the latest version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] from your configured Microsoft repository. The quickstarts use the Cumulative Update (CU) repository for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For more information on repositories and how to configure them, see [Configure repositories for installing and upgrading SQL Server on Linux](change-repo.md).

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

When you install or upgrade [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you get the latest version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] from your configured Microsoft repository. The quickstarts use the Cumulative Update (CU) repository for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. For more information on repositories and how to configure them, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](change-repo-2025.md).

::: moniker-end

<a id="platforms"></a>

## Install SQL Server

You can install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux from the command line. For step-by-step instructions, see one of the following quickstarts:

| Platform | Installation quickstarts |
| --- | --- |
| Red Hat Enterprise Linux (RHEL) | [2017](quickstart-install-red-hat.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-red-hat.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-red-hat.md?view=sql-server-linux-ver16&preserve-view=true) \| [2025](quickstart-install-red-hat.md?view=sql-server-linux-ver17&preserve-view=true) |
| SUSE Linux Enterprise Server (SLES) <sup>1</sup> | [2017](quickstart-install-suse.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-suse.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-suse.md?view=sql-server-linux-ver16&preserve-view=true) |
| Ubuntu | [2017](quickstart-install-ubuntu.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true) \| [2025](quickstart-install-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true) |
| Docker | [2017](quickstart-install-docker.md?view=sql-server-2017&preserve-view=true) \| [2019](quickstart-install-docker.md?view=sql-server-linux-ver15&preserve-view=true) \| [2022](quickstart-install-docker.md?view=sql-server-linux-ver16&preserve-view=true) \| [2025](quickstart-install-docker.md?view=sql-server-linux-ver17&preserve-view=true) |

<sup>1</sup> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

You can also run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux in an Azure virtual machine. For more information, see [Provision a SQL VM in Azure](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart?toc=/sql/toc/toc.json).

After installing, consider making extra configuration changes for optimal performance. For more information, see:

- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../configure/performance-best-practices-sql-server-memory.md)

<a id="upgrade"></a>

## Update or upgrade SQL Server

To update the `mssql-server` package to the latest release, use one of the following commands based on your platform:

| Platform | Package update commands |
| --- | --- |
| RHEL | `sudo yum update mssql-server` |
| SLES | `sudo zypper update mssql-server` |
| Ubuntu | `sudo apt-get update`<br />`sudo apt-get install mssql-server` |

These commands download the newest package and replace the binaries located under `/opt/mssql/`. This operation doesn't affect the user-generated databases or system databases.

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

To upgrade [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], first [change your configured repository](change-repo.md) to the desired version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Then use the same `update` command to upgrade your version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. This step is only possible if the upgrade path is supported between the two repositories.

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

To upgrade [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], first [change your configured repository](change-repo-2025.md) to the desired version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Then use the same `update` command to upgrade your version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. This step is only possible if the upgrade path is supported between the two repositories.

::: moniker-end

<a id="rollback"></a>

## Roll back SQL Server

[!INCLUDE [roll-back-sql-server](../includes/roll-back-sql-server.md)]

<a id="versioncheck"></a>

## Check installed SQL Server version

To verify your current version and edition of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux, use the following procedure:

1. If you don't already have **`sqlcmd`** installed, see [Install the sqlcmd and bcp SQL Server command-line tools on Linux](setup-tools.md).

1. Use **`sqlcmd`** to run a Transact-SQL command that displays your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] version and edition.

   ```bash
   sqlcmd -S localhost -U sa -Q 'select @@VERSION'
   ```

<a id="uninstall"></a>

## Uninstall SQL Server

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

<a id="unattended"></a>

## Unattended install

You can perform an unattended installation in the following way:

- Follow the initial steps in the [quickstarts](#platforms) to register the repositories and install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
- When you run `mssql-conf setup`, set [environment variables](../configure/environment-variables.md) and use the `-n` (no prompt) option.

The following example configures [!INCLUDE [ssdeveloper-md](../../includes/ssdeveloper-md.md)] edition with the `MSSQL_PID` environment variable. It also accepts the EULA (`ACCEPT_EULA`) and sets the `sa` password (`MSSQL_SA_PASSWORD`). The `-n` parameter performs an unprompted installation where the configuration values come from the environment variables.

```bash
sudo MSSQL_PID=Developer ACCEPT_EULA=Y MSSQL_SA_PASSWORD='<password>' /opt/mssql/bin/mssql-conf -n setup
```

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

You can also create a script that performs other actions. For example, you could install other [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] packages.

For a more detailed sample script, see the following examples:

- [Sample: Unattended SQL Server installation script for Red Hat Enterprise Linux](unattended-install-redhat.md)
- [Sample: Unattended SQL Server installation script for SUSE Linux Enterprise Server](unattended-install-suse.md)
- [Sample: Unattended SQL Server installation script for Ubuntu](unattended-install-ubuntu.md)

<a id="offline"></a>

## Offline install

If your Linux machine can't access the online repositories used in the [quickstarts](#platforms), you can download the package files directly. These packages are located at <https://packages.microsoft.com>.

> [!TIP]  
> If you followed a quickstart guide to install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you don't need to download or manually install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] packages. This section is only for the offline scenario.

1. **Download the database engine package for your platform**. Find package download links in the package details section of the [Release information for SQL Server on Linux](../sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the database engine package**. Use one of the following commands based on your platform. Replace the package file name in this example with the exact name you downloaded.

   | Platform | Package install command |
   | --- | --- |
   | RHEL | `sudo yum localinstall mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `sudo zypper install mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `sudo dpkg -i mssql-server_versionnumber_amd64.deb` |

   > [!NOTE]  
   > You can also install the RPM packages (RHEL and SLES) with the `rpm -ivh` command, but the commands in the previous table automatically install dependencies if available from approved repositories.

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. On Ubuntu, if you have access to approved repositories containing those dependencies, the easiest solution is to use the `apt-get -f install` command. This command also completes the installation of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. To manually inspect dependencies, use the following commands:

   | Platform | List dependencies command |
   | --- | --- |
   | RHEL | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `dpkg -I mssql-server_versionnumber_amd64.deb` |

   After you resolve the missing dependencies, you can try installing the `mssql-server` package again.

1. **Complete the SQL Server setup**. Use **`mssql-conf`** to complete the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

<a id="licensing-and-pricing"></a>

## License and pricing

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is licensed the same for Linux and Windows. For more information about [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] licensing and pricing, see [How to license SQL Server](https://www.microsoft.com/sql-server/sql-server-2022-pricing), and [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server).

## Optional SQL Server features

After installation, you can also install or enable optional [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] features.

- [Install the sqlcmd and bcp SQL Server command-line tools on Linux](setup-tools.md)
- [Install SQL Server Agent on Linux](setup-sql-agent.md)
- [Install SQL Server Full-Text Search on Linux](setup-full-text-search.md)
- [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](setup-machine-learning.md)
- [Install SQL Server Integration Services (SSIS) on Linux](setup-ssis.md)

[!INCLUDE [Get Help Options](../../includes/paragraph-content/get-help-options.md)]

## Related content

- [SQL Server on Linux FAQ](../sql-server-linux-faq.yml)
