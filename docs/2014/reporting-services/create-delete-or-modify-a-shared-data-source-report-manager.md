---
title: "Create, Delete, or Modify a Shared Data Source (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "shared data sources [Reporting Services]"
  - "removing shared data sources"
  - "data sources [Reporting Services], shared"
  - "deleting shared data sources"
  - "modifying shared data sources"
ms.assetid: cd7bace3-f8ec-4ee3-8a9f-2f217cdca9f2
author: markingmyname
ms.author: maghan
manager: kfile
---
# Create, Delete, or Modify a Shared Data Source (Report Manager)
  A shared data source specifies connection properties for a data source. If you have a data source that is used by a large number of reports, models, or data-driven subscriptions, consider creating a shared data source to eliminate the overhead of having to maintain the same connection information in multiple places.  
  
 The following icon indicates a shared data source in the Report Manager folder hierarchy:  
  
 ![Shared data source icon](media/hlp-16datasource.png "Shared data source icon")  
shared data source icon  
  
### To create a shared data source  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page.  
  
3.  Click **New Data Source**. The **New Data Source** page opens.  
  
4.  Type a name for the item. A name must contain at least one character and it must start with a letter. It can also include certain symbols, but not spaces or the characters ; ? : \@ & = + , $ / * \< > | " /.  
  
5.  Optionally type a description to provide users with information about the connection. This description will appear on the **Contents** page in Report Manager.  
  
6.  In the **Data source type** list, specify the data processing extension that is used to process data from the data source.  
  
7.  For **Connection string**, specify the connection string that the report server uses to connect to the data source. It is recommended that you do not specify credentials in the connection string.  
  
     The following example illustrates a connection string for connecting to the local [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssSampleDBobject](../includes/sssampledbobject-md.md)] database:  
  
    ```  
    data source=<localservername>; initial catalog=AdventureWorks2012  
    ```  
  
8.  For **Connect using**, specify how credentials are obtained when the report runs:  
  
    -   If you want to prompt the user for a logon name and password, click **Credentials supplied by the user running the report**. To use the credentials that the user enters as Windows credentials, click **Use as Windows credentials when connecting to the data source**. If the user name and password are database credentials, do not select this option.  
  
    -   If you intend to use the data source as a shared data source with saved credentials that are managed by the owner of the data source, or for reports that support subscriptions or other scheduled operations (such as automated report history generation), click **Credentials stored securely in the report server**. If the database server supports impersonation or delegation, you can select **Impersonate the authenticated user after a connection has been made to the data source**.  
  
    -   If you want the report server to pass the credentials of the user accessing the report to the server hosting the external data source, click **Windows Integrated Security**. In this case, the user is not prompted to type a user name or password.  
  
    -   If the data source does not use credentials (for example, if the data source is an XML file that is accessed from the file system), click **Credentials are not required**. You should only specify this credential type if it is valid for the data source. If you select this option for a data source that requires authentication, the connection will fail. If you select this option, be sure to configure the unattended execution account that allows the report server to connect to other computers to retrieve data or files when user credentials are not available.  
  
     For more information about configuring credentials, see [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md). For more information about the unattended execution account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
9. Click the **Test Connection** button to validate the data source configuration.  
  
    > [!NOTE]  
    >  The Test Connection button is not supported for the XML data source type.  
  
10. Click **OK**  
  
### To modify a shared data source  
  
1.  In Report Manager, navigate to the Contents page.  
  
2.  Navigate to the shared data source item, hover over the item, click the drop-down list, and from the context menu, click **Manage**. The **Properties** page opens.  
  
3.  Modify the data source, and then click **Apply**.  
  
### To delete a shared data source  
  
-   In Report Manager, navigate to the **Contents** page and do one of the following:  
  
    -   Navigate to the shared data source item.  
  
         Click the item to open it. The General Properties page opens.  
  
         Click **Delete**, and then click **OK**.  
  
    -   In the **Contents** page, navigate to the folder that contains the data source you want to delete.  
  
         Hover over the item, click the drop-down list, and from the context menu, click **Delete**.  
  
         [!INCLUDE[clickOK](../includes/clickok-md.md)]  
  
## See Also  
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Contents Page &#40;Report Manager&#41;](../../2014/reporting-services/contents-page-report-manager.md)   
 [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](report-data/create-modify-and-delete-shared-data-sources-ssrs.md)   
 [Manage Report Data Sources](report-data/manage-report-data-sources.md)   
 [Configure Data Source Properties for a Report  &#40;Report Manager&#41;](report-data/configure-data-source-properties-for-a-report-report-manager.md)  
  
  
