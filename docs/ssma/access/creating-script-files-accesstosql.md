---
title: "Creating Script Files (AccessToSQL)"
description: "Creating Script Files (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
---
# Create script files (AccessToSQL)

Before you start the SQL Server Migration Assistant (SSMA) Console application, create the script file. If needed, create the variable value file and the server connection file.

The script file has three sections:

1. `config`: Set the configuration parameters for the console application.

1. `servers`: Set the source and target server definitions. You can also put these definitions in a separate server connection file.

1. `script-commands`: Execute SSMA workflow commands.

Each section is described in detail in the following sections:

## Configure access console settings

The console script file displays the configurations of a script.

If you specify any of the elements in the configuration node, you set the global setting for all script commands. To override the global setting, set these configuration elements within each command in the `script-command` section.

You can configure the following options:

1. **Output window provider**: If you set the `suppress-messages` attribute to `true`, the console doesn't display the command-specific messages. The attributes are:

   - `destination`: Specifies whether the output prints to a file or `stdout`. This attribute is `false` by default.

   - `file-name`: The path of the file (optional).

   - `suppress-messages`: Suppresses messages on the console. This attribute is `false` by default.

   **Example**:

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

1. **Data Migration Connection Provider**: Specifies which source or target server to use for data migration. `source-use-last-used` indicates that the last used source server is used for data migration. Similarly, `target-use-last-used` indicates that the last used target server is used for data migration. Use the `source-server` or `target-server` attributes to specify the server.

   You can only use one of the following attributes:

   - `source-use-last-used="true"` (default) or `source-server="source_servername"`
   - `target-use-last-used="true"` (default) or `target-server="target_servername"`

   **Example**:

   ```xml
   <output-providers>
     <data-migration-connection   source-use-last-used="true"
                                  target-server="target_1"/>
   </output-providers>
   ```

   *or*

   ```xml
   <migrate-data>
     <data-migration-connection   source-server="source_1"
                                  target-use-last-used="true"/>
   </migrate-data>
   ```

1. **User Input Popup**: Handles errors when the console loads objects from the database. The user provides the input modes, and if there's an error, the console proceeds as user specifies.

   The modes include:

   - `ask-user`: Prompts the user to continue (`yes`) or error out (`no`).
   - `error`: The console displays an error and halts the execution.
   - `continue`: The console proceeds with the execution.

   The default mode is `error`.

   **Example**:

   ```xml
   <output-providers>
     <user-input-popup mode="<ask-user/continue/error>"/>
   </output-providers>
   ```

   *or*

   ```xml
   <!-- Connect to target database -->
   <connect-target-database server="target_0">
     <user-input-popup mode="<ask-user/continue/error>"/>
   </connect-target-database>
   ```

1. **Reconnect Provider**: Sets the reconnection settings if there are connection failures. Set this option for both source and target servers.

   The reconnection modes are:

   - `reconnect-to-last-used-server`: If the connection isn't active, it tries to reconnect to the last server used at most five times.
   - `generate-an-error`: If the connection isn't active, the system generates an error.

   The default mode is `generate-an-error`.

   **Example**:

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
   <migrate-data server="target_0">
     <reconnect-manager
       on-source-reconnect="reconnect-to-last-used-server"
       on-target-reconnect="generate-an-error"/>
   </migrate-data>
   ```

1. **Converter Overwrite Provider**: Use this setting to handle objects that are already present on the target metabase. The possible actions include:

   - `error`: The console displays an error and halts the execution.
   - `overwrite`: Overwrites existing object values. This action is the default.
   - `skip`: The console skips the objects that already exist on the database.
   - `ask-user`: Prompts the user for input (`yes` or `no`).

   **Example**:

   ```xml
   <output-providers>
     <object-overwrite action="<error|skip|overwrite|ask-user>"/>
   </output-providers>
   ```

   *or*

   ```xml
   <convert-schema object-name="ssma.TT1">
     <object-overwrite action="<error|skip|overwrite|ask-user>"/>
   </convert-schema>
   ```

1. **Failed Prerequisites Provider**: Use this setting to handle any prerequisites that are required for processing a command. By default, `strict-mode` is `false`. If you set it to `true`, the system generates an exception for failure to meet the prerequisites.

   **Example**:

   ```xml
   <output-providers>
     <prerequisites strict-mode="<true|false>"/>
   </output-providers>
   ```

1. **Stop Operation**: To stop the operation during the mid-operation, use the **Ctrl**+**C** keyboard shortcut. SSMA Console waits for the operation to complete and terminates the console execution.

   If you want to stop the execution immediately, press the **Ctrl**+**C** keyboard shortcut again for abrupt termination of the SSMA Console application.

1. **Progress Provider**: Informs the progress of each console command. This setting is disabled by default. The progress-reporting attributes comprise:

   - `off`
   - `every-1%`
   - `every-2%`
   - `every-5%`
   - `every-10%`
   - `every-20%`

   **Example**:

   ```xml
   <output-providers>
     <progress-reporting enable="<true|false>"           (optional)
                         report-messages="<true|false>"  (optional)
                         report-progress="every-1%|every-2%|every-5%|every-10%|every-20%|off" (optional)/>
   </output-providers>
   ```

   *or*

   ```xml
   <...All commands...>
     <progress-reporting
       enable="<true|false>"              (optional)
       report-messages="<true|false>"     (optional)
       report-progress="every-1%|every-2%|every-5%|every-10%|every-20%|off"     (optional)/>
   </...All commands...>
   ```

1. **Logger Verbosity**: Sets log verbosity level. This setting corresponds with the **All Categories** option in the UI. By default, the log verbosity level is `error`.

   The logger-level options include:

   - `fatal-error`: Logs only fatal error messages.
   - `error`: Logs only error and fatal error messages.
   - `warning`: Logs all levels except debug and info messages.
   - `info`: Logs all levels except debug messages.
   - `debug`: Logs all levels of messages.

   > [!NOTE]  
   > SSMA logs mandatory messages at any level.

   **Example**:

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

1. **Override Encrypted Password**: If `true`, the server definition section of the server connection file or the script file uses the clear text password to override the encrypted password stored in protected storage, if it exists. If the clear text password isn't specified, the user is prompted to enter the password.

   Two cases arise:

   1. If the override option is `false`, the order of search is Protected storage > Script file > Server connection file > Prompt user.

   1. If the override option is `true`, the order of search is Script file > Server connection file > Prompt user.

   **Example**:

   ```xml
   <output-providers>
     <encrypted-password override="<true/false>"/>
   </output-providers>
   ```

The non-configurable option is:

- **Maximum Reconnect Attempts**: When an established connection times out or breaks due to network failure, the server needs to reconnect. The console allows up to five (`5`) retries for reconnection. After these retries, the console automatically performs the reconnection. The automatic reconnection feature reduces your effort in rerunning the script.

## Server connection parameters

You can define server connection parameters in the script file or in the server connection file. For more information, see [Create the server connection files](creating-the-server-connection-files-accesstosql.md).

## Script commands

The script file contains a sequence of migration workflow commands in the XML format. The SSMA Console application processes the migration in the order of the commands appearing in the script file.

For example, a typical data migration of a specific table in an Access database follows the hierarchy of: Database > Table.

When all the commands in the script file execute successfully, the SSMA Console application exits and returns control to the user. The contents of a script file are more or less static, with variable information contained either in a [Create variable value files](creating-variable-value-files-accesstosql.md) or in a separate section within the script file for variable values.

**Example**:

```xml
<!--Sample of script file commands -->
<ssma-script-file>
  <script-commands>
    <create-new-project project-folder="$project_folder$"
                        project-name="$project_name$"
                        overwrite-if-exists="true"/>
    <connect-source-database server="source_2"/>
    <save-project/>
    <close-project/>
  </script-commands>
</ssma-script-file>
```

The Sample Console Scripts folder of the product directory provides templates that consist of three script files (for executing various scenarios), a variable value file, and a server connection file:

- `AssessmentReportGenerationSample.xml`
- `ConversionAndDataMigrationSample.xml`
- `VariableValueFileSample.xml`
- `ServersConnectionFileSample.xml`

You can execute the templates after changing the parameters for relevancy.

For a complete list of script commands, see [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md).

## Script file validation

You can validate your script file against the schema definition file `A2SSConsoleScriptSchema.xsd` in the `Schemas` folder.

## Related content

- [Create variable value files](creating-variable-value-files-accesstosql.md)
