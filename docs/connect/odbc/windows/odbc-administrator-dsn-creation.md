---
title: ODBC Data Source Administrator DSN options
description: This article describes the options available when creating a new DSN connection to SQL Server using the ODBC Data Source Administrator application.
author: David-Engel
ms.author: davidengel
ms.date: "03/29/2024"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# ODBC Data Source Administrator DSN options

This article describes the options available when creating a new DSN connection to SQL Server using the ODBC Data Source Administrator application.

When you create a DSN, the wizard displays a series of screens that allow you to specify the information needed to connect to SQL Server.

## Create New Data Source

This article only pertains to creating a DSN using the ODBC driver for SQL Server. The ODBC Data Source Administrator dialog box is displayed when you select **Add** in the **User DSN**, **System DSN**, or **File DSN** tab of the ODBC Data Source Administrator dialog box. Choose the driver and select **Finish** to display the first screen of the wizard.

## Create a New Data Source to SQL Server - Screen 1

### Name

The data source name used by an ODBC application when it requests a connection to the data source. For example, "Personnel." The data source name is displayed in the ODBC Data Source Administrator dialog box.

### Description

(Optional) A description of the data source. For example, "Hire date, salary history, and current review of all employees."

### Select or enter a server name

The name of an instance of SQL Server on your network. You'll need to specify a server in the next edit box.

In most cases, the ODBC driver can connect by using the default protocol order and the server name supplied in this box. Use SQL Server Configuration Manager if you want to create an alias for the server or configure client network libraries.

You can enter "(local)" in the server box when you're using the same computer as SQL Server. The user can then connect to the local instance of SQL Server, even when running a non-networked version of SQL Server. Multiple instances of SQL Server can run on the same computer. To specify a named instance of SQL Server, the server name is specified as _ServerName_\\_InstanceName_.

For more information about server names for different types of networks, see [Logging In to SQL Server](../../../database-engine/configure-windows/logging-in-to-sql-server.md#format-for-specifying-the-name-of-sql-server).

### Finish (optional)

If the information specified on this screen is all that is needed to connect to SQL Server, you can select **Finish**. Defaults are used for all attributes specified on other screens of the wizard.

## Create a New Data Source to SQL Server - Screen 2

Specify the method of authentication, and set up Microsoft SQL Server advanced-client entries and the login and password the ODBC driver for SQL Server will use to connect to SQL Server while configuring the data source.

[!INCLUDE [entra-id](../../../includes/entra-id-hard-coded.md)]

### With Integrated Windows Authentication

Specifies that the driver request a secure (or trusted) connection to a SQL Server. When selected, SQL Server uses integrated login security to establish connections using this data source, regardless of the current login security mode at the server. Any login ID or password supplied is ignored. The SQL Server system administrator must have associated your Windows login with a SQL Server login ID (for example, by using SQL Server Management Studio).

Optionally, you can specify a service principal name (SPN) for the server.

### With Active Directory Integrated Authentication

Specifies that the driver authenticate to SQL Server using Microsoft Entra ID. When selected, SQL Server uses Microsoft Entra integrated login security to establish a connection using this data source, regardless of the current login security mode at the server.

### With SQL Server authentication

Specifies that the driver authenticate to SQL Server using a login ID and password.

### With Active Directory Password authentication

Specifies that the driver authenticate to SQL Server using a Microsoft Entra login ID and password.

### With Active Directory Interactive authentication

Specifies that the driver authenticate to SQL Server using Microsoft Entra interactive mode by providing a login ID. This option triggers the Azure Authentication prompt dialog.

### With Managed Identity authentication

Specifies that the driver authenticate to SQL Server using a Managed Identity.

### With Active Directory Service Principal authentication

Specifies that the driver authenticate to SQL Server using a Microsoft Entra service principal.

### Login ID

Specifies the login ID the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** or **With Active Directory Interactive authentication using a login ID entered by the user** is selected. If **With Managed Identity authentication** is selected, specify the object ID of the managed identity or leave blank to use the default identity. This field only applies to the connection made to determine the server default settings; it doesn't apply to subsequent connections made using the data source after it has been created except if using Managed Identity authentication.

### Password

Specifies the password the driver uses when connecting to SQL Server if **With SQL Server Authentication using a login ID and password entered by the user** or **With Active Directory Password authentication using a login ID and password entered by the user** is selected. This field only applies to the connection made to determine the server default settings; it doesn't apply to subsequent connections made using the new data source.

Both the **Login ID** and **Password** boxes are disabled if **With Integrated Windows authentication** or **With Active Directory Integrated authentication** is selected.

## Create a New Data Source to SQL Server - Screen 3

Specify the default database, various ANSI options to be used by the driver, and the name of a mirror server.

### Change the default database to

Specifies the name of the default database for any connection made using this data source. When this box is cleared, connections use the default database defined for the login ID on the server. When this box is selected, the database named in the box overrides the default database defined for the login ID. If the **Attach database filename** box has the name of a primary file, the database described by the primary file is attached as a database using the database name specified in the **Change the default database to** box.

Using the default database for the login ID is more efficient than specifying a default database in the ODBC data source.

### Mirror server

Specifies the name of the failover partner of the database to be mirrored. If a database name isn't shown in the **Change the default database to** box, or the name shown is the default database, **Mirror Server** is grayed out.

Optionally, you can specify a server principal name (SPN) for the mirror server. The SPN for the mirror server is used for mutual authentication between client and server.

### Attach database filename

Specifies the name of the primary file for an attachable database. This database is attached and used as the default database for the data source. Specify the full path and file name for the primary file. The database name specified in the **Change the default database to** box is used as the name for the attached database.

### Use ANSI quoted identifiers

Specifies that `QUOTED_IDENTIFIER` is set to on when the ODBC driver for SQL Server connects. When this check box is selected, SQL Server enforces ANSI rules regarding quote marks. Double quotes can only be used for identifiers, such as column and table names. Character strings must be enclosed in single quotes:

```sql
SELECT "LastName"
FROM "Person.Contact"
WHERE "LastName" = 'O''Brien'
```

When this check box is cleared, applications that use quoted identifiers, such as the Microsoft Query utility that comes with Microsoft Excel, encounter errors when they generate SQL statements with quoted identifiers.

### Use ANSI nulls, paddings, and warnings

Specifies that the ANSI_NULLS, ANSI_WARNINGS, and ANSI_PADDINGS options be set on when the ODBC Driver for SQL Server connects.

With ANSI_NULLS set on, the server enforces ANSI rules regarding comparing columns for NULL. The ANSI syntax "IS NULL" or "IS NOT NULL" must be used for all NULL comparisons. The Transact-SQL syntax "= NULL" isn't supported.

With ANSI_WARNINGS set on, SQL Server issues warning messages for conditions that violate ANSI rules but don't violate the rules of Transact-SQL. Examples of such errors are data truncation on execution of an INSERT or UPDATE statement, or encountering a null value during an aggregate function.

With ANSI_PADDING set on, trailing blanks on **varchar** values and trailing zeroes on **varbinary** values aren't automatically trimmed.

### Application intent

Declares the application workload type when connecting to a server. Possible values are **ReadOnly** and **ReadWrite**.

### Multi-subnet failover

If your application is connecting to a high-availability, disaster recovery (Always On Availability Groups) availability group (AG) on different subnets, enabling **Multi-subnet failover** configures ODBC Driver for SQL Server to provide faster detection of and connection to the (currently) active server.

### Transparent Network IP Resolution

Alters the behavior of **Multi-subnet failover** to allow for faster reconnection during failover. For more information, see [Using Transparent Network IP Resolution](../using-transparent-network-ip-resolution.md).

### Column Encryption

Enables automatic decryption and encryption of data transfers to and from columns encrypted with the [Always Encrypted](../using-always-encrypted-with-the-odbc-driver.md) feature available in SQL Server 2016 and later.

### Use FMTONLY metadata discovery

Use the legacy SET FMTONLY metadata discovery method when connecting to SQL Server 2012 or newer. Enable this option only when using queries not supported by [sp_describe_first_result_set](../../../relational-databases/system-stored-procedures/sp-describe-first-result-set-transact-sql.md), such as those containing temporary tables.

## Create a New Data Source to SQL Server - Screen 4

Specify the language to be used for SQL Server messages, the character set translation, and whether the ODBC driver for SQL Server should use regional settings. You can also control the logging of long-running queries and driver statistics settings.

### Change the language of SQL Server system messages to

Each instance of SQL Server can have multiple sets of system messages, with each set in a different language (for example, English, Spanish, French, and so on). If a data source is defined against a server that has multiple sets of system messages, you can specify which language you want to use for system messages. In the list, select the language. This option is unavailable if only one language is installed on the SQL Server.

### Use strong encryption for data

When selected, data that is passed through connections that are made using this DSN will be encrypted. Logins are encrypted by default, even if the check box is cleared. This option is available in ODBC Driver 17 and older.

### Connection Encryption

Declares the connection encryption mode to be used when connections are made using this DSN. Selecting the **Optional** or **Mandatory** option is equivalent to having **Use strong encryption for data** unselected or selected, respectively. When **Strict** is used, connections will be encrypted using TDS 8.0. This option is available in ODBC Driver 18 and newer.

### Trust server certificate

This option is applicable only when **Use strong encryption for data** is enabled (ODBC Driver 17 and older), or when **Connection Encryption** is set to **Optional** or **Mandatory** (ODBC Driver 18 and newer). When selected, the server's certificate won't be validated to have the correct hostname of the server and be issued by a trusted certificate authority. The server's certificate will always be validated when using the **Strict** encryption mode.

### Server certificate (optional)

Specifies the server certificate (PEM, DER, or CER format) to match against the certificate returned by the server during encryption negotiation. When specified, certificate validation is done by checking if the server's certificate is an exact match against the certificate specified. The **Hostname in certificate** option is ignored when a server certificate is specified. This option is applicable only when **Connection Encryption** is set to **Strict** and is available in ODBC Driver 18.1 and newer.

### Hostname in certificate (optional)

Specifies the hostname to be used when validating the server's certificate. When left blank, the server name is used as the hostname for validation. A hostname can only be specified when **Trust server certificate** is unselected. This option is available in ODBC Driver 18 and newer.

### Perform translation for character data

When this check box is selected, the ODBC driver for SQL Server converts ANSI strings sent between the client computer and SQL Server by using Unicode. The ODBC driver sometimes converts between the SQL Server code page and Unicode on the client computer. This option requires that the code page used by SQL Server is one of the code pages available on the client computer.

When this check box is cleared, no translation of extended characters in ANSI character strings is done when they're sent between the client application and the server. If the client computer is using an ANSI code page (ACP) different from the SQL Server code page, extended characters in ANSI character strings might be misinterpreted. If the client computer is using the same code page for its ACP that SQL Server is using, the extended characters are interpreted correctly.

### Use regional settings when outputting currency, numbers, dates, and times

Specifies that the driver use the regional settings of the client computer for formatting currency, numbers, dates, and times in character output strings. The driver uses the default regional setting for the Windows login account of the user connecting through the data source. Select this option for applications that only display data, not for applications that process data.

### Save long running queries to the log file

Specifies that the driver log any query that takes longer than the **Long query time** value. Long-running queries are logged to the specified file. To specify a log file, either type the full path and file name in the box, or select **Browse** to select a log file by navigating through existing file directories.

### Long query time (milliseconds)

Specifies a threshold value, in milliseconds, for long-running query logging. Any query that takes longer than this number of milliseconds to run is logged.

### Log ODBC driver statistics to the log file

Specifies that statistics be logged. Statistics are logged to the specified file. To specify a log file, either type the full path and file name in the box or select **Browse** to select a log file by navigating through existing file directories.

The statistics log is a tab-delimited file that can be analyzed in Microsoft Excel or any other application that supports tab-delimited files.

### Connect retry count

Specifies the number of times to retry an unsuccessful connection attempt.

### Connect retry interval (seconds)

Specifies the number of seconds between each connection retry attempt. For more information on the operation of this option and the **Connect retry count** options, see [Connection Resiliency](../connection-resiliency.md).

### Finish

If the information specified on this screen is complete, you can select **Finish**. The DSN is created using all attributes specified on this and other screens of the wizard, and you're given an opportunity to test the newly created DSN.

## Related content

[Microsoft ODBC Driver for SQL Server on Windows](../../../connect/odbc/windows/microsoft-odbc-driver-for-sql-server-on-windows.md)
