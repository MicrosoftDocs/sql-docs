---
title: "Configure a Report Server on a Network Load Balancing Cluster | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "report servers [Reporting Services], network load balancing"
ms.assetid: 6bfa5698-de65-43c3-b940-044f41c162d3
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure a Report Server on a Network Load Balancing Cluster
  If you are configuring a report server scale-out to run on a Network Load Balancing (NLB) cluster, you must do the following:  
  
-   Ensure that the NLB cluster is accessible through a virtual server name that maps to the virtual server IP address. A virtual server name is necessary so that you can configure a single point of entry to the NLB cluster. When you configure a URL for each report server instance, you will specify the virtual server name as the host.  
  
-   Configure view state validation to support interactive report viewing. Interactive reports are typically rendered numerous times during a single user session to visualize new or different data in response to user actions. By configuring view state validation, continuity is preserved within the user session regardless of which report server services the actual request.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not provide functionality for load balancing a scale-out deployment or for defining a single point of access through a shared URL. You must implement a separate software or hardware NLB cluster solution to support a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] scale-out deployment.  
  
 You can install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on nodes that are already part of an NLB cluster, or you can configure a scale-out deployment first and then install cluster software.  
  
## Steps for Report Server Deployment on an NLB Cluster  
 Use the following guidelines to install and configure your deployment:  
  
|Step|Description|More information|  
|----------|-----------------|----------------------|  
|1|Before you install Reporting Services on server nodes in an NLB cluster, check the requirements for scale-out deployment.|[Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-native-mode-report-server-scale-out-deployment.md) [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online|  
|2|Configure the NLB cluster and verify it is working correctly.<br /><br /> Be sure to map a host header name to the virtual server IP of the NLB cluster. The host header name is used in the report server URL, and is easier to remember and type than an IP address.|For more information, see the Windows Server product documentation for the version of the Windows operating system that you run.|  
|3|Add the NetBIOS and Fully Qualified Domain Name (FQDN) for the host header to the list of **BackConnectionHostNames** stored in the Windows Registry. Use the steps in **Method 2: Specify host names** in [KB 896861](https://support.microsoft.com/kb/896861) (https://support.microsoft.com/kb/896861), with the following adjustment. **Step 7** of the KB article says "Quit Registry Editor, and then restart the IISAdmin service." Instead, reboot the computer to ensure the changes take effect.<br /><br /> For example, if the host header name \<MyServer> is a virtual name for the Windows computer name of "contoso", you can probably reference the FQDN form as "contoso.domain.com". You will need to add both the hostheader name (MyServer ) and FQDN name (contoso.domain.com) to the list in **BackConnectionHostNames**.|This step is required if your server environment involves NTLM authentication on the local computer, creating a loop back connection.<br /><br /> If this is the case, you will experience the requests between Report Manager and Report Server to fail with 401 (Unauthorized).|  
|4|Install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in files-only mode on nodes that are already part of a NLB cluster, and configure the report server instances for scale-out deployment.<br /><br /> The scale-out that you configure might not respond to requests that are directed to the virtual server IP. Configuring the scale-out to use the virtual server IP occurs at a later step, after you configure view state validation.|[Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)|  
|5|Configure view state validation.<br /><br /> For best results, perform this step after you configure the scale-out deployment, and before you configure the report server instances to use the virtual server IP. By configuring view state validation first, you can avoid exceptions about failed state validation when users attempt to access interactive reports.|[How to Configure View State Validation](#ViewState) in this topic.|  
|6|Configure `Hostname` and `UrlRoot` to use the virtual server IP of the NLB cluster.|[How to Configure Hostname and UrlRoot](#SpecifyingVirtualServerName) in this topic.|  
|7|Verify the servers are accessible through the host name you specified.|[Verify Report Server Access](#Verify) in this topic.|  
  
##  <a name="ViewState"></a> How to Configure View State Validation  
 To run a scale-out deployment on an NLB cluster, you must configure view state validation so that users can view interactive HTML reports. You must do this for the report server and for Report Manager.  
  
 View state validation is controlled by the ASP.NET. By default, view state validation is enabled and uses the identity of the Web service to perform the validation. However, in an NLB cluster scenario, there are multiple service instances and web service identities that run on different computers. Because the service identity varies for each node, you cannot rely on a single process identity to perform the validation.  
  
 To work around this issue, you can generate an arbitrary validation key to support view state validation, and then manually configure each report server node to use the same key. You can use any randomly generated hexadecimal sequence. The validation algorithm (such as SHA1) determines how long the hexadecimal sequence must be.  
  
1.  Generate a validation key and decryption key by using the autogenerate functionality provided by the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)]. In the end, you must have a single <`machineKey`> entry that you can paste into the Web.config file for each Report Manager instance in the scale-out deployment.  
  
     The following example provides an illustration of the value you must obtain. Do not copy the example into your configuration files; the key values are not valid.  
  
    ```  
    <machineKey validationKey="123455555" decryptionKey="678999999" validation="SHA1" decryption="AES"/>  
    ```  
  
2.  Open the Web.config file for Report Manager, and in the <`system.web`> section paste the <`machineKey`> element that you generated. By default, the Report Manager Web.config file is located in \Program Files\Microsoft SQL Server\MSRS10_50.MSSQLSERVER\Reporting Services\ReportManager\Web.config.  
  
3.  Save the file.  
  
4.  Repeat the previous step for each report server in the scale-out deployment.  
  
5.  Verify that all Web.Config files in the \Reporting Services\Report Manager folders contain identical <`machineKey`> elements in the <`system.web`> section.  
  
##  <a name="SpecifyingVirtualServerName"></a> How to Configure Hostname and UrlRoot  
 To configure a report server scale-out deployment on an NLB cluster, you must define a single virtual server name that provides a single point of access to the server cluster. Then register this virtual server name with the Domain Name Server (DNS) in your environment.  
  
 After you define the virtual server name, you can configure the `Hostname` and `UrlRoot` properties in the RSReportServer.config file to include the virtual server name in the report server URL.  
  
 Configure the `Hostname` property when you are using wildcard URL reservations in your reporting environment. When you specify the `Hostname` property to be the virtual server name of the NLB server, network traffic for the reporting environment is directed to the NLB server. The NLB then distributes requests among the report server nodes.  
  
 Additionally, configure the `UrlRoot` property so that report links work in reports that have been exported to static reports, such as in an Excel or PDF format, or in reports that are generated by subscriptions, such as e-mail subscriptions.  
  
 If you integrate [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] with [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 or [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007, or you host your reports in a custom Web application, you might need to configure only the `UrlRoot` property. In this case, configure the `UrlRoot` property to be the URL of the SharePoint site or Web application. This will direct network traffic for the reporting environment to the application that handles the reports rather than to the report server or NLB cluster.  
  
 Do not modify `ReportServerUrl`. If you modify this URL, you will introduce an extra roundtrip through the virtual server each time an internal request is handled. For more information, see [URLs in Configuration Files  &#40;SSRS Configuration Manager&#41;](../install-windows/urls-in-configuration-files-ssrs-configuration-manager.md). For more information about editing the configuration file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
1.  Open RSReportServer.config in a text editor.  
  
2.  Find the **\<Service>** section, and add the following information to the configuration file, replacing the `Hostname` value with the virtual server name for your NLB server:  
  
    ```  
    <Hostname>virtual_server</Hostname>  
    ```  
  
3.  Find `UrlRoot`. The element is unspecified in the configuration file, but the default value used is a URL in this format: http:// or https://\<*computername*>/\<*reportserver*>, where \<*reportserver*> is the virtual directory name of the Report Server Web service.  
  
4.  Type a value for `UrlRoot` that includes the virtual name of the cluster in this format: http:// or https://\<*virtual_server*>/\<*reportserver*>.  
  
5.  Save the file.  
  
6.  Repeat these steps in each RSReportServer.config file for each report server in the scale-out deployment.  
  
##  <a name="Verify"></a> Verify Report Server Access  
 Verify that you can access the scale-out deployment through the virtual server name (for example, https://MyVirtualServerName/reportserver and https://MyVirtualServerName/reports).  
  
 You can check which node actually processes reports by looking at the report server log files or by checking the RS execution log (the execution log table contains a column called **InstanceName** that shows which instance processed a particular request). For more information, see [Reporting Services Log Files and Sources](../report-server/reporting-services-log-files-and-sources.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 If you cannot connect to the report server, check the NLB to ensure that requests are sent to the report server and view the report server HTTP log to ensure that the server is receiving the requests.  
  
#### Troubleshooting Failed Requests  
 If requests do not reach the report server instances, check the RSReportServer.config file to verify that the virtual server name is specified as the host name for the report server URLs:  
  
1.  Open the RSReportServer.config file in a text editor.  
  
2.  Find <`Hostname`>, <`ReportServerUrl`>, and <`UrlRoot`>, and check the host name for each settings. If the value is not the host name you expect, replace it with the correct host name.  
  
 If you start the Reporting Services Configuration tool after making these changes, the tool might change the <`ReportServerUrl`> settings to the default value. Always keep a backup copy of the configuration files in case you need to replace them with the version that contains the settings you want to use.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-url-ssrs-configuration-manager.md)   
 [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)   
 [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md)  
  
  
