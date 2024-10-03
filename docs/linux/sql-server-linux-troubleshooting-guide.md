---
title: Troubleshoot SQL Server on Linux
description: Troubleshoot SQL Server running on Linux or in a Linux container. Learn where to find information about supported features and known limitations.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/15/2024
ms.service: sql
ms.subservice: linux
ms.topic: troubleshooting
ms.custom:
  - linux-related-content
---
# Troubleshoot SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to troubleshoot [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] running on Linux or in a Linux container. When troubleshooting [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux, remember to review the supported features and known limitations:

- [Release notes for SQL Server 2022 on Linux](sql-server-linux-release-notes-2022.md)
- [Release notes for SQL Server 2019 on Linux](sql-server-linux-release-notes-2019.md)
- [Release notes for SQL Server 2017 on Linux](sql-server-linux-release-notes-2017.md)

For answers to frequently asked questions, see the [SQL Server on Linux FAQ](sql-server-linux-faq.yml).

## <a id="connection"></a> Troubleshoot connection failures

If you have difficulty connecting to your Linux [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance, there are a few things to check.

- If you're unable to connect locally using `localhost`, try using the IP address 127.0.0.1 instead. It's possible that `localhost` isn't properly mapped to this address.

- Verify that the server name or IP address is reachable from your client machine.

  To find the IP address of your Ubuntu machine, you can run the `ifconfig` command as in the following example:

  ```bash
  sudo ifconfig eth0 | grep 'inet addr'
  ```

  For Red Hat, you can use the `ip addr` command as in the following example:

  ```bash
  sudo ip addr show eth0 | grep "inet"
  ```

  > [!TIP]  
  > One exception to this technique relates to Azure VMs. For Azure VMs, [find the public IP for the VM in the Azure portal](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart#connect).

- If applicable, check that you opened the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] port (default 1433) on the firewall.

- For Azure VMs, check that you have a [network security group rule for the default SQL Server port](/azure/azure-sql/virtual-machines/linux/sql-vm-create-portal-quickstart#remote).

- Verify that the user name and password don't contain any typos, extra spaces, or incorrect casing.

- Try to explicitly set the protocol and port number with the server name like the following example: `tcp:servername,1433`.

- Network connectivity issues can also cause connection errors and timeouts. After verifying your connection information and network connectivity, try the connection again.

## Manage the SQL Server service

The following section shows how to manage the execution of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Linux containers. To manage services for Linux, see [Start, stop, and restart SQL Server services on Linux](sql-server-linux-start-stop-restart-sql-server-services.md).

### Manage the execution of the SQL Server Linux container

You can get the status and container ID of the latest created [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Linux container by running the following command (The ID is under the `CONTAINER ID` column):

   ```bash
   sudo docker ps -l
   ```

You can stop or restart the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] service as needed using the following commands:

   ```bash
   sudo docker stop <container ID>
   sudo docker restart <container ID>
   ```

> [!TIP]  
> For more troubleshooting tips for Linux containers, see [Troubleshoot SQL Server Docker containers](sql-server-linux-docker-container-troubleshooting.md).

## Access the log files

The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../includes/ssde-md.md)] logs to the `/var/opt/mssql/log/errorlog` file in both the Linux and container installations. You need to be in **superuser** mode to browse this directory.

The installer logs here: `/var/opt/mssql/setup-<time stamp representing time of install>`
You can browse the `errorlog` files with any UTF-16 compatible tool like **vim** or **cat** like this:

   ```bash
   sudo cat errorlog
   ```

If you prefer, you can also convert the files to UTF-8 to read them with **more** or **less** with the following command:

   ```bash
   sudo iconv -f UTF-16LE -t UTF-8 <errorlog> -o <output errorlog file>
   ```

## Extended events

Extended events can be queried via a SQL command. For more information, see [extended events](../relational-databases/extended-events/extended-events.md).

## Crash dumps

Look for dumps in the log directory in Linux. Check under the `/var/opt/mssql/log` directory for Linux Core dumps (`.tar.gz2` extension) or SQL minidumps (`.mdmp` extension).

For example, to view core dumps:

   ```bash
   sudo ls /var/opt/mssql/log | grep .tar.gz2
   ```

For SQL dumps, use this script:

   ```bash
   sudo ls /var/opt/mssql/log | grep .mdmp
   ```

## Start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in minimal configuration or in single user mode

### Start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in minimal configuration mode

This mode is useful if the setting of a configuration value (for example, over-committing memory) prevents the server from starting.

   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -f
   ```

### Start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in single user mode

Sometimes you might have to start an instance of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in single-user mode by using the startup option `-m`. For more information, see [startup parameters](../database-engine/configure-windows/database-engine-service-startup-options.md#other-startup-options). For example, you might want to change server configuration options or recover a damaged `master` database or other system database.

For example, use the following script to start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in single user mode:

   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -m
   ```

This script starts [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] in single user mode with **sqlcmd**:

   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr -m sqlcmd
   ```

You should always start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux with the `mssql` user to prevent future startup issues. For example: `sudo -u mssql /opt/mssql/bin/sqlservr [STARTUP OPTIONS]`

If you accidentally start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] with another user, you must change ownership of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] database files back to the `mssql` user before you start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] with **systemd**. For example, to change ownership of all database files under `/var/opt/mssql` to the `mssql` user, run the following command:

   ```bash
   chown -R mssql:mssql /var/opt/mssql/
   ```

## Rebuild system databases

As a last resort, you can choose to rebuild the `master` and `model` databases back to default versions.

> [!WARNING]  
> This process is dangerous, because you can **delete all [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] system data** that you have configured, including information about your user databases (but not the user databases themselves).

You need to attach the user databases to the instance afterwards. It also deletes other information stored in the system databases, including:

- database master key (DMK) information
- any certificates loaded in `master`
- the password for the SA login
- job-related information from `msdb`
- Database Mail information from `msdb`
- `sp_configure` options

You aren't able to reattach any user databases encrypted with [transparent data encryption (TDE)](../relational-databases/security/encryption/transparent-data-encryption.md) unless your certificates and private keys are also backed up.

Only use these steps if you understand the implications.

1. Stop [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../includes/ssde-md.md)]

   ```bash
   sudo systemctl stop mssql-server
   ```

1. Run **sqlservr** with the `force-setup` parameter

   ```bash
   sudo -u mssql /opt/mssql/bin/sqlservr --force-setup
   ```

   You should always start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] on Linux with the `mssql` user to prevent future startup issues.

1. After you see the message "Recovery is complete", press **Ctrl+C**. This shuts down [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)].

1. Reconfigure the SA password.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set-sa-password
   ```

1. Start [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] and reconfigure the server, including restoring or reattaching any user databases.

   ```bash
   sudo systemctl start mssql-server
   ```

## Improve performance

Many factors affect performance, including database design, hardware, and workload demands. If you're looking to improve performance, start by reviewing the best practices in the article, [Performance best practices and configuration guidelines for SQL Server on Linux](sql-server-linux-performance-best-practices.md). Then explore some of the available tools for troubleshooting performance problems.

- [Monitor performance by using the Query Store](../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [System dynamic management views](../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [Performance Dashboard in SQL Server Management Studio](/archive/blogs/sql_server_team/new-in-ssms-performance-dashboard-built-in)

## Common issues

1. You can't connect to your remote [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] instance.

   See the troubleshooting section of the article, [Connect to SQL Server on Linux](#connection).

1. You experience the error message: `ERROR: Hostname must be 15 characters or less.`

   This is a known issue that happens whenever the name of the machine that is trying to install the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] package is longer than 15 characters. There are currently no workarounds other than changing the name of the machine. You can do this by editing both `/etc/hostname` and `/etc/hosts`, changing the hostname, saving each file, and restarting the computer.

1. The system administration (SA) password must be reset, which stops the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] service temporarily.

   If you forget the system administrator (SA) password or need to reset it for some other reason, follow these steps.

   Sign in to the host terminal, run the following commands and follow the prompts to reset the SA password:

   ```bash
   sudo systemctl stop mssql-server
   sudo /opt/mssql/bin/mssql-conf setup
   ```

1. Special characters in login passwords cause errors or login failures.

   If you use some characters in the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] login password, you might need to escape them with a backslash when you use them on the Linux command line. For example, you must escape the dollar sign ($) anytime you use it in a terminal command/shell script:

   Doesn't work:

   ```bash
   sudo sqlcmd -S myserver -U sa -P Test$$
   ```

   Does work:

   ```bash
   sqlcmd -S myserver -U sa -P Test\$\$
   ```

## Related content

- [Special characters](https://tldp.org/LDP/abs/html/special-chars.html)
- [Escaping](https://tldp.org/LDP/abs/html/escapingsection.html)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
