---
title: "Create, Modify, and Delete Shared Data Sources (SSRS) | Microsoft Docs"
ms.date: 05/24/2018
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "modifying data source properties"
  - "shared data sources [Reporting Services]"
  - "removing shared data sources"
  - "roles [Reporting Services], shared data sources"
  - "data sources [Reporting Services], shared"
  - "data sources [Reporting Services], modifying properties"
  - "deleting shared data sources"
ms.assetid: 1e58c1c2-5ecf-4ce6-9d04-0a8acfba17be
author: markingmyname
ms.author: maghan
---
# Create, Modify, and Delete Shared Data Sources (SSRS)
  A shared data source is a set of data source connection properties that can be referenced by multiple reports, models, and data-driven subscriptions that run on a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server.  Shared data sources provide an easy way to manage data source properties that often change over time. If a user account or password changes, or if you move the database to a different server, you can update the connection information in one place.  
  
 Shared data sources are optional for reports and data-driven subscriptions, but required for report models. If you plan to use report models for ad hoc reporting, you must create and maintain a shared data source item to provide connection information to the model.  
  
 A shared data source consists of the following parts:  
  
|Part|Description|  
|----------|-----------------|  
|Name|A name that identifies the item within the report server folder hierarchy.|  
|Description|A description that appears with the item in the web portal when you view the contents of the folder.|  
|Connection type|The data processing extension used with the data source. You can only use data processing extensions that are deployed on the report server. For more information about data processing extensions included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).|  
|Connection string|The connection string for the database. For more information and to view examples of connection strings to frequently used data sources, see [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).|  
|Credential type|Specifies how credentials are obtained for the connection and whether they are to be used after the connection is made. For more information, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md).|  
  
 A shared data source does not contain query information used to retrieve data. The query is always kept within a report definition.  
  
## Creating and Modifying Shared Data Sources  
 To create a shared data source or modify its properties, you must have **Manage data sources** permissions on the report server. If the report server runs in native mode, you can create and configure the shared data source in the web portal. If the report server runs in SharePoint integrated mode, you can use the application pages on a SharePoint site. For any report server regardless of its mode, you can create a shared data source in Report Designer and then publish it to a target server.  
  
 After you create a shared data source on the report server, you can create role assignments to control access to it, move it to a different location, rename it, or take it offline to prevent report processing while maintenance operations are performed on the external data source. If you rename or move a shared data source item to another location in the report server folder hierarchy, the path information in all reports or subscriptions that reference the shared data source are updated accordingly. If you take the shared data source offline, all reports, models, and subscriptions will not run until you re-enable the data source.  
  
 For more information about controlling access to shared data sources in the report server folder hierarchy, see [Secure Shared Data Source Items](../../reporting-services/security/secure-shared-data-source-items.md).  
  
 **To create a shared data source in Report Designer**  
  
1.  On the toolbar in the Report Data pane, click **New** and then click **Data Source**. The **Data Source Properties** dialog box opens.  
  
    > [!NOTE]  
    >  If the Report Data pane is not visible, click **Report Data** on the **View** menu.  
  
2.  In the **Name** text box, type a name for the data source or accept the default. The data source name is used internally within the report. For clarity, we recommend that the name of the data source contain the name of the database specified in the connection string.  
  
3.  Verify that **Use shared data source reference** is selected and then do the following.  
  
    1.  Click **New**. In the **Shared Data Source** properties dialog box, follow steps 2 and 3 to create a new data source.  
  
    2.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
         The new shared data source appears in the Shared Data Sources folder in Solution Explorer.  
  
4.  Click **Credentials**.  
  
     Specify the credentials to use for this data source. The owner of the data source chooses the type of credentials that are supported.  
  
 **To create a shared data source in the web portal**  
  
1.  In the web portal, select **New** > **Data Source**. 
  
4.  Type a name for the item. A name must contain at least one character and it must start with a letter. It can also include certain symbols, but not spaces or the characters ; ? : \@ & = + , $ / * < > | " /.  
  
5.  Optionally type a description to provide users with information about the connection..  
  
6.  In the **Data source type** list, specify the data processing extension that is used to process data from the data source.  
  
7.  For **Connection string**, specify the connection string that the report server uses to connect to the data source. We recommend not specifying credentials in the connection string.  
  
     The following example illustrates a connection string for connecting to the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] AdventureWorks2016 database:  
  
    ```  
    data source=<localservername>; initial catalog=AdventureWorks2016 
    ```  
  
8.  For **Connect using**, specify how credentials are obtained when the report runs:  
  
    -   If you want to prompt the user for a logon name and password, click **Credentials supplied by the user running the report**. To use the credentials that the user enters as Windows credentials, click **Use as Windows credentials when connecting to the data source**. If the user name and password are database credentials, do not select this option.  
  
    -   If you intend to use the data source as a shared data source with saved credentials that are managed by the owner of the data source, or for reports that support subscriptions or other scheduled operations (such as automated report history generation), click **Credentials stored securely in the report server**. If the database server supports impersonation or delegation, you can select **Impersonate the authenticated user after a connection has been made to the data source**.  
  
    -   If you want the report server to pass the credentials of the user accessing the report to the server hosting the external data source, click **Windows Integrated Security**. In this case, the user is not prompted to type a user name or password.  
  
    -   If the data source does not use credentials (for example, if the data source is an XML file that is accessed from the file system), click **Credentials are not required**. You should only specify this credential type if it is valid for the data source. If you select this option for a data source that requires authentication, the connection will fail. If you select this option, be sure to configure the unattended execution account that allows the report server to connect to other computers to retrieve data or files when user credentials are not available.  
  
         For more information about configuring credentials, see [Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md). For more information about the unattended execution account, see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
9. Click the **Test Connection** button to validate the data source configuration.  
  
    > [!NOTE]  
    >  The Test Connection button is not supported for the XML data source type.  
  
10. Click **OK**  
  
 **To modify a shared data source in the web portal**  
  
1.  In the web portal, navigate to the shared data source.  
  
2.  Select the ellipsis (...) in the upper-right corner of the shared data source > **Manage**.   

    The **Properties** page opens.
  
3.  Modify the data source, and then click **Apply**.  
  
## Deleting Shared Data Sources  
 You can delete a shared data source the same way that you delete any item from the report server.  
  
 **To delete a shared data source**  
  
1. In the web portal, navigate to the shared data source.  
  
2.  Select the ellipsis (...) in the upper-right corner of the shared data source > **Manage**.    
    The **Properties** page opens.
  
3. Click **Delete**, and then click **OK**.  
  
Deleting a shared data source deactivates any report, model, or data-driven subscription that uses it. Without the data source connection information, the items will no longer run. To activate these items, you must open each one and do the following:  
  
-   For reports and data-driven subscriptions that reference the shared data source, you can specify data source connection information in report properties or subscription, or you can select a new shared data source that has the values you want to use.  
  
-   For models and Report Builder reports that use that model, you must specify a new shared data source. Models get data source connection information only through shared data sources.  
  
 There is no Undo operation for deleting a shared data source. However, if you accidentally delete a shared data source, you can create a new one using the same property values as the one you deleted. You will have to open each report, model, and data-driven subscription to rebind the shared data source to the item that uses it, but as long as the data source properties are the same as before, the reports, models, and subscriptions will continue to function as before.  
  
## Importing Shared Data Sources  

**To import an existing data source in Report Designer**  
  
1.  In Solution Explorer, right-click the **Shared Data Sources** folder in the report server project, and then click **Add Existing Item**. The **Add Existing Item** dialog box opens.  
  
2.  Navigate to an existing Report Definition Shared data source (rds) file and then click **Open**.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## Shared Data Sources in SharePoint  
 When you run a report from a SharePoint library, connection information can be defined inside the report or in an external file that is linked to the report. If the connection information is embedded within the report, it is called a custom data source. If the connection information is defined in an external file, it is called a shared data source. The external file can be a report server data source (.rsds) file or an Office Data Connection (.odc) file.  
  
 An .rsds file is similar to an .rds file, but it has a different schema. To create an .rsds file, you can publish an .rds from Report Designer or Model Designer to a SharePoint library (a new .rsds file is created from the original .rds file). Or, you can create a new file in a library on a SharePoint Site.  
  
 After you create or publish a shared data source, you can edit connection properties or delete the file if it is no longer used. Before you delete a shared data source, you should determine whether it is used by reports and report models. You can do this by viewing dependent items that reference the shared data source.  
  
 Although the list of dependent items tells you whether the shared data source is referenced, it does not tell you whether the item is actively used. To determine whether the shared data source or model is actively used, you can review the log files on the report server computer. If you do not have access to the log files or if the files do not contain the information you want, consider moving the report to an inaccessible folder while you determine its actual status.  
  
 **To create a shared data source (.rsds) file (SharePoint 2010)**  
  
1.  Click the **Documents** tab on the library ribbon.  
  
2.  On the **New Document** menu, click **Report Data Source**  
  
    > [!NOTE]  
    >  If you do not see the **Report Data Source** item on the menu, the report data source content type has not been enabled. For more information, see [Add Reporting Services Content Types to a SharePoint Library](../../reporting-services/report-server-sharepoint/add-reporting-services-content-types-to-a-sharepoint-library.md).  
  
3.  In **Name**, enter a descriptive name for the .rsds file.  
  
4.  In **Data Source Type**, select the type of data source from the list. For more information, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
5.  In **Connection String**, specify a pointer to the data source and any other settings that are necessary for establishing a connection to the external data source. The type of data source you are using determines the syntax of the connection string. For more information and examples, see [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
6.  In **Credentials**, specify how the report server obtains credentials to access the external data source. Credentials can be stored, prompted, integrated, or configured for unattended report processing.  
  
    -   Select **Windows authentication (integrated)** if you want to access the data using the credentials of the user who opened the report. Do not select this option if the SharePoint site or farm uses forms authentication or connects to the report server through a trusted account. Do not select this option if you want to schedule subscription or data processing for this report. This option works best when Kerberos authentication is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos authentication is not enabled, Windows credentials can only be passed to one other computer. This means that if the external data source is on another computer, requiring an additional connection, you will get an error instead of the data you expect.  
  
    -   Select **Prompt for credentials** if you want the user to enter his or her credentials each time he or she runs the report. Do not select this option if you want to schedule subscription or data processing for this report.  
  
    -   Select **Stored credentials** if you want to access the data using a single set of credentials. The credentials are encrypted before they are stored. You can select options that determine how the stored credentials are authenticated. Select Use as Windows credentials if the stored credentials belong to a Windows user account. Select **Set execution context to this account** if you want to set the execution context on the database server. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, this option sets the SETUSER function. For more information, see [SETUSER &#40;Transact-SQL&#41;](../../t-sql/statements/setuser-transact-sql.md).  
  
    -   Select **Credentials are not required** if you want to specify credentials in the connection string, or if you want to run the report using a least-privilege account that is configured on the report server. If this account is not configured on the report server, users will be prompted for credentials and any scheduled operations that you define for the report will not run.  
  
7.  Select **Enable this data source** if you want the data source to be active. If the data source is configured but not active, users will see an error message when they attempt to use a report based on the data source.  
  
8.  Click the **Test Connection** button to validate the data source configuration.  
  
    > [!NOTE]  
    >  The Test Connection button is not supported for the XML data source type.  
  
9. Click **OK** to save create the shared data source.  
  
 **To delete a shared data source (.rsds) file**  
  
1.  Open the library that contains the .rsds file.  
  
2.  Point to the shared data source.  
  
3.  Click to display a down arrow, and click **Delete**.  
  
 If you mistakenly delete a shared data source that you meant to keep, you can create a new one that contains the same connection information. After you recreate the shared data source, you must open each report and model that used that data source and select the shared data source. The new shared data source item can have a different name, credentials, or connection string syntax from the one you delete. As long as the connection resolves to the same data source, data source properties can vary from the original values.  
  
 Use caution when deleting a report model. If you delete a model, you can no longer open and modify any reports that are based on that model in Report Builder. If you inadvertently delete a model that is used by existing reports, you must regenerate the model, re-create and save any reports that use the model, and re-specify any model item security that you want to use. You cannot simply regenerate the model and then attach it to an existing report.  
  
## Dependent Items  
 To view a list of reports and models that use the data source, open the Dependent Items page for the shared data source. You can access this page when you open the data source in the web portal or a SharePoint application page. Note that the Dependent Items page does not show data-driven subscriptions. If a shared data source is used by a subscription, the subscription will not appear in the dependent items list.  
  
 **To view dependent items in SharePoint**  
  
1.  Open the library that contains the .rsds file.  
  
2.  Point to the shared data source.  
  
3.  Click to display a down arrow, and select **View Dependent Items**.  
  
     For report models, the list of dependent items shows the reports that were created in Report Builder. For shared data sources, the dependent items list can include both reports and report models.  
  
## See Also  
 [Data Connections, Data Sources, and Connection Strings &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)   
 [Manage Report Data Sources](../../reporting-services/report-data/manage-report-data-sources.md)   
 [Configure Data Source Properties for a Paginated Report](../../reporting-services/report-data/configure-data-source-properties-for-a-report-report-manager.md)  
  
  
