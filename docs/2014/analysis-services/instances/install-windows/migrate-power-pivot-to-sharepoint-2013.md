---
title: "Migrate PowerPivot to SharePoint 2013 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: f698ceb1-d53e-4717-a3a0-225b346760d0
author: minewiskan
ms.author: owend
manager: craigg
---
# Migrate PowerPivot to SharePoint 2013
  
  
 SharePoint 2013 does not support in-place upgrade. However the procedure of **database-attach upgrade is supported**. The behavior is different from upgrading to SharePoint 2010, where a customer could choose between the two basic upgrade approaches, in-place upgrade and database-attach upgrade.  
  
 If you have a [!INCLUDE[ssGeminiShort](../../../includes/ssgeminishort-md.md)] installation integrated with SharePoint 2010, you cannot upgrade in-place the SharePoint server. However you can migrate content databases and service application databases from the SharePoint 2010 farm to a SharePoint 2013 farm. This topic is an overview of the steps required to complete a database-attach upgrade and complete a migration related to PowerPivot:  
  
 **[!INCLUDE[applies](../../../includes/applies-md.md)]**  SharePoint 2013  
  
### Migration Overview  
  
|1|2|3|4|  
|-------|-------|-------|-------|  
|Prepare the SharePoint 2013 farm|Backup, copy, restore databases.|Mount content databases|Migrate [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Schedules|  
||[!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]|SharePoint Central Administration<br /><br /> Windows PowerShell|SharePoint application Pages<br /><br /> Windows PowerShell|  
  
 **In this topic:**  
  
-   [1) Prepare the SharePoint 2013 Farm](#bkmk_prepare_sharepoint2013)  
  
-   [2) Backup, Copy, Restore the Databases](#bkmk_backup_restore)  
  
-   [3) Prepare Web Applications and Mount Content Databases](#bkmk_prepare_mount_databases)  
  
-   [4) Upgrade PowerPivot Schedules](#bkmk_upgrade_powerpivot_schedules)  
  
-   [Additional Resources](#bkmk_additional_resources)  
  
##  <a name="bkmk_prepare_sharepoint2013"></a> 1) Prepare the SharePoint 2013 Farm  
  
1.  > [!TIP]  
    >  Review the authentication method your existing web applications are configured for. SharePoint 2013 web applications default to claims-based authentication. SharePoint 2010 web applications configured for classic-mode authentication require additional steps to migrate databases from SharePoint 2010 to SharePoint 2013. If your web applications are configured for classic-mode authentication, review the SharePoint 2013 documentation.  
  
2.  Install a new SharePoint Server 2013 farm.  
  
3.  Install an instance of a [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] server in SharePoint mode. For more information, see [PowerPivot for SharePoint 2013 Installation](../../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md).  
  
4.  Run the [!INCLUDE[ssGeminiShort](../../../includes/ssgeminishort-md.md)] 2013 installation package **spPowerPivot.msi** on each server in the SharePoint farm. For more information, see [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md).  
  
5.  In SharePoint 2013 Central Administration, configure the Excel Services service application to use the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] SharePoint mode server created in the previous step. For more information, see the "Configure Basic Analysis Services SharePoint Integration" section of [PowerPivot for SharePoint 2013 Installation](../../../analysis-services/instances/install-windows/install-analysis-services-in-power-pivot-mode.md).  
  
##  <a name="bkmk_backup_restore"></a> 2) Backup, Copy, Restore the Databases  
 The "SharePoint database-attach upgrade" process is a sequence of steps to back up, copy, and restore PowerPivot related content and service application databases to the SharePoint 2013 farm.  
  
1.  **Set Database to read-only:** In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], right-click the database name and click **Properties**. On the **Options** page, set the **Database read-Only** property to **True**.  
  
2.  **Back up:** Back up each content database and service application database that you want to migrate to the SharePoint 2013 farm. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], right-click the database name, click **Tasks**, and click **Back up**.  
  
3.  File copy the database backup files (.bak) to the desired destination server.  
  
4.  **Restore:** Restore the databases to the destination [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)]. This step can be completed using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
5.  **Set Database to read-write:** Set the **Database read-Only** to **False**.  
  
##  <a name="bkmk_prepare_mount_databases"></a> 3) Prepare Web Applications and Mount Content Databases  
 For a more detailed explanation of the following procedures, see [Upgrade databases from SharePoint 2010 to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256690) (https://go.microsoft.com/fwlink/p/?LinkId=256690).  
  
1.  **Take Databases Offline:**  
  
     Take each SharePoint 2013 content database offline, using SharePoint Central Administration. The content databases are replaced by the databases you copied over. Consider what is the best sequence for your environment. Consider taking each database offline and mount its relevant replacement database before taking the next content database offline. Another option is to take all the content databases offline as a group.  
  
    1.  In SharePoint Central Administration, click **Application Management**.  
  
    2.  Click **Manage Content Databases**.  
  
    3.  Click the name of the database.  
  
    4.  On the **Manage Content Database Settings**, set **Database status** to **Offline**.  
  
    5.  Select **Remove Content Database**. Note the warning that sites stored in the content database will no longer be accessible.  
  
-   **Mount content databases:**  
  
     Use PowerShell cmdlets in the SharePoint 2013 Management shell to mount the migrated content database. The service application database does not need to be mounted, only the content databases: ![PowerShell related content](../../../reporting-services/media/rs-powershellicon.jpg "PowerShell related content")  
  
    ```  
    Mount-SPContentDatabase "SharePoint_Content_O14-KJSP1" -DatabaseServer "[server name]\powerpivot" -WebApplication [web application URL]  
    ```  
  
     For more information, see [Attach or detach content databases (SharePoint Server 2010)](https://technet.microsoft.com/library/ff628582.aspx) (https://technet.microsoft.com/library/ff628582.aspx).  
  
     **Status when the step is complete:**  When the mount operation is complete, users can see files that were in the old content database. Therefore users can see and open the workbooks in the document library.  
  
    > [!TIP]  
    >  It is possible at this point in the migration process to create new schedules for the migrated workbooks. However, the schedules are created in the new [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] service application database, and not the database you copied from the old SharePoint farm. Therefore it will not contain any of the old schedules. After you complete the following steps to use the old database and migrate the old schedules, the new schedules are not available.  
  
### Troubleshoot issues when you attempt to mount databases  
 This section summarizes possible issues encountered when mounting the database.  
  
1.  **Authentication errors:** If you see errors related to authentication, review what authentication mode the source web applications are using. The error could be caused by a mismatch in authentication between the SharePoint 2013 web application and the SharePoint 2010 web application. See the [1) Prepare the SharePoint 2013 Farm](#bkmk_prepare_sharepoint2013) for more information.  
  
2.  **Missing PowerPivot.Files:** If you see errors related to missing PowerPivot .dlls, the **spPowerPivot.msi** has not been installed or the [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] Configuration Tool has not been used to configure PowerPivot.  
  
##  <a name="bkmk_upgrade_powerpivot_schedules"></a> 4) Upgrade PowerPivot Schedules  
 This section describes the details and options for migrating PowerPivot schedules. Schedule migration is a two-step process. First configure the PowerPivot service application to use the migrated service application database. Second, choose one of two options for schedule migration.  
  
 **Configure the service application to use the migrated service application database.**  
  
 In SharePoint Central Administration to configure the PowerPivot services application to use the old service application database you copied over. The PowerPivot Service upgrades the service application database to the new schema.  
  
1.  In SharePoint Central Administration, click **Manage Service Applications**.  
  
2.  Find the PowerPivot service application, for example "Default PowerPivot Service Application", click the name of the service application and click **Properties** in the SharePoint ribbon.  
  
3.  Update the database server name-instance and the database name. To the correct names for the database you backed up, copied, and restored. Once you click **Ok**, the service application database is upgraded. Errors will be in the ULS log.  
  
 **Upgrade PowerPivot schedules**  
  
 Configure the PowerPivot service application to migrate refresh schedules.  
  
-   **Migrate Schedules option1: SharePoint farm administrator**  
  
    1.  In the SharePoint 2013 Management Run the `Set-PowerPivotServiceApplication` cmdlet with the `-StartMigratingRefreshSchedules` switch to enable automatic on demand schedule migration ![PowerShell related content](../../../reporting-services/media/rs-powershellicon.jpg "PowerShell related content"). The following Windows PowerShell script assumes that there is only one PowerPivot service application.  
  
        ```  
        $app=Get-PowerPivotServiceApplication  
        Set-PowerPivotServiceApplication $app -StartMigratingRefreshSchedules  
        ```  
  
         After the Windows PowerShell script is run, the schedules are active and the schedules will run at the next appropriate time. However, the status one the schedule refresh page is not enabled. When the schedule runs the first time it will be migrated and on the schedule refresh page, **Enabled**  will be true.  
  
    2.  If you want to check the current value of the StartMigratingRefreshSchedules property, run the following PowerShell script. The Script loops through all PowerPivot service application objects and display the name and property values:  
  
        ```  
        $apps = Get-PowerPivotServiceApplication  
        foreach ($app in $apps){}  
        Get-PowerPivotServiceApplication $appp | format-table -property displayname,id,StartMigratingRefreshSchedules  
        ```  
  
     **Migrate Schedules option2: User updates each workbook**  
  
    1.  Another option to migrate schedules is to enable the schedule refresh for each workbook. Navigate to the document library that contains the workbooks.  
  
    2.  Open the context menu and click **Manage PowerPivot Data Refresh**.  
  
    3.  In the **schedule refresh** section, click **Enable**.  
  
    4.  You can select **Also refresh as soon as possible**. This option adds one instance of the refresh to the queue as soon as you click ok. The regular refresh schedule still triggers at the appropriate time.  
  
    5.  Click **Ok**. The refresh history is now visible in the refresh page, the schedule will fire at the normal time.  
  
 **SQL Server 2008 R2 PowerPivot workbooks**  
  
-   SQL Server 2008 R2 PowerPivot workbooks do not automatically upgrade when they are used in SQL Server 2012 SP1 PowerPivot for SharePoint 2013. After you migrate a content database containing the 2008 R2 workbooks, you can use the workbooks but the schedules do not upgrade.  
  
-   For more information, see [Upgrade Workbooks and Scheduled Data Refresh &#40;SharePoint 2013&#41;](../../../analysis-services/instances/install-windows/upgrade-workbooks-and-scheduled-data-refresh-sharepoint-2013.md).  
  
##  <a name="bkmk_additional_resources"></a> Additional Resources  
  
> [!NOTE]  
>  For more information on [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] and SharePoint database-attach upgrade, see the following:  
  
-   [Upgrade Workbooks and Scheduled Data Refresh &#40;SharePoint 2013&#41;](../../../analysis-services/instances/install-windows/upgrade-workbooks-and-scheduled-data-refresh-sharepoint-2013.md).  
  
-   [Overview of the upgrade process to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256688) (https://go.microsoft.com/fwlink/p/?LinkId=256688).  
  
-   [Clean up preparations before an upgrade to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256689) (https://go.microsoft.com/fwlink/p/?LinkId=256689).  
  
-   [Upgrade databases from SharePoint 2010 to SharePoint 2013](https://go.microsoft.com/fwlink/p/?LinkId=256690) (https://go.microsoft.com/fwlink/p/?LinkId=256690).  
  
  
