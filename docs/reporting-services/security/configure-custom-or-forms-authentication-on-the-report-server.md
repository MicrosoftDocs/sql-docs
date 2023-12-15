---
title: "Configure custom or forms authentication on the report server"
description: "Configure custom or forms authentication on the report server"
author: maggiesMSFT
ms.author: maggies
ms.date: 11/11/2023
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Forms authentication, configuring"
  - "custom authentication [Reporting Services]"
---
# Configure custom or forms authentication on the report server

Reporting Services provides an extensible architecture that allows you to plug in custom or forms-based authentication modules. You might consider implementing a custom authentication extension if deployment requirements don't include Windows integrated security or Basic authentication. The most common scenario for using custom authentication is to support Internet or extranet access to a Web application. Replacing the default Windows Authentication extension with a custom authentication extension gives you more control over how external users are granted access to the report server.  

In practice, deploying a custom authentication extension requires multiple steps that include copying assemblies and application files, modifying configuration files, and testing. This article focuses on just the authentication settings that you specify in the configuration files.  

> [!NOTE]
>  Creating a custom authentication extension requires custom code and expertise in [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] security. If you do not want to create a custom authentication extension, you can use [!INCLUDE[msCoName](../../includes/msconame-md.md)] Entry ID groups and accounts, but you should greatly reduce the scope of a report server deployment. For more information about custom authentication, see [Implement a security extension](../../reporting-services/extensions/security-extension/implementing-a-security-extension.md).

Additionally, you can use Forms authentication or a custom authentication extension in a SQL Server Reporting Services environment that is integrated with a SharePoint product. To do so, you must configure the SharePoint site to use the authentication method that you choose. For more information about configuring authentication in SharePoint, see [Authentication samples](/previous-versions/office/sharepoint-2007-products-and-technologies/cc262069(v=office.12)) on [!INCLUDE[msCoName](../../includes/msconame-md.md)] Developer Network (MSDN).



### Configure a report server to use custom authentication

1.  Open the rsreportserver.config in a text editor.

2.  Find the `<Authentication>` section.

3.  Copy the following XML structure:

    ```
    <Authentication>
          <AuthenticationTypes>
                 <Custom />
          </AuthenticationTypes>
          <EnableAuthPersistence>true</EnableAuthPersistence>
    </Authentication>
    ```

4.  Paste it over the existing entries for the `<Authentication>` section.

     You can't use ``Custom`` with other authentication types.

5.  Save the file.

6.  Open the Web.config file for the report server. By default, the file is located in the same folder as the rsreportserver.config file (see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md#bkmk_file_location)).

7.  Find `authentication mode` and set it to `Forms`.

    ```
    <authentication mode = "Forms" />
    ```

8.  Find `identity impersonate` and set it to `False`.

    ```
    <identity impersonate = "false" />  
    ```
9. Add the `PassThroughCookies` element structure to the configuration file. For more information, see [Configure the web portal to pass custom authentication cookies](../../reporting-services/security/configure-the-web-portal-to-pass-custom-authentication-cookies.md)
  
10. Save the file.  
  
11. If you configured a scale-out deployment, repeat all of the previous steps for other report servers in the deployment.  
  
12. Restart the report server to clear any sessions that are currently open.  

## Related content

[Implement a security extension](../../reporting-services/extensions/security-extension/implementing-a-security-extension.md)  
[Reporting Services custom security sample (GitHub)](https://github.com/Microsoft/Reporting-Services/tree/master/CustomSecuritySample)  
[Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)   
[RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[Configure Basic authentication on the report server](../../reporting-services/security/configure-basic-authentication-on-the-report-server.md)   
[Configure Windows Authentication on the Report Server](../../reporting-services/security/configure-windows-authentication-on-the-report-server.md)  
More questions? [Try the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
