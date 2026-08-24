---
title: "Executing the SSMA Console (SybaseToSQL)"
description: Executing the SSMA Console (SybaseToSQL)
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 11/10/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
helpviewer_keywords:
  - "Sybase Console,Database Connection Commands"
  - "Sybase Console,Manageability Commands"
  - "Sybase Console,Migration Commands"
  - "Sybase Console,Migration Preparation Command"
  - "Sybase Console,Project Commands"
  - "Sybase Console,Report Commands"
  - "Sybase Console,Script File Commands"
  - "Sybase Console,Script Generation Commands"
---
# Executing the SSMA console (SybaseToSQL)

Microsoft provides you with a robust set of script file commands to execute and control SQL Server Migration Assistant (SSMA) activities. The ensuing sections detail the same.

## Script file commands

The console application uses certain standard script file commands as enumerated in this section.

## Project commands

The Project commands handle creating projects, opening, saving, and exiting projects.

### `create-new-project` command

This command creates a new SSMA project.

- `project-folder` indicates the folder of the project getting created.

- `project-name` indicates the name of the project. {string}

- `overwrite-if-exists` Optional attribute indicates whether an existing project should be overwritten. {boolean}

- `project-type`: Optional attribute. Indicates the project type, that is, `sql-server-2016`, `sql-server-2017`, `sql-server-2019`, `sql-server-2022`, `sql-server-2025`, or `sql-azure`. The default is `sql-server-2016`.

#### Syntax example

```xml
<create-new-project
  project-folder="<project-folder>"
  project-name="<project-name>"
  overwrite-if-exists="<true/false>" (optional)
  project-type=="<sql-server-2016 | sql-server-2017 | sql-server-2019 | sql-server-2022 | sql-server-2025 | sql-azure>"
/>
```

Attribute `overwrite-if-exists` is `false` by default.

Attribute `project-type` is `sql-server-2016` by default.

### `open-project` command

This command opens the project.

- `project-folder` indicates the folder of the project getting created. The command fails if the specified folder doesn't exist. {string}

- `project-name` indicates the name of the project. The command fails if the specified project doesn't exist. {string}

#### Syntax example

```xml
<open-project
  project-folder="<project-folder>"
  project-name="<project-name>"
/>
```

> [!NOTE]  
> The SSMA for SAP ASE Console Application supports backward compatibility. You can use it to open projects created by previous version of SSMA.

### `save-project` command

This command saves the migration project.

#### Syntax example

```xml
<save-project/>
```

### `close-project` command

This command closes the migration project.

#### Syntax example

```xml
<close-project
  if-modified="<save/error/ignore>"   (optional)
/>
```

Attribute `if-modified` is optional, `ignore` by default.

## Database Connection commands

The Database Connection commands help connect to the database.

The **Browse** feature of the UI isn't supported in console.  

For more information, see [Creating Script Files](creating-script-files-sybasetosql.md).

### `connect-source-database` command

This command performs connection to the source database and loads high-level metadata of the source database, but not all of the metadata.

If the connection to the source can't be established, an error is generated and the console application stops further execution.

The server definition is retrieved from the name attribute defined for each connection in the server section of the server connection file or the script file.

#### Syntax example

```xml
<connect-source-database  server="<server-unique-name>"/>
```

### `force-load-source/target-database` command

This command loads the source metadata, and it's useful for working on migration project offline.

If the connection to the source/target can't be established, an error is generated and the console application stops further execution.

This command requires one or several metabase nodes as command-line parameter.

#### Syntax example

```xml
<force-load metabase="<source/target>" >
  <metabase-object object-name="<object-name>"/>
</force-load>
```

### `reconnect-source-database` command

This command reconnects to the source database but doesn't load any metadata unlike the connect-source-database command.

If (re)connection with the source can't be established, an error is generated and the console application stops further execution.

#### Syntax example

```xml
<reconnect-source-database  server="<server-unique-name>"/>
```

### `connect-target-database` command

This command connects to the target SQL Server database and loads high-level metadata of the target database but not the metadata entirely.

If the connection to the target can't be established, an error is generated and the console application stops further execution.

The server definition is retrieved from the name attribute defined for each connection, in the server section of the server connection file or the script file.

#### Syntax example

```xml
<connect-target-database  server="<server-unique-name>"/>
```

### `reconnect-target-database` command

This command reconnects to the target database but doesn't load any metadata, unlike the connect-target-database command.

If (re)connection to the target can't be established, an error is generated and the console application stops further execution.

#### Syntax example

```xml
<reconnect-target-database  server="<server-unique-name>"/>
```

## Report commands

The Report commands generate reports on the performance of various SSMA Console activities.

### `generate-assessment-report` command

This command generates assessment reports on the source database.

If the source database connection isn't performed before executing this command, an error is generated and the console application exits.

Failure to connect to the source database server during the command execution, also results in terminating the console application.

- `conversion-report-folder`: Specifies the folder in which the assessment report can be stored. (optional attribute)

- `object-name`: Specifies the objects considered for assessment report generation (supports individual object names or a group object name).

- `object-type`: Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type is "category").

- `conversion-report-overwrite`: Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value**: false. (optional attribute)

- `write-summary-report-to`: Specifies the path at which the report is generated.

  If only the folder path is mentioned, then file by name `AssessmentReport<n>.xml` is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` (="true/false", with default as "false" (optional attributes))

  - `verbose` (="true/false", with default as "false" (optional attributes))

#### Syntax example

```xml
<generate-assessment-report
  object-name="<object-name>"
  object-type="<object-category>"
  write-summary-report-to="<file-name/folder-name>"             (optional)
  verbose="<true/false>"                       (optional)
  report-errors="<true/false>"                 (optional)
  assessment-report-folder="<folder-name>"          (optional)
  conversion-report-overwrite="<true/false>"   (optional)
/>
```

Or:

```xml
<generate-assessment-report
  assessment-report-folder="<folder-name>"            (optional)
  conversion-report-overwrite="<true/false>"     (optional)
>
<metabase-object object-name="<object-name>"
   object-type="<object-category>"/>
</generate-assessment-report>
```

## Migration commands

The Migration commands convert the target database schema to the source schema and migrate data to the target server.

### `convert-schema` command

This command performs schema conversion from source to the target schema.

If the source or target database connection isn't performed before executing this command, or the connection to the source or target database server fails during the command execution, an error is generated and the console application exits.

- `conversion-report-folder`: Specifies folder in which the assessment report can be stored. (optional attribute)

- `object-name`: Specifies the source objects considered for converting schema (supports individual object names or a group object name).

- `object-type`: Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type is "category").

- `conversion-report-overwrite`: Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value**: false. (optional attribute)

- `write-summary-report-to`: Specifies the path at which the summary report is generated.

  If only the folder path is mentioned, then file by name `SchemaConversionReport<n>.xml` is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` (="true/false", with default as "false" (optional attributes))

  - `verbose` (="true/false", with default as "false" (optional attributes))

#### Syntax example

```xml
<convert-schema
  object-name="<object-name>"
  object-type="<object-category>"
  write-summary-report-to="<file-name/folder-name>"     (optional)
  verbose="<true/false>"                          (optional)
  report-errors="<true/false>"                    (optional)
  conversion-report-folder="<folder-name>"             (optional)
  conversion-report-overwrite="<true/false>"      (optional)
/>
```

Or:

```xml
<convert-schema
  conversion-report-folder="<folder-name>"         (optional)
  conversion-report-overwrite="<true/false>"> (optional)
  <metabase-object object-name="<object-name>"
    object-type="<object-category>"/>
</convert-schema>
```

### `migrate-data` command

This command migrates the source data to the target.

- `object-name`: Specifies the source objects considered for migrating data (supports individual object names or a group object name).

- `object-type`: specifies the type of the object called out in the object-name attribute (if object category is specified then object type is "category").

- `write-summary-report-to`: Specifies the path at which the report is generated.

  If only the folder path is mentioned, then file by name `DataMigrationReport<n>.xml` is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` (="true/false", with default as "false" (optional attributes))

  - `verbose` (="true/false", with default as "false" (optional attributes))

#### Syntax example

```xml
<migrate-data
  write-summary-report-to="<file-name/folder-name>"
  report-errors="<true/false>" verbose="<true/false>">
    <metabase-object object-name="<object-name>"/>
    <metabase-object object-name="<object-name>"/>
    <metabase-object object-name="<object-name>"/>
    <data-migration-connection
      source-use-last-used="true"/source-server="<server-unique-name>"
      target-use-last-used="true"/target-server="<server-unique-name>"/>
</migrate-data>
```

Or:

```xml
<migrate-data
  object-name="<object-name>"
  object-type="<object-category>"
  write-summary-report-to="<file-name/folder-name>"
  report-errors="<true/false>" verbose="<true/false>"/>
```

## Migration Preparation command

The Migration Preparation command initiates schema mapping between the source and target databases.

> [!NOTE]  
> The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.

### `map-schema` command

This command provides the schema mapping of the source database to the target schema.

- `source-schema` Specifies the source schema to migrate.

- `sql-server-schema` Specifies the target schema to which the source schema is migrated.

#### Syntax example

```xml
<map-schema source-schema="<source-schema>"
sql-server-schema="<target-schema>"/>
```

## Manageability commands

The Manageability commands help synchronize the target database objects with the source database.

> [!NOTE]  
> The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.

### `synchronize-target` command

This command synchronizes the target objects with the target database.

If this command is executed against the source database, an error is encountered.

If the target database connection isn't performed before executing this command, or the connection to the target database server fails during the command execution, an error is generated and the console application exits.

- `object-name`: Specifies the target objects considered for synchronizing with target database (supports individual object names or a group object name).

- `object-type`: Specifies the type of the object called out in the object-name attribute (if object category is specified then object type is "category").

- `on-error`: Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:

  - `report-total-as-warning`

  - `report-each-as-warning`

  - `fail-script`

- `report-errors-to`: Specifies location of error report for the synchronization operation (optional attribute)

  If only the folder path is given, then a file by the name of `TargetSynchronizationReport.xml` is created.

#### Syntax example

```xml
<synchronize-target
object-name="<object-name>"
on-error="<report-total-as-warning/report-each-as-warning/fail-script>" (optional)
  report-errors-to="<file-name/folder-name>"        (optional)
/>
```

Or:

```xml
<synchronize-target
  object-name="<object-name>"
  object-type="<object-category>"/>
```

Or:

```xml
<synchronize-target>
  <metabase-object object-name="<object-name>"/>
  <metabase-object object-name="<object-name>"/>
  <metabase-object object-name="<object-name>"/>
</synchronize-target>
```

### `refresh-from-database` command

This command refreshes the source objects from database.

If this command is executed against the target database, an error is generated.

This command requires one or several metabase nodes as command-line parameter.

- `object-name`: Specifies the source objects considered for refreshing from source database (supports individual object names or a group object name).

- `object-type`: Specifies the type of the object specified in the object-name attribute (if object category is specified then object type is "category").

- `on-error`: Specifies whether to call out refresh errors as warnings or errors. Available options for on-error:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

- `report-errors-to`: Specifies location of error report for the synchronization operation (optional attribute)

  If only the folder path is given, then a file by the name of `SourceDBRefreshReport.xml` is created.

#### Syntax example

```xml
<refresh-from-database
  object-name="<object-name>"
  on-error="<report-total-as-warning/
             report-each-as-warning/
             fail-script>"              (optional)
  report-errors-to="<file-name/folder-name>"        (optional)
/>
```

Or:

```xml
<refresh-from-database
  object-name="<object-name>"
  object-type="<object-category>" />
```

Or:

```xml
<refresh-from-database>
  <metabase-object object-name="<object-name>"/>
</refresh-from-database>
```

## Script Generation commands

The Script Generation commands perform dual tasks: they help save the console output in a script file, and they record the T-SQL output to the console or a file based on the parameter you specify.

### `save-as-script` command

Used to save the scripts of the objects to a file mentioned when `metabase=target`. This is an alternative to the synchronization command, in which we get the scripts and execute the same on the target database.

This command requires one or several metabase nodes as command-line parameter.

- `object-name`: Specifies the objects whose scripts are to be saved (supports individual object names or a group object name).

- `object-type`: Specifies the type of the object called out in the object-name attribute (if object category is specified, then object type is "category").

- `metabase`: Specifies whether it's the source or target metabase.

- `destination`: Specifies the path or the folder in which the script must be saved. If the file name isn't given, then a file name in the format `(object_name attribute value).out` is provided.

- `overwrite`: If true, then it overwrites the same filename if it exists. It can have the values (true/false).

#### Syntax example

```xml
<save-as-script
  metabase="<source/target>"
  object-name="<object-name>"
  object-type="<object-category>"
  destination="<file-name/folder-name>"
  overwrite="<true/false>"   (optional)
/>
```

Or:

```xml
<save-as-script
  metabase="<source/target>"
  destination="<file-name/folder-name>"
    <metabase-object object-name="<object-name>"
                     object-type="<object-category>"/>
</save-as-script>
```

### `convert-sql-statement` command

This command converts the SQL statement.

- `context` Specifies the schema name.

- `destination` Specifies whether the output should be stored in a file.

  If this attribute isn't specified, then the converted T-SQL statement is displayed on the console. (optional attribute)

- `conversion-report-folder` Specifies the folder in which the assessment report can be stored. (optional attribute)

- `conversion-report-overwrite` Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value**: false. (optional attribute)

- `write-converted-sql-to` specifies the file (or) folder path to which the converted T-SQL should be stored. When a folder path is specified along with the `sql-files` attribute, each source file has a corresponding target T-SQL file created under the specified folder. When a folder path is specified along with the `sql` attribute, the converted T-SQL is written to a file named Result.out under the specified folder.

- `sql` specifies the Sybase sql statements to be converted, one or more statements can be separated using a ";"

- `sql-files` specifies the path of the sql files that has to be converted to T-SQL code.

- `write-summary-report-to` specifies the path where the summary report is generated. If only the folder path is mentioned, then file by name `ConvertSQLReport.xml` is created. (optional attribute)

  Summary report creation has two further subcategories, namely:

  - report-errors (="true/false", with default as "false" (optional attributes)).

  - verbose (="true/false", with default as "false" (optional attributes)).

This command requires one or several metabase nodes as command-line parameter.

#### Syntax example

```xml
<convert-sql-statement
       context="<database-name>.<schema-name>"
        conversion-report-folder="<folder-name>"
        conversion-report-overwrite="<true/false>"
        write-summary-report-to="<file-name/folder-name>"   (optional)
        verbose="<true/false>"   (optional)
        report-errors="<true/false>"   (optional)
        destination="<stdout/file>"   (optional)
        write-converted-sql-to ="<file-name/folder-name>"
        sql="SELECT 1 FROM DUAL;">
    <output-window suppress-messages="<true/false>" />
</convert-sql-statement>
```

Or:

```xml
<convert-sql-statement
         context="<database-name>.<schema-name>"
         conversion-report-folder="<folder-name>"
         conversion-report-overwrite="<true/false>"
         write-summary-report-to="<file-name/folder-name>"   (optional)
         verbose="<true/false>"   (optional)
         report-errors="<true/false>"   (optional)
         destination="<stdout/file>"   (optional)
         write-converted-sql-to ="<file-name/folder-name>"
         sql-files="<folder-name>\*.sql"
/>
```

Or:

```xml
<convert-sql-statement
         context="<database-name>.<schema-name>"
         conversion-report-folder="<folder-name>"
         conversion-report-overwrite="<true/false>"
         sql-files="<folder-name>\*.sql"
/>
```

## Related content

- [Command-line options in the SSMA Console](../access/command-line-options-in-ssma-console-accesstosql.md)
- [Working with the sample console script files](working-with-the-sample-console-script-files-sybasetosql.md)
- [Managing Passwords](managing-passwords-sybasetosql.md)
- [Generating Reports](generating-reports-sybasetosql.md)
- [Troubleshooting](troubleshooting-sybasetosql.md)
