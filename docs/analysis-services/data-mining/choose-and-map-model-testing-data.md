---
title: "Choose and Map Model Testing Data | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Choose and Map Model Testing Data
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  To create an accuracy chart in [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], you must choose the data that will be used to test the model, and map the data to the model.  
  
 By default, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] will use the mining model testing data, provided that you created a holdout data set when you built the mining structure. Creating a holdout test set is the easiest way to test models that are based on the same mining structure, because the column names and data types will always match the model, and you can be reasonably assured that the distribution of the data is similar. Also, the designer will automatically create the relationships between the input and model columns.  
  
 Alternatively, you can specify an external source of data. For external data, there are some additional requirements:  
  
-   The external data set must be defined as a data source view in an instance of [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
-   The external data set must at least contain one column that can be mapped to the predictable column in the mining model. You can choose to ignore some columns.  
  
-   You cannot add new columns or map columns in a different data source view. The data source view that you select must contain all the columns that you need for the prediction query.  
  
-   If the external column names exactly match those in the model, the designer will map them for you. If the mappings are wrong, you can change them, or delete and create new mappings for existing columns.  
  
-   If you use an external data source, you can apply filters to restrict the testing data to a relevant subset of cases.  
  
-   Even when you use the holdout test set, you should be aware that filters can cause differences between the testing data associated with a mining structure and the mining model test cases.  
  
 This topic describes how to choose and map the testing data:  
  
 [Select input tables to test the accuracy of a mining model](#bkmk_SelectInputs)  
  
 [Map model columns to the columns in the testing data](#bkmk_MapColumns)  
  
 [Change the way that columns in the testing data are mapped to the model](#bkmk_ChangeMappings)  
  
##  <a name="bkmk_SelectInputs"></a> To select input tables to test the accuracy of a mining model  
  
1.  In Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], double-click the mining structure that contains the models you want to chart.  
  
2.  Select the **Mining Accuracy Chart** tab.  
  
3.  On the **Input Selection Tab** of the **Mining Accuracy Chart** view, select one of the following options:  
  
     **Use mining model test cases**  
  
     **Use mining structure test cases**  
  
     **Specify a different data set**  
  
4.  If you selected **Specify a different data set**, optionally click **Open Filter Editor** to create filter conditions on the input data set. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
5.  Click the **Lift Chart** tab or the **Classification Matrix** tab to automatically build the chart by using the testing data you specified.  
  
##  <a name="bkmk_MapColumns"></a> To map model columns to the columns in the testing data  
  
1.  Double-click the mining structure that contains the models you want to chart, to open the structure and models in Data Mining Designer.  
  
2.  Select the **Mining Accuracy Chart** tab, and then select the **Input Selection** tab.  
  
3.  In the **Input Selection** tab, under **Select data set to be used for Accuracy Chart**, select **Specify a different data set**.  
  
4.  Click the browse button **(...)** to open a dialog box and build the definition of the external data set.  
  
5.  In the **Select Mining Structure** dialog box, select the mining structure that contains the models you want to work with, and then click **OK**.  
  
6.  On the **Select Input Table(s)** table of the **Mining Accuracy Chart** tab, click **Select Case Table** to open the **Select Table** dialog box.  
  
7.  In the **Select Table** dialog box, select a data source from the **Data Source** list. Choose a table that contains the data that you want to use in the prediction queries to determine the accuracy of the models.  
  
8.  In the **Table/View Name** box, select the table that contains the data that you want to use to test the models.  
  
9. Edit the mappings, if necessary. Columns in the mining structure are automatically mapped to the columns with the same name in the input table. To manually create mappings, click a column in the **Select Input Table(s)** table and drag it onto the corresponding column in the **Mining Structure** table. To delete a mapping, click the line that links the column in the **Mining Structure** table to the mapped column in the **Select Input Table(s)** table, and then press DELETE.  
  
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
##  <a name="bkmk_ChangeMappings"></a> To modify the way input data is mapped to the model  
  
1.  In Data Mining Designer, double-click the structure that contains the models you want to chart.  
  
2.  Select the **Mining Accuracy Chart** tab.  
  
3.  Click the **Input Selection** tab.  
  
4.  In **Select data set to be used for Accuracy Chart**, select the option **Specify a different data set**.  
  
5.  Click the browse button **(...)** to open a dialog box and build the definition of the external data source.  
  
6.  In the **Specify Column Mapping** dialog box, click **Select Case Table**.  
  
7.  In the Select Table dialog box, Select a data source view from the list, and select the table that contains the case data. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
8.  If the tables you need are not available, close the dialog box and create a new data source view that contains the table. For information about how to create a data source view, see [Defining a Data Source View &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/defining-a-data-source-view-analysis-services.md).  
  
9. If the mining model contains a nested table, click **Select Nested Table**, and select the nested table from the list of tables in the data source view. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
10. Select the join line of the mapping you want to modify, and select **Modify Connections**.  
  
     The **Modify Mapping** dialog box opens. In the table in this dialog box, **Mining Structure Column** lists each column that the selected mining structure contains, and **Table Column** lists the columns from input tables that are mapped to columns in the mining structure.  
  
11. Under **Table Column**, select the row that corresponds to the row under **Mining Structure Column** for which you want to modify a relationship. Select a new column from the list, or select the blank entry from the list to delete the column.  
  
12. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     The new column mappings are displayed in the **Specify Column Mapping** dialog box. You can remove a mapping by selecting the line between the columns and pressing the DELETE key. You can create a new connection by selecting a column in the **Mining Structure** table and dragging it to the corresponding column in the **SelectInput Table(s)** table.  
  
## See Also  
 [Testing and Validation Tasks and How-tos &#40;Data Mining&#41;](../../analysis-services/data-mining/testing-and-validation-tasks-and-how-tos-data-mining.md)  
  
  
