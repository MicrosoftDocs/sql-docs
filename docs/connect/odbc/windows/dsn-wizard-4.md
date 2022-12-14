---
title: Data Source Wizard Screen 4 (ODBC Driver for SQL Server)
description: Learn how to define advanced options in the Data Source Wizard to create a new ODBC connection to SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: 08/08/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Data Source Wizard Screen 4

Specify the language to be used for SQL Server messages, the character set translation, and whether the ODBC driver for SQL Server should use regional settings. You can also control the logging of long-running queries and driver statistics settings.

## Options

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

When this check box is cleared, no translation of extended characters in ANSI character strings is done when they're sent between the client application and the server. If the client computer is using an ANSI code page (ACP) different from the SQL Server code page, extended characters in ANSI character strings may be misinterpreted. If the client computer is using the same code page for its ACP that SQL Server is using, the extended characters are interpreted correctly.

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

### Back

Select this button to go back to the previous page of the wizard.

### Finish

If the information specified on this screen is complete, you can select **Finish**. The DSN is created using all attributes specified on this and other screens of the wizard, and you're given an opportunity to test the newly created DSN.

## Next steps

[Data Source Wizard Screen 3](dsn-wizard-3.md)
