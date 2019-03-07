---
title: "Cache Refresh Options (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 227da40c-6bd2-48ec-aa9c-50ce6c1ca3a6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Cache Refresh Options (Report Manager)
  Use the Cache Refresh options page to create schedules for preloading the cache with temporary copies of data for a report or for a shared dataset. A refresh plan includes a schedule and the option to specify or override values for parameters. For a shared dataset, you cannot override values for parameters that are marked read-only. You can create and use more than one refresh plan as part of the refresh options page.  
  
 Default role assignments that enable you to add, delete, change, and view related reports and shared datasets for cache refresh plans are Content Manager, My Reports, and Publisher.  
  
> [!NOTE]  
>  This feature is not available in every edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
## To open the Cache Refresh Plan properties page for a report or shared dataset  
  
1.  Open Report Manager, and locate the report or shared dataset for which you want to configure cache refresh plan properties.  
  
2.  Hover over the report or shared dataset, and click the drop-down arrow.  
  
3.  In the drop-down list, click **Manage**. The **General properties** page opens.  
  
4.  Click the **Cache Refresh Plan** tab.  
  
5.  To create a new cache plan, click **New Cache Refresh Plan**.  
  
    > [!NOTE]  
    >  You must enable and start the SQL Server Agent service to create a cache refresh plan.  
  
6.  To create a copy of a cache plan and then customize it, click **New from Existing**.  
  
## Cache Refresh Options  
 **Delete**  
 Deletes all of the currently selected refresh plans.  
  
 **New from existing**  
 This option is enabled when one and only one cache refresh plan is selected. This option will create a new refresh plan which is copied from the original plan. The cache refresh plan page opens pre-populated with details from the plan that was selected. You can then modify the refresh plan options and save the plan with a new description.  
  
 **New cache refresh plan**  
 Click to create a new refresh plan to be used in the current cache refresh options.  
  
 **Edit**  
 Select this option to edit the current refresh plan.  
  
## Cache Refresh Plan Options  
 **Description**  
 Specify a description for the cache refresh plan.  
  
 **Item-specific schedule**  
 Select this option to create a schedule that is used only by this item.  
  
 **Configure**  
 Click to open the Schedule page, which is used to specify frequency information.  
  
 For more information, see [New Schedule: Edit Schedule Page &#40;Report Manager&#41;](../../2014/reporting-services/new-schedule-edit-schedule-page-report-manager.md).  
  
 **Shared schedule**  
 Select this option to select an existing schedule.  
  
 For more information, see [Create, Modify, and Delete Schedules](subscriptions/create-modify-and-delete-schedules.md).  
  
 **@\<** *Parameter* **>**  
 Specify one combination of parameter values. This section appears only if the current dataset or report has parameters.  
  
 See [Specifying Parameters](#Parameters) in the next section.  
  
 **Use Default**  
 Select this option to use the predefined default value for this parameter.  
  
##  <a name="Parameters"></a> Specifying Parameters  
 To create a cache refresh plan, each report or shared dataset parameter must have a value. If the report or shared dataset item does not have a default value specified in the definition, you must specify a value. If a default value exists, you do not need to provide one here. If you do provide a value, the value overrides the default value.  
  
 To specify multiple combinations of parameter values, create a separate cache refresh plan for each combination.  
  
 Additions, changes, and deletions made to parameters on a report or shared dataset can affect the cache refresh plan. If you add a parameter with a default value for a report, remove a parameter, or change the data type or the read-only option for a shared dataset parameter, the changes take affect the next time the cache refresh plan is processed.  
  
### Shared Dataset Parameters  
 For a shared dataset, the following information is derived from the shared dataset definition:  
  
-   **Name** Specifies the name of the query parameter.  
  
-   **Type** Specifies the data type of the query parameter. Because this data type is unknown until the data provider processes the dataset query, data type validation cannot occur until the shared dataset is processed.  
  
-   **Nullable** Specifies whether NULL is a valid value.  
  
-   **ReadOnly** Specifies whether this parameter is marked read-only in the shared dataset definition. Read only parameters do not appear in the parameter list for cache refresh options and must have a default specified as part of the shared dataset definition.  
  
-   **DefaultValues** Default values that have been specified in the shared dataset definition. Query parameters can be multivalued. To override the default values, type new values in the text box prompt areas.  
  
 If the shared dataset definition specifies the option **Omit from query** for a parameter, you do not need to provide a default value. This flag indicates that the dataset parameter is not used in the query. For example, the parameter appears in the shared dataset definition because it is a report parameter that is used in the dataset filter only.  
  
 To view or change dataset parameter options, you must edit the shared dataset definition. For more information, see [Manage Shared Datasets](report-data/manage-shared-datasets.md).  
  
### Report Parameters  
 For a report, each parameter value must be valid before you can successfully create a cache refresh plan. You must type or select a default value for each report parameter. The value that you set overrides the default value that is defined for the report parameter on the report server.  
  
 Parameters must conform to the requirements specified in the parameter properties on the report server. For example, if the property AllowBlank is false for a report parameter, an empty string is not a valid value.  
  
 To view or change report parameter options, you must edit the report parameters in the report, or independently, on the report server. For more information, see [Report Parameters Concept &#40;Report Builder and SSRS&#41;](report-design/report-parameters-concepts-report-builder-and-ssrs.md).  
  
## Conditions that Cause a Cache Refresh Plan to be Inactive  
 The following conditions can cause a shared dataset or report cache refresh plan to become inactive.  
  
-   The shared dataset cache or report cache option is disabled.  
  
-   The required parameter values are not defined, not valid, or missing. All queries for a report must be valid before the report will be processed. For a report that has subreports, all dataset queries, including datasets for the subreport, are processed first. If any dataset cannot be successfully processed, the report cannot run.  
  
## Conditions that Cause a Cache Refresh Plan to be Reactivated  
 After a plan is inactive, do one of the following to trigger evaluation of a cache refresh plan:  
  
-   Change an option for the plan.  
  
-   Enable caching for a shared dataset or report that is associated with the refresh plan.  
  
-   Clear or select the read-only option for a dataset query parameter associated with the refresh plan, and then save the new definition to the report server.  
  
## See Also  
 [Item-Level Tasks](security/tasks-and-permissions-item-level-tasks.md)   
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)   
 [Report Manager F1 Help](../../2014/reporting-services/report-manager-f1-help.md)   
 [Caching Reports &#40;SSRS&#41;](report-server/caching-reports-ssrs.md)   
 [Manage Shared Datasets](report-data/manage-shared-datasets.md)  
  
  
