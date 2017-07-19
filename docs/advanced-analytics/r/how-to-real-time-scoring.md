---
title: "How to perform realtime scoring or native scoring in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
## How to perform realtime scoring or native scoring in SQL Server

This topic provides instructions and sample code for how to execute the realtime scoring and native scoring features in SQL Server 2016 and SQL Server 2017. The goal of both realtime scoring and native scoring is to improve the performance of scoring operations in small batches.

Both realtime scoring and native scoring are designed to let you use a machine learning model without having to install R. All you need to do is obtain a pretrained model in a compatible format, and save it in a SQL Server database.

## Choosing a scoring method

The following options are supported for fast batch prediction:

+ **Native scoring**: T-SQL PREDICT function in SQL Server 2017
+ **Realtime scoring**: Using the sp_rxPredict stored procedure in either  SQL Server 2016 or SQL Server 2017.

> [!NOTE]
> Use of the PREDICT function is recommended in SQL Server 2017.
> To use sp_rxPredict requires that you enable SQLCLR integration. Consider the security implications before you enable this option.

The overall process of preparing the model and then generating scores is very similar:

1. Create a model using a supported algorithm.
2. Serialize the model using a special binary format.
3. Make the model available to SQL Server. Typically this means storing the serialized model in a SQL Server table.
4. Call the function or stored procedure, and pass the model and input data.

### Requirements

+ The PREDICT function is available in all editions of SQL Server 2017 and is enabled by default. You do not need to install R or enable additional features.

+ If using sp\_rxPredict, some additional steps are required. See [Enable realtime scoring](#bkmk_enableRtScoring).

+ At the time of this writing, only RevoScaleR and MicrosoftML can create compatible models. Additional model types might become available in future. For the list of currently supported algorithms, see [Realtime scoring](../real-time-scoring.md).

### Serialization and storage

To use a model with either of the fast scoring options, the model must be saved in a special serialized format, which has been optimized for size and scoring efficiency.

+ Call `rxSerializeModel` to write a supported model to the **raw** format.
+ Call `rxUnserializeModel` to reconstitute the model for use in other R code, or to view the model.

For more information, see [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel).

**Using SQL**

From SQL code, you can train the model using `sp_execute_external_script`, and directly insert the trained models into a table, in a column of type **varbinary(max)**.

For a simple example, see [this tutorial](/tutorials/rtsql-create-a-predictive-model-r.md)

**Using R**

From R code, there are two ways to save the model to a table:

+ Call the `rxWriteObject` function, from the RevoScaleR package, to write the model directly to the database.

  The `rxWriteObject()` function can retrieve R objects from an ODBC data source like SQL Server, or write objects to SQL Server. The API is modeled after a simple key-value store.
  
  If you use this function, be sure to serialize the model using the new serialization function first. Then, set the *serialize* flag in `rxWriteObject` to FALSE, to avoid repeating the serialization step.

+ You can also save the model in raw format to a file and then read from the file into SQL Server. This option might be useful if you are moving or copying models between environments.

## Native scoring with PREDICT

In this example, you'll create a model and then call the realtime prediction function from T-SQL.

### 1. Prepare and save the model

Run the following code to create the sample database and required tables.

```SQL
CREATE DATABASE NativeScoringTest;
GO
USE NativeScoringTest;
GO
DROP TABLE IF EXISTS iris_rx_data;
GO
CREATE TABLE iris_rx_data (
  "Sepal.Length" float not null, "Sepal.Width" float not null
  , "Petal.Length" float not null, "Petal.Width" float not null
  , "Species" varchar(100) null
);
GO
```

Use the following statement to populate the data table with data from the **iris** dataset.

```SQL
INSERT INTO iris_rx_data ("Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width" , "Species")
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N'iris_data <- iris;'
  , @input_data_1 = N''
  , @output_data_1_name = N'iris_data';
GO
```

Now, create a table for storing models.

```SQL
DROP TABLE IF EXISTS ml_models;
GO
CREATE TABLE ml_models ( model_name nvarchar(100) not null primary key
  , model_version nvarchar(100) not null
  , native_model_object varbinary(max) not null);
GO
```

The following code creates a model based on the **iris** dataset and saves it to the models table.

```SQL
DECLARE @model varbinary(max);
EXECUTE sp_execute_external_script
  @language = N'R'
  , @script = N'
    iris.sub <- c(sample(1:50, 25), sample(51:100, 25), sample(101:150, 25))
    iris.dtree <- rxDTree(Species ~ Sepal.Length + Sepal.Width + Petal.Length + Petal.Width, data = iris[iris.sub, ])
    model <- rxSerializeModel(iris.dtree, realtimeScoringOnly = TRUE)
    '
  , @params = N'@model varbinary(max) OUTPUT'
  , @model = @model OUTPUT
  INSERT [dbo].[ml_models]([model_name], [model_version], [native_model_object])
  VALUES('iris.dtree','v1', @model) ;
```

> [!NOTE] 
> You must use the [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel) function from RevoScaleR to save the model. The standard R `serialize` function cannot generate the required format.

You can run a statement such as the following to view the stored model in binary format:

```SQL
SELECT *, datalength(native_model_object)/1024. as model_size_kb
FROM ml_models;
```

### 2. Run PREDICT on the model

The following simple PREDICT statement gets a classification from the decision tree model using the **native scoring** function. It predicts the iris species based on attributes you provide, petal length and width.

```SQL
DECLARE @model varbinary(max) = (
  SELECT native_model_object
  FROM ml_models
  WHERE model_name = 'iris.dtree.model'
  AND model_version = 'v1');
SELECT d.*, p.*
  FROM PREDICT(MODEL = @model, DATA = dbo.iris_rx_data as d)
  WITH(setosa_Pred float, versicolor_Pred float, virginica_Pred float) as p;
go
```

If you get the error, "Error occurred during execution of the function PREDICT. Model is corrupt or invalid", it usually means that your query didn't return a model. Check whether you typed the model name correctly, or if the models table is empty.

> [!NOTE]
> Because the columns and values returned by **PREDICT** can vary by model type, you must define the schema of the returned data by using a **WITH** clause.

## Realtime scoring with sp_rxPredict

This section describes the steps required to set up **realtime** prediction, and provides an example of how to call the function from T-SQL.

### <a name ="bkmk_enableRtScoring"></a> 1. Enable the realtime scoring procedure

You must enable this feature for each database that you want to use for scoring. The server administrator should run the command-line utility, RegisterRExt.exe, which is included with the RevoScaleR package.

> [!NOTE]
> In order for realtime scoring to work, SQL CLR functionality needs to be enabled in the instance and the database needs to be marked trustworthy. When you run the script, these actions are performed for you. However, you should consider the additional security implications of doing this.

1. Open an elevated command prompt, and navigate to the folder where RegisterRExt.exe is located. The following path can be used in a default installation:
    
    `<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\`

2. Run the following command, substituting the name of your instance and the target database where you want to enable the extended stored procedures:

    `RegisterRExt.exe /installRts [/instance:name] /database:databasename`

    For example, to add the extended stored procedure to the CLRPredict database on the default instance, type:

    `RegisterRExt.exe /installRts /database:CLRPRedict`

    The instance name is optional if the database is on the default instance. If you are using a named instance, you must specify the instance name.

3. RegisterRExt.exe creates the following objects:

	+ Trusted assemblies
	+ The stored procedure `sp_rxPredict`
	+ A new database role, `rxpredict_users`. The database administrator can use this role to grant permission to users who use the realtime scoring functionality.

4. Add any users who need to run `sp_rxPredict` to the new role.

> [!NOTE]
> 
> In SQL Server 2017, additional security measures are in place to prevent problems with CLR integration. These measures impose additional restrictions on the use of this stored procedure as well.


### 2. Prepare and save the model

The binary format required by sp\_rxPredict is the same as that for PREDICT.

Therefore, in your R code, include a call to [rxSerializeModel](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxserializemodel), and be sure to specify _realtimeScoringOnly_ = TRUE, as in this example:

```R
model <- rxSerializeModel(model.name, realtimeScoringOnly = TRUE)
```

### 3. Call sp_rxPredict

You call sp_rxPredict as you would any other stored procedure. In the current release, the stored procedure takes only two parameters: _@model_ for the model in binary format, and _@inputData_ for the data to use in scoring, defined as a valid SQL query.

Because the binary format is the same that is used by the PREDICT function, you can use the models and data table from the preceding example.

```SQL
DECLARE @irismodel varbinary(max)
SELECT @irismodel = [native_model_object] from [ml_models]
WHERE model_name = 'iris.dtree.model' AND model_version = 'v1''

EXEC sp_rxPredict
@model = @irismodel,
@inputData = N'SELECT * FROM iris_rx_data'
```

> [!NOTE]
> 
> The call to `sp_rxPredict` fails if the input data for scoring does not include columns that match the requirements of the model. Currently, only the following .NET data types are supported: double, float, short, ushort, long, ulong and string.
> 
> Therefore, you might need to filter out unsupported types in your input data before using it for realtime scoring.
> 
> For information about corresponding SQL types, see [SQL-CLR Type Mapping](https://msdn.microsoft.com/library/bb386947.aspx) or [Mapping CLR Parameter Data](https://docs.microsoft.com/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data).

### Disable realtime scoring

To disable realtime scoring functionality, open an elevated command prompt, and run the following command: `RegisterRExt.exe /uninstallrts /database:<database_name> [/instance:name]`

### Realtime scoring in Microsoft R Server

For information regarding realtime scoring in a distributed environment based on Microsoft R Server, please refer to the [publishService](https://msdn.microsoft.com/microsoft-r/mrsdeploy/packagehelp/publishservice) function available in the [mrsDeploy package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy), which supports publishing models for realtime scoring as a new a web service running on R Server.
