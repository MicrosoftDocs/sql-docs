---
title: "Data Connections, Data Sources, and Connection Strings in Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "connections [Reporting Services], data sources"
  - "reports [Reporting Services], data"
  - "expressions [Reporting Services], data sources"
  - "data sources [Reporting Services], connections"
  - "connection strings [Reporting Services]"
  - "shared data sources [Reporting Services]"
  - "Reporting Services, data sources"
  - "logins [Reporting Services]"
ms.assetid: 4d8f0ae1-102b-4b3d-9155-fa584c962c9e
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Data Connections, Data Sources, and Connection Strings in Reporting Services
  To include data in a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report, you must first create *data sources* and *datasets*. This topic explains the type of data sources, how to create data sources, and important information related to data source credentials. A data source includes the data source type, connection information, and the type of credentials to use. There are two types of data sources: embedded and shared. An embedded data source is defined in the report and used only by that report. A shared data source is defined independently from a report and can be used by multiple reports. For more information, see [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/embedded-and-shared-data-connections-or-data-sources-report-builder-and-ssrs.md) and [Embedded and Shared Datasets &#40;Report Builder and SSRS&#41;](report-data/embedded-and-shared-datasets-report-builder-and-ssrs.md).  
  
||  
|-|  
|**[!INCLUDE[applies](../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  

  
##  <a name="bkmk_data_sources"></a> Embedded and shared data sources  
 The difference between the embedded and shared data sources is in how they are created, stored, and managed.  
  
-   In Report Designer, create embedded or shared data sources as part of a [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] project. You can control whether to use them locally for preview or to deploy them as part of the project to a report server or SharePoint site. You can use custom data extensions that have been installed on your computer and on the report server or SharePoint site where you deploy your reports.  
  
     System administrators can install and configure additional data processing extensions and .NET Framework data providers. For more information, see [Data Processing Extensions and .NET Framework Data Providers &#40;SSRS&#41;](report-data/data-processing-extensions-and-net-framework-data-providers-ssrs.md).  
  
     Developers can use the <xref:Microsoft.ReportingServices.DataProcessing> API to create data processing extensions to support additional types of data sources.  
  
-   In Report Builder, browse to a report server or SharePoint site and select shared data sources or create embedded data sources in the report. You cannot create a shared data source in Report Builder. You cannot use custom data extensions in Report Builder  
  
##  <a name="bkmk_DataConnections"></a> Built-in data extensions  
 Default data extensions in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] include the following types of data connections:  
  
-   Microsoft SQL Server  
  
-   Microsoft SQL Server Analysis Services  
  
-   Microsoft SharePoint List  
  
-   [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)]  
  
-   Microsoft SQL Server Parallel Data Warehouse  
  
-   OLE DB  
  
-   Oracle  
  
-   SAP NetWeaver BI  
  
-   Hyperion Essbase  
  
-   Teradata  
  
-   XML  
  
-   ODBC  
  
-   Microsoft BI Semantic Model for Power View : On a SharePoint site that has been configured for a PowerPivot gallery and [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)], this data source type is available. This data source type is used only for [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] presentations. For more information, see [Building the Perfect BI Semantic Tabular Models for Power View](https://technet.microsoft.com/video/building-the-perfect-bi-semantic-tabular-models-for-power-view.aspx).  
  
 For a full list of data sources and versions [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] supports, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md).  
  
##  <a name="bkmk_create_data_source"></a> Create a data source  
 To create a data source, you must have the following information:  
  
-   **Data source type** The connection type, for example, [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Choose this value from the drop-down list of connection types.  
  
-   **Connection information** Connection information includes the name and location of the data source, and connection properties that are specific to each data provider. The *connection string* is the text representation of connection information. For example, if the data source is a SQL Server database, you can specify the name of the database. For embedded data sources, you can also write expression-based connection strings that are evaluated at run time. For more information, see [Expression-based Connection Strings](#Expressions) later in this topic.  
  
-   **Credentials** You provide the credentials that are needed to access the data. The data source owner must have granted you the appropriate permissions to access both the data source and the specific data on the data source. For example, to connect to the [!INCLUDE[ssSampleDBnormal](../includes/sssampledbnormal-md.md)] sample database installed on a network server, you must have permission to connect to the server and also read-only permission to access the database.  
  
    > [!NOTE]  
    >  By design, credentials are managed independently from data sources. Credentials that you use to preview your report on a local system may differ from credentials you need to view your published report. After you save a data source to the report server or SharePoint site, you might need to change the credentials to work from that location. For more information, see [Credentials for Data Sources](#bkmk_credentials).  
  
> [!NOTE]  
>  When you create an embedded data source for a report in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], you must create the data source in Report Designer in either Solution Explorer or the Report Data pane, but not in Server Explorer. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Report Designer does not support [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] data sources created in Server Explorer.  
  
 The Report Data pane displays embedded data sources and references to shared data sources that have been added to the report. In Report Builder, a shared data source reference points to a shared data source on a report server or SharePoint site. In Report Designer, a shared data source reference points to a shared data source in Solution Explorer under the Shared Data Source folder.  
  
##  <a name="bkmk_credentials"></a> Credentials for data sources  
 By design, credentials can be saved and managed independently from connection information. Credentials are used to create a data source, to run a dataset query, and to preview a report.  
  
> [!NOTE]  
>  We recommend that you do not include login information, such as login names and passwords, to the connection properties of the data source. Use shared data sources with stored credentials whenever possible. In an authoring environment, use the Credentials page of the **Data Source** dialog box to enter credentials when you create a data connection or run a dataset query.  
  
 Credentials that you enter for data access from your computer are stored securely in the local project configuration file and are specific to your computer. If you copy the project files to another computer, you must redefine the credentials for the data source.  
  
 When you deploy a report to the report server or SharePoint site, its embedded and shared data sources are managed independently. The data source credentials needed to access the data from your computer may differ from the credentials needed for the report server to access the data.  
  
 ![note](media/rs-fyinote.png "note")A good practice is to verify that the data source connections continue to connect successfully after you publish a report. If you need to change the credentials, you can modify them directly on the report server.  
  
 To change the data sources that a report uses, you can modify the report properties in Native mode Report Manager or from document libraries in SharePoint mode. For more information, see the following:  
  
-   [Store Credentials in a Reporting Services Data Source](report-data/store-credentials-in-a-reporting-services-data-source.md) [Store Credentials in a Reporting Services Data Source](report-data/store-credentials-in-a-reporting-services-data-source.md)  
  
-   [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)  
  
-   [Specify Connections for Custom Data Processing Extensions](report-data/specify-connections-for-custom-data-processing-extensions.md)  
  
-   [Specify Credentials in Report Builder](../../2014/reporting-services/specify-credentials-in-report-builder.md)  
  
-   [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
##  <a name="bkmk_connection_examples"></a> Common connection string examples  
 Connection strings are the text representation of connection properties for a data provider. The following table lists examples of connections strings for various data connection types.  
  
|**Data source**|**Example**|**Description**|  
|---------------------|-----------------|---------------------|  
|SQL Server database on the local server|`data source="(local)";initial catalog=AdventureWorks`|Set data source type to `Microsoft SQL Server`. For more information, see [SQL Server Connection Type &#40;SSRS&#41;](report-data/sql-server-connection-type-ssrs.md).|  
|SQL Server database on the local server|`data source="(local)";initial catalog=AdventureWorks`|Set data source type to `Microsoft SQL Server`.|  
|SQL Server Instance<br /><br /> database|`Data Source=localhost\MSSQL10_50.InstanceName; Initial Catalog=AdventureWorks`|Set data source type to `Microsoft SQL Server`.|  
|SQL Server Express database|`Data Source=localhost\MSSQL10_50.SQLEXPRESS; Initial Catalog=AdventureWorks`|Set data source type to `Microsoft SQL Server`.|  
|[!INCLUDE[ssSDS](../includes/sssds-md.md)] in the cloud|`Data Source=<host>;Initial Catalog=AdventureWorks; Encrypt=True`|Set data source type to `Windows Azure SQL Database`. For more information, see [SQL Azure Connection Type &#40;SSRS&#41;](report-data/sql-azure-connection-type-ssrs.md).|  
|SQL Server Parallel Data Warehouse|`HOST=<IP address>;database= AdventureWorks; port=<port>`|Set data source type to `Microsoft SQL Server Parallel Data Warehouse`. For more information, see [SQL Server Parallel Data Warehouse Connection Type &#40;SSRS&#41;](report-data/sql-server-parallel-data-warehouse-connection-type-ssrs.md).|  
|Analysis Services database on the local server|`data source=localhost;initial catalog=Adventure Works DW`|Set data source type to `Microsoft SQL Server Analysis Services`. For more information, see [Analysis Services Connection Type for MDX &#40;SSRS&#41;](report-data/analysis-services-connection-type-for-mdx-ssrs.md) or [Analysis Services Connection Type for DMX &#40;SSRS&#41;](report-data/analysis-services-connection-type-for-dmx-ssrs.md).|  
|Analysis Services tabular model database with Sales perspective|`Data source=<servername>;initial catalog= Adventure Works DW;cube='Sales'`|Set data source type to `Microsoft SQL Server Analysis Services`. Specify perspective name in cube= setting. For more information, see [Perspectives &#40;SSAS Tabular&#41;](../analysis-services/tabular-models/perspectives-ssas-tabular.md).|  
|Report model data source on a report server configured in native mode|`Server=http://myreportservername/reportserver; datasource=/models/Adventure Works`|Specify the report server or document library URL and the path to the published model in the report server folder or document library folder namespace. For more information, see [Report Model Connection &#40;SSRS&#41;](report-data/report-model-connection-ssrs.md).|  
|Report model data source on a report server configured in SharePoint integrated mode|`Server=http://server; datasource=http://server/site/documents/models/Adventure Works.smdl`|Specify the report server or document library URL and the path to the published model in the report server folder or document library folder namespace.|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2000 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] server|`provider=MSOLAP.2;data source=<remote server name>;initial catalog=FoodMart 2000`|Set the data source type to `OLE DB Provider for OLAP Services 8.0`.<br /><br /> You can achieve a faster connection to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2000 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] data sources if you set the `ConnectTo` property to `8.0`. To set this property, use the **Connection Properties** dialog box, **Advanced Properties** tab.|  
|Oracle server|`data source=myserver`|Set the data source type to `Oracle`. The Oracle client tools must be installed on the Report Designer computer and on the report server. For more information, see [Oracle Connection Type &#40;SSRS&#41;](report-data/oracle-connection-type-ssrs.md).|  
|SAP NetWeaver BI data source|`DataSource=http://mySAPNetWeaverBIServer:8000/sap/bw/xml/soap/xmla`|Set the data source type to `SAP NetWeaver BI`. For more information, see [SAP NetWeaver BI Connection Type &#40;SSRS&#41;](report-data/sap-netweaver-bi-connection-type-ssrs.md).|  
|Hyperion Essbase data source|`Data Source=http://localhost:13080/aps/XMLA; Initial Catalog=Sample`|Set the data source type to `Hyperion Essbase`. For more information, see [Hyperion Essbase Connection Type &#40;SSRS&#41;](report-data/hyperion-essbase-connection-type-ssrs.md).|  
|Teradata data source|`data source=`\<NNN>.\<NNN>.\<NNN>.\<NNN>`;`|Set the data source type to `Teradata`. The connection string is an Internet Protocol (IP) address in the form of four fields, where each field can be from one to three digits. For more information, see [Teradata Connection Type &#40;SSRS&#41;](report-data/teradata-connection-type-ssrs.md).|  
|XML data source, Web service|`data source=http://adventure-works.com/results.aspx`|Set the data source type to `XML`. The connection string is a URL for a web service that supports Web Services Definition Language (WSDL). For more information, see [XML Connection Type &#40;SSRS&#41;](report-data/xml-connection-type-ssrs.md).|  
|XML data source, XML document|`http://localhost/XML/Customers.xml`|Set the data source type to `XML`. The connection string is a URL to the XML document.|  
|XML data source, embedded XML document|*Empty*|Set the data source type to `XML`. The XML data is embedded in the report definition.|  
  
 If you fail to connect to a report server using `localhost`, check that the network protocol for TCP/IP protocol is enabled. For more information, see [Configure Client Protocols](../database-engine/configure-windows/configure-client-protocols.md).  
  
 For more information about the configurations needed to connect to these data source types, see the specific data connection topic under [Add Data from External Data Sources &#40;SSRS&#41;](report-data/add-data-from-external-data-sources-ssrs.md) or [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md).  
  
##  <a name="bkmk_special_password_characters"></a> Special characters in a password  
 If you configure your ODBC or SQL data source to prompt for a password or to include the password in the connection string, and a user enters the password with special characters like punctuation marks, some underlying data source drivers cannot validate the special characters. When you process the report, the message "Not a valid password" may indicate this problem. If changing the password is impractical, you can work with your database administrator to store the appropriate credentials on the server as part of a system ODBC data source name (DSN). For more information, see "OdbcConnection.ConnectionString" in the [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] SDK documentation.  
  
##  <a name="bkmk_Expressions_in_connection_strings"></a> Expression-based Connection Strings  
 Expression-based connection strings are evaluated at run time. For example, you can specify the data source as a parameter, include the parameter reference in the connection string, and allow the user to choose a data source for the report. For example, suppose a multinational firm has data servers in several countries. With an expression-based connection string, a user who is running a sales report can select a data source for a particular country before running the report.  
  
 The following example illustrates the use of a data source expression in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] connection string. The example assumes you have created a report parameter named `ServerName`:  
  
```  
="data source=" & Parameters!ServerName.Value & ";initial catalog=AdventureWorks"  
```  
  
 Data source expressions are processed at run time or when a report is previewed. The expression must be written in [!INCLUDE[vbprvb](../includes/vbprvb-md.md)]. Use the following guidelines when defining a data source expression:  
  
-   Design the report using a static connection string. A static connection string refers to a connection string that is not set through an expression (for example, when you follow the steps for creating a report-specific or shared data source, you are defining a static connection string). Using a static connection string allows you to connect to the data source in Report Designer so that you can get the query results you need to create the report.  
  
-   When defining the data source connection, do not use a shared data source. You cannot use a data source expression in a shared data source. You must define an embedded data source for the report.  
  
-   Specify credentials separately from the connection string. You can use stored credentials, prompted credentials, or integrated security.  
  
-   Add a report parameter to specify a data source. For parameter values, you can either provide a static list of available values (in this case, the available values should be data sources you can use with the report) or define a query that retrieves a list of data sources at run time.  
  
-   Be sure that the list of data sources shares the same database schema. All report design begins with schema information. If there is a mismatch between the schema used to define the report and the actual schema used by the report at run time, the report might not run.  
  
-   Before publishing the report, replace the static connection string with an expression. Wait until you are finished designing the report before you replace the static connection string with an expression. Once you use an expression, you cannot execute the query in Report Designer. Furthermore, the field list in the Report Data pane and the Parameters list will not update automatically.  
  
## See Also  
 [Embedded and Shared Data Connections or Data Sources &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/embedded-and-shared-data-connections-or-data-sources-report-builder-and-ssrs.md)   
 [Manage Report Data Sources](report-data/manage-report-data-sources.md)   
 [Data Source Properties Dialog Box, Credentials](../../2014/reporting-services/data-source-properties-dialog-box-credentials.md)   
 [Shared Data Source Properties Dialog Box, Credentials](../../2014/reporting-services/shared-data-source-properties-dialog-box-credentials.md)   
 [Create, Modify, and Delete Shared Data Sources &#40;SSRS&#41;](report-data/create-modify-and-delete-shared-data-sources-ssrs.md)   
 [Set Deployment Properties &#40;Reporting Services&#41;](tools/set-deployment-properties-reporting-services.md)   
 [Specify Credential and Connection Information for Report Data Sources](report-data/specify-credential-and-connection-information-for-report-data-sources.md)   
 [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
  
