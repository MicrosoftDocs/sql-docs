---
title: "Work with Sample Console Scripts in the SSMA Console"
description: "Work with sample scripts in the SSMA Console (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: sample
ms.collection:
  - sql-migration-content
---
# Work with sample scripts in the SSMA Console (AccessToSQL)

SQL Server Migration Assistant (SSMA) includes sample files for user reference and use. This section describes how to customize these scripts to fit your needs.

## Sample console script files

The following sample console script files cover different scenarios:

- `ServersConnectionFileSample.xml`:

  - This sample provides the different modes of connection available to the source and target database. Select any mode as per your requirement. This sample contains the server definitions.

    - Connect to the required database by changing the values to the required source and target server definitions. In the example, all values are variable values that you find in `VariableValueFileSample.xml`. Remove all other connection parameters from your working server connection file.

  - For more information on connecting to the source and target server, see [Create the server connection files](creating-the-server-connection-files-accesstosql.md).

- `VariableValueFileSample.xml`: This file collates all variables that are used in the sample console script files and `ServersConnectionFileSample.xml`. To execute the sample console scripts, replace the sample variable values with user defined ones and pass this file as an additional command line argument along with the script file.

  For more information on Variable Value File, see [Create variable value files](creating-variable-value-files-accesstosql.md).

- `AssessmentReportGenerationSample.xml`: Use this sample to generate an XML assessment report for analysis before you begin to convert and migrate data.

  In the `generate-assessment-report` command, change the variable value (refer `VariableValueFileSample.xml`) in the `object-name` attribute to the database name you're using. Depending on the kind of object specified, you must also change the `object-type` value.

  If you need to assess multiple objects or databases, specify multiple `metabase-object` nodes as illustrated in the `generate-assessment-report` command's Example 4 of the sample console script file.

  For more information on generating reports, see [Generate reports](generating-reports-accesstosql.md).

  - Pass the variable value file command line argument to the console application and update `VariableValueFileSample.xml` with the user specified values.

  - Pass the server connection file command line argument to the console application and update the `ServersConnectionFileSample.xml` with correct server parameter values.

- `ConversionAndDataMigrationSample.xml`: Use this sample to perform an end to end migration from conversion to data migration. Change the following mandatory attribute values:

  | Command name | Description | Attribute |
  | --- | --- | --- |
  | `map-schema` | Schema mapping of source database to the target schema. | `source-schema`: Specifies the source database that you want to convert.<br /><br />`sql-server-schema`: Specifies the target database that you want to migrate to |
  | `convert-schema` | Converts the schema from source to the target schema.<br /><br />If you need to assess multiple objects or databases, specify multiple `metabase-object` nodes as illustrated in the `convert-schema` command's Example 4 of the sample console script file. | `object-name`: Specify the source database or object name that you want to convert. Ensure that you change the corresponding `object-type` based on the type of object that you specify in the `object-name` |
  | `synchronize-target` | Synchronizes the target objects with the target database.<br /><br />If you need to assess multiple objects or databases, specify multiple `metabase-object` nodes as illustrated in the `synchronize-target` command's Example 3 of the sample console script file. | `object-name`: Specify the SQL Server database or object name that you want to create. Ensure that you change the corresponding `object-type` based on the type of object that you specify in the `object-name` |
  | `migrate-data` | Migrates the source data to the target.<br /><br />If you need to assess multiple objects or databases, specify multiple `metabase-object` nodes as illustrated in the `migrate-data` command's Example 2 of the sample console script file. | `object-name`: Specifies the source database or tables name that you want to migrate. Ensure that you change the corresponding `object-type` based on the type of object that you specify in the `object-name` |

## Related content

- [Create variable value files](creating-variable-value-files-accesstosql.md)
- [Create the server connection files](creating-the-server-connection-files-accesstosql.md)
- [Generate reports](generating-reports-accesstosql.md)
