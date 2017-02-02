---
title: "Connecting with sqlcmd | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "sqlcmd"
ms.assetid: 61a2ec0d-1bcb-4231-bea0-cff866c21463
caps.latest.revision: 45
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Connecting with sqlcmd
[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The [sqlcmd](http://go.microsoft.com/fwlink/?LinkID=154481) utility is available in the [!INCLUDE[msCoName](../../../includes/msconame_md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] on Linux.  
  
The following commands show first how to use Windows Authentication and second, how to use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] Authentication.  
  
```  
sqlcmd –E –Sxxx.xxx.xxx.xxx  
sqlcmd –Sxxx.xxx.xxx.xxx –Uxxx -Pxxx  
```  
  
## sqlcmd Options that are Available  
In the current release, the following sqlcmd options are available:  
  
-?  
Display sqlcmd usage.  
  
-a  
Request a packet size.  
  
-b  
Terminate batch job if there is an error.  
  
-c *batch_terminator*  
Specify the batch terminator.  
  
-C  
Trust server certificate.  
  
-d *database_name*  
Issue a USE *database_name* statement when you start sqlcmd.  
  
-D  
Causes the value passed to the sqlcmd -S option to be interpreted as a data source name (DSN). For more information, see "DSN Support in sqlcmd and bcp" at the end of this topic.  
  
-e  
Write input scripts to the standard output device (stdout).  
  
-E  
Use trusted connection, integrated authentication.  
  
For more information about making trusted connections that use integrated authentication from a Linux client:  
  
-   [Native LDAP, native Kerberos, and Windows Server 2003 R2 AD Services and schema for cross platform identity management](http://downloads.buffalo.nas-central.org/Users/itimpi/Using%20native%20LDAP%20native%20Kerberos%20and%20Windows%20Server%202003%20R2.pdf)  
  
-   [Authenticate Linux Clients with Active Directory](http://technet.microsoft.com/magazine/2008.12.linux.aspx#id0060048)  
  
-   [Linux Creating or Adding New Network Alias To a Network Card (NIC)](http://www.cyberciti.biz/faq/linux-creating-or-adding-new-network-alias-to-a-network-card-nic/)  
  
-h *number_of_rows*  
Specify the number of rows to print between the column headings.  
  
-H  
Specify a workstation name.  
  
-i *input_file*[,*input_file*[,…]]  
Identify the file that contains a batch of SQL statements or stored procedures.  
  
-I  
Set the SET QUOTED_IDENTIFIER connection option to ON.  
  
-k  
Remove or replace control characters.  
  
**-K***application_intent*  
Declares the application workload type when connecting to a server. The only currently supported value is **ReadOnly**. If **-K** is not specified, sqlcmd does not support connectivity to a secondary replica in an AlwaysOn availability group. For more information, see [ODBC Driver on Linux Support for High Availability, Disaster Recovery](../../../connect/odbc/linux/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  
  
> [!NOTE]  
> **-K** is not supported in the CTP for SUSE Linux. You can, however, specify the **ApplicationIntent=ReadOnly** keyword in a DSN file passed to sqlcmd. For more information, see "DSN Support in sqlcmd and bcp" at the end of this topic.  
  
-l  
Specify the number of seconds before a sqlcmd login to the ODBC driver times out when you try to connect to a server.  
  
-m *error_level*  
Control which error messages are sent to stdout.  
  
**-M***multisubnet_failover*  
Always specify **-M** when connecting to the availability group listener of a [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] availability group or a [!INCLUDE[ssSQL11](../../../includes/sssql11_md.md)] Failover Cluster Instance. **-M** provides for faster detection of and connection to the (currently) active server. If **–M** is not specified, **-M** is off. For more information about [!INCLUDE[ssHADR](../../../includes/sshadr_md.md)], see [ODBC Driver on Linux Support for High Availability, Disaster Recovery](../../../connect/odbc/linux/odbc-driver-on-linux-support-for-high-availability-disaster-recovery.md).  
  
> [!NOTE]  
> **-M** is not supported in the CTP for SUSE Linux. You can, however, specify the **MultiSubnetFailover=Yes** keyword in a DSN file passed to sqlcmd. For more information, see "DSN Support in sqlcmd and bcp" at the end of this topic.  
  
-N  
Encrypt connection.  
  
-o *output_file*  
Identify the file that receives output from sqlcmd.  
  
-p  
Print performance statistics for every result set.  
  
-P  
Specify a user password.  
  
-q *commandline_query*  
Execute a query when sqlcmd starts, but does not exit sqlcmd when the query has finished running.  
  
-Q *commandline_query*  
Execute a query when sqlcmd starts. sqlcmd will exit when the query finishes.  
  
-r  
Redirects error messages to stderr.  
  
-R  
Causes the driver to use client regional settings to convert currency and date and time data to character data. Currently only uses en_US (US English) formatting.  
  
-s *column_separator_char*  
Specify the column-separator character.  
  
-S [*protocol*:] *server*[**,***port*]  
Specify the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] to which to connect. Or, if -D is used, a DSN. The ODBC driver on Linux requires -S. For the ODBC driver on Linux, **tcp** is the only valid protocol.  
  
-t *query_timeout*  
Specify the number of seconds before a command (or SQL statement) times out.  
  
-u  
Specify that output_file is stored in Unicode format, regardless of the format of input_file.  
  
-U *login_id*  
Specify a user login ID.  
  
-V *error_severity_level*  
Control the severity level that is used to set the ERRORLEVEL variable.  
  
-w *column_width*  
Specify the screen width for output.  
  
-W  
Remove trailing spaces from a column.  
  
-x  
Disable variable substitution.  
  
-X  
Disable commands, startup script, and environment variables.  
  
-y *variable_length_type_display_width*  
Set the sqlcmd scripting variable SQLCMDMAXFIXEDTYPEWIDTH.  
  
-Y *fixed_length_type_display_width*  
Set the sqlcmd scripting variable SQLCMDMAXVARTYPEWIDTH.  
  
In the current release, the following sqlcmd commands are available:  
  
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
  
## sqlcmd Options that are Not Available  
In the current release, the following sqlcmd options are not available:  
  
-A  
Log in to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion_md.md)] with a Dedicated Administrator Connection (DAC). For information on how to make a dedicated administrator connection (DAC), see [Programming Guidelines](../../../connect/odbc/linux/programming-guidelines.md).  
  
-f *code_page*  
Specify the input and output code pages.  
  
-L  
List the locally configured server computers, and the names of the server computers that are broadcasting on the network.  
  
-v  
Create a sqlcmd scripting variable that can be used in a sqlcmd script.  
  
You can use the following alternative method:  
  
-   Put the parameters inside one file. Which you can then append to another file. This will help you use a parameter file to replace the values. Create a file called a.sql (the parameter file) with the following content:  
  
    ```  
    :setvar ColumnName object_id  
    :setvar TableName sys.objects  
    ```  
  
-   Create a file called b.sql,with the parameters for replacement:  
  
    ```  
    select $(ColumnName) from $(TableName)  
    ```  
  
-   At the command line, combine a.sql and b.sql into c.sql, using the following commands:  
  
    cat a.sql > c.sql  
  
    cat b.sql >> c.sql  
  
-   Run sqlcmd and use c.sql as input file:  
  
    slqcmd -S\<…> -P\<..> –U\<..> -I c.sql  
  
-z *password*  
Change password.  
  
-Z *password*  
Change password and exit.  
  
In the current release, the following sqlcmd commands are not available:  
  
-   :ED  
  
-   :ServerList  
  
-   :XML  
  
## DSN Support in sqlcmd and bcp  
You can specify a data source name (DSN) instead of a server name in the **sqlcmd** or **bcp-S** option (or **sqlcmd** :Connect command) if you specify -D. -D causes the -S option to retrieve the specified DSN. **sqlcmd** or **bcp** connect to the server specified in the DSN.  
  
System DSNs are stored in the odbc.ini file in the ODBC SysConfigDir directory (/etc/odbc.ini on standard installations). User DSNs are stored in .odbc.ini in a user's home directory (~/.odbc.ini).  
  
The following entries are supported in a DSN on Linux:  
  
-   **ApplicationIntent=ReadOnly**  
  
-   **Database=***database_name*  
  
-   **Driver=ODBC Driver 11 for SQL Server**  
  
-   **MultiSubnetFailover=Yes**  
  
-   **Server=***server_name_or_IP_address*  
  
-   **Trusted_Connection=yes**|**no**  
  
In a DSN, only the DRIVER entry is required, but to connect to a server, **sqlcmd** or **bcp** need the value in the SERVER entry.  
  
If the same option is specified in both the DSN and the **sqlcmd** or **bcp** command line, the **sqlcmd** or **bcp** option override the value used in the DSN. For example, if the DSN has a DATABASE entry and the **sqlcmd** command line includes -d, the value passed to **-d** is used. If **Trusted_Connection=yes** is specified in the DSN, Kerberos authentication is used, and user name (**–U**) and password (**–P**) , if provided, is ignored.  
  
Existing scripts that invoke **isql** can be modified to use **sqlcmd** by defining the following alias: **alias isql="sqlcmd –D"**.  
  
## See Also  
[Microsoft ODBC Driver for SQL Server on Linux](../../../connect/odbc/linux/microsoft-odbc-driver-for-sql-server-on-linux.md)  
  
