---
title: "RHEL: Install SQL Server on Linux"
titleSuffix: SQL Server
description: This quickstart shows how to install SQL Server on Red Hat Enterprise Linux (RHEL) and then create and query a database with sqlcmd.
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
# Quickstart: Install SQL Server and create a database on Red Hat Enterprise Linux

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

In this quickstart, you install [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on Red Hat Enterprise Linux (RHEL) 8.x. Then you connect by using **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

In this quickstart, you install [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Red Hat Enterprise Linux (RHEL) 8.x. Then you connect by using **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

In this quickstart, you install [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on Red Hat Enterprise Linux (RHEL) 9.x. Then you connect by using **`sqlcmd`** to create your first database and run queries.

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

In this quickstart, you install [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] on Red Hat Enterprise Linux (RHEL) 10.x. Then you connect by using **`sqlcmd`** to create your first database and run queries.

> [!NOTE]  
> [!INCLUDE [rhel-9](../includes/rhel-9.md)] [!INCLUDE [rhel-10](../includes/rhel-10.md)] TLS 1.3 is enabled by default on [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

::: moniker-end
::: moniker range=">=sql-server-linux-ver16 || >=sql-server-ver16"

If you want to automate your installation using Ansible, see [Quickstart: Deploy SQL Server on Linux using an Ansible playbook](deploy-ansible.md).

::: moniker-end

For more information on supported platforms, see [Release information for SQL Server on Linux](../sql-server-linux-release-notes.md).

> [!TIP]  
> This quickstart requires user input and an internet connection. If you're interested in the [unattended](setup.md#unattended) or [offline](setup.md#offline) installation procedures, see [Installation guidance for SQL Server on Linux](setup.md). If you choose to have a preinstalled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] VM on RHEL ready to run your production-based workload, follow the [best practices](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices-checklist) for creating the SQL Server VM.

## Prerequisites

You need a machine running a supported version of RHEL with **at least 2 GB** of memory.

To install Red Hat Enterprise Linux on your own machine, go to [https://access.redhat.com/products/red-hat-enterprise-linux/evaluation](https://access.redhat.com/products/red-hat-enterprise-linux/evaluation). You can also create RHEL virtual machines in Azure. See [Create and Manage Linux VMs with the Azure CLI](/azure/virtual-machines/linux/tutorial-manage-vm), and use `--image RHEL` in the call to `az vm create`.

::: moniker range="<=sql-server-linux-ver16 || <=sql-server-ver16"

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](change-repo.md).

::: moniker-end
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

If you previously installed a preview version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must first remove the old repository before following these steps. For more information, see [Configure repositories for installing and upgrading SQL Server 2025 on Linux](change-repo-2025.md).

::: moniker-end

For other system requirements, see [System requirements for SQL Server on Linux](setup.md#system).

To ensure you configure your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance according to the recommended standards, see:

- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../configure/performance-best-practices-sql-server-memory.md)

<a id="install"></a>

## Install SQL Server

::: moniker range="<=sql-server-linux-ver15 || <=sql-server-ver15"

The following commands for installing [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] point to the RHEL 8 repository. RHEL 8 doesn't come with `python2` preinstalled, but [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requires it. Before you begin the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] install steps, run the command and verify that `python2` is selected as the interpreter:

```bash
sudo alternatives --config python
# If not configured, install python2 and openssl10 using the following commands:
sudo yum install python2
sudo yum install compat-openssl10
# Configure python2 as the default interpreter using this command:
sudo alternatives --config python
```

For more information, see the following blog on installing `python2` and configuring it as the default interpreter: <https://www.redhat.com/blog/installing-microsoft-sql-server-red-hat-enterprise-linux-8-beta>.

::: moniker-end
::: moniker range=">=sql-server-linux-ver16 || >=sql-server-ver16"

Starting with RHEL 9, you can run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as a confined application with SELinux enabled. For more information about confined and unconfined applications with SELinux, see [Getting started with SELinux](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/using_selinux/getting-started-with-selinux_using-selinux).

To run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as a confined application, follow these steps:

- Ensure that [SELinux is enabled and in enforcing mode](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/using_selinux/changing-selinux-states-and-modes_using-selinux).

- Install the `mssql-server` package using the steps mentioned later in this section.

- Install the `mssql-server-selinux` package.

  ```bash
  sudo yum install -y mssql-server-selinux
  ```

> [!NOTE]  
> You can still install and run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as an unconfined application like in previous versions of RHEL.

::: moniker-end

To configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on RHEL, run the following commands in a terminal to install the `mssql-server` package:

<!--SQL Server 2017 on Linux-->
::: moniker range="=sql-server-linux-2017 || =sql-server-2017"

Download the [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] RHEL repository configuration file:

```bash
sudo curl -fsSL -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2017.repo
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="=sql-server-linux-ver15 || =sql-server-ver15"

Download the [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] RHEL repository configuration file:

```bash
sudo curl -fsSL -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/8/mssql-server-2019.repo
```

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range="=sql-server-linux-ver16 || =sql-server-ver16"

Download the [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] RHEL 9 repository configuration file:

```bash
sudo curl -fsSL -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/9/mssql-server-2022.repo
```

::: moniker-end
<!--SQL Server 2025 on Linux-->
::: moniker range=">=sql-server-linux-ver17 || >=sql-server-ver17"

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] CU 1 and later versions.

Download the [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] RHEL 10 repository configuration file:

```bash
curl -fsSL https://packages.microsoft.com/config/rhel/10/mssql-server-2025.repo | sudo tee /etc/yum.repos.d/mssql-server-2025.repo
```

::: moniker-end

> [!TIP]  
> To install a different version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], see the [SQL Server 2017](quickstart-install-red-hat.md?view=sql-server-linux-2017&preserve-view=true#install), [SQL Server 2019](quickstart-install-red-hat.md?view=sql-server-linux-ver15&preserve-view=true#install), [SQL Server 2022](quickstart-install-red-hat.md?view=sql-server-linux-ver16&preserve-view=true#install), or [SQL Server 2025](quickstart-install-red-hat.md?view=sql-server-linux-ver17&preserve-view=true#install) versions of this article.

1. Run the following command to install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

   ```bash
   sudo yum install -y mssql-server
   ```

1. After the package installation finishes, run `mssql-conf setup` by using its full path. Follow the prompts to set the `sa` password and choose your edition. As a reminder, the following [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] editions are freely licensed: Evaluation, Developer, and Express.

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../includes/password-complexity.md)]

1. When the configuration is done, verify that the service is running:

   ```bash
   systemctl status mssql-server --no-pager
   ```

1. To allow remote connections, open the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port on the RHEL firewall. The default [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] port is TCP 1433. If you're using **FirewallD** for your firewall, use the following commands:

   ```bash
   sudo firewall-cmd --zone=public --add-port=1433/tcp --permanent
   sudo firewall-cmd --reload
   ```

At this point, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running on your RHEL machine and is ready to use.

## Disable the `sa` account as a best practice

[!INCLUDE [connect-with-sa](../includes/connect-with-sa.md)]

<a id="tools"></a>

## Install the SQL Server command-line tools

To create a database, you need to connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using a tool that can run Transact-SQL statements. The following steps install the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] command-line tools: [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md) and [bcp utility](../../tools/bcp/bcp-utility.md).

[!INCLUDE [odbc-redhat](../includes/odbc-redhat.md)]

[!INCLUDE [Connect, create, and query data](../includes/quickstart-connect-query.md)]
