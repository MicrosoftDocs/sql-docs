---
title: "Managing Passwords (AccessToSQL)"
description: "Managing Passwords (AccessToSQL)"
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
# Manage passwords (AccessToSQL)

This section describes how to secure database passwords in SQL Server Migration Assistant (SSMA) and how to import or export passwords across servers.

## Secure your password

SSMA enables you to secure your password for a database. Use the following methods to implement a secure connection.

> [!NOTE]  
> If the server section of the server connection file or the script file doesn't contain a password, or if the password isn't secured on the local machine, SSMA prompts you to enter the password.

### Clear text

Type the database password in the value attribute of the `password` node. You can find this node under the server definition node in the **Server** section of the script file or server connection file.

Passwords in clear text aren't secure. Therefore, you see the following warning message in the console output:

```output
Server <server-id> password is provided in non-secure clear text form, SSMA Console application provides an option to protect the password through encryption, please see -securepassword option in SSMA help file for more information.
```

> [!IMPORTANT]  
> The clear text password that you specify in the script or server connection file takes precedence over the encrypted password in the secured file.

### Encrypted passwords

The specified password is stored in an encrypted form on the local machine in `ProtectedStorage.ssma`.

#### Securing passwords

1. Run `SSMAforAccessConsole.exe` with the `-securepassword` and `add` switch at the command line, passing the server connection or script file that contains the password node in the server definition section.

1. At the prompt, enter the database password and confirm it.

  Each server definition ID and its corresponding encrypted password is stored in a file on the local machine.

  **Example 1**:

  Specify password:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -securepassword -add all -s "D:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\AssessmentReportGenerationSample.xml" -v "D:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ VariableValueFileSample.xml"
  ```

  Enter and confirm the password as prompted.

  **Example 2**:

  Specify password:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -securepassword -add "source_1,target_1" -c "D:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ServersConnectionFileSample.xml" - v "D:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ VariableValueFileSample.xml" -o
  ```

  Enter and confirm the passwords as prompted.

#### Remove encrypted passwords

Run `SSMAforAccessConsole.exe` with the `-securepassword` and `-remove` switch at the command line, passing each server ID in a comma-delimited list to remove the encrypted passwords from the protected storage file on the local machine.

```console
C:\SSMA\SSMAforAccessConsole.exe -securepassword -remove all
C:\SSMA\SSMAforAccessConsole.exe -securepassword -remove "source_1,target_1"
```

#### List Server IDs whose passwords are encrypted

Run `SSMAforAccessConsole.exe` with the `-securepassword` and `-list` switch at the command line to list the ID of each server whose password is encrypted.

```console
C:\SSMA\SSMAforAccessConsole.exe -securepassword -list
```

## Export or import encrypted passwords

You can use the SSMA Console application to export encrypted database passwords from a file on the local machine to a secured file, and vice versa. This process makes the encrypted passwords machine independent. The export functionality reads the server ID and password from the local protected storage and saves the information in an encrypted file. You're prompted to enter the password for the secured file. Make sure the password you enter is eight characters or more. You can port this secured file across different machines. The import functionality reads the server ID and password information from the secured file. You're prompted to enter the password for the secured file. The process appends the information to the local protected storage.

### Export password

- To export passwords for all servers, use the following example:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -securepassword -export all "machine1passwords.file"
  ```

  Enter a password to protect the exported file, and then confirm it.

- To export passwords for two servers, use the following example:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -p -e "AccessDB_1_1,Sql_1" "machine2passwords.file"
  ```

  Enter a password to protect the exported file, and then confirm it.

### Import an encrypted password

- To import passwords for all servers, use the following example:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -securepassword -import all "machine1passwords.file"
  ```

  Enter the password to import the servers from the encrypted file, and then confirm it.

- To import passwords for two servers, use the following example:

  ```console
  C:\SSMA\SSMAforAccessConsole.exe -p -i "AccessDB_1,Sql_1" "machine2passwords.file"
  ```

  Enter the password to import the servers from the encrypted file, and then confirm it.

## Related content

- [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md)
