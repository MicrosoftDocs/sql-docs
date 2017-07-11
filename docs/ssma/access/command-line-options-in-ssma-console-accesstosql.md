---
title: "Command Line Options in SSMA Console (AccessToSQL) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "sql-ssma"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "Azure SQL Database"
  - "SQL Server"
ms.assetid: c1f3b3f0-0f3e-4e07-b745-2fbdde85c67e
caps.latest.revision: 11
author: "sabotta"
ms.author: "carlasab"
manager: "lonnyb"
---
# Command Line Options in SSMA Console (AccessToSQL)
Microsoft provides you with a robust set command line options to execute and control SSMA activities. The ensuing sections detail the same.  
  
## Command Line Options in SSMA Console  
Described herein are the console command options.  
  
For the purpose of this section, the term ‘option’ is also referred to as ‘switch’.  
  
Options are not case-sensitive and may start with either ‘**-**’ or, ‘**/**’ character.  
  
If options are specified, it becomes mandatory to specify the corresponding option parameters.  
  
Option parameters must be separated from the option character by white space.  
  
**Syntax Examples:**  
  
`C:\> SSMAforAccessConsole.EXE -s scriptfile`  
  
`C:\> SSMAforAccessConsole.EXE -s “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\AssessmentReportGenerationSample.xml” –v “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\VariableValueFileSample.xml” –c “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ServersConnectionFileSample.xml”`  
  
Folder or file names containing spaces should be specified in double quotes.  
  
The output of command line entries and error messages are stored in STDOUT or in a specified file.  
  
### Script File Option: –s/script  
A mandatory switch, the script file path/name specifies the script of command sequences to be executed by SSMA.  
  
**Syntax Examples:**  
  
`C:\>SSMAforAccessConsole.EXE –s “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”`  
  
### Variable Value File Option: –v/variable  
This file comprises variables used in the script file. This is an optional switch. If variables are not declared in variable file and used in the script file, the application generates an error and terminates the console execution.  
  
**Syntax Examples:**  
  
-   Variables defined in multiple variable value files, perhaps one with a default value and another with an instance specific value when applicable. The last variable file specified in the command line arguments takes the preference, in case there is a duplication of variables:  
  
    `C:\>SSMAforAccessConsole.EXE -s`  
  
    `“C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml” –v c:\migration`  
  
    `projects\global_variablevaluefile.xml –v “c:\migrationprojects\instance_variablevaluefile.xml”`  
  
### Server Connection File Option: –c/serverconnection  
This file contains server connection information for each server. Each server definition is identified by a unique Server ID. The Server IDs are referenced in the script file for connection related commands.  
  
Server definition can be a part of server connection file and/or script file. Server id in script file takes precedence over the server connection file, in case there is a duplication of server id.  
  
**Syntax Examples:**  
  
-   Server IDs are used in the script file and they are defined in a separate server connection file, server connection file uses variables which are defined in the variable value file:  
  
    `C:\>SSMAforAccessConsole.EXE –s “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”  –v`  
  
    `c:\SsmaProjects\myvaluefile1.xml –c`  
  
    `c:\SsmaProjects\myserverconnectionsfile1.xml`  
  
-   Server definition is embedded in the script file:  
  
    `C:\>SSMAforAccessConsole.EXE –s “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”`  
  
### XML Output Option: -x/xmloutput [xmloutputfile]  
This command is used for outputting the command output messages in an xml format either to console or to an xml file.  
  
There are two options available for xmloutput, viz..,:  
  
-   If the filepath is provided after the xmloutput switch the output is redirected to the file.  
  
    **Syntax Example:**  
  
    `C:\>SSMAforAccessConsole.EXE –s`  
  
    `“C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”  –x d:\xmloutput\project1output.xml`  
  
-   If no filepath is provided after the xmloutput  switch then the xmlout is displayed on the console itself.  
  
    **Syntax Example:**  
  
    `C:\>SSMAforAccessConsole.EXE –s “C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”  –xmloutput`  
  
### Log File Option: –l/log  
All the SSMA operations in the Console application get recorded in a log file. This is an optional switch. If a log file and its path are specified at the command line, the log gets generated in the specified location. Otherwise, it gets generated in its default location.  
  
**Syntax Example:**  
  
`C:\>SSMAforAccessConsole.EXE`  
  
`“C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”  –l c:\SsmaProjects\migration1.log`  
  
### Project Environment Folder Option: –e/projectenvironment  
This denotes the project environment settings folder for the current SSMA project. This switch is optional.  
  
**Syntax Example:**  
  
`C:\>SSMAforAccessConsole.EXE –s`  
  
`“C:\Program Files\Microsoft SQL Server Migration Assistant for Access\Sample Console Scripts\ConversionAndDataMigrationSample.xml”  –e c:\SsmaProjects\CommonEnvironment`  
  
||  
|-|  
||  
  
### Secure Password Option: –p/securepassword  
This option indicates the encrypted password for server connections. It differs from all other options: the option neither executes any script nor helps in any migration-related activities but helps manage password-encryption for the server connections used in the migration project.  
  
You cannot enter any other option or password as the command line parameter. Otherwise, it results in an error. For more information, refer to the [Managing Passwords](http://msdn.microsoft.com/en-us/b099d0f9-dd37-4c87-8b6f-ed0177881ea4) section.  
  
The following sub-options are supported for `–p/securepassword`:  
  
-   To add password to protected storage for a specified Server ID or for all Server IDs defined in the server connection file. The -overwrite option, below, updates the password if it already exists:  
  
    `-p|-securepassword -a|add    {"<server_id>[, .n]"|all}``-c|-serverconnection <server-connection-file> [-v|variable <variable-value-file>]``[-o|overwrite]`  
  
    `-p|-securepassword -a|add    {"<server_id>[, .n]"|all}``-s|-script <server-connection-file> [-v|variable <variable-value-file>] [-o|overwrite]`  
  
-   To remove the encrypted password from the protected storage of the specified Server ID or for all Server IDs:  
  
    `–p/securepassword –r/remove {<server_id> [, …n] | all}`  
  
-   To display a list of Server IDs for which the password is encrypted:  
  
    `–p/securepassword –l/list`  
  
-   To export the passwords stored in protected storage to an encrypted file. This file is encrypted with the user-specified pass-phrase.  
  
    `–p/securepassword –e/export {<server-id> [, …n] | all} <encrypted-password -file>`  
  
-   The encrypted-file that was earlier exported is imported to local protected storage using the user-specified pass-phrase. Once the file is decrypted, it is stored in a new file, which in turn, is encrypted on the local machine.  
  
    `–p/securepassword –i/import {<server-id> [, …n] | all} <encrypted-password -file>`  
  
    Multiple Server IDs can be specified using comma-separators.  
  
### Help Option: –?/Help  
Displays the syntax summary of SSMA Console options:  
  
`C:\>SSMAforAccessConsole.EXE -?`  
  
For a tabular display of the SSMA Console command line options, refer to [Appendix - 1 &#40;AccessToSQL&#41;](../../ssma/access/appendix-1-accesstosql.md).  
  
### SecurePassword Help Option: –securepassword -?/Help  
Displays the syntax summary of SSMA Console options:  
  
`C:\>SSMAforAccessConsole.EXE -securepassword -?`  
  
For a tabular display of the SSMA Console command line options, refer to [Appendix - 1 &#40;AccessToSQL&#41;](../../ssma/access/appendix-1-accesstosql.md)  
  
### Next Step  
The next step depends on your project requirements:  
  
1.  For specifying a password or export/ import passwords, see [Managing Passwords &#40;AccessToSQL&#41;](../../ssma/access/managing-passwords-accesstosql.md).  
  
2.  For generating reports, see [Generating Reports &#40;AccessToSQL&#41;](../../ssma/access/generating-reports-accesstosql.md).  
  
3.  For troubleshooting issues in console, see [Troubleshooting &#40;AccessToSQL&#41;](../../ssma/access/troubleshooting-accesstosql.md).  
  
