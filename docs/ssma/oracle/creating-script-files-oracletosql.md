---
description: "Creating Script Files (OracleToSQL)"
title: "Creating Script Files (OracleToSQL) | Microsoft Docs"
ms.service: sql
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
helpviewer_keywords: 
  - "Script File Creation, Configuring Oracle Console Settings"
  - "Script File Creation, Non-Configurable option"
  - "Script File Creation, Script File Validation"
ms.assetid: 55e5bc68-3040-4f07-bb00-0408a17c9821
author: cpichuka 
ms.author: cpichuka 
---
# Creating Script Files (OracleToSQL)
The first step before launching the SSMA console application is to create the script file and if required creating the variable value file and the server connection file.  
  
The script file can be divided into three sections, viz..,:  
  
1.  **config:** Enables the user to set the configuration parameters for the console application.  
  
2.  **servers:** Enables the user to set the source/target server definitions. This can also be in a separate server connection file.  
  
3.  **script-commands:** Enables the user to execute SSMA workflow commands.  
  
Each section is described in detail below:  
  
## Configuring Oracle Console Settings  
The configurations of a script are displayed in the console script file.  
  
If any of the elements are specified in the configuration node, they are set as the global setting i.e. they are applicable for all script commands. These configuration elements can also be set within each command in the script-command section if the user wants to override the global setting.  
  
The user-configurable options include:  
  
1.  **Output Window Provider:** If suppress-messages attribute is set to 'true', the command-specific messages do not get displayed on the console. The Attributes description is given below:  
  
    -   destination: Specifies whether the output needs to get printed to a file or stdout. This is false by default.  
  
    -   file-name: The path of the file (Optional).  
  
    -   suppress-messages: Suppresses messages on the console. This is 'false' by default.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <output-window  
  
        suppress-messages="<true/false>"   (optional)  
  
        destination="<file/stdout>"        (optional)  
  
        file-name="<file-name>"            (optional)  
  
       />  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <...All commands...>  
  
      <output-window  
  
         suppress-messages="<true/false>"   (optional)  
  
         destination="<file/stdout>"        (optional)  
  
         file-name="<file-name>"            (optional)  
  
       />  
  
    </...All commands...>  
    ```  
  
2.  **Data Migration Connection Provider:** This specifies which source/target server is to be considered for data-migration.  Source-use-last-used indicates that the last used source server is used for data migration. Similarly target-use-last-used indicates that the last used target server is used for data migration. The user can also specify the server (source or target) by using the attributes source-server or target-server.  
  
    Only one or the other specified attribute can be used i.e.:  
  
    -   source-use-last-used="true" (default) or source-server="source_servername"  
  
    -   target-use-last-used="true" (default) or target-server="target_servername"  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <data-migration-connection source-use-last-used="true"  
  
                                 target-server="<target-server-unique-name>"/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <migrate-data>  
  
      <data-migration-connection   source-server="<source-server-unique-name>"  
  
                                   target-use-last-used="true"/>  
  
    </migrate-data>  
    ```  
  
3.  **User Input Popup:** This allows handling of errors, when the objects are loaded from the database. The user provides the input modes, and in case of an error, the console proceeds as user specifies.  
  
    The modes include:  
  
    -   **ask-user -** Prompts the user to continue('yes') or error out ('no').  
  
    -   **error-** The console displays an error and halts the execution.  
  
    -   **continue-** The console proceeds with the execution.  
  
    The default mode is **error**.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <user-input-popup mode="<ask-user/continue/error>"/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <!-- Connect to target database -->  
  
    <connect-target-database server="<target-server-unique-name>">  
  
      <user-input-popup mode="<ask-user/continue/error>"/>  
  
    </connect-target-database>  
    ```  
  
4.  **Reconnect Provider:** This allows the user to set the reconnection settings incase of connection failures. This can be set for both source and target servers.  
  
    The reconnection modes are:  
  
    -   reconnect-to-last-used-server: If the connection is not active, it tries to reconnect to the last server used at most 5 times.  
  
    -   generate-an-error: If the connection is not active, an error is generated.  
  
    The default mode is **generate-an-error**.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <reconnect-manager on-source-reconnect="<reconnect-to-last-used-server/generate-an-error>"  
  
                         on-target-reconnect="<reconnect-to-last-used-server/generate-an-error>"/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <!--synchronization-->  
  
    <synchronize-target>  
  
      <reconnect-manager on-target-reconnect="reconnect-to-last-used-server"/>  
  
    </synchronize-target>  
    ```  
    *or*  
  
    ```xml  
    <!--data migration-->  
  
    <migrate-data server="<target-server-unique-name>">  
  
      <reconnect-manager  
  
        on-source-reconnect="reconnect-to-last-used-server"  
  
        on-target-reconnect="generate-an-error"/>  
  
    </migrate-data>  
    ```  
  
5.  **Converter Overwrite Provider:** This enables the user to handle objects that are already present on the target metabase. The possible actions include:  
  
    -   error: The console displays an error and halts the execution.  
  
    -   overwrite: Overwrites existing object values. This action is done by default.  
  
    -   skip: The console skips the objects that already exist on the database  
  
    -   ask-user: Prompts the user for input ('yes'/ 'no')  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <object-overwrite action="<error/skip/overwrite/ask-user>"/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <convert-schema object-name="<object-name>">  
  
      <object-overwrite action="<error/skip/overwrite/ask-user>"/>  
  
    </convert-schema>  
    ```  
  
6.  **Failed Prerequisites Provider:** This enables the user to handle any prerequisites that are required for processing a command. By default, strict-mode is 'false'. If it is set to 'true', an exception gets generated for failure to meet the prerequisites.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <prerequisites strict-mode="<true/false>"/>  
  
    </output-providers>  
    ```  
  
7.  **Stop Operation:** During the mid-operation, if the user wants to stop the operation, then **'Ctrl+C'** hotkey can be used. SSMA for Oracle Console will wait for the operation to complete and terminates the console execution.  
  
    If the user wants to stop the execution immediately, then, **'Ctrl+C'** hotkey can be pressed again for abrupt termination of the SSMA Console application.  
  
8.  **Progress Provider:** Informs the progress of each console command. This is disabled by default. The progress-reporting attributes comprise:  
  
    -   off  
  
    -   every-1%  
  
    -   every-2%  
  
    -   every-5%  
  
    -   every-10%  
  
    -   every-20%  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <progress-reporting enable="<true/false>"            (optional)  
  
                          report-messages="<true/false>"   (optional)  
  
                          report-progress="every-1%/every-2%/every-5%/every-10%/every-20%/off" (optional)/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <...All commands...>  
  
      <progress-reporting  
  
        enable="<true/false>"            (optional)  
  
        report-messages="<true/false>"   (optional)  
  
        report-progress="every-1%/every-2%/every-5%/every-10%/every-20%/off"     (optional)/>  
  
    </...All commands...>  
    ```  
  
9. **Logger Verbosity:** Sets log verbosity level. This corresponds with the All Categories option in the UI. By default, the log verbosity level is "error".  
  
    The logger-level options include:  
  
    -   fatal-error: Only fatal-error messages are logged.  
  
    -   error: Only error and fatal-error messages are logged.  
  
    -   warning: All levels except debug and info messages are logged.  
  
    -   info: All levels except debug messages are logged.  
  
    -   debug: All levels of messages logged.  
  
    > [!NOTE]  
    > Mandatory messages are logged at any level.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <log-verbosity level="fatal-error/error/warning/info/debug"/>  
  
    </output-providers>  
    ```  
    *or*  
  
    ```xml  
    <...All commands...>  
  
      <log-verbosity level="fatal-error/error/warning/info/debug"/>  
  
    </...All commands...>  
    ```  
  
10. **Override Encrypted Password:** If 'true', the clear text password specified in the server definition section of the server connection file or in the script file, overrides the encrypted password stored in protected storage if exists. If no password is specified in clear text, the user is prompted to enter the password.  
  
    Here two cases arise:  
  
    1.  If override option is **false**, the order of search will be Protected storage-&gt;Script File-&gt;Server Connection File-&gt; Prompt User.  
  
    2.  If override option is **true**, the order of search will be Script File-&gt;Server Connection File-&gt;Prompt User.  
  
    **Example:**  
  
    ```xml  
    <output-providers>  
  
      <encrypted-password override="<true/false>"/>  
  
    </output-providers>  
    ```  
  
The non-configurable option is:  
  
-   **Maximum Reconnect Attempts:** When an established connection times out or breaks due to network failure, the server is required to be reconnected. The reconnection attempts are allowed to a maximum of **5** retries after which, the console automatically performs the reconnection. The facility of automatic reconnection reduces your effort in rerunning the script.  
  
## Server Connection Parameters  
Server connection parameters can be defined in the script file or in the server connection file. Please refer to the [Creating the Server Connection Files &#40;OracleToSQL&#41;](../../ssma/oracle/creating-the-server-connection-files-oracletosql.md) section for more details.  
  
## Script Commands  
The script file contains a sequence of migration workflow commands in the XML format. The SSMA console application processes the migration in the order of the commands appearing in the script file.  
  
For example, a typical data migration of a specific table in an Oracle database follows the hierarchy of: Schema -&gt; Table.  
  
When all the commands in the script file are executed successfully, the SSMA console application exits and returns the control to the user. The contents of a script file are more or less static with variable information contained either in a [Creating Variable Value Files &#40;OracleToSQL&#41;](../../ssma/oracle/creating-variable-value-files-oracletosql.md) or, in a separate section within the script file for variable values.  
  
**Example:**  
  
```xml  
<!--Sample of script file commands -->  
  
<ssma-script-file>  
  
  <script-commands>  
  
    <create-new-project project-folder="<project-folder>"  
  
                        project-name="<project-name>"  
  
                        overwrite-if-exists="<true/false>"/>  
  
    <connect-source-database server="<source-server-unique-name>"/>  
  
    <save-project/>  
  
    <close-project/>  
  
  </script-commands>  
  
</ssma-script-file>  
```  
Templates consisting of 3 script files (for executing various scenarios), variable value file, and a server connection file are provided in the Sample Console Scripts folder of the product directory:  
  
-   AssessmentReportGenerationSample.xml  
  
-   ConversionAndDataMigrationSample.xml  
  
-   SqlStatementConversionSample.xml  
  
-   VariableValueFileSample.xml  
  
-   ServersConnectionFileSample.xml  
  
You can execute the templates (files) after changing the parameters displayed therein for relevancy.  
  
Complete list of script-commands can be found in [Executing the SSMA Console &#40;OracleToSQL&#41;](../../ssma/oracle/executing-the-ssma-console-oracletosql.md)  
  
## Script File Validation  
The user can easily validate his/her script file against the schema definition file **'O2SSConsoleScriptSchema.xsd'** available in the 'Schemas' folder.  
  
## Next Step  
The next step in operating the console is [Creating Variable Value Files &#40;OracleToSQL&#41;](../../ssma/oracle/creating-variable-value-files-oracletosql.md).  
  
## See Also  
[Creating Variable Value Files &#40;OracleToSQL&#41;](../../ssma/oracle/creating-variable-value-files-oracletosql.md)  
  
