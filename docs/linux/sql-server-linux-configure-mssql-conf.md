---
title: Configure SQL Server settings on Linux | Microsoft Docs
description: This topic describes how to use the mssql-conf tool to  configure SQL Server 2017 settings on Linux.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 09/20/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 06798dff-65c7-43e0-9ab3-ffb23374b322
---
# Configure SQL Server on Linux with the mssql-conf tool

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

**mssql-conf** is a configuration script that installs with SQL Server 2017 RC2 for Red Hat Enterprise Linux, SUSE Linux Enterprise Server, and Ubuntu. You can use this utility to set the following parameters:

|||
|---|---|
| [Collation](#collation) | Set a new collation for SQL Server on Linux. |
| [Customer feedback](#customerfeedback) | Choose whether or not SQL Server sends feedback to Microsoft. |
| [Database Mail Profile](#dbmail) | Set the default database mail profile for SQL Server on Linux |
| [Default data directory](#datadir) | Change the default directory for new SQL Server database data files (.mdf). |
| [Default log directory](#datadir) | Changes the default directory for new SQL Server database log (.ldf) files. |
| [Default dump directory](#dumpdir) | Change the default directory for new memory dumps and other troubleshooting files. |
| [Default backup directory](#backupdir) | Change the default directory for new backup files. |
| [Dump type](#coredump) | Choose the type of dump memory dump file to collect. |
| [High availability](#hadr) | Enable Availability Groups. |
| [Local Audit directory](#localaudit) | Set a a directory to add Local Audit files. |
| [Locale](#lcid) | Set the locale for SQL Server to use. |
| [Memory limit](#memorylimit) | Set the memory limit for SQL Server. |
| [TCP port](#tcpport) | Change the port where SQL Server listens for connections. |
| [TLS](#tls) | Configure Transport Level Security. |
| [Traceflags](#traceflags) | Set the traceflags that the service is going to use. |

> [!TIP]
> Some of these settings can also be configured with environment variables. For more information, see [Configure SQL Server settings with environment variables](sql-server-linux-configure-environment-variables.md).

## Usage tips

* For Always On Availability Groups and shared disk clusters, always make the same configuration changes on each node.

* For the shared disk cluster scenario, do not attempt to restart the **mssql-server** service to apply changes. SQL Server is running as an application. Instead, take the resource offline and then back online.

* These examples run mssql-conf by specify the full path: **/opt/mssql/bin/mssql-conf**. If you choose to navigate to that path instead, run mssql-conf in the context of the current directory: **./mssql-conf**.

## <a id="collation"></a> Change the SQL Server collation

The **set-collation** option changes the collation value to any of the supported collations.

1. First [backup any user databases](sql-server-linux-backup-and-restore-database.md) on your server.

1. Then use the [sp_detach_db](../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md) stored procedure to detach the user databases.

1. Run the **set-collation** option and follow the prompts:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set-collation
   ```

1. The mssql-conf utility will attempt to change to the specified collation value and restart the service. If there are any errors, it rolls back the collation to the previous value.

1. Retore your user database backups.

For a list of supported collations, run the [sys.fn_helpcollations](../relational-databases/system-functions/sys-fn-helpcollations-transact-sql.md) function: `SELECT Name from sys.fn_helpcollations()`.

## <a id="customerfeedback"></a> Configure customer feedback

The **telemetry.customerfeedback** setting changes whether SQL Server sends feedback to Microsoft or not. By default, this value is set to **true**. To change the value, run the following commands:

1. Run the mssql-conf script as root with the **set** command for **telemetry.customerfeedback**. The following example turns off customer feedback by specifying **false**.

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.customerfeedback false
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

For more information, see [Customer Feedback for SQL Server on Linux](sql-server-linux-customer-feedback.md).

## <a id="datadir"></a> Change the default data or log directory location

The **filelocation.defaultdatadir** and **filelocation.defaultlogdir** settings change the location where the new database and log files are created. By default, this location is /var/opt/mssql/data. To change these settings, use the following steps:

1. Create the target directory for new database data and log files. The following example creates a new **/tmp/data** directory:

   ```bash
   sudo mkdir /tmp/data
   ```

1. Change the owner and group of the directory to the **mssql** user:

   ```bash
   sudo chown mssql /tmp/data
   sudo chgrp mssql /tmp/data
   ```

1. Use mssql-conf to change the default data directory with the **set** command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultdatadir /tmp/data
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

1. Now all the database files for the new databases created will be stored in this new location. If you would like to change the location of the log (.ldf) files of the new databases, you can use the following "set" command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultlogdir /tmp/log
   ```

1. This command also assumes that a /tmp/log directory exists, and that it is under the user and group **mssql**.

## <a id="dumpdir"></a> Change the default dump directory location

The **filelocation.defaultdumpdir** setting changes the default location where the memory and SQL dumps are generated whenever there is a crash. By default, these files are generated in /var/opt/mssql/log.

To set up this new location, use the following commands:

1. Create the target directory for new dump files. The following example creates a new **/tmp/dump** directory:

   ```bash
   sudo mkdir /tmp/dump
   ```

1. Change the owner and group of the directory to the **mssql** user:

   ```bash
   sudo chown mssql /tmp/dump
   sudo chgrp mssql /tmp/dump
   ```

1. Use mssql-conf to change the default data directory with the **set** command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultdumpdir /tmp/dump
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="backupdir"></a> Change the default backup directory location

The **filelocation.defaultbackupdir** setting changes the default location where the backup files are generated. By default, these files are generated in /var/opt/mssql/data.

To set up this new location, use the following commands:

1. Create the target directory for new backup files. The following example creates a new **/tmp/backup** directory:

   ```bash
   sudo mkdir /tmp/backup
   ```

1. Change the owner and group of the directory to the **mssql** user:

   ```bash
   sudo chown mssql /tmp/backup
   sudo chgrp mssql /tmp/backup
   ```

1. Use mssql-conf to change the default backup directory with the "set" command:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set filelocation.defaultbackupdir /tmp/backup
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="coredump"></a> Specify core dump settings

If an exception occurs in one of the SQL Server processes, SQL Server creates a memory dump.

There are two options for controlling the type of memory dumps that SQL Server collects: **coredump.coredumptype** and **coredump.captureminiandfull**. These relate to the two phases of core dump capture. 

The first phase capture is controlled by the **coredump.coredumptype** setting, which determines the type of dump file generated during an exception. The second phase is enabled when the **coredump.captureminiandfull** setting. If **coredump.captureminiandfull** is set to true, the dump file specified by **coredump.coredumptype** is generated and a second mini dump is also generated. Setting **coredump.captureminiandfull** to false disables the second capture attempt.

1. Decide whether to capture both mini and full dumps with the **coredump.captureminiandfull** setting.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set coredump.captureminiandfull <true or false>
    ```

    Default: **false**

1. Specify the type of dump file with the **coredump.coredumptype** setting.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set coredump.coredumptype <dump_type>
    ```

    Default: **miniplus**

    The following table lists the possible **coredump.coredumptype** values.

    | Type | Description |
    |-----|-----|
    | **mini** | Mini is the smallest dump file type. It uses the Linux system information to determine threads and modules in the process. The dump contains only the host environment thread stacks and modules. It does not contain indirect memory references or globals. |
    | **miniplus** | MiniPlus is similar to mini, but it includes additional memory. It understands the internals of SQLPAL and the host environment, adding the following memory regions to the dump:</br></br> - Various globals</br> - All memory above 64TB</br> - All named regions found in **/proc/$pid/maps**</br> - Indirect memory from threads and stacks</br> - Thread information</br> - Associated Teb’s and Peb’s</br> - Module Information</br> - VMM and VAD tree |
    | **filtered** | Filtered uses a subtraction-based design where all memory in the process is included unless specifically excluded. The design understands the internals of SQLPAL and the host environment, excluding certain regions from the dump.
    | **full** | Full is a complete process dump that includes all regions located in **/proc/$pid/maps**. This is not controlled by **coredump.captureminiandfull** setting. |

## <a id="dbmail"></a> Set the default database mail profile for SQL Server on Linux

The **sqlpagent.databasemailprofile** allows you to set the default DB Mail profile for email alerts.

```bash
sudo /opt/mssq/bin/mssql-conf set sqlagent.databasemailprofile <profile_name>
```
## <a id="hadr"></a> High Availability

The **hadr.hadrenabled** option enables availability groups on your SQL Server instance. The following command enables availability groups by setting **hadr.hadrenabled** to 1. You must restart SQL Server for the setting to take effect.

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
sudo systemctl restart mssql-server
```

For information how this is used with availability groups, see the following two topics.

- [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md)
- [Configure read-scale availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md)

## <a id="localaudit"></a> Set local audit directory

The **telemetry.userrequestedlocalauditdirectory** setting enables Local Audit and lets you set the directory where the Local Audit logs are created.

1. Create a target directory for new Local Audit logs. The following example creates a new **/tmp/audit** directory:

   ```bash
   sudo mkdir /tmp/audit
   ```

1. Change the owner and group of the directory to the **mssql** user:

   ```bash
   sudo chown mssql /tmp/audit
   sudo chgrp mssql /tmp/audit
   ```

1. Run the mssql-conf script as root with the **set** command for **telemetry.userrequestedlocalauditdirectory**:

   ```bash
   sudo /opt/mssql/bin/mssql-conf set telemetry.userrequestedlocalauditdirectory /tmp/audit
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

For more information, see [Customer Feedback for SQL Server on Linux](sql-server-linux-customer-feedback.md).

## <a id="lcid"></a> Change the SQL Server locale

The **language.lcid** setting changes the SQL Server locale to any supported language identifier (LCID). 

1. The following example changes the locale to French (1036):

   ```bash
   sudo /opt/mssql/bin/mssql-conf set language.lcid 1036
   ```

1. Restart the SQL Server service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="memorylimit"></a> Set the memory limit

The **memory.memorylimitmb** setting controls the amount physical memory (in MB) available to SQL Server. The default is 80% of the physical memory.

1. Run the mssql-conf script as root with the **set** command for **memory.memorylimitmb**. The following example changes the memory available to SQL Server to 3.25 GB (3328 MB).

   ```bash
   sudo /opt/mssql/bin/mssql-conf set memory.memorylimitmb 3328
   ```

1. Restart the SQL Server service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

## <a id="tcpport"></a> Change the TCP port

The **network.tcpport** setting changes the TCP port where SQL Server listens for connections. By default, this port is set to 1433. To change the port, run the following commands:

1. Run the mssql-conf script as root with the "set" command for "network.tcpport":

   ```bash
   sudo /opt/mssql/bin/mssql-conf set network.tcpport <new_tcp_port>
   ```

1. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```

1. When connecting to SQL Server now, you must specify the custom port with a comma (,) after the hostname or IP address. For example, to connect with SQLCMD, you would use the following command:

   ```bash
   sqlcmd -S localhost,<new_tcp_port> -U test -P test
   ```

## <a id="tls"></a> Specify TLS settings

The following options configure TLS for an instance of SQL Server running on Linux.

|Option |Description |
|--- |--- |
|**network.forceencryption** |If 1, then [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] forces all connections to be encrypted. By default, this option is 0. |
|**network.tlscert** |The absolute path to the certificate file that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses for TLS. Example:   `/etc/ssl/certs/mssql.pem`  The certificate file must be accessible by the mssql account. Microsoft recommends restricting access to the file using `chown mssql:mssql <file>; chmod 400 <file>`. |
|**network.tlskey** |The absolute path to the private key file that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses for TLS. Example:  `/etc/ssl/private/mssql.key`  The certificate file must be accessible by the mssql account. Microsoft recommends restricting access to the file using `chown mssql:mssql <file>; chmod 400 <file>`. |
|**network.tlsprotocols** |A comma-separated list of which TLS protocols are allowed by SQL Server. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] always attempts to negotiate the strongest allowed protocol. If a client does not support any allowed protocol, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] rejects the connection attempt.  For compatibility, all supported protocols are allowed by default (1.2, 1.1, 1.0).  If your clients support TLS 1.2, Microsoft recommends allowing only TLS 1.2. |
|**network.tlsciphers** |Specifies which ciphers are allowed by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] for TLS. This string must be formatted per [OpenSSL's cipher list format](https://www.openssl.org/docs/man1.0.2/apps/ciphers.html). In general, you should not need to change this option. <br /> By default, the following ciphers are allowed: <br /> `ECDHE-ECDSA-AES128-GCM-SHA256:ECDHE-ECDSA-AES256-GCM-SHA384:ECDHE-RSA-AES128-GCM-SHA256:ECDHE-RSA-AES256-GCM-SHA384:ECDHE-ECDSA-AES128-SHA256:ECDHE-ECDSA-AES256-SHA384:ECDHE-RSA-AES128-SHA256:ECDHE-RSA-AES256-SHA384:ECDHE-ECDSA-AES256-SHA:ECDHE-ECDSA-AES128-SHA:ECDHE-RSA-AES256-SHA:ECDHE-RSA-AES128-SHA:AES256-GCM-SHA384:AES128-GCM-SHA256:AES256-SHA256:AES128-SHA256:AES256-SHA:AES128-SHA` |
| **network.kerberoskeytabfile** |Path to the Kerberos keytab file |

For an example of using the TLS settings, see [Encrypting Connections to SQL Server on Linux](sql-server-linux-encrypted-connections.md).

## <a id="traceflags"></a> Enable/Disable traceflags

This **traceflag** option enables or disables traceflags for the startup of the SQL Server service. To enable/disable a traceflag use the following commands:

1. Enable a traceflag using the following command. For example, for Traceflag 1234:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 1234 on
   ```

1. You can enable multiple traceflags by specifying them separately:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 2345 3456 on
   ```

1. In a similar way, you can disable one or more enabled traceflags by specifying them and adding the **off** parameter:

   ```bash
   sudo /opt/mssql/bin/mssql-conf traceflag 1234 2345 3456 off
   ```

1. Restart the SQL Server service to apply the changes:

   ```bash
   sudo systemctl restart mssql-server
   ```

## Remove a setting

To unset any setting made with `mssql-conf set`, call **mssql-conf** with the `unset` option and the name of the setting. This clears the setting, effectively returning it to its default value.

1. The following example clears the **network.tcpport** option.

   ```bash
   sudo /opt/mssql/bin/mssql-conf unset network.tcpport
   ```

1. Restart the SQL Server service.

   ```bash
   sudo systemctl restart mssql-server
   ```

## View current settings

To view any configured settings, run the following command to output the contents of the **mssql.conf** file:

```bash
sudo cat /var/opt/mssql/mssql.conf
```

Note that any settings not shown in this file are using their default values. The next section provides a sample **mssql.conf** file.

## mssql.conf format

The following **/var/opt/mssql/mssql.conf** file provides an example for each setting. You can use this format to manually make changes to the **mssql.conf** file as needed. If you do manually change the file, you must restart SQL Server before the changes are applied. To use the **mssql.conf** file with Docker, you must have Docker [persist your data](sql-server-linux-configure-docker.md). First add a complete **mssql.conf** file to your host directory and then run the container. There is an example of this in  [Customer Feedback](sql-server-linux-customer-feedback.md).

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

## Next steps

To instead use environment variables to make some of these configuration changes, see [Configure SQL Server settings with environment variables](sql-server-linux-configure-environment-variables.md).

For other management tools and scenarios, see [Manage SQL Server on Linux](sql-server-linux-management-overview.md).
