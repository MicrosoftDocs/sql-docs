---
title: "SUSE: Install SQL Server on Linux"
description: This quickstart shows how to install SQL Server on SUSE Linux Enterprise Server and then create and query a database with sqlcmd.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/13/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: intro-installation
---
# Quickstart: Install SQL Server and create a database on SUSE Linux Enterprise Server

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] on SUSE Linux Enterprise Server (SLES) v12. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

::: moniker-end
<!--SQL Server 2019+ on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on SUSE Linux Enterprise Server (SLES) v15 (SP3). Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on SUSE Linux Enterprise Server (SLES) v15 (SP3). Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md).

::: moniker-end

> [!TIP]  
> This tutorial requires user input and an internet connection. If you are interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).

## Prerequisites

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

You must have a SLES v12 SP5 machine with **at least 2 GB** of memory. The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.

::: moniker-end

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

You must have a SLES v15 (SP1 - SP3) machine with **at least 2 GB** of memory. The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.

::: moniker-end

<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

You must have a SLES v15 (SP1 - SP3) machine with **at least 2 GB** of memory. The file system must be **XFS** or **EXT4**. Other file systems, such as **BTRFS**, are unsupported.

::: moniker-end

To install SUSE Linux Enterprise Server on your own machine, go to [https://www.suse.com/products/server](https://www.suse.com/products/server). You can also create SLES virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm), and use `--image SLES` in the call to `az vm create`.

If you've previously installed a community technology preview (CTP) or release candidate (RC) of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure Linux repositories for SQL Server](sql-server-linux-change-repo.md).

> [!NOTE]  
> At this time, the [Windows Subsystem for Linux](/windows/wsl/about) for Windows 10 or Windows 11 is not supported as an installation target.

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## <a id="install"></a> Install SQL Server

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on SLES, run the following commands in a terminal to install the **mssql-server** package:

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

1. Download the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server-2017.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-suse.md?view=sql-server-linux-ver15&preserve-view=true#install) version of this article.

1. Refresh your repositories.

   ```bash
   sudo zypper --gpg-auto-import-keys refresh
   ```

   To ensure that the Microsoft package signing key is installed on your system, you can import it using the command below:

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo zypper install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. If you plan to connect remotely, you might also need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall. If you're using the SuSE firewall, you need to edit the `/etc/sysconfig/SuSEfirewall2` configuration file. Modify the `FW_SERVICES_EXT_TCP` entry to include the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port number.

   ```ini
   FW_SERVICES_EXT_TCP="1433"
   ```

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your SLES machine and is ready to use!

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

1. Download the [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/mssql-server-2019.repo
   ```

   > [!WARNING]  
   > SUSE Linux Enterprise Server (SLES) is not a supported platform for the Community Technology Preview (CTP) 2.1 release of [!INCLUDE[sssql22](../includes/sssql22-md.md)]. You won't be able to install [!INCLUDE[sssql22](../includes/sssql22-md.md)].

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-suse.md?view=sql-server-linux-2017&preserve-view=true#install) version of this article.

1. Refresh your repositories.

   ```bash
   sudo zypper --gpg-auto-import-keys refresh
   ```

   To ensure that the Microsoft package signing key is installed on your system, you can import it using the command below:

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo zypper install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. If you plan to connect remotely, you might need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

   > [!NOTE]  
   > On SLES, you can manage your firewall using `firewalld` for example. Install it using `sudo zypper install firewalld`, and then start it up with `sudo systemctl start firewalld`. Add the firewall rule with `sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent`, and then reload the firewall with `sudo firewall-cmd --reload` for the settings to take effect.

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your SLES machine and is ready to use!

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

1. Download the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] SLES repository configuration file:

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/mssql-server-2022.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-suse.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-suse.md?view=sql-server-linux-v15&preserve-view=true#install) version of this article.

1. Refresh your repositories.

   ```bash
   sudo zypper --gpg-auto-import-keys refresh
   ```

   To ensure that the Microsoft package signing key is installed on your system, you can import it using the command below:

   ```bash
   sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc
   ```

1. Run the following commands to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo zypper install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. If you plan to connect remotely, you might need to open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] TCP port (default 1433) on your firewall.

   > [!NOTE]  
   > On SLES, you can manage your firewall using `firewalld` for example. Install it using `sudo zypper install firewalld`, and then start it up with `sudo systemctl start firewalld`. Add the firewall rule with `sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent`, and then reload the firewall with `sudo firewall-cmd --reload` for the settings to take effect.

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your SLES machine and is ready to use!

::: moniker-end

## <a id="tools"></a> Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The following steps install the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools: [sqlcmd](../tools/sqlcmd-utility.md) and [bcp](../tools/bcp-utility.md).

1. Add the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] repository to Zypper.

   ```bash
   sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/15/prod.repo
   sudo zypper --gpg-auto-import-keys refresh
   ```

1. Install `mssql-tools` with the unixODBC developer package. For more information, see [Install the Microsoft ODBC driver for SQL Server (Linux)](../connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

   ```bash
   sudo zypper install -y mssql-tools unixODBC-devel
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
