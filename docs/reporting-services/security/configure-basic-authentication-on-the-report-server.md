---
title: Configure Basic authentication on the report server
description: See how to configure the report server to support Basic authentication. View configuration file entries to use to configure Basic authentication options.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/23/2024
ms.service: reporting-services
ms.subservice: security
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Reporting Services, configuration"
  - "Basic authentication"

#customer intent: As a SQL Server administrator, I want to configure Basic authentication on my report server so that I can accept requests that specify Basic authentication.
---
# Configure Basic authentication on the report server

By default, SQL Server Reporting Services (SSRS) accepts requests that specify Negotiate and New Technology LAN Manager (NTLM) authentication. If your deployment includes client applications or browsers that use Basic authentication, you must add Basic authentication to the list of supported types. Also, if you want to use Report Builder, you must enable Anonymous access to the Report Builder files.

To configure Basic authentication on the SSRS report server, you edit XML elements and values in the RSReportServer.config file. You can copy and paste the examples in this article to replace the default values. After you enable Basic authentication, users can't select the **Windows integrated security** option when they set connection properties for an external data source that provides data to a report. The option isn't available in the data source property pages.

## Prerequisites

- A configured native mode report server
- Write permissions for the RSReportServer.config file

## Security considerations for Basic authentication

Before you enable Basic authentication, verify that your security infrastructure supports it. Under Basic authentication, the report server web service passes credentials to the local security authority. If the credentials specify a local user account, the local security authority on the report server authenticates the user. The user then gets a security token that's valid for local resources. Credentials for domain user accounts are forwarded to and authenticated by a domain controller. The resulting ticket is valid for network resources.

Channel encryption, such as Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), is required if you want to mitigate the risk of having credentials intercepted while in transit to a domain controller in your network. By itself, Basic authentication transmits the user name in clear text and the password in base-64 encoding. Adding channel encryption makes the packet unreadable. For more information, see [Configure TLS Connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).

> [!NOTE]
> The following instructions are intended for a native mode report server. If the report server is deployed in SharePoint integrated mode, you must use the default authentication settings that specify Windows integrated security. The report server uses internal features in the default Windows Authentication extension to support the report server in SharePoint integrated mode.
  
## Configure a report server to use Basic authentication
  
1. Open the RSReportServer.config configuration file in a text editor. For information about the location of your configuration file, see [RsReportServer.config configuration file](../report-server/rsreportserver-config-configuration-file.md#file-location).
  
1. In the file, go to the `<Authentication>` line.

1. Review the following XML structures, and copy the one that best fits your needs. The first XML structure provides placeholders for the `Realm` and `DefaultDomain` elements, which are described in the next section.

   [!INCLUDE [SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE [SSRS applies to 2016](../../includes/ssrs-appliesto-2016.md)]

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

   If you use default values, you can use the following structure, which minimizes the number of elements:

   ```xml
   <AuthenticationTypes>
         <RSWindowsBasic/>
   </AuthenticationTypes>
   ```

   [!INCLUDE [SSRS applies to](../../includes/ssrs-appliesto.md)] [!INCLUDE [SSRS applies to 2017 and later](../../includes/ssrs-appliesto-2017-and-later.md)] [!INCLUDE [SSRS applies to Power BI Reporting Service](../../includes/ssrs-appliesto-pbirs.md)]

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

1. In your configuration file, replace the existing `<Authentication>` section with the structure that you copied.

   If you use multiple authentication types, add the `RSWindowsBasic` element but don't delete the entries for `RSWindowsNegotiate`, `RSWindowsNTLM`, or `RSWindowsKerberos`.

   You can't use the `Custom` authentication type with other authentication types.

1. Replace the empty `<Realm>` and `<DefaultDomain>` values with values that are valid for your environment. For appropriate values, see the next section.

1. Save the file.

1. If you use a scale-out deployment, repeat these steps for other report servers in the deployment.

1. Restart all report servers that you configured for Basic authentication. This step clears any sessions that are currently open.

## Values for Basic authentication elements

You can specify the following elements when you use a `RSWindowsBasic` section to configure Basic authentication.

|Element|Required|Valid values|
|-------------|--------------|------------------|
|LogonMethod|Yes<br /><br /> If you don't specify a value, 3 is used.|Use a value of **2** for a network sign-in. Use this value for high performance servers to authenticate plaintext passwords.<br /><br />Use a value of **3** for a clear-text sign-in. When you use this value, which is the default value, sign-in credentials are preserved in the authentication package that's sent with each HTTP request. The server then impersonates the user when it connects to other servers in the network.<br /><br />Note: Values **0** (for interactive sign-in) and **1** (for batch sign-in) aren't supported in [!INCLUDE[SSRS current version](../../includes/ssrscurrent-md.md)].|
|Realm|Optional|This element specifies a resource partition that includes authorization and authentication features that are used to control access to protected resources in your organization.|
|DefaultDomain|Optional|This element specifies the domain that the server uses to authenticate the user. This value is optional, but if you omit it, the report server uses the computer name as the domain. If the computer is a member of a domain, that domain is the default domain. If you install the report server on a domain controller, the domain that's used is the one that's controlled by the computer.|

## Related content

- [Application domains for report server applications](../../reporting-services/report-server/application-domains-for-report-server-applications.md)
- [Reporting Services security and protection](../../reporting-services/security/reporting-services-security-and-protection.md)
  