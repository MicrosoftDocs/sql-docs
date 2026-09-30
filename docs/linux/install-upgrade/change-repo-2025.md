---
title: Configure Repositories for Installing and Upgrading SQL Server 2025 on Linux
description: Check and configure source repositories for SQL Server on Linux. The source repository affects the version of SQL Server that is applied during installation and upgrade.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: upgrade-and-migration-article
ms.custom:
  - linux-related-content
  - build-2025
monikerRange: ">=sql-server-linux-2017 || >=sql-server-2017"
---
# Configure repositories for installing and upgrading SQL Server 2025 on Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

This article describes how to configure the correct repository for installing and upgrading [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Red Hat Enterprise Linux (RHEL) and Ubuntu.

For instructions on how to configure repositories for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, see [Configure Repositories for Installing and Upgrading SQL Server on Linux](change-repo.md?view=sql-server-ver16&preserve-view=true).

> [!TIP]  
> RHEL 10 and Ubuntu 24.04 are supported starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1. For more information, see the [installation guide](setup.md).

## Repositories

When you install SQL Server on Linux, you must configure a Microsoft repository. Use this repository to get the database engine package, `mssql-server`, and related SQL Server packages. The following repositories are currently available:

| Repository | Name | Description |
| --- | --- | --- |
| **2025** | `mssql-server-2025` | [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] repository. |
| **2022** | `mssql-server-2022` | [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] repository. |
| **2019** | `mssql-server-2019` | [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] Cumulative Update (CU) repository. |
| **2017** | `mssql-server-2017` | [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update (CU) repository. |

The Cumulative Update (CU) repository contains packages for the base SQL Server release, and any bug fixes or improvements since that release. Cumulative updates are specific to a release version, such as [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. They're released on a regular cadence. General distribution release (GDR) updates are released in the same CU repository.

Each release contains the full SQL Server package and all previous updates for that repository. You can also [downgrade](setup.md#rollback) to any release within your major version (for example, 2025).

## Configure repositories

Use the steps in the following sections to configure repositories on your Linux distribution.

## Check for previously configured repositories

First, verify whether you already registered a SQL Server repository.

### [RHEL](#tab/rhel)

1. View the files in the `/etc/yum.repos.d` directory using the following command:

   ```bash
   sudo ls /etc/yum.repos.d
   ```

1. Look for a file that configures the SQL Server repository, such as `mssql-server.repo`.

1. Display the contents of the file using `cat`.

   ```bash
   sudo cat /etc/yum.repos.d/mssql-server.repo
   ```

1. The **name** property is the configured repository. You can identify it using the table in the [Repositories](#repositories) section of this article.

### [Ubuntu](#tab/ubuntu)

1. View the contents of the `/etc/apt/sources.list` file.

   ```bash
   sudo cat /etc/apt/sources.list
   ```

1. Examine the package URL for mssql-server. You can identify it using the table in the [Repositories](#repositories) section of this article.

---

## Remove old repository

If necessary, remove the old repository using the following command.

### [RHEL](#tab/rhel)

```bash
sudo rm -rf /etc/yum.repos.d/mssql-server.repo
```

This command assumes that the file identified in the previous section is named `mssql-server.repo`.

### [Ubuntu](#tab/ubuntu)

Use one of the following commands based on the type of previously configured repository.

| Repository | Command to remove |
| --- | --- |
| **2025** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/22.04/mssql-server-2025 noble main'` |
| **2022** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022 jammy main'` |
| **2019 CU** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019 focal main'` |
| **2017 CU** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/18.04/mssql-server-2017 bionic main'` |

These commands point to the latest repository for a specific distribution. If you use an earlier distribution where that version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is still supported, change the path accordingly, and use the correct [distribution code name](https://releases.ubuntu.com/).

---

## Configure new repository

Configure the new repository to use for SQL Server installations and upgrades. Use one of the following commands to configure the repository of your choice.

### [RHEL](#tab/rhel)

- Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1, RHEL 10 is supported.
- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 10, RHEL 9 is supported.
- Starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 20, RHEL 8 is supported.

The following commands for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] point to the RHEL 8 repository. RHEL 8 doesn't come preinstalled with `python2`, which [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires. For more information, see [Installing Microsoft SQL Server on Red Hat Enterprise Linux 8 Beta](https://www.redhat.com/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta).

Depending on the version of RHEL you use, ensure the paths match `/rhel/8`, `/rhel/9`, or `/rhel/10`. Our packages are agnostic to RHEL minor versions. This means that if you use RHEL 8.7, you need to use the path `/rhel/8` to configure your repository.

| Repository | Version | Release | Command |
| --- | --- | --- |
| **2025** | 2025 | RHEL 10 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/10/mssql-server-2025.repo` |
| **2022** | 2022 | RHEL 9 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/9/mssql-server-2022.repo` |
| **2019 CU** | 2019 | RHEL 8 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo` |
| **2017 CU** | 2017 | RHEL 8 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017.repo` |

### [Ubuntu](#tab/ubuntu)

Configure the new repository for SQL Server installations and upgrades.

- Starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1, Ubuntu 24.04 is supported.
- Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] CU 10, Ubuntu 22.04 is supported.
- Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 10, Ubuntu 20.04 is supported.
- Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 3 and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 20, Ubuntu 18.04 is supported.

The following commands point to the latest repository for a specific distribution. If you use an earlier distribution where that version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is still supported, change the path accordingly, and use the correct [distribution code name](https://releases.ubuntu.com/).

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Use one of the following commands to configure the repository of your choice.

   | Repository | Version | Command |
   | --- | --- | --- |
   | **2025** | 2025 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2025.list)"` |
   | **2022** | 2022 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list)"` |
   | **2019 CU** | 2019 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"` |
   | **2017 CU** | 2017 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"` |

1. Run `apt-get update`.

   ```bash
   sudo apt-get update
   ```

---

If you choose to use a quickstart article, remember that you already configured the target repository. Don't repeat that step in the tutorial.

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](quickstart-install-red-hat.md)
- [Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server](quickstart-install-suse.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-ubuntu.md)
- [Installation guidance for SQL Server on Linux](setup.md)
