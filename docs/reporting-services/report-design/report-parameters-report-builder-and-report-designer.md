---
title: "Paginated report parameters"
description: Learn the common uses for Reporting Services paginated report parameters and the properties you can set.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/24/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5

#customer intent: As a Report Builder user, I want to learn how to use parameters so that I can control and manage my report data.
---

# Paginated report parameters (Report Builder)

::: moniker range="=sql-server-2016"

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

::: moniker-end

::: moniker range=">=sql-server-2017"

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)]

::: moniker-end

This article describes the common uses for paginated report parameters, the properties you can set, and more. Report parameters enable you to control report data, connect related reports together, and vary report presentation. You can use report parameters in paginated reports you create in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] and Report Designer, and also in mobile reports you create in [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]. For more information, see [Report parameters concepts in paginated reports (Report Builder)](../../reporting-services/report-design/report-parameters-concepts-report-builder-and-ssrs.md).  

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

For more information about how to add a parameter to a report, see [Tutorial: Add a parameter to your report (Report Builder)](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md).  

## <a name="bkmk_Common_Uses_for_Parameters"></a> Common uses for parameters

Common uses for parameters include:

    - Control your data with filters that you can adjust with a customizeable interface. For more information, see [Control paginated and mobile report data](#control-paginated-and-mobile-report-data).
    - Connect your main report with drillthrough reports that each solve a specific question. For more information, see [Connect related reports](#connect-related-reports).
    - Vary the presentation of your report by adjusting the rendering. For more information, see [Vary report presentation](#vary-report-presentation).
  
### Control paginated and mobile report data

[!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] and Report Designer give you options for filtering data based on the type of data you have:
  
- Filter paginated report data at the data source by writing dataset queries that contain variables.  
  
- Filter data from a shared dataset. When you add a shared dataset to a paginated report, you can't change the query. In the report, you can add a dataset filter that includes a reference to a report parameter that you create.  
  
- Filter data from a shared dataset in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] mobile report. For more information, see [Create mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md).  
  
- Enable users to specify values to customize the data in a paginated report. For example, provide two parameters for the start date and end date for sales data.  
  
### Connect related reports
  
Use parameters to relate main reports to drillthrough reports, subreports, and linked reports. When you design a set of reports, you can design each report to answer certain questions. Each report can provide a different view or a different level of detail for related information. To provide a set of interrelated reports, create parameters for the related data on target reports.  
  
- For more information, see [Drillthrough reports in a paginated report (Report Builder)](../../reporting-services/report-design/drillthrough-reports-report-builder-and-ssrs.md), [Subreports in paginated reports (Report Builder)](../../reporting-services/report-design/subreports-report-builder-and-ssrs.md), and [Create a linked report](../../reporting-services/reports/create-a-linked-report.md).  

Customize sets of parameters for multiple users. Create two linked reports based on a sales report on the report server. One linked report uses predefined parameter values for sales persons, and the second linked report uses predefined parameter values for sales managers. Both reports use the same report definition.  
  
### Vary report presentation
  
To customize the rendering of a report, send commands to a report server through a URL request. For more information, see [URL access (SSRS)](../../reporting-services/url-access-ssrs.md) and [Pass a report parameter within a URL](../../reporting-services/pass-a-report-parameter-within-a-url.md).  
  
Enable users to specify values to help customize the appearance of a report. For example, provide a Boolean parameter to indicate whether to expand or collapse all nested row groups in a table.  
  
Enable users to customize report data and appearance by including parameters in an expression.  
  
- For more information, see [Parameters collection references in a paginated report (Report Builder)](../../reporting-services/report-design/built-in-collections-parameters-collection-references-report-builder.md).  
  
## <a name="UserInterface"></a> View a report with parameters

When you view a report that has parameters, the **Parameters** pane displays each parameter so you can interactively specify values. The following image shows the **Parameters** pane for a report with parameters @ReportMonth, @ReportYear, @EmployeeID, @ShowAll, @ExpandTableRows, @CategoryQuota, and @SalesDate.  

:::image type="content" source="../../reporting-services/report-design/media/ssrb-rptparamviewrpt.png" alt-text="Screenshot that shows the Parameters pane highlighting different parameters and interface tools.":::
  
1. **Parameters pane**: The **Parameters** pane displays a prompt and default value for each parameter. You can customize the layout of parameters. For more information, see [Customize the parameters pane in a paginated report (Report Builder)](../../reporting-services/report-design/customize-the-parameters-pane-in-a-report-report-builder.md).  
  
1. **@SalesDate parameter**: The parameter @SalesDate is data type **Date/Time**. The prompt **Select the Date** appears next to the text box. To modify the date, type a new date in the text box or use the calendar control.  
  
1. **@ShowAll parameter**: The parameter @ShowAll is data type **Boolean**. Use the radio buttons to specify **True** or **False**.  
  
1. **Show or Hide Parameter Area handle**: On the report viewer toolbar, select this arrow to show or hide the **Parameters** pane.  
  
1. **@CategoryQuota parameter**: The parameter @CategoryQuota is data type **Float**, so it takes a numeric value. @CategoryQuota is set to allow multiple values.  
  
1. **View Report**:  After you enter parameter values, select **View Report** to run the report. If all parameters have default values, the report runs automatically on first view.  
  
## <a name="bkmk_Create_Parameters"></a> Create parameters

You can create report parameters in a few different ways:

    - Use a dataset query with variables or stored procedures containing parameters. For more information, see [Dataset query or stored procedure with parameters](#dataset-query-or-stored-procedure-with-parameters).
    - Manually create a parameter. For more information see [Create a parameter manually](#create-a-parameter-manually).
    - Add a report part containing references to a parameter. For more information, see [Report part with a parameter](#report-part-with-a-parameter).
  
> [!NOTE]
> Not all data sources support parameters.
  
### Dataset query or stored procedure with parameters
  
Add a dataset query that contains variables or a dataset stored procedure that contains input parameters. A dataset parameter is created for each variable or input parameter, and a report parameter is created for each dataset parameter.  

:::image type="content" source="../../reporting-services/report-design/media/ssrb-paramdatasetprops.png" alt-text="Screenshot that shows Report Builder highlighting the Report Data pane, Parameters pane, and Dataset Properties dialog box.":::
  
This image from Report Builder shows:  
  
1. The report parameters in the **Report Data** pane.  
  
1. The dataset with the parameters.  
  
1. The **Parameters** pane.  
  
1. The parameters listed in the **Dataset Properties** dialog box.  
  
The dataset can be embedded or shared. When you add a shared dataset to a report, dataset parameters that are marked internal can't be overridden in the report. You can override dataset parameters that aren't marked internal.  
  
 For more information, see [Dataset Query](#bkmk_Dataset_Parameters).  
  
### Create a parameter manually
  
Create a parameter manually from the **Report Data** pane. You can configure report parameters so that a user can interactively enter values to help customize the contents or appearance of a report. You can also configure report parameters so that a user can't change preconfigured values.  
  
> [!NOTE]  
> Because parameters are managed independently on the server, republishing a main report with new parameter settings doesn't overwrite the existing parameters settings on the report.  
  
### Report part with a parameter

[!INCLUDE [ssrs-report-parts-deprecated](../../includes/ssrs-report-parts-deprecated.md)]

Add a report part that contains references to a parameter or to a shared dataset that contains variables.  
  
Report parts are stored on the report server and are available for others to use in their reports. Report parts that are parameters can't be managed from the report server. You can search for parameters in the **Report Part Gallery**. After you add them, you can configure them in your report. For more information, see [Report Parts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md).  
  
> [!NOTE]  
> Parameters can be published as a separate report part for data regions that have dependent datasets with parameters. Although parameters are listed as a report part, you can't add a report part parameter directly to a report. Instead, add the report part, and any necessary report parameters are automatically generated from dataset queries that are contained or referenced by the report part. For more information about report parts, see [Report Parts &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/report-parts-report-builder-and-ssrs.md) and [Report Parts in Report Designer &#40;SSRS&#41;](../../reporting-services/report-design/report-parts-in-report-designer-ssrs.md).  
  
### Parameter values

The following are options for selecting parameter values in the report:
  
- Select a single parameter value from a drop-down list.  
  
- Select multiple parameter values from a drop-down list.  
  
- Select a value from a drop-down list for one parameter, which determines the values that are available in the drop-down list for another parameter. These values are cascading parameters. Cascading parameters enable you to successively filter parameter values from thousands of values to a manageable number.  
  
    For more information, see [Add cascading parameters to a paginated report (Report Builder)](../../reporting-services/report-design/add-cascading-parameters-to-a-report-report-builder-and-ssrs.md).  
  
- Run the report without having to first select a parameter value because a default value already exists for the parameter.  
  
## <a name="bkmk_Report_Parameters"></a> Report parameter properties

You can change the report parameter properties by using the **Report Properties** dialog box. The following table summarizes the properties that you can set for each parameter:  
  
|Property|Description|  
|--------------|-----------------|  
|Name|Type a case-sensitive name for the parameter. The name must begin with a letter and can have letters, numbers, or an underscore (_). The name can't have spaces. For automatically generated parameters, the name matches the parameter in the dataset query. By default, manually created parameters are similar to ReportParameter1.|  
|Prompt|The text that appears next to the parameter on the report viewer toolbar.|  
|Data type|A report parameter must be one of the following data types:<br /><br /> **Boolean**. The user selects True or False from a radio button.<br /><br /> **DateTime**. The user selects a date from a calendar control.<br /><br /> **Integer**. The user types values in a text box.<br /><br /> **Float**. The user types values in a text box.<br /><br /> **Text**. The user types values in a text box.<br /><br /> When available values are defined for a parameter, the user chooses values from a drop-down list, even when the data type is **DateTime**.<br /><br /> For more information about report data types, see [RDL data types](../../reporting-services/reports/report-definition-language-ssrs.md#bkmk_RDL_Data_Types).|  
|Allow blank value|Select this option if the value of the parameter can be an empty string or blank.<br /><br /> If you specify valid values for a parameter and want one of them to be blank, you must include it as one of the specified values. Selecting this option doesn't automatically include a blank for available values.|  
|Allow null value|Select this option if the value of the parameter can be null.<br /><br /> If you specify valid values for a parameter and you want one of them to be null, you must include null as one of the specified values. Selecting this option doesn't automatically include a null for available values.|  
|Allow multiple values|Provide available values to create a drop-down list that your users can choose from. Use this property to ensure that only valid values are submitted in the dataset query.<br /><br /> Select this option if the value for the parameter can be multiple values that are displayed in a drop-down list. Null values aren't allowed. When this option is selected, check boxes are added to the list of available values in a parameter drop-down list. The top of the list includes a check box for **Select All**. Users can check the values that they want.<br /><br /> If the data that provides values changes rapidly, the list the user sees might not be the most current.|  
|Visible|Select this option to display the report parameter at the top of the report when it runs. This option allows users to select parameter values at run time.|  
|Hidden|Select this option to hide the report parameter in the published report. The report parameter values can still be set on a report URL, in a subscription definition, or on the report server.|  
|Internal|Select this option to hide the report parameter. In the published report, the report parameter can only be viewed in the report definition.|  
|Available values|If you specify available values for a parameter, the valid values always appear as a drop-down list. For example, if you provide available values for a **DateTime** parameter, a drop-down list for dates appears in the **Parameter** pane instead of a calendar control.<br /><br /> You can set an option on the data source to use a single transaction for all queries in the datasets that are associated with a data source. Using this option ensures that the list of values is consistent among the report and subreports.<br /><br /> **Security Note** In any report that includes a parameter of data type **Text**, be sure to use an available values list (also known as a valid values list) and ensure that any user running the report has only the permissions necessary to view the data in the report. For more information, see [Security &#40;Report Builder&#41;](../../reporting-services/report-builder/security-report-builder.md).|  
|Default values|Set default values from a query or from a static list.<br /><br /> When each parameter has a default value, the report runs automatically on first view.|  
|Advanced|Set the report definition attribute **UsedInQuery**, a value that indicates whether this parameter directly or indirectly affects the data in a report.<br /><br /> **Automatically determine when to refresh**<br /> Choose this option when you want the report processor to determine a setting for this value. The value is **True** if the report processor detects a dataset query with a direct or indirect reference to this parameter, or if the report has subreports.<br /><br /> **Always refresh**<br /> Choose this option when the report parameter is used directly or indirectly in a dataset query or parameter expression. This option sets **UsedInQuery** to True.<br /><br /> **Never refresh**<br /> Choose this option when the report parameter isn't used directly or indirectly in a dataset query or parameter expression. This option sets **UsedInQuery** to False.<br /><br /> **Caution** Use **Never Refresh** with caution. On the report server, **UsedInQuery** is used to help control cache options for report data and for rendered reports, and parameter options for snapshot reports. If you set **Never Refresh** incorrectly, you could cause incorrect report data or reports to be cached, or cause a snapshot report to have inconsistent data. For more information, see [Report Definition Language &#40;SSRS&#41;](../../reporting-services/reports/report-definition-language-ssrs.md).|  
  
## <a name="bkmk_Dataset_Parameters"></a> Dataset query  

To filter data in the dataset query, you can include a restriction clause that limits the retrieved data by specifying values to include or exclude from the result set.  
  
Use the query designer for the data source to help build a parameterized query.  
  
- For [!INCLUDE[tsql](../../includes/tsql-md.md)] queries, different data sources support different syntax for parameters. Support ranges from parameters identified in the query by position or by name. For more information, see articles for specific external data source types in [Report Datasets (SSRS)](../../reporting-services/report-data/report-datasets-ssrs.md). In the relational query designer, you must select the parameter option for a filter to create a parameterized query. For more information, see [Relational query designer user interface &#40;Report Builder&#41;](../../reporting-services/report-data/relational-query-designer-user-interface-report-builder.md).  
  
- You can specify whether to create a parameter based on a filter that you specify in the query designer. You can make this specification for queries that are based on a multidimensional data source such as Microsoft SQL Server Analysis Services, SAP NetWeaver BI, or Hyperion Essbase. For more information, see the query designer article in [Query Design Tools &#40;SSRS&#41;](../report-data/query-design-tools-ssrs.md) that corresponds to the data extension.  
  
## <a name="bkmk_Manage_Parameters"></a> Parameter management for a published report  

When you design a report, report parameters are saved in the report definition. When you publish a report, report parameters are saved and managed separately from the report definition.  
  
For a published report, you can use the following tools:  
  
- **Report parameter properties**: Change report parameter values directly on the report server independently from the report definition.  
  
- **Cached reports**: To create a cache plan for a report, each parameter must have a default value. For more information, see [Cache reports (SSRS)](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
- **Cached shared datasets**: To create a cache plan for a shared dataset, each parameter must have a default value. For more information, see [Cache reports (SSRS)](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
- **Linked reports**: You can create linked reports with preset parameter values to filter data for different audiences. For more information, see [Create a linked report](../../reporting-services/reports/create-a-linked-report.md).  
  
- **Report subscriptions**: You can specify parameter values to filter data and deliver reports through subscriptions. For more information, see [Subscriptions and delivery (Reporting Services)](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
- **URL access**: You can specify parameter values in a URL to a report. You can also run reports and specify parameter values using URL access. For more information, see [URL access &#40;SSRS&#41;](../../reporting-services/url-access-ssrs.md).  
  
Parameter properties for a published report are preserved if you republish the report definition. If the report definition is republished as the same report, and parameter names and data types remain the same, your property settings are retained. You might need to change the parameter properties in the published report if you add or delete parameters in the report definition. Or you might need to change the properties to change the data type or name of an existing parameter.  
  
Not all parameters can be modified in all cases. If a report parameter gets a default value from a dataset query, that value can't be modified for a published report. And it can't be modified on the report server. The value used at run time is determined when the query runs. If you use expression-based parameters, it gets determined when the expression is evaluated.  
  
Report execution options can affect how parameters are processed. A report that runs as a snapshot can't use parameters that are derived from a query unless the query includes default values for the parameters.  
  
## <a name="bkmk_Parameters_Subscription"></a> Parameters for a subscription  

You can define a subscription for an on demand or for a snapshot and specify parameter values to use during subscription processing.  
  
- **On-demand report.**  For an on-demand report, you can specify a different parameter value than the published value for each parameter listed for the report. For example, suppose you have a Call Service report that uses a *Time Period* parameter to return customer service requests for the current day, week, or month. If the default parameter value for the report is set to **today**, your subscription can use a different parameter value (such as **week** or **month**) to produce a report that contains weekly or monthly figures.  
  
- **Snapshot.**  For a snapshot, your subscription must use the parameter values defined for the snapshot. Your subscription can't override a parameter value that is defined for a snapshot. For example, suppose you're subscribing to a Western regional sales report that runs as a report snapshot, and the snapshot specifies **Western** as a regional parameter value. In this case, if you create a subscription to this report, you must use the parameter value **Western** in your subscription. To provide a visual indication that the parameter is ignored, the parameter fields on the subscription page are set to read-only fields.  
  
- Report execution options can affect how parameters are processed. Parameterized reports that run as report snapshots use the parameter values defined for the report snapshot. Parameter values are defined in the **Parameter Properties** page of the report. A report that runs as a snapshot can't use parameters that are derived from a query unless the query includes default values for the parameters.  
  
- If a parameter value changes in the report snapshot after the subscription is defined, the report server deactivates the subscription. Deactivating the subscription indicates that the report was modified. To activate the subscription, open and then save the subscription.  
  
> [!NOTE]  
> Data-driven subscriptions can use parameter values that are obtained from a subscriber data source. For more information, see [Use an external data source for subscriber data (data-driven subscription)](../../reporting-services/subscriptions/use-an-external-data-source-for-subscriber-data-data-driven-subscription.md).  
  
For more information, see [Subscriptions and delivery (Reporting Services)](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md).  
  
## <a name="bkmk_Parameters_Security"></a> Parameters and securing data  

Use caution when distributing parameterized reports that contain confidential or sensitive information. A user can easily replace a report parameter with a different value, resulting in information disclosure that you didn't intend.  
  
A secure alternative to using parameters for employee or personal data is to select data based on expressions that include the **UserID** field from the **Built-in Fields**. The **UserID** provides a way to get the identity of the user running the report, and use that identity to retrieve user-specific data.  
  
> [!IMPORTANT]  
> In any report that includes a parameter of type **String**, be sure to use an available values list (also known as a valid values list) and ensure that any user running the report has only the permissions necessary to view the data in the report. When you define a parameter of type **String**, the user is presented with a text box that can take any value. An available values list limits the values that can be entered. If the report parameter is tied to a dataset parameter and you do not use an available values list, it is possible for a report user to type SQL syntax into the text box, potentially opening the report and your server to a SQL injection attack. If the user has sufficient permissions to execute the new SQL statement, it may produce unwanted results on the server.  
>
> If a report parameter is not tied to a dataset parameter and the parameter values are included in the report, it is possible for a report user to type expression syntax or a URL into the parameter value, and render the report to Excel or HTML. If another user then views the report and clicks the rendered parameter contents, the user may inadvertently execute the malicious script or link.  
>
> To mitigate the risk of inadvertently running malicious scripts, open rendered reports only from trusted sources. For more information about securing reports, see [Secure reports and resources](../../reporting-services/security/secure-reports-and-resources.md).  

## <a name="bkmk_Related_Topics"></a> Related content

- [Tutorial: Add a parameter to your report (Report Builder)](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)  
  
- [Report parameters concepts in paginated reports (Report Builder)](../../reporting-services/report-design/report-parameters-concepts-report-builder-and-ssrs.md)  
  
- [Tools available in SQL Server Reporting Services](../../reporting-services/tools/reporting-services-tools.md)