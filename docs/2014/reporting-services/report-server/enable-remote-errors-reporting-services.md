---
title: "Enable Remote Errors (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "remote data source [Reporting Services]"
  - "EnableRemoteError server property"
ms.assetid: 5f05022b-d557-43e0-b50a-f5e2a1846b83
author: markingmyname
ms.author: maghan
manager: craigg
---
# Enable Remote Errors (Reporting Services)
  You can set server properties on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server to return additional information about error conditions that occur on remote servers. If an error message contains the text "For more information about this error, navigate to the report server on the local server machine, or enable remote errors", you can set the `EnableRemoteErrors` property to access additional information that can help you troubleshoot the problem. For more information, see [Report Server System Properties](../report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 **In this topic:**  
  
-   [Enable Remote Errors for SharePoint Mode](#bkmk_sharepoint)  
  
-   [Enable remote errors through SQL Server Management Studio (Native Mode)](#bkmk_mgtStudio)  
  
-   [Enable remote errors through script (Native Mode)](#bkmk_script)  
  
-   [Modifying the ConfigurationInfo table (Native Mode)](#bkmk_ConfigurationInfo)  
  
##  <a name="bkmk_sharepoint"></a> Enable Remote Errors for SharePoint Mode  
 There are two different procedures for enabling remote errors for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode. The procedure is different for the two different report server architectures. The newer SharePoint service based architecture that was introduced in the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] release, utilizes a setting that can be configured for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. The older architecture utilizes a single site level setting.  
  
#### Enable Remote errors for a Reporting Services Service Application  
  
1.  For a SharePoint mode report server installed with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] or a newer version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], enable the service application setting **Enable remote errors**. The setting can be configured for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
2.  In SharePoint Central Administration, click **Manage service applications** in the **Application Management** group.  
  
3.  Find your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application and click the name of your service application.  
  
4.  Click **System Settings**.  
  
5.  Click **Enable Remote Errors** in the **Security** section.  
  
6.  Click **OK**.  
  
#### Enable Remote Errors for a SharePoint Site  
  
1.  For a SharePoint mode report server installed with a version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] prior to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], enable the site setting **Enable remote errors in local mode**.  
  
2.  In **Site Actions** click **Site Settings** for the site you want to modify.  
  
3.  Click **Reporting Services Site Settings** in the **Reporting Services** group.  
  
4.  Click **Enable remote errors in local mode**.  
  
5.  Click **OK**  
  
##  <a name="bkmk_mgtStudio"></a> Enable remote errors through SQL Server Management Studio (Native Mode)  
  
1.  Start Management Studio and connect to a report server instance. For more information, see [Connect to a Report Server in Management Studio](../tools/connect-to-a-report-server-in-management-studio.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
2.  Right-click the report server node, and select **Properties**.  
  
3.  Click **Advanced** to open the properties page. For more information, see [Server Properties &#40;Advanced Page&#41; - Reporting Services](../tools/server-properties-advanced-page-reporting-services.md)in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
4.  In `EnableRemoteErrors`, select `True`.  
  
5.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="bkmk_script"></a> Enable remote errors through script (Native Mode)  
  
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
  
2.  Save the file as EnableRemoteErrors.rss.  
  
3.  Click **Start**, point to **Run**, type **cmd**, and click **OK** to open a command prompt window.  
  
4.  Navigate to the directory that contains the .rss file you just created.  
  
5.  Type the following command line, replacing *servername* with the actual name of your server:  
  
    ```  
    rs -i EnableRemoteErrors.rss -s http://servername/ReportServer  
    ```  
  
6.  For more information, see [RS.exe Utility &#40;SSRS&#41;](../tools/rs-exe-utility-ssrs.md)  
  
##  <a name="bkmk_ConfigurationInfo"></a> Modifying the ConfigurationInfo table (Native Mode)  
  
1.  > [!NOTE]  
    >  You can edit the **ConfigurationInfo** table in the report server database to set `EnableRemoteErrors` to `True`, but if the report server is actively used, you should use SQL Server Management Studio or script to modify the settings. If you modify the setting in the database, you need to restart the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service before the changes take effect.  
  
  
