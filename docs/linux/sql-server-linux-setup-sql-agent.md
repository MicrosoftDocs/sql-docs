---
title: Configure the SQL Server Agent on Linux
description: Learn how to enable or install the SQL Server Agent on Linux. Starting with SQL Server 2017 CU4, SQL Server Agent is included with the mssql-server package.
author: VanMSFT
ms.author: vanto
ms.date: 12/05/2019
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: 77f16adc-e6cb-4a57-82f3-7b9780369868
ms.custom:
  - intro-installation
---

# Install SQL Server Agent on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to enable or install the SQL Server Agent on Linux.

The [SQL Server Agent](../ssms/agent/sql-server-agent.md) runs scheduled SQL Server jobs. Starting with SQL Server 2017 CU4, SQL Server Agent is included with the **mssql-server** package and is disabled by default. For information on the features supported for this release of the SQL Server Agent along with version information, see the [Release Notes](sql-server-linux-release-notes-2017.md).

## Instructions

Before using the SQL Server Agent on Linux, use the following steps to enable or install it.

1. Add your hostname (with and without domain) in the `/etc/hosts` files. The following lines show an example of the format for these entries:

   ```bash
   "IP Address" "hostname"
   "IP Address" "hostname.domain.com"
   ```

1. Follow the instructions in one of the following sections based on your version of SQL Server:

   | Versions | Instructions |
   |---|---|
   | SQL Server 2017 CU4 and higher</br>SQL Server 2019 | [Enable the SQL Server Agent](#EnableAgentAfterCU4) |
   | SQL Server 2017 CU3 and lower | [Install the SQL Server Agent](#InstallAgentBelowCU4) |

## <a id="EnableAgentAfterCU4"></a>Enable the SQL Server Agent

For SQL Server 2019 and SQL Server 2017 CU4 and higher, you only need to enable the SQL Server Agent. You do not need to install a separate package.

To enable SQL Server Agent, follow the steps below.

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true 
sudo systemctl restart mssql-server
```

> [!NOTE]
> If you are upgrading from 2017 CU3 or below with Agent installed, SQL Server Agent will be enabled automatically and previous Agent packages will be uninstalled.  

> [!NOTE]
> SQL Server Management Studio Object Explorer does not display the contents of the SQL Server Agent node unless *Agent XPs* extended stored procedures are enabled, regardless of the SQL Server Agent service state. For more information, see [Agent XPs Server Configuration Option](../database-engine/configure-windows/agent-xps-server-configuration-option.md)

## <a name="InstallAgentBelowCU4"></a>Install the SQL Server Agent

For SQL Server 2017 CU3 and earlier, you must install the SQL Server Agent package.

> [!NOTE]
> The install instructions below apply to SQL Server Versions 2017 CU3 and below. Before installing SQL Server Agent, first [install SQL Server](sql-server-linux-setup.md#platforms). This configures the keys and repositories that you use when you install the **mssql-server-agent** package.

Install the SQL Server Agent for your platform:
- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)

### <a name="RHEL">Install on RHEL</a>

Use the following steps to install the **mssql-server-agent** on Red Hat Enterprise Linux. 

```bash
sudo yum install mssql-server-agent
sudo systemctl restart mssql-server
```

If you already have **mssql-server-agent** installed, you can update to the latest version with the following commands:

```bash
sudo yum check-update
sudo yum update mssql-server-agent
sudo systemctl restart mssql-server
```

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

### <a name="ubuntu">Install on Ubuntu</a>

Use the following steps to install the **mssql-server-agent** on Ubuntu. 

```bash
sudo apt-get update 
sudo apt-get install mssql-server-agent
sudo systemctl restart mssql-server
```

If you already have **mssql-server-agent** installed, you can update to the latest version with the following commands:

```bash
sudo apt-get update 
sudo apt-get install mssql-server-agent
sudo systemctl restart mssql-server
```

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

### <a name="SLES">Install on SLES</a>

Use the following steps to install the **mssql-server-agent** on SUSE Linux Enterprise Server. 

Install **mssql-server-agent** 

```bash
sudo zypper install mssql-server-agent
sudo systemctl restart mssql-server
```

If you already have **mssql-server-agent** installed, you can update to the latest version with the following commands:

```bash
sudo zypper refresh
sudo zypper update mssql-server-agent
sudo systemctl restart mssql-server
```

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## Next steps
For more information on how to use SQL Server Agent to create, schedule, and run jobs, see [Run a SQL Server Agent job on Linux](sql-server-linux-run-sql-server-agent-job.md).
