---
title: "Work with sample console script files (Db2ToSQL)"
description: Learn how to work with sample console script files for SQL Server Migration Assistant for Db2.
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
# Work with sample console script files (Db2ToSQL)

A few sample files are provided for user reference and usage. This section describes ways to easily customize these scripts to fit your requirements.

## Sample console script files

Refer to the following sample console script files covering different scenarios:

- [ServersConnectionFileSample.xml](#serversconnectionfilesamplexml)
- [VariableValueFileSample.xml](#variablevaluefilesamplexml)
- [AssessmentReportGenerationSample.xml](#assessmentreportgenerationsamplexml)
- [SqlStatementConversionSample.xml](#sqlstatementconversionsamplexml)
- [ConversionAndDataMigrationSample.xml](#conversionanddatamigrationsamplexml)

#### ServersConnectionFileSample.xml

This sample gives the different modes of connection available to the source and target database and you can select any mode as per the requirement. This sample contains the Server definitions.

You can connect to the required database by changing the values to the required source and target server definitions. In the example provided, all values are available in `VariableValueFileSample.xml`. All other connection parameters can be removed from your working server connection file.

For more information on connecting to the source and target server, see [Create the server connection files (Db2ToSQL)](creating-the-server-connection-files-db2tosql.md).

#### VariableValueFileSample.xml

All variables that are used in the sample console script files and `ServersConnectionFileSample.xml` are collated in this file. To execute the sample console scripts, you must replace the sample variable values with user defined ones, and pass this file as an additional command line argument along with the script file.

For more information on Variable Value File, see [Create variable value files (Db2ToSQL)](creating-variable-value-files-db2tosql.md).

#### AssessmentReportGenerationSample.xml

Use this sample to generate an XML assessment report, which you can use for analysis before you convert and migrate data.

In the `generate-assessment-report` command, change the variable value (refer to [VariableValueFileSample.xml](#variablevaluefilesamplexml)) in the `object-name` attribute to the database name you specify. Depending on the kind of object specified, the `object-type` value must also change.

If you need to assess multiple objects / databases, you can specify multiple `metabase-object` nodes. For more information, see the `generate-assessment-report` command in Example 4 of the sample console script file.

Ensure that the variable value file command line argument is passed to the console application and `VariableValueFileSample.xml` is updated with your specified values.

Ensure that server connection file command line argument is passed to the console application and the `ServersConnectionFileSample.xml` is updated with correct server parameter values.

For more information on generating reports, see [Generate reports (Db2ToSQL)](generating-reports-db2tosql.md).

#### SqlStatementConversionSample.xml

This sample enables you to generate the corresponding `t-sql` script for the source database `sql` command provided as input.

In the `convert-sql-statement` command, you must change the variable value (refer to [VariableValueFileSample.xml](#variablevaluefilesamplexml)) in the `context` attribute to the database name you specify. You must also change the `sql` attribute value to the source database `sql` command that needs to be converted.

You can also provide `sql` files to be converted. For more information, see the `convert-sql-statement` command in Example 4 of the sample console script file.

Ensure that the variable value file command line argument is passed to the console application and `VariableValueFileSample.xml` is updated with your specified values.

#### ConversionAndDataMigrationSample.xml

This sample enables you to perform an end to end migration from conversion to data migration. Following is a list of mandatory attribute values you need to change:

| Command | Description | Attribute |
| --- | --- | --- |
| `map-schema` | Schema mapping of source database to the target schema. | `source-schema`: Specifies the source database that requires to be converted.<br /><br />`sql-server-schema`: Specifies the target database that is to be migrated to |
| `convert-schema` | Performs schema conversion from source to the target schema.<br /><br />If you need to assess multiple objects / databases, you can specify multiple `metabase-object` nodes. For more information, see the `convert-schema` command in Example 4 of the sample console script file. | `object-name`: Specify the source database / object name that requires to be converted. Ensure that the corresponding `object-type` is changed based on the type of object that is specified in the `object-name` |
| `synchronize-target` | Synchronizes the target objects with the target database.<br /><br />If you need to assess multiple objects / databases, you can specify multiple `metabase-object` nodes. For more information, see the `synchronize-target` command in Example 4 of the sample console script file. | `object-name`: Specify the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database / object name that requires to be created. Ensure that the corresponding `object-type` is changed based on the type of object that is specified in the `object-name`. |
| `migrate-data` | Migrates the source data to the target.<br /><br />If you need to assess multiple objects / databases, you can specify multiple `metabase-object` nodes. For more information, see the `migrate-data` command in Example 4 of the sample console script file. | `object-name`: Specifies the source database / tables name that requires to be migrated. Ensure that the corresponding `object-type` is changed based on the type of object that is specified in the `object-name` |

## Related content

- [Create variable value files (Db2ToSQL)](creating-variable-value-files-db2tosql.md)
- [Create the server connection files (Db2ToSQL)](creating-the-server-connection-files-db2tosql.md)
- [Generate reports (Db2ToSQL)](generating-reports-db2tosql.md)
