---
title: "Choose and Map Input Data for a Prediction Query | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "tables [Analysis Services], prediction queries"
  - "Mining Model Prediction [Analysis Services], input tables"
ms.assetid: 00d330a0-879d-4da0-9f29-53c288116f4d
author: minewiskan
ms.author: owend
manager: craigg
---
# Choose and Map Input Data for a Prediction Query
  When you create predictions from a mining model, you generally do this by feeding new data into the model. (The exception is time series models, which can make predictions based on historical data only.) To provide the model with new data, you must make sure that the data is available as part of a data source view. If you know in advance which data you will use for prediction, you can include it in the data source view that you used to create the model. Otherwise, you might have to create a new data source view. For more information, see [Data Source Views in Multidimensional Models](../multidimensional-models/data-source-views-in-multidimensional-models.md).  
  
 Sometimes the data that you need might be contained within more than one table in a one-to-many join. This is the case with data used for association models or sequence clustering models, which use a case table linked to a nested table that contains product or transaction details. If your model uses a case-nested table structure, the data that you use for prediction must also have a case-nested table structure.  
  
> [!WARNING]  
>  You cannot add new columns or map columns that are in a different data source view. The data source view that you select must contain all the columns that you need for the prediction query.  
  
 After you have identified the tables that contain the data you will use for predictions, you must map the columns in the external data to the columns in the mining model. For example, if your model predicts customer purchasing behavior based on demographics and survey responses, your input data should contains information that generally corresponds to what is in the model. You do not need to have matching data for every single column, but the more columns you can match, the better. If you try to map columns that have different data types, you might get an error. In that case, you could define a named calculation in the data source view to cast or convert the new column data to the data type required by the model. For more information, see [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md).  
  
 When you choose the data to use for prediction, some columns in the selected data source might be automatically mapped to the mining model columns, based on name similarity and matching data type. You can use the **Modify Mapping** dialog box in the **Mining Model Prediction** to change the columns that are mapped, delete inappropriate mappings, or create new mappings for existing columns. The **Mining Model Prediction** design surface also supports drag-and-drop editing of connections.  
  
-   To create a new connection, just select a column in the **Mining Model** table and drag it to the corresponding column in the **SelectInput Table(s)** table.  
  
-   To remove a connection, select the connection line and press the DELETE key.  
  
 The following procedure describes how you can modify the joins that have been created between the case table and a nested table used as inputs to a prediction query, using the **Specify Nested Join** dialog box.  
  
### Select an input table  
  
1.  On the **Select Input Table(s)** table of the **Mining Accuracy Chart** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **Select Case Table**.  
  
     The **Select Table** dialog box opens, in which you can select the table that contains the data on which to base your queries.  
  
2.  In the **Select Table** dialog box, select a data source from the **Data Source** list.  
  
3.  Under **Table/View Name**, select the table that contains the data you want to use to test the models.  
  
4.  Click **OK**.  
  
     The columns in the mining structure are automatically mapped to the columns that have the same name in the input table.  
  
### Change the way that input data is mapped to the model  
  
1.  In Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], select the **Mining Model Prediction** tab.  
  
2.  On the **Mining Model** menu, select **Modify Connections**.  
  
     The **Modify Mapping** dialog box opens. In this dialog box, the column **Mining Model Column** lists the columns in the selected mining structure. The column **Table Column** lists the columns in the external data source that you chose in the **SelectInput Table(s)** dialog box. The columns in the external data source are mapped to columns in the mining model.  
  
3.  Under **Table Column**, select the row that corresponds to the mining model column that you want to map to.  
  
4.  Select a new column from the list of available columns in the external data source. Select the blank item in the list to delete the column mapping.  
  
5.  Click **OK**.  
  
     The new column mappings are displayed in the designer.  
  
### Remove a relationship between input tables  
  
1.  On the **Select Input Table(s)** table of the **Mining Model Prediction** tab in Data Mining Designer in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], click **Modify Join**.  
  
     The **Specify Nested Join** dialog box opens.  
  
2.  Select a relationship.  
  
3.  Click **Remove Relationship**.  
  
4.  Click **OK**.  
  
     The relationship between the case table and the nested table has been removed.  
  
### Create a new relationship between input tables  
  
1.  On the **Select Input Table(s)** table of the **Mining Model Prediction** tab in Data Mining Designer, click **Modify Join**.  
  
     The **Specify Nested Join** dialog box opens.  
  
2.  Click **Add Relationship**.  
  
     The **Create Relationship** dialog box opens.  
  
3.  Select the key of the nested table in **Source Columns**.  
  
4.  Select the key of the case table in **Destination Columns**.  
  
5.  Click **OK** in the **Create Relationship** dialog box.  
  
6.  Click **OK** in the **Specify Nested Join** dialog box.  
  
     A new relationship has been created between the case table and the nested table.  
  
### Add a nested table to the input tables of a prediction query  
  
1.  On the **Mining Model Prediction** tab in Data Mining Designer, click **Select Case Table** to open the **Select Table** dialog box.  
  
    > [!NOTE]  
    >  You cannot add a nested table to the inputs unless you have specified a case table. Use of a nested table requires that the mining model you are using for prediction also uses a nested table.  
  
2.  In the **Select Table** dialog box, select a data source from the **Data Source** list, and select the table in the data source view that contains the case data. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
3.  Click **Select Nested Table** to open the **Select Table** dialog box.  
  
4.  In the **Select Table** dialog box, select a data source from the **Data Source** list, and select the table in the data source view that contains the nested data. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     If a relationship already exists, the columns in the mining model are automatically mapped to the columns that have the same name in the input table. You can modify the relationship between the nested table and the case table by clicking **Modify Join**, which opens the **Create Relationship** dialog box.  
  
## See Also  
 [Prediction Queries &#40;Data Mining&#41;](prediction-queries-data-mining.md)  
  
  
