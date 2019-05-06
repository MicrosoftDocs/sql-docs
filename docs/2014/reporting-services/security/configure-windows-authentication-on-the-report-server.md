---
title: "Configure Windows Authentication on the Report Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Windows authentication [Reporting Services]"
  - "Reporting Services, configuration"
ms.assetid: 4de9c3dd-0ee7-49b3-88bb-209465ca9d86
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure Windows Authentication on the Report Server
  By default, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] accepts requests that specify Negotiate or NTLM authentication. If your deployment includes client applications and browsers that use these security providers, you can use the default values without additional configuration. If you want to use a different security provider for Windows integrated security (for example, if you want to use Kerberos directly), or if you modified the default values and want to restore the original settings, you can use the information in this topic to specify authentication settings on the report server.  
  
 To use Windows integrated security, each user who requires access to a report server must have a valid Windows local or domain user account or be a member of a Windows local or domain group account. You can include accounts from other domains as long as those domains are trusted. The accounts must have access to the report server computer, and must be subsequently assigned to roles in order to gain access to specific report server operations.  
  
 The following additional requirements must also be met:  
  
-   The RSeportServer.config files must have `AuthenticationType` set to `RSWindowsNegotiate`, `RSWindowsKerberos`, or `RSWindowsNTLM`. By default, the RSReportServer.config file includes the `RSWindowsNegotiate` setting if the Report Server service account is either NetworkService or LocalSystem; otherwise, the `RSWindowsNTLM` setting is used. You can add `RSWindowsKerberos` if you have applications that only use Kerberos authentication.  
  
    > [!IMPORTANT]  
    >  Using `RSWindowsNegotiate` will result in a Kerberos authentication error if you configured the Report Server service to run under a domain user account and you did not register a Service Principal Name (SPN) for the account. For more information, see [Resolving Kerberos Authentication Errors When Connecting to a report server](#proxyfirewallRSWindowsNegotiate) in this topic.  
  
-   [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)] must be configured for Windows Authentication. By default, the Web.config files for the Report Server Web service and Report Manager include the \<authentication mode="Windows"> setting. If you change it to \<authentication mode="Forms">, the Windows Authentication for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will fail.  
  
-   The Web.config files for the Report Server Web service and Report Manager must have \<identity impersonate= "true" />.  
  
-   The client application or browser must support Windows integrated security.  
  
 To change the report server authentication settings, edit the XML elements and values in the RSReportServer.config file. You can copy and paste the examples in this topic to implement specific combinations.  
  
 The default settings work best if all client and server computers are in the same domain or in a trusted domain and the report server is deployed for intranet access behind a corporate firewall. Trusted and single domains are a requirement for passing Windows credentials. Credentials can be passed more than one time if you enable the Kerberos version 5 protocol for your servers. Otherwise, credentials can be passed only one time before they expire. For more information about configuring credentials for multiple computer connections, see [Specify Credential and Connection Information for Report Data Sources](../report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
 The following instructions are intended for a native mode report server. If the report server is deployed in SharePoint integrated mode, you must use the default authentication settings that specify Windows integrated security. The report server uses internal features in the default Windows Authentication extension to support report servers in SharePoint integrated mode.  
  
## Extended Protection for Authentication  
 Beginning with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], support for Extended Protection for Authentication is available. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] feature supports the use of channel binding and service binding to enhance protection of authentication. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] features need to be used with an operating system that supports Extended Protection. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration for extended protection is determined by settings in the RSReportServer.config file. The file can be updated by either editing the file or using WMI APIs. For more information, see [Extended Protection for Authentication with Reporting Services](extended-protection-for-authentication-with-reporting-services.md).  
  
### To configure a report server to use Windows integrated security  
  
1.  Open RSReportServer.config in a text editor.  
  
2.  Find <`Authentication`>.  
  
3.  Copy one of the following XML structures that best fits your needs. You can specify `RSWindowsNegotiate`, `RSWindowsNTLM`, and `RSWindowsKerberos` in any order. You should enable authentication persistence if you want to authenticate the connection rather than each individual request. Under authentication persistence, all requests that require authentication will be allowed for the duration of the connection.  
  
     The first XML structure is the default configuration when the Report Server service account is either NetworkService or LocalSystem:  
  
    ```  
    <Authentication>  
          <AuthenticationTypes>  
                 <RSWindowsNegotiate />  
          </AuthenticationTypes>  
          <EnableAuthPersistence>true</EnableAuthPersistence>  
    </Authentication>  
    ```  
  
     The second XML structure is the default configuration when the Report Server service account is not NetworkService or LocalSystem:  
  
    ```  
    <Authentication>  
          <AuthenticationTypes>  
                 <RSWindowsNTLM />  
          </AuthenticationTypes>  
          <EnableAuthPersistence>true</EnableAuthPersistence>  
    ```  
  
     \</Authentication>  
  
     The third XML structure specifies all of the security packages that are used in Windows integrated security:  
  
    ```  
          <AuthenticationTypes>  
                 <RSWindowsNegotiate />  
                 <RSWindowsKerberos />  
                 <RSWindowsNTLM />  
          </AuthenticationTypes>  
    ```  
  
     The fourth XML structure specifies NTLM only for deployments that do not support Kerberos or to work around Kerberos authentication errors:  
  
    ```  
          <AuthenticationTypes>  
                 <RSWindowsNTLM />  
          </AuthenticationTypes>  
    ```  
  
4.  Paste it over the existing entries for <`Authentication`>.  
  
     Note that you cannot use `Custom` with the `RSWindows` types.  
  
5.  Modify as appropriate the settings for extended protection. Extended protection is disabled by default.  If these entries are not present, the current computer may not be running a version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] which supports extended protection. For more information, see [Extended Protection for Authentication with Reporting Services](extended-protection-for-authentication-with-reporting-services.md)  
  
    ```  
          <RSWindowsExtendedProtectionLevel>Allow</RSWindowsExtendedProtectionLevel>  
          <RSWindowsExtendedProtectionScenario>Proxy</RSWindowsExtendedProtectionScenario>  
    ```  
  
6.  Save the file.  
  
7.  If you configured a scale-out deployment, repeat these steps for other report servers in the deployment.  
  
8.  Restart the report server to clear any sessions that are currently open.  
  
##  <a name="proxyfirewallRSWindowsNegotiate"></a> Resolving Kerberos Authentication Errors When Connecting to a Report Server  
 On a report server that is configured for Negotiate or Kerberos authentication, a client connection to the report server will fail if there is a Kerberos authentication error. Kerberos authentication errors are known to occur when:  
  
-   The Report Server service runs as a Windows domain user account and you did not register a Service Principal Name (SPN) for the account.  
  
-   The report server is configured with the `RSWindowsNegotiate` setting.  
  
-   The browser chooses Kerberos over NTLM in the authentication header in the request it sends to the report server.  
  
 You can detect the error if you enabled Kerberos logging. Another symptom of the error is that you are prompted for credentials multiple times and then see an empty browser window.  
  
 You can confirm that you are encountering a Kerberos authentication error by removing < `RSWindowsNegotiate` /> from your configuration file and reattempting the connection.  
  
 After you confirm the problem, you can address it in the following ways:  
  
-   Register an SPN for the Report Server service under the domain user account. For more information, see [Register a Service Principal Name &#40;SPN&#41; for a Report Server](../report-server/register-a-service-principal-name-spn-for-a-report-server.md).  
  
-   Change the service account to run under a built-in account such as Network Service. Built-in accounts map HTTP SPN to the Host SPN, which is defined when you join a computer to your network. For more information, see [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-service-account-ssrs-configuration-manager.md).  
  
-   Use NTLM. NTLM will generally work in cases where Kerberos authentication fails. To use NTLM, remove `RSWindowsNegotiate` from the RSReportServer.config file and verify that only `RSWindowsNTLM` is specified. If you choose this approach, you can continue to use a domain user account for the Report Server service even if you do not define an SPN for it.  
  
#### Logging information  
 There are several sources of logging information that can help resolve Kerberos related issues.  
  
##### User-Account-Control Attribute  
 Determine if the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service account has the sufficient attribute set in Active Directory. Review the reporting services service trace log file to find the value logged for the UserAccountControl attribute. The value logged is in decimal form. You need to convert the decimal value to hexadecimal form and then find that value in the MSDN topic describing User-Account-Control Attribute.  
  
-   The reporting services service trace log entry will look similar to the following:  
  
    ```  
    appdomainmanager!DefaultDomain!8f8!01/14/2010-14:42:28:: i INFO: The UserAccountControl value for the service account is 590336  
    ```  
  
-   One option for converting the value Decimal value to hexadecimal form is to us the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Calculator. Windows Calculator supports several modes that show the 'Dec' option and 'Hex' options. Select the 'Dec' option, paste or type in the decimal value you found in the log file and then select the 'Hex' option.  
  
-   Then refer to the topic [User-Account-Control Attribute](https://go.microsoft.com/fwlink/?LinkId=183366) to derive the attribute for the service account.  
  
##### SPNs Configured in Active Directory for the Reporting Services service account.  
 To log the SPNs in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service trace log file, you can enable the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Extended Protection feature temporarily.  
  
-   Modify the configuration file `rsreportserver.config` by setting the following:  
  
    ```  
    <RSWindowsExtendedProtectionLevel>Allow</RSWindowsExtendedProtectionLevel>   
    <RSWindowsExtendedProtectionScenario>Any</RSWindowsExtendedProtectionScenario>  
    ```  
  
-   Restart the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service and look for entries similar to the following in the trace log file:  
  
    ```  
    rshost!rshost!e44!01/14/2010-14:43:51:: i INFO: Registered valid SPNs list for endpoint 2: rshost!rshost!e44!01/14/2010-14:43:52:: i INFO: SPN Whitelist Added <Explicit> - <HTTP/sqlpod064-13.w2k3.net>.  
    ```  
  
-   The values under \<Explicit> will contain the SPNs configured in Active Directory for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service account.  
  
 If you do not want continue using Extended Protection, then set the configuration values back to defaults and restart the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Service account.  
  
```  
<RSWindowsExtendedProtectionLevel>Off</RSWindowsExtendedProtectionLevel>  
<RSWindowsExtendedProtectionScenario>Proxy</RSWindowsExtendedProtectionScenario>  
```  
  
 For more information see, [Extended Protection for Authentication with Reporting Services](extended-protection-for-authentication-with-reporting-services.md)  
  
#### How the Browser Chooses Negotiated Kerberos or Negotiated NTLM  
 When you use Internet Explorer to connect to the report server, it specifies either Negotiated Kerberos or NTLM on the authentication header. NTLM is used instead of Kerberos when:  
  
-   The request is sent to a local report server.  
  
-   The request is sent to an IP address of the report server computer rather than a host header or server name.  
  
-   Firewall software blocks ports used for Kerberos authentication.  
  
-   The operating system of a particular server does not have Kerberos enabled.  
  
-   The domain includes older versions of Windows client and server operating systems that do not support the Kerberos authentication feature built into newer versions of the operating system.  
  
 In addition, Internet Explorer might choose either Negotiated Kerberos or NTLM depending on how you configured URL, LAN, and proxy settings.  
  
###### Report Server URL  
 If the URL includes a fully qualified domain name, Internet Explorer selects NTLM. If the URL specifies localhost, Internet Explorer selects NTLM. If the URL specifies the network name of the computer, Internet Explorer selects Negotiate, which will succeed or fail depending on whether an SPN exists for the Report Server service account.  
  
###### LAN and Proxy Settings on the Client  
 LAN and proxy settings that you set in Internet Explorer can determine whether NTLM is chosen over Kerberos. However, because LAN and proxy settings vary across organizations, it is not possible to precisely determine the exact settings that contribute to Kerberos authentication errors. For example, your organization might enforce proxy settings that transform URLs from intranet URLs to fully-qualified domain name URLs that resolve over Internet connections. If different authentication providers are used for different types of URLs, you might find that some connections succeed when you would have expected them to fail.  
  
 If you encounter connection errors that you think are due to authentication failures, you can try different combinations of LAN and proxy settings to isolate the problem. In Internet Explorer, LAN and proxy settings are on the **Local Area Network (LAN) Settings** dialog box, which you open by clicking **LAN settings** on the **Connection** tab of **Internet Options**.  
  
## External resources  
  
-   For additional information regarding Kerberos and report servers, see [Deploying a Business Intelligence Solution Using SharePoint, Reporting Services, and PerformancePoint Monitoring Server with Kerberos.](https://go.microsoft.com/fwlink/?LinkID=177751)  
  
## See Also  
 [Authentication with the Report Server](authentication-with-the-report-server.md)   
 [Granting Permissions on a Native Mode Report Server](granting-permissions-on-a-native-mode-report-server.md)   
 [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md)   
 [Configure Basic Authentication on the Report Server](configure-basic-authentication-on-the-report-server.md)   
 [Configure Custom or Forms Authentication on the Report Server](configure-custom-or-forms-authentication-on-the-report-server.md)   
 [Extended Protection for Authentication with Reporting Services](extended-protection-for-authentication-with-reporting-services.md)  
  
  
