---
title: "Generate reports (Db2ToSQL)"
description: Learn how to generate reports in SQL Server Migration Assistant for Db2.
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
# Generate reports (Db2ToSQL)

The reports of certain activities performed using commands are generated in SQL Server Migration Assistant (SSMA) Console at object tree level.

## Generate a report

Use the following procedure to generate reports:

1. Specify the `write-summary-report-to` parameter. The related report is stored as the file name (if specified) or in the folder you specify. The file name is system-predefined as mentioned in the following table, where `<n>` is the unique file number that increments with a digit with each execution of the same command.

   The reports are related to the commands as follows:

   | Slot number | Command | Report title |
   | --- | --- | --- |
   | 1 | `generate-assessment-report` | `AssessmentReport<n>.xml` |
   | 2 | `convert-schema` | `SchemaConversionReport<n>.xml` |
   | 3 | `migrate-data` | `DataMigrationReport<n>.xml` |
   | 4 | `convert-sql-statement` | `ConvertSQLReport<n>.xml` |
   | 5 | `synchronize-target` | `TargetSynchronizationReport<n>.xml` |
   | 6 | `refresh-from-database` | `SourceDBRefreshReport<n>.xml` |

   > [!IMPORTANT]  
   > An output report is different from Assessment Report. The former is a report on the performance of an executed command, while the latter is an XML report for programmatic consumption.

   For the command options for output reports (from Slot number 2 - 4 in the previous table), refer to the [Execute the SSMA console](executing-the-ssma-console-db2tosql.md) section.

1. Indicate the extent of detail you desire in the output report using the Report Verbosity settings:

   | Slot number | Command and parameter | Output description |
   | --- | --- | --- |
   | 1 | `verbose="false"` | Generates a summarized report of the activity. |
   | 2 | `verbose="true"` | Generates a summarized and detailed status report for each activity. |

   > [!NOTE]  
   > Report Verbosity settings apply to the `generate-assessment-report`, `convert-schema`, `migrate-data`, and `convert-sql-statement` commands.

1. Indicate the extent of detail you desire in the error reports using the Error Reporting settings:

   | Slot number | Command and parameter | Output description |
   | --- | --- | --- |
   | 1 | `report-errors="false"` | No details on error, warning, or info messages. |
   | 2 | `report-errors="true"` | Detailed error, warning, or info messages. |

   > [!NOTE]  
   > Error Reporting settings apply to the `generate-assessment-report`, `convert-schema`, `migrate-data`, and `convert-sql-statement` commands.

### Example

```xml
<generate-assessment-report
   object-name="<object-name>"
   object-type="<object-type>"
   verbose="<true/false>"
   report-erors="<true/false>"
   write-summary-report-to="<file-name/folder-name>"
   assessment-report-folder="<folder-name>"
   assessment-report-overwrite="<true/false>"/>
```

## Report commands

#### synchronize-target

The command `synchronize-target` has `report-errors-to` parameter, which specifies the location of error report for the synchronization operation. Then, a file by name `TargetSynchronizationReport<n>.xml` is created at the specified location, where `<n>` is the unique file number that increments with a digit with each execution of the same command.

> [!NOTE]  
> If the folder path is given, then `report-errors-to` parameter becomes an optional attribute for the command `synchronize-target`.

The following example synchronizes the entire target database with all attributes.

```xml
<synchronize-target
   object-name="<object-name>"
   on-error="report-total-as-warning/report-each-as-warning/fail-script"
   report-errors-to="<file-name/folder-name>"/>
```

- `object-name` specifies the objects considered for synchronization. It can also have individual object names or a group object name.

- `on-error` specifies whether to specify synchronization errors as warnings or error. Available options for `on-error`:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

#### refresh-from-database

The command `refresh-from-database` has the `report-errors-to` parameter, which specifies the location of error report for the refresh operation. Then, a file by name `SourceDBRefreshReport<n>.xml` is created at the specified location, where `<n>` is the unique file number that increments with a digit with each execution of the same command.

If the folder path is given, then `report-errors-to` parameter becomes an optional attribute for the command `synchronize-target`.

The following example refreshes the entire schema with all attributes.

```xml
<refresh-from-database
   object-name="<object-name>"
   object-type ="<object-type>"
   on-error="report-total-as-warning/report-each-as-warning/fail-script"
   report-errors-to="<file-name/folder-name>"/>
```

- `object-name` specifies the objects considered for refresh. It can also have individual object names or a group object name.

- `on-error` specifies whether to specify refresh errors as warnings or error. Available options for `on-error`:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

## Related content

- [Execute the SSMA console (Db2ToSQL)](executing-the-ssma-console-db2tosql.md)
