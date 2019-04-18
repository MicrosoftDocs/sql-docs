---
title: "SharePoint List Query Designer (Report Builder) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
f1_keywords: 
  - "10016"
ms.assetid: b8a3122c-8008-4950-b515-937636d7967f
author: maggiesMSFT
ms.author: maggies
---
# SharePoint List Query Designer (Report Builder)
  Report Builder and Report Designer provide both a graphical query designer and a text-based query designer to help you create a query that specifies the data to retrieve from a SharePoint site for a report dataset. Use the graphical query designer to explore the SharePoint list metadata, interactively build a query, and view the results of your query. Use the text-based query designer to view the query that was built by the graphical query designer, modify a query, or type the query commands. You can also import an existing query from a file or report.  
  
> [!IMPORTANT]  
>  Users access data sources when they create and run queries. You should grant minimal permissions on the data sources, such as read-only permissions.  
  
## Graphical Query Designer  
 In the graphical query designer you can explorer the SharePoint site, interactively build the command that retrieve SharePoint list data for a dataset. You choose the fields to include in the dataset and optionally, specify filters that limit the data in the dataset. You can specify that filters are used as parameters and provide the value of the filter at run-time.  
  
 SharePoint lists include a large number of SharePoint specific fields that might not be useful to include in reports. The query designer provides an option to hide these fields to make it easier and quicker to determine the fields to use.  
  
 The graphical query designer is divided into three areas  
  
-   Explore pane in which you select the list items and their fields to use.  
  
-   Design area in which you build the query.  
  
-   Results pane in which you view the query results.  
  
 The following figure shows the graphical query designer when it is used with SharePoint lists.  
  
 ![rsQD_Relational_Graphical_SharePoint](../../reporting-services/report-data/media/rsqd-relational-graphical-sharepoint.gif "rsQD_Relational_Graphical_SharePoint")  
  
 The following table describes the function of each pane.  
  
 [SharePoint Lists](#DatabaseView)  
 Displays SharePoint lists and the fields within each item in the list.  
  
 [Selected fields](#SelectedFields)  
 Displays the list of SharePoint list field names from the selected items in the SharePoint Lists pane. These fields become the field collection for the report dataset.  
  
 [Applied filters](#AppliedFilters)  
 Displays a list of fields and filter criteria for tables or views in the Database view.  
  
 [Query results](#QueryResults)  
 Displays sample data for the result set for the automatically generated query.  
  
###  <a name="DatabaseView"></a> SharePoint Lists Pane  
 The SharePoint Lists pane displays the metadata for database objects that you have the permissions to view, which is determined by the data source connection and credentials. The hierarchical view displays database objects organized by database schema. Expand the node for each schema to view tables, views, stored procedures, and table-valued functions. Expand a table or view to display the columns.  
  
###  <a name="SelectedFields"></a> Selected Fields Pane  
 The Selected Fields pane displays the list item fields that you select for SharePoint list items. The fields that are displayed in this pane become the field collection for the report dataset. After you create a dataset and a query, use the Report Data pane to view the field collection for a report dataset. These fields represent the data you can display in tables, charts, and other report items when you view a report.  
  
 To add or remove fields to this pane, select or clear check boxes for the table or view fields in the SharePoint Lists pane.  
  
###  <a name="AppliedFilters"></a> Applied Filters Pane  
 The Applied Filters pane displays the criteria that are used to limit the number of rows of data that are retrieved at run-time. Criteria specified in this pane are used to generate a [!INCLUDE[tsql](../../includes/tsql-md.md)] WHERE clause. When you select the parameter option, a report parameter is automatically created. Report parameters that are based on query parameters enable a user to specify values for the query to control the data in the report.  
  
 The following columns are displayed:  
  
-   **Field Name** Displays the name of the field to apply the criteria to.  
  
-   **Operator** Displays the operation to use in the filter expression.  
  
-   **Value** Displays the value to use in the filter expression.  
  
-   **Parameter** Displays the option to add a query parameter to the query. Use the Dataset properties to view the relationship between query parameter and report parameter.  
  
###  <a name="QueryResults"></a> Query Results Pane  
 The Query results pane displays the results for the automatically generated query that is specified by selections in the other panes. The columns in the result set are the fields that you specify in the Selected Fields pane and the row data is limited by the filters that you specify in the Applied Filters pane.  
  
 This data represents values from the data source at the time that you run the query. The data is not saved in the report definition .The actual data in the report is retrieved when the report is processed.  
  
 Sort order in the result set is determined by the order the data is retrieved from the data source. Sort order can be changed by modifying the query or after the data is retrieved for the report.  
  
### Graphical Query Designer Toolbar  
 The relational query designer toolbar provides the following buttons to help you specify or view the results of a query.  
  
|Button|Description|  
|------------|-----------------|  
|**Edit As Text**|Toggle to the text-based query designer to view the automatically generated query or to modify the query.|  
|**Import**|Import an existing query from a file or report. File types .sql and .rdl are supported.|  
|**Run Query**|Run the query. The Query results pane displays the result set.|  
|**Show Hidden Fields**|Toggle to show or hide the fields that were automatically generated by SharePoint such as the ProgId and Level for SharePoint link items, but are typically not used in reports.. Hiding these fields makes the field list shorter and easier to use.|  
  
## See Also  
 [Query Designers &#40;Report Builder&#41;](https://msdn.microsoft.com/library/553f0d4e-8b1d-4148-9321-8b41a1e8e1b9)  
  
  
