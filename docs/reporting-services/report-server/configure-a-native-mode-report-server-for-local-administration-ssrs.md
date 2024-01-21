---
title: "Configure a native mode report server for local administration"
description: Learn how to configure the report server for local administration, if you install a Reporting Services report server in certain environments.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/28/2019
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "UAC"
  - "installing Reporting Services"
  - "Windows Vista"
  - "Localhost"
  - "windows server 2008"
  - "Vista"
---
# Configure a native mode report server for local administration (SSRS)
  Deploying a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server on one of the following operating systems requires more configuration steps if you want to administer the report server instance locally. This article explains how to configure the report server for local administration. If you need to install the report server, see [Install SQL Server from the winstallation wizard &#40;setup&#41;](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md). If you need to configure the report server, see [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md).  
  
[!INCLUDE[applies](../../includes/applies-md.md)]  

- [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode 
  
-   [!INCLUDE[winserver2012r2](../../includes/winserver2012r2-md.md)]  
  
-   [!INCLUDE[win81](../../includes/win81-md.md)]  
  
-   [!INCLUDE[win8](../../includes/win8-md.md)]  
  
-   [!INCLUDE[winserver2012](../../includes/winserver2012-md.md)]  
  
-   [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)]  
  
-   [!INCLUDE[win7](../../includes/win7-md.md)]  
  
-   [!INCLUDE[winserver2008](../../includes/winserver2008-md.md)]  
  
 Because the noted operating systems limit permissions, members of the local Administrators group run most applications as if they're using the Standard User account.  
  
 While this practice improves the overall security of your system, it prevents you from using the predefined, built-in role assignments that Reporting Services creates for local administrators.  
  
-   [Overview of configuration changes](#bkmk_configuraiton_overview)  
  
-   [Configure local report server and the web portal administration](#bkmk_configure_local_server)  
  
-   [Configure SQL Server Management Studio (SSMS) for local report server administration](#bkmk_configure_ssms)  
  
-   [Configure SQL Server Data Tools (SSDT) to publish to a local report server](#bkmk_configure_ssdt)  
  
-   [Additional information](#bkmk_addiitonal_informaiton)  
  
##  <a name="bkmk_configuraiton_overview"></a> Overview of configuration changes  
 The following configuration changes configure the server so that you can use standard user permissions to manage report server content and operations:  
  
-   Add [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URLs to trusted sites. By default, Internet Explorer running on the listed operating systems runs in **Protected Mode**. This feature that blocks browser requests from reaching high-level processes that run on the same computer. You can disable protected mode for the report server applications by adding them as Trusted Sites.  
  
-   Create role assignments that grant you, the report server administrator, permission to manage content and operations without having to use the **Run as administrator** feature on Internet Explorer. By creating role assignments for your Windows user account, you gain access to a report server with Content Manager and System Administrator permissions. You gain the access through explicit role assignments that replace the predefined, built-in role assignments that Reporting Services creates.  
  
##  <a name="bkmk_configure_local_server"></a> Configure local report server and web portal administration  
 Complete the configuration steps in this section if you're browsing to a local report server and you see errors similar to the following example:  
  
-   The `Domain\[user name]` user doesn't include required permissions. Verify that sufficient permissions are granted and Windows User Account Control (UAC) restrictions are addressed.  
  
###  <a name="bkmk_site_settings"></a> Trusted site settings in the browser  
  
1.  Open a browser window with **Run as administrator** permissions. From the **Start** menu, right-click **Internet Explorer**, and select **Run as administrator**.  
  
1.  Select **Yes** when prompted to continue.  
  
1.  In the URL address, enter the web portal URL. For instructions, see [The web portal of a report server &#40;SSRS Native Mode&#41;](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
1.  Select **Tools**.  
  
1.  Select **Internet Options**.  
  
1.  Select **Security**.  
  
1.  Select **Trusted Sites**.  
  
1.  Select **Sites**.  
  
1. Add `https://<your-server-name>`.  
  
1. Clear the **Require server certification (https:) for all sites in this zone** checkbox if you aren't using HTTPS for the default site.  
  
1. Choose **Add**.  
  
1. Select **OK**.  
  
###  <a name="bkmk_configure_folder_settings"></a> Web portal folder settings  
  
1.  In the web portal, on the Home page, select **Manage folder**.  
  
1.  On the **Manage folders** page, select **Security** and then choose **Add group or user**.  
  
1.  On the **New Role Assignment** page, in the **Group or user** field, enter your Windows user account in this format: `<domain>\<user>`.  
  
1.  Select **Content Manager**.  
  
1.  Select **OK**.  
  
###  <a name="bkmk_configure_site_settings"></a> Web portal site settings  
  
1.  Open your browser with administrative privileges and browse to web portal, `https://<server name>/reports`.  
  
1.  Select the gear icon on the top row of the **Home** page, and then choose **Site Settings** from the menu. 
  
    :::image type="content" source="../media/ssrsgearmenu.png" alt-text="Screenshot of the Gear icon where the Site Settings option is highlighted.":::

    > [!TIP]  
    > If you don't see the **Site Settings** option, close and reopen your browser. Browse to web portal with administrative privileges.  
  
1.  On the Site settings page, select **Security** and then select **Add group or user**.  
  
1.  In the **Group or user name** field, type your Windows user account in this format: `<domain>\<user>`.  

1.  Select **System Administrator**.  
  
1.  Select **OK**.  
  
1.  Close the web portal.  
  
1. Reopen the web portal in Internet Explorer without using **Run as administrator**.  
  
##  <a name="bkmk_configure_ssms"></a> Configure SQL Server Management Studio (SSMS) for local report server administration  
 By default, you can't access all of the report server properties available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] unless you start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with administrative privileges.  
  
 **Configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]** role properties and role assignments so you don't need to start [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] with elevated permissions each time:  
  
1. From the **Start** menu, right-click **Microsoft SQL Server [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]**, and then choose **Run as administrator**.  
  
1. Connect to your local [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server.  
  
1. In the **Security** node, select **System Roles**.  
  
1. Right-click **System Administrator** and then choose **Properties**.  
  
1. In the **System Role Properties** page, select **View report server properties**. Select any other properties you want associated with members of the system administrators role.  
  
1. Select **OK**.  
  
1. Close [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]  
  
1. To add a user to the "system administrator" system role, see the [Web portal Site Settings](#bkmk_configure_site_settings) section earlier in this article.  
  
 Now when you open [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and don't explicitly select **Run as administrator** you have access to the report server properties.  
  
##  <a name="bkmk_configure_ssdt"></a> To configure SQL Server Data Tools (SSDT) to publish to a local report server  
 If you installed [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] on one of the operating systems listed in the first section of this article, you might experience permission errors. This result occurs when you want SSDT to interact with a local Native mode report server. It happens unless you open [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] with elevated permissions or configure reporting services roles. For example, if you don't have sufficient permissions, you experience issues similar to the following example:  
  
-   When you attempt to deploy report items to the local report server, you see an error message similar to the following in the **Error List** window:  
  
    -   The permissions granted to user `Domain\<user name>` are insufficient for performing this operation.  
  
## Run with elevated permissions each time you open SSDT  
  
1. From the Start menu, select **Microsoft SQL Server**, and then right-click **SQL Server Data Tools**. Select **Run as administrator**  
  
2. Select **Yes** when prompted to continue.  
  
Now you can deploy reports and other items to a local report server.  
  
## Configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] role assignments 

Follow the steps in the [Web portal folder settings](#bkmk_configure_folder_settings) and [Web portal Site Settings](#bkmk_configure_site_settings) sections earlier in this article.  
  
##  <a name="bkmk_addiitonal_informaiton"></a> Additional information  
 Another and common configuration step related to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] administration is to open port 80 in Windows Firewall to allow access to the report server computer. For instructions, see [Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).  
  
## Related content
 [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)  
  
  
