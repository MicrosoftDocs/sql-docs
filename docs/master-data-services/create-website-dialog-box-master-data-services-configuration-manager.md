---
description: "Create Website Dialog Box (Master Data Services Configuration Manager)"
title: Create Website Dialog Box
ms.custom: "seo-lt-2019"
ms.date: "03/20/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.mds.configmanager.createsite.f1"
ms.assetid: 179c9c1e-3b06-421b-b71b-1cb64d104f5e
author: CordeliaGrey
ms.author: jiwang6
---
# Create Website Dialog Box (Master Data Services Configuration Manager)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Use the **Create Website** dialog box to create a new website on the local computer. When you create a website in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)], the site is added to Internet Information Services (IIS) on the local computer with a root application that is configured as the [!INCLUDE[ssMDSmdm](../includes/ssmdsmdm-md.md)] web application. A new application pool is also created and the web application is placed in that application pool.  
  
## Web Site  
  
|Control Name|Description|  
|------------------|-----------------|  
|**Website name**|Type a name for the website or use the default name. This name is a friendly name that is used only to identify the site in IIS. It is not used to access the site from a web browser.<br /><br /> The name must be unique among all the sites in IIS on the local computer.|  
|**Protocol**|Displays **http**. Use hypertext transfer protocol (HTTP) when you do not require client and server communication to take place over an encrypted channel.<br /><br /> **Note**: You cannot create an HTTPS site in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)]. HTTPS is the HTTP protocol using Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), and it is useful when exchanging confidential or personal data, or when you want users to confirm the identity of the server before transmitting personal information. If you require information to be transferred between the server and a client over an encrypted channel, you must use an IIS tool, such as IIS Manager, to configure the site with an HTTPS binding and to associate the website binding with a server certificate; this is required before you can successfully open the website in a web browser. For more information about server certificates, see [Configuring Server Certificates in IIS 7](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc732230(v=ws.10)) on [!INCLUDE[msCoName](../includes/msconame-md.md)] TechNet.|  
|**IP address**|Select an IP address that users can use to access the site. By default, **All Unassigned** is selected. Unless you have a reason to use a specific IPv4 or IPv6 address, use the default value.<br /><br /> With **All Unassigned**, this site responds to requests for all IP addresses on the port and optional host name that you specify. If another site on the server has a binding on the same port but with a specific IP address, that site receives HTTP requests to that port and specific IP address, and the site with the **All Unassigned** IP address receives all other HTTP requests to that port and the other IP addresses.|  
|**Port**|Type the port for requests made to this website. If you select the HTTP protocol, the default port is 80. If you specify a port different from the default ports, clients must specify the port number in order to connect to the website.<br /><br /> **Note**: The **Default Website** in IIS is configured to use the HTTP protocol on port 80 with all unassigned IP addresses. If you attempt to create the website in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)] with the default binding information, you receive an error that a duplicate binding exists. You must either change the binding information for the site in [!INCLUDE[ssMDScfgmgr](../includes/ssmdscfgmgr-md.md)], or change the binding information for the Default Website by using an IIS tool, such as IIS Manager. Alternately, you can specify a host header to enable IIS to uniquely identify the website. Make sure that you configure your firewall to accept traffic over the port you specify.|  
|**Host header**|Optional value. Type a host header name. Use this when you want to assign a host name, also known as a domain name, to a computer that uses a single IP address or port. When you specify a host name, clients must use that name instead of the IP address to access the website. When you configure a host name, you cannot open the site in a web browser until your DNS server has an entry for that host name.<br /><br /> For example, if you want users to access your site at `https://www.contoso.com/`, you must specify www.contoso.com as the host name and the DNS server must have an entry for it.<br /><br /> If your website is available on an intranet, you do not have to specify a host name if users type the server name in a browser, for example, `https://server_name`. However, if the DNS server in your environment is configured to store other names for this web server, you can create a separate binding for each host name so that users can use the other names stored by the DNS server. If you must configure more than one host name for your site, use an IIS tool, such as IIS Manager to add additional site bindings.|  
  
## Application Pool  
  
|Control Name|Description|  
|------------------|-----------------|  
|**Name**|Type a unique, friendly name for a new application pool, or use the default name that is provided. The root web application for this website runs in this application pool.<br /><br /> Application pools provide boundaries that prevent applications in one application pool from affecting applications in another application pool.|  
|**User name**|Type a domain and user name from Active Directory. This account is the identity of the application pool that the web application runs in.<br /><br /> This account is added to the mds_exec database role in the [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] database for database access. For more information, see [Database Logins, Users, and Roles &#40;Master Data Services&#41;](../master-data-services/database-logins-users-and-roles-master-data-services.md). It is also added to a [!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] Windows group, **MDS_ServiceAccounts**, which is granted permission to the temporary compilation directory, **MDSTempDir**, in the file system. For more information, see [Folder and File Permissions &#40;Master Data Services&#41;](../master-data-services/folder-and-file-permissions-master-data-services.md).|  
|**Password**|Type the password for the specified user account.|  
|**Confirm password**|Retype the password for the specified user account. The **Password** and **Confirm password** fields must contain the same password.|  
  
## See Also  
 [Web Configuration Page &#40;Master Data Services Configuration Manager&#41;](../master-data-services/web-configuration-page-master-data-services-configuration-manager.md)   
[Master Data Services Installation and Configuration](../master-data-services/master-data-services-installation-and-configuration.md)
 [Web Application Requirements &#40;Master Data Services&#41;](../master-data-services/install-windows/web-application-requirements-master-data-services.md)   
 [Create a Master Data Manager Web Application &#40;Master Data Services&#41;](../master-data-services/install-windows/create-a-master-data-manager-web-application-master-data-services.md)  
  
