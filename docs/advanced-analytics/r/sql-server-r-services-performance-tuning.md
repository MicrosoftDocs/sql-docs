---
title: SQL Server R Services Performance Tuning - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Performance tuning for R in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article is the first in a series of four articles that describe performance optimization for R Services, based on two case studies:

- Performance tests conducted by the product development team to validate the performance profile of R solutions

- Performance optimization by the Microsoft data science team for a specific machine learning scenario often requested by customers.

The goal of this series is to provide guidance about the types of performance tuning techniques that are most useful for running R jobs in SQL Server.

+ This (first) topic provides an overview of the architecture and some common problems when optimizing for data science tasks.
+ The second article covers specific hardware and SQL Server optimizations.
+ The third article covers optimizations in R code and resources for operationalization.
+ The fourth article describes test methods in detail, and reports findings and conclusions.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Performance goals and targeted scenarios

The R Services feature was introduced in SQL Server 2016 to support execution of R scripts by SQL Server. When you use this feature, the R runtime operates in a separate process from the database engine, but exchanges data securely with the database engine, using server resources and services that interact with SQL Server, such as the Trusted Launchpad.

In SQL Server 2017, support was announced for running Python scripts using the same architecture, with additional language to follow in future.

As the number of supported versions and language grows, it is important that the database administrator and database architect are aware of the potential for resource contention due to machine learning jobs, and that they are able to configure the server to support the new workloads. Although keeping analytics close to the data eliminates insecure data movements, it also moves analytical workloads off the laptop of the data scientist and back onto the server hosting the data.

Performance optimization for machine learning is clearly not a one-size-fits-all proposition. The following common tasks might have very different performance profiles:

- Training tasks: creating and training a regression model vs. training a neural network
- Feature engineering using R vs. feature extraction using T-SQL
- Scoring on single rows or small batches, vs. bulk scoring using tabular inputs
- Running scoring in R vs. deploying models to production on SQL Server in stored procedures
- Modifying R code to minimize data transfer or remove costly data transformations
- Enable automated testing and retraining of models

Because the choice of optimization techniques depends on which task is critical for your application or use case, the case studies cover both general performance tips, and guidance on how to optimize for a specific scenario, optimization for batch scoring.

+ **Individual optimization options in SQL Server**

    In the first performance case study, multiple tests were run on a single dataset using a single type of model. The rxLinMod algorithm in RevoscaleR was used to build a model and create scores from it, but the code as well as the underlying data tables were systematically altered to test the impact of each change.

    For example, in one test run, the R code was altered so that a comparison could be made between feature engineering using a transformation function vs. pre-computing the features and then loading features from a table. In another test run, model training performance was compared between using a standard indexed table vs. data in a table with various types of compression or new index types.

+ **Optimization for a specific high-volume scoring scenario**

    The machine learning task in the second case study involves processing many resumes submitted for multiple positions, and finding the best candidate for each job position. The machine learning model itself is formulated as a binary classification problem: it takes a resume and job description as input, and generates the probability for each resume-job pair. Because the goal is to find the best match, a user-defined probability threshold is used to further filter and get just the good matches.

    For this solution, the main objective was to achieve low latency during scoring. However, this task is computationally expensive even during the scoring process, because each new job must be matched against millions of resumes within a reasonable time frame. Moreover, the feature engineering step produces over 2000 features per resume or job, and is a significant performance bottleneck.

We suggest that you review all results from the first case study to determine which techniques are applicable to your solution, and weigh their potential impact.

Then, review the results of the scoring optimization case study to see how the author applied different techniques and optimized the server to support a particular workload.

## Performance optimization process

Configuration and tuning for performance requires creating a solid base, on which to layer optimizations designed for specific workloads:

- Choose an appropriate server to host analytics. Typically, a reporting secondary, data warehouse or other server that is already used for other reporting or analytics is preferred. However, in a hybrid transactional-analytical processing (HTAP) solution, operational data can be used as the input to R for fast scoring.

- Configure the SQL Server instance to balance database engine operations and R or Python script execution at appropriate levels. This can include changing SQL Server defaults for memory and CPU usage, NUMA and processor affinity settings, and creation of resource groups.

- Optimize the server computer to support efficient use of external scripts. This can include increasing the number of accounts used for external script execution, enabling package management, and assigning users to related security roles.

- Applying optimizations specific to data storage and transfer in SQL Server, including indexing and statistics strategies, query design and query optimization, and the design of tables that are used for machine learning. You might also analyze data sources and data transport methods, or modify ETL processes to optimize feature extraction.

- Tune the analytical model to avoid inefficiencies. The scope of the optimizations that are possible depend on the complexity of your R code and the packages and data you are using. Key tasks include eliminating costly data transformations or offloading data preparation or feature engineering tasks from R or Python to ETL processes or SQL. You might also refactor scripts, eliminate in-line package installs, divide R code into separate procedures for training, scoring, and evaluation, or simplify code to work more efficiently as a stored procedure.

## Articles in this series

+ [Performance tuning for R in SQL Server - hardware](../r/sql-server-configuration-r-services.md)

    Provides guidance for configuring the hardware that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is installed on, and for configuring the SQL Server instance to better support external scripts. It is particularly useful for **database administrators**.

+ [Performance tuning for R in SQL Server - code and data optimization](../r/r-and-data-optimization-r-services.md)

    Provides specific tips on how to optimize the external script to avoid known problems. It is most useful to **data scientists**.

    > [!NOTE]
    > While much of the information in this section applies to R in general, some information is specific to RevoScaleR analytic functions. Detailed performance guidance is not available for **revoscalepy** and other supported Python libraries.
    >

+ [Performance tuning for R in SQL Server - methods and results](../r/performance-case-study-r-services.md)

    Summarizes what data was used the two case studies, how performance was tested, and how the optimizations affected results.
