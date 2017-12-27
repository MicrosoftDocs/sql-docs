---
title: "Using R in Azure SQL Database  | Microsoft Docs"
ms.custom: ""
ms.date: "12/08/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 0a90c438-d78b-47be-ac05-479de64378b2
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Using R in Azure SQL Database

In October 2017, the SQL Server development team announced plans to support execution of R code in-database using stored procedures, similar to R Services in SQL Server 2016. 

To keep up to date on the public release schedule and upcoming events, see the [SQL Server blog](https://blogs.technet.microsoft.com/dataplatforminsider/) or the [Microsoft R Server blog](https://blogs.msdn.microsoft.com/rserver/).

> [!IMPORTANT]
> Currently, the preview of R support is available in Azure SQL Database in West Central US only, and features are limited in comparison with the features for R and Python code execution in SQL Server 2016 or 2017.

## What's included

The in-database R functionality can be used on the following database service tiers and performance levels:
 
- Premium Service Tier – P1, P2, P4, P6, P11, P15
- Premium RS Service Tier – PRS1, PRS2, PRS4, PRS6
- Premium Elastic Pool – 125 eDTUs or greater
- Premium RS Elastic Pool – 125 eDTUs or greater

The preview version includes these packages:

+	Microsoft R Open with R version 3.3.3
+	Base R packages and functions are pre-installed
+	Microsoft R Server 9.2, including the RevoScaleR package

In the current preview release, you can perform the following tasks:

+ Training of models using any data that fits in memory
+	Scoring of models using any data that fits in memory
+	Trivial parallelism for R Script execution (using the @parallel parameter in sp_execute_external_script)
+	Streaming execution for R Script execution (using @r_RowsPerRead parameter in sp_execute_external_script)
+	Execute a single R script at one time


The following tasks are not supported in the preview release of R on Azure SQL Database:

+ You cannot enable R script execution on specific databases.
+ DMVs provided to monitor CPU and memory usage of R scripts are not available.
+ No third-party packages can be installed. The CREATE EXTERNAL LIBRARY statement is not supported.

## Example

In Azure SQL DB, all R commands are run from T-SQL, using the stored procedure [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql). 

The following example demonstrates how to try out the preview feature, using the iris data set included with base R.

### Step 1. Create the data tables

Start by creating two tables: one to store the source data extracted from R, and one for storing the trained model.

```sql
DROP TABLE IF EXISTS iris_data, iris_models;
GO
CREATE TABLE iris_data (
		id INT NOT NULL IDENTITY PRIMARY KEY
		, "Sepal.Length" FLOAT NOT NULL, "Sepal.Width" FLOAT NOT NULL
		, "Petal.Length" FLOAT NOT NULL, "Petal.Width" FLOAT NOT NULL
		, "Species" VARCHAR(100) NULL
);
CREATE TABLE iris_models (
	model_name VARCHAR(30) NOT NULL PRIMARY KEY,
	model VARBINARY(max) NOT NULL,
	native_model VARBINARY(MAX) NOT NULL
);
GO
```

### Step 2. Populate table with data from the iris dataset

After the tables have been created, run the following code to insert training data into the table. The stored procedure sp_execute_external_script calls R and returns the iris dataset as a data frame, using the schema specified in the INSERT INTO statement.

```sql
INSERT INTO iris_data
("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species")
EXEC sp_execute_external_script @language = N'R'
  , @script = N'iris_data <- iris;'
  , @input_data_1 = N''
  , @output_data_1_name = N'iris_data';
GO
```

### Step 3. Create the stored procedure that generates the model

The following stored procedure does the work of actually creating and training the model, which can be saved in either of two binary formats.

```sql
CREATE PROCEDURE generate_iris_model
    @trained_model VARBINARY(MAX) OUTPUT, 
    @native_trained_model VARBINARY(MAX) OUTPUT
AS
BEGIN
  EXEC sp_execute_external_script @language = N'R'
  , @script = N'
    iris_model <- rxDTree(Species ~ Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, data = iris_rx_data);
    trained_model <- as.raw(serialize(iris_model, connection=NULL));
    native_trained_model <- rxSerializeModel(iris_model, realtimeScoringOnly = TRUE)
    '
  , @input_data_1 = N'SELECT "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species" FROM iris_data'
  , @input_data_1_name = N'iris_rx_data'
  , @params = N'@trained_model VARBINARY(MAX) OUTPUT, @native_trained_model VARBINARY(MAX) OUTPUT
	, @trained_model = @trained_model OUTPUT
	, @native_trained_model = @native_trained_model OUTPUT;
End
```

+ The **OUTPUT** keyword on the input parameters indicates that the values should be passed through and used for output as well.
+ The line beginning with `iris_model` defines a decision tree model to predict species based on flower attributes.
+ The call to `serialize` saves the model in a binary format suited for storage in SQL Server. 
+ Alternatively, with models based on RevoScaleR algorithms, you can use the [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) function, which saves the model in a new native binary format. Models saved in this format can be loaded for scoring with the PREDICT function in SQL Server.

### Step 4. Train and save the model

Having created the stored procedure, call it to process input data and create a model. The following code also saves the model to the table **iris_models**, so that you can use it later to predict the species of flower.

```sql
DECLARE @model VARBINARY(MAX), @native_modelVARBINARY(MAX);
EXEC generate_iris_model @model OUTPUT, @native_model OUTPUT;
DELETE FROM iris_models WHERE model_name = 'iris.dtree';
INSERT INTO iris_models (model_name, model, native_model) VALUES('iris.dtree', @model, @native_model);
SELECT model_name, datalength(model)/1024. AS model_size_kb, datalength(native_model)/1024. AS native_model_size_kb FROM iris_models;
GO
```

### Step 5. Create a stored procedure for scoring

Next, create a stored procedure for scoring. This stored procedure loads a specified model from the table, and creates scores based on input data.

```sql
CREATE PROCEDURE predict_iris_species (@model varchar(100))
AS
BEGIN
  DECLARE @rx_model VARBINARY(MAX) = (SELECT model FROM iris_models WHERE model_name = @model);
  EXEC sp_execute_external_script @language = N'R'
  , @script = N'
    irismodel<-unserialize(rx_model);
    OutputDataSet <-rxPredict(irismodel, iris_rx_data, extraVarsToWrite = c("Species", "id"));
	  '
  , @input_data_1 = N'SELECT "id", "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "Species" FROM iris_data'
  , @input_data_1_name = N'iris_rx_data'
  , @params = N'@rx_model varbinary(max)'
  , @rx_model = @rx_model
  WITH RESULT SETS ( ("setosa_Pred" FLOAT, "versicolor_Pred" FLOAT, "virginica_Pred" FLOAT, "Species.Actual" VARCHAR(100), "id" INT));
END;
GO
```

This stored procedure uses the [rxPredict](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxpredict) function, but you could also use the native PREDICT function in T-SQL as shown [here](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/09/25/announcing-general-availability-of-native-scoring-using-predict-function-in-azure-sql-database/). Use of the PREDICT function requires that you use an [**rx** model](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-revoscaler) and save the model using [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel).

### Step 6. Use the stored procedure to generate predictions

To generate scores from the model, run the stored procedure. You can either insert the values into a table, or return the predictions to a calling application.

```sql
EXEC predict_iris_species 'iris.dtree';
GO
```

## Related resources

The Azure Marketplace also provides multiple virtual machines that include SQL Server 2017:

+ [Provision a virtual machine for machine learning on Azure](provision-the-r-server-only-sql-server-2016-enterprise-vm-on-azure.md)

Also check out these VMs, which come preconfigured with a variety of popular machine learning tools:

+ [Data Science Virtual Machine](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/overview)
+ [Deep Learning Virtual Machine](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/deep-learning-dsvm-overview)

