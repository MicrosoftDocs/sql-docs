---
title: "Real-time scoring in SQL Server 2016 with Machine Learning Server | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
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

## What is Real-Time Scoring

In SQL Server 2016, Microsoft created an extensibility framework that allows R scripts to be executed from T-SQL. This framework supports any operation you might perform in R, ranging from simple functions to training complex machine learning models. However, the dual-process architecture that pairs R with SQL Server means that an external R processes must be invoked for every call, regardless of the complexity of the operation. In cases where you are only loading a pre-trained model from a table and scoring against it on data already in SQL Server, the overhead of calling the external R process represents an unnecessary performance cost.

In large deployments, scoring more typically involve getting scores from a model and inserting the scores into a table for many millions of rows, or retrieving a single score in real time. To support extremely fast execution of scoring in these scenarios, SQL Server Machine Learning Services (and Microsoft Machine Learning Server) now provide built-in prediction and scoring libraries. 


### Supported platforms
+ Microsoft R Server 
+ SQL Server R Services 2016: requires upgrade of the R Services instance to Microsoft R Server 9.1.0  
+ SQL Server Machine Learning Services (2017; includes Microsoft R Server 9.1.0) 

### Benefits

Because a model can be published and used for scoring without having to call the R interpreter, the overhead of multiple process interactions is reduced. This supports much faster prediction performance in enterprise production scenarios. 

Also, because the data never leaves SQL Server, results can be generated and inserted into a new table without any data translation between R and SQL.

## How it Works

To perform real-time scoring in SQL Server, you call sp_rxPredict, a new stored procedure provided specifically for real-time scoring. 

In Microsoft R Server (or Machine Learning Server Standalone), the equivalent functionality is provided by the rxPredict function.

This real-time scoring library has been designed in C+ and optimized specifically for scoring operations. With sp_rxPredict, you can load a specified model from a database, define a query with new input data, and generate scores based on the model. You can run the scoring library either from SQL Server, using the stored procedure, or from R code, by calling the rxPredict function.

Some restrictions apply:

+ On SQL Server, you must enable the real-time scoring feature in advance. This is because the feature entails installation of CLR-based libraries into SQL Server.
+ The model must be trained in advance and saved using a new serialization function provided in Microsoft R Server 9.1.0. The serialization function is optimized to support fast scoring.
+ The model must be *published* to SQL Server or another supported platform.
+ Only some model types can be used for scoring. Currently a selection of models from the MicrosoftML and revoScaleR pacakges are supported.
+ Models containing R transformations are not supported currently. 

See the [Technical Notes](#bkmk_Notes) section for a list of further restrictions and known issues.

## Perform Real-Time Scoring

1. Enable the serialization feature
1. Serialize the model using the format required for real-time scoring 
2. Publish the model to SQL Server as described here
3. Use T-SQL to call real-time scoring stored procedure 

### Step 1. Enable serialization on the database

By default, real-time scoring functionality is disabled in SQL Server. You must enable this feature for each SQL Server database that you want to use for scoring. 

> [!NOTE] 
> In order for real-time scoring to work, SQL CLR functionality needs to be enabled in the instance and the database needs to be marked trustworthy. Please carefully consider additional security implications of doing this. 

A server administrator must use the RegisterRExt.exe command-line utility to enable real-time scoring. This utility is included with the RevoScaleR package. 

**To enable real-time scoring**

1. Open an elevated command prompt, and locate RegisterRExt.exe. In a default installation, the path is 
`<SQLInstancePath>\R_SERVICES\library\RevoScaleR\x64\` 
2. Run the following command, substituting the name of your instance and the target database: 

  `RegisterRExt.exe /installRts [/instance:name] /database:databasename` 

  The instance name is optional if the database is on the default instance. 
  If you are using a named instance, you must specify the instance name. 
4. RegisterRExt.exe creates the following objects:
	+ Trusted assemblies 
	+ The stored procedure sp_rxPredict
	+ A new database role, `rxpredict_users`. The database administrator can use this role to grant permission to users who will run the real-time scoring functionality. 

**To disable real-time scoring functionality**

1. Open an elevated command prompt.
2. Run the following command: 

  `RegisterRExt.exe /uninstallrts /database:databasename [/instance:name]` 

### Step 2. Serialize the model

To provide the best performance, real-time scoring includes a new serialization method for storing models and loading them for scoring. The serialization format uses an optimized raw format (not human readable) that allows the model to be scored in an efficient manner. 

When you train a model, call the new serialization function to save the model in a SQL Server table using the new format that supports real-time scoring. 


* `rxSerializeModel()`  Serializes a supported model in raw format. 
  
  The model must be based on a supported model type from the RevoScaleR or MicrosoftML packages. Only models saved with this format can be loaded into SQL Server for real-time scoring. 

* `rxUnserializeModel()` Reconstitutes the original R model object from the serialized raw format. 


### Step 3. Publish model to SQL Server

Publishing refers to the process of saving a trained model, using the new serialized format, in a table in SQL Server. A model that has been published using this process can be read from a table for real-time scoring, without having to invoke the R runtime. 

**Requirements**

+ The database containing the table must have real-time scoring enabled.
+ Use a column of type VARBINARY to save the serialized models. 

When creating the model from your R code, there are two ways to save the model to a table:

+ Call the `rxWriteObject` function, from the RevoScaleR package, to write the model directly to the database. 

  The `rxWriteObject()` function can retrieve R objects from an ODBC data source like SQL Server, or write objects to SQL Server. The API is modeled after a simple key-value store.
  
  If you use this function, be sure to serialize the model using the new serialization function first. Then, set the *serialize* flag in `rxWriteObject` to FALSE, to avoid repeating the serialization step.  
+ Save the model in raw format to a file and then read from the file into SQL Server. 


### Step 4. Call sp_rxPredict

After you have enabled the real-time scoring feature, the SQL Server database will have a new stored procedure called **sp_rxPredict**. 

**Syntax**

```sql
sp_rxPredict @model = @model,  
@inputData = N'SELECT * FROM data' 
```
+ *@model* A trained model to be used for scoring. The model must have been trained using one of the supported algorithms, and saved using the new `rxSerializeModel()` function. 

+ *@inputData* A valid SQL query that defines the input data for scoring. The data must include columns compatible with the columns in the stored model. 

+ Currently, `sp_rxPredict` supports only the following .NET column types: double, float, short, ushort, long, ulong and string. You may need to filter out unsupported types in your input data before using it for real-time scoring. 

  For information about corresponding SQL types, see [SQL-CLR Type Mapping](https://msdn.microsoft.com/library/bb386947.aspx) or [Mapping CLR Parameter Data](https://docs.microsoft.com/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data). 

See the [Technical Notes](#bkmk_Notes) section for a list of further restrictions and known issues.

**Supported algorithms**

+ RevoScaleR models
  
  `rxLinMod`, `rxLogit`, `rxDTree`, `rxDForest` and `rxBTrees` 

+ Microsoft ML models
  
  `rxFastTrees`, `rxFastForest`, `rxLogisticRegression`, `rxOneClassSvm`, `rxNeuralNet`, `rxFastLinear`, `featurizeText`, `concat`, `categorical`, `categoricalHash`, `selectFeatures`

**Algorithms and models not supported**
  
+ Models containing R transformations
+ Models using the `rxGlm` or `rxNaiveBayes` algorithms in RevoScaleR 
+ PMML models 
+ Models created using other R libraries from CRAN or other repositories

### Real-Time Scoring in Microsoft R Server

For information regarding real-time scoring in a distributed environment based on Microsoft R Server, please refer to the [publishService](https://msdn.microsoft.com/microsoft-r/mrsdeploy/packagehelp/publishservice) function available in the [mrsDeploy package](https://msdn.microsoft.com/microsoft-r/mrsdeploy/mrsdeploy), which supports publishing models for real-time scoring as a new a web service running on R Server. 

## <a name="bkmk_Notes"></a> Technical Notes

Real-time scoring does not use an R interpreter; therefore, any functionality that might require R interpreter is not supported during real-time scoring.  These might include: 

+ Models using the `rxGlm` or `rxNaiveBayes` algorithms are not currently supported 

* RevoScaleR models that use an R transformation function, or a formula that contains a transformation, such as <code>A ~ log(B)</code> are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on the to input data before passing the data to real-time scoring. 

**Known Issues** 

+ `sp_rxPredict` is optimized for fast predictions on smaller data sets, ranging from a few rows to a few thousand rows. Performance might begin to degrading as data sets grow larger. 

+ `sp_rxPredict` returns an inaccurate message when NULL value is passed as model System.Data.SqlTypes.SqlNullValueException:Data in Null 


## See Also

