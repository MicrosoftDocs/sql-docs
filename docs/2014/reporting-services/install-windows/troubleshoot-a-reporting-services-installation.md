---
title: "Troubleshoot a Reporting Services Installation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: e2536f7f-d90c-4571-9ffd-6bbfe69018d6
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Troubleshoot a Reporting Services Installation
  If you cannot install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] because of errors that occur during setup, use the instructions in this topic to address the conditions that are most likely to cause installation errors.  
  
 For The latest information regarding issues with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see [Reporting Services SQL Server 2012 Tips Tricks, and Troubleshooting](https://go.microsoft.com/fwlink/?LinkId=221297)  
  
 For information about other errors and issues related to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] see [Troubleshoot SSRS issues and errors.](https://social.technet.microsoft.com/wiki/contents/articles/ssrs-troubleshooting-issues-and-errors.aspx)  
  
 Review the [Online Release Notes](https://go.microsoft.com/fwlink/?linkid=236893) in case the issue you encounter is described in the release notes.  
  
 This topic contains the following information:  
  
-   [Check Setup Logs](#bkmk_setuplogs)  
  
-   [Check Prerequisites](#bkmk_prereq)  
  
-   [Troubleshoot Problems with SharePoint Mode installations](#bkmk_tshoot_sharepoint)  
  
-   [Troubleshoot Problems with the Native Mode Installations](#bkmk_tshoot_native)  
  
-   [Additional Resources](#bkmk_additional)  
  
##  <a name="bkmk_setuplogs"></a> Check Setup Logs  
 Setup errors are recorded in log files in the **Program Files\Microsoft SQL Server\110\Setup Bootstrap\Log** folder. A subfolder is created each time you run Setup. The subfolder name is the time and date you ran Setup. For instructions on how to view the Setup log files, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  
  
-   The log files include a collection of files.  
  
-   Open the *_summary.txt file to view product, component, and instance information.  
  
-   Open the *_errorlog.txt file to view error information generated during Setup.  
  
-   Open the *_RS\_\*_ComponentUpdateSetup.log to view [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] setup information.  
  
##  <a name="bkmk_prereq"></a> Check Prerequisites  
 Setup checks prerequisites automatically. However, if you are troubleshooting setup problems, it is helpful to know which requirements Setup is checking for.  
  
-   Account requirements for running Setup include membership in the local Administrators group. Setup must have permission to add files, registry settings, create local security groups, and set permissions. If you are installing a default configuration, Setup must have permission to create a report server database on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on which you are installing.  
  
-   Operating System must support HTTP.SYS 1.1.  
  
-   HTTP service must be enabled and running.  
  
-   Distributed Transaction Coordinator (DTC) must be running if you are also installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service.  
  
-   Authz.dll must be present in the System32 folder.  
  
 Setup no longer checks for Internet Information Services (IIS) or [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] requires MDAC 2.0 and the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] version 2.0; Setup will install these if they are not already installed.  
  
##  <a name="bkmk_tshoot_sharepoint"></a> Troubleshoot Problems with SharePoint Mode installations  
  
-   [Reporting Services Configuration Manager Does not start](#bkmk_configmanager_notstart)  
  
-   [You do not see the SQL Server Reporting Services service in SharePoint Central Administration after installing SQL Server 2012 SSRS in SharePoint mode](#bkmk_no_ssrs_service)  
  
-   [Reporting Services PowerShell cmdlets are not available and commands are not recognized](#bkmk_cmdlets_not_recognized)  
  
-   [You see an error message indicating the URL is not configured](#bkmk_URL_not_configured)  
  
-   [Setup fails on a computer with SharePoint installed but it is not configured](#bkmk_sharepoint_not_confiugred)  
  
-   [SharePoint Central Administration Page is blank](#bkmk_central_admin_blank)  
  
-   [You see an error Message when you try to create a new Report Builder Report](#bkmk_reportbuilder_newreport_error)  
  
-   [You see an error message that RS_SHP is not supported with PREPAREIMAGE](#bkmk_RS_SHP_notsupported)  
  
###  <a name="bkmk_configmanager_notstart"></a> Reporting Services Configuration Manager Does not start  
 **Description:** This issue is by design in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] is now architected for the SharePoint service architecture. The Configuration Manager is no longer needed to configure and administer [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode.  
  
 **Workaround:** Use SharePoint Central Administration to configure a report server in SharePoint mode. For more information, see [Manage a Reporting Services SharePoint Service Application](../../../2014/reporting-services/manage-a-reporting-services-sharepoint-service-application.md)  
  
###  <a name="bkmk_no_ssrs_service"></a> You do not see the SQL Server Reporting Services service in SharePoint Central Administration after installing SQL Server 2012 SSRS in SharePoint mode  
 **Description:** If after successfully installing [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode and the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for SharePoint 2010, you do not see "SQL Server Reporting Services" in the following two menus, then the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service has not been registered:  
  
-   SharePoint 2010 Central Administration -> "Application Management" -> "Manage Services on Server" page  
  
-   SharePoint 2010 Central Administration -> "Application Management" -> "Manage Service Applications" ->"New" menu  
  
 **Workaround:** To register and start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint Services, complete the following:  
  
1.  On the computer that runs SharePoint 2010 Central Administration  
  
    1.  Open the SharePoint 2010 Management Shell with administrator privileges. Right click the icon and click, "Run As Administrator". Run the following three cmdlets from the shell:  
  
    2.  ```  
        Install-SPRSService  
        ```  
  
    3.  ```  
        Install-SPRSServiceProxy  
        ```  
  
    4.  ```  
        Get-SPServiceInstance -all |where {$_.TypeName -like "SQL Server Reporting*"} | Start-SPServiceInstance  
        ```  
  
2.  Verify the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service shows status as "**Started**" on the page: SharePoint 2010 Central Administration -> "**Application Management**" -> "**Manage Services on Server**"  
  
###  <a name="bkmk_cmdlets_not_recognized"></a> Reporting Services PowerShell cmdlets are not available and commands are not recognized  
 **Description:** When you try to run a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] PowerShell cmdlet, you see an error message similar to the following:  
  
-   The term 'Install-SPRSServiceInstall-SPRSService' **is not recognized** as the name of a cmdlet, function, script file, or operable program. Check the spelling of the name, or if a path was included, verify that the path is correct and try again.At line:1 char:39+ Install-SPRSServiceInstall-SPRSService <<<<    + CategoryInfo          : ObjectNotFound: (Install-SPRSServiceInstall-SPRSService:String) [], CommandNotFoundExcep  
  
 **Workaround:** Complete one of the following:  
  
-   Run the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products. **rssharepoint.msi**.  
  
-   Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode from the SQL Server installation media.  
  
 **Note:** If the **SharePoint 2013 Management Shell** is open when you complete one of the workarounds, close and reopen the management shell.  
  
 For more information, see the following:  
  
-   [Where to find the Reporting Services add-in for SharePoint Products](../../reporting-services/install-windows/where-to-find-the-reporting-services-add-in-for-sharepoint-products.md)  
  
-   [Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)  
  
-   [Install Reporting Services SharePoint Mode for SharePoint 2013](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2013.md)  
  
###  <a name="bkmk_URL_not_configured"></a> You see an error message indicating the URL is not configured  
 **Description:** You see and error message similar to the following:  
  
 This SQL Server Reporting Services (SSRS) functionality is not supported. Use Central Administration to verify and fix one or more of the following issues:•A report server URL is not configured. Use the SSRS Integration page to set it.•The SSRS service application proxy is not configured. Use the SSRS service application pages to configure the proxy.•The SSRS service application is not mapped to this web application. Use the SSRS service application pages to associate the SSRS service application proxy to the Application Proxy Group for this web application.  
  
 **Workaround:** The error message contains three suggested steps to correct this issue. The first suggestion in the message 'A report server URL is not configured..' is relevant when integrating with report server version previous to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. SharePoint Configuration for the previous report server versions is completed on the **General Application Settings** page, using the **SQL Server Reporting Services (2008 and 2008 R2)**..  
  
 **More Information:** You will see this error message when attempting to use any of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] functionality that requires a connection to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service. This includes:  
  
-   Opening SQL Server Report Builder from a SharePoint document library.  
  
-   Manage Subscriptions.  
  
-   Manage a service application.  
  
###  <a name="bkmk_sharepoint_not_confiugred"></a> Setup fails on a computer with SharePoint installed but it is not configured  
 **Description:** If you select to install Reporting Services SharePoint Mode on a computer that has SharePoint installed but SharePoint is not configured, you will see a message similar to the following and setup will stop:  
  
 SQL Server Setup has stopped working  
  
 **Workaround:** Configure SharePoint and then run SQL Server Installation.  
  
 **More Information:** When installing [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] into and existing SharePoint installation, setup attempts to install and start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint service. If SharePoint is not configured, the service installation fails, causing setup to fail.  
  
###  <a name="bkmk_central_admin_blank"></a> SharePoint Central Administration Page is blank  
 **Description:** You were able to successfully install SharePoint 2010, with no installation errors. However when you browse to Central Administration, you only see a blank page:  
  
 **Workaround:** This issue is not specific to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] but is related to the configuration of permissions in your overall SharePoint installation. The following is a list of suggested:  
  
-   Review the SharePoint topic on development environments. [Setting Up the Development Environment for SharePoint 2010 on Windows Vista, Windows 7, and Windows Server 2008](https://msdn.microsoft.com/library/ee554869\(office.14\).aspx)  
  
-   Review the forum post: [Central Administration returns blank page after installation on Windows 7](https://social.technet.microsoft.com/Forums/en/sharepoint2010setup/thread/a422a3c8-39f6-4b9e-988a-4c4d1e745694)  
  
-   The Service account you are using for SharePoint services such as the SharePoint 2010 Central Administration Service, should have administrative privileges in the local operating system.  
  
###  <a name="bkmk_reportbuilder_newreport_error"></a> You see an error Message when you try to create a new Report Builder Report  
 **Description:** You see an error message similar to the following when you attempt to create a Report Builder report inside a document library:  
  
 This functionality is not supported because a SQL Server Reporting Services service application does not exist or a report server URL has not been configured in Central Administration.  
  
 **Workaround:** Verify you have an [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application and it is correctly configured. For more information see, the section 'Create a Reporting Services Service Application' in [Install Reporting Services SharePoint Mode for SharePoint 2010](../../../2014/sql-server/install/install-reporting-services-sharepoint-mode-for-sharepoint-2010.md)  
  
###  <a name="bkmk_RS_SHP_notsupported"></a> You see an error message that RS_SHP is not supported with PREPAREIMAGE  
 **Description:** When you try to run PREPAREIMAGE for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] you see an error message similar to the following:  
  
 "The specified feature 'RS_SHP' is not supported when running the PREPAREIMAGE action, since it does not support SysPrep. Remove the features that are not compatible with SysPrep and run setup again."  
  
 **Workaround:** There is no work around. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not support SYSPREP (PREPAREIMAGE). [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode does support SYSPREP.  
  
##  <a name="bkmk_tshoot_native"></a> Troubleshoot Problems with the Native Mode Installations  
  
###  <a name="PerfCounters"></a> Performance counters are not visible after upgrading to Windows Vista or Windows Server 2008  
 If you upgrade the operating system to [!INCLUDE[wiprlhext](../../includes/wiprlhext-md.md)] or [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] on a computer that runs [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] performance counters will not be set after the upgrade.  
  
#### To reinstate Reporting Services performance counters  
  
1.  Delete the following registry keys:  
  
    -   **HKLM\SYSTEM\CurrentControlSet\Services\MSRS 2011 Web Service**  
  
    -   **HKLM\SYSTEM\CurrentControlSet\Services\MSRS 2011 Windows Service**  
  
2.  Open a command window and type the following command at the prompt:  
  
    -   **run \<** *.NET 2.0 Framework directory* **>\InstallUtil.exe \<** *Report Server Bin directory* **>\ReportingServicesLibrary.dll**  
  
        > [!NOTE]  
        >  Replace \<*.NET 2.0 Framework directory*> with the physical path of the .NET Framework 2.0 files and replace \<*Report Server Bin directory*> with the physical path of the report server bin files.  
  
3.  Restart the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.  
  
 To verify that the steps worked, open a Web browser and navigate to the Report Manager URL or the Report Server URL. Then open Performance Monitor to verify that the counters are working.  
  
#### To re-add the performance registry keys by using Registry Editor  
  
1.  Open the Registry Editor:  
  
    1.  Click **Start**, and click **Run**.  
  
    2.  In the **Run** dialog box, in the **Open** box, type `regedit`.  
  
2.  In Registry Editor, select the following registry key: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Web Service\Performance`  
  
3.  Right-click the **Performance** node, point to **New**, and click **Multi-String Value**.  
  
4.  Type `Counter Names` and then press ENTER.  
  
5.  Repeat to add the `Counter Types` registry key in this node.  
  
6.  Navigate to the following registry key: `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Web Service\Performance`  
  
7.  Right-click the **Performance** node, point to **New**, and click **Multi-String Value**.  
  
8.  Type `Counter Names` and then press ENTER.  
  
9. Repeat to add the `Counter Types` registry key in this node.  
  
 After you repair the 64-bit instance or re-add the registry keys manually, you can use Performance Monitor to configure the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] performance objects that you want to monitor.  
  
###  <a name="ConfigPropsMissing"></a> ReportServerExternalURL and PassThroughCookies configuration properties are not configured after an upgrade from SQL Server 2005  
 When you upgrade from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] to [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)], the `ReportServerExternalURL` and `PassThroughCookies` configuration properties are not configured by the upgrade process. `ReportServerExternalURL` is an optional property, and it should be set only if you are using SharePoint 2.0 Web Parts and you want users to be able to retrieve a report and open it in a new browser window. For more information about `ReportServerExternalURL`, see [URLs in Configuration Files  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/urls-in-configuration-files-ssrs-configuration-manager.md). `PassThroughCookies` is required only when using Custom authentication method. For more information about `PassThroughCookies`, see [Configure Report Manager to Pass Custom Authentication Cookies](../security/configure-the-web-portal-to-pass-custom-authentication-cookies.md).  
  
> [!NOTE]  
>  When you use Custom authentication, it is recommended that you migrate your installation rather than performing an upgrade. For more information about migrating [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Migrate a Reporting Services Installation &#40;Native Mode&#41;](../../reporting-services/install-windows/migrate-a-reporting-services-installation-native-mode.md).  
  
 By default, these properties do not exist in the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] configuration. If you configured these properties in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and you continue to require the functionality that they provide, you must manually add them to the **RSReportServer.config** file following the upgrade process. For more information, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md).  
  
###  <a name="Default2005InstallBreaks2008"></a> Installation fails for a default instance of SQL Server 2005 Reporting Services on a computer that runs SQL Server 2012Reporting Services  
 If you attempt to install a default instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on a computer that already runs an instance of [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)], the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance will fail to install with the following error message:  
  
 "An instance with the same name is already installed on this computer. To proceed with SQL Server Setup, provide a unique instance name."  
  
 This issue happens regardless of whether the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] instance is a default or named instance, and regardless of whether a [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] instance with that name already exists on the computer.  
  
 To work around this issue, you have one of the following options:  
  
-   If you must run [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] as the default instance on the computer, you must install the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance before the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] instance.  
  
-   If the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance does not need to be a default instance, you can install the [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance as a named instance after you install the [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] instance.  
  
###  <a name="WindowsAuthBreaksAfterUpgrade"></a> 401-Unauthorized error when using Windows authentication after an upgrade from SQL Server 2005 to SQL Server 2012  
 If you upgrade from [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)], and you use NTLM authentication with a built-in account for the Report Server service account, you might encounter a 401-Unauthorized error when you access the report server or Report Manager after the upgrade.  
  
 This happens because of a change in the default [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] configuration for Windows authentication. Negotiate is configured when the Report Server service account is either Network Service or Local System. NTLM is configured when the Report Server service account is not one of those built-in accounts. To fix this issue after you upgrade, you can edit the RSReportServer.config file and configure the `AuthenticationType` to be `RSWindowsNTLM`. For more information, see [Configure Windows Authentication on the Report Server](../security/configure-windows-authentication-on-the-report-server.md).  
  
###  <a name="Uninstall32BitBreaks64Bit"></a> Uninstalling 32-bit instance of SQL Server 2012 Reporting Services in side-by-side deployment with a 64-bit instance breaks the 64-bit instance  
 When you install a 32-bit instance and a 64-bit instance of [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] side-by-side on a computer, and you uninstall the 32-bit instance, four [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] registry keys are removed. This breaks the 64-bit instance of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] registry keys that are removed when you uninstall the 32-bit instance are:  
  
 `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Web Service\Performance:Counter Names` `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Windows Service\Performance:Counter Names` `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Web Service\Performance:Counter Types` `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MSRS 2011 Windows Service\Performance:Counter Types`  
  
 To fix this issue, you can repair the 64-bit instance. Although it is recommended to use repair, you can re-add the registry keys manually by using Registry Editor.  
  
> [!CAUTION]  
>  Incorrectly editing the registry can severely damage your system. Before making changes to the registry, you should back up any valued data on the computer.  
  
##  <a name="bkmk_additional"></a> Additional Resources  
 The following are additional resources you can review to assist you with trouble shooting issues:  
  
-   TechNet Wiki: Trouble Shooting topics [Troubleshoot SQL Server Reporting Services (SSRS) in SharePoint Integrated Mode](https://social.technet.microsoft.com/wiki/contents/articles/troubleshoot-sql-server-reporting-services-ssrs-in-sharepoint-integrated-mode.aspx)  
  
-   [Forum: SQL Server Reporting Services](http://social.msdn.microsoft.com/Forums/sqlreportingservices/threads)  
  
 ![SharePoint Settings](../../../2014/analysis-services/media/as-sharepoint2013-settings-gear.gif "SharePoint Settings") [Submit feedback and contact information through Microsoft SQL Server Connect](https://connect.microsoft.com/SQLServer/Feedback) (https://connect.microsoft.com/SQLServer/Feedback).  
  
  
