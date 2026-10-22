---
title: "Execute the SSMA Console (Db2ToSQL)"
description: Learn about running commands in the SSMA console for Db2.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 11/10/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# Execute the SSMA console (Db2ToSQL)

Microsoft provides you with a robust set of script file commands to execute and control SQL Server Migration Assistant (SSMA) activities. The ensuing sections detail the same. The console application uses certain standard script file commands as enumerated in this section.

## Project script file commands

The Project commands handle creating projects, opening, saving, and exiting projects.

### `create-new-project` command

Creates a new SSMA project.

#### Script

- `project-folder`. Indicates the folder of the project getting created.

- `project-name`. Indicates the name of the project. {string}

- `overwrite-if-exists`. Optional attribute. Indicates if an existing project should be overwritten. {boolean}

### `project-type` command

Optional attribute. Indicates the project type, that is, `sql-server-2016`, `sql-server-2017`, `sql-server-2019`, `sql-server-2022`, `sql-server-2025`, or `sql-azure`. The default is `sql-server-2016`.

#### Syntax example

```xml
<create-new-project
   project-folder="<project-folder>"
   project-name="<project-name>"
   overwrite-if-exists="<true/false>"   (optional)
   project-type="<sql-server-2016 | sql-server-2017 | sql-server-2019 | sql-server-2022 | sql-azure>"   (optional)
/>
```

Attribute `overwrite-if-exists` is `false` by default.

Attribute `project-type` is `sql-server-2016` by default.

### `open-project` command

Opens an existing project.

#### Script

- `project-folder` indicates the folder of the project getting created. The command fails if the specified folder doesn't exist. {string}

- `project-name` indicates the name of the project. The command fails if the specified project doesn't exist. {string}

#### Syntax example

```xml
<open-project
   project-folder="<project-folder>"
   project-name="<project-name>"
/>
```

SSMA for Db2 Console Application supports backward compatibility. You can open projects created by previous version of SSMA.

### `save-project` command

Saves the migration project.

#### Syntax example

```xml
<save-project/>
```

### `close-project` command

Closes the migration project.

#### Syntax example

```xml
<close-project
   if-modified="<save/error/ignore>"   (optional)
/>
```

## Database connection script file commands

The Database Connection commands help connect to the database.

The **Browse** feature of the UI isn't supported in console.

For more information, see [Create script files](creating-script-files-db2tosql.md).

### `connect-source-database` command

Performs connection to the source database and loads high level metadata of the source database but not all of the metadata.

If the connection to the source can't be established, an error is generated and the console application stops further execution

#### Script

The server definition is retrieved from the name attribute defined for each connection, in the server section of the server connection file or the script file.

#### Syntax example

```xml
<connect-source-database  server="<server-unique-name>"/>
```

### `force-load-source-database`/`force-load-target-database`

Loads the source metadata.

Useful for working on migration project offline.

If the connection to the source/target can't be established, an error is generated and the console application stops further execution

#### Script

Requires one or several metabase nodes as command line parameter.

#### Syntax example

```xml
<force-load object-name="<object-name>"
  metabase="<source/target>"/>
```

Or:

```xml
<force-load>
   <metabase-object object-name="<object-name>"/>
</force-load>
```

### `reconnect-source-database`

Reconnects to the source database but doesn't load any metadata unlike the connect-source-database command.

If (re)connection with the source can't be established, an error is generated and the console application stops further execution.

#### Syntax example

```xml
<reconnect-source-database  server="<server-unique-name>"/>
```

### `connect-target-database`

Connects to the target SQL Server database and loads high level metadata of the target database but not the metadata entirely.

If the connection to the target can't be established, an error is generated and the console application stops further execution.

#### Script

The erver definition is retrieved from the name attribute defined for each connection, in the server section of the server connection file or the script file

#### Syntax example

```xml
<connect-target-database  server="<server-unique-name>"/>
```

### `reconnect-target-database`

Reconnects to the target database but doesn't load any metadata, unlike the connect-target-database command.

If (re)connection to the target can't be established, an error is generated and the console application stops further execution.

#### Syntax example

```xml
<reconnect-target-database  server="<server-unique-name>"/>
```

## Report script file commands

The Report commands generate reports on the performance of various SSMA console activities.

### `generate-assessment-report`

Generates assessment reports on the source database.

If the source database connection isn't performed before executing this command, an error is generated and the console application exits.

Failure to connect to the source database server during the command execution, also results in terminating the console application.

#### Script

- `conversion-report-folder`: Specifies folder where the assessment report can be stored. (optional attribute)

- `object-name`: Specifies the objects considered for assessment report generation (It can have individual object names or a group object name).

- `object-type`: specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `conversion-report-overwrite`: Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value:** false. (optional attribute)

- `write-summary-report-to`: Specifies the path where the summary report is generated.

  If only the folder path is mentioned, then file by name **AssessmentReport&lt;n&gt;.XML** is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` `true` or `false`, with default as `false` (optional attributes)
  - `verbose` `true` or `false`, with default as `false` (optional attributes)

#### Syntax example

```xml
<generate-assessment-report
   object-name="<object-name>"
   object-type="<object-category>"
   write-summary-report-to="<file>"   (optional)
   verbose="<true/false>"   (optional)
   report-errors="<true/false>"   (optional)
   assessment-report-folder="<folder-name>"   (optional)
   conversion-report-overwrite="<true/false>"   (optional)
/>
```

Or:

```xml
<generate-assessment-report
   conversion-report-folder="<folder-name>"   (optional)
   conversion-report-overwrite="<true/false>"   (optional)
>
      <metabase-object object-name="<object-name>"
         object-type="<object-category>"/>
</generate-assessment-report>
```

## Migration script file commands

The Migration commands convert the target database schema to the source schema and migrates data to the target server. The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.

### `convert-schema`

Performs schema conversion from source to the target schema.

If the source or target database connection isn't performed before executing this command, or the connection to the source or target database server fails during the command execution, an error is generated and the console application exits.

#### Script

- `conversion-report-folder`: Specifies folder where the assessment report can be stored. (optional attribute)

- `object-name`: Specifies the source objects considered for converting schema (It can have individual object names or a group object name).

- `object-type`: specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `conversion-report-overwrite`: Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value:** false. (optional attribute)

- `write-summary-report-to`: Specifies the path where the summary report is generated.

  If only the folder path is mentioned, then file by name **SchemaConversionReport&lt;n&gt;.XML** is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` `true` or `false`, with default as `false` (optional attributes)

  - `verbose` `true` or `false`, with default as `false` (optional attributes)

#### Syntax example

```xml
<convert-schema
   object-name="<object-name>"
   object-type="<object-category>"
   write-summary-report-to="<file-name/folder-name>"   (optional)
   verbose="<true/false>"   (optional)
   report-errors="<true/false>"   (optional)
   conversion-report-folder="<folder-name>"   (optional)
   conversion-report-overwrite="<true/false>"   (optional)
/>
```

Or:

```xml
<convert-schema
   conversion-report-folder="<folder-name>"   (optional)
   conversion-report-overwrite="<true/false>"   (optional)
      <metabase-object object-name="<object-name>"
         object-type="<object-category>"/>
</convert-schema>
```

### `migrate-data` command

Migrates the source data to the target.

#### Script

- `conversion-report-folder`: Specifies folder where the assessment report can be stored. (optional attribute)

- `object-name`: Specifies the source objects considered for migrating data (It can have individual object names or a group object name).

- `object-type`: specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `conversion-report-overwrite`: Specifies whether to overwrite the assessment report folder if it already exists.

  **Default value:** false. (optional attribute)

- `write-summary-report-to`: Specifies the path where the summary report is generated.

  If only the folder path is mentioned, then file by name `DataMigrationReport<n>.xml` is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors` `true` or `false`, with default as `false` (optional attributes)
  - `verbose` `true` or `false`, with default as `false` (optional attributes)

#### Syntax example

```xml
<migrate-data
   write-summary-report-to="<file-name/folder-name>"
   report-errors="<true/false>"
   verbose="<true/false>">
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
   report-errors="<true/false>"
   verbose="<true/false>"/>
```

## Migration preparation script file commands

The Migration Preparation command initiates schema mapping between the source and target databases.

### `map-schema` command

Schema mapping of source database to the target schema.

### `source-schema` command

Specifies the source schema we intend to migrate.

### `sql-server-schema` command

Specifies the target schema where we want it to be migrated.

#### Syntax example

```xml
<map-schema
   source-schema="<source-schema>"
   sql-server-schema="<target-schema>"/>
```

### `map-schema` command

Schema mapping of source database to the target schema.

#### Script

`source-schema` specifies the source schema we intend to migrate.

`sql-server-schema` specifies the target schema where we want it to be migrated.

#### Syntax example

```xml
<map-schema
   source-schema="<source-schema>"
   sql-server-schema="<target-schema>"/>
```

## Manageability script file commands

The Manageability commands help synchronize the target database objects with the source database.

The default console output setting for the migration commands is 'Full' output report with no detailed error reporting: Only summary at the source object tree root node.

### `synchronize-target` command

Synchronizes the target objects with the target database.

If this command is executed against the source database, an error is encountered.

If the target database connection isn't performed before executing this command, or the connection to the target database server fails during the command execution, an error is generated and the console application exits.

#### Script

- `object-name`: Specifies the target objects considered for synchronizing with target database (It can have individual object names or a group object name).

- `object-type`: specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `on-error`: Specifies whether to specify synchronization errors as warnings or error. Available options for on-error:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

- `report-errors-to`: Specifies location of error report for the synchronization operation (optional attribute)

  If only folder path is given, then file by name `TargetSynchronizationReport.xml` is created.

#### Syntax example

```xml
<synchronize-target
   object-name="<object-name>"
   on-error="<report-total-as-warning/
               report-each-as-warning/
               fail-script>"   (optional)
   report-errors-to="<file-name/folder-name>"   (optional)
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

Refreshes the source objects from database.

If this command is executed against the target database, an error is generated.

#### Script

Requires one or several metabase nodes as command line parameter.

- `object-name`: Specifies the source objects considered for refreshing from source database (It can have individual object names or a group object name).

- `object-type`: Specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `on-error`: Specifies whether to specify refresh errors as warnings or error. Available options for on-error:

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
               fail-script>"   (optional)
   report-errors-to="<file-name/folder-name>"   (optional)
/>
```

Or:

```xml
<refresh-from-database
   object-name="<object-name>"
   object-type="<object-category>"/>
```

Or:

```xml
<refresh-from-database>
   <metabase-object object-name="<object-name>"/>
</refresh-from-database>
```

## Script generation script file commands

The Script Generation commands perform dual tasks: They help save the console output in a script file; and record the T-SQL output to the console or a file based on the parameter you specify.

### `save-as-script` command

Used to save the scripts of the objects to a file mentioned when `metabase=target`. This is an alternative to the synchronization command, in which we get the scripts and execute the same on the target database.

#### Script

Requires one or several metabase nodes as command line parameter.

- `object-name`: Specifies the objects whose scripts are to be saved. (It can have individual object names or a group object name)

- `object-type`: specifies the type of the object specified in the object-name attribute (if object category is specified then object type is `category`).

- `metabase`: Specifies whether it's the source or target metabase.

- `destination`: Specifies the path or the folder where the script has to be saved, if the file name isn't given then a file name in the format (object_name   attribute value).out

- `overwrite`: if `true` then it overwrites if the same filename exists. It can have the values (true/false).

#### Syntax example

```xml
<save-as-script
   metabase="<source/target>"
   object-name="<object-name>"
   object-type="<object-category>"
   destination="<file/folder>"
   overwrite="<true/false>"   (optional)
/>
```

Or:

```xml
<save-as-script
   metabase="<source/target>"
   destination="<file/folder>"
      <metabase-object object-name="<object-name>"
         object-type="<object-category>"/>
</save-as-script>
```

### `convert-sql-statement` command

- `context` specifies the schema name.

- `destination` specifies whether the output should be stored in a file.

  If this attribute isn't specified, then the converted T-SQL statement is displayed on the console. (optional attribute)

- `conversion-report-folder` specifies folder where the assessment report can be stored. (optional attribute)

- `conversion-report-overwrite` specifies whether to overwrite the assessment report folder if it already exists.

  **Default value:** false. (optional attribute)

- `write-converted-sql-to` specifies the file (or) folder path where the converted T-SQL is to be stored. When a folder path is specified along with the `sql-files` attribute, each source file has a corresponding target T-SQL file created under the specified folder. When a folder path is specified along with the `sql` attribute, the converted T-SQL is written to a file named `Result.out` under the specified folder.

- `sql` specifies the Db2 SQL statements to be converted, one or more statements can be separated using a ";"

- `sql-files` specifies the path of the SQL files that has to be converted to T-SQL code.

- `write-summary-report-to` specifies the path where the report is generated. If only the folder path is mentioned, then file by name `ConvertSQLReport.xml` is created. (optional attribute)

  Report creation has two further subcategories:

  - `report-errors`: `true` or `false`, with default as `false` (optional attributes)
  - `verbose`: `true` or `false`, with default as `false` (optional attributes)

#### Script

Requires one or several metabase nodes as command line parameter.

#### Syntax example

```xml
<convert-sql-statement
   context="<schema-name>"
   conversion-report-folder="<folder-name>"
   conversion-report-overwrite="<true/false>"
   write-summary-report-to="<file-name/folder-name>"   (optional)
   verbose="<true/false>"   (optional)
   report-errors="<true/false>"   (optional)
   destination="<stdout/file>"   (optional)
   file-name="<file-name>"
   sql="SELECT 1 FROM DUAL;">
   <output-window suppress-messages="<true/false>" />
</convert-sql-statement>
```

Or:

```xml
<convert-sql-statement
   context="<schema-name>"
   conversion-report-folder="<folder-name>"
   conversion-report-overwrite="<true/false>"
   write-summary-report-to="<file-name/folder-name>" (optional)
   verbose="<true/false>" (optional)
   report-errors="<true/false>"
   destination="<stdout/file>"   (optional)
   write-converted-sql-to="<file-name/folder-name>"
   sql-files="<folder-name>\*.sql" />
```

Or:

```xml
<convert-sql-statement
   context="<schema-name>"
   conversion-report-folder="<folder-name>"
   conversion-report-overwrite="<true/false>"
   sql-files="<folder-name>\*.sql" />
```

## Execute SSMA console in parallel

The SSMA console utility can be executed in parallel through scripting, by specifying the database name and corresponding folder path as input parameters. In the following example, the databases `SAMPLE1`, `SAMPLE2`, and `SAMPLE3`, with their respective folder paths, are supplied as input to the script.

```txt
SAMPLE1,C:\folder path\SSMA Project1
SAMPLE2,C:\folder path\SSMA Project2
SAMPLE3,C:\folder path\SSMA Project3
```

The following sample PowerShell script enables parallel execution of the SSMA console.

```powershell
$baseFolder = "C:\folder path\folder1"
$ssmaExe = "C:\folder path\SSMAforDb2Console.exe"
$databaselistPath = Join-Path $baseFolder "Databaselist.txt"
$conversionXmlTemplate = Join-Path $baseFolder "ConversionAndDataMigrationSample.xml"
$variableXmlTemplate = Join-Path $baseFolder "VariableValueFileSample.xml"

# Read all entries
$entries = Get-Content $databaselistPath | Where-Object { $_.Trim() -ne "" }

# Prepare the entries
$preparedEntries = foreach ($entry in $entries) {
    $parts = $entry -split ","
    $dbName = $parts[0].Trim()
    $workingFolder = $parts[1].Trim()
    if ($dbNameCounts.ContainsKey($dbName)) {
        $dbNameCounts[$dbName]++
        $suffix = "_{0:D2}" -f $dbNameCounts[$dbName]
        $fileDbName = "$dbName$suffix"
    }
    else {
        $dbNameCounts[$dbName] = 0
        $fileDbName = $dbName
    }
    [PSCustomObject]@{
        DbName        = $dbName
        WorkingFolder = $workingFolder
        FileDbName    = $fileDbName
    }
}

# Run in parallel
$preparedEntries | ForEach-Object -Parallel {
    $dbName = $_.DbName
    $workingFolder = $_.WorkingFolder
    $fileDbName = $_.FileDbName

    # Update ConversionAndDataMigrationSample.xml
    $convTree = [xml](Get-Content $using:conversionXmlTemplate)
    $convTree.SelectNodes("//initial-catalog") | ForEach-Object { $_.SetAttribute("value", $dbName) }
    $conversionXmlPath = Join-Path $using:baseFolder "ConversionAndDataMigrationSample_$fileDbName.xml"
    $convTree.Save($conversionXmlPath)

    # Update VariableValueFileSample.xml
    $varTree = [xml](Get-Content $using:variableXmlTemplate)
    $nodes = $varTree.SelectNodes('//variable[@name="$WorkingFolder$"]')
    if ($nodes.Count -eq 0) {
        Write-Host "No variable node found for `$WorkingFolder$"
    }
    else {
        $nodes | ForEach-Object { $_.value = $workingFolder }
    }
    $nodes2 = $varTree.SelectNodes('//variable[@name="$Db2InitialCatalog$"]')
    if ($nodes2.Count -eq 0) {
        Write-Host "No variable node found for `$Db2InitialCatalog$"
    }
    else {
        $nodes2 | ForEach-Object { $_.value = $dbName }
    }
    $variableXmlPath = Join-Path $using:baseFolder "VariableValueFileSample_$fileDbName.xml"
    $varTree.Save($variableXmlPath)

    # Prepare output/error file paths
    $outputFile = Join-Path $using:baseFolder "ssma_output_$fileDbName.txt"
    $errorFile = Join-Path $using:baseFolder "ssma_error_$fileDbName.txt"

    # Prepare argument list
    $arguments = "-s `"$conversionXmlPath`" -v `"$variableXmlPath`""

    # Run SSMA console
    Start-Process -FilePath $using:ssmaExe -ArgumentList $arguments -RedirectStandardOutput $outputFile -RedirectStandardError $errorFile -Wait
    Write-Host "Executed command: `"$using:ssmaExe`" $arguments"
}
```

## Related content

- [Command line options in SSMA console](command-line-options-in-ssma-console-db2tosql.md)
- [Work with sample console script files](working-with-the-sample-console-script-files-db2tosql.md)
- [Manage passwords](managing-passwords-db2tosql.md)
- [Generate reports](generating-reports-db2tosql.md)
- [Troubleshoot](troubleshooting-db2tosql.md)
