---
title: "Upgrade Power Pivot for SharePoint"
description: "Upgrade Power Pivot for SharePoint"
author: "Minewiskan"
ms.author: "owend"
ms.reviewer: maghan
ms.date: 10/17/2022
ms.service: sql
ms.topic: conceptual
monikerRange: ">=sql-server-2016"
---

# Upgrade Power Pivot for SharePoint

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

  This article summarizes the steps required to upgrade a deployment of [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] to [!INCLUDE[ssGeminiLong](../../includes/ssgeminilong-md.md)]. The specific steps depend on the version of SharePoint your environment is currently running and include the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint Add-in (**spPowerPivot.msi**).

 **[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2010 | SharePoint 2013

 For release notes, see [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md).

 **In this article:**

 [Prerequisites](#bkmk_prereq)

 [Upgrade an Existing SharePoint 2013 Farm](#bkmk_uprgade_sharepoint2013)

 [Upgrade an Existing SharePoint 2010 Farm](#bkmk_uprgade_sharepoint2010)

 [Workbooks](#bkmk_workbooks)

 [Data Refresh](#bkmk_datarefresh)

 [Verify the Versions of Power Pivot Components and Services](#bkmk_verify_versions)

 [Upgrading Multiple Power Pivot for SharePoint Servers in a SharePoint Farm](#geminifarm)

 [Applying a QFE to a Power Pivot instance in the farm](#qfe)

 [Post-upgrade verification tasks](#verify)

## Background

- If you're upgrading a multi-server SharePoint 2010 farm that has two or more [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] instances, you must fully upgrade each server **before** continuing to the next server. A full upgrade includes running SQL Server Setup to upgrade [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] program files, followed by SharePoint upgrade actions that configure the upgraded services. Server availability will be limited until you run upgrade actions in the appropriate [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool or Windows PowerShell.

- All instances of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and Analysis Services in a SharePoint 2010 farm must be the same version. For information on how to verify the version, see the section [Verify the Versions of Power Pivot Components and Services](#bkmk_verify_versions) in this article.

- The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] configuration tools are one of the SQL Server shared features and all shared features upgrade at the same time. If during an upgrade process you select other SQL Server instances or features that require a shared feature upgrade, then the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] configuration tool will also be upgraded. You may have issues if the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] configuration tool is upgraded but your [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] instance isn't. For more information about SQL Server shared features, see [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md).

- The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint Add-in (**spPowerPivot.msi**) installs side by side with previous versions. For example, the add-in installs to the folder `c:\Program Files\Microsoft SQL Server\nnn\Tools\PowerPivotTools`. See [File locations](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md#shared-files-for-all-instances-of-) for information about SQL Server installation files.

## <a id="bkmk_prereq"></a> Prerequisites

 **Permissions**

- You must be a farm administrator to upgrade a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installation. You must be a local administrator to run SQL Server Setup.

- You must have **db_owner** permissions on the farm configuration database.

 **SQL Server:**

- If the existing [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] installation is [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Service Pack 2 (SP2) is required for an upgrade to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)].

- If the existing [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] installation is [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Service Pack 1 (SP1) is required for an upgrade to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)].

 **SharePoint 2010:**

- If the existing installation is running SharePoint 2010, install the SharePoint 2010 Service Pack 2 before upgrading to [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)][!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)]. For more information, see [Service Pack 2 for Microsoft SharePoint 2010](https://www.microsoft.com/download/details.aspx?id=39672). Use the PowerShell command `(Get-SPfarm).BuildVersion.ToString()` to verify the version. To reference the build version to release date, see [SharePoint 2010 Build Numbers](/officeupdates/sharepoint-updates#sharepoint-2010-update-history).

## <a id="bkmk_uprgade_sharepoint2013"></a> Upgrade an Existing SharePoint 2013 Farm

 To upgrade [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] deployed in SharePoint 2013, do the following:

 :::image type="content" source="media/upgrade-power-pivot-for-sharepoint/as-power-pivot-upgrade-flow-sharepoint-2013.png" alt-text="Powerpivot for sharepoint 2013 upgrade.":::

1. Run [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Setup on backend server(s) that run [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] in SharePoint mode. If the server hosts multiple instances of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], upgrade at least the **POWERPIVOT** instance. The following list is a summary of setup wizard steps related to a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] upgrade:

    1. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup Wizard, select **Installation**.

    1. Select **Upgrade from SQL Server.....**.

    1. On the **Select Instance** page, select the **POWERPIVOT** instance name and then select **Next**.

    1. For more information, see [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md)

1. Restart the server.

1. Run the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint add-in (**spPowerPivot.msi**) on each server in the SharePoint 2013 farm to install the data providers. The exception is servers where you ran the SQL Server setup wizard, which also upgrades data providers. For more information, see [Download Microsoft SQL Server 2014 Power Pivot for Microsoft SharePoint 2013](https://www.microsoft.com/download/details.aspx?id=42300) and  [Install or Uninstall the Power Pivot for SharePoint Add-in &#40;SharePoint 2013&#41;](/analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013).

1. **Run the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration** tool on one of the SharePoint application servers to configure the SharePoint farm with the updated solution files that the add-in installed. You can't use Central SharePoint Administration for this step. For more information, see the following:

    1. From the Windows Start page, type **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]** and in the search results, select **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration**. Note the search may return both versions of the configuration tool.

         :::image type="content" source="/analysis-services/analysis-services/instances/install-windows/media/as-powerpivot-configtools-bothicons.gif" alt-text="Two powerpivot configuration tools.":::

         Or

         On the **Start** menu, point to **All Programs**, select [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], select **Configuration Tools**, and then select **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2013 Configuration Too**. This tool is listed only when [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is installed on the local server.

    1. At startup, the configuration tool checks the upgrade status of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] farm solution and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] web application solutions. If older versions of these solutions are detected, you'll see the message "**Newer versions of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution files have been detected. Please select the upgrade option to upgrade your farm**." Select **OK** to close the system validation message.

    1. Select **Upgrade Features, Services, Applications and Solutions**, and thenselectk **OK**.

    1. Review the actions in the left pane task list and exclude any that you don't want the tool to perform. All actions are included by default. To remove an action, select it in the left task list, and then on the **Parameters** page, clear the **Include this action in the task list** checkbox.

    1. Optionally, review detailed information in the **Script** or **Output** tab.

         The Output tab is a summary of the actions that will be performed by the tool. This information is saved in log files at `C:\Program Files\Microsoft SQL Server\130\Tools\PowerPivotTools\SPAddinConfiguration\Log`.

         The Script tab shows the PowerShell cmdlets or references the PowerShell script files that the tool will run.

    1. Select **Validate** to check whether each action is valid. If **Validate** isn't available, it means that all of the actions are valid for your system. If **Validate** is available, you might have modified an input value (for example, the Excel service application name), or the tool might have determined that a particular action can't be performed. If an action can't be performed, you must exclude it or fix the underlying conditions that cause the action to be flagged as invalid.

        > [!IMPORTANT]  
        >  The first action, **Upgrade Farm Solution**, must always be processed first. It registers the PowerShell cmdlets that are used to configure the server. If you get an error on this action, do not continue. Instead, use the information provided by the error to diagnose and resolve the problem before processing additional actions in the task list.

    1. Select **Run** to perform all of the actions that are valid for this task. **Run** is available only after the validation check is passed. When youselectk **Run**, the following warning appears, reminding you that actions are processed in batch mode: "**All of the configuration settings that are flagged as valid in the tool will be applied to the SharePoint farm. Do you want to continue?**"

    1. Select **Yes** to continue.

    1. Upgrading solutions and features in the farm can take several minutes to complete. During this time, connection requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data **will fail** with errors similar to "**Unable to refresh data**" or "**An error has occurred trying to perform the requested action. Please try again**." After upgrade is finished, the server will become available, and these errors will no longer occur.

     For more information, see the following:

    -   [Power Pivot Configuration Tools](/analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools)

    -   [Configure or Repair Power Pivot for SharePoint 2013 &#40;Power Pivot Configuration Tool&#41;](/analysis-services/power-pivot-sharepoint/configure-or-repair-power-pivot-for-sharepoint-2013)

    -   [Power Pivot Configuration using Windows PowerShell](/analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell)

    -   [PowerShell Reference for Power Pivot for SharePoint](/analysis-services/powershell/powershell-reference-for-power-pivot-for-sharepoint)

1. Verify that upgrade succeeded by performing the post-upgrade steps and by checking the version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] servers in the farm. For more information, see [Post-upgrade verification tasks](#verify) in this article and the following section.

## <a id="bkmk_uprgade_sharepoint2010"></a> Upgrade an Existing SharePoint 2010 Farm

 To upgrade [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] deployed in SharePoint 2010, do the following:

 :::image type="content" source="media/upgrade-power-pivot-for-sharepoint/as-power-pivot-upgrade-flow-sharepoint-2010.png" alt-text="Powerpivot for Sharepoint 2010 upgrade.":::

1. Download [Service Pack 2 for Microsoft SharePoint 2010](https://www.microsoft.com/download/details.aspx?id=39672) and apply it on all servers in the farm. Verify that SharePoint SP2 installation succeeded. In Central Administration, on the Upgrade and Migration page, open the Check product and patch installation status page to view status messages related to SP2.

1. Verify that the SharePoint 2010 Administration Windows service is running.

    ```
    Get-Service | where {$_.displayname -like "*SharePoint*"}
    ```

1. Verify the **SharePoint** services **SQL Server Analysis Services** and **SQL Server [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service** are started in SharePoint Central Administration or use the following PowerShell command:

    ```
    get-SPserviceinstance | where {$_.typename -like "*sql*"}
    ```

1. Verify the **Windows** service **SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)])** is running.

    ```
    Get-Service | where {$_.displayname -like "*powerpivot*"}
    ```

1. **Run [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Setup** on the first SharePoint application server that runs the **SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)])** Windows service to upgrade the POWERPIVOT instance. On the Installation page of the SQL Server Setup Wizard, choose the upgrade option. For more information, see [Upgrade to SQL Server 2016 Using the Installation Wizard &#40;Setup&#41;](../../database-engine/install-windows/upgrade-sql-server-using-the-installation-wizard-setup.md).

1. **Restart the server** before running the configuration tool. This step ensures that any updates or prerequisites installed by SQL Server Setup are fully configured on the system.

1. **Run the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool** on the first SharePoint application server that runs SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) service to upgrade the solutions and Web services in SharePoint. You can't use Central Administration for this step.

    1. On the **Start** menu, point to **All Programs**, select [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], select **Configuration Tools**, and then select **[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool**. This tool is listed only when [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is installed on the local server.

    1. At startup, the configuration tool checks the upgrade status of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] farm solution and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] web application solutions. If older versions of these solutions are detected, you'll see the message "Newer versions of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution files have been detected. elect the upgrade option to upgrade your farm." Select **OK** to close the message.

    1. Select **Upgrade Features, Services, Applications and Solutions**, and thenselectk **OK** to continue.

    1. The following warning appears: "Workbooks in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard are about to be upgraded to the latest version. Any customizations you made to the existing workbooks will be lost. Do you want to continue?"

         This warning refers to workbooks in the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard that report on data refresh activity. If you customized these workbooks, any changes you made to those workbooks will be lost when existing files are replaced with newer versions.

         Select **Yes** to overwrite the workbooks with newer versions. Otherwise,select **No** to return to the home page. Save the workbooks to a different location so that you have a copy, and then return to this step when you're ready to continue.

         For more information about customizing workbooks used in the dashboard, see [Customizing the Power Pivot Management Dashboard](/previous-versions/sql/sql-server-2008-r2/ff718155(v=sql.105)).

    1. Review the actions in the task list and exclude any that you don't want the tool to perform. All actions are included by default. To remove an action, select it in the task list, and then clear the **Include this action in the task list** checkbox on the Parameters page.

    1. Optionally, review detailed information in the **Output** tab or **Script** tab.

         The Output tab is a summary of the actions that will be performed by the tool. This information is saved in log files at `c:\Program Files\Microsoft SQL Server\130\Tools\PowerPivotTools\ConfigurationTool\Log`.

         The Script tab shows the PowerShell cmdlets or references the PowerShell script files that the tool will run.

    1. Select **Validate** to check whether each action is valid. If **Validate** isn't available, it means that all of the actions are valid for your system. If **Validate** is available, you might have modified an input value (for example, the Excel service application name), or the tool might have determined that a particular action can't be performed. If an action can't be performed, you must exclude it or fix the underlying conditions that cause the action to be flagged as invalid.

        > [!IMPORTANT]  
        >  The first action, **Upgrade Farm Solution**, must always be processed first. It registers the PowerShell cmdlets that are used to configure the server. If you get an error on this action, do not continue. Instead, use the information provided by the error to diagnose and resolve the problem before processing additional actions in the task list.

    1. Select **Run** to perform all of the actions that are valid for this task. **Run** is available only after the validation check is passed. When youselectk **Run**, the following warning appears, reminding you that actions are processed in batch mode: "All of the configuration settings that are flagged as valid in the tool will be applied to the SharePoint farm. Do you want to continue?"

    1. Select **Yes** to continue.

    1. Upgrading solutions and features in the farm can take several minutes to complete. During this time, connection requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data will fail with errors like "Unable to refresh data" or "An error has occurred trying to perform the requested action. Try again." After upgrade is finished, the server will become available, and these errors will no longer occur.

1. **Repeat the process** for each SQL Server Analysis Services ([!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) service in the farm: 1) Run SQL Server setup 2) Run the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration tool.

1. Verify that upgrade succeeded by performing the post-upgrade steps and by checking the version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] servers in the farm. For more information, see [Post-upgrade verification tasks](#verify) in this article and the following section.

1. **Troubleshooting errors**

     You can view error information in the Parameters pane for each action.

     For problems related to solution deployment or retraction, verify the SharePoint 2010 Administrator service is started. This service runs the timer jobs that trigger configuration changes in a farm. If the service isn't running, solution deployment or retraction will fail. Persistent errors indicate that an existing deployment or retraction job is already in the queue and blocking further action from the configuration tool.

    1. Start the SharePoint 2010 Management Shell as an administrator and then run the following command to view jobs in the queue:

        ```
        Stsadm -o enumdeployments
        ```

    1. Review existing deployments for the following information: **Type** is Retraction or Deployment, **File** is powerpivotwebapp.wsp or powerpivotfarm.wsp.

    1. For deployments or retractions related to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solutions, copy the GUID value for **JobId** and then paste it into the following command (use the Mark, Copy, and Paste commands on the Shell's Edit menu to copy the GUID):

        ```
        Stsadm -o canceldeployment -id "<GUID>"
        ```

    1. Retry the task in the configuration tool by selecting **Validate** followed by **Run**.

     For all other errors, check the ULS logs. For more information, see [Configure and View SharePoint Log Files  and Diagnostic Logging &#40;Power Pivot for SharePoint&#41;](/analysis-services/power-pivot-sharepoint/configure-and-view-sharepoint-and-diagnostic-logging).

## <a id="bkmk_workbooks"></a> Workbooks

 Upgrading a server doesn't necessarily upgrade the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that run on it, but older workbooks created in the previous version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel will continue to work as before, using the features available in that release. Workbooks remain functional because an upgraded server has the version of the Analysis Services OLE DB provider that was part of the previous installation.

## <a id="bkmk_datarefresh"></a> Data Refresh

 Upgrade will impact data refresh operations. Scheduled data refresh on the server is available only for workbooks that match the server version. If you're hosting workbooks from the previous version, data refresh might no longer work for those workbooks. To re-enable data refresh, you must upgrade the workbooks. You can upgrade each workbook manually in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for Excel, or enable the auto-upgrade for data refresh feature in SharePoint 2010. Auto-upgrade will upgrade a workbook to the current version prior to running data refresh, allowing data refresh operations to remain on schedule.

## <a id="bkmk_verify_versions"></a> Verify the Versions of Power Pivot Components and Services

 All instances of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and Analysis Services must be the same version. To verify that all server components are at the same version, check version information for the following:

### Verify the version of Power Pivot Solutions and the Power Pivot System Service

 Run the following PowerShell command:

```
Get-PowerPivotSystemService
```

 Verify the **CurrentSolutionVersion**. [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] is version 13.0.\<major build>.\<minor build>

### Verify the version of the Analysis Services Windows Service

 If you upgraded only some of your [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] servers in a SharePoint 2010 farm, the instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] on unupgraded servers will be older than the version expected in the farm. You'll need to upgrade all of your servers to the same version in order for them to be usable. Use one of the following methods to verify the version of the SQL Server Analysis Services ( [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]) Windows service on each computer.

 **Windows File Explorer**:

1. Navigate to the **Bin** folder for the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] instance. For example `C:\Program Files\Microsoft SQL Server\MSAS13.POWERPIVOT\OLAP\bin`.

1. Right-click `msmdsrv.exe`, and select **Properties**.

1. Select **Details**.

1. [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] file version should be 13.00.\<major build>.\<minor build>.

1. Verify that this number is identical to the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution and system service version.

 **Service start information:**

 When the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service starts, it writes version information into the Windows event log.

1. Run Windows `eventvwr`

1. Create a filter for source `MSOLAP$POWERPIVOT`.

1. Look for an information level event similar to the following

     Service started. Microsoft SQL Server Analysis Services 64 Bit Evaluation (x64) RTM **13.0.2000.8**.

 **Use PowerShell to verify file version.**

 You can use PowerShell to verify the product version. PowerShell is a good option if you want to script or automate version verification.

```
(get-childitem "C:\Program Files\Microsoft SQL Server\MSAS13.POWERPIVOT2000\OLAP\bin\msmdsrv.exe").VersionInfo
```

 The above PowerShell command returns information similar to the following:

 ProductVersion   FileVersion           FileName

 **13.0.2000.8** 2016.0130.200    C:\Program Files\Microsoft SQL Server\MSAS13.POWERPIVOT2000\OLAP\bin\msmdsrv.exe

### Verify the MSOLAP Data Provider Version on SharePoint

 Use the following instructions to check which versions of the Analysis Services OLE DB providers are trusted by Excel Services. You must be a farm or service application administrator to check the trusted data provider settings of Excel Services.

1. In Central Administration, in Application Management, select **Manage service applications**.

1. Select the name of the Excel Services service application, for example **ExcelServiceApp1**.

1. Select **Trusted Data Providers**. You should see MSOLAP.5 (Microsoft OLE DB Provider for OLAP Services 11.0). If you upgraded your [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation, you'll also see MSOLAP.4 from the previous version.

1. For more information, see [Add MSOLAP.5 as a Trusted Data Provider in Excel Services](/analysis-services/power-pivot-sharepoint/add-msolap-5-as-a-trusted-data-provider-in-excel-services).

 MSOLAP.4 is described as the Microsoft OLE DB Provider for OLAP Services 10.0. This version might be the default version from [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] that is installed with Excel Services, or it might be the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version. The default version that SharePoint installs doesn't support [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data access. You must have the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version or later to connect to [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks on SharePoint. To verify you have the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] version, use the instructions in the previous section that explain how to verify the version by viewing file properties.

### Verify the ADOMD.NET Data Provider Version

 Use the following instructions to check which version of ADOMD.NET is installed. You must be a farm or service application administrator to check the trusted data provider settings of Excel Services.

1. On your SharePoint application server, browse to `c:\Windows\Assembly`.

1. Sort by assembly name and find the **Microsoft.Analysis Services.Adomd.Client**.

1. Verify you have version 13.0.\<build number>.

## <a id="geminifarm"></a> Upgrade Multiple Power Pivot for SharePoint Servers in a SharePoint Farm

 In a multi-server topology that includes more than one [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] server, all server instances and components must be the same version. The server that runs the highest version of the software sets the level for all servers in the farm. If you upgrade just some of the servers, the ones that are running older versions of the software will become unavailable until they're also upgraded.

 After you upgrade the first server, additional servers that aren't yet upgraded **will become unavailable**. Availability is restored after all servers run at the same level.

 SQL Server Setup upgrades the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] solution files in place on the physical computer, but to upgrade the solutions in use by the farm, you must use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool described in a previous section of this article.

## <a id="qfe"></a> Apply a QFE to a Power Pivot instance in the farm

 Patching a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server updates existing program files with a newer version that includes a fix for a specific problem. When applying a QFE to a multi-server topology, there's no primary server that you must begin with. You can start with any server as long as you apply the same QFE to the other [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] servers in the farm.

 When you apply the QFE, you must also perform a configuration step that updates the server version information in the farm configuration database. The version of the patched server becomes the new expected version for the farm. Until the QFE is applied and configured across all machines, the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint instances that don't have the QFE will be unavailable to handle requests for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data.

 To ensure the QFE is applied and configured correctly, follow these instructions:

1. Install the patch using the instructions that are provided with the QFE.

1. Start the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool.

1. Select **Upgrade Features, Services, Applications and Solutions**, and thenselectk **OK**.

1. Review the actions that are included in the upgrade task, and then select **Validate**.

1. Select **Run** to apply the actions.

1. Repeat for additional [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint instances in the farm.

    > [!IMPORTANT]  
    >  In a multi-server deployment, be sure to both patch and configure each instance before continuing to the next machine. The [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Configuration Tool must complete the upgrade task for the current instance before you move on to the next instance.

 To check version information for services in the farm, use the **Check product and patch installation status** page in the Upgrade and Patch Management section in Central Administration.

## <a id="verify"></a> Post-upgrade verification tasks

 After upgrade is complete, use the following steps to verify the server is operational.

|Task|Link|
|----------|----------|
|Verify the service is running on all computers that run [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.|[Start or Stop a Power Pivot for SharePoint Server](/analysis-services/power-pivot-sharepoint/start-or-stop-a-power-pivot-for-sharepoint-server)|
|Verify feature activation at the site collection level.|[Activate Power Pivot Feature Integration for Site Collections in Central Administration](/analysis-services/power-pivot-sharepoint/activate-power-pivot-integration-for-site-collections-in-ca)|
|Verify individual [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks are loading properly by opening a workbook and selecting on filters and slicers to initiate a query.|Check for the presence of cached files on the hard drive. A cached file confirms that the data file was loaded on that physical server. Look for cached files in the c:\Program Files\Microsoft SQL Server\MSAS13.POWERPIVOT\OLAP\Backup folder.|
|Test data refresh on selected workbooks that are configured for data refresh.|The easiest way to test data refresh is to modify a data refresh schedule, choosing the **Also refresh as soon as possible** checkbox so that data refresh runs immediately. This step will determine whether data refresh is successful for the current workbook. Repeat these steps for other frequently used workbooks to ensure that data refresh is functional. For more information about scheduling data refresh, see [Schedule a Data Refresh (Power Pivot for SharePoint)](/sharepoint/administration/data-refresh-using-the-unattended-data-refresh-account).|
|Over time, monitor the data refresh reports in [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Management Dashboard to confirm there are no data refresh errors.|[Power Pivot Management Dashboard and Usage Data](/analysis-services/power-pivot-sharepoint/power-pivot-management-dashboard-and-usage-data)|

 For more information about how to configure [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] settings and features, see [Power Pivot Server Administration and Configuration in Central Administration](/analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration).

 For step-by-step instructions that guide you through all of the post-installation configuration tasks, see [Initial Configuration (Power Pivot for SharePoint)](/sharepoint/administration/configure-power-pivot-for-sharepoint-2013).

## See also

- [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
- [Power Pivot for SharePoint 2010 Installation](https://sharepointgeorge.com/2012/installing-sql-server-powerpivot-sharepointstep-step-guide/)
