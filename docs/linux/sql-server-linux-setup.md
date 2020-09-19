---
title: Installation guidance for SQL Server on Linux
titleSuffix: SQL Server
description: Install, update, and uninstall SQL Server on Linux. This article covers online, offline, and unattended scenarios. 
author: VanMSFT 
ms.author: vanto
ms.date: 06/22/2020
ms.topic: conceptual
ms.prod: sql
ms.custom: "sqlfreshmay19"
ms.technology: linux
ms.assetid: 565156c3-7256-4e63-aaf0-884522ef2a52
---
# Installation guidance for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article provides guidance for installing, updating, and uninstalling SQL Server 2017 and SQL Server 2019 on Linux.

For other deployment scenarios, see:

- [Windows](../database-engine/install-windows/install-sql-server.md)
- [Docker containers](../linux/sql-server-linux-configure-docker.md)
- [Kubernetes - Big Data Clusters](../big-data-cluster/deploy-get-started.md)

> [!TIP]
> This guide covers several deployment scenarios. If you are only looking for step-by-step installation instructions, jump to one of the quickstarts:
> - [RHEL quickstart](quickstart-install-connect-red-hat.md)
> - [SLES quickstart](quickstart-install-connect-suse.md)
> - [Ubuntu quickstart](quickstart-install-connect-ubuntu.md)
> - [Docker quickstart](quickstart-install-connect-docker.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](../linux/sql-server-linux-faq.md).

## <a id="supportedplatforms"></a> Supported platforms

SQL Server is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It is also supported as a Docker image, which can run on Docker Engine on Linux or Docker for Windows/Mac.

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

| Platform | Supported version(s) | Get
|-----|-----|-----
| **Red Hat Enterprise Linux** | 7.3, 7.4, 7.5, 7.6, 8  | [Get RHEL 7.6](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation)
| **SUSE Linux Enterprise Server** | v12 SP2 | [Get SLES v12 SP2](https://www.suse.com/products/server)
| **Ubuntu** | 16.04 | [Get Ubuntu 16.04](http://releases.ubuntu.com/xenial/)
| **Docker Engine** | 1.8+ | [Get Docker](https://www.docker.com/get-started)

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15 || =sqlallproducts-allversions"

[!INCLUDE [linux-supported-platfoms-2019](../includes/linux-supported-platfoms-2019.md)]

::: moniker-end

Microsoft also supports deploying and managing SQL Server containers by using OpenShift and Kubernetes.

> [!NOTE]
> SQL Server is tested and supported on Linux for the previously listed distributions. If you choose to install SQL Server on an unsupported operating system, please review the **Support policy** section of the [Technical support policy for Microsoft SQL Server](https://support.microsoft.com/help/4047326/support-policy-for-microsoft-sql-server) to understand the support implications.

## <a id="system"></a> System requirements

SQL Server has the following system requirements for Linux:

|||
|-----|-----|
| **Memory** | 2 GB |
| **File System** | **XFS** or **EXT4** (other file systems, such as **BTRFS**, are unsupported) |
| **Disk space** | 6 GB |
| **Processor speed** | 2 GHz |
| **Processor cores** | 2 cores |
| **Processor type** | x64-compatible only |

If you use **Network File System (NFS)** remote shares in production, note the following support requirements:

- Use NFS version **4.2 or higher**. Older versions of NFS do not support required features, such as fallocate and sparse file creation, common to modern file systems.
- Locate only the **/var/opt/mssql** directories on the NFS mount. Other files, such as the SQL Server system binaries, are not supported.
- Ensure that NFS clients use the 'nolock' option when mounting the remote share.

## <a id="repositories"></a> Configure source repositories

When you install or upgrade SQL Server, you get the latest version of SQL Server from your configured Microsoft repository. The quickstarts use the Cumulative Update **CU** repository for SQL Server. But you can instead configure a **GDR** repository. For more information on repositories and how to configure them, see [Configure repositories for SQL Server on Linux](sql-server-linux-change-repo.md).

## <a id="platforms"></a> Install SQL Server

You can install SQL Server 2017 or SQL Server 2019 on Linux from the command line. For step-by-step instructions, see one of the following quickstarts:

| Platform | Installation quickstarts |
|---|---|
| Red Hat Enterprise Linux (RHEL) | [2017](quickstart-install-connect-red-hat.md?view=sql-server-2017) \| [2019](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15) |
| SUSE Linux Enterprise Server (SLES) | [2017](quickstart-install-connect-suse.md?view=sql-server-2017) \| [2019](quickstart-install-connect-suse.md?view=sql-server-linux-ver15) |
| Ubuntu | [2017](quickstart-install-connect-ubuntu.md?view=sql-server-2017) \| [2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15) |
| Docker | [2017](quickstart-install-connect-docker.md?view=sql-server-2017) \| [2019](quickstart-install-connect-docker.md?view=sql-server-linux-ver15) |

You can also run SQL Server on Linux in an Azure virtual machine. For more information, see [Provision a SQL VM in Azure](/azure/virtual-machines/linux/sql/provision-sql-server-linux-virtual-machine?toc=/sql/toc/toc.json).

After installing, consider making additional configuration changes for optimal performance. For more information, see [Performance best practices and configuration guidelines for SQL Server on Linux](sql-server-linux-performance-best-practices.md).

## <a id="upgrade"></a> Update or Upgrade SQL Server

To update the **mssql-server** package to the latest release, use one of the following commands based on your platform:

| Platform | Package update command(s) |
|-----|-----|
| RHEL | `sudo yum update mssql-server` |
| SLES | `sudo zypper update mssql-server` |
| Ubuntu | `sudo apt-get update`<br/>`sudo apt-get install mssql-server` |

These commands download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases are not affected by this operation.

To upgrade SQL Server, first [change your configured repository](sql-server-linux-change-repo.md) to the desired version of SQL Server. Then use the same **update** command to upgrade your version of SQL Server. This is only possible if the upgrade path is supported between the two repositories.

## <a id="rollback"></a> Rollback SQL Server

To rollback or downgrade SQL Server to a previous release, use the following steps:

1. Identify the version number for the SQL Server package you want to downgrade to. For a list of package numbers, see the [Release notes](../linux/sql-server-linux-release-notes.md).

1. Downgrade to a previous version of SQL Server. In the following commands, replace `<version_number>` with the SQL Server version number you identified in step one.

   | Platform | Package update command(s) |
   |-----|-----|
   | RHEL | `sudo yum downgrade mssql-server-<version_number>.x86_64` |
   | SLES | `sudo zypper install --oldpackage mssql-server=<version_number>` |
   | Ubuntu | `sudo apt-get install mssql-server=<version_number>`<br/>`sudo systemctl start mssql-server` |

> [!NOTE]
> It is only supported to downgrade to a release within the same major version, such as SQL Server 2019.

## <a id="versioncheck"></a> Check installed SQL Server version

To verify your current version and edition of SQL Server on Linux, use the following procedure:

1. If not already installed, install the [SQL Server command-line tools](sql-server-linux-setup-tools.md).

1. Use **sqlcmd** to run a Transact-SQL command that displays your SQL Server version and edition.

   ```bash
   sqlcmd -S localhost -U SA -Q 'select @@VERSION'
   ```

## <a id="uninstall"></a> Uninstall SQL Server

To remove the **mssql-server** package on Linux, use one of the following commands based on your platform:

| Platform | Package removal command(s) |
|-----|-----|
| RHEL | `sudo yum remove mssql-server` |
| SLES | `sudo zypper remove mssql-server` |
| Ubuntu | `sudo apt-get remove mssql-server` |

Removing the package does not delete the generated database files. If you want to delete the database files, use the following command:

```bash
sudo rm -rf /var/opt/mssql/
```

## <a id="unattended"></a> Unattended install

You can perform an unattended installation in the following way:

- Follow the initial steps in the [quickstarts](#platforms) to register the repositories and install SQL Server.
- When you run `mssql-conf setup`, set [environment variables](sql-server-linux-configure-environment-variables.md) and use the `-n` (no prompt) option.

The following example configures the Developer edition of SQL Server with the **MSSQL_PID** environment variable. It also accepts the EULA (**ACCEPT_EULA**) and sets the SA user password (**MSSQL_SA_PASSWORD**). The `-n` parameter performs an unprompted installation where the configuration values are pulled from the environment variables.

```bash
sudo MSSQL_PID=Developer ACCEPT_EULA=Y MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' /opt/mssql/bin/mssql-conf -n setup
```

You can also create a script that performs other actions. For example, you could install other SQL Server packages.

For a more detailed sample script, see the following examples:

- [Red Hat unattended installation script](sample-unattended-install-redhat.md)
- [SUSE unattended installation script](sample-unattended-install-suse.md)
- [Ubuntu unattended installation script](sample-unattended-install-ubuntu.md)

## <a id="offline"></a> Offline install

If your Linux machine does not have access to the online repositories used in the [quick starts](#platforms), you can download the package files directly. These packages are located in the Microsoft repository, [https://packages.microsoft.com](https://packages.microsoft.com).

> [!TIP]
> If you successfully installed with the steps in the quick starts, you do not need to download or manually install the SQL Server package(s). This section is only for the offline scenario.

1. **Download the database engine package for your platform**. Find package download links in the package details section of the [Release Notes](../linux/sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the database engine package**. Use one of the following commands based on your platform. Replace the package file name in this example with the exact name you downloaded.

   | Platform | Package install command |
   |-----|-----|
   | RHEL | `sudo yum localinstall mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `sudo zypper install mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `sudo dpkg -i mssql-server_versionnumber_amd64.deb` |

    > [!NOTE]
    > You can also install the RPM packages (RHEL and SLES) with the `rpm -ivh` command, but the commands in the previous table automatically install dependencies if available from approved repositories.

1. **Resolve missing dependencies**: You might have missing dependencies at this point. If not, you can skip this step. On Ubuntu, if you have access to approved repositories containing those dependencies, the easiest solution is to use the `apt-get -f install` command. This command also completes the installation of SQL Server. To manually inspect dependencies, use the following commands:

   | Platform | List dependencies command |
   |-----|-----|
   | RHEL | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | SLES | `rpm -qpR mssql-server_versionnumber.x86_64.rpm` |
   | Ubuntu | `dpkg -I mssql-server_versionnumber_amd64.deb` |

   After resolving the missing dependencies, attempt to install the mssql-server package again.

1. **Complete the SQL Server setup**. Use **mssql-conf** to complete the SQL Server setup:

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

## Licensing and pricing

SQL Server is licensed the same for Linux and Windows. For more information about SQL Server licensing and pricing, see [How to license SQL Server](https://www.microsoft.com/sql-server/sql-server-2017-pricing).

## Optional SQL Server features

After installation, you can also install or enable optional SQL Server features.

- [SQL Server command-line tools](sql-server-linux-setup-tools.md)
- [SQL Server Agent](sql-server-linux-setup-sql-agent.md)
- [SQL Server Full Text Search](sql-server-linux-setup-full-text-search.md)
- [Machine Learning Services (R, Python)](sql-server-linux-setup-machine-learning.md)
- [SQL Server Integration Services](sql-server-linux-setup-ssis.md)

[!INCLUDE[Get Help Options](../includes/paragraph-content/get-help-options.md)]

> [!TIP]
> For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.md).
