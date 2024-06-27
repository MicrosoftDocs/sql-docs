---
title: "RHEL: Install SQL Server on Linux"
titleSuffix: SQL Server
description: This quickstart shows how to install SQL Server on Red Hat Enterprise Linux (RHEL) and then create and query a database with sqlcmd.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/24/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - intro-installation
  - linux-related-content
---
# Quickstart: Install SQL Server and create a database on Red Hat

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] on Red Hat Enterprise Linux (RHEL) 8.x. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md).

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] on Red Hat Enterprise Linux (RHEL) 8.x. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md).

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Red Hat Enterprise Linux (RHEL) 8.x or 9.x. Then you can connect with **sqlcmd** to create your first database and run queries.

For more information on supported platforms, see [Release notes for [!INCLUDE [sssql22](../includes/sssql22-md.md)] on Linux](sql-server-linux-release-notes-2022.md).

::: moniker-end

> [!TIP]  
> This tutorial requires user input and an internet connection. If you're interested in the [unattended](sql-server-linux-setup.md#unattended) or [offline](sql-server-linux-setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md).
If you choose to have a pre-installed SQL Server VM on RHEL ready to run your production-based workload, then please follow the [best practices](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist) for creating the SQL Server VM.

<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

## Azure Marketplace images

You can create your VM based on the following Azure Marketplace image:

- [RHEL 8.x](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftsqlserver.sql2019-rhel8)

When you use the above marketplace image, you avoid the installation step, and can directly configure the instance by providing the SKU and the `sa` password needed to get started with SQL Server. SQL Server Azure VMs deployed on RHEL using the above Marketplace images, are fully supported by both Microsoft and Red Hat.

You can configure SQL Server on Linux with **mssql-conf**, using the following command:

```bash
sudo /opt/mssql/bin/mssql-conf setup
```

::: moniker-end

## Prerequisites

You must have a RHEL 8.x machine with **at least 2 GB** of memory.

To install Red Hat Enterprise Linux on your own machine, go to [https://access.redhat.com/products/red-hat-enterprise-linux/evaluation](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation). You can also create RHEL virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm), and use `--image RHEL` in the call to `az vm create`.

If you've previously installed a Community Technology Preview (CTP) or Release Candidate (RC) of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](sql-server-linux-change-repo.md).

For other system requirements, see [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## <a id="install"></a> Install SQL Server

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

The following commands for installing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] point to the RHEL 8 repository. RHEL 8 doesn't come preinstalled with `python2`, which is required by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Before you begin the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] install steps, execute the command and verify that `python2` is selected as the interpreter:

```bash
sudo alternatives --config python
# If not configured, install python2 and openssl10 using the following commands:
sudo yum install python2
sudo yum install compat-openssl10
# Configure python2 as the default interpreter using this command:
sudo alternatives --config python
```

For more information, see the following blog on installing `python2` and configuring it as the default interpreter: https://www.redhat.com/en/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta.

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on RHEL, run the following commands in a terminal to install the `mssql-server` package:

1. Download the [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] Red Hat repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true#install) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following command to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo yum install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` using its full path, and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. To allow remote connections, open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port on the RHEL firewall. The default [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port is TCP 1433. If you're using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your RHEL machine and is ready to use!

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

The following commands for installing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] point to the RHEL 8 repository. RHEL 8 doesn't come preinstalled with `python2`, which is required by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. Before you begin the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] install steps, execute the command and verify that `python2` is selected as the interpreter:

```bash
sudo alternatives --config python
# If not configured, install python2 and openssl10 using the following commands:
sudo yum install python2
sudo yum install compat-openssl10
# Configure python2 as the default interpreter using this command:
sudo alternatives --config python
```

For more information, see the following blog on installing `python2` and configuring it as the default interpreter: https://www.redhat.com/en/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta.

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on RHEL, run the following commands in a terminal to install the `mssql-server` package:

1. Download the [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] Red Hat repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql22-md](../includes/sssql22-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver16&preserve-view=true#install) versions of this article.

1. Run the following command to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo yum install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` using its full path, and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. To allow remote connections, open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port on the RHEL firewall. The default [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port is TCP 1433. If you're using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your RHEL machine and is ready to use!

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

### [Red Hat 8](#tab/rhel8)

The following commands for installing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] point to the RHEL 8 repository.

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on RHEL, run the following commands in a terminal to install the `mssql-server` package:

1. Download the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Red Hat 8 repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2022.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true#install) versions of this article.

1. Run the following command to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo yum install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` using its full path, and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. To allow remote connections, open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port on the RHEL firewall. The default [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port is TCP 1433. If you're using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your RHEL machine and is ready to use!

### [Red Hat 9](#tab/rhel9)

Starting with RHEL 9, you can run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as a confined application with SELinux enabled. For more information about confined and unconfined applications with SELinux, see [Getting started with SELinux](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/9/html/using_selinux/getting-started-with-selinux_using-selinux).

To run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as a confined application, follow these steps:

- Ensure that [SELinux is enabled and in enforcing mode](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/9/html/using_selinux/changing-selinux-states-and-modes_using-selinux).

- Install the `mssql-server` package using the steps mentioned later in this section.

- Install the new `mssql-server-selinux` package.

  ```bash
  sudo yum install -y mssql-server-selinux
  ```

> [!NOTE]  
> You can still install and run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as an unconfined application like in previous versions of RHEL.

To configure [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on RHEL 9, run the following commands in a terminal to install the `mssql-server` package:

1. Download the [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Red Hat 9 repository configuration file:

   ```bash
   sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/9/mssql-server-2022.repo
   ```

   > [!TIP]  
   > If you want to install a different version of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-2017&preserve-view=true#install) or [[!INCLUDE [sssql19-md](../includes/sssql19-md.md)]](quickstart-install-connect-red-hat.md?view=sql-server-linux-ver15&preserve-view=true#install) versions of this article.

1. Run the following command to install [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]:

   ```bash
   sudo yum install -y mssql-server
   ```

   Optionally, if you want to run [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as a confined application, install the `mssql-server-selinux` package, which enables custom policies.

   ```bash
   sudo yum install -y mssql-server-selinux
   ```

1. After the package installation finishes, run `mssql-conf setup` using its full path, and follow the prompts to set the SA password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   Remember to specify a strong password for the SA account. You need a minimum length 8 characters, including uppercase and lowercase letters, base-10 digits and/or non-alphanumeric symbols.

1. Once the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server
   ```

1. To allow remote connections, open the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port on the RHEL firewall. The default [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] port is TCP 1433. If you're using **FirewallD** for your firewall, you can use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running on your RHEL machine and is ready to use!

---

::: moniker-end

## Disable the `sa` account as a best practice

[!INCLUDE [connect-with-sa](includes/connect-with-sa.md)]

## <a id="tools"></a> Install the SQL Server command-line tools

To create a database, you need to connect with a tool that can run Transact-SQL statements on [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The following steps install the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] command-line tools: [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md) and [bcp utility](../tools/bcp-utility.md).

[!INCLUDE [odbc-redhat](includes/odbc-redhat.md)]

[!INCLUDE [Connect, create, and query data](includes/quickstart-connect-query.md)]
