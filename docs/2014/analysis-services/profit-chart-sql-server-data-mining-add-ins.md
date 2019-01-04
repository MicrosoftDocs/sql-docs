---
title: "Profit Chart (SQL Server Data Mining Add-ins) | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "accuracy chart"
  - "profit chart"
  - "mining models, charting"
  - "mining models, testing"
ms.assetid: 5c902543-4da9-4db3-99d5-4ce04c43d7ef
author: minewiskan
ms.author: owend
manager: craigg
---
# Profit Chart (SQL Server Data Mining Add-ins)
  ![Profit Chart button in Data Mining ribbon](media/dmc-profitchart.gif "Profit Chart button in Data Mining ribbon")  
  
 A profit chart displays the estimated profit increase that is associated with using a mining model to determine which customers a company should contact in a business scenario. The Y-axis of the chart represents the profit, while the X-axis represents the percentage of the population that the company contacted. A typical profit chart shows an increase in profits up to a point, after which profits decrease as more of the population is contacted.  
  
## Configuring the Profit Chart  
 Whereas the accuracy chart assesses only the probability that predictions are right or wrong, the profit chart incorporates real-world knowledge about the consequences of taking action on a prediction. This is achieved by taking into account the following factors, which you specify when you run the wizard:  
  
-   **Population**  
  
     The number of cases in the dataset that is being used to create the lift chart. For example, the number of potential customers.  
  
-   **Fixed Cost**  
  
     The fixed cost that is associated with the business problem. If this were for a targeted mailing solution, the cost would not depend on variables such as the number of telephone calls made or the number of promotional mailings sent.  
  
-   **Individual Cost**  
  
     Costs that are in addition to the fixed cost, that can be associated with each customer contact. For example, promotional mailings or telephone calls.  
  
-   **Revenue Per Individual**  
  
     The amount of revenue that is associated with each successful sale.  
  
## Using the Profit Chart Wizard  
 To create a profit chart, you must reference an existing data mining model. You can browse models to find a model that matches your data by clicking **Manage Models** or **Browse** to see details about the algorithm that was used and the columns in the mining model.  
  
 For more information, see [Browsing Models in Excel &#40;SQL Server Data Mining Add-ins&#41;](browsing-models-in-excel-sql-server-data-mining-add-ins.md) and [Manage Models &#40;SQL Server Data Mining Add-ins&#41;](manage-models-sql-server-data-mining-add-ins.md).  
  
#### To create a profit chart  
  
1.  Select an existing model.  
  
2.  Specify the column that you want to predict, and a target value, if appropriate.  
  
3.  Select the source data, which means the data you will pass through the model in order to create a prediction. This should not be the same data that you used to create the model.  
  
4.  Map the columns in the new (source) date to the columns used in the data mining model. If the column names are similar, the wizard will automatically map them.  
  
5.  Enter the cost information required by the wizard: the fixed cost, individual cost, the population, and the revenue expected.  
  
6.  Optionally, you can enter a graduated series of costs (click the browse **(...)** button). For example, a mailing might become cheaper as you increase the number of items that are sent, so you can enter a different cost depending on the number of items, and the wizard will automatically adjust the costs for each sample size.  
  
7.  The wizard creates a chart that includes the cost-benefit analysis for the model.  
  
### Requirements  
 If you are predicting a discrete numeric value, you must select the exact target value to predict.  
  
## Understanding the Profit Chart  
 The profit chart contains a gray vertical line, which you can move by clicking a location in the chart. The **Mining Legend** displays a score, the population correct, and the predict probability that are associated with the location of the gray line on the chart. If you select the maximum point of profits in the chart by using the gray line, you can use the predict probability value to determine a probability threshold for contacting a customer.  
  
 For example, if the peak of the profit curve is at 55 percent of the population and the associated predict probability is 20 percent, this indicates that to achieve maximum profits you should only contact those customers whose response is predicted with a 20 percent or greater probability.  
  
## See Also  
 [Validating Models and Using Models for Prediction &#40;Data Mining Add-ins for Excel&#41;](validating-models-and-using-models-for-prediction-data-mining-add-ins-for-excel.md)  
  
  
