---
title: "Ubuntu: Install SQL Server on Linux"
description: This quickstart shows how to install SQL Server 2017 and later versions on Ubuntu and then create and query a database with sqlcmd.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/08/2026
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
ms.custom:
  - intro-installation
  - linux-related-content
  - ignite-2025
---
# Quickstart: Install SQL Server and create a database on Ubuntu

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on Ubuntu 18.04. Then you can connect with **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Ubuntu 20.04. Then you can connect with **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on Ubuntu 22.04. Then you can connect with **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

In this quickstart, you install [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Ubuntu 24.04. Then you can connect with **`sqlcmd`** to create your first database and run queries.

::: moniker-end

For more information on supported platforms, see [Release information for SQL Server on Linux](../sql-server-linux-release-notes.md).

> [!TIP]  
> This tutorial requires user input and an internet connection. If you're interested in the [unattended](setup.md#unattended) or [offline](setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](setup.md).

> [!CAUTION]  
> [!INCLUDE [password-complexity](../includes/password-complexity.md)]

## Prerequisites

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

You need an Ubuntu 18.04 machine with **at least 2 GB** of memory.

To install Ubuntu 18.04 on your own machine, go to <https://releases.ubuntu.com/18.04/>.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

You need an Ubuntu 20.04 machine with **at least 2 GB** of memory.

To install Ubuntu 20.04 on your own machine, go to <https://releases.ubuntu.com/20.04/>.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

You need an Ubuntu 22.04 machine with **at least 2 GB** of memory.

To install Ubuntu 22.04 on your own machine, go to <https://releases.ubuntu.com/22.04/>.

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

You need an Ubuntu 24.04 machine with **at least 2 GB** of memory.

To install Ubuntu 24.04 on your own machine, go to <https://releases.ubuntu.com/24.04/>.

::: moniker-end

You can also create Ubuntu or Ubuntu Pro virtual machines in Azure. See [Tutorial: Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](change-repo.md).

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](change-repo-2025.md).

::: moniker-end

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Windows Subsystem for Linux (WSL) is supported for development purposes only. For instructions on installing SQL Server on WSL, see [Quickstart: Install SQL Server and create a database on Windows Subsystem for Linux (WSL 2)](quickstart-install-windows-subsystem-linux.md).

For other system requirements, see [System requirements for SQL Server on Linux](setup.md#system).

> [!TIP]  
> For production environments that require FIPS compliance or Expanded Security Maintenance (ESM) coverage for Ubuntu Universe packages, use **Ubuntu Pro**. You can enable Ubuntu Pro on your existing instance, or select a preconfigured Ubuntu Pro image when you provision your virtual machine in Azure.

<a id="install"></a>

## Install SQL Server

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

Import the public repository GPG keys:

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
```

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1 and later versions.

Download the public key, convert it from ASCII to GPG format, and write it to the required location:

```bash
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --batch --yes --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
```

::: moniker-end

To configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Ubuntu, run the following commands in a terminal to install the `mssql-server` package.

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

Register the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Ubuntu repository:

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

Register the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Ubuntu repository:

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
```

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

Register the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Ubuntu repository:

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list | sudo tee /etc/apt/sources.list.d/mssql-server.list
```

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

Register the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Ubuntu repository:

```bash
curl -fsSL https://packages.microsoft.com/config/ubuntu/24.04/mssql-server-2025.list | sudo tee /etc/apt/sources.list.d/mssql-server-2025.list
```

::: moniker-end

> [!TIP]  
> To install a different version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see the [SQL Server 2025](quickstart-install-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true#install), [SQL Server 2022](quickstart-install-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install), [SQL Server 2019](quickstart-install-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), or [SQL Server 2017](quickstart-install-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install) versions of this article.

1. Install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. The following [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express. ([!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] offers Enterprise Developer and Standard Developer editions.)

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

1. When the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

At this point, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running on your Ubuntu machine and is ready to use.

## Disable the `sa` account as a best practice

[!INCLUDE [connect-with-sa](../includes/connect-with-sa.md)]

<a id="tools"></a>

## Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The following steps install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] command-line tools: [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) and [bcp utility](../../tools/bcp/bcp-utility.md).

[!INCLUDE [odbc-ubuntu](../includes/odbc-ubuntu.md)]

[!INCLUDE [Connect, create, and query data](../includes/quickstart-connect-query.md)]
