---
title: Configure SQL Server settings on Linux
description: This article describes how to use the mssql-conf tool to configure SQL Server settings on Linux.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/24/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Configure SQL Server on Linux with the mssql-conf tool

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

**mssql-conf** is a configuration script that installs with [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] for Red Hat Enterprise Linux, SUSE Linux Enterprise Server, and Ubuntu. It modifies the [mssql.conf file](#mssql-conf-format) where configuration values are stored. You can use **mssql-conf** utility to set the following parameters:

| Parameter | Description |
| --- | --- |
| [Agent](#agent) | Enable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent. |
| [Authenticate with Windows](#windows-active-directory) | Settings for Windows Server Active Directory authentication. |
| [Collation](#collation) | Set a new collation for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Customer feedback](#customerfeedback) | Choose whether or not [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sends feedback to Microsoft. |
| [Database Mail Profile](#dbmail) | Set the default database mail profile for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Default data directory](#datadir) | Change the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database data files (.mdf). |
| [Default log directory](#datadir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database log (.ldf) files. |
| [Default master database directory](#masterdatabasedir) | Changes the default directory for the `master` database and log files. |
| [Default master database file name](#masterdatabasename) | Changes the name of `master` database files. |
| [Default dump directory](#dumpdir) | Change the default directory for new memory dumps and other troubleshooting files. |
| [Default error log directory](#errorlogdir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Error Log, Default Profiler Trace, System Health Session XE, and Hekaton Session XE files. |
| [Default backup directory](#backupdir) | Change the default directory for new backup files. |
| [Dump type](#coredump) | Choose the type of dump memory dump file to collect. |
| [Edition](#edition) | Set the edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [High availability](#hadr) | Enable Availability Groups. |
| [Local Audit directory](#localaudit) | Set a directory to add Local Audit files. |
| [Locale](#lcid) | Set the locale for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use. |
| [Memory limit](#memorylimit) | Set the memory limit for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [Network settings](#network) | Additional network settings for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [Microsoft Distributed Transaction Coordinator](#msdtc) | Configure and troubleshoot MSDTC on Linux. |
| [TCP port](#tcpport) | Change the port where [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] listens for connections. |
| [TLS](#tls) | Configure Transport Level Security. |
| [Trace flags](#traceflags) | Set the trace flags that the service is going to use. |

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range="= sql-server-linux-ver15 || = sql-server-ver15"

**mssql-conf** is a configuration script that installs with [!INCLUDE[SQL Server 2019](../includes/sssql19-md.md)] for Red Hat Enterprise Linux, SUSE Linux Enterprise Server, and Ubuntu. You can use this utility to set the following parameters:

| Parameter | Description |
| --- | --- |
| [Agent](#agent) | Enable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent |
| [Authenticate with Windows](#windows-active-directory) | Settings for Windows Server Active Directory authentication. |
| [Collation](#collation) | Set a new collation for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Customer feedback](#customerfeedback) | Choose whether or not [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sends feedback to Microsoft. |
| [Database Mail Profile](#dbmail) | Set the default database mail profile for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Default data directory](#datadir) | Change the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database data files (.mdf). |
| [Default log directory](#datadir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database log (.ldf) files. |
| [Default master database file directory](#masterdatabasedir) | Changes the default directory for the `master` database files on existing SQL installation. |
| [Default master database file name](#masterdatabasename) | Changes the name of `master` database files. |
| [Default dump directory](#dumpdir) | Change the default directory for new memory dumps and other troubleshooting files. |
| [Default error log directory](#errorlogdir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Error Log, Default Profiler Trace, System Health Session XE, and Hekaton Session XE files. |
| [Default backup directory](#backupdir) | Change the default directory for new backup files. |
| [Dump type](#coredump) | Choose the type of dump memory dump file to collect. |
| [Edition](#edition) | Set the edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [High availability](#hadr) | Enable Availability Groups. |
| [Local Audit directory](#localaudit) | Set a directory to add Local Audit files. |
| [Locale](#lcid) | Set the locale for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use. |
| [Memory limit](#memorylimit) | Set the memory limit for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [Microsoft Distributed Transaction Coordinator](#msdtc) | Configure and troubleshoot MSDTC on Linux. |
| [MLServices EULAs](#mlservices-eula) | Accept R and Python EULAs for mlservices packages. Applies to [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] only. |
| [Network settings](#network) | Additional network settings for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [outboundnetworkaccess](#mlservices-outbound-access) | Enable outbound network access for [mlservices](sql-server-linux-setup-machine-learning.md) R, Python, and Java extensions. |
| [TCP port](#tcpport) | Change the port where [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] listens for connections. |
| [TLS](#tls) | Configure Transport Level Security. |
| [Trace flags](#traceflags) | Set the trace flags that the service is going to use. |

::: moniker-end
<!--SQL Server 2022 on Linux-->
::: moniker range=">= sql-server-linux-ver16 || >= sql-server-ver16"

**mssql-conf** is a configuration script that installs with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] for Red Hat Enterprise Linux, and Ubuntu. You can use this utility to set the following parameters:

| Parameter | Description |
| --- | --- |
| [Agent](#agent) | Enable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent |
| [Authenticate with Azure AD](#azure-ad) | Settings for Azure Active Directory authentication. |
| [Authenticate with Windows](#windows-active-directory) | Settings for Windows Server Active Directory authentication. |
| [Collation](#collation) | Set a new collation for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Customer feedback](#customerfeedback) | Choose whether or not [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sends feedback to Microsoft. |
| [Database Mail Profile](#dbmail) | Set the default database mail profile for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. |
| [Default data directory](#datadir) | Change the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database data files (.mdf). |
| [Default log directory](#datadir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] database log (.ldf) files. |
| [Default master database file directory](#masterdatabasedir) | Changes the default directory for the `master` database files on existing SQL installation. |
| [Default master database file name](#masterdatabasename) | Changes the name of `master` database files. |
| [Default dump directory](#dumpdir) | Change the default directory for new memory dumps and other troubleshooting files. |
| [Default error log directory](#errorlogdir) | Changes the default directory for new [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Error Log, Default Profiler Trace, System Health Session XE, and Hekaton Session XE files. |
| [Default backup directory](#backupdir) | Change the default directory for new backup files. |
| [Dump type](#coredump) | Choose the type of dump memory dump file to collect. |
| [Edition](#edition) | Set the edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [High availability](#hadr) | Enable Availability Groups. |
| [Local Audit directory](#localaudit) | Set a directory to add Local Audit files. |
| [Locale](#lcid) | Set the locale for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to use. |
| [Memory limit](#memorylimit) | Set the memory limit for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [Microsoft Distributed Transaction Coordinator](#msdtc) | Configure and troubleshoot MSDTC on Linux. |
| [MLServices EULAs](#mlservices-eula) | Accept R and Python EULAs for mlservices packages. Applies to [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] only. |
| [Network settings](#network) | Additional network settings for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. |
| [Outbound network access](#mlservices-outbound-access) | Enable outbound network access for [MLServices](sql-server-linux-setup-machine-learning.md) R, Python, and Java extensions. |
| [TCP port](#tcpport) | Change the port where [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] listens for connections. |
| [TLS](#tls) | Configure Transport Level Security. |
| [Trace flags](#traceflags) | Set the trace flags that the service is going to use. |

::: moniker-end

> [!TIP]  
> Some of these settings can also be configured with environment variables. For more information, see [Configure SQL Server settings with environment variables](sql-server-linux-configure-environment-variables.md).

## Usage tips

- For Always On Availability Groups and shared disk clusters, always make the same configuration changes on each node.

- For the shared disk cluster scenario, don't attempt to restart the `mssql-server` service to apply changes. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is running as an application. Instead, take the resource offline and then back online.

- These examples run **mssql-conf** by specifying the full path: `/opt/mssql/bin/mssql-conf`. If you choose to navigate to that path instead, run **mssql-conf** in the context of the current directory: `./mssql-conf`.

- If you want to modify the `mssql.conf` file inside of a container, create a `mssql.conf` file on the host where you have the container running with your desired settings, and then redeploy your container. For example, the following addition to the `mssql.conf` file enables [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent.

  ```ini
  [sqlagent]
  enabled = true
  ```

  You can deploy your container with the following commands:

  ```bash
  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
  -p 5433:1433 --name sql1 \
  -v /container/sql1:/var/opt/mssql \
  -d mcr.microsoft.com/mssql/server:2019-latest
  ```

  For more information, see [Create the config files to be used by the SQL Server container](sql-server-linux-containers-ad-auth-adutil-tutorial.md#create-the-config-files-to-be-used-by-the-sql-server-container).

## <a id="agent"></a> Enable SQL Server Agent

The `sqlagent.enabled` setting enables [SQL Server Agent](sql-server-linux-run-sql-server-agent-job.md). By default, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent is disabled. If `sqlagent.enabled` isn't present in the mssql.conf settings file, then [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] internally assumes that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent is disabled.

To change this setting, use the following steps:

1. Enable the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Agent:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set sqlagent.enabled true
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

### <a id="dbmail"></a> Set the default database mail profile for SQL Server on Linux

The `sqlagent.databasemailprofile` allows you to set the default DB Mail profile for email alerts.

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.databasemailprofile <profile_name>
```

### <a id="agenterrorlog"></a> SQL Agent error logs

The `sqlagent.errorlogfile` and `sqlagent.errorlogginglevel` settings allows you to set the SQL Agent log file path and logging level respectively.

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.errorfile <path>
```

SQL Agent logging levels are bitmask values that equal:

- 1 = Errors
- 2 = Warnings
- 4 = Info

If you want to capture all levels, use `7` as the value.

```bash
sudo /opt/mssql/bin/mssql-conf set sqlagent.errorlogginglevel <level>
```

## <a id="azure-ad"></a> Configure Azure Active Directory authentication

Starting with [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], you can configure Azure Active Directory (Azure AD) for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. To configure Azure AD, you must install the Azure extension for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] following the installation of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For information on how to configure Azure AD, see [Tutorial: Set up Azure Active Directory authentication for SQL Server](../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-setup-tutorial.md).

### Change the default Azure AD certificate path

By default, the Azure AD certificate file is stored in `/var/opt/mssql/aadsecrets/`. You can change this path if you use a certificate store or an encrypted drive. To change the path, you can use the following command:

```bash
sudo /opt/mssql/bin/mssql-conf set network.aadcertificatefilepath /path/to/new/location.pfx
```

In the previous example, `/path/to/new/location.pfx` is your preferred path *including* the certificate name.

The certificate for Azure AD authentication, downloaded by the Azure extension for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], is stored at this location. You won't be able to change it to `/var/opt/mssql/secrets`.

> [!NOTE]  
> The default Azure AD certificate path can be changed at any time after [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is installed, but must be changed *before* enabling Azure AD.

### Azure AD configuration options

The following options are used by Azure AD authentication for an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on Linux.

> [!WARNING]  
> Azure AD parameters are configured by the Azure extension for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], and should not be reconfigured manually. They are listed here for information purposes.

| Option | Description |
| --- | --- |
| `network.aadauthenticationendpoint` | Endpoint for Azure AD authentication |
| `network.aadcertificatefilepath` | Path to certificate file for authenticating to Azure AD |
| `network.aadclientcertblacklist` | Azure AD Client Certificate blocklist |
| `network.aadclientid` | Azure AD Client GUID |
| `network.aadfederationmetadataendpoint` | Endpoint for Azure AD Federation Metadata |
| `network.aadgraphapiendpoint` | Endpoint for Azure AD Graph API |
| `network.aadgraphendpoint` | Azure AD Graph Endpoint |
| `network.aadissuerurl` | Azure AD Issuer URL |
| `network.aadmsgraphendpoint` | Azure AD MS Graph Endpoint |
| `network.aadonbehalfofauthority` | Azure AD On Behalf of Authority |
| `network.aadprimarytenant` | Azure AD Primary Tenant GUID |
| `network.aadsendx5c` | Azure AD Send X5C |
| `network.aadserveradminname` | Name of the Azure AD account that will be made sysadmin |
| `network.aadserveradminsid` | SID of the Azure AD account that will be made sysadmin |
| `network.aadserveradmintype` | Type of the Azure AD account that will be made sysadmin |
| `network.aadserviceprincipalname` | Azure AD Service Principal Name |
| `network.aadserviceprincipalnamenoslash` | Azure AD Service Principal Name, with no slash |
| `network.aadstsurl` | Azure AD STS URL |

## <a id="windows-active-directory"></a> Configure Windows Active Directory authentication

The `setup-ad-keytab` option can be used to create a keytab, but the user and Service Principal Names (SPNs) must have been created to use this option. The Active Directory utility, [**adutil**](sql-server-linux-ad-auth-adutil-introduction.md) can be used to create users, SPNs, and keytabs.

For options on using `setup-ad-keytab`, run the following command:

```bash
sudo /opt/mssql/bin/mssql-conf setup-ad-keytab --help
```

The `validate-ad-config` option will validate the configuration for Active Directory authentication.

## <a id="collation"></a> Change the SQL Server collation

The `set-collation` option changes the collation value to any of the supported collations. To make this change, the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service needs to be stopped.

1. First [backup any user databases](sql-server-linux-backup-and-restore-database.md) on your server.

1. Then use the [sp_detach_db](../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md) stored procedure to detach the user databases.

1. Run the `set-collation` option and follow the prompts:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set-collation
   ```

1. The **mssql-conf** utility will attempt to change to the specified collation value and restart the service. If there are any errors, it rolls back the collation to the previous value.

1. Restore your user database backups.

For a list of supported collations, run the [sys.fn_helpcollations](../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md) function: `SELECT Name from sys.fn_helpcollations()`.

## <a id="customerfeedback"></a> Configure customer feedback

The `telemetry.customerfeedback` setting changes whether [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] sends feedback to Microsoft or not. By default, this value is set to `true` for all editions. To change the value, run the following commands:

> [!IMPORTANT]  
> You can not turn off customer feedback for free editions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], Express and Developer.

1. Run the **mssql-conf** script as root with the `set` command for `telemetry.customerfeedback`. The following example turns off customer feedback by specifying `false`.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.customerfeedback false
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

For more information, see [Customer Feedback for SQL Server on Linux](./usage-and-diagnostic-data-configuration-for-sql-server-linux.md) and the [SQL Server Privacy Statement](../sql-server/sql-server-privacy.md).

## <a id="datadir"></a> Change the default data or log directory location

The `filelocation.defaultdatadir` and `filelocation.defaultlogdir` settings change the location where the new database and log files are created. By default, this location is `/var/opt/mssql/data`. To change these settings, use the following steps:

1. Create the target directory for new database data and log files. The following example creates a new `/tmp/data` directory:

   ```bash
   sudo mkdir /tmp/data
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/data
   sudo chgrp mssql /tmp/data
   ```

1. Use **mssql-conf** to change the default data directory with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultdatadir /tmp/data
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

1. Now all the database files for the new databases created will be stored in this new location. If you would like to change the location of the log (.ldf) files of the new databases, you can use the following `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultlogdir /tmp/log
   ```

1. This command also assumes that a /tmp/log directory exists, and that it is under the user and group `mssql`.

## <a id="masterdatabasedir"></a> Change the default `master` database file directory location

The `filelocation.masterdatafile` and `filelocation.masterlogfile` setting changes the location where the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] [!INCLUDE [ssde-md](../includes/ssde-md.md)] looks for the `master` database files. By default, this location is `/var/opt/mssql/data`.

To change these settings, use the following steps:

1. Create the target directory for new error log files. The following example creates a new `/tmp/masterdatabasedir` directory:

   ```bash
   sudo mkdir /tmp/masterdatabasedir
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/masterdatabasedir
   sudo chgrp mssql /tmp/masterdatabasedir
   ```

1. Use **mssql-conf** to change the default `master` database directory for the master data and log files with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.masterdatafile /tmp/masterdatabasedir/master.mdf
   sudo /opt/mssql/bin/mssql-conf set filelocation.masterlogfile /tmp/masterdatabasedir/mastlog.ldf
   ```

   > [!NOTE]  
   > In addition to moving the master data and log files, this also moves the default location for all other system databases.

1. Stop the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl stop mssql-server
   ```

1. Move the `master.mdf` and `mastlog.ldf` files:

   ```bash
   sudo mv /var/opt/mssql/data/master.mdf /tmp/masterdatabasedir/master.mdf
   sudo mv /var/opt/mssql/data/mastlog.ldf /tmp/masterdatabasedir/mastlog.ldf
   ```

1. Start the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl start mssql-server
   ```

   > [!NOTE]  
   > If [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] cannot find `master.mdf` and `mastlog.ldf` files in the specified directory, a templated copy of the system databases will be automatically created in the specified directory, and [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] will successfully start up. However, metadata such as user databases, server logins, server certificates, encryption keys, SQL agent jobs, or old SA login password will not be updated in the new `master` database. You will have to stop [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and move your old `master.mdf` and `mastlog.ldf` to the new specified location and start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to continue using the existing metadata.

## <a id="masterdatabasename"></a> Change the name of `master` database files

The `filelocation.masterdatafile` and `filelocation.masterlogfile` setting changes the location where the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] [!INCLUDE [ssde-md](../includes/ssde-md.md)] looks for the `master` database files. You can also use this to change the name of the `master` database and log files.

To change these settings, use the following steps:

1. Stop the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl stop mssql-server
   ```

1. Use **mssql-conf** to change the expected `master` database names for the `master` data and log files with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.masterdatafile /var/opt/mssql/data/masternew.mdf
   sudo /opt/mssql/bin/mssql-conf set filelocation.mastlogfile /var/opt/mssql/data/mastlognew.ldf
   ```

   > [!IMPORTANT]  
   > You can only change the name of the `master` database and log files after [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] has started successfully. Before the initial run, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] expects the files to be named `master.mdf` and `mastlog.ldf`.

1. Change the name of the `master` database data and log files:

   ```bash
   sudo mv /var/opt/mssql/data/master.mdf /var/opt/mssql/data/masternew.mdf
   sudo mv /var/opt/mssql/data/mastlog.ldf /var/opt/mssql/data/mastlognew.ldf
   ```

1. Start the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl start mssql-server
   ```

## <a id="dumpdir"></a> Change the default dump directory location

The `filelocation.defaultdumpdir` setting changes the default location where the memory and SQL dumps are generated whenever there's a crash. By default, these files are generated in `/var/opt/mssql/log`.

To set up this new location, use the following commands:

1. Create the target directory for new dump files. The following example creates a new `/tmp/dump` directory:

   ```bash
   sudo mkdir /tmp/dump
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/dump
   sudo chgrp mssql /tmp/dump
   ```

1. Use **mssql-conf** to change the default data directory with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultdumpdir /tmp/dump
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="errorlogdir"></a> Change the default error log file directory location

The `filelocation.errorlogfile` setting changes the location where the new error log, default profiler trace, system health session XE and Hekaton session XE files are created. By default, this location is `/var/opt/mssql/log`. The directory in which the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] error log file is set, becomes the default log directory for other logs.

To change these settings:

1. Create the target directory for new error log files. The following example creates a new `/tmp/logs` directory:

   ```bash
   sudo mkdir /tmp/logs
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/logs
   sudo chgrp mssql /tmp/logs
   ```

1. Use **mssql-conf** to change the default error log filename with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.errorlogfile /tmp/logs/errorlog
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

The `errorlog.numerrorlogs` setting will allow you to specify the number of error logs maintained before cycling the log.

## <a id="backupdir"></a> Change the default backup directory location

The `filelocation.defaultbackupdir` setting changes the default location where the backup files are generated. By default, these files are generated in `/var/opt/mssql/data`.

To set up this new location, use the following commands:

1. Create the target directory for new backup files. The following example creates a new `/tmp/backup` directory:

   ```bash
   sudo mkdir /tmp/backup
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/backup
   sudo chgrp mssql /tmp/backup
   ```

1. Use **mssql-conf** to change the default backup directory with the `set` command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultbackupdir /tmp/backup
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="coredump"></a> Specify core dump settings

If an exception or crash occurs in one of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] processes, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] creates a memory dump. Capturing a memory dump may take a long time and take up significant space. To save resources and avoid repeated memory dumps, you can disable automatic dump capture using the `coredump.disablecoredump` option.

```bash
sudo /opt/mssql/bin/mssql-conf set coredump.disablecoredump <true or false>
```

Users can still generate memory dumps manually when automatic core dump is disabled (`coredump.disablecoredump` set to `true`).

There are two options for controlling the type of memory dumps that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] collects: `coredump.coredumptype` and `coredump.captureminiandfull`. These relate to the two phases of core dump capture.

The first phase capture is controlled by the `coredump.coredumptype` setting, which determines the type of dump file generated during an exception. The second phase is enabled when the `coredump.captureminiandfull` setting. If `coredump.captureminiandfull` is set to true, the dump file specified by `coredump.coredumptype` is generated, and a second mini dump is also generated. Setting `coredump.captureminiandfull` to false disables the second capture attempt.

1. Decide whether to capture both mini and full dumps with the `coredump.captureminiandfull` setting.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set coredump.captureminiandfull <true or false>
    ```

    Default: `false`

1. Specify the type of dump file with the `coredump.coredumptype` setting.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set coredump.coredumptype <dump_type>
    ```

    Default: `miniplus`

    The following table lists the possible `coredump.coredumptype` values.

    | Type | Description |
    | --- | --- |
    | `mini` | Mini is the smallest dump file type. It uses the Linux system information to determine threads and modules in the process. The dump contains only the host environment thread stacks and modules. It doesn't contain indirect memory references or globals. |
    | `miniplus` | MiniPlus is similar to mini, but it includes additional memory. It understands the internals of SQLPAL and the host environment, adding the following memory regions to the dump:<br /><br />- Various globals<br />- All memory above 64 TB<br />- All named regions found in `/proc/$pid/maps`<br />- Indirect memory from threads and stacks<br />- Thread information, including associated thread environment blocks (TEBs) and process environment blocks (PEBs)<br />- Module information<br />- VMM and VAD tree |
    | `filtered` | Filtered uses a subtraction-based design where all memory in the process is included unless specifically excluded. The design understands the internals of SQLPAL and the host environment, excluding certain regions from the dump.
    | `full` | Full is a complete process dump that includes all regions located in `/proc/$pid/maps`. This isn't controlled by the `coredump.captureminiandfull` setting. |

## <a id="edition"></a> Edition

The edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] can be changed using the `set-edition` option. To change the edition of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service first needs to be stopped. For more information on available [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux editions, see [SQL Server editions](sql-server-linux-editions-and-components-2019.md#-editions)

## <a id="hadr"></a> High availability

The `hadr.hadrenabled` option enables availability groups on your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance. The following command enables availability groups by setting `hadr.hadrenabled` to 1. You must restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] for the setting to take effect.

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
sudo systemctl restart mssql-server
```

For information on how this is used with availability groups, see the following two articles.

- [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md)
- [Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md)

## <a id="localaudit"></a> Set local audit directory

The `telemetry.userrequestedlocalauditdirectory` setting enables Local Audit and lets you set the directory where the Local Audit logs are created.

1. Create a target directory for new Local Audit logs. The following example creates a new `/tmp/audit` directory:

   ```bash
   sudo mkdir /tmp/audit
   ```

1. Change the owner and group of the directory to the `mssql` user:

   ```bash
   sudo chown mssql /tmp/audit
   sudo chgrp mssql /tmp/audit
   ```

1. Run the **mssql-conf** script as root with the `set` command for `telemetry.userrequestedlocalauditdirectory`:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.userrequestedlocalauditdirectory /tmp/audit
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

For more information, see [Customer Feedback for SQL Server on Linux](./usage-and-diagnostic-data-configuration-for-sql-server-linux.md).

## <a id="lcid"></a> Change the SQL Server locale

The `language.lcid` setting changes the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] locale to any supported language identifier (LCID).

1. The following example changes the locale to French (1036):

   ```bash
   sudo /opt/mssql/bin/mssql-conf set language.lcid 1036
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="memorylimit"></a> Set the memory limit

The `memory.memorylimitmb` setting controls the amount of physical memory (in MB) available to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. The default is 80% of the physical memory, to prevent out-of-memory (OOM) conditions.

> [!IMPORTANT]
> The `memory.memorylimitmb` setting limits the amount of *physical memory* available to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] process. The **max server memory (MB)** setting can be used to adjust the amount of memory available to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] *buffer pool*, but it can never exceed the amount of physical memory available to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. For more information about the **max server memory (MB)** server configuration option, see [Server memory configuration options](../database-engine/configure-windows/server-memory-server-configuration-options.md).

1. Run the **mssql-conf** script as root with the `set` command for `memory.memorylimitmb`. The following example changes the memory available to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] to 3.25 GB (3328 MB).

   ```bash
   sudo /opt/mssql/bin/mssql-conf set memory.memorylimitmb 3328
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

### Additional memory settings

The following options are available to the memory settings.

| Option | Description |
| --- | --- |
| `memory.disablememorypressure` | [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] disable memory pressure. Values can be `true` or `false`. |
| `memory.memory_optimized` | Enable or disable [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] memory optimized features - persistent memory file enlightenment, memory protection. Values can be `true` or `false`. |
| `memory.enablecontainersharedmemory` | Applicable for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers only. Use this setting to enable shared memory inside [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers. By default, this is set to `false`. Values can be `true` or `false`. |

## <a id="msdtc"></a> Configure MSDTC

The `network.rpcport` and `distributedtransaction.servertcpport` settings are used to configure the Microsoft Distributed Transaction Coordinator (MSDTC). To change these settings, run the following commands:

1. Run the **mssql-conf** script as root with the `set` command for `network.rpcport`:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.rpcport <rcp_port>
   ```

1. Then set the `distributedtransaction.servertcpport` setting:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set distributedtransaction.servertcpport <servertcpport_port>
   ```

In addition to setting these values, you must also configure routing and update the firewall for port 135. For more information on how to do this, see [How to configure MSDTC on Linux](sql-server-linux-configure-msdtc.md).

There are several other settings for **mssql-conf** that you can use to monitor and troubleshoot MSDTC. The following table briefly describes these settings. For more information on their use, see the details in the Windows support article, [How to enable diagnostic tracing for MS DTC](https://support.microsoft.com/help/926099/how-to-enable-diagnostic-tracing-for-ms-dtc-on-a-windows-based-compute).

| Option | Description |
| --- | --- |
| `distributedtransaction.allowonlysecurerpccalls` | Configure secure only RPC calls for distributed transactions |
| `distributedtransaction.fallbacktounsecurerpcifnecessary` | Configure security only RPC calls for distributed transactions |
| `distributedtransaction.maxlogsize` | DTC transaction log file size in MB. Default is 64 MB |
| `distributedtransaction.memorybuffersize` | Circular buffer size in which traces are stored. This size is in MB and default is 10 MB |
| `distributedtransaction.servertcpport` | MSDTC rpc server port |
| `distributedtransaction.trace_cm` | Traces in the connection manager |
| `distributedtransaction.trace_contact` | Traces the contact pool and contacts |
| `distributedtransaction.trace_gateway` | Traces Gateway source |
| `distributedtransaction.trace_log` | Log tracing |
| `distributedtransaction.trace_misc` | Traces that can't be categorized into the other categories |
| `distributedtransaction.trace_proxy` | Traces that are generated in the MSDTC proxy |
| `distributedtransaction.trace_svc` | Traces service and .exe file startup |
| `distributedtransaction.trace_trace` | The trace infrastructure itself |
| `distributedtransaction.trace_util` | Traces utility routines that are called from multiple locations |
| `distributedtransaction.trace_xa` | XA Transaction Manager (XATM) tracing source |
| `distributedtransaction.tracefilepath` | Folder in which trace files should be stored |
| `distributedtransaction.turnoffrpcsecurity` | Enable or disable RPC security for distributed transactions |

::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"

## <a id="mlservices-eula"></a> Accept MLServices EULAs

Adding [machine learning R or Python packages](sql-server-linux-setup-machine-learning.md) to the [!INCLUDE [ssde-md](../includes/ssde-md.md)] requires that you accept the licensing terms for open-source distributions of R and Python. The following table enumerates all available commands or options related to mlservices EULAs. The same EULA parameter is used for R and Python, depending on what you installed.

```bash
# For all packages: database engine and mlservices
# Setup prompts for mlservices EULAs, which you need to accept
sudo /opt/mssql/bin/mssql-conf setup

# Add R or Python to an existing installation
sudo /opt/mssql/bin/mssql-conf setup accept-eula-ml

# Alternative valid syntax
# Adds the EULA section to the INI and sets acceptulam to yes
sudo /opt/mssql/bin/mssql-conf set EULA accepteulaml Y

# Rescind EULA acceptance and removes the setting
sudo /opt/mssql/bin/mssql-conf unset EULA accepteulaml
```

You can also add EULA acceptance directly to the [mssql.conf file](#mssql-conf-format):

```ini
[EULA]
accepteula = Y
accepteulaml = Y
```

:::moniker-end
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"

## <a id="mlservices-outbound-access"></a> Enable outbound network access

Outbound network access for R, Python, and Java extensions in the [SQL Server Machine Learning Services](sql-server-linux-setup-machine-learning.md) feature is disabled by default. To enable outbound requests, set the `outboundnetworkaccess` Boolean property using mssql-conf.

After setting the property, restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Launchpad service to read the updated values from the INI file. A restart message reminds you whenever an extensibility-related setting is modified.

```bash
# Adds the extensibility section and property.
# Sets "outboundnetworkaccess" to true.
# This setting is required if you want to access data or operations off the server.
sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 1

# Turns off network access but preserves the setting
sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 0

# Removes the setting and rescinds network access
sudo /opt/mssql/bin/mssql-conf unset extensibility.outboundnetworkaccess
```

You can also add `outboundnetworkaccess` directly to the [mssql.conf file](#mssql-conf-format):

```ini
[extensibility]
outboundnetworkaccess = 1
```

:::moniker-end

## <a id="tcpport"></a> Change the TCP port

The `network.tcpport` setting changes the TCP port where [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] listens for connections. By default, this port is set to 1433. To change the port, run the following commands:

1. Run the **mssql-conf** script as root with the `set` command for `network.tcpport`:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.tcpport <new_tcp_port>
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service:

   ```bash
   sudo systemctl restart mssql-server
   ```

1. When connecting to [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] now, you must specify the custom port with a comma (,) after the hostname or IP address. For example, to connect with **sqlcmd**, you would use the following command:

   ```bash
   sqlcmd -S localhost,<new_tcp_port> -U test -P test
   ```

## <a id="tls"></a> Specify TLS settings

The following options configure TLS for an instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] running on Linux.

| Option | Description |
| --- | --- |
| `network.forceencryption` | If 1, then [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] forces all connections to be encrypted. By default, this option is 0. |
| `network.tlscert` | The absolute path to the certificate file that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses for TLS. Example: `/etc/ssl/certs/mssql.pem` The certificate file must be accessible by the mssql account. Microsoft recommends restricting access to the file using `chown mssql:mssql <file>; chmod 400 <file>`. |
| `network.tlskey` | The absolute path to the private key file that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses for TLS. Example: `/etc/ssl/private/mssql.key` The certificate file must be accessible by the mssql account. Microsoft recommends restricting access to the file using `chown mssql:mssql <file>; chmod 400 <file>`. |
| `network.tlsprotocols` | A comma-separated list of which TLS protocols are allowed by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] always attempts to negotiate the strongest allowed protocol. If a client doesn't support any allowed protocol, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] rejects the connection attempt. For compatibility, all supported protocols are allowed by default (1.2, 1.1, 1.0). If your clients support TLS 1.2, Microsoft recommends allowing only TLS 1.2. |
| `network.tlsciphers` | Specifies which ciphers are allowed by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for TLS. This string must be formatted per [OpenSSL's cipher list format](https://www.openssl.org/docs/manmaster/man1/ciphers.html). In general, you shouldn't need to change this option.<br />By default, the following ciphers are allowed:<br />`ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-RSA-AES128-SHA256:ECDHE-RSA-AES256-SHA384:ECDHE-ECDSA-AES256-SHA:ECDHE-ECDSA-AES128-SHA:ECDHE-RSA-AES256-SHA:ECDHE-RSA-AES128-SHA:AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA` |
| `network.kerberoskeytabfile` | Path to the Kerberos keytab file |

For an example of using the TLS settings, see [Encrypting Connections to SQL Server on Linux](sql-server-linux-encrypted-connections.md).

## <a id="network"></a> Network settings

See [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md) for comprehensive information on using Active Directory authentication with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

The following options are additional network settings configurable using **mssql-conf**.

| Option | Description |
| --- | --- |
| `network.disablesssd` | Disable querying SSSD for Active Directory account information and default to LDAP calls. Values can be `true` or `false`. |
| `network.enablekdcfromkrb5conf` | Enable looking up KDC information from krb5.conf. Values can be `true` or `false`. |
| `network.forcesecureldap` | Force using LDAPS to contact domain controller. Values can be `true` or `false`. |
| `network.ipaddress` | IP address for incoming connections. |
| `network.kerberoscredupdatefrequency` | Time in seconds between checks for kerberos credentials that need to be updated. Value is an integer. |
| `network.privilegedadaccount` | Privileged Active Directory user to use for Active Directory authentication. Value is `<username>`. For more information, see [Tutorial: Use Active Directory authentication with SQL Server on Linux](sql-server-linux-active-directory-authentication.md#spn) |
| `uncmapping` | Maps UNC path to a local path. For example, `sudo /opt/mssql/bin/mssql-conf set uncmapping //servername/sharename /tmp/folder`. |
| `ldaphostcanon` | Set whether OpenLDAP should canonicalize hostnames during the bind step. Values can be `true` or `false`. |

## <a id="traceflags"></a> Enable or disable trace flags

The `traceflag` option enables or disables trace flags for the startup of the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service. To enable/disable a trace flag, use the following commands:

1. Enable a trace flag using the following command. For example, for Trace Flag 1234:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 1234 on
   ```

1. You can enable multiple trace flags by specifying them separately:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 2345 3456 on
   ```

1. In a similar way, you can disable one or more enabled trace flags by specifying them and adding the `off` parameter:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 1234 2345 3456 off
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

## Remove a setting

To unset any setting made with `mssql-conf set`, call **mssql-conf** with the `unset` option and the name of the setting. This clears the setting, effectively returning it to its default value.

1. The following example clears the `network.tcpport` option.

   ```bash
   sudo /opt/mssql/bin/mssql-conf unset network.tcpport
   ```

1. Restart the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] service.

   ```bash
   sudo systemctl restart mssql-server
   ```

## View current settings

To view any configured settings, run the following command to output the contents of the `mssql.conf` file:

```bash
sudo cat /var/opt/mssql/mssql.conf
```

Any settings not shown in this file are using their default values. The next section provides a sample `mssql.conf` file.

## View various options

To view the various options that can be configured using the **mssql-conf** utility, run the `help` command:

```bash
sudo /opt/mssql/bin/mssql-conf --help
```

The results will give you various configuration options and a short description for each of the settings.

## <a id="mssql-conf-format"></a> mssql.conf format

The following `/var/opt/mssql/mssql.conf` file provides an example for each setting. You can use this format to manually make changes to the `mssql.conf` file as needed. If you do manually change the file, you must restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] before the changes are applied. To use the `mssql.conf` file with Docker, you must have Docker [persist your data](./sql-server-linux-docker-container-deployment.md). First add a complete `mssql.conf` file to your host directory and then run the container. There is an example of this in  [Customer Feedback](./usage-and-diagnostic-data-configuration-for-sql-server-linux.md).

<!--SQL Server 2017 on Linux-->
::: moniker range="= sql-server-linux-2017 || = sql-server-2017"

```ini
[EULA]
accepteula = Y

[coredump]
captureminiandfull = true
coredumptype = full

[filelocation]
defaultbackupdir = /var/opt/mssql/data/
defaultdatadir = /var/opt/mssql/data/
defaultdumpdir = /var/opt/mssql/data/
defaultlogdir = /var/opt/mssql/data/

[hadr]
hadrenabled = 0

[language]
lcid = 1033

[memory]
memorylimitmb = 4096

[network]
forceencryption = 0
ipaddress = 10.192.0.0
kerberoskeytabfile = /var/opt/mssql/secrets/mssql.keytab
tcpport = 1401
tlscert = /etc/ssl/certs/mssql.pem
tlsciphers = ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-RSA-AES128-SHA256:ECDHE-RSA-AES256-SHA384:ECDHE-ECDSA-AES256-SHA:ECDHE-ECDSA-AES128-SHA:ECDHE-RSA-AES256-SHA:ECDHE-RSA-AES128-SHA:AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA
tlskey = /etc/ssl/private/mssql.key
tlsprotocols = 1.2,1.1,1.0

[sqlagent]
databasemailprofile = default
errorlogfile = /var/opt/mssql/log/sqlagentlog.log
errorlogginglevel = 7

[telemetry]
customerfeedback = true
userrequestedlocalauditdirectory = /tmp/audit

[traceflag]
traceflag0 = 1204
traceflag1 = 2345
traceflag = 3456
```

::: moniker-end
<!--SQL Server 2019 on Linux-->
::: moniker range=">= sql-server-linux-ver15 || >= sql-server-ver15"

```ini
[EULA]
accepteula = Y
accepteulaml = Y

[coredump]
captureminiandfull = true
coredumptype = full

[distributedtransaction]
servertcpport = 51999

[filelocation]
defaultbackupdir = /var/opt/mssql/data/
defaultdatadir = /var/opt/mssql/data/
defaultdumpdir = /var/opt/mssql/data/
defaultlogdir = /var/opt/mssql/data/

[hadr]
hadrenabled = 0

[language]
lcid = 1033

[memory]
memorylimitmb = 4096

[network]
forceencryption = 0
ipaddress = 10.192.0.0
kerberoskeytabfile = /var/opt/mssql/secrets/mssql.keytab
rpcport = 13500
tcpport = 1401
tlscert = /etc/ssl/certs/mssql.pem
tlsciphers = ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-RSA-AES128-SHA256:ECDHE-RSA-AES256-SHA384:ECDHE-ECDSA-AES256-SHA:ECDHE-ECDSA-AES128-SHA:ECDHE-RSA-AES256-SHA:ECDHE-RSA-AES128-SHA:AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA
tlskey = /etc/ssl/private/mssql.key
tlsprotocols = 1.2,1.1,1.0

[sqlagent]
databasemailprofile = default
errorlogfile = /var/opt/mssql/log/sqlagentlog.log
errorlogginglevel = 7

[telemetry]
customerfeedback = true
userrequestedlocalauditdirectory = /tmp/audit

[traceflag]
traceflag0 = 1204
traceflag1 = 2345
traceflag = 3456
```

::: moniker-end

## Next steps

- [Configure SQL Server settings with environment variables](sql-server-linux-configure-environment-variables.md)
- [Manage SQL Server on Linux](sql-server-linux-management-overview.md)
