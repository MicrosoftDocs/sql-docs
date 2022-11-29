---
title: "Build a Query in the Relational Query Designer (Report Builder and SSRS"
description: Learn how to build a query in the Relational Query Designer so that you can specify which data to retrieve from an external data source for a report dataset. 
ms.service: reporting-services
ms.subservice: report-data
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
ms.reviewer: ""
ms.custom: ""
ms.date: 04/25/2019
---

# Build a Query in the Relational Query Designer (Report Builder and SSRS)

A query designer helps you specify which data to retrieve from an external data source for a report dataset. You use a query designer when you build a query in a wizard or create a dataset query.  
  
> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 A dataset is based on a data source. The type of data source and the authoring environment determines which query designer opens when you define the dataset query. Query designer features vary based on the underlying data source. For more information about data layers, see [Create data connection strings - Report Builder & SSRS](../../reporting-services/report-data/data-connections-data-sources-and-connection-strings-report-builder-and-ssrs.md).

 You can use a query designer for the following tasks:  
  
-   Explore the metadata for multiple schemas on the external data source  
  
-   Specify fields to retrieve for the dataset  
  
-   Specify relationships between two objects such as tables  
  
-   Specify filters to restrict the data before it is retrieved as report data  
  
-   Indicate whether to create parameters  
  
-   Specify aggregates to perform calculations on the external data source  
  
 After you open a query designer, you build a query in the same way for either an embedded dataset or a shared dataset. The following procedures use an embedded dataset query.  
  
 For more information, see [Relational Query Designer User Interface &#40;Report Builder&#41;](../../reporting-services/report-data/relational-query-designer-user-interface-report-builder.md).  
  
### To build a query for an embedded dataset in Report Design View  
  
1.  Open the query designer. In the Report Data pane, right-click the dataset, and then click **Query**.  
  
     The query designer that is associated with the data source opens.  
  
2.  In the Database view pane, expand the folders that display a hierarchical view of database schema objects such as tables, views, and stored procedures. Click the select box to select all fields for an object or expand the node to select individual fields.  
  
     As you select fields from the Database view pane, the **Select fields** pane displays your selections.  
  
     If you select fields from more than one related database table, use the Relationships pane to view the table relationships that were detected from the database schema.  
  
3.  Select **OK**.
  
4.  Select **OK**.
  
     The list of dataset fields appears in the Report Data pane.  
  
### To specify limits for a query  
  
1.  In the relational query designer, verify that you have fields selected and that the fields appear in the **Selected fields** pane.  
  
2.  In the Applied filters pane toolbar, click **Add Filter**. A new filter row appears.  
  
3.  In **Field name**, click to display the drop-down list of fields, and then click the name of the field that you want to filter by. For example, to filter by quantity, click the field that contains the number of items.  
  
4.  In **Operator**, click to display the drop-down list of operators, and then select the comparison operator to use in the filter.  
  
5.  In **Value**, type the value that you want to filter by. For example, to filter on quantity greater than 100, type 100.  
  
6.  Select the parameter option in this row to create a dataset parameter to enable a user to specify a filter value. A report parameter that matches the dataset parameter is automatically created.  
  
7.  Select **OK**.
  
8.  Select **OK**.
  
 The list of dataset fields appears in the Report Data pane.  
  
### To view a query result set  
  
1.  On the query designer toolbar, click **Run Query (!)**.  
  
    > [!NOTE]  
    >  The query designer uses design time credentials to run the query and retrieve the result set. For more information, see [Specify Credential and Connection Information for Report Data Sources](specify-credential-and-connection-information-for-report-data-sources.md).  
  
 The query runs on the data source and returns example data in the Query results pane.  
  
## See Also  
 [Report Datasets &#40;SSRS&#41;](../../reporting-services/report-data/report-datasets-ssrs.md)   
 [Add Data from External Data Sources &#40;SSRS&#41;](../../reporting-services/report-data/add-data-from-external-data-sources-ssrs.md)   
 [Query Design Tools &#40;SSRS&#41;](query-design-tools-ssrs.md)   
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)   
 [Report Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/report-design-view-report-builder.md)   
 [Shared Dataset Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/shared-dataset-design-view-report-builder.md)   
 [Reporting Services Query Designers](/previous-versions/sql/)  
  
