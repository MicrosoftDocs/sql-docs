---
title: "Real-time scoring in SQL Server 2016 with Machine Learning Server | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2017"
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

# Real-time Scoring

This topic describes a new feature in SQL Server 2017 that provides scoring on machine learning models in near real-time.

+ What is real-time scoring vs. native scoring
+ How it works
+ Supported platforms

## What is real-time scoring

In SQL Server 2016, Microsoft created an extensibility framework that allows R scripts to be executed from T-SQL. This framework supports any operation you might perform in R, ranging from simple functions to training complex machine learning models. However, the dual-process architecture that pairs R with SQL Server means that external R processes must be invoked for every call, regardless of the complexity of the operation. If you are loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external R process represents an unnecessary performance cost.

Scoring is a two-step process: a pre-trained model is loaded from a table, and new input data, either  table or single row, is provided as input. When scoring high volumes of data very fast, the scores are typically inserted into a table as part of the scoring procedure.  You can also retrieve a single score in real time. In this case, to scoring of successive inputs, the model might be cached so that it can be reloaded into memory very fast.

To support extremely fast scoring in both scenarios, SQL Server Machine Learning Services (and Microsoft Machine Learning Server) provide built-in prediction and scoring libraries. These include:

+ The rxPredict function, for fast scoring within R code
the PREDICT function in Transact-SQL, for scoring without having to call the R runtime

### Supported platforms

+ SQL Server 2017 Machine Learning Services (includes Microsoft R Server 9.1.0)
+ Microsoft Machine Learning Server
+ SQL Server R Services 2016, with an upgrade of the R Services instance to Microsoft R Server 9.1.0 or later

### Benefits

Because a model can be published and used for scoring without having to call the R interpreter, the overhead of multiple process interactions is reduced. This supports much faster prediction performance in enterprise production scenarios.

Also, because the data never leaves SQL Server, results can be generated and inserted into a new table without any data translation between R and SQL.

## How it works

To use real-time scoring in SQL Server 2017, call the new scoring function, and pass the following required inputs:

+ a compatible model
+ a SQL query that defines the input data

The function returns predictions for the input data, together with any columns of source data that you want to pass through.

sp_rxPredict is a table-valued function and supports CTE. It can have multiple scalar outputs.
To support very fast scoring, the deserialized model is cached until batch execution is complete.

In Microsoft R Server (or Machine Learning Server Standalone), the equivalent functionality is provided by the [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict) function. This function supports the same models and transformations, and has been optimized for very high throughput.

For an example of how rxPredict can used for scoring ,see this sample: [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

### Requirements

+ On SQL Server, you must enable the real-time scoring feature in advance. This is because the feature requires installation of CLR-based libraries into SQL Server.
+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](#supported-algorithms).
+ The model must be saved using the new serialization function provided in Microsoft R Server 9.1.0. The serialization function is optimized to support fast scoring.

### Supported algorithms

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlinmod)
  + [rxLogit](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxlogit)
  + [rxBTrees](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxbtrees)
  + [rxDtree](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdtree)
  + [rxdForest](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxdforest)

+ MicrosoftML models

  + [rxFastTrees](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfasttrees)
  + [rxFastForest](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfastforest)
  + [rxLogisticRegression](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxlogisticregression)
  + [rxOneClassSvm](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxoneclasssvm)
  + [rxNeuralNet](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxneuralnet)
  + [rxFastLinear](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfastlinear)

+ Transformations supplied by MicrosoftML

  + [featurizeText](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxfasttrees)
  + [concat](https://docs.microsoft.com/r-server/r-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/r-server/r-reference/microsoftml/categorical)
  + [categoricalHash](https://docs.microsoft.com/r-server/r-reference/microsoftml/categoricalHash)
  + [selectFeatures](https://docs.microsoft.com/r-server/r-reference/microsoftml/selectFeatures)

### Restrictions

The following model types are not supported:

+ Models containing other, unsupported types of R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR 
+ PMML models
+ Models created using other R libraries from CRAN or other repositories
+ Models containing any other kind of R transformation other than those listed here

See the [Technical Notes](#bkmk_Notes) section for a list of further restrictions and known issues.

## Perform real-time scoring

in this section, we'll walk through the steps required to set up real-time prediction, and then call the function from T-SQL.

1. Enable the serialization feature
2. Serialize the model using the format required for real-time scoring
3. Publish the model to SQL Server as described here
4. Use T-SQL to call real-time scoring stored procedure

### 1. Enable serialization on the database

By default, real-time scoring functionality is disabled in SQL Server. You must enable this feature for each SQL Server database that you want to use for scoring. A server administrator must use the RegisterRExt.exe command-line utility to enable real-time scoring. This utility is included with the RevoScaleR package.

> [!NOTE] 
> In order for real-time scoring to work, SQL CLR functionality needs to be enabled in the instance and the database needs to be marked trustworthy. When you run the script, these actions are performed for you. However, you should consider the additional security implications of doing this.

1. Open an elevated command prompt, and navigate to the folder where RegisterRExt.exe is located. The following path can be used in a default installation:
    
    `<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\`

2. Run the following command, substituting the name of your instance and the target database where you want to enable the extended stored procedures:

    `RegisterRExt.exe /installRts [/instance:name] /database:databasename`

    For example, to add the extended stored procedure to a database on the default instance, type:

    `RegisterRExt.exe /installRts /database:RSampleDatabase`

    The instance name is optional if the database is on the default instance. If you are using a named instance, you must specify the instance name.

4. RegisterRExt.exe creates the following objects:

	+ Trusted assemblies
	+ The stored procedure sp_rxPredict
	+ A new database role, `rxpredict_users`. The database administrator can use this role to grant permission to users who will run the real-time scoring functionality.

> [!TIP]
> To disable real-time scoring functionality, open an elevated command prompt, and run the following command: `RegisterRExt.exe /uninstallrts /database:databasename [/instance:name]`

### Step 2. Serialize the model

Any model that you call using this procedure must be saved in a special serialized format, that is optimized for size and fast loading for scoring. The serialization format uses an optimized raw format (not human readable) that allows the model to be scored in an efficient manner.

Use `rxSerializeModel` to write a supported model to the **raw** format. The model must be based on a supported model type from the RevoScaleR or MicrosoftML packages.

If you want to use the model in R code later, or view the model, call `rxUnserializeModel`. This function reconstitutes the original R model object from the serialized raw format.

### Step 3. Publish model to SQL Server

Publishing refers to the process of saving a trained model, using the new serialized format, in a table in SQL Server. A model that has been published using this process can be read from a table for real-time scoring, without having to invoke the R runtime.

The database containing the table must also have real-time scoring enabled. If you have enabled real-time scoring on the database by using the script, the requirement is met. Moreover, the model must be saved using a column of type VARBINARY.

When creating the model from your R code, there are two ways to save the model to a table:

+ Call the `rxWriteObject` function, from the RevoScaleR package, to write the model directly to the database.

  The `rxWriteObject()` function can retrieve R objects from an ODBC data source like SQL Server, or write objects to SQL Server. The API is modeled after a simple key-value store.
  
  If you use this function, be sure to serialize the model using the new serialization function first. Then, set the *serialize* flag in `rxWriteObject` to FALSE, to avoid repeating the serialization step.

+ You can also save the model in raw format to a file and then read from the file into SQL Server. This option might be useful if you are moving or copying models between environments.

### Step 4. Call sp_rxPredict

After you have enabled the real-time scoring feature, the SQL Server database will have a new stored procedure called **sp_rxPredict**.

**Syntax**

```sql
DECLARE @model = SELECT @model from model_table where model_name = 'rxLogit trained'
sp_rxPredict @model = @model,
@inputData = N'SELECT * FROM data'
```

+ *@inputData* must be a valid SQL query. The input data for scoring must include columns compatible with the columns in the stored model.

+ Currently, `sp_rxPredict` supports only the following .NET column types: double, float, short, ushort, long, ulong and string. You may need to filter out unsupported types in your input data before using it for real-time scoring. 

  For information about corresponding SQL types, see [SQL-CLR Type Mapping](https://msdn.microsoft.com/library/bb386947.aspx) or [Mapping CLR Parameter Data](https://docs.microsoft.com/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data). 

See the [Technical Notes](#bkmk_Notes) section for a list of further restrictions and known issues.

### Real-Time Scoring in Microsoft R Server

For information regarding real-time scoring in a distributed environment based on Microsoft R Server, please refer to the [publishService](https://msdn.microsoft.com/microsoft-r/mrsdeploy/packagehelp/publishservice) function available in the [mrsDeploy package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy), which supports publishing models for real-time scoring as a new a web service running on R Server.

## <a name="bkmk_Notes"></a> Technical Notes

+ Real-time scoring does not use an R interpreter; therefore, any functionality that might require R interpreter is not supported during real-time scoring.  These might include:

  + Models using the `rxGlm` or `rxNaiveBayes` algorithms are not currently supported

  + RevoScaleR models that use an R transformation function, or a formula that contains a transformation, such as <code>A ~ log(B)</code> are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on the to input data before passing the data to real-time scoring.

+ In SQL Server 2017 CTP 2.0, real-time scoring is based on `sp_rxPredict`, an expended stored procedure provided as a wrapper for rxPredict. This stored procedure is written in C+ and is optimized specifically for scoring operations. With `sp_rxPredict`, you can load a specified model from a database, define a query with new input data, and generate scores based on the model.

    In the release version of SQL Server 2017, this stored procedure will no longer be available, and will be replaced by a native T-SQL function, **PREDICT**.

+ Real-time scoring is currently optimized for fast predictions on smaller data sets, ranging from a few rows to a hundreds of thousand of rows. On very large datasets, scoring in R using rxPredict might be faster.

+ `sp_rxPredict` returns an inaccurate message when a NULL value is passed as the model: "System.Data.SqlTypes.SqlNullValueException:Data in Null". You can ignore this message.
