---
title: "Ubuntu: Install SQL Server on Linux"
description: This quickstart shows how to install SQL Server 2017 or SQL Server 2019 on Ubuntu and then create and query a database with sqlcmd.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/20/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - seo-lt-2019
  - intro-installation
---
# Quickstart: Install SQL Server and create a database on Ubuntu

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] on Ubuntu 18.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on Ubuntu 20.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Ubuntu 20.04. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for [!INCLUDE[sssql22](../includes/sssql22-md.md)] on Linux](sql-server-linux-release-notes-2022.md).

::: moniker-end

> [!TIP]  
> This tutorial requires user input and an internet connection. If you are interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

If you choose to have a pre-installed SQL Server VM on Ubuntu ready to run your production-based workload, then please follow the [best practices](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist) for creating the SQL Server VM.

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

## Azure Marketplace images

You can create your VM based on one of the following two Azure Marketplace images:

- [Ubuntu 20.04](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftsqlserver.sql2019-ubuntupro2004)
- [Ubuntu 18.04](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftsqlserver.sql2019-ubuntupro1804)

When you use these images, you avoid the installation step, and can directly configure the instance by providing the SKU and the `sa` password needed to get started with SQL Server. SQL Server Azure VMs deployed on Ubuntu Pro using the above Marketplace images, are fully supported by both Microsoft and Canonical.

You can configure SQL Server on Linux with **mssql-conf**, using the following command:

```bash
sudo /opt/mssql/bin/mssql-conf setup
```

::: moniker-end

## Prerequisites

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

You must have an Ubuntu 18.04 machine with **at least 2 GB** of memory.

To install Ubuntu 18.04 on your own machine, go to <https://releases.ubuntu.com/18.04/>. You can also create Ubuntu virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

You must have an Ubuntu 20.04 machine with **at least 2 GB** of memory.

To install Ubuntu 20.04 on your own machine, go to <https://releases.ubuntu.com/20.04/>. You can also create Ubuntu virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

You must have an Ubuntu 20.04 machine with **at least 2 GB** of memory.

To install Ubuntu 20.04 on your own machine, go to <https://releases.ubuntu.com/20.04/>. You can also create Ubuntu virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm).

::: moniker-end

If you've previously installed a Community Technology Preview (CTP) or Release Candidate (RC) of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure Linux repositories for SQL Server](sql-server-linux-change-repo.md).

The [Windows Subsystem for Linux](/windows/wsl/about) isn't supported as an installation target for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## <a id="install"></a> Install SQL Server

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Ubuntu, run the following commands in a terminal to install the **mssql-server** package.

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Import the public repository GPG keys:

   ```bash
   wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2017.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

1. Import the public repository GPG keys:

   ```bash
   wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

1. Import the public repository GPG keys:

   ```bash
   wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Ubuntu repository:

   ```bash
   sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list)"
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-ubuntu.md?view=sql-server-linux-ver15&preserve-view=true#install) versions of this article.

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo apt-get update
   sudo apt-get install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

::: moniker-end

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your Ubuntu machine and is ready to use!

## <a id="tools"></a> Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The following steps install the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools: [**sqlcmd**](../tools/sqlcmd-utility.md) and [**bcp**](../tools/bcp-utility.md).

Use the following steps to install the **mssql-tools** on Ubuntu. If **curl** isn't installed, you can run this code:

```bash
sudo apt-get update
sudo apt install curl
```

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Update the sources list and run the installation command with the `unixODBC` developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools unixodbc-dev
   ```

   You can update to the latest version of **mssql-tools** using the following commands:

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your `PATH` environment variable, to make **sqlcmd** or **bcp** accessible from the bash shell.

   For interactive sessions, modify the `PATH` environment variable in your `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   ```

   For non-interactive sessions, modify the `PATH` environment variable in your `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

[!INCLUDE [Connect, create, and query data](includes/sql-linux-quickstart-connect-query.md)]

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Update the sources list and run the installation command with the `unixODBC` developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools unixodbc-dev
   ```

   You can update to the latest version of **mssql-tools** using the following commands:

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your `PATH` environment variable, to make **sqlcmd** or **bcp** accessible from the bash shell.

   For interactive sessions, modify the `PATH` environment variable in your `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   ```

   For non-interactive sessions, modify the `PATH` environment variable in your `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

[!INCLUDE [Connect, create, and query data](includes/sql-linux-quickstart-connect-query.md)]

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

1. Import the public repository GPG keys.

   ```bash
   curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
   ```

1. Register the Ubuntu repository.

   ```bash
   curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
   ```

1. Update the sources list and run the installation command with the `unixODBC` developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools unixodbc-dev
   ```

   You can update to the latest version of **mssql-tools** using the following commands:

   ```bash
   sudo apt-get update
   sudo apt-get install mssql-tools
   ```

1. For convenience, add `/opt/mssql-tools/bin/` to your `PATH` environment variable, to make **sqlcmd** or **bcp** accessible from the bash shell.

   For interactive sessions, modify the `PATH` environment variable in your `~/.bash_profile` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
   ```

   For non-interactive sessions, modify the `PATH` environment variable in your `~/.bashrc` file with the following command:

   ```bash
   echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
   source ~/.bashrc
   ```

[!INCLUDE [Connect, create, and query data](includes/sql-linux-quickstart-connect-query.md)]

::: moniker-end
