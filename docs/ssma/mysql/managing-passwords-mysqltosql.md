---
description: "Managing Passwords (MySQLToSQL)"
title: "Managing Passwords (MySQLToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: 07/06/2020
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Password management, importing or exporting encrypted password"
  - "Password management, securing password"
ms.assetid: 4ffbc587-ea3f-49ad-bc42-a654f672325e
author: cpichuka 
ms.author: cpichuka 
---
# Managing Passwords (MySQLToSQL)
This article is about securing database passwords and the procedure to import or export them across servers.
  
## Securing Password  
SSMA allows you to secure your password of a database.  
  
Use the following procedure to implement a secure connection:  
  
Specify a valid password using one of the following three methods:  
  
1.  **Clear Text:** Type the database password in the value attribute of the 'password' node. It is found under the server definition node in the Server section of the script file or server connection file.  
  
    Passwords in clear text are not secure. Therefore, you will encounter the following warning message in the console output: *"Server &lt;server-id&gt; password is provided in non-secure clear text form, SSMA Console application provides an option to protect the password through encryption, please see -securepassword option in SSMA help file for more information."*  
  
    **Encrypted Passwords:** The specified password, in this case, is stored in an encrypted form on the local machine in ProtectedStorage.ssma.  
  
    -   **Securing Passwords**  
  
        -   Execute the `SSMAforMySQLConsole.exe` with the `-securepassword` and add switch at command line passing the server connection or script file containing the password node in the server definition section.  
  
        -   At prompt, the user is asked to enter the database password and confirm it.  
  
            The server definition ids and its corresponding encrypted passwords are stored in a file on the local machine  
            
            **Example 1:**
            
            1. Specify password
            
            2. `C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -add all -s "D:\Program Files\Microsoft SQL Server Migration Assistant for MySQL\Sample Console Scripts\AssessmentReportGenerationSample.xml" -v "D:\Program Files\Microsoft SQL Server Migration Assistant for MySQL\Sample Console Scripts\ VariableValueFileSample.xml"`
            
            3. Enter password for server_id 'XXX_1': xxxxxxx
            
            4. Re-enter password for server_id 'XXX_1': xxxxxxx
            
            **Example 2:**
            
            1. `C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -add "source_1,target_1" -c "D:\Program Files\Microsoft SQL Server Migration Assistant for MySQL\Sample Console Scripts\ServersConnectionFileSample.xml" - v "D:\Program Files\Microsoft SQL Server Migration Assistant for MySQL\Sample Console Scripts\ VariableValueFileSample.xml" -o`
            
            2. Enter password for server_id 'source_1': xxxxxxx
            
            3. Re-enter password for server_id 'source_1': xxxxxxx
            
            4. Enter password for server_id 'target_1': xxxxxxx
            
            5. Re-enter password for server_id 'target _1': xxxxxxx
            
    -   **Removing Encrypted Passwords**  
  
        Execute the `SSMAforMySQLConsole.exe` with the`-securepassword` and `-remove` switch at command line passing the server ids, to remove the encrypted passwords from the protected storage file present on the local machine.  
  
        Example:  

        ```console
        C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -remove all
        C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -remove "source_1,target_1"  
        ```
  
    -   **Listing Server Ids whose passwords are encrypted**  
  
        Execute the `SSMAforMySQLConsole.exe` with the `-securepassword` and `-list` switch at command line to list all the server ids whose passwords have been encrypted.  
  
        Example:  
        
        ```console
        C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -list  
        ```
  
    > [!NOTE]  
    > 1.  The password in clear text mentioned in script or server connection file takes precedence over the encrypted password in secured file.  
    > 2.  When no password exists in the server section of the server connection file or the script file or if it has not been secured on the local machine, the console prompts you to enter the password.  
  
## Exporting or Importing Encrypted Passwords  
The SSMA Console application allows you to export encrypted database passwords present in a file on the local machine to a secured file and vice-versa. It helps in making the encrypted passwords machine independent.

Export functionality reads the server id and password from the local protected storage and saves the information in an encrypted file. The user is prompted to enter the password for the secured file. Make sure the password entered is 8 character length or more. This secured file is portable across different machines.

Import functionality reads the server id and password information from the secured file. The user is prompted to enter the password for the secured file and appends the information to the local protected storage.  
  
### Export example:  

1. Export password

2. Enter password for protecting the exported file

3. C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -export all "machine1passwords.file"

4. Enter password for protecting the exported file: xxxxxxxx

5. Please confirm password: xxxxxxxx

6. C:\SSMA\SSMAforMySQLConsole.EXE -p -e "MySQLDB_1_1,Sql_1" "machine2passwords.file"

7. Enter password for protecting the exported file: xxxxxxxx

8. Please confirm password: xxxxxxxx  
  
### Import example:  

1. Import an encrypted password

2. Enter password for protecting the imported file

3. C:\SSMA\SSMAforMySQLConsole.EXE -securepassword -import all "machine1passwords.file"

4. Enter password to import the servers from encrypted file: xxxxxxxx

5. Please confirm password: xxxxxxxx

6. C:\SSMA\SSMAforMySQLConsole.EXE -p -i "MySQLDB_1,Sql_1" "machine2passwords.file"

7. Enter password to import the servers from encrypted file: xxxxxxxx

8. Please confirm password: xxxxxxxx  
  
## See Also  
[Executing the SSMA Console (MySQL)](./executing-the-ssma-console-mysqltosql.md)  
