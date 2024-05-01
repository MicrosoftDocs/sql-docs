---
title: "Extended protection for authentication with Reporting Services"
description: "Extended protection for authentication with Reporting Services"
author: maggiesMSFT
ms.author: maggies
ms.date: 06/22/2020
ms.service: reporting-services
ms.subservice: security
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Extended protection for authentication with Reporting Services

  Extended Protection is a set of enhancements to recent versions of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating system. Extended protection enhances how applications can protect credentials and authentication. The feature itself doesn't directly provide protection against specific attacks such as credential forwarding, but it provides an infrastructure for applications such as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] to enforce Extended Protection for Authentication.  
  
 The main authentication enhancements that are part of extended protection are service binding and channel binding. Channel binding uses a channel binding token (CBT), to verify that the channel established between two end points wasn't compromised. Service binding uses Service Principal Names (SPN) to validate the intended destination of authentication tokens. For more background information about extended protection, see [Integrated Windows authentication with extended protection](/previous-versions/visualstudio/visual-studio-2008/dd639324(v=vs.90)).  
  
SQL Server Reporting Services (SSRS) supports and enforces Extended Protection that was enabled in the operating system and configured in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. By default, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] accepts requests that specify Negotiate or NTLM authentication and could therefore benefit from Extended Protection support in the operating system and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extended protection features.  
  
> [!IMPORTANT]  
>  By default, Windows does not enable Extended Protection. For information about how to enable Extended Protection in Windows, see [Extended protection for authentication](https://support.microsoft.com/topic/microsoft-security-advisory-extended-protection-for-authentication-7dd2ee6d-c2e9-3484-2d8e-466261d3f0c7). Both the operating system and client authentication stack must support Extended Protection so that authentication succeeds. For older operating systems you may need to install more than one update for a complete, Extended Protection ready computer. For information on recent developments with Extended Protection, see [updated information with extended protection](/previous-versions/sql/sql-server-2008/dd146365(v=sql.100)).  

## Reporting Services extended protection overview

SSRS supports and enforces extended protection that was enabled in the operating system. If the operating system doesn't support extended protection or the feature in the operating system wasn't enabled, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extended protection feature fails authentication. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Extended Protection also requires a TLS/SSL Certificate. For more information, see [Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md)  
  
> [!IMPORTANT]  
>  By default, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not enable Extended Protection. The feature can be enabled by modifying the **rsreportserver.config** configuration file or by using WMI APIs to update the configuration file. SSRS does not provide a user interface to modify or view extended protection settings. For more information, see the [configuration settings](#ConfigurationSettings) section in this topic.  
  
 Common issues that occur because of changes in extended protection settings or incorrectly configured settings aren't exposed with obvious error messages or dialog windows. Issues related to extended protection configuration and compatibility result in authentication failures and errors in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] trace logs.  
  
> [!IMPORTANT]
>  Some data access technologies may not support extended protection. A data access technology is used to connect to SQL Server data sources and to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] catalog database. Failure of a data access technology to support extended protection impacts [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the following ways:  
> 
>  -   The SQL Server that runs the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] catalog database cannot have extended protection enabled or the report server will not successfully connect to the catalog database and return authentication errors.  
> -   SQL Server instances that are used as [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report data sources cannot have extended protection enabled or tries by the report server to connect to the report data source will fail and return authentication errors.  
> 
>  The documentation for a data access technology should have information about support for extended protection.  
  
### Upgrade  
  
-   Upgrading a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] server to SQL Server 2016 adds configuration settings with default values to the **rsreportserver.config** file. If the settings were already present, the SQL Server 2016 installation preserves them in the **rsreportserver.config** file.  
  
-   When the configuration settings are added to the **rsreportserver.config** configuration file, the default behavior is for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extended protection feature to be off, and you must enable the feature as described in this article. For more information, see the [configuration settings](#ConfigurationSettings) section in this article.  
  
-   The default value for the setting `RSWindowsExtendedProtectionLevel` is `Off`.  
  
-   The default value for the setting `RSWindowsExtendedProtectionScenario` is `Proxy`.  
  
-   Upgrade Advisor doesn't verify that the operating system or the current installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] with Extended Protection support enabled.  
  
### What Reporting Services extended protection does not cover  
 The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extended protection feature doesn't support the following feature areas and scenarios:  
  
-   Authors of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] custom security extensions must add support for extended protection to their custom security extension.  
  
-   The third-party vendor must update the third-party components added to or used by a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation to support extended protection. For more information, contact the third-party vendor.  
  
## Deployment scenarios and recommendations  
 The following scenarios illustrate different deployments and topologies and the recommended configuration to secure them with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Extended Protection.  
  
### Direct  
 This scenario describes directly connecting to a report server, for example, an intranet environment.  
  
|Scenario|Scenario Diagram|How to secure|  
|--------------|----------------------|-------------------|  
|Direct TLS communication.<br /><br /> The report server enforces client to report server Channel Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-directssl.gif" alt-text="Diagram that shows direct TLS communication." lightbox="../../reporting-services/security/media/rs-extendedprotection-directssl.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Direct`.<br /><br /> <br /><br /> -Service Binding isn't necessary because the TLS channel is used for Channel Binding.|  
|Direct HTTP communication. The report server enforces Client to report server Service Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-direct.gif" alt-text="Diagram that shows HTTP communication." lightbox="../../reporting-services/security/media/rs-extendedprotection-directssl.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Any`.<br /><br /> <br /><br /> -There is no TLS Channel therefore no enforcement of Channel Binding is possible.<br /><br /> -Service Binding can be validated, however, it isn't a complete defense without Channel binding and Service Binding on its own only protects from basic threats.|  
  
### Proxy and network load balancing  
 Client applications connect to a device or software that performs TLS and passes through the credentials to the server for authentication, for example, an extranet, Internet, or Secure Intranet. The client connects to a Proxy or all clients use a proxy.  
  
 The situation is the same when you use a Network Load Balancing (NLB) device.  
  
|Scenario|Scenario Diagram|How to secure|  
|--------------|----------------------|-------------------|  
|HTTP communication. The report server enforces client to report server Service Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-indirect.gif" alt-text="Diagram that shows indirect HTTP communication." lightbox="../../reporting-services/security/media/rs-extendedprotection-indirect.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Proxy|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Any`.<br /><br /> <br /><br /> -There is no TLS Channel therefore no enforcement of Channel Binding is possible.<br /><br /> -The report server must be configured to know the name of the proxy server to make sure that the service binding is correctly enforced.|  
|HTTP communication.<br /><br /> The report server enforces client to Proxy Channel Binding and client to report server Service Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-indirect-ssl.gif" alt-text="Diagram that shows indirect SSL communication." lightbox="../../reporting-services/security/media/rs-extendedprotection-indirect-ssl.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Proxy|Set <br />                    `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Proxy`.<br /><br /> <br /><br /> -TLS channel to proxy is available therefore channel binding to the proxy can be enforced.<br /><br /> -Service Binding can also be enforced.<br /><br /> -The Proxy name must be known to the report server and the report server administrator should either create a URL reservation for it, with a host header or configure the Proxy name in the Windows registry entry `BackConnectionHostNames`.|  
|Indirect HTTPS communication with a secure proxy. Report server enforces client to proxy Channel Binding and Client to report server Service Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-indirectsslandhttps.gif" alt-text="Diagram that shows indirect HTTPS communication with a secure proxy." lightbox="../../reporting-services/security/media/rs-extendedprotection-indirectsslandhttps.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Proxy|Set <br />                    `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Proxy`.<br /><br /> <br /><br /> -TLS channel to proxy is available therefore channel binding to the proxy can be enforced.<br /><br /> -Service Binding can also be enforced.<br /><br /> -The Proxy name must be known to the report server and the report server administrator should either create a URL reservation for it, with a host header or configure the Proxy name in the Windows registry entry `BackConnectionHostNames`.|  
  
### Gateway  
 This scenario describes Client applications connecting to a device or software that performs TLS and authenticates the user. Then the device or software impersonates the user context or a different user context before it makes a request to the report server.  
  
|Scenario|Scenario Diagram|How to secure|  
|--------------|----------------------|-------------------|  
|Indirect HTTP communication.<br /><br /> Gateway enforces Client to Gateway channel binding. There's a Gateway to report server Service Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-indirect-ssl.gif" alt-text="Diagram that shows indirect SSL communication." lightbox="../../reporting-services/security/media/rs-extendedprotection-indirect-ssl.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Gateway device|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Any`.<br /><br /> <br /><br /> -Channel Binding from client to report server isn't possible because the gateway impersonates a context and therefore creates a new NTLM token.<br /><br /> -There is no TLS from the Gateway to report server therefore channel binding can't be enforced.<br /><br /> -Service Binding can be enforced.<br /><br /> -Your administrator should configure the Gateway device to enforce channel binding.|  
|Indirect HTTPS communication with a Secure Gateway. The Gateway enforces Client to Gateway Channel Binding and the report server enforces Gateway to report server Channel Binding.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-indirectsslandhttps.gif" alt-text="Diagram that shows indirect HTTPS communication with a Secure Gateway." lightbox="../../reporting-services/security/media/rs-extendedprotection-indirectsslandhttps.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Gateway device|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Direct`.<br /><br /> <br /><br /> -Channel Binding from client to report server isn't possible because the gateway impersonates a context and therefore creates a new NTLM token.<br /><br /> -TLS from Gateway to the report server means channel binding can be enforced.<br /><br /> -Service Binding isn't required.<br /><br /> -Your administrator should configure the Gateway device to enforce channel binding.|  
  
### Combination  
 This scenario describes Extranet or Internet environments where the client connects a Proxy in combination with an intranet environment where a client connects to report server.  
  
|Scenario|Scenario Diagram|How to secure|  
|--------------|----------------------|-------------------|  
|Indirect and direct access from client to report server service without TLS on either of the client to proxy or client to report server connections.|1) Client application<br /><br /> 2) Report server<br /><br /> 3) Proxy<br /><br /> 4) Client application|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Any`.<br /><br /> <br /><br /> -Service Binding from client to report server can be enforced.<br /><br /> -The Proxy name must be known to the report server and the report server administrator should either create a URL reservation for it, with a host header or configure the Proxy name in the Windows registry entry `BackConnectionHostNames`.|  
|Indirect and direct access from client to report server where the client establishes a TLS connection to the proxy or report server.|:::image type="content" source="../../reporting-services/security/media/rs-extendedprotection-combinationssl.gif" alt-text="Diagram that shows indirect and direct access from client to report server." lightbox="../../reporting-services/security/media/rs-extendedprotection-combinationssl.gif":::<br /><br /> 1) Client application<br /><br /> 2) Report server<br /><br /> 3) Proxy<br /><br /> 4) Client application|Set `RSWindowsExtendedProtectionLevel` to `Allow` or `Require`.<br /><br /> Set `RSWindowsExtendedProtectionScenario` to `Proxy`.<br /><br /> <br /><br /> -Channel Binding can be used<br /><br /> -The Proxy name must be known to the report server and the report server administrator should either create a URL reservation for the proxy, with a host header or configure the Proxy name in the Windows registry entry `BackConnectionHostNames`.|  
  
## Configuring Reporting Services extended protection  
 The **rsreportserver.config** file contains the configuration values that control the behavior of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] extended protection.  
  
 For more information about how to use and edit the **rsreportserver.config** file, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md). The extended protection settings can also be changed and inspected by using WMI APIs. For more information, see [SetExtendedProtectionSettings method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setextendedprotectionsettings.md).  
  
 When validation of the configuration settings fails, the authentication types `RSWindowsNTLM`, `RSWindowsKerberos` and `RSWindowsNegotiate` are disabled on the report server.  
  
###  <a name="ConfigurationSettings"></a> Configuration settings for Reporting Services extended protection  
 The following table provides information about configuration settings that appear in the `rsreportserver.config` for extended protection.  
  
|Setting|Description|  
|-------------|-----------------|  
|`RSWindowsExtendedProtectionLevel`|Specifies the degree of enforcement of extended protection. Valid values are:<br /><br /> `Off`: Default. Specifies no channel binding or service binding verification.<br /><br /> `Allow` supports extended protection but doesn't require it.  Specifies:<br /><br /> -Extended protection is enforced for client applications that are running on operating systems that support extended protection. How protection is enforced is determined by setting `RsWindowsExtendedProtectionScenario`<br /><br /> -Authentication is allowed for applications that are running on operating systems that don't support extended protection.<br /><br /> `Require` specifies:<br /><br /> -Extended protection is enforced for client applications that are running on operating systems that support extended protection.<br /><br /> -Authentication is **not** allowed for applications that are running on operating systems that don't support extended protection.|  
|`RsWindowsExtendedProtectionScenario`|Specifies what forms of extended protection are validated: Channel binding, Service Binding, or both. Valid values are:<br /><br /> `Proxy`: Default. Specifies:<br /><br /> -Windows NTLM, Kerberos, and Negotiate authentication when a channel binding token is present.<br /><br /> -Service Binding is enforced.<br /><br /> `Any` Specifies:<br /><br /> -Windows NTLM, Kerberos, and Negotiate authentication and a channel binding aren't required.<br /><br /> -Service binding is enforced.<br /><br /> `Direct` Specifies:<br /><br /> -Windows NTLM, Kerberos, and Negotiate authentication when a CBT is present, a TLS connection to the current service is present, and the CBT for the TLS connection matches the CBT of the NTLM, Kerberos or negotiate token.<br /><br /> -Service Binding isn't enforced.<br /><br /> <br /><br /> Note: The `RsWindowsExtendedProtectionScenario` setting is ignored if `RsWindowsExtendedProtectionLevel` is set to `OFF`.|  
  
 Example entries in the **rsreportserver.config** configuration file:  
  
```  
<Authentication>  
         <RSWindowsExtendedProtectionLevel>Allow</RSWindowsExtendedProtectionLevel>  
         <RSWindowsExtendedProtectionScenario>Proxy</RSWindowsExtendedProtectionLevel>  
</Authentication>  
```  
  
## Service binding and included SPNs  
 Service binding uses Service Principal Names or SPN to validate the intended destination of authentication tokens. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses the existing URL reservation information to build a list of SPNs that are considered valid. The URL reservation information for validation of both SPN and URL reservations enables system administrators to manage both from a single location.  
  
 The list of valid SPNs is updated when one of the following actions occurs: 

- The report server starts.
- The configuration settings for extended protection are changed.
- The application domain is recycled.  
  
 The valid list of SPNs is specific for each application. For example, Report Manager and Report Server each have a different list of valid SPNs calculated.  
  
 The following factors determine the valid SPNs calculated for an application:  
  
-   Each URL reservation.  
  
-   Each SPN retrieved from the domain controller for the service account for Reporting Services.  
  
-   If a URL reservation includes wildcard characters ('*' or '+'), then Report Server adds each entry from the hosts collection.  
  
### Hosts collection sources.  
 The following table lists the potential sources for the Hosts collection.  
  
|Type of source|Description|  
|--------------------|-----------------|  
|ComputerNameDnsDomain|The name of the DNS domain assigned to the local computer. If the local computer is a node in a cluster, the DNS domain name of the cluster virtual server is used.|  
|ComputerNameDnsFullyQualified|The fully qualified DNS name that uniquely identifies the local computer. This name is a combination of the DNS host name and the DNS domain name, that uses the form *HostName*.*DomainName*. If the local computer is a node in a cluster, the fully qualified DNS name of the cluster virtual server is used.|  
|ComputerNameDnsHostname|The DNS host name of the local computer. If the local computer is a node in a cluster, the DNS host name of the cluster virtual server is used.|  
|ComputerNameNetBIOS|The NetBIOS name of the local computer. If the local computer is a node in a cluster, the NetBIOS name of the cluster virtual server is used.|  
|ComputerNamePhysicalDnsDomain|The name of the DNS domain assigned to the local computer. If the local computer is a node in a cluster, the DNS domain name of the local computer is used, not the name of the cluster virtual server.|  
|ComputerNamePhysicalDnsFullyQualified|The fully qualified DNS name that uniquely identifies the computer. If the local computer is a node in a cluster, the fully qualified DNS name of the local computer, is used not the name of the cluster virtual server.<br /><br /> The fully qualified DNS name is a combination of the DNS host name and the DNS domain name, that uses the form *HostName*.*DomainName*.|  
|ComputerNamePhysicalDnsHostname|The DNS host name of the local computer. If the local computer is a node in a cluster, the DNS host name of the local computer is used, not the name of the cluster virtual server.|  
|ComputerNamePhysicalNetBIOS|The NetBIOS name of the local computer. If the local computer is a node in a cluster, this source is the NetBIOS name of the local computer, not the name of the cluster virtual server. |  
  
For more information, see [Register a service principal name &#40;SPN&#41; for a report server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md) and [About URL reservations and registration  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md).  
  
## Related content

[Connect to the database engine by using extended protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md)   
[Extended protection for authentication overview](/previous-versions/dotnet/netframework-3.5/dd767318(v=vs.90))   
[Integrated Windows authentication with extended protection](/previous-versions/visualstudio/visual-studio-2008/dd639324(v=vs.90))   
[Microsoft Security Advisory: Extended protection for authentication](https://go.microsoft.com/fwlink/?LinkId=179923)   
[Report server service trace log](../../reporting-services/report-server/report-server-service-trace-log.md)   
[RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[SetExtendedProtectionSettings method &#40;WMI MSReportServer_ConfigurationSetting&#41;](../../reporting-services/wmi-provider-library-reference/configurationsetting-method-setextendedprotectionsettings.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
