---
title: "Store Credentials in a Reporting Services Data Source | Microsoft Docs"
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
manager: kfile
ms.reviewer: ""
ms.custom: ""
ms.date: 05/24/2018
---

# Store Credentials in a Reporting Services Data Source

::: moniker range="<=sql-server-2016||=sqlallproducts-allversions"

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

::: moniker-end

::: moniker range=">=sql-server-2017||=sqlallproducts-allversions"

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)]

::: moniker-end

You can configure stored credentials that a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server uses to access external data for a report. Stored credentials are used if the report runs unattended, for example a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription that publishes a report as an e-mail. The report server retrieves and uses the credentials when report processing is scheduled or triggered. This topic walks you through configuring stored credentials for both Native mode and SharePoint mode report servers.  
  
##  <a name="bkmk_top"></a> Security policy requirements for stored credentials  
 ![as_powerpivot_refresh_sss_set_key](../../analysis-services/power-pivot-sharepoint/media/as-powerpivot-refresh-sss-set-key.gif "as_powerpivot_refresh_sss_set_key") It is required that the account you use for stored credentials, is configured for one of the following security policies on the report server. It is recommended you select the policy with the minimum level of permissions you require for your environment.  
  
1.  **Allow log on locally**. For more information, see [Allow log on locally](https://technet.microsoft.com/library/cc756809\(v=WS.10\).aspx).  
  
2.  **Log on as a batch job**. For more information, see [Log on as a batch job](https://technet.microsoft.com/library/cc755659\(v=ws.10\).aspx).  
  
3.  For general information on policies, see [Edit security settings on a Group Policy object](https://technet.microsoft.com/library/cc736516\(v=ws.10\).aspx).  
  
##  <a name="bkmk_stored_credentials_data_source_native"></a> Configure stored credentials for a report-specific data source (Native mode)  
  
1.  In the web portal, browse to the folder that contains the report. Click the ellipsis (...) in the upper-right corner of the report tile.  
  
2.  Click **Manage** and then click **Data Sources**.  
  
3.  Select **A custom data source**.  
  
4.  In the **Data Source Type** list, select the data processing extension that is used to process data from the data source.  
  
5.  For **Connection String**, specify the connection string that the report server uses to connect to the data source. The following example illustrates a connection string used to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<servername>;initial catalog=AdventureWorks2012  
    ```  
  
6.  For **Connect Using**, select **Credentials stored securely in the report server**.  
  
7.  Type a user name and password.  
  
    -   If the account is a Windows domain user account, specify it in this format: \<domain>\\<account\>, and then select **Use as Windows credentials when connecting to the data source.**  
  
    -   If the user name and password are database credentials, do not select **Use as Windows credentials when connecting to the data source**. If the database server supports impersonation or delegation, you can select **Impersonate the authenticated user after a connection has been made to the data source**.  
  
8.  Click **Apply**.  
  
     ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Security policy requirements for stored credentials](#bkmk_top)  
  
##  <a name="bkmk_stored_credentials_data_source_sharepoint"></a> Configure stored credentials for a report-specific data source (SharePoint mode)  
  
1.  Browse to the document library that contains the report and then click the open menu ![document library context menu for ssrs items](../../reporting-services/report-data/media/ssrs-sharepoint-item-context-menu.png "document library context menu for ssrs items").  
  
2.  Click second open menu ![document library context menu for ssrs items](../../reporting-services/report-data/media/ssrs-sharepoint-item-context-menu.png "document library context menu for ssrs items") and then click **Manage Data Sources**.  
  
3.  Click the name of the **Custom** data source you want to configure with stored credentials.  
  
4.  In the **Data Source Type** list, select the data processing extension that is used to process data from the data source.  
  
5.  For **Connection String**, specify the connection string that the report server uses to connect to the data source. The following example illustrates a connection string used to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<servername>;initial catalog=AdventureWorks2012  
    ```  
  
6.  For **Credentials**, select **Stored Credentials**.  
  
7.  Type a **user name** and **password**.  
  
    -   If the account is a Windows domain user account, specify it in this format: \<domain>\\<account\>, and then select **Use as Windows credentials when connecting to the data source.**  
  
    -   If the user name and password are database credentials, do not select **Use as Windows credentials**. If the database server supports impersonation or delegation, you can select **Set execution context to this account**.  
  
8.  Click **ok**.  
  
     ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Security policy requirements for stored credentials](#bkmk_top)  
  
##  <a name="bkmk_stored_credentials_shared_data_source_native"></a> Configure stored credentials for a shared data source (Native mode)  
  
1.  In the web portal, browse to the shared data source item. 
  
2.  Click the ellipsis (...) in the upper-right corner of the report tile > **Manage**. 
  
3.  In the **Type** list, specify the data processing extension that is used to process data from the data source.  
  
4.  For **Connection String**, specify the connection string that the report server uses to connect to the data source. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you do not specify credentials in the connection string.  
  
     The following example illustrates a connection string used to connect to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<localservername>; initial catalog=AdventureWorks2012  
    ```  
  
5.  Type a user name and password.  
  
    -   If the account is a Windows domain user account, specify it in this format: \<domain>\\<account\>, and then select **Use as Windows credentials when connecting to the data source.**  
  
    -   If the user name and password are database credentials, do not select **Use as Windows credentials when connecting to the data source**. If the database server supports impersonation or delegation, you can select **Impersonate the authenticated user after a connection has been made to the data source**.  
  
6.  Click **Apply**.  
  
     ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Security policy requirements for stored credentials](#bkmk_top)  
  
##  <a name="bkmk_stored_credentials_shared_data_source_sharepoint"></a> Configure stored credentials for a shared data source (SharePoint mode)  
  
1.  In the document library, browse to the shared data source item.![Shared data source icon](../../reporting-services/report-data/media/hlp-16datasource.png "Shared data source icon")  
  
2.  Click the context menu ![document library context menu for ssrs items](../../reporting-services/report-data/media/ssrs-sharepoint-item-context-menu.png "document library context menu for ssrs items") and then click the second context menu ![document library context menu for ssrs items](../../reporting-services/report-data/media/ssrs-sharepoint-item-context-menu.png "document library context menu for ssrs items").  
  
3.  Click **Edit Data Source Definition**.  
  
4.  In the **Data Source Type** list, specify the data processing extension that is used to process data from the data source.  
  
5.  For **Connection String**, specify the connection string that the report server uses to connect to the data source. [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you do not specify credentials in the connection string.  
  
     The following example illustrates a connection string used to connect to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<localservername>; initial catalog=AdventureWorks2012  
    ```  
  
6.  Type a user name and password.  
  
    -   If the account is a Windows domain user account, specify it in this format: \<domain>\\<account\>, and then select **Use as Windows credentials.**  
  
    -   If the user name and password are database credentials, do not select **Use as Windows credentials**. If the database server supports impersonation or delegation, you can select **Set Execution context to this account**.  
  
7.  Click **Ok**.  
  
     ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Security policy requirements for stored credentials](#bkmk_top)  
  
## See Also  
 [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
  
