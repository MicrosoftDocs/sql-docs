---
title: "Command-Line Options in the SSMA Console (AccessToSQL)"
description: "Command-line options in the SSMA Console (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Command-line options in the SSMA Console (AccessToSQL)

Microsoft provides a robust set of command-line options to execute and control SQL Server Migration Assistant (SSMA) activities. Learn more in the following sections.

## Command-line options in the SSMA Console

This section describes the console command options.

In this section, the terms *option* and *switch* refer to the same thing.

Options aren't case-sensitive and can start with either the `-` or `/` character.

If you specify options, you must also specify the corresponding option parameters.

You must separate option parameters from the option character using white space.

**Syntax examples**:

```console
SSMAforAccessConsole.exe -s scriptfile
```

```console
SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\AssessmentReportGenerationSample.xml" -v "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\VariableValueFileSample.xml" -c "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ServersConnectionFileSample.xml"
```

Folder or file names containing spaces must be specified with double quotes.

The output of command-line entries and error messages goes to `stdout` or to a specified file.

### Script file option: -s/script

Use this mandatory switch to specify the script file path and name. The script file contains command sequences that SSMA executes.

**Syntax example**:

```console
SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml"
```

### Variable value file option: -v/variable

Use this optional switch to specify a variable value file. The file contains variables used in the script file. If the script file uses variables that aren't declared in a variable value file, SSMA generates an error and terminates the console execution.

**Syntax example**:

Define variables in multiple variable value files, such as one file with default values and another file with instance-specific values. If there's a duplication of variables, the last variable file specified in the command-line arguments takes preference:

```console
SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -v C:\migrationprojects\global_variablevaluefile.xml -v "C:\migrationprojects\instance_variablevaluefile.xml"
```

### Server connection file option: -c/serverconnection

This file contains server connection information for each server. Each server definition is identified by a unique server ID. The script file references each server ID in connection-related commands.

A server definition can be part of a server connection file or a script file. If there's a duplication of server ID, the script file server ID takes precedence over the server connection file.

**Syntax examples**:

- Use server IDs in the script file, and define them in a separate server connection file. This file uses variables that are defined in the variable value file:

   ```console
   SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -v C:\SsmaProjects\myvaluefile1.xml -c C:\SsmaProjects\myserverconnectionsfile1.xml
   ```

- Embed the server definition in the script file:

   ```console
   SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml"
   ```

### XML output option: -x/xmloutput [xmloutputfile]

Use this command to output the command output messages in an XML format, either to the console or to an XML file.

`xmloutput` has two options.

- If you provide the file path after the `xmloutput` switch, redirect the output to the file.

  **Syntax example**:

  ```console
  SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -x d:\xmloutput\project1output.xml
  ```

- If you don't provide a file path after the `xmloutput` switch, the output displays on the console.

  **Syntax example**:

  ```console
  SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -xmloutput
  ```

### Log file option: -l/log

The Console application records all the SSMA operations in a log file. The switch is optional. If you specify a log file and its path at the command line, the log is generated in the specified location. Otherwise, the log is generated in its default location.

**Syntax example**:

```console
SSMAforAccessConsole.exe "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -l C:\SsmaProjects\migration1.log
```

### Project environment folder option: -e/projectenvironment

Use this optional switch to specify the project environment settings folder for the current SSMA project.

**Syntax example**:

```console
SSMAforAccessConsole.exe -s "C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml" -e C:\SsmaProjects\CommonEnvironment
```

### Secure password option: -p/securepassword

Use this option to specify the encrypted password for server connections. It differs from all other options in that it doesn't execute any script or help in any migration-related activities. Instead, it helps manage password encryption for the server connections used in the migration project.

When you use this option, it must be the only parameter you provide. Any other options or passwords cause the command to fail. For more information, see the [Manage passwords](managing-passwords-accesstosql.md) section.

The following suboptions are supported for `-p/securepassword`:

- Add or update a password in protected storage for a specified server ID, or for all server IDs defined in the server connection file:

  ```syntax
  -p|-securepassword -a|add {"<server_id>[, .n]"|all} -c|-serverconnection <server-connection-file> [-v|variable <variable-value-file>] [-o|overwrite]
  ```

  ```syntax
  -p|-securepassword -a|add {"<server_id>[, .n]"|all} -s|-script <server-connection-file> [-v|variable <variable-value-file>] [-o|overwrite]
  ```

- Remove the encrypted password from the protected storage of the specified server ID or for all server IDs:

  ```syntax
  -p/securepassword -r/remove {<server_id> [, ...n] | all}
  ```

- Display a list of server IDs for which the password is encrypted:

  ```syntax
  -p/securepassword -l/list
  ```

- Export the passwords stored in protected storage to an encrypted file. This file is encrypted with the user-specified passphrase.

  ```syntax
  -p/securepassword -e/export {<server-id> [, ...n] | all} <encrypted-password -file>
  ```

- The previously exported encrypted file is imported to local protected storage, using the user-specified passphrase. Once the file is decrypted, it stores the contents in a new file, which in turn is encrypted on the local machine.

  ```syntax
  -p/securepassword -i/import {<server-id> [, ...n] | all} <encrypted-password -file>
  ```

  You can specify multiple server IDs by using comma separators.

### Help option: -?/Help

Displays the syntax summary of SSMA Console options.

For a tabular display of the SSMA Console command-line options, see [Appendix - 1](appendix-1-accesstosql.md).

**Syntax example**:

```console
SSMAforAccessConsole.exe -?
```

### SecurePassword Help option: -securepassword -?/Help

Displays the syntax summary of SSMA Console options:

For a tabular display of the SSMA Console command-line options, see [Appendix - 1](appendix-1-accesstosql.md).

**Syntax example**:

```console
SSMAforAccessConsole.exe -securepassword -?
```

### Related content

- [Manage passwords](managing-passwords-accesstosql.md)
- [Generate reports](generating-reports-accesstosql.md)
- [Troubleshooting](troubleshooting-accesstosql.md)
