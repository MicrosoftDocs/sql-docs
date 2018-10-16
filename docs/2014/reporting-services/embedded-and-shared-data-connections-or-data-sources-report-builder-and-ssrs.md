---
title: "Embedded and Shared Data Connections or Data Sources (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "embedded data sources"
  - "shared data sources"
  - "data sources"
ms.assetid: f417782c-b85a-4c4d-8a40-839176daba56
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Embedded and Shared Data Connections or Data Sources (Report Builder and SSRS)
  Reports use data connections to retrieve data for a report when a query runs or when the report is processed. You choose from a list of built-in data connection types to connect to a relational database, a multidimensional database, a Web service, or some other source of data. The following terms are used when describing data connections.  
  
-   **Data connection.** Also known as a *data source*. A data connection includes a name and connection properties that are dependent on the connection type. By design, a data connection does not include credentials. A data connection does not specify which data to retrieve from the external data source. To do that, you specify a query when you create a dataset.  
  
-   **Data source definition.** A file that contains the XML representation of a report data source. When a report is published, its data sources are saved on the report server or SharePoint site as data source definitions, independently from the report definition. For example, a report server administrator might update the connection string or credentials. On a native report server, the file type is .rds. On a SharePoint site, the file type is .rsds.  
  
-   **Connection string.** A connection string is a string version of the connection properties that are needed to connect to a data source. Connection properties differ based on data connection type. For examples, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
-   **Shared data source.** A data source that is available on a report server or SharePoint site to be used by multiple reports.  
  
-   **Embedded data source.** Also known as a *report-specific data source*. A data source that is defined in a report and used only by that report.  
  
-   **Credentials.** Credentials are the authentication information that must be provided to allow you access to external data.  
  
 The difference between the embedded and shared data sources is in how they are created, stored, and managed.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../includes/ssrbrddup-md.md)]  
  
## Shared Data Sources  
 Shared data sources are useful when you have data sources that you use often. It is recommended that you use shared data sources as much as possible. They make reports and report access easier to manage, and help to keep reports and the data sources they access more secure. If you need a shared data source, ask your system administrator to create one for you.  
  
 In Report Builder, you cannot create a shared data source. You can browse to and select a shared data source from the report server. For more information, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-report-builder.md).  
  
 In Report Designer, you cannot browse to a shared data source on the report server. You can create shared data sources as part of a project in Solution Explorer and choose whether to deploy them to a report server. You might choose to use them locally only because of differences in credentials required from your computer or from the report server. For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
 The following icon indicates a shared data source item in the report server folder hierarchy: ![Shared data source icon](media/hlp-16datasource.png "Shared data source icon")  
  
## Embedded Data Sources  
 An embedded data source is a data connection that is saved in the report definition. Embedded data source connection information can be used only by the report in which it is embedded. To define and manage embedded data sources, use the **Data Source Properties** dialog box.  
  
##  <a name="Comparing"></a> Comparing Embedded and Shared Data Sources  
 The following table summarizes the differences between embedded and shared data sources:  
  
|Description|Embedded<br /><br /> Data Source|Shared<br /><br /> Data Source|  
|-----------------|------------------------------|----------------------------|  
|Data connection is embedded in the report definition.|![Available](media/greencheck.gif "Available")||  
|Pointer to the data connection on the report server is embedded in the report definition.||![Available](media/greencheck.gif "Available")|  
|Managed on the report server|![Available](media/greencheck.gif "Available")|![Available](media/greencheck.gif "Available")|  
|Required for shared datasets||![Available](media/greencheck.gif "Available")|  
|Required for components||![Available](media/greencheck.gif "Available")|  
  
## Data Source Credentials  
 Credentials are used to create an embedded data source, to run a query, or to retrieve data during report processing. The owner of the data source determines the type of credentials that you must use to access the data. Credentials are managed independently from the data connection on a report server, a SharePoint site, or on a local computer in a report authoring environment. Depending on the type of data source, credentials can be saved to avoid prompting or set to prompt each user. The credentials that you need might differ depending on whether you are connecting to the data source from your computer or from the report server. For more information, see [Specify Credentials in Report Builder](../../2014/reporting-services/specify-credentials-in-report-builder.md) and [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../2014/reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
## See Also  
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-data/report-datasets-ssrs.md)   
 [Report Authoring Concepts &#40;Report Builder and SSRS&#41;](report-design/report-authoring-concepts-report-builder-and-ssrs.md)   
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md)   
 [Add and Verify a Data Connection or Data Source &#40;Report Builder and SSRS&#41;](report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)   
 [Embedded and Shared Datasets &#40;Report Builder and SSRS&#41;](report-data/embedded-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
