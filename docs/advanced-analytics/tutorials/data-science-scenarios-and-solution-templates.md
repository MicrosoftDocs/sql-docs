---
title: "Data science scenarios and solution templates | Microsoft Docs"
ms.custom: ""
ms.date: "08/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 49e54fa9-9b28-44ba-b256-06dad4e8dece
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Data science scenarios and solution templates

Templates are sample solutions that demonstrate best practices and provide building blocks to help you implement a solution fast. Each template is designed to solve a specific problem, and includes sample data, R code or Python code, and SQL stored procedures. The tasks in each template extend from data preparation and feature engineering to model training and scoring. The code can be run in your preferred R or Python development environment, with computations done in SQL Server. In some cases, you can run code directly using T-SQL and any SQL client tool, such as SQL Server Management Studio.

You can use these templates to learn how [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] works, and build and deploy your own solution by customizing he template to fit your own scenario.

See this announcement for details and updates: [Exciting new templates in Azure ML](https://blogs.technet.microsoft.com/machinelearning/2015/04/09/exciting-new-templates-in-azure-ml/)

> [!TIP]
> For download and setup instructions, see [How to use the templates](#bkmk_HowTo) at the end of this topic.

## Fraud detection

[Online fraud detectiontemplate (SQL Server R Services)](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/FraudDetection/Introduction.md)

One of the important tasks for online business is to detect fraudulent transactions, and to identify the transactions made by stolen payment instruments or credentials, in order to reduce charge back losses. When fraudulent transactions are discovered, businesses typically take measures to block certain accounts as soon as possible, to prevent further losses. In this scenario, you'll learn how to use data from online purchase transactions to identify likely fraud. This methodology is one that you can easily apply to fraud detection in other domains.

In this template, you'll learn how to use data from online purchase transactions to identify likely fraud. Fraud detection is solved as a binary classification problem. The methodology used in this template can be easily applied to fraud detection in other domains.

## Campaign optimization

[Predict how and when to contact leads](https://microsoft.github.io/r-server-campaign-optimization/)

This solution uses insurance industry data to preedict leads based ondemographics, historical response data, and product-specific details.  Recommendations are also generated to indicate the best channel and time to approach users to influence purchase behavior.

Parallel samples are provided for SQL Server in-database, in Azure, and HDInsight Spark.

## Customer churn

[Customer churn prediction template (SQL Server R Services)](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/Churn/Introduction.md)

Analyzing and predicting customer churn is important in any industry where the loss of customers to competitors must be managed and prevented:  banking, telecommunications, and retail, to name a few. The goal of churn analysis is to identify which customers are likely to churn, and then take appropriate actions to retain such customers and keep their business.

This template get you started with churn prevention by formulating the churn problem as a **binary classification** problem. It uses sample data from two sources, customer demographics and customer transactions, to classify customers as likely or unlikely to churn.
  
## Predictive maintenance

[Predictive maintenance template (SQL Server 2016)](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/PredictiveMaintenance/Introduction.md)

The goal of "data-driven" predictive maintenance is to increase the efficiency of maintenance tasks by capturing past failures and using that information to predict when or where a device might fail. The ability to forecast device obsolescence is particularly important for applications that rely on distributed data or sensors, as exemplified by the Internet of Things (IoT).

This template focuses on answering the question of “When will an in-service machine fail?” The input data represents simulated sensor measurements for aircraft engines. Data obtained from monitoring the engine’s current operation conditions, such as the current working cycle, settings, sensor measurements and so forth, are used to create three types of predictive models:

-   **Regression models**, to predict how much longer an engine will last before it fails. The sample model predicts the metric Remaining Useful Life (RUL), also called Time to Failure (TTF).
  
-   **Classification models**, to predict whether an engine is likely to fail.
  
    The **binary classification model** predicts if an engine will fail within a certain time frame (number of days).
  
    The **multi-class classification model** predicts whether a particular engine will fail, and if it will fail, provides a probable time window of failure. For example, for a given day, you can predict whether any device is likely to fail on the given day, or in some time period following the given day.

See this announcement for more information: [New predictive maintenance template](https://blogs.technet.microsoft.com/machinelearning/2015/04/09/exciting-new-templates-in-azure-ml/)

## Energy demand forecasting

[Energy demand forecasting template with SQL Server R Services](https://gallery.cortanaintelligence.com/Tutorial/Energy-Demand-Forecast_Template_with_SQL-Server-R-Services-1)

This template demonstrates how to use SQL Server R Services to predict demand for electricity. The solution includes a demand simulator, all the R and T-SQL code needed to train a model, and stored procedures that you can use to generate and report predictions.

## <a name="bkmk_HowTo"></a>How to use the templates

To download the files included in each template, you can use GitHub commands, or you can open the link and click **Download Zip** to save all files to your computer.  When downloaded, the solution typically contains these folders:
  
-   **Data**: Contains the sample data for each application.
  
-   **R**: Contains all the R development code you need for the solution. The solution requires the libraries provided by Microsoft R Server, but can be opened and edited in any R IDE. The R code has been optimized so that computations are performed "in-database", by setting the compute context to a SQL Server instance.
  
-   **SQLR**: Contains multiple .sql files that you can run in a SQL environment such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create the stored procedures that perform related tasks such as data processing, feature engineering, and model deployment.
  
    The folder also contains a PowerShell script that you can run to invoke all scripts and create the end-to-end environment. Be sure to edit the script to suit your environment.

## Next steps

+ [SQL Server machine learning tutorials](machine-learning-services-tutorials.md)




