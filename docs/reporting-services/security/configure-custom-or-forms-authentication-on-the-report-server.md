---
title: "Configure Custom or Forms Authentication on the Report Server | Microsoft Docs"
ms.date: 04/18/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: security


ms.topic: conceptual
helpviewer_keywords: 
  - "Forms authentication, configuring"
  - "custom authentication [Reporting Services]"
ms.assetid: e8601a8f-e66d-4649-8e4d-a46ca20ec7d0
author: markingmyname
ms.author: maghan
---
# Configure Custom or Forms Authentication on the Report Server

Reporting Services provides an extensible architecture that allows you to plug in custom or forms-based authentication modules. You might consider implementing a custom authentication extension if deployment requirements do not include Windows integrated security or Basic authentication. The most common scenario for using custom authentication is to support Internet or extranet access to a Web application. Replacing the default Windows Authentication extension with a custom authentication extension gives you more control over how external users are granted access to the report server.  

In practice, deploying a custom authentication extension requires multiple steps that include copying assemblies and application files, modifying configuration files, and testing. This topic focuses on just the authentication settings that you specify in the configuration files.  

> [!NOTE]
>  Creating a custom authentication extension requires custom code and expertise in [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] security. If you do not want to create a custom authentication extension, you can use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Active Directory groups and accounts, but you should greatly reduce the scope of a report server deployment. For more information about custom authentication, see [Implementing a Security Extension](../../reporting-services/extensions/security-extension/implementing-a-security-extension.md).

Additionally, if you want to use Forms authentication or a custom authentication extension in a SQL Server Reporting Services environment that is integrated with a SharePoint product, you must configure the SharePoint site to use the authentication method that you choose. For more information about configuring authentication in SharePoint, see [Authentication Samples](https://go.microsoft.com/fwlink/?LinkId=115575) on [!INCLUDE[msCoName](../../includes/msconame-md.md)] Developer Network (MSDN).



### To configure a report server to use Custom authentication

1.  Open RSReportServer.config in a text editor.

2.  Find \<**Authentication**>.

3.  Copy the following XML structure:

    ```
    <Authentication>
          <AuthenticationTypes>
                 <Custom />
          </AuthenticationTypes>
          <EnableAuthPersistence>true</EnableAuthPersistence>
    </Authentication>
    ```

4.  Paste it over the existing entries for \<**Authentication**>.

     Note that you cannot use **Custom** with other authentication types.

5.  Save the file.

6.  Open the Web.config file for the report server. By default, it is located at \Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\ReportServer.

7.  Find **authentication mode** and set it **Forms**.

    ```
    <authentication mode = "Forms" />
    ```

8.  Find **identity impersonate** and set it to **False**.

    ```
    <identity impersonate = "false" />  
    ```
9. Add the **PassThroughCookies** element structure to the configuration file. For more information, see [Configure the Web Portal to Pass Custom Authentication Cookies](../../reporting-services/security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)
  
10. Save the file.  
  
11. If you configured a scale-out deployment, repeat all of the previous steps for other report servers in the deployment.  
  
12. Restart the report server to clear any sessions that are currently open.  

## See Also

[Implementing a Security Extension](../../reporting-services/extensions/security-extension/implementing-a-security-extension.md)  
[Reporting Services Custom Security Sample (GitHub)](https://github.com/Microsoft/Reporting-Services/tree/master/CustomSecuritySample)  
[Authentication with the Report Server](../../reporting-services/security/authentication-with-the-report-server.md)   
[RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[Configure Basic Authentication on the Report Server](../../reporting-services/security/configure-basic-authentication-on-the-report-server.md)   
[Configure Windows Authentication on the Report Server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)  
More questions? [Try the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
