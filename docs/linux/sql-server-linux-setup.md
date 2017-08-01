---
# required metadata

title: Install SQL Server 2017 on Linux | Microsoft Docs
description: Install, update, and uninstall SQL Server on Linux. This topic covers online, offline, and unattended scenarios. 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 07/24/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 565156c3-7256-4e63-aaf0-884522ef2a52

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Installation guidance for SQL Server on Linux

This topic explains how to install, update, and uninstall SQL Server 2017 on Linux. SQL Server 2017 RC1 is supported on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. It is also available as a Docker image, which can run on Docker Engine on Linux or Docker for Windows/Mac.

> [!TIP]
> To get started quickly, jump to one of the quick start tutorials for [RHEL](quickstart-install-connect-red-hat.md), [SLES](quickstart-install-connect-suse.md), [Ubuntu](quickstart-install-connect-ubuntu.md), or [Docker](quickstart-install-connect-docker.md).

## <a id="supportedplatforms"></a> Supported platforms

SQL Server 2017 is supported on the following Linux platforms:

| Platform | Supported version(s) | Get
|-----|-----|-----
| **Red Hat Enterprise Linux** | 7.3 | [Get RHEL 7.3](http://access.redhat.com/products/red-hat-enterprise-linux/evaluation)
| **SUSE Linux Enterprise Server** | v12 SP2 | [Get SLES v12 SP2](https://www.suse.com/products/server)
| **Ubuntu** | 16.04 | [Get Ubuntu 16.04](http://www.ubuntu.com/download/server)
| **Docker Engine** | 1.8+ | [Get Docker](http://www.docker.com/products/overview)

## <a id="system"></a> System requirements

SQL Server 2017 has the following system requirements for Linux:

|||
|-----|-----|
| **Memory** | 3.25 GB |
| **File System** | **XFS** or **EXT4** (other file systems, such as **BTRFS**, are unsupported) |
| **Disk space** | 6 GB |
| **Processor speed** | 2 GHz |
| **Processor cores** | 2 cores |
| **Processor type** | x64-compatible only |

> [!NOTE]
> SQL Server Engine has been tested up to 1 TB of memory at this time.

## <a id="platforms"></a> Install SQL Server

You can install SQL Server on Linux from the command-line. For instructions, see one of the following quick start tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)

## <a id="upgrade"></a> Upgrade SQL Server

To upgrade the **mssql-server** package on Linux, use one of the following commands based on your platform:

| Platform | Package update command(s) |
|-----|-----|
| RHEL | `sudo yum update mssql-server` |
| SLES | `sudo zypper update mssql-server` |
| Ubuntu | `sudo apt-get update`<br/>`sudo apt-get install mssql-server` |

These commands download the newest package and replace the binaries located under `/opt/mssql/`. The user generated databases and system databases are not affected by this operation.

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

- Follow the initial steps in the [quick start tutorials](#platforms) to register the repositories and install SQL Server.
- When you run `mssql-conf setup`, set [environment variables](sql-server-linux-configure-environment-variables.md) and use the `-n` (no prompt) option.

The following example configures the Developer edition of SQL Server with the **MSSQL_PID** environment variable. It also sets the SA user password with the **MSSQL_SA_PASSWORD** environment variable. The `-n` parameter pulls in these values for a simple unattended install:

```bash
sudo MSSQL_PID=Developer MSSQL_SA_PASSWORD='<YourStrong!Passw0rd>' /opt/mssql/bin/mssql-conf -n setup
```

You can also create a script that performs other actions. For example, you could install other SQL Server packages.

For a more detailed sample script, see the following examples:

- [Red Hat unattended installation script](sample-unattended-install-redhat.md)
- [SUSE unattended installation script](sample-unattended-install-suse.md)
- [Ubuntu unattended installation script](sample-unattended-install-ubuntu.md)

## <a id="offline"></a> Offline install

If your Linux machine does not have access to the online repositories used in the [quick starts](#platforms), you can download the package files directly. These packages are located in the Microsoft repository, [https://packages.microsoft.com](https://packages.microsoft.com).

> [!TIP]
> If you successfully installed with the steps in the quick starts, you do not need to download or manually install the package(s) below. This section is only for the offline scenario.

1. **Download the database engine package for your platform**. Find package download links in the package details section of the [Release Notes](sql-server-linux-release-notes.md).

1. **Move the downloaded package to your Linux machine**. If you used a different machine to download the packages, one way to move the packages to your Linux machine is with the **scp** command.

1. **Install the database engine package**. Use one of the following commands based on your platform. Replace the package file name in this example with the exact name you downloaded.

   | Platform | Package removal command |
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

## Next steps

After installation, you can also install other optional SQL Server packages.

- [SQL Server command-line tools](sql-server-linux-setup-tools.md)
- [SQL Server Agent](sql-server-linux-setup-sql-agent.md)
- [SQL Server Full Text Search](sql-server-linux-setup-full-text-search.md)
- [SQL Server Integration Services (Ubuntu)](sql-server-linux-setup-ssis.md)

Connect to your SQL Server instance to begin creating and managing databases. To get started, see the quick start tutorials:

- [Install on Red Hat Enterprise Linux](quickstart-install-connect-red-hat.md)
- [Install on SUSE Linux Enterprise Server](quickstart-install-connect-suse.md)
- [Install on Ubuntu](quickstart-install-connect-ubuntu.md)
- [Run on Docker](quickstart-install-connect-ubuntu.md)
