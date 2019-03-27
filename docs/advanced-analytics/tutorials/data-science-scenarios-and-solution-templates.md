---
title: Data science scenarios and solution templates - SQL Server Machine Learning
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: tutorial
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Data science scenarios and solution templates
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Templates are sample solutions that demonstrate best practices and provide building blocks to help you implement a solution fast. Each template is designed to solve a specific problem, for a specific vertical or industry. The tasks in each template extend from data preparation and feature engineering to model training and scoring. Use these templates to learn how [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] works. Then, feel free to customize the template to fit your own scenario and build a custom solution. 

Each solution includes sample data, R code or Python code, and SQL stored procedures if applicable. The code can be run in your preferred R or Python development environment, with computations done in SQL Server. In some cases, you can run code directly using T-SQL and any SQL client tool, such as SQL Server Management Studio.

> [!TIP]
> 
> Most of the templates come in multiple versions supporting both on-premises and cloud platforms for machine learning. For example, you can build the solution using only SQL Server, or you can build the solution in Microsoft R Server, or in Azure Machine Learning.

+ For details and updates, see this announcement: [Exciting new templates in Azure ML](https://blogs.technet.microsoft.com/machinelearning/2015/04/09/exciting-new-templates-in-azure-ml/)

+ For download and setup instructions, see [How to use the templates](#bkmk_HowTo).

## Fraud detection

[Online fraud detection template (SQL Server R Services)](https://github.com/Microsoft/r-server-fraud-detection)

**What:** The ability to detect fraudulent transactions is important for online businesses. To reduce charge-back losses, businesses need to quickly identify transactions that were made using stolen payment instruments or credentials. When fraudulent transactions are discovered, businesses typically take measures to block certain accounts as soon as possible, to prevent further losses. In this scenario, you learn how to use data from online purchase transactions to identify likely fraud.

**How:**  Fraud detection is solved as a binary classification problem. The methodology used in this template can be easily applied to fraud detection in other domains.


## Campaign optimization

[Predict how and when to contact leads](https://microsoft.github.io/r-server-campaign-optimization/)

**What:** This solution uses insurance industry data to predict leads based on demographics, historical response data, and product-specific details.  Recommendations are also generated to indicate the best channel and time to approach users to influence purchase behavior.

**How:** The solution creates and compares multiple models. The solution also demonstrates automated data integration and data preparation using stored procedures. Parallel samples are provided for SQL Server in-database, in Azure, and HDInsight Spark. 

## Health care: predict length of stay in hospital 

[Predicting length of stay in hospitals](https://gallery.cortanaintelligence.com/Solution/Predicting-Length-of-Stay-in-Hospitals-1)

**What:** Accurately predicting which patients might require long-term hospitalization is an important part of both care and planning. Administrators need to be able to determine which facilities require more resources, and caregivers want to guarantee that they can meet the needs of patients.

**How:** This solution uses the Data Science Virtual Machine, and includes an instance of SQL Server with machine learning enabled. It also includes a set of Power BI reports that you can use to interact with a deployed model.

## Customer churn

[Customer churn prediction template (SQL Server R Services)](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/Churn/README.md)

**What:** Analyzing and predicting customer churn is important in any industry where the loss of customers to competitors must be managed and prevented:  banking, telecommunications, and retail, to name a few. The goal of churn analysis is to identify which customers are likely to churn, and then take appropriate actions to retain such customers and keep their business.

**How:** This template formulates the churn problem as a **binary classification** problem. It uses sample data from two sources, customer demographics and customer transactions, to classify customers as likely or unlikely to churn.
  
## Predictive maintenance

[Predictive maintenance template (SQL Server 2016)](https://github.com/Microsoft/SQL-Server-R-Services-Samples/blob/master/PredictiveMaintenance/README.md)

**What:** Predictive maintenance aims to increase the efficiency of maintenance tasks by capturing past failures and using that information to predict when or where a device might fail. The ability to forecast device obsolescence is especially valuable for applications that rely on distributed data or sensors. this method could also be applied to monitor or predict error in IoT (Internet of Things) devices.

See this announcement for more information: [New predictive maintenance template](https://blogs.technet.microsoft.com/machinelearning/2015/04/09/exciting-new-templates-in-azure-ml/)

**How:** This solution focuses on answering the question, "When will an in-service machine fail?" The input data represents simulated sensor measurements for aircraft engines. Data obtained from monitoring the engine's current operation conditions, such as the current working cycle, settings, and sensor measurements, are used to create three types of predictive models:

-   **Regression models**, to predict how much longer an engine will last before it fails. The sample model predicts the metric "Remaining Useful Life" (RUL), also called "Time to Failure" (TTF).
  
-   **Classification models**, to predict whether an engine is likely to fail.
  
    The **binary classification model** predicts if an engine will fail within a certain time frame.

    The **multi-class classification model** predicts whether a particular engine might fail, and provides a probable time window of failure. For example, for a given day, you can predict whether any device is likely to fail on the given day, or in some time period following the given day.

## Energy demand forecasting

[Energy demand forecasting template with SQL Server R Services](https://gallery.cortanaintelligence.com/Tutorial/Energy-Demand-Forecast-Template-with-SQL-Server-R-Services-1)

**What:**: Demand forecasting is an important problem in various domains including energy, retail, and services. Accurate demand forecasting helps companies conduct better production planning, resource allocation, and make other important business decisions. In the energy sector, demand forecasting is critical for reducing energy storage cost and balancing supply and demand.

**How:** This template uses SQL Server R Services to predict demand for electricity. The model used for prediction is a random forest regression model based on **rxDForest**, a high-performance machine learning algorithm included in Microsoft R Server. The solution includes a demand simulator, all the R and T-SQL code needed to train a model, and stored procedures that you can use to generate and report predictions. 


## <a name="bkmk_HowTo"></a>How to use the templates

To download the files included in each template, you can use GitHub commands, or you can open the link and click **Download Zip** to save all files to your computer.  When downloaded, the solution typically contains these folders:
  
-   **Data**: Contains the sample data for each application.
  
-   **R**: Contains all the R development code you need for the solution. The solution requires the libraries provided by Microsoft R Server, but can be opened and edited in any R IDE. The R code has been optimized so that computations are performed "in-database", by setting the compute context to a SQL Server instance.
  
-   **SQLR**: Contains multiple .sql files that you can run in a SQL environment such as [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create the stored procedures that perform related tasks such as data processing, feature engineering, and model deployment.
  
    The folder also contains a PowerShell script that you can run to invoke all scripts and create the end-to-end environment. Be sure to edit the script to suit your environment.

## Next steps

+ [SQL Server machine learning tutorials](machine-learning-services-tutorials.md)




