---
description: "Configure Basic Authentication on the Report Server"
title: "Configure Basic Authentication on the Report Server | Microsoft Docs"
ms.date: 02/10/2021
ms.service: reporting-services
ms.subservice: security


ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, configuration"
  - "Basic authentication"
ms.assetid: 8faf2938-b71b-4e61-a172-46da2209ff55
author: maggiesMSFT
ms.author: maggies
---
# Configure Basic Authentication on the Report Server
  By default, Reporting Services accepts requests that specify Negotiate and NTLM authentication. If your deployment includes client applications or browsers that use Basic authentication, you must add Basic authentication to the list of supported types. In addition, if you want to use Report Builder, you must enable Anonymous access to the Report Builder files.  
  
 To configure Basic authentication on the report server, you edit XML elements and values in the RSReportServer.config file. You can copy and paste the examples in this topic to replace the default values.  
  
 Before you enable Basic authentication, verify that your security infrastructure supports it. Under Basic authentication, the Report Server Web service will pass credentials to the local security authority. If the credentials specify a local user account, the user is authenticated by the local security authority on the report server computer and the user will get a security token that is valid for local resources. Credentials for domain user accounts are forwarded to and authenticated by a domain controller. The resulting ticket is valid for network resources.  
  
 Channel encryption, such as Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), is required if you want to mitigate the risk of having credentials intercepted while in transit to a domain controller in your network. By itself, Basic authentication transmits the user name in clear text and the password in base-64 encoding. Adding channel encryption makes the packet unreadable. For more information, see [Configure TLS Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).  
  
 After you enable Basic authentication, be aware that users cannot select the **Windows integrated security** option when setting connection properties to an external data source that provides data to a report. The option will be grayed out in the data source property pages.  
  
> [!NOTE]  
>  The following instructions are intended for a native mode report server. If the report server is deployed in SharePoint integrated mode, you must use the default authentication settings that specify Windows integrated security. The report server uses internal features in the default Windows Authentication extension to support report server in SharePoint integrated mode.  
  
### To configure a report server to use Basic authentication  
  
1. Open RSReportServer.config in a text editor.  
  
     To find the config file, see the [File Location](../report-server/rsreportserver-config-configuration-file.md#bkmk_file_location) section in the "RsReportServer.config Configuration File" article.
  
2. Find \<**Authentication**>.  
  
3. Copy one of the following XML structures that best fits your needs. The first XML structure provides placeholders for specifying all of the elements, which are described in the next section:  

    [!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016](../../includes/ssrs-appliesto-2016.md)]

    ```xml
    <Authentication>  
          <AuthenticationTypes>  
                 <RSWindowsBasic>  
                       <LogonMethod>3</LogonMethod>  
                       <Realm></Realm>  
                       <DefaultDomain></DefaultDomain>  
                 </RSWindowsBasic>  
          </AuthenticationTypes>  
          <EnableAuthPersistence>true</EnableAuthPersistence>  
    </Authentication>  
    ```  
  
    If you are using default values, you can copy the minimum element structure:  
  
    ```xml
          <AuthenticationTypes>  
                 <RSWindowsBasic/>  
          </AuthenticationTypes>  
    ```  

    [!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2017-and-later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirs](../../includes/ssrs-appliesto-pbirs.md)]

    ```xml
      <Authentication>
          <AuthenticationTypes>
                      <RSWindowsBasic/>
          </AuthenticationTypes>
          <EnableAuthPersistence>true</EnableAuthPersistence>
      <RSWindowsExtendedProtectionLevel>Off</RSWindowsExtendedProtectionLevel>
      <RSWindowsExtendedProtectionScenario>Any</RSWindowsExtendedProtectionScenario>
      </Authentication>
    ```

4. Paste it over the existing entries for \<**Authentication**>.  
  
     If you are using multiple authentication types, add just the **RSWindowsBasic** element but do not delete the entries for **RSWindowsNegotiate**, **RSWindowsNTLM**, or **RSWindowsKerberos**.  
  
     Note that you cannot use **Custom** with other authentication types.  
  
5. Replace empty values for \<**Realm**> or \<**DefaultDomain**> with values that are valid for your environment.  
  
6. Save the file.  
  
7. If you configured a scale-out deployment, repeat these steps for other report servers in the deployment.  
  
8. Restart the report server to clear any sessions that are currently open.  
  
## RSWindowsBasic Reference  
 The following elements can be specified when configuring Basic authentication.  
  
|Element|Required|Valid Values|  
|-------------|--------------|------------------|  
|LogonMethod|Yes<br /><br /> If you do not specify a value, 3 will be used.|**2** = Network logon, intended for high performance servers to authenticate plain text passwords.<br /><br /> **3** = Cleartext logon, which preserves logon credentials in the authentication package that is sent with each HTTP request, allowing the server to impersonate the user when connecting to other servers in the network. (Default)<br /><br /> Note: Values 0 (for interactive logon) and 1 (for batch logon) are **NOT** supported in [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)].|  
|Realm|Optional|Specifies a resource partition that includes authorization and authentication features used to control access to protected resources in your organization.|  
|DefaultDomain|Optional|Specifies the domain used by the server to authenticate the user. This value is optional, but if you omit it the report server will use the computer name as the domain. If the computer is a member of domain, that domain is the default domain. If you installed the report server on a domain controller, the domain used is the one controlled by the computer.|  
  
## See Also  
 [Application Domains for Report Server Applications](../../reporting-services/report-server/application-domains-for-report-server-applications.md)   
 [Reporting Services Security and Protection](../../reporting-services/security/reporting-services-security-and-protection.md)  
  
  
