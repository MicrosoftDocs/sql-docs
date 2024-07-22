---
title: "Add cascading parameters to a paginated report"
description: Find out how to use cascading parameters in your paginated reports in Report Builder to manage large amounts of report data.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/17/2018
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add cascading parameters to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  Cascading parameters provide a way of managing large amounts of data in a  paginated report. You can define a set of related parameters so that the list of values for one parameter depends on the value chosen in another parameter. For example, the first parameter is independent and might present a list of product categories. When the user selects a category, the second parameter is dependent on the value of the first parameter. Its values are updated with a list of subcategories within the chosen category. When the user views the report, the values for both the category and subcategory parameters are used to filter report data.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
 To create cascading parameters, you define the dataset query first and include a query parameter for each cascading parameter that you need. You must also create a separate dataset for each cascading parameter to provide available values. For more information, see [Add, change, or delete available values for a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-available-values-for-a-report-parameter.md).  
  
 Order is important for cascading parameters because the dataset query for a parameter later in the list includes a reference to each parameter that is earlier in the list. At run time, the order of the parameters in the Report Data pane determines the order in which the parameter queries appear in the report. Therefore, the order in which a user chooses each successive parameter value.  
  
 For information about creating cascading parameters with multiple values and including the Select All feature, see [How to have a select all multivalue cascading parameter](/archive/blogs/psssql/).  
  
## Create the main dataset with a query that includes multiple related parameters  
  
1.  In the Report Data pane, right-click a data source, and then select **Add Dataset**.  
  
1.  In **Name**, enter the name of the dataset.  
  
1.  In **Data source**, choose the name of the data source or select **New** to create one.  
  
1.  In **Query type**, choose the type of query for the selected data source. In this article, query type **Text** is assumed.  
  
1.  In **Query**, enter the query to use to retrieve data for this report. The query must include the following parts:  
  
    1.  A list of data source fields. For example, in a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement, the `SELECT` statement specifies a list of database column names from a given table or view.  
  
    1.  One query parameter for each cascading parameter. A query parameter limits the data retrieved from the data source by specifying certain values to include or exclude from the query. Typically, query parameters occur in a restriction clause in the query. For example, in a [!INCLUDE[tsql](../../includes/tsql-md.md)] `SELECT` statement, query parameters occur in the WHERE clause.  
  
1.  Select **Run** (**!**). After you include query parameters and then run the query, report parameters that correspond to the query parameters are automatically created.  
  
    > [!NOTE]  
    >  The order of query parameters the first time you run a query determines the order that they are created in the report. To change the order, see [Change the order of a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/change-the-order-of-a-report-parameter-report-builder-and-ssrs.md)  
  
1.  Select **OK**.
  
 Next, create a dataset that provides the values for the independent parameter.  
  
## Create a dataset to provide values for an independent parameter  
  
1.  In the Report Data pane, right-click a data source, and then select **Add Dataset**.  
  
1.  In **Name**, enter the name of the dataset.  
  
1.  In **Data source**, verify the name is the name of the data source you chose in step 1.  
  
1.  In **Query type**, choose the type of query for the selected data source. In this article, query type **Text** is assumed.  
  
1.  In **Query**, enter the query to use to retrieve values for this parameter. Queries for independent parameters typically don't contain query parameters. For example, to create a query for a parameter that provides all category values, you might use a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement similar to the following block:  
  
    ```  
    SELECT DISTINCT <column name> FROM <table>  
    ```  
  
     The `SELECT DISTINCT` command removes duplicate values from the result set so that you get each unique value from the specified column in the specified table.  
  
     Select **Run** (**!**). The result set shows the values that are available for this first parameter.  
  
1.  Select **OK**.
  
 Next, set the properties of the first parameter to use this dataset to populate its available values at run-time.  
  
## Set available values for a report parameter  
  
1.  In the **Report Data** pane, in the **Parameters** folder, right-click the first parameter, and then select **Parameter Properties**.  
  
1.  In **Name**, verify that the name of the parameter is correct.  
  
1.  Select **Available Values**.  
  
1.  Select **Get values from a query**. Three fields appear.  
  
1.  In **Dataset**, from the list, select the name of the dataset you created in the previous procedure.  
  
1.  In **Value**, select the name of the field that provides the parameter value.  
  
1.  In **Label**, select the name of the field that provides the parameter label.  
  
1.  Select **OK**.
  
 Next, create a dataset that provides the values for a dependent parameter.  
  
## Create a dataset to provide values for a dependent parameter  
  
1.  In the **Report Data** pane, right-click a data source, and then select **Add Dataset**.  
  
1.  In **Name**, enter the name of the dataset.  
  
1.  In **Data source**, verify the name is the name of the data source you chose in step 1.  
  
1.  In **Query type**, choose the type of query for the selected data source. In this article, query type **Text** is assumed.  
  
1.  In **Query**, enter the query to use to retrieve values for this parameter. Queries for dependent parameters typically include query parameters for each parameter that this parameter is dependent on. For example, you can create a query for a parameter that provides all subcategory (dependent parameter) values for a category (independent parameter). To do so, you might use a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement similar to the following block:  
  
    ```  
    SELECT DISTINCT Subcategory FROM <table>   
    WHERE (Category = @Category)  
    ```  
  
     In the `WHERE` clause, **Category** is the name of a field from `<table>` and `@Category` is a query parameter. This statement produces a list of subcategories for the category specified in `@Category`. At run time, this value is filled in with the value that the user chooses for the report parameter that has the same name.  
  
1.  Select **OK**.
  
 Next, set the properties of the second parameter to use this dataset to populate its available values at run time.  
  
## Set available values for the second parameter  
  
1.  In the **Report Data** pane, in the **Parameters** folder, right-click the first parameter, and then select **Parameter Properties**.  
  
1.  In **Name**, verify that the name of the parameter is correct.  
  
1.  Select **Available Values**.  
  
1.  Select **Get values from a query**.  
  
1.  In **Dataset**, from the list, select the name of the dataset you created in the previous procedure.  
  
1.  In **Value**, select the name of the field that provides the parameter value.  
  
1.  In **Label**, select the name of the field that provides the parameter label.  
  
1.  Select **OK**.
  
## Test the cascading parameters  
  
1.  Select **Run**.  
  
1.  From the list for the first, independent parameter, choose a value.  
  
     The report processor runs the dataset query for the next parameter and passes it the value you chose for the first parameter. The list for the second parameter is populated with the available values based on the first parameter value.  
  
1.  From the list for the second, dependent parameter, choose a value.  
  
     The report doesn't run automatically after you choose the last parameter so that you can change your choice.  
  
1.  Select **View Report**. The report updates the display based on the parameters you chose.  
  
## Related content  
 [Add, change, or delete a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md)   
 [Report parameters &#40;Report Builder and Report Designer&#41;](../../reporting-services/report-design/report-parameters-report-builder-and-report-designer.md)   
 [Tutorial: Add a parameter to your report &#40;Report Builder&#41;](../../reporting-services/tutorial-add-a-parameter-to-your-report-report-builder.md)   
 [Report Builder tutorials](../../reporting-services/report-builder-tutorials.md)   
 [Add dataset filters, data region filters, and group filters &#40;Report Builder&#41;](../../reporting-services/report-design/add-dataset-filters-data-region-filters-and-group-filters.md)   
 [Report embedded datasets and shared datasets &#40;Report Builder&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
