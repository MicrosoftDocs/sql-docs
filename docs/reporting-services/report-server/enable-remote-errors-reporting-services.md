---
title: "Enable remote errors (Reporting Services)"
description: Learn how to set server properties on a Reporting Services report server to return additional information about error conditions that occur on remote servers.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/20/2017
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "remote data source [Reporting Services]"
  - "EnableRemoteError server property"
---
# Enable remote errors (Reporting Services)
  You can set server properties on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server to return additional information about error conditions that occur on remote servers. If an error message contains the text, "For more information about this error, navigate to the report server on the local server machine, or enable remote errors," you can set the `EnableRemoteErrors` property to access additional information that can help you troubleshoot the problem. For more information, see [Report server system properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md).  
  
 In this article:  
  
-   [Enable remote errors for SharePoint mode](#bkmk_sharepoint)  
  
-   [Enable remote errors through SQL Server Management Studio (native mode)](#bkmk_mgtStudio)  
  
-   [Enable remote errors through script (native mode)](#bkmk_script)  
  
-   [Modify the ConfigurationInfo table (native mode)](#bkmk_ConfigurationInfo)  
  
##  <a name="bkmk_sharepoint"></a> Enable remote errors for SharePoint mode  
 There are two different procedures for enabling remote errors for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode. The procedure is different for the two different report server architectures. The newer SharePoint service based architecture that was introduced in the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, utilizes a setting that can be configured for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. The older architecture utilizes a single site level setting.  
  
### Enable remote errors for a Reporting Services service application  
  
1.  For a SharePoint mode report server installed with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or a newer version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], enable the service application setting **Enable remote errors**. The setting can be configured for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
1.  In SharePoint Central Administration, select **Manage service applications** in the **Application Management** group.  
  
1.  Find your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application and choose the name of your service application.  
  
1.  Select **System Settings**.  
  
1.  Choose **Enable Remote Errors** in the **Security** section.  
  
1.  Select **OK**.  
  
### Enable remote errors for a SharePoint site  
  
1.  For a SharePoint mode report server installed with a version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], enable the site setting **Enable remote errors in local mode**.  
  
1.  In **Site Actions**, choose **Site Settings** for the site you want to modify.  
  
1.  Select **Reporting Services Site Settings** in the **Reporting Services** group.  
  
1.  Choose **Enable remote errors in local mode**.  
  
1.  Select **OK**.  
  
##  <a name="bkmk_mgtStudio"></a> Enable remote errors through SQL Server Management Studio (native mode)  
  
1.  Start Management Studio and connect to a report server instance. For more information, see [Connect to a report server in Management Studio](../../reporting-services/tools/connect-to-a-report-server-in-management-studio.md).  
  
1.  Right-click the report server node, and select **Properties**.  
  
1.  Select **Advanced** to open the properties page. For more information, see [Server Properties &#40;Advanced Page&#41; - Reporting Services](../../reporting-services/tools/server-properties-advanced-page-reporting-services.md).  
  
1.  In the **Security** section, for **EnableRemoteErrors**, select **True**.  
  
1.  Select **OK**.
  
##  <a name="bkmk_script"></a> Enable remote errors through script (native mode)  
  
1.  Create a text file and copy the following script into the file.  
  
    ```  
    Public Sub Main()  
      Dim P As New [Property]()  
      P.Name = "EnableRemoteErrors"  
      P.Value = True  
      Dim Properties(0) As [Property]  
      Properties(0) = P  
      Try  
        rs.SetSystemProperties(Properties)  
        Console.WriteLine("Remote errors enabled.")  
      Catch SE As SoapException  
        Console.WriteLine(SE.Detail.OuterXml)  
      End Try  
    End Sub  
    ```  
  
1.  Save the file as `EnableRemoteErrors.rss`.  
  
1.  Select **Start**, point to **Run**, enter **cmd**, and select **OK** to open a command prompt window.  
  
1.  Navigate to the directory that contains the .rss file you created.  
  
1.  Type the following command line, replacing *servername* with the actual name of your server:  
  
    ```  
    rs -i EnableRemoteErrors.rss -s https://servername/ReportServer  
    ```  
  
1.  For more information, see [RS.exe utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md).  
  
##  <a name="bkmk_ConfigurationInfo"></a> Modify the ConfigurationInfo table (native mode)  
  
> [!NOTE]  
>  You can edit the `ConfigurationInfo` table in the report server database to set `EnableRemoteErrors` to **True**, but if the report server is actively used, you should use SQL Server Management Studio or script to modify the settings. If you modify the setting in the database, you need to restart the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service before the changes take effect.  
  
  
