---
title: "Register a Service Principal Name (SPN) for a Report Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: dda91d4f-77cc-4898-ad03-810ece5f8e74
author: markingmyname
ms.author: maghan
manager: kfile
---
# Register a Service Principal Name (SPN) for a Report Server
  If you are deploying [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in a network that uses the Kerberos protocol for mutual authentication, you must create a Service Principal Name (SPN) for the Report Server service if you configure it to run as a domain user account.  
  
## About SPNs  
 An SPN is a unique identifier for a service on a network that uses Kerberos authentication. It consists of a service class, a host name, and a port. On a network that uses Kerberos authentication, an SPN for the server must be registered under either a built-in computer account (such as NetworkService or LocalSystem) or user account. SPNs are registered for built-in accounts automatically. However, when you run a service under a domain user account, you must manually register the SPN for the account you want to use.  
  
 To create an SPN, you can use the **SetSPN** command line utility. For more information, see the following:  
  
-   [Setspn](https://technet.microsoft.com/library/cc731241\(WS.10\).aspx) (https://technet.microsoft.com/library/cc731241(WS.10).aspx).  
  
-   [Service Principal Names (SPNs) SetSPN Syntax (Setspn.exe)](https://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spns-setspn-syntax-setspn-exe.aspx) (https://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spns-setspn-syntax-setspn-exe.aspx).  
  
 You must be a domain administrator to run the utility on the domain controller.  
  
## Syntax  
 The command syntax for using SetSPN utility to create an SPN for the report server resembles the following:  
  
```  
Setspn -s http/<computername>.<domainname>:<port> <domain-user-account>  
```  
  
 **SetSPN** is available with Windows Server. The `-s` argument adds a SPN after validating no duplicate exists. **NOTE:-s** is available in Windows Server starting with Windows Server 2008.  
  
 `HTTP` is the service class. The Report Server Web service runs in HTTP.SYS. A by-product of creating an SPN for HTTP is that all Web applications on the same computer that run in HTTP.SYS (including applications hosted in IIS) will be granted tickets based on the domain user account. If those services run under a different account, the authentication requests will fail. To avoid this problem, be sure to configure all HTTP applications to run under the same account, or consider creating host headers for each application and then creating separate SPNs for each host header. When you configure host headers, DNS changes are required regardless of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration.  
  
 The values that you specify for \<*computername*>, \<*domainname*>, and \<*port*> identify the unique network address of the computer that hosts the report server. This can be a local host name or a fully qualified domain name (FQDN). If you only have one domain and are using port 80, you can omit \<*domainname*> and \<*port*> from your command line. \<*domain-user-account*> is the user account under which the Report Server service runs and for which the SPN must be registered.  
  
## Register an SPN for Domain User Account  
  
#### To register an SPN for a Report Server service running as a domain user  
  
1.  Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and configure the Report Server service to run as a domain user account. Note that users will not be able to connect to the report server until you complete the following steps.  
  
2.  Log on to the domain controller as domain administrator.  
  
3.  Open a Command Prompt window.  
  
4.  Copy the following command, replacing placeholder values with actual values that are valid for your network:  
  
    ```  
    Setspn -s http/<computer-name>.<domain-name>:<port> <domain-user-account>  
    ```  
  
     For example: `Setspn -s http/MyReportServer.MyDomain.com:80 MyDomainUser`  
  
5.  Run the command.  
  
6.  Open the **RsReportServer.config** file and locate the `<AuthenticationTypes>` section.  
  
7.  Add `<RSWindowsNegotiate/>` as the first entry in this section to enable NTLM.  
  
## See Also  
 [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-service-account-ssrs-configuration-manager.md)   
 [Configure the Report Server Service Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)   
 [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md)  
  
  
