---
title: "Generating Reports (AccessToSQL)"
description: "Generating Reports (AccessToSQL)"
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
# Generate reports (AccessToSQL)

The reports of certain activities performed using commands are generated in SSMA Console at object tree level.

## Generate reports

Use the following procedure to generate reports:

1. Specify the `write-summary-report-to` parameter. The related report is stored as the file name (if specified) or in the folder you specify. The file name is system-predefined as mentioned in the following table where, `<n>` is the unique file number that increments with a digit with each execution of the same command.

   The reports relate to the commands as follows:

   | Slot number | Command | Report title |
   | --- | --- | --- |
   | 1 | `generate-assessment-report` | `AssessmentReport<n>.xml` |
   | 2 | `convert-schema` | `SchemaConversionReport<n>.xml` |
   | 3 | `migrate-data` | `DataMigrationReport<n>.xml` |
   | 4 | `synchronize-target` | `TargetSynchronizationReport<n>.xml` |
   | 5 | `refresh-from-database` | `SourceDBRefreshReport<n>.xml` |

   > [!IMPORTANT]  
   > An output report is different from Assessment Report. The former is a report on the performance of an executed command while, the latter is an XML report for programmatic consumption.

   For the command options for output reports (from Slot number 2-4 previously), refer to the [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md) section.

1. Indicate the extent of detail you desire in the output report using the Report Verbosity settings:

   | Slot number | Command and parameter | Output Description |
   | --- | --- | --- |
   | 1 | `verbose="false"` | Generates a summarized report of the activity. |
   | 2 | `verbose="true"` | Generates a summarized and detailed status report for each activity. |

   > [!NOTE]  
   > The Report Verbosity Settings specified previously are applicable for generate-assessment-report, convert-schema, migrate-data commands.

1. Indicate the extent of detail you desire in the error reports using the Error Reporting settings:

   | Slot number | Command and parameter | Output Description |
   | --- | --- | --- |
   | 1 | `report-errors="false"` | No details on error/ warning/ info messages. |
   | 2 | `report-errors="true"` | Detailed error/ warning/ info messages. |

   > [!NOTE]  
   > The Error Reporting Settings specified previously are applicable for generate-assessment-report, convert-schema, migrate-data commands.

### Example

```xml
<generate-assessment-report
    object-name="testschema"
    object-type="Schemas"
    verbose="yes"
    report-errors="yes"
    write-summary-report-to="$AssessmentFolder$\Report1.xml"
    assessment-report-folder="$AssessmentFolder$\assessment_report"
    assessment-report-overwrite="true"
/>
```

### synchronize-target

The command `synchronize-target` has `report-errors-to` parameter, which specifies the location of error report for the synchronization operation. Then, a file by name `TargetSynchronizationReport<n>.xml` is created at the specified location, where `<n>` is the unique file number that increments with a digit with each execution of the same command.

If the folder path is given, then `report-errors-to` parameter becomes an optional attribute for the command `synchronize-target`.

The following example synchronizes the entire database with all attributes:

```xml
<synchronize-target
    object-name="$TargetDB$.dbo"
    on-error="fail-script"
    report-errors-to="$SynchronizationReports$"
/>
```

`object-name`: Specifies the objects considered for synchronization (It can also have individual object names or a group object name).

- `on-error`: Specifies whether to specify synchronization errors as warnings or error. Available options:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

### refresh-from-database

The command `refresh-from-database` has `report-errors-to` parameter, which specifies the location of error report for the refresh operation. Then, a file by name `SourceDBRefreshReport<n>.xml` is created at the specified location, where `<n>` is the unique file number that increments with a digit with each execution of the same command.

If the folder path is given, then `report-errors-to` parameter becomes an optional attribute for the command `synchronize-target`.

The following example refreshes the entire schema with all attributes:

```xml
<refresh-from-database
    object-name="$SourceDatabaseStandard$"
    object-type ="Databases"
    on-error="fail-script"
    report-errors-to="$RefreshDBFolder$\RefreshReport.xml"
/>
```

- `object-name`: Specifies the objects considered for refresh (It can also have individual object names or a group object name).

- `on-error`: Specifies whether to specify refresh errors as warnings or error. Available options:

  - `report-total-as-warning`
  - `report-each-as-warning`
  - `fail-script`

## Related content

- [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md)
