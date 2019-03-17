---
title: Configure SQL Server Replication on Linux | Microsoft Docs
description: This article describes how to configure SQL Server replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray
manager: craigg
ms.date: 03/20/2018
ms.topic: article
ms.prod: sql
ms.prod_service: "database-engine"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Configure SQL Server Replication on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

[!INCLUDE[SQL Server 2019](../includes/sssqlv15-md.md)] introduces SQL Server Replication for instances of SQL Server on Linux.

For detailed information about replication, see [SQL Server replication documentation](../relational-databases/replication/sql-server-replication.md).

Configure replication on Linux with either SQL Server Management Studio (SSMS) or Transact-SQL stored procedures.

* To use SSMS, follow the instructions in this article.

  Use SSMS on a Windows operating system to connect to instances of SQL Server. For background and instructions, see [Use SSMS to Manage SQL Server on Linux](./sql-server-linux-manage-ssms.md).
  
* For an example with stored procedures, follow the [Configure SQL Server replication on Linux](sql-server-linux-replication-tutorial-tsql.md) tutorial.

## Prerequisites

Before configuring publishers, distributors, and subscribers, you need to complete a couple configuration steps for the SQL Server instance.

1. Enable SQL Server Agent to use replication agents. On all Linux servers, run the following commands in the terminal.

  ```bash
  sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
  sudo systemctl restart mssql-server
  ```

1. Configure the SQL Server instance for replication. To configure the SQL Server instance for replication, run `sys.sp_MSrepl_createdatatypemappings` on all instances participating in replication.

  ```sql
  USE msdb
  GO
  exec sys.sp_MSrepl_createdatatypemappings;
  GO
  ```

1. Create a snapshot folder. The SQL Server agents require a snapshot folder to read/write to. Create the snapshot folder on the distributor.

  To create the snapshot folder, and grant access to `mssql` user, run the following command:

  ```bash
  sudo mkdir /var/opt/mssql/data/ReplData/
  sudo chown mssql /var/opt/mssql/data/ReplData/
  sudo chgrp mssql /var/opt/mssql/data/ReplData/
  ```

## Configure and monitor replication with SQL Server Management Studio (SSMS)

### Configure the distributor
  
To configure the distributor: 

1. On SSMS connect to your instance of SQL Server in Object Explorer.

1. Right-click **Replication**, and click **Configure Distribution...**.

1. Follow the instructions on the **Configure Distribution Wizard**.

### Create publication and articles

To create a publication and articles:

1. In Object Explorer, click **Replication** > **Local Publications**> **New Publication...**.

1. Follow the instruction on the **New Publication Wizard** to configure the type of replication, and the articles that belong to the publication.

### Configure the subscription

To configure the subscription in Object Explorer, click **Replication** > **Local Subscriptions**> **New subscriptions...**.

### Monitor replication jobs

Use Replication Monitor to monitor replication jobs.

In Object Explorer, right-click **Replication**, and click **Launch Replication Monitor**.

## Next steps

[Concepts: SQL Server replication on Linux](sql-server-linux-replication.md)

[Replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).
