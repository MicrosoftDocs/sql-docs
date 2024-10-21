---
title: "Create script files (Db2ToSQL)"
description: Learn how to create the script file, the variable value file, and the server connection file in SSMA for Db2.
author: "F"
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Create script files (Db2ToSQL)

Before you can launch the SQL Server Migration Assistant (SSMA) console application, you must create the script file. If necessary, you can also create the variable value file and the server connection file.

The script file can be divided into three sections:

| Section | Description |
| --- | --- |
| `config` | Set the configuration parameters for the console application. |
| `servers` | Set the source/target server definitions. Can also be in a separate server connection file. |
| `script-commands` | Execute SSMA workflow commands. |

Each section is described in detail in this article.

## Configure SSMA console application settings

The configurations of a script are displayed in the console script file.

If any of the elements are specified in the configuration node, they're set as the global setting. In other words, they apply to all script commands. These configuration elements can also be set within each command in the script-command section if you want to override the global setting.

The user-configurable options include:

1. **Output Window Provider**: If `suppress-messages` attribute is set to `true`, the command-specific messages aren't displayed on the console.

   | Attribute | Description |
   | --- | --- |
   | `destination` | Specifies whether the output needs to get printed to a file or stdout. `false` by default. |
   | `file-name` (optional) | The path of the file. |
   | `suppress-messages` | Suppresses messages on the console. `false` by default. |

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

1. **Data Migration Connection Provider**: Specifies which source/target server is to be considered for data-migration. Source-use-last-used indicates that the last used source server is used for data migration. Similarly target-use-last-used indicates that the last used target server is used for data migration. You can also specify the server (source or target) by using the attributes source-server or target-server.

   Only one of these attributes can be set at a time:

   - `source-use-last-used="true"` (default) or `source-server="<source-server-unique-name>"`
   - `target-use-last-used="true"` (default) or `target-server="<target-server-unique-name>"`

   **Example:**

   ```xml
   <output-providers>
     <data-migration-connection   source-use-last-used="true"
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

1. **User Input Popup**: Allows handling of errors, when the objects are loaded from the database. You provide the input modes, and if there's an error, the console proceeds as specified.

   | Mode | Description |
   | --- | --- |
   | `ask-user` | Prompts you to continue (`yes`) or error out (`no`). |
   | `error` (default) | The console displays an error and halts the execution. |
   | `continue` | The console proceeds with the execution. |

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

1. **Reconnect Provider**: Allows you to set the reconnection settings if there's a connection failure. This can be set for both source and target servers.

   | Reconnection mode | Description |
   | --- | --- |
   | `reconnect-to-last-used-server` | If the connection isn't active, it tries to reconnect to the last server used five times at most. |
   | `generate-an-error` (default) | If the connection isn't active, an error is generated. |

    **Example:**

    ```xml
    <output-providers>
      <reconnect-manager  on-source-reconnect="<reconnect-to-last-used-server/generate-an-error>"
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

1. **Converter Overwrite Provider**: Enables you to handle objects that are already present on the target metabase.

   | Action | Description |
   | --- | --- |
   | `error` | The console displays an error and halts the execution. |
   | `overwrite` (default) | Overwrites existing object values. |
   | `skip` | The console skips the objects that already exist on the database. |
   | `ask-user` | Prompts you for input (`yes` / `no`). |

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

1. **Failed Prerequisites Provider**: You can handle any prerequisites that are required for processing a command. By default, `strict-mode` is `false`. If `true`, an exception gets generated for failure to meet the prerequisites.

   **Example:**

   ```xml
   <output-providers>
     <prerequisites strict-mode="<true/false>"/>
   </output-providers>
   ```

1. **Stop Operation**: During the mid-operation, if you want to stop the operation, then the **Ctrl**+**C** hotkey can be used. SSMA for SSMA console application waits for the operation to complete and terminates the console execution.

   If you want to stop the execution immediately, then the **Ctrl**+**C** hotkey can be pressed again for termination of the SSMA console application.

1. **Progress Provider**: Informs the progress of each console command. Disabled by default. The progress-reporting attributes comprise:

   - `off`
   - `every-1%`
   - `every-2%`
   - `every-5%`
   - `every-10%`
   - `every-20%`

   **Example:**

   ```xml
   <output-providers>
     progress-reporting   enable="<true/false>"            (optional)
                          report-messages="<true/false>"   (optional)
                          report-progress="every-1%/every-2%/every-5%/every-10%/every-20%/off" (optional)/>
   </output-providers>
   ```

   *or*

   ```xml
   <...All commands...>
     <progress-reporting
       enable="<true/false>"              (optional)
       report-messages="<true/false>"     (optional)
       report-progress="every-1%/every-2%/every-5%/every-10%/every-20%/off"     (optional)/>
   </...All commands...>
   ```

1. **Logger Verbosity**: Sets log verbosity level. This corresponds with the **All Categories** option in the UI.

   | Logger level | Description |
   | --- | --- |
   | `fatal-error` | Only fatal-error messages are logged. |
   | `error` (default) | Only error and fatal-error messages are logged. |
   | `warning` | All levels except debug and info messages are logged. |
   | `info` | All levels except debug messages are logged. |
   | `debug` | All levels of messages logged. |

   Mandatory messages are logged at any level.

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

1. **Override Encrypted Password**: If `true`, the clear text password specified in the server definition section of the server connection file or in the script file, overrides the encrypted password stored in protected storage if it exists. If no password is specified in clear text, you're prompted to enter the password.

   Here, two cases arise:

   1. If override option is **false**, the order of search is Protected storage > Script File > Server Connection File > Prompt User.

   1. If override option is **true**, the order of search is Script File > Server Connection File > Prompt User.

   **Example:**

   ```xml
   <output-providers>
     <encrypted-password override="<true/false>"/>
   </output-providers>
   ```

The nonconfigurable option is:

- **Maximum Reconnect Attempts**: When an established connection times out or breaks due to network failure, the server is required to be reconnected. The reconnection attempts are allowed to a maximum of `5` retries after which, the console automatically performs the reconnection. The facility of automatic reconnection reduces your effort in rerunning the script.

## Server connection parameters

Server connection parameters can be defined in the script file or in the server connection file. For more information, see [Create the server connection files](creating-the-server-connection-files-db2tosql.md).

## Script commands

The script file contains a sequence of migration workflow commands in the XML format. The SSMA console application processes the migration in the order of the commands appearing in the script file.

For example, a typical data migration of a specific table in a Db2 database follows the hierarchy of *schema* > *table*.

When all the commands in the script file are executed successfully, the SSMA console application exits. The contents of a script file are more or less static with variable information contained either in a [variable value file](creating-variable-value-files-db2tosql.md) or, in a separate section within the script file for variable values.

**Example:**

Here's a sample of the script file commands:

```xml
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

Templates consisting of three script files (for executing various scenarios), variable value file, and a server connection file are provided in the Sample Console Scripts folder of the product directory:

- `AssessmentReportGenerationSample.xml`
- `ConversionAndDataMigrationSample.xml`
- `SqlStatementConversionSample.xml`
- `VariableValueFileSample.xml`
- `ServersConnectionFileSample.xml`

You can execute the templates (files) after changing the parameters displayed therein for relevancy.

Complete list of script-commands can be found in [Execute the SSMA console](executing-the-ssma-console-db2tosql.md)

## Script file validation

You can validate the script file against the schema definition file `O2SSConsoleScriptSchema.xsd`, available in the `Schemas` folder.

## Related content

- [Create variable value files (Db2ToSQL)](creating-variable-value-files-db2tosql.md)
