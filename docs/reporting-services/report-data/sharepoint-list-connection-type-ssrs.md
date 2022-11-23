---
title: "SharePoint List Connection Type | Microsoft Docs"
description: Use the information in this article about the SharePoint List connection type to learn how to build a data source.
ms.date: 05/10/2017
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
ms.assetid: 2c4adf2f-e9c4-4fae-bd3c-97fe64436caf
author: maggiesMSFT
ms.author: maggies
---
# SharePoint List Connection Type (SSRS)

[!INCLUDE[ssrs-appliesto-sql2016-preview](../../includes/ssrs-appliesto-sql2016-preview.md)] [!INCLUDE [ssrs-appliesto-sharepoint-2013-2016](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

To include data from a Microsoft SharePoint list in your report, you must add or create a dataset that is based on a report data source of type Microsoft SharePoint List. This is a built-in data source type based on the Microsoft SQL Server Reporting Services SharePoint List data extension. Use this data source type to connect to and retrieve list data from SharePoint 2013 and later.

Use the information in this topic to build a data source. For step-by-step instructions, see [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md).  

##  <a name="Connection"></a> Connection String  
 The connection string to a SharePoint list is the URL to the SharePoint site or subsite, for example, `https://MySharePointWeb/MySharePointSite` or `https://MySharePointWeb/MySharePointSite/Subsite`.  
  
 The query designer automatically displays the SharePoint lists that you have sufficient permissions to access.  
  
 For more connection string examples, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).  
  
##  <a name="Credentials"></a> Credentials  
 Credentials are required to run queries, to preview the report locally, and to preview the report from the report server. After you publish your report, you may need to change the credentials for the data source so that when the report runs on the report server, the permissions to retrieve the data are valid. The types of credentials that can be used with this data extension depend on the SharePoint technology configuration for the SharePoint list that you are using as a data source.  
  
 The following tables outline credential retrieval behavior for the SharePoint list extension, when connecting to a local farm SharePoint list and to a remote SharePoint list.  
  
 **Table 1** is for reports deployed to a legacy Windows SharePoint Site. A legacy Windows site supports only Kerberos, NTLM, and Forms Based Authentication (FBA). **Table 2** is for reports deployed to a Claims-based SharePoint site.  
  
 **Table 1**  
  
|Retrieval Type|Supported Credentials|Classic Mode Windows Authentication|*Claims Authentication|  
|-|---------------------------|-----------------------------------------|-----------------------------|  
|Local farm SharePoint List|Windows Authentication (integrated) or SharePoint User Token|Yes|Yes|  
||Stored, Prompt, None (with Windows credentials)<br /><br /> Stored and prompt credentials with non-Windows credentials is not supported.|Yes|No|  
|Remote SharePoint List|Windows Authentication (integrated) or SharePoint User Token|Yes|No<br /><br /> Forms-based authentication and Claims authentication are not supported for remote SharePoint lists.|  
||Stored, Prompt, None (with Windows credentials)<br /><br /> Stored and prompt credentials with non-Windows credentials is not supported.|Yes|No<br /><br /> Forms-based authentication and Claims authentication are not supported for remote SharePoint lists.|  
  
 *Windows authentication, Forms Based authentication (FBA), Secure Application Markup Language (SAML) tokens, other identity providers or a combination of more than one of the above mentioned authentication providers.  
  
 **Table 2**  
  
|Retrieval Type|Supported Credentials|Classic Mode Windows Authentication|*Claims Authentication|  
|-|---------------------------|-----------------------------------------|-----------------------------|  
|Local Farm SharePoint List|Windows Authentication (integrated) or SharePoint User Token|Yes|Yes|  
||Stored, Prompt, None (with Windows credentials)<br /><br /> Stored and prompt credentials with non-Windows credentials is not supported.|No|No|  
|Remote SharePoint List|Windows Authentication (integrated) or SharePoint User Token|Yes|No<br /><br /> Forms-based authentication and Claims authentication are not supported for remote SharePoint lists.|  
||Stored, Prompt, None (with Windows credentials)<br /><br /> Stored and prompt credentials with non-Windows credentials is not supported.|No|No<br /><br /> Forms-based authentication and Claims authentication are not supported for remote SharePoint lists.|  
  
 *Windows authentication, Forms Based authentication (FBA), Secure Application Markup Language (SAML) tokens, other identity providers or a combination of more than one of the above mentioned authentication providers.  
  
 **Windows Authentication**  
 For a SharePoint technology that is configured to work with a report server in Trusted Account mode, this option is not supported. This applies only to releases prior to SQL Server 2012 Reporting Services.

 For a SharePoint technology that is configured to work with a report server in Windows Integrated mode, this option applies to both the current Windows user and the current SharePoint user.
 
 For a SharePoint technology that is configured to work without a Report Server (local mode), this option is not supported. For more information on local mode, see [Local Mode vs. Connected Mode Reports in the Report Viewer &#40;Reporting Services in SharePoint Mode&#41;](../../reporting-services/report-server-sharepoint/local-mode-vs-connected-mode-reports-in-the-report-viewer.md).  
  
 **Credentials are not required (Do not use credentials):**  
 To use this option, the unattended execution account must be configured on the report server. For more information, see [Configure the Unattended Execution Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md).  
  
 For information about Claims authentication support across the Microsoft BI stack, see [Using Claims Authentication across the Microsoft BI Stack](https://social.technet.microsoft.com/wiki/contents/articles/15274.using-claims-authentication-across-the-microsoft-bi-stack.aspx).  
  
 For more information, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md), [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md), and [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
##  <a name="Query"></a> Queries  
 To design a query, create a new dataset based on the data source, and then open the associated query designer. For more information, see [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md).  
  
 The SharePoint List graphical query designer displays four panes:  
  
 **SharePoint Lists**  Displays a list of all the SharePoint lists on the site for this data source. Select a list and then select the fields that you want in your query. The names of fields in this pane are the SharePoint friendly names, also known as display names. Hover over an item to display the following properties in the tooltip:  
  
-   **Name** The unique name of the field.  
  
-   **Identifier** The unique identifier of the field.  
  
-   **Field Type** The data type of the field.  
  
-   **Hidden** Whether the field displays in the SharePoint list view.  
  
 Selecting fields from multiple lists is not supported. You can create a dataset for each list and select fields from each dataset. If the lists have a common field, you can use the Lookup function in a tablix data region that is bound to one dataset to retrieve a value from the other dataset that is not bound to the data region. For more information, see [Lookup Function &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-builder-functions-lookup-function.md).  
  
-   **Selected Fields**  Displays the fields that you have selected. The names of fields in this pane are friendly names that a SharePoint user has specified. When you close the query designer, you see these names in the dataset field collection in the Report Data pane. The relationship between unique names and friendly names is available in the [Dataset Properties Dialog Box, Fields &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md) page.  
  
-   **Applied Filters**  Limits the data that is returned from the SharePoint list, before the data is returned to the report. Select the field name, operator, and value to use to limit the data that is retrieved in the list. The operators vary depending on the data type of the value that you select.  
  
     You cannot change the sort order or specify groups in the graphical query designer. To do that, set sort expressions on the report dataset, and group expressions on the data regions in the report. Query parameters are not supported. To filter data in the report, use report filters or report parameters that you create. For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md) and [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
-   **Query Results**  Displays example rows that are returned when the query runs. If the SharePoint list values change frequently on the SharePoint site, the values that you see in the query results pane might differ from the values that you see in the report.  
  
-   **Selected Fields**  Displays the fields that you have selected. The names of fields in this pane are friendly names that a SharePoint user has specified. When you close the query designer, you see these names in the dataset field collection in the Report Data pane. The relationship between unique names and friendly names is available in the [Dataset Properties Dialog Box, Fields &#40;Report Builder&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md) page.  
  
-   **Applied Filters**  Limits the data that is returned from the SharePoint list, before the data is returned to the report. Select the field name, operator, and value to use to limit the data that is retrieved in the list. The operators vary depending on the data type of the value that you select.  
  
     You cannot change the sort order or specify groups in the graphical query designer. To do that, set sort expressions on the report dataset, and group expressions on the data regions in the report. Query parameters are not supported. To filter data in the report, use report filters or report parameters that you create. For more information, see [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md) and [Report Parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md).  
  
-   **Query Results**  Displays example rows that are returned when the query runs. If the SharePoint list values change frequently on the SharePoint site, the values that you see in the query results pane might differ from the values that you see in the report.  
  
 For more information, see [SharePoint List Query Designer &#40;Report Builder&#41;](../../reporting-services/report-data/sharepoint-list-query-designer-report-builder.md).  
  
### Query Text  
 To view the query that is generated by the graphical query designer, switch to the text-based query designer. In this view, you can see the XML that is created by the graphical query designer. The XML includes elements for the list name, the field collection, and the filter.  
  
#### Example 1. Specified fields for a list  
 The following example shows a well-formed SharePoint query:  
  
```  
<RSSharePointList>  
<listName>MyList</listName>  
<viewFields>  
  <FieldRef Name="Field1"/>  
  <FieldRef Name="Field4"/>  
</viewFields>  
<Query>  
  <Where>  
    <And>  
      <Gt>  
        <FieldRef Name="Field1"/>  
        <Value Type="Integer">1</Value>  
      </Gt>  
      <IsNotNull>  
        <FieldRef Name="Field2"/>  
        <Value Type="string"/>  
      </IsNotNull>   
    </And>  
  </Where>  
</Query>  
</RSSharePointList>  
```  
  
 You can edit this view of the query as long as it remains well-formed XML text.  
  
#### Example 2. All fields for a list  
 You can also specify only the name of a list, and all fields, including hidden fields, are returned. The following example retrieves all the fields from a list that is named Tasks:  
  
```  
<RSSharePointList>  
<listName>Tasks</listName>  
</RSSharePointList>  
```  
  
 All fields for the list Tasks are returned in the query results.  
  
##  <a name="Parameters"></a> Parameters  
 Parameters are not supported by this data extension.  
  
##  <a name="HowTo"></a> How-To Topics  
 This section contains step-by-step instructions for working with data connections, data sources, and datasets.  
  
 [Add and Verify a Data Connection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-and-verify-a-data-connection-report-builder-and-ssrs.md)  
  
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)  
  
 [Add a Filter to a Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/add-a-filter-to-a-dataset-report-builder-and-ssrs.md)  
  
##  <a name="Related"></a> Related Sections  
 These sections of the documentation provide in-depth conceptual information about report data, as well as procedural information about how to define, customize, and use parts of a report that are related to data.  
  
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)  
 Provides an overview of accessing data for your report.  
  
 [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md)  
 Provides information about data connections and data sources.  
  
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
 Provides information about embedded and shared datasets.  
  
 [Dataset Fields Collection &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/dataset-fields-collection-report-builder-and-ssrs.md)  
 Provides information about the dataset field collection generated by the query.  
  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
 Provides in-depth information about platform and version support for each data extension.  

## See Also

[Report Parameters](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
[Filter, Group, and Sort Data](../../reporting-services/report-design/filter-group-and-sort-data-report-builder-and-ssrs.md)   
[Expressions &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/expressions-report-builder-and-ssrs.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
