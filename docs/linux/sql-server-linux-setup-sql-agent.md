---
title: Install SQL Server Agent on Linux | Microsoft Docs
description: This article describes how to install the SQL Server Agent on Linux.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 02/20/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 77f16adc-e6cb-4a57-82f3-7b9780369868
---
# Install SQL Server Agent on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

 The [SQL Server Agent](https://docs.microsoft.com/sql/ssms/agent/sql-server-agent) runs scheduled SQL Server jobs. Starting with SQL Server 2017 CU4, SQL Server Agent is included with the **mssql-server** package and is disabled by default. For information on the features supported for this release of the SQL Server Agent along with version information, see the [Release Notes](sql-server-linux-release-notes.md).

 Install/Enable the SQL Server Agent:
- [For Versions 2017 CU4 and above, Enable the SQL Server Agent](#EnableAgentAfterCU4)
- [For Versions 2017 CU3 and Below, Install the SQL Server Agent](#InstallAgentBelowCU4)


## <a name="EnableAgentAfterCU4">For Versions 2017 CU4 and above, Enable the SQL Server Agent</a>

 To enable SQL Server Agent, follow the steps below.

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true 
sudo systemctl restart mssql-server
```

> [!NOTE]
> If you are upgrading from 2017 CU3 or below with Agent installed, SQL Server Agent will be enabled automatically and previous Agent packages will be uninstalled.  

## <a name="InstallAgentBelowCU4">For Versions 2017 CU3 and Below, Install the SQL Server Agent</a>

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

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

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

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

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

If you need an offline installation, locate the SQL Server Agent package download in the [Release notes](sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## Next steps
For more information on how to use SQL Server Agent to create, schedule, and run jobs, see [Run a SQL Server Agent job on Linux](sql-server-linux-run-sql-server-agent-job.md).
