---
title: "Manage passwords (Db2ToSQL)"
description: Learn about securing database passwords in SQL Server Migration Assistant for Db2.
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
# Manage passwords (Db2ToSQL)

This section is about securing database passwords in SQL Server Migration Assistant (SSMA) and the procedure to import or export them across servers.

## Secure password

SSMA allows you to secure your password of a database.

Use the following procedure to implement a secure connection:

Specify a valid password using one of the following methods:

1. **Clear text:** Type the database password in the value attribute of the `password` node. This node is under the server definition node in the Server section of the script file or server connection file.

   Passwords in clear text aren't secure. Therefore, you might encounter the following warning message in the console output.

   ```output
   Server <server-id> password is provided in non-secure clear text form, SSMA console application provides an option to protect the password through encryption, please see -securepassword option in SSMA help file for more information.
   ```

1. **Encrypted passwords:** The specified password, in this case, is stored in an encrypted form on the local machine in `ProtectedStorage.ssma`.

   - **Secure passwords**

     - Execute the `SSMAforDb2Console.exe` with the `-securepassword` and add switch at command line passing the server connection or script file containing the password node in the server definition section.

     - At prompt, the user is asked to enter the database password and confirm it.

       The server definition IDs and its corresponding encrypted passwords are stored in a file on the local machine

       **Example 1:**

       ```output
       Specify password
       C:\SSMA\SSMAforDb2Console.exe -securepassword -add all -s "D:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\AssessmentReportGenerationSample.xml" -v "D:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ VariableValueFileSample.xml"

       Enter password for server_id 'XXX_1'.

       Re-enter password for server_id 'XXX_1'.
       ```

       **Example 2:**

       ```output
       C:\SSMA\SSMAforDb2Console.exe -securepassword -add "source_1,target_1" -c "D:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ServersConnectionFileSample.xml" - v "D:\Program Files\Microsoft SQL Server Migration Assistant for Db2\Sample Console Scripts\ VariableValueFileSample.xml" -o

       Enter password for server_id 'source_1'.

       Re-enter password for server_id 'source_1'.

       Enter password for server_id 'target_1'.

       Re-enter password for server_id 'target _1'.
       ```

   - **Remove encrypted passwords**

     Execute the `SSMAforDb2Console.exe` with the`-securepassword` and `-remove` switch at command line passing the server IDs, to remove the encrypted passwords from the protected storage file present on the local machine.

     **Example:**

     ```cmd
     C:\SSMA\SSMAforDb2Console.exe -securepassword -remove all
     C:\SSMA\SSMAforDb2Console.exe -securepassword -remove "source_1,target_1"
     ```

   - **List server IDs whose passwords are encrypted**

     Execute the `SSMAforDb2Console.exe` with the `-securepassword` and `-list` switch at command line to list all the server IDs whose passwords are encrypted.

     **Example:**

     ```cmd
     C:\SSMA\SSMAforDb2Console.exe -securepassword -list
     ```

The password in clear text mentioned in script or server connection file takes precedence over the encrypted password in secured file.

When no password exists in the server section of the server connection file or the script file, or if it isn't secured on the local machine, the console prompts you to enter the password.

## Export or import encrypted passwords

The SSMA console application allows you to export encrypted database passwords present in a file on the local machine to a secured file and vice-versa. It helps in making the encrypted passwords machine independent.

*Export functionality* reads the server ID and password from the local protected storage. The system then saves the ID and password in an encrypted file. The user is prompted to enter the password for the secured file. Make sure the password entered is eight or more characters in length. This secured file is portable across different machines.

*Import functionality* reads the server ID and password information from the secured file. The user is prompted to enter the password for the secured file, and appends the information to the local protected storage.

### Export example

1. Export the password.
1. Enter the password for protecting the exported file.
1. Run: `C:\SSMA\SSMAforDb2Console.exe -securepassword -export all "machine1passwords.file"`
1. Enter the password for protecting the exported file.
1. Confirm the password.
1. Run: `C:\SSMA\SSMAforDb2Console.exe -p -e "Db2DB_1_1,Sql_1" "machine2passwords.file"`
1. Enter the password for protecting the exported file:
1. Confirm the password.

### Import example

1. Import an encrypted password.
1. Enter the password for protecting the imported file.
1. Run: `C:\SSMA\SSMAforDb2Console.exe -securepassword -import all "machine1passwords.file"`
1. Enter the password to import the servers from encrypted file.
1. Confirm the password.
1. Run: `C:\SSMA\SSMAforDb2Console.exe -p -i "Db2DB_1,Sql_1" "machine2passwords.file"`
1. Enter the password to import the servers from encrypted file.
1. Confirm password.

## Related content

- [Execute the SSMA console (Db2ToSQL)](executing-the-ssma-console-db2tosql.md)
