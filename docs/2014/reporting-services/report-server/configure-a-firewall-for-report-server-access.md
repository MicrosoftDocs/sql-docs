---
title: "Configure a Firewall for Report Server Access | Microsoft Docs"
ms.custom: ""
ms.date: "08/10/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "firewall systems [Reporting Services]"
  - "configuring servers [Reporting Services]"
ms.assetid: 04dae07a-a3a4-424c-9bcb-a8000e20dc93
author: markingmyname
ms.author: maghan
manager: kfile
---
# Configure a Firewall for Report Server Access
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Report server applications and published reports are accessed through URLs that specify an IP address, port, and virtual directory. If Windows Firewall is turned on, the port that the report server is configured to use is most likely closed. Indications that a port might be closed are the appearance of a blank Web page after requesting a report, or a blank page when you attempt to open Report Manager from a remote client computer.  
  
 To open a port, you must use the Windows Firewall utility on the report server computer. Reporting Services will not open ports for you; you must perform this step manually.  
  
 By default, the report server listens for HTTP requests on port 80. As such, the following instructions include steps that specify that port. If you configured the report server URLs to use a different port, you must specify that port number when following the instructions below.  
  
 If you are accessing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases on external computers, or if the report server database is on an external [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, you must open port 1433 and 1434 on the external computer. For more information, see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. For more information about the default Windows firewall settings, and a description of the TCP ports that affect the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Prerequisites  
 These instructions assume that you already configured the service account, created the report server database, and configured URLs for the Report Server Web service and Report Manager. For more information, see [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md).  
  
 You should also have verified that the report server is accessible over a local Web browser connection to the local report server instance. This step establishes that you have a working installation. You should verify that the installation is configured correctly before you begin opening ports. To complete this step on  Windows Server, you must have also added the report server site to Trusted Sites. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
## Opening Ports in Windows Firewall  
 There are separate instructions for different versions of Windows Firewall.  
  
#### To open port 80 on Windows 7, Windows Server 2008 R2, Windows Server 2012 and 2012 R2  
  
1.  From the **Start** menu, click **Control Panel**, click **System and Security**, and then click **Windows Firewall**. Control Panel is not configured for 'Category' view, you only need to select **Windows Firewall.**  
  
2.  Click **Advanced Settings**.  
  
3.  Click **Inbound Rules.**  
  
4.  Click **New Rule** in the **Actions** window**.**  
  
5.  Click **Rule Type** of **Port.**  
  
6.  Click **Next**.  
  
7.  On the **Protocol and Ports** page click **TCP**.  
  
8.  Select **Specific Local Ports** and type a value of **80**.  
  
9. Click **Next**.  
  
10. On the **Action** page click **Allow the connection**.  
  
11. Click **Next**.  
  
12. On the **Profile** page click the appropriate options for your environment.  
  
13. Click **Next**.  
  
14. On the **Name** page enter a name of**ReportServer (TCP on port 80)**  
  
15. Click **Finish**.  
  
16. Restart the computer.  
  
#### To open port 80 on Windows Vista or Windows Server 2008  
  
1.  From the **Start** menu, click **Control Panel**, click **Security**, and then click **Windows Firewall**.  
  
2.  Click **Allow a program through Windows Firewall**.  
  
3.  Click **Continue**.  
  
4.  On the Exceptions tab, click **Add Port**.  
  
5.  In Name, type **ReportServer (TCP on port 80)**.  
  
6.  In Port number, type **80**.  
  
7.  Verify that **TCP** is selected.  
  
8.  Click **Change Scope**.  
  
9. Click **My network (subnet) only**, and then click **OK**.  
  
10. Click **OK** to close the dialog box.  
  
11. Restart the computer.  
  
## Next Steps  
 After you open the port and before you confirm whether remote users can access the report server on the port that you open, you must grant user access to the report server through role assignments on Home and at the site level. You can open a port correctly and still have report server connections fail if users do not have sufficient permissions. For more information, see [Grant User Access to a Report Server &#40;Report Manager&#41;](../security/grant-user-access-to-a-report-server.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 You can also verify that the port is opened correctly by starting Report Manager on a different computer. For more information, see [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-report-server-urls-ssrs-configuration-manager.md)   
 [Create a Report Server Database  &#40;SSRS Configuration Manager&#41;](../../sql-server/install/create-a-report-server-database-ssrs-configuration-manager.md)   
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md)  
  
  
