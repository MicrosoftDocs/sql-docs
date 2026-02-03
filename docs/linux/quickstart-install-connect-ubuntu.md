---
title: "Ubuntu: Install SQL Server on Linux"
description: This quickstart shows how to install SQL Server 2017 and later versions on Ubuntu and then create and query a database with sqlcmd.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/27/2026
ms.service: sql
ms.subservice: linux
ms.topic: quickstart
ms.custom:
  - intro-installation
  - linux-related-content
  - ignite-2025
---
# Quickstart: Install SQL Server and create a database on Ubuntu

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] on Ubuntu 18.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on Ubuntu 20.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Ubuntu 20.04 or 22.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md).

::: moniker-end

<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

In this quickstart, you install [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] on Ubuntu 22.04. Then you can connect with **sqlcmd** to create your first database and run queries.

> [!NOTE]  
> Starting with [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] CU 1, Ubuntu 24.04 is supported.

For more information on supported platforms, see [Release notes for SQL Server 2025 on Linux](sql-server-linux-release-notes-2025.md).

::: moniker-end

> [!TIP]  
> This tutorial requires user input and an internet connection. If you're interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

> [!CAUTION]  
> [!INCLUDE [password-complexity](includes/password-complexity.md)]

## Prerequisites

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

You must have an Ubuntu 18.04 machine with **at least 2 GB** of memory.

To install Ubuntu 18.04 on your own machine, go to <https://releases.ubuntu.com/18.04/>. You can also create Ubuntu or Ubuntu Pro virtual machines in Azure. See [Tutorial: Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

You must have an Ubuntu 20.04 machine with **at least 2 GB** of memory.

To install Ubuntu 20.04 on your own machine, go to <https://releases.ubuntu.com/20.04/>. You can also create Ubuntu or Ubuntu Pro virtual machines in Azure. See [Tutorial: Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

You must have an Ubuntu 20.04 machine with **at least 2 GB** of memory.

To install Ubuntu 20.04 on your own machine, go to <https://releases.ubuntu.com/20.04/>. You can also create Ubuntu or Ubuntu Pro virtual machines in Azure. See [Tutorial: Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

You must have an Ubuntu 22.04 machine with **at least 2 GB** of memory.

To install Ubuntu 22.04 on your own machine, go to <https://releases.ubuntu.com/22.04/>. You can also create Ubuntu or Ubuntu Pro virtual machines in Azure. See [Tutorial: Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](sql-server-linux-change-repo-2025.md).

::: moniker-end

> [!NOTE]
> [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Windows Subsystem for Linux (WSL) is supported for development purposes only. For instructions on installing SQL Server on WSL, see [Quickstart: Install SQL Server and create a database on Windows Subsystem for Linux (WSL 2)](quickstart-install-connect-wsl-2.md).

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

> [!TIP]
> **Production and Compliance**
> For production environments that require **FIPS compliance** or expanded security maintenance (ESM) for the full open source stack (Universe repository), we recommend using **Ubuntu Pro**.
>
> You can enable Ubuntu Pro on your existing instance or select a pre-configured Ubuntu Pro image when provisioning in Azure.

<a id="install"></a>

## Install SQL Server

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Ubuntu, run the following commands in a terminal to install the **mssql-server** package.

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), [SQL Server 2022](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install), or [SQL Server 2025](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2022](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install), or [SQL Server 2025](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

### [Ubuntu 20.04](#tab/ubuntu2004)

1. Import the public repository GPG keys:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), or [SQL Server 2025](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

### [Ubuntu 22.04](#tab/ubuntu2204)

1. Download the public key, convert from ASCII to GPG format, and write it to the required location:

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   ```

   If you receive a warning about the public key not being available, you can use the following command instead:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Manually download and register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list | sudo tee /etc/apt/sources.list.d/mssql-server-2022.list
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), or [SQL Server 2025](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver17&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

---

::: moniker-end

<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

### [Ubuntu 22.04](#tab/2025ubuntu2204)

1. Download the public key, convert from ASCII to GPG format, and write it to the required location:

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   ```

   If you receive a warning about the public key not being available, you can use the following command instead:

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc
   ```

1. Manually download and register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   curl -fsSL https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2025.list | sudo tee /etc/apt/sources.list.d/mssql-server-2025.list
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), or [SQL Server 2022](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your Ubuntu machine and is ready to use.

### [Ubuntu 24.04](#tab/2505ubuntu2404)

1. Download the public key, convert from ASCII to GPG format, and write it to the required location:

   ```bash
   curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft-prod.gpg
   ```

1. Manually download and register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   curl -fsSL https://packages.microsoft.com/config/ubuntu/24.04/mssql-server-2025.list | sudo tee /etc/apt/sources.list.d/mssql-server-2025.list
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2019](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install), or [SQL Server 2022](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](includes/password-complexity.md)]

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your Ubuntu machine and is ready to use.

---

::: moniker-end

## Disable the SA account as a best practice

[!INCLUDE [connect-with-sa](includes/connect-with-sa.md)]

<a id="tools"></a>

## Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The following steps install the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools: [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md) and [bcp utility](../tools/bcp-utility.md).

[!INCLUDE [odbc-ubuntu](includes/odbc-ubuntu.md)]

[!INCLUDE [Connect, create, and query data](includes/quickstart-connect-query.md)]
