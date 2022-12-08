---
title: Start, stop, and restart SQL Server services on Linux
description: Find out how to start, stop, or restart various SQL Server services on Linux. See how to use Transact-SQL and command-line tools for these actions.
ms.date: 02/24/2022
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server on Linux, startup options"
  - "Database Engine [SQL Server], starting and stopping services on Linux"
  - "command line [SQL Server], starting and stopping SQL Server services on Linux"
  - "starting SQL Server Database Engine on Linux"
  - "command prompt [SQL Server] on Linux"
  - "startup options [SQL Server] on Linux"
  - "systemctl commands [SQL Server]"
  - "SQL Server on Linux, starting and stopping"
  - "stopping SQL Server on Linux"
  - "SQL Server Database Engine on Linux setting startup options"
  - "administering SQL Server on Linux, starting and stopping services"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
---

# Start, stop, and restart SQL Server services on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to start, stop, or restart the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] and SQL Server Agent on Linux by using the command line, or Transact-SQL.

For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on Windows, see [Start, stop, pause, resume, and restart SQL Server services](../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).

## Identify the service

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] components are executable programs that run as services (also known as *daemons* on Linux). Linux services can run without displaying any activity on the computer screen and without user interaction on the command line.

### [!INCLUDE[ssDE](../includes/ssde-md.md)] service

The [!INCLUDE[ssDE](../includes/ssde-md.md)] service is the default instance, with a limit of one per computer. Named instances aren't supported on Linux. To run multiple instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on a single computer using containers, see [Deploy and connect to SQL Server Docker containers](sql-server-linux-docker-container-deployment.md).

### SQL Server Agent service

The SQL Server Agent service executes scheduled administrative tasks, which are called jobs and alerts. For more information, see [SQL Server Agent](../ssms/agent/sql-server-agent.md). SQL Server Agent isn't available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2019](../sql-server/editions-and-components-of-sql-server-2019.md).

### Additional Information

- On Linux, you can't pause the [!INCLUDE[ssDE](../includes/ssde-md.md)] service like you can on Windows. The SQL Server Agent service also can't be paused or resumed.

- When running on a cluster, use the appropriate cluster management tool to manage the [!INCLUDE[ssDE](../includes/ssde-md.md)] for your Linux distribution. See [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md) for an example using Pacemaker.

### Permissions

By default, only members of the local administrator group can start, stop, or restart a service.

Stopping the [!INCLUDE[ssDE](../includes/ssde-md.md)] by using the Transact-SQL **SHUTDOWN** command requires membership in the **sysadmin** or **serveradmin** fixed server roles, and is not transferable.

## <a name="CommandLine"></a> Using command-line tools

The following steps show how to start, stop, restart, and check the status of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] service on Linux. To manage a SQL Server Docker container, see [Troubleshoot SQL Server on Linux](sql-server-linux-troubleshooting-guide.md).

Check the status of the [!INCLUDE[ssDE](../includes/ssde-md.md)] service using this command:

   ```bash
   sudo systemctl status mssql-server
   ```

You can stop, start, or restart the [!INCLUDE[ssDE](../includes/ssde-md.md)] service as needed using the following commands:

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl start mssql-server
   sudo systemctl restart mssql-server
   ```

To set up and manage the SQL Server Agent, see [Install SQL Server Agent on Linux](sql-server-linux-setup-sql-agent.md). To restart the SQL Server Agent service, you must restart the [!INCLUDE[ssDE](../includes/ssde-md.md)] service.

## <a name="TsqlProcedure"></a> Transact-SQL

The [!INCLUDE[ssDE](../includes/ssde-md.md)] can be stopped by using the **SHUTDOWN** statement.

### To stop the [!INCLUDE[ssDE](../includes/ssde-md.md)] using Transact-SQL

- To wait for currently running Transact-SQL statements and stored procedures to finish, and then stop the [!INCLUDE[ssDE](../includes/ssde-md.md)], execute the following statement.  
  
    ```sql
    SHUTDOWN;
    ```
  
- To stop the [!INCLUDE[ssDE](../includes/ssde-md.md)] immediately, execute the following statement.  
  
    ```sql
    SHUTDOWN WITH NOWAIT;
    ```

For more information about the **SHUTDOWN** statement, see [SHUTDOWN &#40;Transact-SQL&#41;](../t-sql/language-elements/shutdown-transact-sql.md).
  
## Next steps

- [Installation guidance for SQL Server on Linux](sql-server-linux-setup.md)
- [Troubleshoot SQL Server on Linux](sql-server-linux-troubleshooting-guide.md)
- [Install the SQL Server command-line tools sqlcmd and bcp on Linux](sql-server-linux-setup-tools.md)
