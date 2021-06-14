---
title: "Connecting with sqlcmd"
description: "Learn how to use the sqlcmd utility with the Microsoft ODBC Driver for SQL Server on Linux and macOS."
ms.custom: ""
ms.date: 02/24/2021
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "sqlcmd"
ms.assetid: 61a2ec0d-1bcb-4231-bea0-cff866c21463
author: David-Engel
ms.author: v-daenge
---
# Connecting with sqlcmd
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The [sqlcmd](../../../tools/sqlcmd-utility.md) utility is available with the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS.
  
The following commands show how to use Windows Authentication (Kerberos) and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication, respectively:
  
```console
sqlcmd -E -Sxxx.xxx.xxx.xxx  
sqlcmd -Sxxx.xxx.xxx.xxx -Uxxx -Pxxx  
```  
  
## Available Options

In the current release, the following options are available:  
  
- -? Display `sqlcmd` usage.  
  
- -a Request a packet size.  
  
- -b Terminate batch job if there is an error.  
  
- -c *batch_terminator* Specify the batch terminator.  
  
- -C Trust server certificate.  

- -d *database_name*  Issue a `USE` *database_name* statement when you start `sqlcmd`.  

- -D Causes the value passed to the `sqlcmd` -S option to be interpreted as a data source name (DSN). For more information, see "DSN Support in `sqlcmd` and `bcp`" at the end of this topic.  
  
- -e Write input scripts to the standard output device (stdout).

- -E Use trusted connection (integrated authentication.) For more information about making trusted connections that use integrated authentication from a Linux or macOS client, see [Using Integrated Authentication](using-integrated-authentication.md).

- -f codepage | i:codepage[,o:codepage] | o:codepage[,i:codepage]
Specifies the input and output code pages. The codepage number is a numeric value that specifies an installed Linux code page.
(available since 17.5.1.1)

- -G This switch is used by the client when connecting to SQL Database or Azure Synapse Analytics to specify that the user be authenticated using Azure Active Directory authentication. Can be combined with just the -P option to use access token authentication (v17.8+) This option sets the sqlcmd scripting variable SQLCMDUSEAAD = true. The -G switch requires at least sqlcmd version 17.6. To determine your version, execute sqlcmd -?.

> [!IMPORTANT]
> The `-G` option only applies to Azure SQL Database and Azure Synapse Analytics.
> AAD Interactive Authentication is not currently supported on Linux or macOS. AAD Integrated Authentication requires [Microsoft ODBC Driver 17 for SQL Server](../download-odbc-driver-for-sql-server.md) version 17.6.1 or higher and a properly [configured Kerberos environment](using-integrated-authentication.md#configure-kerberos).

- -h *number_of_rows*  Specify the number of rows to print between the column headings.  
  
- -H Specify a workstation name.  
  
- -i *input_file*[,*input_file*[,...]] Identify the file that contains a batch of SQL statements or stored procedures.  
  
- -I  Set the `SET QUOTED_IDENTIFIER` connection option to ON.  
  
- -k  Remove or replace control characters.  
  
- **-K**_application\_intent_  
Declares the application workload type when connecting to a server. The only currently supported value is **ReadOnly**. If **-K** is not specified, `sqlcmd` does not support connectivity to a secondary replica in an AlwaysOn availability group. For more information, see [ODBC Driver on Linux and macOS - High Availability and Disaster Recovery](odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  
  
> [!NOTE]  
> **-K** is not supported in the CTP for SUSE Linux. You can, however, specify the **ApplicationIntent=ReadOnly** keyword in a DSN file passed to `sqlcmd`. For more information, see "DSN Support in `sqlcmd` and `bcp`" at the end of this topic.  
  
- -l *timeout* Specify the number of seconds before a `sqlcmd` login times out when you try to connect to a server.

- -m *error_level* Control which error messages are sent to stdout.  
  
- **-M**_multisubnet\_failover_  
Always specify **-M** when connecting to the availability group listener of a [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] availability group or a [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] Failover Cluster Instance. **-M** provides for faster detection of failovers and connection to the (currently) active server. If **-M** is not specified, **-M** is off. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)], see [ODBC Driver on Linux and macOS - High Availability and Disaster Recovery](odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  
  
> [!NOTE]  
> **-M** is not supported in the CTP for SUSE Linux. You can, however, specify the **MultiSubnetFailover=Yes** keyword in a DSN file passed to `sqlcmd`. For more information, see "DSN Support in `sqlcmd` and `bcp`" at the end of this topic.  
  
- -N Encrypt connection.  
  
- -o *output_file* Identify the file that receives output from `sqlcmd`.  
  
- -p  Print performance statistics for every result set.  
  
- -P  Specify a user password.  
  
- -q *commandline_query*  Execute a query when `sqlcmd` starts, but does not exit when the query has finished running.  

- -Q *commandline_query*  Execute a query when `sqlcmd` starts. `sqlcmd` will exit when the query finishes.  

- -r Redirects error messages to stderr.

- -R Causes the driver to use client regional settings to convert currency and date and time data to character data. Currently only uses en_US (US English) formatting.
  
- -s *column_separator_char*  Specify the column-separator character.  

- -S [*protocol*:] *server*[**,**_port_]  
Specify the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to connect to, or if -D is used, a DSN. The ODBC driver on Linux and macOS requires -S. Note that **tcp** is the only valid protocol.  
  
- -t *query_timeout* Specify the number of seconds before a command (or SQL statement) times out.  
  
- -u Specify that output_file is stored in Unicode format, regardless of the format of input_file.  
  
- -U *login_id* Specify a user login ID.  
  
- -V *error_severity_level* Control the severity level that is used to set the ERRORLEVEL variable.  
  
- -w *column_width* Specify the screen width for output.  
  
- -W Remove trailing spaces from a column.  
  
- -x Disable variable substitution.  
  
- -X Disable commands, startup script, and environment variables.  
  
- -y *variable_length_type_display_width* Set the `sqlcmd` scripting variable `SQLCMDMAXFIXEDTYPEWIDTH`.
  
- -Y *fixed_length_type_display_width* Set the `sqlcmd` scripting variable `SQLCMDMAXVARTYPEWIDTH`.

- -z *password* Change password.  
  
- -Z *password* Change password and exit.  


## Available Commands

In the current release, the following commands are available:  
  
-   [:]!!  
  
-   :Connect  
  
-   :Error  
  
-   [:]EXIT  
  
-   GO [*count*]  
  
-   :Help  
  
-   :List  
  
-   :Listvar  
  
-   :On Error  
  
-   :Out  
  
-   :Perftrace  
  
-   [:]QUIT  
  
-   :r  
  
-   :RESET  
  
-   :setvar  
  
## Unavailable Options
In the current release, the following options are not available:  

- -A  Log in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with a Dedicated Administrator Connection (DAC). For information on how to make a dedicated administrator connection (DAC), see [Programming Guidelines](programming-guidelines.md).  
  
- -L  List the locally configured server computers, and the names of the server computers that are broadcasting on the network.  
  
- -v  Create a `sqlcmd` scripting variable that can be used in a `sqlcmd` script.  
  
You can use the following alternative method: Put the parameters inside one file, which you can then append to another file. This will help you use a parameter file to replace the values. For example, create a file called `a.sql` (the parameter file) with the following content:
  
```console
    :setvar ColumnName object_id  
    :setvar TableName sys.objects  
```
  
Then create a file called `b.sql`, with the parameters for replacement:  
  
```sql
    select $(ColumnName) from $(TableName)  
```

At the command line, combine `a.sql` and `b.sql` into `c.sql` using the following commands:  
  
```console
    cat a.sql > c.sql 
  
    cat b.sql >> c.sql  
```
  
Run `sqlcmd` and use `c.sql` as input file:  
  
```console
    sqlcmd -S<...> -P<..> -U<..> -I c.sql  
```

## Unavailable Commands

In the current release, the following commands are not available:  
  
-   :ED  
  
-   :ServerList  
  
-   :XML  
  
## DSN Support in sqlcmd and bcp

You can specify a data source name (DSN) instead of a server name in the **sqlcmd** or **bcp** `-S` option (or **sqlcmd** :Connect command) if you specify `-D`. `-D` causes **sqlcmd** or **bcp** to connect to the server specified in the DSN by the `-S` option.  
  
System DSNs are stored in the `odbc.ini` file in the ODBC SysConfigDir directory (`/etc/odbc.ini` on standard installations). User DSNs are stored in `.odbc.ini` in a user's home directory (`~/.odbc.ini`).

On Windows systems, System and User DSNs are stored in the registry and managed via odbcad32.exe. File DSNs are not supported by bcp and sqlcmd.

See [DSN and Connection String Keywords and Attributes](../dsn-connection-string-attribute.md) for the list of entries which the driver supports.

In a DSN, only the DRIVER entry is required, but to connect to a remote server, `sqlcmd` or `bcp` needs a value in the SERVER element. If the SERVER element is empty or not present in the DSN, `sqlcmd` and `bcp` will attempt to connect to the default instance on the local system.

When using bcp on Windows systems, [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] and earlier require the SQL Native Client 11 driver (sqlncli11.dll) while [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and higher require the Microsoft ODBC Driver 17 for SQL Server driver (msodbcsql17.dll).  

If the same option is specified in both the DSN and the `sqlcmd` or `bcp` command line, the command line option overrides the value used in the DSN. For example, if the DSN has a DATABASE entry and the `sqlcmd` command line includes **-d**, the value passed to **-d** is used. If **Trusted_Connection=yes** is specified in the DSN, Kerberos authentication is used and user name (**-U**) and password (**-P**), if provided, are ignored.

Existing scripts that invoke `isql` can be modified to use `sqlcmd` by defining the following alias: `alias isql="sqlcmd -D"`.  

## See Also  
[Connecting with **bcp**](connecting-with-bcp.md)  
[Release Notes](release-notes-tools.md)
