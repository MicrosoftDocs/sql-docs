---
title: "Configure Basic Authentication on the Report Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, configuration"
  - "Basic authentication"
ms.assetid: 8faf2938-b71b-4e61-a172-46da2209ff55
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure Basic Authentication on the Report Server
  By default, Reporting Services accepts requests that specify Negotiate and NTLM authentication. If your deployment includes client applications or browsers that use Basic authentication, you must add Basic authentication to the list of supported types. In addition, if you want to use Report Builder, you must enable Anonymous access to the Report Builder files.  
  
 To configure Basic authentication on the report server, you edit XML elements and values in the RSReportServer.config file. You can copy and paste the examples in this topic to replace the default values.  
  
 Before you enable Basic authentication, verify that your security infrastructure supports it. Under Basic authentication, the Report Server Web service will pass credentials to the local security authority. If the credentials specify a local user account, the user is authenticated by the local security authority on the report server computer and the user will get a security token that is valid for local resources. Credentials for domain user accounts are forwarded to and authenticated by a domain controller. The resulting ticket is valid for network resources.  
  
 Channel encryption, such as Secure Sockets Layer (SSL), is required if you want to mitigate the risk of having credentials intercepted while in transit to a domain controller in your network. By itself, Basic authentication transmits the user name in clear text and the password in base-64 encoding. Adding channel encryption makes the packet unreadable. For more information, see [Configure SSL Connections on a Native Mode Report Server](configure-ssl-connections-on-a-native-mode-report-server.md).  
  
 After you enable Basic authentication, be aware that users cannot select the **Windows integrated security** option when setting connection properties to an external data source that provides data to a report. The option will be grayed out in the data source property pages.  
  
> [!NOTE]  
>  The following instructions are intended for a native mode report server. If the report server is deployed in SharePoint integrated mode, you must use the default authentication settings that specify Windows integrated security. The report server uses internal features in the default Windows Authentication extension to support report server in SharePoint integrated mode.  
  
### To configure a report server to use Basic authentication  
  
1.  Open RSReportServer.config in a text editor.  
  
     The file is located at *\<drive>:*\Program Files\Microsoft SQL Server\MSRS12.MSSQLSERVER\Reporting Services\ReportServer.  
  
2.  Find <`Authentication`>.  
  
3.  Copy one of the following XML structures that best fits your needs. The first XML structure provides placeholders for specifying all of the elements, which are described in the next section:  
  
    ```  
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
  
    ```  
          <AuthenticationTypes>  
                 <RSWindowsBasic/>  
          </AuthenticationTypes>  
    ```  
  
4.  Paste it over the existing entries for <`Authentication`>.  
  
     If you are using multiple authentication types, add just the `RSWindowsBasic` element but do not delete the entries for `RSWindowsNegotiate`, `RSWindowsNTLM`, or `RSWindowsKerberos`.  
  
     To support the Safari browser, you cannot configure the report server to use multiple authentication types. You must specify only `RSWindowsBasic` and delete the other entries.  
  
     Note that you cannot use `Custom` with other authentication types.  
  
5.  Replace empty values for <`Realm`> or <`DefaultDomain`> with values that are valid for your environment.  
  
6.  Save the file.  
  
7.  If you configured a scale-out deployment, repeat these steps for other report servers in the deployment.  
  
8.  Restart the report server to clear any sessions that are currently open.  
  
## RSWindowsBasic Reference  
 The following elements can be specified when configuring Basic authentication.  
  
|Element|Required|Valid Values|  
|-------------|--------------|------------------|  
|LogonMethod|Yes<br /><br /> If you do not specify a value, 3 will be used.|`2` = Network logon, intended for high performance servers to authenticate plain text passwords.<br /><br /> `3` = Cleartext logon, which preserves logon credentials in the authentication package that is sent with each HTTP request, allowing the server to impersonate the user when connecting to other servers in the network. (Default)<br /><br /> Note: Values 0 (for interactive logon) and 1 (for batch logon) are not supported in [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)].|  
|Realm|Optional|Specifies a resource partition that includes authorization and authentication features used to control access to protected resources in your organization.|  
|DefaultDomain|Optional|Specifies the domain used by the server to authenticate the user. This value is optional, but if you omit it the report server will use the computer name as the domain. If the computer is a member of domain, that domain is the default domain. If you installed the report server on a domain controller, the domain used is the one controlled by the computer.|  
  
## Enabling Anonymous Access to Report Builder Application Files  
 Report Builder uses ClickOnce technology to download and install application files on the client computer. When it starts on the client computer, the ClickOnce application launcher will make a request for additional application files on the report server computer. If the report server is configured for Basic authentication, the ClickOnce application launcher will fail the authentication check because it does not support Basic authentication.  
  
 To work around this issue, you can configure Anonymous access to the Report Builder program files. Doing so allows ClickOnce to bypass the authentication check when retrieving its files. Enable Anonymous access by doing the following:  
  
-   Verify that the report server is configured for Basic authentication.  
  
-   Create a bin folder under ReportBuilder and copy four assemblies to the folder.  
  
-   Add the `IsReportBuilderAnonymousAccessEnabled` element to the RSReportServer.config and set it to `True`. After you save the file, the report server creates a new endpoint to Report Builder. The endpoint is used internally to access program files and does not have a programmatic interface that you can use in code. Having a separate endpoint allows Report Builder to run in its own application domain within the Report Server service process boundary.  
  
-   Optionally, you can specify a least-privilege account to process requests under a security context that is different from the report server. This account becomes the anonymous account for accessing Report Builder files on a report server. The account sets the identity of the thread in the ASP.NET worker process. Requests that run in that thread are passed to the report server without an authentication check. This account is equivalent to the IUSR_\<machine> account in Internet Information Services (IIS), which is used to set the security context for ASP.NET worker processes when Anonymous access and impersonation are enabled. To specify the account, you add it to a Report Builder Web.config file.  
  
 The report server must be configured for Basic authentication if you want to enable Anonymous access to the Report Builder program files. If the report server is not configured for Basic authentication, you will get an error when you attempt to enable Anonymous access.  
  
 For more information about authentication issues and Report Builder, see [Configure Report Builder Access](../report-server/configure-report-builder-access.md).  
  
#### To configure Report Builder access on a report server configured for Basic authentication  
  
1.  Verify the report server is configured for Basic authentication by checking the authentication settings in the RSReportServer.config file.  
  
2.  Create a BIN folder under the ReportBuilder folder. By default, this folder is located at \Program Files\Microsoft SQL Server\MSRS12.MSSQLSERVER\Reporting Services\ReportServer\ReportBuilder.  
  
3.  Copy the following assemblies from the ReportServer\Bin folder to the ReportBuilder\BIN folder:  
  
     Microsoft.ReportingServices.Diagnostics.dll  
  
     Microsoft.ReportingServices.Interfaces.dll  
  
     ReportingServicesAppDomainManager.dll  
  
     RSHttpRuntime.dll  
  
4.  Optionally, create a Web.config file to process Report Builder requests under an Anonymous account:  
  
    ```  
    <?xml version="1.0" encoding="utf-8" ?>  
    <configuration>  
    <system.web>  
    <authentication mode="Windows" />    
    <identity impersonate="true " userName="username" password="password"/>  
    </system.web>  
    </configuration>  
    ```  
  
     Authentication mode must be set to `Windows` if you include a Web.config file.  
  
     `Identity impersonate` can be `True` or `False`.  
  
    -   Set it to `False` if you do not want ASP.NET to read the security token. The request will run in the security context of the Report Server service.  
  
    -   Set it to `True` if you want ASP.NET to read the security token from the host layer. If you set it to `True`, you must also specify `userName` and `password` to designate an Anonymous account. The credentials you specify will determine the security context under which the request is issued.  
  
5.  Save the Web.config file to the ReportBuilder\bin folder.  
  
6.  Open RSReportServer.config file, in the Services section, find `IsReportManagerEnabled` and add the following setting below it:  
  
    ```  
    <IsReportBuilderAnonymousAccessEnabled>True</IsReportBuilderAnonymousAccessEnabled>  
    ```  
  
7.  Save RSReportServer.config and close the file.  
  
8.  Restart the report server.  
  
## See Also  
 [Application Domains for Report Server Applications](../report-server/application-domains-for-report-server-applications.md)   
 [Reporting Services Security and Protection](reporting-services-security-and-protection.md)  
  
  
