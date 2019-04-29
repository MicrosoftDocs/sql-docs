---
title: "Creating Predictions for the Call Center Models (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: 5be0cec7-f639-4eeb-835e-e3204ae619e9
author: minewiskan
ms.author: owend
manager: kfile
---
# Creating Predictions for the Call Center Models (Intermediate Data Mining Tutorial)
  Now that you have learned something about the interactions between shifts, the number of operators, calls, and service grade, you are ready to create some prediction queries that can be used in business analysis and planning. You will first create some predictions on the exploratory model to test some assumptions. Next, you will create bulk predictions by using the logistic regression model.  
  
 This lesson assumes that you are already familiar with the concept of prediction queries.  
  
## Creating Predictions using the Neural Network Model  
 The following example demonstrates how to make a singleton prediction using the neural network model that was created for exploration. Singleton predictions are a good way to try out different values to see the effect in the model. In this scenario, you will predict the service grade for the midnight shift (no day of the week specified) if six experienced operators are on duty.  
  
#### To create a singleton query by using the neural network model  
  
1.  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], open the solution that contains the model that you want to use.  
  
2.  In Data Mining Designer, click the **Mining Model Prediction** tab.  
  
3.  In the **Mining Model** pane, click **Select Model**.  
  
4.  The **Select Mining Model** dialog box shows a list of mining structures. Expand the mining structure to view a list of mining models associated with that structure.  
  
5.  Expand the mining structure Call Center Default, and select the neural network model, Call Center - LR.  
  
6.  From the **Mining Model** menu, select **Singleton Query**.  
  
     The **Singleton Query Input** dialog box appears, with columns mapped to the columns in the mining model.  
  
7.  In the **Singleton Query Input** dialog box, click the row for Shift, and then select *midnight*.  
  
8.  Click the row for Lvl 2 Operators, and type `6`.  
  
9. In the bottom half of the **Mining Model Prediction** tab, click the first row in the grid.  
  
10. In the **Source** column, click the down arrow, and select **Prediction function**. In the **Field** column, select **PredictHistogram**.  
  
     A list of arguments that you can use with this prediction function automatically appears in the **Criteria/Arguments** box.  
  
11. Drag the ServiceGrade column from the list of columns in the **Mining Model** pane to the **Criteria/Arguments** box.  
  
     The name of the column is automatically inserted as the argument. You can choose any predictable attribute column to drag into this text box.  
  
12. Click the button **Switch to query results view**, in the upper corner of the Prediction Query Builder.  
  
 The expected results contain the possible predicted values for each service grade given these inputs, together with support and probability values for each prediction. You can return to design view at any time and change the inputs, or add more inputs.  
  
## Creating Predictions by using a Logistic Regression Model  
 If you already know the attributes that are relevant to the business problem, you can use a logistic regression model to predict the effect of making changes in some attributes. Logistic regression is a statistical method that is commonly used to make predictions based on changes in independent variables: for example, it is used in financial scoring, to predict customer behavior based on customer demographics.  
  
 In this task, you will learn how to create a data source that will be used for predictions, and then make predictions to help answer several business questions.  
  
### Generating Data used for Bulk Prediction  
 There are many ways to provide input data: for example, you might import staffing levels from a spreadsheet, and run that data through the model to predict service quality for the next month.  
  
 In this lesson, you will use the Data Source View designer to create a named query. This named query is a custom Transact-SQL statement that for each shift on the schedule calculates the maximum number of operators on staff, the minimum calls received, and the average number of issues that are generated. You will then join that data to a mining model to make predictions about a series of upcoming dates.  
  
##### To generate input data for a bulk prediction query  
  
1.  In Solution Explorer, right-click **Data Source Views**, and then select **New Data Source View**.  
  
2.  In the Data Source View wizard, select [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] as the data source, and then click **Next**.  
  
3.  On the **Select Tables and Views** page, click **Next** without selecting any tables.  
  
4.  On the **Completing the Wizard** page, type the name, `Shifts`.  
  
     This name will appear in Solution Explorer as the name of the data source view.  
  
5.  Right-click the empty design pane, then select **New Named Query**.  
  
6.  In the **Create Named Query** dialog box, for **Name**, type `Shifts for Call Center`.  
  
     This name will appear in Data Source View designer only as the name of the named query.  
  
7.  Paste the following query statement into the SQL text pane in the lower half of the dialog box.  
  
    ```  
    SELECT DISTINCT WageType, Shift,   
    AVG(Orders) as AvgOrders, MIN(Orders) as MinOrders, MAX(Orders) as MaxOrders,  
    AVG(Calls) as AvgCalls, MIN(Calls) as MinCalls, MAX(Calls) as MaxCalls,  
    AVG(LevelTwoOperators) as AvgOperators, MIN(LevelTwoOperators) as MinOperators, MAX(LevelTwoOperators) as MaxOperators,  
    AVG(IssuesRaised) as AvgIssues, MIN(IssuesRaised) as MinIssues, MAX(IssuesRaised) as MaxIssues  
    FROM dbo.FactCallCenter  
    GROUP BY Shift, WageType  
    ```  
  
8.  In the design pane, right-click the table, Shifts for Call Center, and select **Explore Data** to preview the data as returned by the T-SQL query.  
  
9. Right-click the tab, **Shifts.dsv (Design),** and then click **Save** to save the new data source view definition.  
  
### Predicting Service Metrics for Each Shift  
 Now that you have generated some values for each shift, you will use those values as input to the logistic regression model that you built, to generate some predictions that can be used in business planning.  
  
##### To use the new DSV as input to a prediction query  
  
1.  In Data Mining Designer, click the **Mining Model Prediction** tab.  
  
2.  In the **Mining Model** pane, click **Select Model**, and choose Call Center - LR from the list of available models.  
  
3.  From the **Mining Model** menu, clear the option, **Singleton Query**. A warning tells you that the singleton query inputs will be lost. Click **OK**.  
  
     The **Singleton Query Input** dialog box is replaced with the **Select Input Table(s)** dialog box.  
  
4.  Click **Select Case Table**.  
  
5.  In the **Select Table** dialog box, selectShifts from the list of data sources. In the **Table/View name** list, select Shifts for Call Center (it might be automatically selected), and then click **OK.**  
  
     The **Mining Model Prediction** design surface is updated to show mappings that are created based on the names and data types of columns in the input data and in the model.  
  
6.  Right-click one of the join lines, and then select **Modify Connections**.  
  
     In this dialog box, you can see exactly which columns are mapped and which are not. The mining model contains columns for Calls, Orders, IssuesRaised, and LvlTwoOperators, which you can map to any of the aggregates that you created based on these columns in the source data. In this scenario, you will map to the averages.  
  
7.  Click the empty cell next to LevelTwoOperators, and select **Shifts for Call Center.AvgOperators**.  
  
8.  Click the empty cell next to Calls, select **Shifts for Call Center.AvgCalls**. and then click **OK**.  
  
##### To create the predictions for each shift  
  
1.  In the grid at the bottom half of the **Prediction Query Builder**, click the empty cell under **Source,** and then select Shifts for Call Center.  
  
2.  In the empty cell under **Field**, select Shift.  
  
3.  Click the next empty line in the grid and repeat the procedure described above to add another row for WageType.  
  
4.  Click the next empty line in the grid. In the **Source** column, select **Prediction Function**. In the **Field** column, select **Predict**.  
  
5.  Drag the column ServiceGrade from the **Mining Model** pane down to the grid, and into the **Criteria/Argument** cell. In the **Alias** field, type **Predicted Service Grade**.  
  
6.  Click the next empty line in the grid. In the **Source** column, select **Prediction Function**. In the **Field** column, select **PredictProbability**.  
  
7.  Drag the column ServiceGrade from the **Mining Model** pane down to the grid, and into the **Criteria/Argument** cell. In the **Alias** field, type **Probability**.  
  
8.  Click **Switch to query result view** to view the predictions.  
  
 The following table shows sample results for each shift.  
  
|Shift|WageType|Predicted Service Grade|Probability|  
|-----------|--------------|-----------------------------|-----------------|  
|AM|holiday|0.165|0.377520666|  
|midnight|holiday|0.105|0.364105573|  
|PM1|holiday|0.165|0.40056055|  
|PM2|holiday|0.165|0.338532973|  
|AM|weekday|0.165|0.370847617|  
|midnight|weekday|0.08|0.352999173|  
|PM1|weekday|0.165|0.317419177|  
|PM2|weekday|0.105|0.311672027|  
  
### Predicting the Effect of Reduced Response Time on Service Grade  
 You generated some average values for each shift, and used those values as input to the logistic regression model. However, given that the business objective is to keep abandon rate within the range 0.00-0.05, the results are not encouraging.  
  
 Therefore, based on the original model, which showed a strong influence of response time on service grade, the Operations team decides to run some predictions to assess whether reducing the average time for responding to calls might improve service quality. For example, if you cut the call response time to 90 percent or even to 80 percent of the current call response time, what would happen to service grade values?  
  
 It is easy to create a data source view (DSV) that calculates the average response times for each shift, and then add columns that calculate 80% or 90% of the average response time. You can then use the DSV as input to the model.  
  
 Although the exact steps are not shown here, the following table compares the effects on service grade when you reduce response times to 80% or to 90% of current response times.  
  
 From these results, you might conclude that on targeted shifts you should reduce the response time to 90 percent of the current rate in order to improve service quality.  
  
|Shift, wage, and day|Predicted service quality with current average response time|Predicted service quality with 90 percent reduction in response time|Predicted service quality with 80 percent reduction in response time|  
|--------------------------|------------------------------------------------------------------|--------------------------------------------------------------------------|--------------------------------------------------------------------------|  
|Holiday AM|0.165|0.05|0.05|  
|Holiday PM1|0.05|0.05|0.05|  
|Holiday Midnight|0.165|0.05|0.05|  
  
 There are a variety of other prediction queries that you can create on this model. For example, you could predict how many operators are required to meet a certain service level or to respond to a certain number of incoming calls. Because you can include multiple outputs in a logistic regression model, it is easy to experiment with different independent variables and outcomes without having to create many separate models.  
  
## Remarks  
 The Data Mining Add-Ins for Excel 2007 provide logistic regression wizards that make it easy to answer complex questions, such as how many Level Two Operators would be required to improve service grade to a target level for a specific shift. The data mining add-ins are a free download, and include wizards that are based on the neural network or logistic regression algorithms. For more information, see the following links:  
  
-   [SQL Server 2005 Data Mining Add-Ins for Office 2007](https://www.microsoft.com/sql/technologies/dm/addins.mspx): Goal Seek and What If Scenario Analysis  
  
-   [SQL Server 2008 Data Mining Add-Ins for Office 2007](https://go.microsoft.com/fwlink/?LinkID=117790): Goal Seek Scenario Analysis, What If Scenario Analysis, and Prediction Calculator  
  
## Conclusion  
 You have learned to create, customize, and interpret mining models that are based on the Microsoft Neural Network algorithm and the Microsoft Logistic Regression algorithm. These model types are sophisticated and permit almost infinite variety in analysis, and therefore can be complex and difficult to master.  
  
 However, these algorithms can iterate through many combinations of factors and automatically identify the strongest correlations, providing statistical support for insights that would be very difficult to discover through manual exploration of data using Transact-SQL or even PowerPivot.  
  
## See Also  
 [Logistic Regression Model Query Examples](../../2014/analysis-services/data-mining/logistic-regression-model-query-examples.md)   
 [Microsoft Logistic Regression Algorithm](../../2014/analysis-services/data-mining/microsoft-logistic-regression-algorithm.md)   
 [Microsoft Neural Network Algorithm](../../2014/analysis-services/data-mining/microsoft-neural-network-algorithm.md)   
 [Neural Network Model Query Examples](../../2014/analysis-services/data-mining/neural-network-model-query-examples.md)  
  
  
