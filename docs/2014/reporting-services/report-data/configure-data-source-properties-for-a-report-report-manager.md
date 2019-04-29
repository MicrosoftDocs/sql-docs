---
title: "Configure Data Source Properties for a Report  (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [Reporting Services], embedded"
ms.assetid: 27af5195-c845-40e0-9a9c-efe569424022
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure Data Source Properties for a Report  (Report Manager)
  When you run a report, the report server retrieves property information to determine how to connect to a data source. The data source type, connection string, and credential information are specified in the Data Source property pages of the published report. You can set the properties to vary the data source connection information from the original values that were specified when the report was created.  
  
 Alternatively, if you have a predefined shared data source that already specifies the connection information you want to use, you can specify a shared data source instead. To use a shared data source, click **A shared data source** on the Data Source properties page of the report.  
  
### To configure an embedded data source  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page. Navigate to the report that you want to configure a report-specific data source for, and open the report.  
  
3.  Click the **Properties** tab. The **General** properties page opens.  
  
4.  Click the **Data Sources** tab. This opens the Data Source properties page of the report.  
  
5.  Click **A custom data source** to specify data source connection information within the report.  
  
6.  In the **Connection Type** list, specify the data processing extension that is used to process data from the data source.  
  
7.  For **Connection String**, specify the connection string that the report server uses to connect to the data source. It is recommended that you do not specify credentials in the connection string.  
  
     The following example illustrates a connection string for connecting to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<localservername>; initial catalog=AdventureWorks2012  
    ```  
  
8.  For **Connect using**, specify how credentials are obtained when the report runs:  
  
    -   If you want to prompt the user for a logon name and password, click **Credentials supplied by the user running the report**.  
  
    -   If you intend to use the data source with reports that support subscriptions or other scheduled operations (such as automated report history generation), click **Credentials stored securely in the report server**.  
  
    -   If you want the report server to pass the credentials of the user accessing the report to the server hosting the external data source, click **Windows Integrated Security**. In this case, the user is not prompted to type a user name or password.  
  
    -   If the data source does not use credentials (for example, if the data source is an XML file that is accessed from the file system), click **Credentials are not required**. You should only specify this credential type if it is valid for the data source. If you select this option for a data source that requires authentication, the connection will fail. If you select this option, be sure to configure the unattended execution account that allows the report server to connect to other computers to retrieve data or files when user credentials are not available.  
  
 For more information about configuring credentials, see [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md). For more information about the unattended execution account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
## See Also  
 [Contents Page &#40;Report Manager&#41;](../contents-page-report-manager.md)   
 [New Data Source Page &#40;Report Manager&#41;](../new-data-source-page-report-manager.md)   
 [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](create-modify-and-delete-shared-data-sources-ssrs.md)   
 [Manage Report Data Sources](manage-report-data-sources.md)   
 [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../create-delete-or-modify-a-shared-data-source-report-manager.md)   
 [Data Sources Properties Page &#40;Report Manager&#41;](../data-sources-properties-page-report-manager.md)  
  
  
