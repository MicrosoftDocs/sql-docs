---
title: "Getting Started with Data Mining (Data Mining Add-ins for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: cbe10a19-e194-408e-a65b-5fdf3fb1e880
author: minewiskan
ms.author: owend
manager: craigg
---
# Getting Started with Data Mining (Data Mining Add-ins for Excel)
  Data mining is the process of discovering meaningful patterns in data. Data mining is a natural complement to the process of exploring and understanding your data through traditional BI. Machine algorithms can process very large amounts of data and discover patterns and trends that would otherwise be hidden.  
  
 To do data mining, you collect data that is relevant to a specific question, such as "Who are my customers?" or "what products were purchased?" and then apply an algorithm to find statistical correlations in the data. The patterns and trends found through analysis are stored as a mining model. You can then apply the mining model to new data, in business scenarios such as these:  
  
-   Use past trends to forecast sales for the next quarter, inventory requirements, or customer satisfaction.  
  
-   Apply knowledge of current customers to profile new customers and recommend new products or opportunities.  
  
-   Find correlations among past events to predict server failures or downtime.  
  
-   Analyze which products customers purchase together and use the information to recommend bundles or plan product placement.  
  
 The method of analysis you choose depends on your goals. The Data Mining Add-ins support these types of analysis:  
  
-   Supervised and unsupervised learning  
  
-   Clustering (segmentation)  
  
-   Factor analysis, using Bayesian and neural networks  
  
-   Time series analysis  
  
-   Association analysis, recommendations, and shopping basket analysis  
  
-   Scoring binary outcomes  
  
-   Linear regression  
  
 In addition, the add-ins provide help the data preparation phase: data selection, exploration, and data cleansing.  
  
## Define Your Goal  
 Before you get started, take a minute to consider the question you really want to answer. Exploration for its own sake is illuminating, but if you want to apply your findings to new data, you should be able to state clearly what you expect the model to produce, and how you will measure whether the model accomplishes your goals.  
  
 For example, rather than a goal of "finding new customers", clarify your objective to something more concrete, such as "identify the demographics of customers that are likely to buy our product, with a probability of at least 65%".  
  
-   Your dataset should contain at least one "result" attribute that you can use for training and prediction. If there is no such attribute, you can manually label some training data, or use other columns to create a proxy for the outcome.  
  
     For example, if you want to predict "the best prospects", you should apply some business rule beforehand to label existing customers, so that data mining can learn from the examples you provide.  
  
-   If you are working with a value that changes over time, and want to predict future trends, think about the granularity of the results you need. Do you want predictions for a month, day, or yearly basis? Your data has to be analyzed using the same units that you want to predict.  
  
     With cyclical patterns, if you don't get good results with daily data, try different time slices, or try using week days, months, or even holidays.  
  
-   Before you launch a wizard to find new correlations in your data, take one more look at your data and consider what sort of existing relationships might be present in the dataset. Are there confounding variables? Are there duplicates or proxies?  
  
-   What are the metrics by which the model's success will be evaluated? How will you know that the model is "good enough"?  
  
-   Do you want to make predictions from the data mining model or just look for interesting patterns and associations?  
  
## Explore Your Data and Explore the Model  
 Perhaps you already thoroughly understand the data and the domain. Even if you do, you should take some time to profile your data with modeling in mind.  
  
 Take a minute to view the distribution of values, and identify potential problems such as missing values or placeholders.  
  
 If you are planning to perform data mining against a data set that was so large or complex that you couldn't analyze it with other methods, consider sampling or data reduction.  
  
-   How is the data distributed?  
  
-   How are the columns related, or if there are multiple tables, how are the tables related?  
  
-   Are any values missing? Are there values that need conversion or preprocessing?  
  
-   Is the data mostly text, mostly numbers, or a mix?  
  
-   Do you have enough data to support analysis of the targeted outcomes? If you are analyzing associations among products, you might need lots more data. If you are predicting a binary outcome, you can get by with much less, assuming the dataset is balanced.  
  
 After your model is complete, take some time to review the results and identify ways to amend the data or get better results. It is exceedingly rare that your first model provides all the answers. Data mining is typically an iterative process.  
  
 As you try binning your data different ways, or adding new columns, remember to use the **Document Model** wizard to capture a snapshot of each model's metadata and results. Having a record will make it easier to track progress in your exploration.  
  
 [Exploring and Cleaning Data](exploring-and-cleaning-data.md)  
  
## Validate Your Model  
 As you run each wizard or tool, the algorithm analyzes the contents of the data and determines whether a statistically valid pattern exists. If the algorithm cannot find valid patterns, you'll get an error message. However, even if a model was successfully created, you'll want to test the model to see if it validates your assumptions. You can use tools such as the [Accuracy Chart &#40;SQL Server Data Mining Add-ins&#41;](accuracy-chart-sql-server-data-mining-add-ins.md) or [Cross-Validation &#40;SQL Server Data Mining Add-ins&#41;](cross-validation-sql-server-data-mining-add-ins.md) to produce statistical measures of model quality.  
  
 As you assess the results of your first model, ask yourself questions such as these:  
  
-   What kinds of patterns were found? What are the probabilities and support values?  
  
-   Were our guesses about trends correct, or were there surprising correlations?  
  
-   Did we collect enough data? Would binning the data produce clearer patterns?  
  
-   Is the data set balanced? Cross-validation can test the representativeness of your data.  
  
 [Table Analysis Tools for Excel](table-analysis-tools-for-excel.md)  
  
 [Data Mining Client for Excel &#40;SQL Server Data Mining Add-ins&#41;](data-mining-client-for-excel-sql-server-data-mining-add-ins.md)  
  
 [Choosing a Model](choosing-a-model.md)  
  
## Explore and Refine  
 If the model appears to be valid, you can use the model for prediction, recommendation, deriving insights, or planning business strategies..  
  
-   Use the data mining browser in the Data Mining Client for Excel to explore and interact with the model.  
  
-   Use Excel to rearrange and filter the results.  
  
-   Use Visio to create presentations and highlight the relationships found in the data.  
  
 Often, the first result of analysis is that you see ways to improve the analysis, or realize that you need to get new and better data. Because the models that you create using the Data Mining Add-ins for Excel can be saved to an instance of Analysis Service, it is relatively easy to update the model with new data, and continue to refine and re-use successful models.  
  
 An important use of data mining models is to create predictions and recommendations. The Data Mining Add-ins for Excel includes tools that make it easy to generate even complex prediction queries, for converting insights into actionable results. All of these tools are fully integrated with Excel.  
  
 [Viewing Models &#40;Data Mining Add-ins for Office&#41;](viewing-models-data-mining-add-ins-for-office.md)  
  
 [Validating Models and Using Models for Prediction &#40;Data Mining Add-ins for Excel&#41;](validating-models-and-using-models-for-prediction-data-mining-add-ins-for-excel.md)  
  
## See Also  
 [What's Included in the Data Mining Add-Ins for Office](what-s-included-in-the-data-mining-add-ins-for-office.md)   
 [Technical Reference &#40;Data Mining Add-ins for Excel&#41;](technical-reference-data-mining-add-ins-for-excel.md)  
  
  
