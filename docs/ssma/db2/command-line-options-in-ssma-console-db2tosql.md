---
title: "Command Line options in SSMA console (Db2ToSQL)"
description: Learn about the command line options available in SQL Server Migration Assistant, for executing and controlling Db2ToSQL activities
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Command line options in SSMA console (Db2ToSQL)

SQL Server Migration Assistant (SSMA) provides you with a robust set of command line options to execute and control Db2 activities. The ensuing sections detail the same.

## Command line options in SSMA console

This section describes the console command options, also known as switches.

Options aren't case-sensitive, and might start with either `-` or, `/` character.

If options are specified, you must specify the corresponding option parameters.

Option parameters must be separated from the option character by white space.

**Syntax examples:**

```cmd
SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\AssessmentReportGenerationSample.xml" -v "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\VariableValueFileSample.xml" -c "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ServersConnectionFileSample.xml"
```

Folders and file names containing spaces should be specified in double quotes.

The output of command line entries and error messages are stored in STDOUT or in a specified file.

### Script file option: -s | script

A mandatory switch, the script file path/name specifies the script of command sequences that SSMA executes.

**Syntax example:**

```cmd
SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml"
```

### Variable value file option: -v | variable

This file comprises variables used in the script file. This switch is optional. If variables aren't declared in variable file and used in the script file, the application generates an error and terminates the console execution.

**Syntax example:**

Variables defined in multiple variable value files, perhaps one with a default value and another with an instance specific value when applicable. The last variable file specified in the command line arguments takes the preference, in case there's a duplication of variables:

```cmd
SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -v C:\migration projects\global_variablevaluefile.xml -v "C:\migrationprojects\instance_variablevaluefile.xml"
```

### Server connection file option: -c | serverconnection

This file contains server connection information for each server. Each server definition is identified by a unique Server ID. The Server IDs are referenced in the script file for connection related commands.

Server definition can be a part of server connection file and/or script file. Server ID in script file takes precedence over the server connection file, in case there's a duplication of server ID.

**Syntax examples:**

- Server IDs are used in the script file and you define them in a separate server connection file. The server connection file uses variables, which are defined in the variable value file:

  ```cmd
  SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -v C:\SsmaProjects\myvaluefile1.xml -c C:\SsmaProjects\myserverconnectionsfile1.xml
  ```

- Server definition is embedded in the script file:

  ```cmd
  SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml"
  ```

### XML output option: -x | xmloutput [ xmloutputfile ]

This command is used for outputting the command output messages in an XML format either to console or to an XML file.

There are two options available for `xmloutput`, for example:

- If the file path is provided after the `xmloutput` switch, the output is redirected to the file.

  **Syntax example:**

  ```cmd
  SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -x d:\xmloutput\project1output.xml
  ```

- If no file path is provided after the `xmloutput` switch, the `xmlout` is displayed on the console itself.

  **Syntax example:**

  ```cmd
  SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -xmloutput
  ```

### Log file option: -l | log

All the SSMA operations in the Console application get recorded in a log file. This switch is optional. If a log file and its path are specified at the command line, the log gets generated in the specified location. Otherwise, it gets generated in its default location.

**Syntax example:**

```cmd
SSMAforDb2Console.exe "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -l C:\SsmaProjects\migration1.log
```

### Project environment folder option: -e | projectenvironment

Denotes the project environment settings folder for the current SSMA project. This switch is optional.

**Syntax example:**

```cmd
SSMAforDb2Console.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -e C:\SsmaProjects\CommonEnvironment
```

### Secure password option: -p | securepassword

This option indicates the encrypted password for server connections. It differs from all other options: the option doesn't execute any script, or helps in any migration-related activities. Instead, it helps manage password encryption for the server connections used in the migration project.

You can't enter any other option or password as the command line parameter. Otherwise, it results in an error. For more information, see [Manage Passwords](managing-passwords-db2tosql.md).

The following suboptions are supported for `-p | securepassword`:

- To add password to protected storage for a specified Server ID or for all Server IDs defined in the server connection file. The `-overwrite` option updates the password if it already exists:

  ```syntax
  -p | -securepassword -a | add { "<server_id> [, ...n]" | all }
  -c | -serverconnection <server-connection-file> [ -v | variable <variable-value-file> ]
  [ -o | overwrite]
  ```

  ```syntax
  -p | -securepassword -a | add { "<server_id>[, ...n]" | all }
  -s | -script <server-connection-file> [ -v | variable <variable-value-file> ]
  [ -o | overwrite]
  ```

- To remove the encrypted password from the protected storage of the specified Server ID or for all Server IDs:

  ```syntax
  -p | securepassword -r | remove { <server_id> [, ...n] | all }
  ```

- To display a list of Server IDs for which the password is encrypted:

  ```syntax
  -p | securepassword -l | list
  ```

- To export the passwords stored in protected storage to an encrypted file. This file is encrypted with the user-specified pass-phrase.

  ```syntax
  -p | securepassword -e | export { <server-id> [, ...n] | all } <encrypted-password-file>
  ```

- The encrypted-file that was earlier exported is imported to local protected storage using the user-specified pass-phrase. Once the file is decrypted, it's stored in a new file, which in turn, is encrypted on the local machine.

  ```syntax
  -p | securepassword -i | import { <server-id> [, ...n] | all } <encrypted-password-file>
  ```

  Multiple Server IDs can be specified using comma separators.

### Help option: -? | help

Displays the syntax summary of SSMA console options:

```cmd
SSMAforDb2Console.exe -?
```

For a tabular display of the SSMA console command line options, see [Appendix 1: Console command line options](appendix-1-db2tosql.md).

### SecurePassword help option: -securepassword -? | help

Displays the syntax summary of SSMA console options:

```cmd
SSMAforDb2Console.exe -securepassword -?
```

For a tabular display of the SSMA console command line options, see [Appendix 1: Console command line options](appendix-1-db2tosql.md)

## Related content

- [Manage Passwords (Db2ToSQL)](managing-passwords-db2tosql.md)
- [Generate reports (Db2ToSQL)](generating-reports-db2tosql.md)
- [Troubleshoot (Db2ToSQL)](troubleshooting-db2tosql.md)
