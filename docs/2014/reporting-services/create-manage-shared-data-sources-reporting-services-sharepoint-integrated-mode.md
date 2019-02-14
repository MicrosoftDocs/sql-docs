---
title: "Create and Manage Shared Data Sources (Reporting Services in SharePoint Integrated Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], shared data sources"
  - "shared data sources [Reporting Services]"
ms.assetid: 2d3428e4-a810-4e66-a287-ff18e57fad76
author: maggiesmsft
ms.author: maghan
manager: kfile
---
# Create and Manage Shared Data Sources (Reporting Services in SharePoint Integrated Mode)
  When you run a report from a SharePoint library, connection information can be defined inside the report or in an external file that is linked to the report. If the connection information is embedded within the report, it is called a custom data source. If the connection information is defined in an external file, it is called a shared data source. The external file can be a report server data source (.rsds) file or an Office Data Connection (.odc) file.  
  
 An .rsds file is similar to an .rds file, but it has a different schema. To create an .rsds file, you can publish an .rds from Report Designer or Model Designer to a SharePoint library (a new .rsds file is created from the original .rds file). Or, you can create a new file in a library on a SharePoint Site.  
  
 After you create or publish a shared data source, you can edit connection properties or delete the file if it is no longer used. Before you delete a shared data source, you should determine whether it is used by reports and report models. You can do this by viewing dependent items that reference the shared data source.  
  
 Although the list of dependent items tells you whether the shared data source is referenced, it does not tell you whether the item is actively used. To determine whether the shared data source or model is actively used, you can review the log files on the report server computer. If you do not have access to the log files or if the files do not contain the information you want, consider moving the report to an inaccessible folder while you determine its actual status.  
  
### To create a shared data source (.rsds) file (SharePoint 2010)  
  
1.  Click the **Documents** tab on the library ribbon.  
  
2.  On the **New Document** menu, click **Report Data Source**  
  
    > [!NOTE]  
    >  If you do not see the **Report Data Source** item on the menu, the report data source content type has not been enabled. For more information, see [Add Report Server Content Types to a Library &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../2014/reporting-services/add-reporting-services-content-types-to-a-sharepoint-library.md).  
  
3.  In **Name**, enter a descriptive name for the .rsds file.  
  
4.  In **Data Source Type**, select the type of data source from the list. For more information, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md).  
  
5.  In **Connection String**, specify a pointer to the data source and any other settings that are necessary for establishing a connection to the external data source. The type of data source you are using determines the syntax of the connection string. For more information and examples, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
6.  In **Credentials**, specify how the report server obtains credentials to access the external data source. Credentials can be stored, prompted, integrated, or configured for unattended report processing.  
  
    -   Select **Windows authentication (integrated)** if you want to access the data using the credentials of the user who opened the report. Do not select this option if the SharePoint site or farm uses forms authentication or connects to the report server through a trusted account. Do not select this option if you want to schedule subscription or data processing for this report. This option works best when Kerberos authentication is enabled for your domain, or when the data source is on the same computer as the report server. If Kerberos authentication is not enabled, Windows credentials can only be passed to one other computer. This means that if the external data source is on another computer, requiring an additional connection, you will get an error instead of the data you expect.  
  
    -   Select **Prompt for credentials** if you want the user to enter his or her credentials each time he or she runs the report. Do not select this option if you want to schedule subscription or data processing for this report.  
  
    -   Select **Stored credentials** if you want to access the data using a single set of credentials. The credentials are encrypted before they are stored. You can select options that determine how the stored credentials are authenticated. Select Use as Windows credentials if the stored credentials belong to a Windows user account. Select **Set execution context to this account** if you want to set the execution context on the database server. For [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] databases, this option sets the SETUSER function. For more information, see [SETUSER &#40;Transact-SQL&#41;](/sql/t-sql/statements/setuser-transact-sql).  
  
    -   Select **Credentials are not required** if you want to specify credentials in the connection string, or if you want to run the report using a least-privilege account that is configured on the report server. If this account is not configured on the report server, users will be prompted for credentials and any scheduled operations that you define for the report will not run.  
  
7.  Select **Enable this data source** if you want the data source to be active. If the data source is configured but not active, users will see an error message when they attempt to use a report based on the data source.  
  
8.  Click the **Test Connection** button to validate the data source configuration.  
  
    > [!NOTE]  
    >  The Test Connection button is not supported for the XML data source type.  
  
9. Click **OK** to save create the shared data source.  
  
### To view dependent items  
  
1.  Open the library that contains the .rsds file.  
  
2.  Point to the shared data source.  
  
3.  Click to display a down arrow, and select **View Dependent Items**.  
  
     For report models, the list of dependent items shows the reports that were created in Report Builder. For shared data sources, the dependent items list can include both reports and report models.  
  
### To delete a shared data source (.rsds) file  
  
1.  Open the library that contains the .rsds file.  
  
2.  Point to the shared data source.  
  
3.  Click to display a down arrow, and click **Delete**.  
  
 If you mistakenly delete a shared data source that you meant to keep, you can create a new one that contains the same connection information. After you recreate the shared data source, you must open each report and model that used that data source and select the shared data source. The new shared data source item can have a different name, credentials, or connection string syntax from the one you delete. As long as the connection resolves to the same data source, data source properties can vary from the original values.  
  
 Use caution when deleting a report model. If you delete a model, you can no longer open and modify any reports that are based on that model in Report Builder. If you inadvertently delete a model that is used by existing reports, you must regenerate the model, re-create and save any reports that use the model, and re-specify any model item security that you want to use. You cannot simply regenerate the model and then attach it to an existing report.  
  
## See Also  
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
  
