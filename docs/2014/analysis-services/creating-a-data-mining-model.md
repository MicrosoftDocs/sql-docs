---
title: "Creating a Data Mining Model | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "data mining models, creating"
  - "forecasting [data mining]"
  - "mining models, creating"
  - "clustering [data mining]"
  - "association [data mining]"
  - "data modeling [data mining]"
  - "estimation"
  - "classification [data mining]"
ms.assetid: 804b7db3-1f6a-4f73-a81d-bbe02520d7c6
author: minewiskan
ms.author: owend
manager: craigg
---
# Creating a Data Mining Model
  Data modeling is the step of data mining where you build patterns and trends by applying *algorithms* to data. Later you can use those patterns for analysis, or to make predictions.  
  
 The Data Mining Add-ins for Office support data mining through wizards that make it easy to create models. The wizards analyze the data, identify correlations, compute statistical significance of all variables, and automatically select the best model.  
  
 Although this functionality is every bit as powerful as the data mining tools provided by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] and [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], the combination of wizards and the familiar Excel interface makes it easy to create, modify, and use data mining.  
  
## Advanced (Data Mining)  
 The Advanced wizards let you create new data mining models, based on data stored in Excel, by using one of the data mining algorithms in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
### Create Mining Structure  
 The Create Mining Structure wizard helps you build a new data mining structure, which you can use as the basis for multiple mining models. The wizard gives you the option to set aside a portion of the data to use as a testing set, so that you can evaluate all models that use the same data according to a consistent testing standard.  
  
 [Create Mining Structure &#40;SQL Server Data Mining Add-ins&#41;](create-mining-structure-sql-server-data-mining-add-ins.md)  
  
### Add Model to Structure  
 The Add Model to Structure wizard lets you choose an existing data mining structure and create a new data mining model for it. You can add multiple mining models to a structure, changing the parameters or choosing different data mining algorithms, and customize the output.  
  
 [Add Model to Structure &#40;Data Mining Add-ins for Excel&#41;](add-model-to-structure-data-mining-add-ins-for-excel.md)  
  
## Analyze Key Influencers (Analyze)  
 You choose a column or output value of interest, and then the algorithm analyzes all the input data to identify the factors that have the most influence on the target. Optionally, you can create a report that compares any two values, so that you can see how the influencers change.  
  
 The **Analyze Key Influencers** tool uses the Microsoft Naïve Bayes algorithm.  
  
 [Analyze Key Influencers &#40;Table Analysis Tools for Excel&#41;](analyze-key-influencers-table-analysis-tools-for-excel.md)  
  
## Associate (Data Mining)  
 The **Associate** wizard builds an association model that detects associations between items that appear in multiple transactions: for example, in market basket analysis.  
  
 [Associate Wizard &#40;Data Mining Client for Excel&#41;](associate-wizard-data-mining-client-for-excel.md)  
  
## Classify (Data Mining)  
 The  **Classify** wizard builds a classification model that analyzes the factors that contributed to a target outcome. You can use multiple algorithms with this wizard, including Decision Trees, Naïve Bayes, and Neural Networks.  
  
 [Classify Wizard &#40;Data Mining Add-ins for Excel&#41;](classify-wizard-data-mining-add-ins-for-excel.md)  
  
## Cluster (Data Mining)  
 The **Cluster** wizard builds a clustering model that detects groups of rows that share similar characteristics. Clustering (sometimes called *segmentation*) is an unsupervised learning technique that is very useful when trying to understand patterns and groupings in new data.  
  
 The Microsoft Clustering algorithm supports several varieties of both K-means and Expectation maximization (EM) clustering  
  
 [Cluster Wizard &#40;Data Mining Add-ins for Excel&#41;](cluster-wizard-data-mining-add-ins-for-excel.md).  
  
## Detect Categories (Analyze)  
 The **Detect Categories** tool lets you add any data set and apply clustering to find groupings of data. It's useful for finding similarities and for creating groups to further analyze.  
  
 The **Detect Categories** tool uses the Microsoft Clustering algorithm.  
  
 [Detect Categories &#40;Table Analysis Tools for Excel&#41;](detect-categories-table-analysis-tools-for-excel.md)  
  
## Estimate (Data Mining)  
 The Estimate wizard builds an estimation model that extracts data patterns and uses the patterns to predict continuous numeric, date, or time values. It uses the [!INCLUDE[msCoName](../includes/msconame-md.md)] Decision Trees algorithm.  
  
 [Estimate Wizard &#40;Data Mining Add-ins for Excel&#41;](estimate-wizard-data-mining-add-ins-for-excel.md)  
  
## Fill From Example (Analyze)  
 The **Fill From Example** tool helps you impute missing values. You provide some examples of what the missing values should be, and the tool builds patterns based on all data in the table, and then recommends new values based on patterns in the data.  
  
 The **Fill From Example** tool uses the Microsoft Logistic Regression algorithm.  
  
 [Fill From Example &#40;Table Analysis Tools for Excel&#41;](fill-from-example-table-analysis-tools-for-excel.md)  
  
## Forecast (Analyze)  
 The **Forecast** tool takes data that changes over time, and predicts future values.  
  
 The **Forecast** tool uses the Microsoft Time Series algorithm.  
  
 [Forecast &#40;Table Analysis Tools for Excel&#41;](forecast-table-analysis-tools-for-excel.md)  
  
## Forecast (Data Mining)  
 The **Forecast** wizard builds a forecasting model that detects patterns in a series of cells, and then forecasts additional values.  
  
 [Forecast Wizard &#40;Data Mining Add-ins for Excel&#41;](forecast-wizard-data-mining-add-ins-for-excel.md)  
  
## Highlight Exceptions (Analyze)  
 The **Highlight Exceptions** tool analyzes patterns in a table of data and finds rows and values that don't fit the pattern. You can then review and correct them and rerun the model, or flag values for later action.  
  
 The **Highlight Exceptions** tool uses the Microsoft Clustering algorithm.  
  
 [Highlight Exceptions &#40;Table Analysis Tools for Excel&#41;](highlight-exceptions-table-analysis-tools-for-excel.md)  
  
## Prediction Calculator (Analyze)  
 This tool creates a model that analyzes the factors leading to target outcomes, and then predicts a result for any new input, based on criteria derived from these patterns It also generates an interactive decision making worksheet that lets you easily score new inputs. You can also create a printed version of the scoring worksheet for offline use.  
  
 The **Prediction Calculator** tool uses the Microsoft Logistic Regression algorithm.  
  
 [Prediction Calculator &#40;Table Analysis Tools for Excel&#41;](prediction-calculator-table-analysis-tools-for-excel.md)  
  
## Scenario: Goal Seek (Analyze)  
 In the **Goal Seek** tool, you specify a target value, and the tool identifies the underlying factors that must change to meet that target. For example, if you know that you must increase call satisfaction by 20%, you can ask the model to predict the factors that should change to produce that goal.  
  
 The **Goal Seek** tool uses the Microsoft Logistic Regression algorithm.  
  
 details  
  
 [Goal Seek Scenario &#40;Table Analysis Tools for Excel&#41;](goal-seek-scenario-table-analysis-tools-for-excel.md)  
  
## Scenario: What-If Scenario (Analyze)  
 The **What-If Analysis** tool complements the **Goal Seek** tool. With this tool, you entered the value you want to change, and the model predicts whether that change will be sufficient to achieve the desired outcome. For example, you might ask the model to infer whether adding one extra call operator would increase customer satisfaction by one point.  
  
 The **What-If** tool uses the Microsoft Logistic Regression algorithm.  
  
 [What-If Scenario &#40;Table Analysis Tools for Excel&#41;](what-if-scenario-table-analysis-tools-for-excel.md)  
  
## Shopping Basket Analysis (Analyze)  
 The **Shopping Basket Analysis** tool creates groups of products that are frequently purchased together, to identify patterns that can be used in cross-selling or up-selling. It also generates reports based on the price and cost of related product bundles, to aid in decision-making.  
  
 You can also use this tool for events that frequently occur together, factors leading to a diagnosis, or any other set of potential causes and outcomes.  
  
 The **Shopping Basket Analysis** tool uses the Microsoft Association algorithm.  
  
 [Shopping Basket Analysis &#40;Table AnalysisTools for Excel&#41;](shopping-basket-analysis-table-analysistools-for-excel.md)  
  
## See Also  
 [Exploring and Cleaning Data](exploring-and-cleaning-data.md)   
 [Validating Models and Using Models for Prediction &#40;Data Mining Add-ins for Excel&#41;](validating-models-and-using-models-for-prediction-data-mining-add-ins-for-excel.md)   
 [Deploying and Scaling Mining Models &#40;Data Mining Add-ins for Excel&#41;](deploying-and-scaling-mining-models-data-mining-add-ins-for-excel.md)  
  
  
