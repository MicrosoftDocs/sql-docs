---
title: "PowerShell Cmdlet for Migration Evaluation"
description: Learn about Save-SqlMigrationReport, which evaluates the migration fitness of objects in a SQL Server database for In-Memory OLTP.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/30/2019"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: 117250d3-9982-47fe-94fd-6f29f6159940
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# PowerShell Cmdlet for Migration Evaluation

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The `Save-SqlMigrationReport` cmdlet is a tool that evaluates the migration fitness of multiple objects in a SQL Server database.

Currently, this cmdlet is limited to evaluating the migration fitness for In-Memory OLTP. The cmdlet can run in both an elevated Windows PowerShell environment and sqlps.

As an alternative to running this PowerShell cmdlet directly, you can run the cmdlet implicitly by using SQL Server Management Studio (SSMS). In the SSMS **Object Explorer**, you can right-click a table, and then click **Memory Optimization Advisor**.

## Syntax

```
Save-SqlMigrationReport
    -FolderPath <output_path>
    [ -MigrationType <migration_scenario_type> ]
    [
        [ -Server <server_name> -Database <database_name>
            [ -Schema <schema_name> ] [ -Object <object_name> ]
        ]
       |
        [ -InputObject <smo_object> ]
    ]
;
```

## Parameters

The following table describes the parameters.

There are syntax aspects that should be emphasized. If you specify the parameter `-InputObject`, then you cannot specify any of the following parameters:

- `-Server`
- `-Database`
- `-Schema`
- `-Object`

Conversely, if you do _not_ specify `-InputObject`, then you must specify `-Server` and `-Database`. If you specify `-Server`, you have the option to narrow the scope by specifying either `-Schema` or `-Object`, or both.

| Parameter name | Description |
| :------------- | :---------- |
| Database | The name of the target SQL Server database. Mandatory when `-Server` is mandatory.<br/><br/> Optional in SQLPS. |
| FolderPath | The folder under which the cmdlet should store the generated reports.<br/><br/> Required. |
| InputObject | The SMO object the cmdlet should target.<br/><br/> Mandatory in Windows Powershell environment if `-Server` is not supplied.<br/><br/> Optional in SQLPS. |
| MigrationType | The type of migration scenario the cmdlet is targeting. Currently the only value is the default **'OLTP'**.<br/><br/> Optional. |
| Object | The name of the object to report about. Can be a table or stored procedure. |
| Password | Required when `-Username` is required. |
| Schema | The name of the schema that owns the object to be reported about.<br/><br/> Optional. |
| Server | The name of the target SQL Server instance. Mandatory in Windows Powershell environment if `-InputObject` parameter is not supplied.<br/><br/> Optional in SQLPS. |
| Username | Required when connecting through SQL Server Authentication, as opposed to Windows Authentication. Otherwise omit. |

## Prerequisites

Before you can run this cmdlet, you must first install the module named **SqlServer**:

- `Install-Module -Name SqlServer`

> [!NOTE]
> The old `SQLPS` module is no longer being maintained. Use the newer `SqlServer` module.

For more information, see [Install SQL Server PowerShell module](../../powershell/download-sql-server-ps-module.md).

## Example cmdlet line

Next is the actual cmdlet line that ran to generate the report that is displayed later in this article.

```powershell
Save-SqlMigrationReport `
  -FolderPath 'C:\Test\PowerShell-ps1\Save-SqlMigrationReport\' `
  -Server 'MyUserName123456.database.windows.net' `
  -Database 'MyDatabaseName_31' `
  -Schema 'dbo' `
  -Object 'Table2' `
  -Username 'MyUserName' `
  -Password 'MyPassword' `
  -MigrationType 'OLTP' `
;
```

## Example output report

Under the folder specified for the `-FolderPath` parameter, the following two folder paths are created by running this cmdlet. Both paths start with the _server\_name_ value:

- MyDatabaseName_31\Tables\
- MyDatabaseName_31\Stored Procedures\

Each object report file is stored under the appropriate folder.

The report file names have the extension **.html**. For instance, an actual generated file name was **MigrationAdvisorChecklistReport_Table2_20190728.html**.

The HTML is mostly a two-column table with the following headers:

- Description
- Validation Result

Next is an actual example of the HTML report for one table.

```html
<?xml version="1.0" encoding="utf-8"?>
<html>
  <head>
    <title>Memory optimization checklist for [MyDatabaseName_31].[Table2]</title>
  </head>
  <body>
    <p STYLE="font-family: Verdana, Arial, sans-serif; font-size: 14pt;">
      <b>Memory optimization checklist for [MyDatabaseName_31].[Table2]</b>
    </p>
    <p STYLE="font-family: Verdana, Arial, sans-serif; font-size: 10pt;">
      <b>Report Date/Time:</b>7/28/2019 2:25 PM<br /></p>
    <table border="1" cellpadding="5" cellspacing="0" STYLE="font-family: Verdana, Arial, sans-serif; font-size: 9pt;">
      <tr style="background-color:Silver">
        <th colspan="2" align="center">Description</th>
        <th align="center">Validation Result</th>
      </tr>
      <tr valign="top">
        <td colspan="2">No unsupported data types are defined on this table. </td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top" style="background-color:LightYellow">
        <td colspan="2">No sparse columns are defined for this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top">
        <td colspan="2">No identity columns with unsupported seed and increment are defined for this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top" style="background-color:LightYellow">
        <td colspan="2">No foreign key relationships are defined on this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top">
        <td colspan="2">No unsupported constraints are defined on this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top" style="background-color:LightYellow">
        <td colspan="2">No unsupported indexes are defined on this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top">
        <td colspan="2">No unsupported triggers are defined on this table.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top" style="background-color:LightYellow">
        <td colspan="2">Post migration row size does not exceed the row size limit of memory-optimized tables.</td>
        <td>Succeeded</td>
      </tr>
      <tr valign="top">
        <td colspan="2">Table is not partitioned or replicated.</td>
        <td>Succeeded</td>
      </tr>
    </table>
  </body>
</html>
```

And next is an approximation of what the table looks like.

| Description | Validation Result |
| :---------- | :---------------- |
| No unsupported data types are defined on this table. | Succeeded |
| No sparse columns are defined for this table. | Succeeded |
| No identity columns with unsupported seed and increment are defined for this table. | Succeeded |
| No foreign key relationships are defined on this table. | Succeeded |
| No unsupported constraints are defined on this table. | Succeeded |
| No unsupported indexes are defined on this table. | Succeeded |
| No unsupported triggers are defined on this table. | Succeeded |
| Post migration row size does not exceed the row size limit of memory-optimized tables. | Succeeded |
| Table is not partitioned or replicated. | Succeeded |

## Related links

- Reference documentation: [Save-SqlMigrationReport](/powershell/module/sqlserver/save-sqlmigrationreport)
