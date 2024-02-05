---
title: "Configure Windows authentication on the report server"
description: "Configure Windows Authentication on the Report Server"
author: maggiesMSFT
ms.author: maggies
ms.reviewer: randolphwest
ms.date: 08/21/2023
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Windows authentication [Reporting Services]"
  - "Reporting Services, configuration"
---
# Configure Windows authentication on the report server

By default, [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] accepts requests that specify Negotiate or NTLM authentication. If your deployment includes client applications and browsers that use these security providers, you can use the default values without other configuration. Let's say you want to use a different security provider for Windows integrated security, or if you modify the default values and want to restore the original settings. You can use the information in this article to specify authentication settings on the report server.

To use Windows integrated security, each user who requires access to a report server must have a valid Windows local or domain user account. Or, they must be a member of a Windows local or domain group account. You can include accounts from other domains as long as those domains are trusted. The accounts must have access to the report server computer, and must be then assigned to roles in order to gain access to specific report server operations.

The following requirements must also be met:

- The RSReportServer.config files must have `AuthenticationType` set to `RSWindowsNegotiate`, `RSWindowsKerberos`, or `RSWindowsNTLM`. By default, the RSReportServer.config file includes the `RSWindowsNegotiate` setting if the Report Server service account is either NetworkService or LocalSystem; otherwise, the `RSWindowsNTLM` setting is used. You can add `RSWindowsKerberos` if you have applications that only use Kerberos authentication.

  > [!IMPORTANT]  
  > When you use `RSWindowsNegotiate`, a Kerberos authentication error occurs if you configured the Report Server service to run under a domain user account and you didn't register a Service Principal Name (SPN) for the account. For more information, see [Resolving Kerberos authentication errors when connecting to a report server](#proxyfirewallRSWindowsNegotiate) in this topic.

- [!INCLUDE [vstecasp](../../includes/vstecasp-md.md)] must be configured for Windows Authentication. By default, the Web.config files for the Report Server Web service include the `<authentication mode="Windows">` setting. If you change it to `<authentication mode="Forms">`, the Windows Authentication for [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] fails.

- The Web.config files for the Report Server Web service must have `<identity impersonate= "true" />`.
- The client application or browser must support Windows integrated security.
- The [!INCLUDE [ssRSWebPortal](../../includes/ssrswebportal.md)] doesn't need more configuration.

To change the report server authentication settings, edit the XML elements and values in the RSReportServer.config file. You can copy and paste the examples in this article to implement specific combinations.

The default settings work best if all client and server computers are in the same domain or in a trusted domain. And, the report server is deployed for intranet access behind a corporate firewall. Trusted and single domains are a requirement for passing Windows credentials. Credentials can be passed more than one time if you enable the Kerberos version 5 protocol for your servers. Otherwise, credentials can be passed only one time before they expire. For more information about configuring credentials for multiple computer connections, see [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).

The following instructions are intended for a native mode report server. If the report server is deployed in SharePoint integrated mode, you must use the default authentication settings that specify Windows integrated security. The report server uses internal features in the default Windows Authentication extension to support report servers in SharePoint integrated mode.

## Extended Protection for authentication

Beginning with [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)], support for Extended Protection for Authentication is available. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] feature supports the use of channel binding and service binding to enhance protection of authentication. The [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] features need to be used with an operating system that supports Extended Protection. You can determine [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration for extended protection by specific settings in the RSReportServer.config file. You can update the file by either editing the file or by using WMI APIs. For more information, see [Extended Protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md).

### Configure a report server to use Windows integrated security

1. Open RSReportServer.config in a text editor.

1. Find `<Authentication>`.

1. Copy one of the following XML structures that best fits your needs. You can specify `RSWindowsNegotiate`, `RSWindowsNTLM`, and `RSWindowsKerberos` in any order. You should enable authentication persistence if you want to authenticate the connection rather than each individual request. Under authentication persistence, all requests that require authentication are allowed during the connection.

   The first XML structure is the default configuration when the Report Server service account is either NetworkService or LocalSystem:

   ```xml
   <Authentication>
       <AuthenticationTypes>
           <RSWindowsNegotiate />
       </AuthenticationTypes>
       <EnableAuthPersistence>true</EnableAuthPersistence>
   </Authentication>
   ```

   The second XML structure is the default configuration when the Report Server service account isn't NetworkService or LocalSystem:

   ```xml
   <Authentication>
       <AuthenticationTypes>
               <RSWindowsNTLM />
       </AuthenticationTypes>
       <EnableAuthPersistence>true</EnableAuthPersistence>
   </Authentication>
   ```

   The third XML structure specifies all of the security packages that are used in Windows integrated security:

   ```xml
   <AuthenticationTypes>
       <RSWindowsNegotiate />
       <RSWindowsKerberos />
       <RSWindowsNTLM />
   </AuthenticationTypes>
   ```

   The fourth XML structure specifies NTLM only for deployments that don't support Kerberos or to work around Kerberos authentication errors:

   ```xml
   <AuthenticationTypes>
       <RSWindowsNTLM />
   </AuthenticationTypes>
   ```

1. Paste it over the existing entries for `<Authentication>`.

   You can't use `Custom` with the `RSWindows` types.

1. Modify as appropriate the settings for extended protection. Extended protection is disabled by default.  If these entries aren't present, the current computer might not be running a version of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] which supports extended protection. For more information, see [Extended protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)

   ```xml
   <RSWindowsExtendedProtectionLevel>Allow</RSWindowsExtendedProtectionLevel>
   <RSWindowsExtendedProtectionScenario>Proxy</RSWindowsExtendedProtectionScenario>
   ```

1. Save the file.

1. If you configured a scale-out deployment, repeat these steps for other report servers in the deployment.

1. Restart the report server to clear any sessions that are currently open.

## <a id="proxyfirewallRSWindowsNegotiate"></a> Resolve Kerberos authentication errors when connecting to a Report Server

On a report server that is configured for Negotiate or Kerberos authentication, a client connection to the report server fails if there's a Kerberos authentication error. Kerberos authentication errors are known to occur when:

- The Report Server service runs as a Windows domain user account and you didn't register a Service Principal Name (SPN) for the account.

- The report server is configured with the `RSWindowsNegotiate` setting.

- The browser chooses Kerberos over NTLM in the authentication header in the request it sends to the report server.

You can detect the error if you enabled Kerberos logging. Another symptom of the error is that you're prompted for credentials multiple times and then see an empty browser window.

You can confirm that you're encountering a Kerberos authentication error by removing `<RSWindowsNegotiate>` from your configuration file and reattempting the connection.

After you confirm the problem, you can address it in the following ways:

- Register an SPN for the Report Server service under the domain user account. For more information, see [Register a Service Principal Name (SPN) for a report server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md).

- Change the service account to run under a built-in account such as Network Service. Built-in accounts map HTTP SPN to the Host SPN, which is defined when you join a computer to your network. For more information, see [Configure a service account (Report Server Configuration Manager)](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md).

- Use NTLM. NTLM generally works in cases where Kerberos authentication fails. To use NTLM, remove `RSWindowsNegotiate` from the RSReportServer.config file and verify that only `RSWindowsNTLM` is specified. If you choose this approach, you can continue to use a domain user account for the Report Server service even if you don't define an SPN for it.

To summarize, you should run commands similar to the following example. Replace the values as appropriate.

```cmd
setspn -S HTTP/<SSRS Server FDQN> <SSRS Service Account>
setspn -S HTTP/<host header for Report server web site> <SSRS Service Account>
setspn -S HTTP/<SharePoint Server FDQN> <SharePoint Application Pool Account>
setspn -S HTTP/<host header for SharePoint site>  <SharePoint Application Pool Account>
setspn -S HTTP/Dummy <Claims to Windows Taken Service Account>
```

### Log information

There are several sources of logging information that can help resolve Kerberos related issues.

#### User-Account-Control attribute

Determine if the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service account has the sufficient attribute set in Active Directory. Review the Reporting Services service trace log file to find the value logged for the UserAccountControl attribute. The value logged is in decimal form. You need to convert the decimal value to hexadecimal form and then find that value in the MSDN article describing User-Account-Control Attribute.

- The Reporting Services service trace log entry looks similar to the following example:

  ```output
  appdomainmanager!DefaultDomain!8f8!01/14/2010-14:42:28:: i INFO: The UserAccountControl value for the service account is 590336
  ```

- One option for converting the value Decimal value to hexadecimal form is to us the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows Calculator. Windows Calculator supports several modes that show the `Dec` option and `Hex` options. Select the `Dec` option, paste or type in the decimal value you found in the log file and then select the 'Hex' option.

- Then refer to the article [User-Account-Control Attribute](/windows/win32/adschema/a-useraccountcontrol) to derive the attribute for the service account.

#### SPNs configured in Active Directory for the Reporting Services service account

To log the SPNs in the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service trace log file, you can enable the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] Extended Protection feature temporarily.

- Modify the configuration file **rsreportserver.config** by setting the following:

  ```xml
  <RSWindowsExtendedProtectionLevel>Allow</RSWindowsExtendedProtectionLevel>
  <RSWindowsExtendedProtectionScenario>Any</RSWindowsExtendedProtectionScenario>
  ```

- Restart the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service.

If you don't want to continue to use Extended Protection, then set the configuration values back to defaults and restart the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service account.

```xml
<RSWindowsExtendedProtectionLevel>Off</RSWindowsExtendedProtectionLevel>
<RSWindowsExtendedProtectionScenario>Proxy</RSWindowsExtendedProtectionScenario>
```

For more information, see [Extended Protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md).

#### How the browser chooses Negotiated Kerberos or Negotiated NTLM

When you use Internet Explorer to connect to the report server, it specifies either Negotiated Kerberos or NTLM on the authentication header. NTLM is used instead of Kerberos when:

- The request is sent to a local report server.

- The request is sent to an IP address of the report server computer rather than a host header or server name.

- Firewall software blocks ports used for Kerberos authentication.

- The operating system of a particular server doesn't have Kerberos enabled.

- The domain includes older versions of Windows client and server operating systems that don't support the Kerberos authentication feature built into newer versions of the operating system.

In addition, Internet Explorer might choose either Negotiated Kerberos or NTLM depending on how you configured URL, LAN, and proxy settings.

#### Report server URL

If the URL includes a fully qualified domain name, Internet Explorer selects NTLM. If the URL specifies localhost, Internet Explorer selects NTLM. If the URL specifies the network name of the computer, Internet Explorer selects Negotiate, which succeeds or fails, depending on whether an SPN exists for the Report Server service account.

#### LAN and proxy settings on the client

LAN and proxy settings that you set in Internet Explorer can determine whether NTLM is chosen over Kerberos. However, because LAN and proxy settings vary across organizations, it isn't possible to precisely determine the exact settings that contribute to Kerberos authentication errors. For example, your organization might enforce proxy settings that transform URLs from intranet URLs to fully qualified domain name URLs that resolve over Internet connections. If different authentication providers are used for different types of URLs, you might find that some connections succeed when you expect them to fail.

You might encounter connection errors that you think are due to authentication failures. If so, you can try different combinations of LAN and proxy settings to isolate the problem. In Internet Explorer, LAN and proxy settings are on the **Local Area Network (LAN) Settings** dialog box, which you open by selecting **LAN settings** on the **Connection** tab of **Internet Options**.

## Additional information for Kerberos and report servers

- For more information regarding Kerberos and report servers, see [Deploying a Business Intelligence Solution Using SharePoint, Reporting Services, and PerformancePoint Monitoring Server with Kerberos.](https://www.kasperonbi.com/deploying-a-business-intelligence-solution-using-sharepoint-reporting-services-and-performancepoint-monitoring-server-with-kerberos/)

## Related content

- [Authentication with the report server](../../reporting-services/security/authentication-with-the-report-server.md)
- [Grant permissions on a native mode report server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)
- [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)
- [Configure basic authentication on the report server](../../reporting-services/security/configure-basic-authentication-on-the-report-server.md)
- [Configure custom or forms authentication on the report server](../../reporting-services/security/configure-custom-or-forms-authentication-on-the-report-server.md)
- [Extended Protection for authentication with reporting services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md)
