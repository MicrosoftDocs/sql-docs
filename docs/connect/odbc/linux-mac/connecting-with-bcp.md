---
title: Connecting with bcp
description: Learn the options and commands available in the bcp utility, available in the mssql-tools package on Linux and macOS.
author: David-Engel
ms.author: v-davidengel
ms.date: 02/15/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "bcp"
---
# Connecting with bcp

[!INCLUDE[Driver_ODBC_Download](../../../includes/driver_odbc_download.md)]

The [bcp](../../../tools/bcp-utility.md) utility is available with the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ODBC Driver for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on Linux and macOS. This page documents the differences from the [Windows version of bcp](../../../tools/bcp-utility.md).

- The field terminator is a tab ("\t").

- The line terminator is a newline ("\n").

- Character mode is the preferred format for `bcp` format files and data files that don't contain extended characters.

> [!NOTE]
> A backslash '\\' on a command-line argument must either be quoted or escaped. For example, to specify a newline as a custom row terminator, you must use one of the following mechanisms:
>
> - -r\\\n
> - -r"\n"
> - -r'\n'

The following example is a command invocation of `bcp` to copy table rows to a text file:

```bash
bcp AdventureWorks2008R2.Person.Address out test.dat -Usa -Pxxxx -Sxxx.xxx.xxx.xxx
```

## Available options

In the current release, the following syntax and options are available:

[_database_**.**]_schema_**.**_table_ **in** *data_file* | **out** *data_file*

**-a** *packet_size*  
Specifies the number of bytes, per network packet, sent to and from the server.

**-b** *batch_size*  
Specifies the number of rows per batch of imported data.

**-c**  
Uses a character data type.

**-d** *database_name*  
Specifies the database to connect to.

**-D**  
Causes the value passed to the `bcp` -S option to be interpreted as a data source name (DSN). For more information, see "DSN Support in sqlcmd and bcp" in [Connecting with sqlcmd](connecting-with-sqlcmd.md).

**-e** *error_file*  
Specifies the full path of an error file used to store any rows that the `bcp` utility can't transfer from the file to the database.

**-E**  
Uses an identity value or values in the imported data file for the identity column.

**-f** *format_file*  
Specifies the full path of a format file.

**-F** *first_row*  
Specifies the number of the first row to export from a table or import from a data file.

**-G**  
This switch is used by the client when connecting to Azure SQL Database or Azure Synapse Analytics to specify that the user be authenticated using Azure Active Directory authentication. It can be combined with just the -P option to use access token authentication (v17.8+). The -G switch requires at least bcp version 17.6. To determine your version, execute bcp -v.

> [!IMPORTANT]
> The `-G` option only applies to Azure SQL Database and Azure Synapse Analytics.
> AAD Interactive Authentication is not currently supported on Linux or macOS. AAD Integrated Authentication requires [Microsoft ODBC Driver 17 for SQL Server](../download-odbc-driver-for-sql-server.md) version 17.6.1 or higher and a properly [configured Kerberos environment](using-integrated-authentication.md#configure-kerberos).

**-k**  
Specifies that empty columns should keep a null value during the operation, rather than have any default values for the columns inserted.

**-l**  
Specifies a login timeout. The -l option specifies the number of seconds before a login to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] times out when you try to connect to a server. The default login timeout is 15 seconds. The login timeout must be a number between 0 and 65534. If the value supplied isn't numeric or doesn't fall into that range, `bcp` generates an error message. A value of 0 specifies an infinite timeout.

**-L** *last_row*  
Specifies the number of the last row to export from a table or import from a data file.
  
**-m** *max_errors*  
Specifies the maximum number of syntax errors that can occur before the `bcp` operation is canceled.

**-n**  
Uses the native (database) data types of the data to do the bulk-copy operation.

**-P** *password*  
Specifies the password for the login ID. When used with the -G option without -U, specifies a file that contains an access token (v17.8+). The token file should be in UTF-16LE (no BOM) format.

Access tokens can be obtained via various methods. It's important to ensure the access token is correct byte-for-byte, as it will be sent as-is. Below is an example command that obtains an access token. The command uses the Azure CLI and Linux commands and saves it to a file in the proper format. If your system or terminal's default encoding isn't ASCII or UTF-8, you may need to adjust the `iconv` options. Be sure to carefully secure the resulting file and delete it when it's no longer required.

```azurecli
az account get-access-token --resource https://database.windows.net --output tsv | cut -f 1 | tr -d '\n' | iconv -f ascii -t UTF-16LE > /tmp/tokenFile
```

**-q**  
Executes the SET QUOTED_IDENTIFIERS ON statement in the connection between the `bcp` utility and an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

**-r** *row_terminator*  
Specifies the row terminator.

**-R**  
Specifies that currency, date, and time data is bulk copied into [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using the regional format defined for the locale setting of the client computer.

**-S** *server*  
Specifies the name of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance to connect to, or if -D is used, a DSN.

**-t** *field_terminator*  
Specifies the field terminator.

**-T**  
Specifies that the `bcp` utility connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] with a trusted connection (integrated security).

**-u**  
Trust server certificate. (available since `bcp` version 18)

**-U** *login_id*  
Specifies the login ID used to connect to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

**-v**  
Reports the `bcp` utility version number and copyright.

**-w**  
Uses Unicode characters to do the bulk copy operation.

In this release, Latin-1 and UTF-16 characters are supported.

**-Y**[s|m|o]  
Specifies the connection encryption mode. The options are Strict, Mandatory, and Optional. Using -Y without any parameters uses the Mandatory encryption mode, and is equivalent to -Ym. (available since `bcp` version 18)

## Unavailable options

In the current release, the following syntax and options aren't available:

**-C**  
Specifies the code page of the data in the data file.

**-h** *hint*  
Specifies the hint or hints used during a bulk import of data into a table or view.

**-i** *input_file*  
Specifies the name of a response file.

**-N**  
Uses the native (database) data types of the data for noncharacter data, and Unicode characters for character data.

**-o** *output_file*  
Specifies the name of a file that receives output redirected from the command prompt.

**-V** (80 | 90 | 100)  
Uses data types from an earlier version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

**-x**  
Used with the format and -f format_file options, generates an XML-based format file instead of the default non-XML format file.

## See also

[Connecting with sqlcmd](connecting-with-sqlcmd.md)  
[Release notes](release-notes-tools.md)
