---
title: "Adding a Data Source View for Call Center Data (Intermediate Data Mining Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: analysis-services
ms.topic: conceptual
ms.assetid: a448e7e4-dbd1-4d31-90bc-4d4a1c23b352
author: minewiskan
ms.author: owend
manager: kfile
---
# Adding a Data Source View for Call Center Data (Intermediate Data Mining Tutorial)
  In this task, you add a data source view that will be used to access the call center data. The same data will be used to build both the initial neural network model for exploration, and the logistic regression model that you will use to make recommendations.  
  
 You will also use the Data Source View Designer to add a column for the day of the week. That is because, although the source data tracks call center data by dates, your experience tells you that there are recurring patterns both in terms of call volume and service quality, depending on whether the day is a weekend or a weekday.  
  
## Procedures  
  
#### To add a data source view  
  
1.  In **Solution Explorer**, right-click **Data Source Views**, and select **New Data Source View**.  
  
     The Data Source View Wizard opens.  
  
2.  On the **Welcome to the Data Source View Wizard** page, click **Next**.  
  
3.  On the **Select a Data Source** page, under **Relational data sources**, select the [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] data source. If you do not have this data source, see [Basic Data Mining Tutorial](../../2014/tutorials/basic-data-mining-tutorial.md). Click **Next**.  
  
4.  On the **Select Tables and Views** page, select the following table and then click the right arrow to add it to the data source view:  
  
    -   **FactCallCenter (dbo)**  
  
    -   **DimDate**  
  
5.  Click **Next**.  
  
6.  On the **Completing the Wizard** page, by default the data source view is named [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)]. Change the name to **CallCenter**, and then click **Finish**.  
  
     Data Source View Designer opens to display the **CallCenter** data source view.  
  
7.  Right-click inside the Data Source View pane, and select **Add/Remove Tables**. Select the table, **DimDate** and click **OK**.  
  
     A relationship should be automatically added between the `DateKey` columns in each table. You will use this relationship to get the column, **EnglishDayNameOfWeek**, from the **DimDate** table and use it in your model.  
  
8.  In the Data Source View designer, right-click the table, **FactCallCenter**, and select **New Named Calculation**.  
  
     In the **Create Named Calculation** dialog box, type the following values:  
  
    |||  
    |-|-|  
    |**Column name**|DayOfWeek|  
    |**Description**|Get day of week from DimDate table|  
    |**Expression**|`(SELECT EnglishDayNameOfWeek AS DayOfWeek FROM DimDate where FactCallCenter.DateKey = DimDate.DateKey)`|  
  
     To verify that the expression creates the data you need, right-click the table **FactCallCenter**, and then select **Explore Data**.  
  
9. Take a minute to review the data that is available, so that you can understand how it is used in data mining:  
  
|Column name|Contains|  
|-----------------|--------------|  
|FactCallCenterID|An arbitrary key created when the data was imported to the data warehouse.<br /><br /> This column identifies unique records and should be used as the case key for the data mining model.|  
|DateKey|The date of the call center operation, expressed as an integer. Integer date keys are often used in data warehouses, but you might want to obtain the date in date/time format if you were going to group by date values.<br /><br /> Note that dates are not unique because the vendor provides a separate report for each shift in each day of operation.|  
|WageType|Indicates whether the day was a weekday, a weekend, or a holiday.<br /><br /> It is possible that there is a difference in quality of customer service on weekends vs. weekdays so you will use this column as an input.|  
|Shift|Indicates the shift for which calls are recorded. This call center divides the working day into four shifts: AM, PM1, PM2, and Midnight.<br /><br /> It is possible that the shift influences the quality of customer service so you will use this as an input.|  
|LevelOneOperators|Indicates the number of Level 1 operators on duty.<br /><br /> Call center employees start at Level 1 so these employees are less experienced.|  
|LevelTwoOperators|Indicates the number of Level 2 operators on duty.<br /><br /> An employee must log a certain number of service hours to qualify as a Level 2 operator.|  
|TotalOperators|The total number of operators present during the shift.|  
|Calls|Number of calls received during the shift.|  
|AutomaticResponses|The number of calls that were handled entirely by automated call processing (Interactive Voice Response, or IVR).|  
|Orders|The number of orders that resulted from calls.|  
|IssuesRaised|The number of issues requiring follow-up that were generated by calls.|  
|AverageTimePerIssue|The average time required to respond to an incoming call.|  
|ServiceGrade|A metric that indicates the general quality of service, measured as the *abandon rate* for the entire shift. The higher the abandon rate, the more likely it is that customers are dissatisfied and that potential orders are being lost.|  
  
 Note that the data includes four different columns that are based on a single date column: `WageType`, **DayOfWeek**, `Shift`, and `DateKey`. Ordinarily in data mining it is not a good idea to use multiple columns that are derived from the same data, as the values correlate with each other too strongly and can obscure other patterns.  
  
 However, we will not use `DateKey` in the model because it contains too many unique values. There is no direct relationship between `Shift` and **DayOfWeek**, and `WageType` and **DayOfWeek** are only partly related. If you were worried about collinearity, you could create the structure using all of the available columns, and then ignore different columns in each model and test the effect.  
  
## Next Task in Lesson  
 [Creating a Neural Network Structure and Model &#40;Intermediate Data Mining Tutorial&#41;](../../2014/tutorials/creating-a-neural-network-structure-and-model-intermediate-data-mining-tutorial.md)  
  
## See Also  
 [Data Source Views in Multidimensional Models](../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  
