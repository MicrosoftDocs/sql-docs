---
title: "Uninstall PowerPivot for SharePoint | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 3941a2f0-0d0c-4d1a-8618-7a6a7751beac
author: markingmyname
ms.author: maghan
manager: craigg
---
# Uninstall PowerPivot for SharePoint
  Uninstalling a [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installation is a multi-step operation that includes preparing for uninstall, removing features and solutions from the farm, and removing program files and registry settings.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  SharePoint 2013 | SharePoint 2010  
  
 **In this topic:**  
  
-   [Prerequisites](#prereq)  
  
-   [Step 1: Pre-Uninstall Checklist](#bkmk_before)  
  
-   [Step 2: Remove Features and Solutions from SharePoint](#bkmk_remove)  
  
-   [Step 3: Run SQL Server Setup to Remove Programs from the Local Computer](#bkmk_uninstall)  
  
-   [Step 4: Uninstall the PowerPivot for SharePoint add-in](#bkmk_addin)  
  
-   [Step 5: Verify Uninstall](#verify)  
  
-   [Step 6: Post-Uninstall Checklist](#bkmk_post)  
  
##  <a name="prereq"></a> Prerequisites  
  
-   You must be a SharePoint farm administrator or service application administrator to uninstall features and solutions in the farm.  
  
-   You must be a SQL Server System Administrator and a member of the local Administrators group if you are also uninstalling the Database Engine.  
  
-   You must be an Analysis Services System Administrator and a member of the local Administrators group to uninstall Analysis Services and [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)].  
  
##  <a name="bkmk_before"></a> Step 1: Pre-Uninstall Checklist  
 PowerPivot data access will be disabled once the software that supports query and data processing is removed from the farm. As a first step, you should preemptively delete files and libraries that will no longer be operational. This lets you address any questions or concerns about 'missing data' before you uninstall the software.  
  
1.  Delete all PowerPivot workbooks, documents, and libraries that are associated with a PowerPivot for SharePoint installation. Neither the libraries nor the documents will function once the software is uninstalled.  
  
    -   [Delete PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/delete-power-pivot-gallery.md)  
  
    -   [Delete a PowerPivot Data Feed Library](../../analysis-services/power-pivot-sharepoint/delete-a-power-pivot-data-feed-library.md)  
  
2.  Delete Excel workbooks or Reporting Services reports in other libraries that contain or reference PowerPivot data.  
  
3.  Remove any web part in a PerformancePoint dashboard that references PowerPivot data.  
  
4.  Review SharePoint permissions on existing sites and libraries to determine whether you need to change them. Some PowerPivot data access scenarios, particularly secondary data access via a URL connection string to PowerPivot data in another workbook, require Read permissions, which is higher than the View permissions that are typically assigned to SharePoint users who only visit a site. If you no longer require Read permissions, you can reduce permissions accordingly.  
  
5.  Optionally, stop the services and wait several days before uninstalling the software. This step is not necessary for uninstall, but it gives you the option of temporarily resuming the service while you work out any data migration or technology substitution issues that you might have missed.  
  
##  <a name="bkmk_remove"></a> Step 2: Remove Features and Solutions from SharePoint  
 Use the PowerPivot Configuration Tool to remove PowerPivot services and applications from SharePoint.  
  
-   You must be a farm administrator, a server administrator on the Analysis Services instance, and **db_owner** on the farm's configuration database.  
  
-   Use the appropriate version of the configuration tool for the version of SharePoint. Do not use either tool with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] installations.  
  
-   Verify that the SharePoint Administration service is running.  
  
1.  **Run the Configuration tool:** Note the configuration tools are only listed only when [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is installed on the local server.On the **Start** menu, point to **All Programs**, click [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], click **Configuration Tools**, and then click one of the following:  
  
    -   **PowerPivot for SharePoint 2013 Configuration**  
  
    -   **PowerPivot Configuration Tool**  
  
2.  Select **Remove Features, Services, Applications, and Solutions** and then click **OK**.  
  
3.  Optionally, expand the window to full size. You should see a button bar at the bottom of the window that includes **Validate**, **Run**, and **Exit** commands.  
  
4.  Review each action in the task list to understand what each one does.  
  
     In **Remove PowerPivot Service Applications**, you are given the option of deleting application data associated with the service application. The application data is a SQL Server database that was created with the service application for the purpose of storing data refresh schedules, database instance information, usage data, and other data used by PowerPivot for SharePoint. It does not store user files, such as PowerPivot workbooks. Unless you have a specific reason to keep the application data (for example, if you have data retention policies related to data refresh or data access) you can delete the application database without removing any files that were created or saved by SharePoint users.  
  
     To delete the database, select **Remove PowerPivot Service Applications** and then select the **Delete application data associated with this service application**.  
  
5.  Optionally, review detailed information in the **Output** tab or **Script** tab.  
  
     The Output tab is a summary of the actions that will be performed by the tool. This information is saved in log files at:  
  
     `C:\Program Files\Microsoft SQL Server\120\Tools\PowerPivotTools\SPAddinConfiguration\Log`  
  
     The Script tab shows the PowerShell cmdlets or references the PowerShell script files that the tool will run.  
  
6.  Click **Validate** to check whether each action is valid. If **Validate** is not available, it means that all of the actions are valid for your system.  
  
7.  Click **Run** to perform all of the actions that are valid for this task. **Run** is available only after the validation check is passed. When you click **Run**, the following warning appears, reminding you that actions are processed in batch mode: "All of the configuration settings that are flagged as valid in the tool will be applied to the SharePoint farm. Do you want to continue?"  
  
8.  Click **Yes** to continue.  
  
 **Troubleshooting errors:**  
  
 In the Configuration Tool, you can view error information in the Parameters pane for each action. For problems related to solution deployment or retraction, verify the SharePoint Administration service is started. This service runs the timer jobs that trigger configuration changes in a farm. If the service is not running, solution deployment or retraction will fail. Persistent errors indicate that an existing deployment or retraction job is already in the queue and blocking further action from the configuration tool. You can use the following PowerShell command to verify the service is running.  
  
```  
Get-Service | where {$_.displayname -like "*sharepoint* administration*"}  
```  
  
 To find and remove a deployment or retraction job that is already in the queue, do the following:  
  
1.  For all other errors, check the ULS logs. For more information, see [Configure and View SharePoint Log Files  and Diagnostic Logging &#40;PowerPivot for SharePoint&#41;](../../analysis-services/power-pivot-sharepoint/configure-and-view-sharepoint-and-diagnostic-logging.md).  
  
2.  Start the SharePoint Management Shell as an administrator and then run the following command to view jobs in the queue:  
  
    ```  
    Stsadm -o enumdeployments  
    ```  
  
3.  Review existing deployments for the following information: **Type** is Retraction or Deployment, **File** is powerpivotwebapp.wsp or powerpivotfarm.wsp.  
  
4.  For deployments or retractions related to PowerPivot solutions, copy the GUID value for **JobId** and then paste it into the following command (use the Mark, Copy, and Paste commands on the Shell's Edit menu to copy the GUID):  
  
    ```  
    Stsadm -o canceldeployment -id "<GUID>"  
    ```  
  
5.  Retry the task in the configuration tool by clicking **Validate** followed by **Run**.  
  
 Alternatively, you can use PowerShell to remove features and solutions from the farm. For more information, see [PowerShell Reference for PowerPivot for SharePoint](/sql/analysis-services/powershell/powershell-reference-for-power-pivot-for-sharepoint).  
  
##  <a name="bkmk_uninstall"></a> Step 3: Run SQL Server Setup to Remove Programs from the Local Computer  
 Deleting program files requires that you run SQL Server Setup to uninstall the software. Uninstall removes both files and the registry entries that were created by Setup. You can use the Programs and Features page to uninstall the software. An installation of [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] is part of a SQL Server installation.  
  
 You can uninstall part of an installation without impacting other SQL Server instances (or features in the same instance) that are already installed. For example, you can uninstall PowerPivot for SharePoint while leaving other components, such as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] or the Database Engine, installed.  
  
1.  Select **Microsoft SQL Server 2014 (64-bit)** from the program list.  
  
2.  Click **Uninstall/Change**.  
  
3.  Click **Remove**. This starts SQL Server Setup.  
  
     From Setup, you can select the **PowerPivot** instance, and then select **Analysis Services** and **Analysis Services SharePoint Integration** to remove just that feature, leaving everything else in place.  
  
##  <a name="bkmk_addin"></a> Step 4: Uninstall the PowerPivot for SharePoint add-in  
 If your [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] deployment has two or more servers and you installed the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] Add-in, then uninstall the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] add-in from each server where it was installed to completely uninstall all [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] files. For more information, see [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md).  
  
##  <a name="verify"></a> Step 5: Verify Uninstall  
  
1.  In Central Administration, in **Manage services on server**, connect to the server from which you uninstalled PowerPivot for SharePoint.  
  
2.  -   If you uninstalled [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013, verify that **SQL Server PowerPivot System Service** no longer appear in the list.  
  
    -   If you uninstalled [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2010, verify that **SQL Server Analysis Services** and **SQL Server PowerPivot System Service** no longer appear in the list.  
  
3.  After you uninstall the last PowerPivot for SharePoint server in the farm, do the following:  
  
    1.  In Application Management, in **Manage service applications**, verify that PowerPivot Service Application no longer appears in the list.  
  
    2.  In System Settings, in **Manage farm features**, verify that PowerPivot Integration Feature is no longer on the page. In **Manage farm solutions**, verify that the PowerPivot solutions no longer appear on the page.  
  
    3.  In Monitoring, in **Configure diagnostic logging** and in **Configure usage and health data collection**, verify that PowerPivot events and event categories no longer appear.  
  
    4.  In General Application Settings, verify that **PowerPivot Management Dashboard** is no longer on the page.  
  
##  <a name="bkmk_post"></a> Step 6: Post-Uninstall Checklist  
 Use the following list to remove software and files that were not deleted during uninstall.  
  
1.  Delete all data files and subfolders from `C:\Program Files\Microsoft SQL Server\MSAS12.PowerPivot`, and then delete the folder. This step also deletes previously cached files in the DATA directory.  
  
2.  Delete all PowerPivot workbooks, documents, and libraries if you have not already done so.  
  
    -   [Delete PowerPivot Gallery](../../analysis-services/power-pivot-sharepoint/delete-power-pivot-gallery.md)  
  
    -   [Delete a PowerPivot Data Feed Library](../../analysis-services/power-pivot-sharepoint/delete-a-power-pivot-data-feed-library.md)  
  
3.  In Secure Store Service, delete any target applications that contain stored credentials used by PowerPivot for SharePoint. Some, but not all, entries in Secure Store Service are deleted when you uninstall PowerPivot for SharePoint. Target applications created for the PowerPivot unattended data refresh account plus any target applications you created for data refresh still exist and must be deleted manually.  
  
     In contrast, individual target applications that were auto-generated by the PowerPivot System Service are deleted automatically when PowerPivot is uninstalled.  
  
4.  In Control Panel, click **Programs**, and then click **Uninstall a program.** Uninstall any Analysis Services client libraries that are no longer used. Analysis Services ADOMD.NET and Microsoft SQL Server Analysis Management Objects are not removed when you uninstall PowerPivot for SharePoint. Because the libraries might be used by other programs that use Analysis Services data, SQL Server Setup does not uninstall them automatically. You must uninstall these client libraries individually if you no longer need them.  
  
     Do not uninstall the SQL Server Reporting Services SharePoint 2010 Add-in unless you are following troubleshooting or installation instructions that specifically direct you to uninstall it. The Reporting Services Add-in is used by Access Services. It is installed by the SharePoint Products Preparation tool and should remain on the system to support functionality required by SharePoint.  
  
     Do not uninstall the Analysis Services OLE DB provider. SharePoint installs the OLE DB provider as a prerequisite for Excel workbooks that connect to Analysis Services databases. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installs a newer version, but this version is backwards compatible so you should leave it on the system to avoid data connection problems later.  
  
## See Also  
 [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md)   
 [PowerPivot Configuration Tools](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-tools.md)  
  
  
