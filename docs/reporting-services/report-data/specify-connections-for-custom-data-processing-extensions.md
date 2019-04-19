---
title: "Specify Connections for Custom Data Processing Extensions | Microsoft Docs"
ms.date: 05/24/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "custom data processing extensions [Reporting Services]"
  - "IDbConnection interface, connection strings"
  - "impersonation [Reporting Services]"
  - "IDbConnectionExtension interface, connection strings"
  - "data sources [Reporting Services], security"
  - "connection strings [Reporting Services]"
  - "credentials [Reporting Services]"
  - "authentication [Reporting Services]"
  - "external data sources [Reporting Services]"
  - "data processing extensions [Reporting Services], connections"
ms.assetid: 2cddc9ea-0e28-4350-80ae-332412908e47
author: maggiesMSFT
ms.author: maggies
---
# Specify Connections for Custom Data Processing Extensions
  You can create or use third-party custom data processing extensions on a report server to enhance the data processing capability of supported data sources, or to support additional types of data sources that are not available in a default [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation. Connections are handled differently depending on the implementation. The following implementations are available for data processing extensions:  
  
-   Custom [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers (if you are accessing data from DB2.NET, Oracle, ODP.NET, or Teradata data sources, you might be using a custom .NET data provider)  
  
-   Custom data processing extensions that support <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection>  
  
-   Custom data processing extensions that support <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension>  
  
> [!NOTE]  
>  Check with your third-party provider to find out how your custom data processing extension is implemented.  
  
## Impersonation and Custom Data Processing Extensions  
 If your custom data processing extension connects to data sources using impersonation, you must use the Open method on either the <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection> or <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension> interfaces to make the request. Alternately, you can store the user identity object (System.Security.Principal.WindowsIdentity) and then reuse it in the other data processing extension APIs.  
  
 In previous releases of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], all custom data processing extensions were called under user impersonation. In this release, only the Open method will be called while impersonating the user. If you have an existing data processing extension that requires integrated security, you must modify your code to use the Open method or store the user identity object.  
  
## Connections for Custom .NET Framework Data Providers  
 When configuring a report to use a specific data source, you set properties that determine the data source type, connection string, and credentials that are used to access the data source. The following table describes the credential types that are supported for [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers. For more information about setting report data source properties, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).  
  
|Credentials|Connections|  
|-----------------|-----------------|  
|Integrated security|If your data provider supports it, you can use Windows integrated security. The request is sent using the credentials of the current user.<br /><br /> When defining the connection string, be sure to include arguments that specify integrated security (for example, a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source might include **Integrated Security=SSPI** on the connection string).|  
|Windows Authentication|If your data provider supports it, you can use a Windows domain user account. The report server impersonates the user account before the data processing extension is called.<br /><br /> When defining the connection string, be sure to include arguments that specify integrated security (for example, a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source might include **Integrated Security=SSPI** on the connection string).|  
|Database credentials|Database authentication is not supported for connections made through a custom .NET data provider. The report server will fail the connection in all cases.|  
|No credentials|You can use the no credentials option with custom .NET data providers. If the unattended execution account is specified, the connection string determines the credentials that are used. The report server impersonates the unattended execution account to make the connection.<br /><br /> If the unattended execution account is not defined, the report server will fail the connection. For more information about defining the account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).|  
  
## Connections for IDbConnection  
 If you are using a custom data processing extension that only supports <xref:Microsoft.ReportingServices.DataProcessing.IDbConnection>, you must specify the connection in the following way:  
  
1.  Configure the unattended execution account. Configuring this account is required for connections made using **IDbConnection**. The report server impersonates the account when making the connection.  
  
2.  Configure the data source properties on the report to use **No credentials**.  
  
3.  Put the credentials used to connect to the data source in the connection string.  
  
 When using **IDbConnection**, the following credential types are not supported: integrated security, Windows user accounts, and database credentials. If a data source connection uses these options, the connection will fail on the report server.  
  
## Connections for IDbConnectionExtension  
 If you are using a custom data processing extension and supports, <xref:Microsoft.ReportingServices.DataProcessing.IDbConnectionExtension>, you can specify the connection in the following ways:  
  
|Credentials|Connections|  
|-----------------|-----------------|  
|Integrated security|If your data provider supports it, you can use Windows integrated security with custom data processing extensions that use **IDbConnectionExtension**.<br /><br /> When defining the connection string, be sure to include arguments that specify integrated security (for example, a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source might include **Integrated Security=SSPI** on the connection string).|  
|Windows Authentication|If your data provider supports it, you can use a Windows domain user account for custom data processing extensions that use **IDbConnectionExtension**.<br /><br /> The report server impersonates the user account before the data processing extension is called. When defining the connection string, be sure to include arguments that specify integrated security (for example, a connection to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data source might include **Integrated Security=SSPI** on the connection string).|  
|Database credentials|You can use database authentication to configure connections for custom data processing extensions that use **IDbConnectionExtension**.|  
|No credentials|If the unattended execution account is specified, the connection string determines the credentials that are used.<br /><br /> If the unattended execution account is not defined, the report server will fail the connection.|  
  
## See Also  
 [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)   
 [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)   
 [Implementing a Data Processing Extension](../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md)   
 [Configure Data Source Properties for a Report](../../reporting-services/report-data/configure-data-source-properties-for-a-report-report-manager.md)  
  
  
