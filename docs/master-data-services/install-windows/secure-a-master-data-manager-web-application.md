---
title: Secure a Master Data Manager Web Application
description: In SQL Server, you can secure the Master Data Manager web application with HTTPS. You must be an administrator and MDS must be installed on the web server.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: e360ba3a-e96b-4f85-b588-ed1f767fa973
author: CordeliaGrey
ms.author: jiwang6
---
# Secure a Master Data Manager Web Application

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  You can secure the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application with HTTPS.  
  
> [!NOTE]  
>  The [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application can use either HTTP or HTTPS, but not both.  
  
## Prerequisites  
 To perform the procedure:  
  
-   You must be an administrator on the web server where [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] is installed.  
  
-   MDS must be installed on the web server, and a web application must exist. For more information, see [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md) and [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](../../master-data-services/install-windows/create-a-master-data-manager-web-application-master-data-services.md).  

- [IIS Extended Protection for Windows authentication](/iis/configuration/system.webserver/security/authentication/windowsauthentication/extendedprotection/) should not be enabled. 

- Configure the web server to listen on all available IP addresses. Do not configure the Web server to listen on a specific IP address. 

### To secure the Master Data Manager web application with HTTPS  
  
1.  After you have confirmed that the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application is configured correctly with HTTP, create a certificate in IIS. For more information, see [Configuring Server Certificates in IIS 7](https://technet.microsoft.com/library/cc732230\(WS.10\).aspx).  
  
2.  In the **Connections** pane, under **Sites**, click the site that hosts the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application.  
  
3.  In the **Actions** pane, click **Bindings**.  
  
4.  Click **Add**.  
  
5.  From the list, select **https**.  
  
6.  Select the TLS/SSL certificate.  
  
7.  Click **OK**.  
  
8.  Optional. To remove HTTP so that users can access the site with HTTPS only, from the list, click the row with **http**. Click **Remove** and on the confirmation dialog box, click **Yes**.  
  
    > [!IMPORTANT]  
    >  You must change basicHttp and wsHttpBinding configurations after removing HTTP.  
  
9. To close the **Site Bindings** dialog box, click **Close**.  
  
10. Now open the web.config file from *drive*:\Program Files\Microsoft SQL Server\130\Master Data Services\WebApplication.  
  
11. Find the string `<security mode="Message">` and change it to `<security mode="Transport">`.  

12. Change `<serviceMetadata httpGetEnable="true" httpsGetEnabled="false">` to `<serviceMetadata httpGetEnable="false" httpsGetEnabled="true">` to prevent issues that may appear in the Silverlight client.

13. Save and close the file. If you get an error, it could be because you have UAC enabled. Users should now be able to use HTTPS to access the site.  

  
## See Also  
 [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](../../master-data-services/install-windows/create-a-master-data-manager-web-application-master-data-services.md)  
  
  
