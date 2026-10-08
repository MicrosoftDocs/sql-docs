---
title: Configure Repositories for Installing and Upgrading SQL Server 2025 on Linux
description: Check and configure source repositories for SQL Server 2025 on Linux. The source repository affects the version of SQL Server that is applied during installation and upgrade.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2026
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

## Distribution support

- [!INCLUDE [rhel-10](../includes/rhel-10.md)]
- [!INCLUDE [ubuntu-2404](../includes/ubuntu-2404.md)]
- [!INCLUDE [ubuntu-2204](../includes/ubuntu-2204.md)]

For more information, see the [installation guide](setup.md).

## Repositories

When you install SQL Server on Linux, you must configure a Microsoft repository. Use this repository to get the database engine package, `mssql-server`, and related SQL Server packages. The following repositories are currently available:

| Repository | Name | Description |
| --- | --- | --- |
| **2025** | `mssql-server-2025` | [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] repository. |

The repository contains packages for the base SQL Server release, and any bug fixes or improvements since that release. Cumulative updates are specific to a release version, such as [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. They're released on a regular cadence. General distribution release (GDR) updates are released in the same repository.

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

1. Look for a file that configures the SQL Server repository, such as `mssql-server-2025.repo`.

1. Display the contents of the file using `cat`.

   ```bash
   sudo cat /etc/yum.repos.d/mssql-server-2025.repo
   ```

1. The repository name in the `baseurl` property, such as `mssql-server-2025`, is the configured repository. You can identify it using the table in the [Repositories](#repositories) section of this article.

### [Ubuntu](#tab/ubuntu)

1. Find the files in the `/etc/apt/sources.list.d` directory that configure a SQL Server repository using the following command:

   ```bash
   grep -rl mssql-server /etc/apt/sources.list.d
   ```

1. Display the contents of each file using `cat`.

   ```bash
   cat /etc/apt/sources.list.d/mssql-server-2025.list
   ```

1. The repository name in the package URL, such as `mssql-server-2025`, is the configured repository. You can identify it using the table in the [Repositories](#repositories) section of this article.

---

## Remove old repository

If necessary, remove the old repository using the following command.

### [RHEL](#tab/rhel)

```bash
sudo rm -rf /etc/yum.repos.d/mssql-server-2025.repo
```

This command assumes that the file identified in the previous section is named `mssql-server-2025.repo`.

### [Ubuntu](#tab/ubuntu)

```bash
sudo rm /etc/apt/sources.list.d/mssql-server-2025.list
```

This command assumes that the file identified in the previous section is named `mssql-server-2025.list`.

---

## Configure new repository

Configure the new repository to use for SQL Server installations and upgrades. Use one of the following commands to configure the repository of your choice.

### [RHEL](#tab/rhel)

[!INCLUDE [rhel-10](../includes/rhel-10.md)]

The packages are agnostic to RHEL minor versions. For example, if you use RHEL 10.1, use the path `/rhel/10` to configure your repository.

```bash
curl -fsSL https://packages.microsoft.com/config/rhel/10/mssql-server-2025.repo | sudo tee /etc/yum.repos.d/mssql-server-2025.repo
```

### [Ubuntu](#tab/ubuntu)

Configure the new repository for SQL Server installations and upgrades.

1. Download the public key, convert it from ASCII to GPG format, and write it to the required location:

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --batch --yes --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   ```

1. Download and register the repository of your choice.

   ```bash
   curl -fsSL https://packages.microsoft.com/config/ubuntu/24.04/mssql-server-2025.list | sudo tee /etc/apt/sources.list.d/mssql-server-2025.list
   ```

1. Run `apt-get update`.

   ```bash
   sudo apt-get update
   ```

---

If you choose to use a quickstart article, remember that you already configured the target repository. Don't repeat that step in the tutorial.

## Related content

- [Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux](quickstart-install-red-hat.md)
- [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-ubuntu.md)
- [Installation guidance for SQL Server on Linux](setup.md)
