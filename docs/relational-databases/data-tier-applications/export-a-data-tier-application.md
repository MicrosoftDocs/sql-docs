---
title: "Export a Data-tier Application"
description: "Export a Data-tier Application."
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf
ms.date: 04/25/2024
ms.service: sql
ms.topic: concept-article
f1_keywords:
  - "sql13.swb.exportdac.progress.f1"
  - "sql13.swb.exportdac.summary.f1"
  - "sql13.swb.exportdac.results.f1"
  - "sql13.swb. exportdac.results.f1"
  - "sql13.swb. exportdac.summary.f1"
  - "sql13.swb. exportdac.settings.f1"
  - "sql13.swb.exportdac.welcome.f1"
  - "sql13.swb.exportdac.settings.f1"
helpviewer_keywords:
  - "How to [DAC], export"
  - "wizard [DAC], export"
  - "export DAC"
  - "data-tier application [SQL Server], export"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Export a Data-tier Application

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Exporting a deployed data-tier application (DAC) or database creates an export file that includes the definitions of the objects in the database and all of the data in the tables. The export file can then be imported to another instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)], or to [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]. The export-import operations can be combined to migrate a DAC between instances, to create an archive, or to create an on-premises copy of a database deployed in [!INCLUDE [ssSDS](../../includes/sssds-md.md)].

## Prerequisites

The export process builds a DAC export file in two stages.

1. The export builds a DAC definition in the export file - BACPAC file - the same way a DAC extract builds a DAC definition in a DAC package file. The exported DAC definition includes all of the objects in the current database. Suppose the export process is run against a database initially deployed from a DAC, and changes were made directly to the database after deployment. In that case, the exported definition matches the object set in the database, not what was defined in the original DAC.

1. The export bulk copies out the data from all of the tables in the database and incorporates the data into the export file.

The export process sets the DAC version to 1.0.0.0 and the DAC description in the export file to an empty string. If the database was deployed from a DAC, the DAC definition in the export file contains the name given to the original DAC. Otherwise, the DAC name is set to the database name.

### <a id="Permissions"></a> Permissions

To export a DAC, you need to possess at least ALTER ANY sign-in and database-level VIEW DEFINITION permissions and SELECT permissions on sys.sql_expression_dependencies`. This task is achievable for individuals holding membership in the securityadmin fixed server role and the database_owner fixed database role within the source database of the DAC. Furthermore, exporting a DAC is feasible for those who are part of the sysadmin fixed server role or those with access to the built-in SQL Server system administrator account named sa.

On Azure SQL Database, you must grant **for each database** VIEW DEFINITION and SELECT permission on all tables or specific tables.

## <a id="UsingDeployDACWizard"></a> Use the Export Data-tier Application Wizard

**To Export a DAC Using a Wizard**

1. Connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], whether on-premises or in [!INCLUDE [ssSDS](../../includes/sssds-md.md)].

1. In **Object Explorer**, expand the node for the instance from which you want to export the DAC.

1. Right-click the database name.

1. Select **Tasks** and then select **Export Data-tier Application...**

1. Complete the wizard dialogs:

- [Introduction Page](#Introduction)
- [Export Settings Page](#Export_settings)
- [Summary Page](#Summary)
- [Progress Page](#Progress)
- [Results Page](#Results)

### <a id="Introduction"></a> Introduction page

This page describes the steps for the Export Data-tier Application Wizard.

**Options**

**Do not show this page again.** - Select the check box to stop the Introduction page from being displayed in the future.

**Next** - Proceeds to the **Select DAC Package** page.

**Cancel** - Cancels the operation and closes the Wizard.

:::image type="content" source="export-a-data-tier-application/introduction.png" alt-text="Screensheet of the export a data-tier application introduction page." lightbox="export-a-data-tier-application/introduction.png":::

### <a id="Export_settings"></a> Export Settings page

Use this page to specify the location where you want the BACPAC file to be created.

- **Save to local disk** - Creates a BACPAC file in a directory on the local computer. Select **Browse...** to navigate to the local computer, or specify the path in the space provided. The path name must include a file name and the .bacpac extension.

- **Save to Azure** - Creates a BACPAC file in an Azure container. You must connect to an Azure container to validate this option. This option also requires that you specify a local directory for the temporary file. The temporary file will be created at the specified location and remain there after the operation.

To specify a subset of tables to export, use the **Advanced** option.

:::image type="content" source="export-a-data-tier-application/export-settings-page.png" alt-text="Screenshot of the export a data-tier application export settings page." lightbox="export-a-data-tier-application/export-settings-page.png":::

### <a id="Summary"></a> Summary page

Use this page to review the operation's specified source and target settings. To complete the export operation using the specified settings, select **Finish**. To cancel the export operation and exit the Wizard, select **Cancel**.

:::image type="content" source="export-a-data-tier-application/summary-page.png" alt-text="Screenshot of the export data- tier summary page." lightbox="export-a-data-tier-application/summary-page.png":::

### <a id="Progress"></a> Progress page

This page displays a progress bar that indicates the status of the operation. To view detailed status, select the **View details** option.

### <a id="Results"></a> Results page

This page reports the export operation's success or failure, showing each action's results. Any action that encountered an error has a link in the **Result** column. Select the link to view a report of the error for that action.

:::image type="content" source="export-a-data-tier-application/results-page.png" alt-text="Screenshot of the data-tier application results page." lightbox="export-a-data-tier-application/results-page.png":::

Select **Finish** to close the Wizard.

## <a id="NetApp"></a> Use a .NET Framework Application

**To export a DAC using the Export() method in a .Net Framework application.**

1. Create an SMO Server object and set it to the instance that contains the DAC to be exported.

1. Open a **ServerConnection** object and connect to the same instance.

1. Use the **Export** method of the **Microsoft.SqlServer.Management.Dac.DacStore** type to export the DAC. Specify the name of the DAC to be exported and the path to the folder where the export file is to be placed.

## <a id="LimitationsRestrictions"></a> Limitations and restrictions

A DAC or database can only be exported from a database in [!INCLUDE [ssSDS](../../includes/sssds-md.md)], or [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 4 (SP4) or later.

You can't export a database with objects that aren't supported in a DAC or contain users. For more information about the types of objects supported in a DAC, see [DAC Support For SQL Server Objects and Versions](/previous-versions/sql/sql-server-2012/ee210549(v=sql.110)).

If you receive a *failing with Out of Disk space* message, it's advisable to configure the % TEMP % folder of the system to reside on a distinct data disk. By doing so, you can ensure sufficient space for the export process to execute smoothly, avoiding potential disk space complications.

To configure the system's [%TEMP%](../../tools/sqlpackage/sqlpackage.md#temporary-files) folder:

- In Windows, open the **System Properties** > **Properties**, then select the link labeled **Advanced system settings**.

- In the ensuing System Properties window, navigate to the bottom and select **Environment Variables**.

- Under the **System variables** section, locate the TEMP and TMP variables, then select **Edit** associated with each.

- Modify the values of both variables to point to a pathway on the separate data disk you have established. For instance, if your data disk is designated as `D:`, set the values as `D:\Temp`.

- Confirm the changes by selecting OK and closing all open windows.


## Related content

- [Data-tier applications (DAC)](data-tier-applications.md)
- [Extract a DAC From a Database](extract-a-dac-from-a-database.md)
