---
title: "Add Cascading Parameters to a Report (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 3a22eec3-57a7-478e-b6fc-102a9dbe0591
author: maggiesMSFT
ms.author: maggies
manager: craigg
---
# Add Cascading Parameters to a Report (Report Builder and SSRS)
  Cascading parameters provide a way of managing large amounts of report data. You can define a set of related parameters so that the list of values for one parameter depends on the value chosen in another parameter. For example, the first parameter is independent and might present a list of product categories. When the user selects a category, the second parameter is dependent on the value of the first parameter. Its values are updated with a list of subcategories within the chosen category. When the user views the report, the values for both the category and subcategory parameters are used to filter report data.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 To create cascading parameters, you define the dataset query first and include a query parameter for each cascading parameter that you need. You must also create a separate dataset for each cascading parameter to provide available values. For more information, see [Add, Change, or Delete Available Values for a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-available-values-for-a-report-parameter.md).  
  
 Order is important for cascading parameters because the dataset query for a parameter later in the list includes a reference to each parameter that is earlier in the list. At run time, the order of the parameters in the Report Data pane determines the order in which the parameter queries appear in the report, and therefore, the order in which a user chooses each successive parameter value.  
  
 For information about creating cascading parameters with multiple values and including the Select All feature, see [How to have a Select All Multivalue Cascading Parameter](https://go.microsoft.com/fwlink/?LinkId=184757).  
  
### To create the main dataset with a query that includes multiple related parameters  
  
1.  In the Report Data pane, right-click a data source, and then click **Add Dataset**.  
  
2.  In **Name**, type the name of the dataset.  
  
3.  In **Data source**, choose the name of the data source or click **New** to create one.  
  
4.  In **Query type**, choose the type of query for the selected data source. In this topic, query type **Text** is assumed.  
  
5.  In **Query**, type the query to use to retrieve data for this report. The query must include the following parts:  
  
    1.  A list of data source fields. For example, in a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, the SELECT statement specifies a list of database column names from a given table or view.  
  
    2.  One query parameter for each cascading parameter. A query parameter limits the data retrieved from the data source by specifying certain values to include or exclude from the query. Typically, query parameters occur in a restriction clause in the query. For example, in a [!INCLUDE[tsql](../../includes/tsql-md.md)] SELECT statement, query parameters occur in the WHERE clause. For more information, see "Filtering Rows by Using WHERE and HAVING" in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] documentation in [SQL Server Books Online](https://go.microsoft.com/fwlink/?linkid=120955).  
  
6.  Click **Run** (**!**). After you include query parameters and then run the query, report parameters that correspond to the query parameters are automatically created.  
  
    > [!NOTE]  
    >  The order of query parameters the first time you run a query determines the order that they are created in the report. To change the order, see [Change the Order of a Report Parameter &#40;Report Builder and SSRS&#41;](change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 Next, you will create a dataset that provides the values for the independent parameter.  
  
### To create a dataset to provide values for an independent parameter  
  
1.  In the Report Data pane, right-click a data source, and then click **Add Dataset**.  
  
2.  In **Name**, type the name of the dataset.  
  
3.  In **Data source**, verify the name is the name of the data source you chose in step 1.  
  
4.  In **Query type**, choose the type of query for the selected data source. In this topic, query type **Text** is assumed.  
  
5.  In **Query**, type the query to use to retrieve values for this parameter. Queries for independent parameters typically do not contain query parameters. For example, to create a query for a parameter that provides all category values, you might use a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement similar to the following:  
  
    ```  
    SELECT DISTINCT <column name> FROM <table>  
    ```  
  
     The SELECT DISTINCT command removes duplicate values from the result set so that you get each unique value from the specified column in the specified table.  
  
     Click **Run** (**!**). The result set shows the values that are available for this first parameter.  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 Next, you will set the properties of the first parameter to use this dataset to populate its available values at run-time.  
  
### To set available values for a report parameter  
  
1.  In the Report Data pane, in the Parameters folder, right-click the first parameter, and then click **Parameter Properties**.  
  
2.  In **Name**, verify that the name of the parameter is correct.  
  
3.  Click **Available Values**.  
  
4.  Click **Get values from a query**. Three fields appear.  
  
5.  In **Dataset**, from the drop-down list, click the name of the dataset you created in the previous procedure.  
  
6.  In **Value** field, click the name of the field that provides the parameter value.  
  
7.  In **Label** field, click the name of the field that provides the parameter label.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 Next, you will create a dataset that provides the values for a dependent parameter.  
  
### To create a dataset to provide values for a dependent parameter  
  
1.  In the Report Data pane, right-click a data source, and then click **Add Dataset**.  
  
2.  In **Name**, type the name of the dataset.  
  
3.  In **Data source**, verify the name is the name of the data source you chose in step 1.  
  
4.  In **Query type**, choose the type of query for the selected data source. In this topic, query type **Text** is assumed.  
  
5.  In **Query**, type the query to use to retrieve values for this parameter. Queries for dependent parameters typically include query parameters for each parameter that this parameter is dependent on. For example, to create a query for a parameter that provides all subcategory (dependent parameter) values for a category (independent parameter), you might use a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement similar to the following:  
  
    ```  
    SELECT DISTINCT Subcategory FROM <table>   
    WHERE (Category = @Category)  
    ```  
  
     In the WHERE clause, Category is the name of a field from \<table> and @Category is a query parameter. This statement produces a list of subcategories for the category specified in @Category. At run time, this value will be filled in with the value that the user chooses for the report parameter that has the same name.  
  
6.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
 Next, you will set the properties of the second parameter to use this dataset to populate its available values at run time.  
  
### To set available values for a report parameter  
  
1.  In the Report Data pane, in the Parameters folder, right-click the first parameter, and then click **Parameter Properties**.  
  
2.  In **Name**, verify that the name of the parameter is correct.  
  
3.  Click **Available Values**.  
  
4.  Click **Get values from a query**.  
  
5.  In **Dataset**, from the drop-down list, click the name of the dataset you created in the previous procedure.  
  
6.  In **Value** field, click the name of the field that provides the parameter value.  
  
7.  In **Label** field, click the name of the field that provides the parameter label.  
  
8.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To test the cascading parameters  
  
1.  Click **Run**.  
  
2.  From the drop-down list for the first, independent parameter, choose a value.  
  
     The report processor runs the dataset query for the next parameter and passes it the value you chose for the first parameter. The drop-down list for the second parameter is populated with the available values based on the first parameter value.  
  
3.  From the drop-down list for the second, dependent parameter, choose a value.  
  
     The report does not run automatically after you choose the last parameter so that you can change your choice.  
  
4.  Click **View Report**. The report updates the display based on the parameters you have chosen.  
  
## See Also  
 [Add, Change, or Delete a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)   
 [Report Parameters &#40;Report Builder and Report Designer&#41;](report-parameters-report-builder-and-report-designer.md)   
 [Tutorial: Add a Parameter to Your Report &#40;Report Builder&#41;](../tutorial-add-a-parameter-to-your-report-report-builder.md)   
 [Tutorials &#40;Report Builder&#41;](../report-builder-tutorials.md)   
 [Add Dataset Filters, Data Region Filters, and Group Filters &#40;Report Builder and SSRS&#41;](add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
