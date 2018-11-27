---
title: "Install PowerPivot from the Command Prompt | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 7f1f2b28-c9f5-49ad-934b-02f2fa6b9328
author: markingmyname
ms.author: maghan
manager: craigg
---
# Install PowerPivot from the Command Prompt
  You can run Setup from the command line to install SQL Server PowerPivot for SharePoint. You must include the `/ROLE` parameter in your command and exclude the `/FEATURES` parameter.  
  
## Prerequisites  
 SharePoint Server 2010 enterprise edition with Service Pack 1 (SP1) must be installed.  
  
 You must use domain accounts to provision Analysis Services.  
  
 The computer must be joined to the same domain as the SharePoint farm.  
  
##  <a name="Commands"></a> /ROLE based installation options  
 For PowerPivot for SharePoint deployments, the `/ROLE` parameter is used in place of the `/FEATURES` parameter. Valid values include:  
  
-   `SPI_AS_ExistingFarm`  
  
-   `SPI_AS_NewFarm`  
  
 Both roles install application, configuration, and deployment files that enable a PowerPivot for SharePoint to run in a SharePoint farm. Specifying either role will cause Setup to check for hardware and software requirements necessary for SharePoint integration.  
  
 The existing farm option assumes that a SharePoint farm is already in place. The new farm option assumes that you will create a new farm; it supports the addition of a Database Engine instance in the command line syntax so that you can use the Database Engine instance as the farm's database server.  
  
 In contrast with the previous releases, all server configuration tasks are performed as post-installation tasks. If you are automating installation and configuration steps, you can use PowerShell to configure the server. For more information, see [PowerPivot Configuration using Windows PowerShell](../../analysis-services/power-pivot-sharepoint/power-pivot-configuration-using-windows-powershell.md).  
  
## Example Commands  
 The following examples illustrate the usage of each option. Example 1 shows `SPI_AS_ExistingFarm`.  
  
```  
Setup.exe /q /IAcceptSQLServerLicenseTerms /ACTION=install /ROLE=SPI_AS_ExistingFarm /INSTANCENAME=PowerPivot /INDICATEPROGRESS/ASSVCACCOUNT=<DomainName\UserName> /ASSVCPASSWORD=<StrongPassword> /ASSYSADMINACCOUNTS=<DomainName\UserName>   
```  
  
 Example 2 shows `SPI_AS_NewFarm`. Notice that it includes parameters for provisioning the Database Engine.  
  
```  
Setup.exe /q /IAcceptSQLServerLicenseTerms /ACTION=install /ROLE=SPI_AS_NewFarm /INSTANCENAME=PowerPivot /INDICATEPROGRESS/SQLSVCACCOUNT=<DomainName\UserName> /SQLSVCPASSWORD=<StrongPassword> /SQLSYSADMINACCOUNTS=<DomainName\UserName> /AGTSVCACCOUNT=<DomainName\UserName> /AGTSVCPASSWORD=<StrongPassword> /ASSVCACCOUNT=<DomainName\UserName> /ASSVCPASSWORD=<StrongPassword> /ASSYSADMINACCOUNTS=<DomainName\UserName>   
```  
  
##  <a name="Join"></a> Modifying the command syntax  
 Use the following steps to modify the example command syntax.  
  
1.  Copy the following command into Notepad:  
  
    ```  
    Setup.exe /q /IAcceptSQLServerLicenseTerms /ACTION=install /ROLE=SPI_AS_ExistingFarm /INSTANCENAME=PowerPivot /INDICATEPROGRESS/ASSVCACCOUNT=<DomainName\UserName> /ASSVCPASSWORD=<StrongPassword> /ASSYSADMINACCOUNTS=<DomainName\UserName>   
    ```  
  
     The `/q` parameter runs Setup in quiet mode, which suppresses the user interface.  
  
     The `/IAcceptSQLServerLicenseTerms` is required when the `/q` or `/qs` parameter is specified for un-attended installations.  
  
     The `/action` parameter instructs Setup to perform an installation.  
  
     The `/role` parameter instructs Setup to install the Analysis Services program and configuration files required for PowerPivot for SharePoint. This role also detects and uses the existing farm connectivity information to access the SharePoint configuration database. This parameter is required. Use it instead of the `/features` parameter to specify the components to install.  
  
     The `/instancename` parameter specifies 'PowerPivot' as a named instance. This value is hard-coded and cannot be changed. It is specified in the command for educational purposes so that you know how the service is installed.  
  
     The `/indicateprogress` parameter allows you to monitor progress in the command prompt window.  
  
2.  The `PID` parameter is omitted from the command, which causes the Evaluation edition to be installed. If you want to install the Enterprise edition, add the PID to your Setup command and provide a valid product key.  
  
    ```  
  
    /PID=<product key for an Enterprise installation>  
  
    ```  
  
3.  Replace the placeholders for \<domain\username> and \<StrongPassword>with valid user accounts and passwords.  
  
     The `/assvaccount` and **/assvcpassword** parameters are used to configure the [!INCLUDE[ssGeminiSrv](../../includes/ssgeminisrv-md.md)] instance on the application server. Replace these placeholders with valid account information.  
  
     The **/assysadminaccounts** parameter must be set to the identity of the user who is running SQL Server Setup. You must specify at least one system administrator. Note that SQL Server Setup no long grants automatic sysadmin permissions to members of the built-in Administrators group.  
  
4.  Remove line breaks.  
  
5.  Select the entire command and then click **Copy** on the Edit menu.  
  
6.  Open an administrator command prompt. To do this, click **Start**, right-click the command prompt, and select **Run as administrator**.  
  
7.  Navigate to the drive or shared folder that contains the SQL Server installation media.  
  
8.  Paste the revised command into the command line. To do this, click the icon in the top left corner of the command prompt window, point to **Edit**, and then click **Paste**.  
  
9. Press **Enter** to run the command. Wait for setup to complete. You can monitor Setup's progress in the command prompt window.  
  
10. To verify installation, check the summary.txt file at \Program Files\SQL Server\120\Setup Bootstrap\Log. Final result should say "Passed" if the server installed without errors.  
  
11. Configure the server. At a minimum, you must deploy solutions, create a service application, and enable the feature for each site collection. For more information, see [Configure or Repair PowerPivot for SharePoint 2010 &#40;PowerPivot Configuration Tool&#41;](../../../2014/analysis-services/configure-repair-powerpivot-sharepoint-2010.md) or [PowerPivot Server Administration and Configuration in Central Administration](../../analysis-services/power-pivot-sharepoint/power-pivot-server-administration-and-configuration-in-central-administration.md).  
  
## See Also  
 [Configure PowerPivot Service Accounts](../../analysis-services/power-pivot-sharepoint/configure-power-pivot-service-accounts.md)   
 [PowerPivot for SharePoint 2010 Installation](../../../2014/sql-server/install/powerpivot-for-sharepoint-2010-installation.md)  
  
  
