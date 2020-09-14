---
title: Configure Linux repositories for SQL Server 2017 and 2019
description: Check and configure source repositories for SQL Server 2019 and SQL Server 2017 on Linux. The source repository affects the version of SQL Server that is applied during installation and upgrade.
author: VanMSFT 
ms.author: vanto
ms.date: 04/28/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: linux
zone_pivot_groups: ld2-linux-distribution
---
# Configure repositories for installing and upgrading SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

::: zone pivot="ld2-rhel"
This article describes how to configure the correct repository for SQL Server 2017 and SQL Server 2019 installations and upgrades on Linux. At the top, your current selection is **Red Hat (RHEL)**.
::: zone-end

::: zone pivot="ld2-sles"
This article describes how to configure the correct repository for SQL Server 2017 and SQL Server 2019 installations and upgrades on Linux. At the top, your current selection is **SUSE (SLES)**.
::: zone-end

::: zone pivot="ld2-ubuntu"
This article describes how to configure the correct repository for SQL Server 2017 and SQL Server 2019 installations and upgrades on Linux. At the top, your current selection is **Ubuntu**.
::: zone-end

> [!TIP]
> SQL Server 2019 is now available! To try it, use this article to configure the new **mssql-server-2019** repository. Then install using the instructions in the [installation guide](sql-server-linux-setup.md).

## <a id="repositories"></a> Repositories

When you install SQL Server on Linux, you must configure a Microsoft repository. This repository is used to acquire the database engine package, **mssql-server**, and related SQL Server packages. There are currently five main repositories:

| Repository | Name | Description |
|---|---|---|
| **2019** | **mssql-server-2019** | SQL Server 2019 Cumulative Update (CU) repository. |
| **2019 GDR** | **mssql-server-2019-gdr** | SQL Server 2019 GDR repository for critical updates only. |
| **2019 Preview** | **mssql-server-preview** | SQL Server 2019 Preview and RC repository. |
| **2017** | **mssql-server-2017** | SQL Server 2017 Cumulative Update (CU) repository. |
| **2017 GDR** | **mssql-server-2017-gdr** | SQL Server 2017 GDR repository for critical updates only. |

## <a id="cuversusgdr"></a> Cumulative Update versus GDR

It is important to note that there are two main types of repositories for each distribution:

- **Cumulative Updates (CU)**: The Cumulative Update (CU) repository contains packages for the base SQL Server release and any bug fixes or improvements since that release. Cumulative updates are specific to a release version, such as SQL Server 2019. They are released on a regular cadence.

- **GDR**: The GDR repository contains packages for the base SQL Server release and only critical fixes and security updates since that release. These updates are also added to the next CU release.

Each CU and GDR release contains the full SQL Server package and all previous updates for that repository. Updating from a GDR release to a CU release is supported by changing your configured repository for SQL Server. You can also [downgrade](sql-server-linux-setup.md#rollback) to any release within your major version (ex: 2017).

> [!NOTE]
> You can update from a GDR release to CU release at any time by changing repositories. Updating from a CU release to a GDR release is not supported.

## Configure repositories

::: zone pivot="ld2-rhel"
Use the steps in the following sections to configure repositories on Red Hat Enterprise Server (RHEL).
::: zone-end

::: zone pivot="ld2-sles"
Use the steps in the following sections to configure repositories on SUSE Linux Enterprise Server (SLES).
::: zone-end

::: zone pivot="ld2-ubuntu"
Use the steps in the following sections to configure repositories on Ubuntu.
::: zone-end

## Check for previously configured repositories

<!--RHEL-->
::: zone pivot="ld2-rhel"
First verify whether you have already registered a SQL Server repository.

1. View the files in the **/etc/yum.repos.d** directory with the following command:

   ```bash
   sudo ls /etc/yum.repos.d
   ```

2. Look for a file that configures the SQL Server directory, such as **mssql-server.repo**.

3. Print out the contents of the file.

   ```bash
   sudo cat /etc/yum.repos.d/mssql-server.repo
   ```

4. The **name** property is the configured repository. You can identify it with the table in the [Repositories](#repositories) section of this article.

::: zone-end

<!--SLES-->
::: zone pivot="ld2-sles"
First verify whether you have already registered a SQL Server repository.

1. Use **zypper info** to get information about any previously configured repository.

   ```bash
   sudo zypper info mssql-server
   ```

2. The **Repository** property is the configured repository. You can identify it with the table in the [Repositories](#repositories) section of this article.

::: zone-end

<!--Ubuntu-->
::: zone pivot="ld2-ubuntu"
First verify whether you have already registered a SQL Server repository.

1. View the contents of the **/etc/apt/sources.list** file.

   ```bash
   sudo cat /etc/apt/sources.list
   ```

2. Examine the package URL for mssql-server. You can identify it with the table in the [Repositories](#repositories) section of this article.

::: zone-end

## Remove old repository

<!--RHEL-->
::: zone pivot="ld2-rhel"
If necessary, remove the old repository with the following command.

```bash
sudo rm -rf /etc/yum.repos.d/mssql-server.repo
```

This command assumes that the file identified in the previous section was named **mssql-server.repo**.

::: zone-end

<!--SLES-->
::: zone pivot="ld2-sles"
If necessary, remove the old repository. Use one of the following commands based on the type of previously configured repository.

| Repository | Command to remove |
|---|---|
| **Preview (2019)** | `sudo zypper removerepo 'packages-microsoft-com-mssql-server-preview'` |
| **2019 CU** | `sudo zypper removerepo 'packages-microsoft-com-mssql-server-2019'` |
| **2019 GDR** | `sudo zypper removerepo 'packages-microsoft-com-mssql-server-2019-gdr'`|
| **2017 CU** | `sudo zypper removerepo 'packages-microsoft-com-mssql-server-2017'` |
| **2017 GDR** | `sudo zypper removerepo 'packages-microsoft-com-mssql-server-2017-gdr'`|

::: zone-end

<!--Ubuntu-->
::: zone pivot="ld2-ubuntu"
If necessary, remove the old repository. Use one of the following commands based on the type of previously configured repository.

> [!NOTE]
> Starting with SQL Server 2019 CU3 and SQL Server 2017 CU20, Ubuntu 18.04 is supported. If you are using Ubuntu 16.04, change the path below to `/ubuntu/16.04` instead of `/ubuntu/18.04`, and use the correct [distribution code name](https://releases.ubuntu.com/).

| Repository | Command to remove |
|---|---|
| **Preview (2019)** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/16.04/mssql-server-preview xenial main'` |
| **2019 CU** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019 bionic main'` | 
| **2019 GDR** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/18.04/mssql-server-2019-gdr bionic main'` |
| **2017 CU** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017 xenial main'` | 
| **2017 GDR** | `sudo add-apt-repository -r 'deb [arch=amd64] https://packages.microsoft.com/ubuntu/16.04/mssql-server-2017-gdr xenial main'` |

::: zone-end

## Configure new repository

<!--RHEL-->
::: zone pivot="ld2-rhel"
Configure the new repository to use for SQL Server installations and upgrades. Use one of the following commands to configure the repository of your choice.

> [!NOTE]
> The following commands for SQL Server 2019 points to the RHEL 8 repository. RHEL 8 does not come preinstalled with python2, which is required by SQL Server. For more information, see the following blog on installing python2 and configuring it as the default interpreter: https://www.redhat.com/en/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta.
>
> Starting with SQL Server 2017 CU20, RHEL 8 is supported.
>
> If you are using RHEL 7 or RHEL 8, ensure the paths match `/rhel/7` or `/rhel/8`. Our packages are agnostic to RHEL minor versions. This means that if you are using RHEL 7.6, you will need to use the path `/rhel/7` to configure your repository.

| Repository | Version | Command |
|---|---|---|
| **2019 CU** | 2019 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo` |
| **2019 GDR** | 2019 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019-gdr.repo` |
| **2017 CU** | 2017 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017.repo` |
| **2017 GDR** | 2017 | `sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017-gdr.repo` |

::: zone-end

<!--SLES-->
::: zone pivot="ld2-sles"
Configure the new repository to use for SQL Server installations and upgrades. Use one of the following commands to configure the repository of your choice.

| Repository | Version | Command |
|---|---|---|
| **2019 CU** | 2019 | `sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2019.repo` |
| **2019 GDR** | 2019 | `sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2019-gdr.repo` |
| **2017 CU** | 2017 | `sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2017.repo` |
| **2017 GDR** | 2017 | `sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2017-gdr.repo` |

::: zone-end

<!--Ubuntu-->
::: zone pivot="ld2-ubuntu"
Configure the new repository to use for SQL Server installations and upgrades.

> [!NOTE]
> Starting with SQL Server 2019 CU3 and SQL Server 2017 CU20, Ubuntu 18.04 is supported. The following commands points to the Ubuntu 18.04 repository.
>
> If you are using Ubuntu 16.04, change the path below to `/ubuntu/16.04` instead of `/ubuntu/18.04`.

1. Import the public repository GPG keys.

   ```bash
   sudo curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

2. Use one of the following commands to configure the repository of your choice.

   | Repository | Version | Command |
   |---|---|---|
   | **2019 CU** | 2019 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"` |
   | **2019 GDR** | 2019 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019-gdr.list)"` |
   | **2017 CU** | 2017 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"` |
   | **2017 GDR** | 2017 | `sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017-gdr.list)"` |

3. Run **apt-get update**.

   ```bash
   sudo apt-get update
   ```

::: zone-end

## Next steps

After you have configured the correct repository, you can proceed to [install](sql-server-linux-setup.md#platforms) or [update](sql-server-linux-setup.md#upgrade) SQL Server and any related packages from the new repository.

::: zone pivot="ld2-rhel"
> [!IMPORTANT]
> At this point, if you choose to use the [RHEL quickstart](quickstart-install-connect-red-hat.md), remember that you have already configured the target repository. Do not repeat that step in the tutorials. This is especially true if you configure the GDR repository, because the quickstart uses the CU repository.
::: zone-end

::: zone pivot="ld2-sles"
> [!IMPORTANT]
> At this point, if you choose to use the [SLES quickstart](quickstart-install-connect-suse.md), remember that you have already configured the target repository. Do not repeat that step in the tutorials. This is especially true if you configure the GDR repository, because the quickstart uses the CU repository.
::: zone-end

::: zone pivot="ld2-ubuntu"
> [!IMPORTANT]
> At this point, if you choose to use the [Ubuntu quickstart](quickstart-install-connect-ubuntu.md), remember that you have already configured the target repository. Do not repeat that step in the tutorials. This is especially true if you configure the GDR repository, because the quickstart uses the CU repository.
::: zone-end

For more information on how to install SQL Server 2017 on Linux, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).
