---
title: Real-time scoring using sp_rxPredict stored procedure - SQL Server Machine Learning Services
description: Generate predictions using sp_rxPredict, scoring data inputs against a pre-trained model written in R on SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Real-time scoring with sp_rxPredict in SQL Server machine learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Real-time scoring uses the [sp_rxPredict](https://docs.microsoft.com//sql/relational-databases/system-stored-procedures/sp-rxpredict-transact-sql) system stored procedure and the CLR extension capabilities in SQL Server for high-performance predictions or scores in forecasting workloads. Real-time scoring is language-agnostic and executes with no dependencies on R or Python run times. Assuming a model created and trained using Microsoft functions, and then serialized to a binary format in SQL Server, you can use real-time scoring to generate predicted outcomes on new data inputs on SQL Server instances that do not have the R or Python add-on installed.

## How real-time scoring works

Real-time scoring is supported in both SQL Server 2017 and SQL Server 2016, on specific model types based on RevoScaleR or MicrosoftML functions such as  [rxLinMod (RevoScaleR)](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlinmod)[rxNeuralNet (MicrosoftML)](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet). It uses native C++ libraries to generate scores, based on user input provided to a machine learning model stored in a special binary format.

Because a trained model can be used for scoring without having to call an external language runtime, the overhead of multiple processes is reduced. This supports much faster prediction performance for production scoring scenarios. Because the data never leaves SQL Server, results can be generated and inserted into a new table without any data translation between R and SQL.

Real-time scoring is a multi-step process:

1. The stored procedure that does scoring must be enabled on a per-database basis.
2. You load the pre-trained model in binary format.
3. You provide new input data to be scored, either tabular or single rows, as input to the model.
4. To generate scores, call the [sp_rxPredict](https://docs.microsoft.com//sql/relational-databases/system-stored-procedures/sp-rxpredict-transact-sql) stored procedure.

> [!TIP]
> For an example of real-time scoring in action, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/)

## Prerequisites

+ [Enable SQL Server CLR integration](https://docs.microsoft.com/dotnet/framework/data/adonet/sql/introduction-to-sql-server-clr-integration).

+ [Enable real-time scoring](#bkmk_enableRtScoring).

+ The model must be trained in advance using one of the supported **rx** algorithms. For R, real-time scoring with `sp_rxPredict` works with [RevoScaleR and MicrosoftML supported algorithms](#bkmk_rt_supported_algos). For Python, see [revoscalepy and microsoftml supported algorithms](#bkmk_py_supported_algos)

+ Serialize the model using [rxSerialize](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R, and [rx_serialize_model](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

+ Save the model to the database engine instance from which you want to call it. This instance is not required to have the R or Python runtime extension.

> [!Note]
> Real-time scoring is currently optimized for fast predictions on smaller data sets, ranging from a few rows to hundreds of thousands of rows. On big datasets, using [rxPredict](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxpredict) might be faster.

<a name="bkmk_py_supported_algos"></a>

## Supported algorithms

### Python algorithms using real-time scoring

+ revoscalepy models

  + [rx_lin_mod](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-lin-mod) \*
  + [rx_logit](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-logit) \*
  + [rx_btrees](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-btrees) \*
  + [rx_dtree](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dtree) \*
  + [rx_dforest](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-dforest) \*
  
  Models marked with \* also support native scoring with the PREDICT function.

+ microsoftml models

  + [rx_fast_trees](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-trees)
  + [rx_fast_forest](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-forest)
  + [rx_logistic_regression](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-logistic-regression)
  + [rx_oneclass_svm](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-oneclass-svm)
  + [rx_neural_net](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-neural-network)
  + [rx_fast_linear](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/rx-fast-linear)

+ Transformations supplied by microsoftml

  + [featurize_text](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/featurize-text)
  + [concat](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/categorical)
  + [categorical_hash](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/categorical-hash)


<a name="bkmk_rt_supported_algos"></a>

### R algorithms using real-time scoring

+ RevoScaleR models

  + [rxLinMod](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlinmod) \*
  + [rxLogit](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxlogit) \*
  + [rxBTrees](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxbtrees) \*
  + [rxDtree](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdtree) \*
  + [rxdForest](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxdforest) \*
  
  Models marked with \* also support native scoring with the PREDICT function.

+ MicrosoftML models

  + [rxFastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [rxFastForest](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastforest)
  + [rxLogisticRegression](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxlogisticregression)
  + [rxOneClassSvm](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxoneclasssvm)
  + [rxNeuralNet](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet)
  + [rxFastLinear](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastlinear)

+ Transformations supplied by MicrosoftML

  + [featurizeText](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees)
  + [concat](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/concat)
  + [categorical](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categorical)
  + [categoricalHash](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categoricalHash)
  + [selectFeatures](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/selectFeatures)

### Unsupported model types

Real-time scoring does not use an interpreter; therefore, any functionality that might require an interpreter is not supported during the scoring step.  These might include:

  + Models using the `rxGlm` or `rxNaiveBayes` algorithms are not supported.

  + Models using a transformation function or formula containing a transformation, such as <code>A ~ log(B)</code> are not supported in real-time scoring. To use a model of this type, we recommend that you perform the transformation on input data before passing the data to real-time scoring.


## Example: sp_rxPredict

This section describes the steps required to set up **real-time** prediction, and provides an example in R of how to call the function from T-SQL.

<a name ="bkmk_enableRtScoring"></a> 

### Step 1. Enable the real-time scoring procedure

You must enable this feature for each database that you want to use for scoring. The server administrator should run the command-line utility, RegisterRExt.exe, which is included with the RevoScaleR package.

> [!NOTE]
> In order for real-time scoring to work, SQL CLR functionality needs to be enabled in the instance; additionally, the database needs to be marked trustworthy. When you run the script, these actions are performed for you. However, consider the additional security implications before doing this!

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
	+ A new database role, `rxpredict_users`. The database administrator can use this role to grant permission to users who use the real-time scoring functionality.

4. Add any users who need to run `sp_rxPredict` to the new role.

> [!NOTE]
> 
> In SQL Server 2017, additional security measures are in place to prevent problems with CLR integration. These measures impose additional restrictions on the use of this stored procedure as well. 

### Step 2. Prepare and save the model

The binary format required by sp\_rxPredict is the same as the format required to use the PREDICT function. Therefore, in your R code, include a call to [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel), and be sure to specify `realtimeScoringOnly = TRUE`, as in this example:

```R
model <- rxSerializeModel(model.name, realtimeScoringOnly = TRUE)
```

### Step 3. Call sp_rxPredict

You call sp\_rxPredict as you would any other stored procedure. In the current release, the stored procedure takes only two parameters: _\@model_ for the model in binary format, and _\@inputData_ for the data to use in scoring, defined as a valid SQL query.

Because the binary format is the same that is used by the PREDICT function, you can use the models and data table from the preceding example.

```sql
DECLARE @irismodel varbinary(max)
SELECT @irismodel = [native_model_object] from [ml_models]
WHERE model_name = 'iris.dtree' 
AND model_version = 'v1''

EXEC sp_rxPredict
@model = @irismodel,
@inputData = N'SELECT * FROM iris_rx_data'
```

> [!NOTE]
> 
> The call to sp\_rxPredict fails if the input data for scoring does not include columns that match the requirements of the model. Currently, only the following .NET data types are supported: double, float, short, ushort, long, ulong and string.
> 
> Therefore, you might need to filter out unsupported types in your input data before using it for real-time scoring.
> 
> For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR Parameter Data](https://docs.microsoft.com/sql/relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data).

## Disable real-time scoring

To disable real-time scoring functionality, open an elevated command prompt, and run the following command: `RegisterRExt.exe /uninstallrts /database:<database_name> [/instance:name]`

## Next steps

For an example of how rxPredict can be used for scoring, see [End to End Loan ChargeOff Prediction Built Using Azure HDInsight Spark Clusters and SQL Server 2016 R Service](https://blogs.msdn.microsoft.com/rserver/2017/06/29/end-to-end-loan-chargeoff-prediction-built-using-azure-hdinsight-spark-clusters-and-sql-server-2016-r-service/).

For more background on scoring in SQL Server, see [How to generate predictions in SQL Server machine learning](r/how-to-do-realtime-scoring.md).
