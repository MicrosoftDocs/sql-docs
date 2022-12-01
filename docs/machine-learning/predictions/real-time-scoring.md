---
title: Real-time scoring using sp_rxPredict
description: Learn how to perform real-time scoring with the sp_rxPredict system stored procedure in SQL Server for high-performance predictions or scores in forecasting workloads.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 04/05/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Real-time scoring with sp_rxPredict in SQL Server
[!INCLUDE[sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Learn how to perform real-time scoring with the [sp_rxPredict](../../relational-databases/system-stored-procedures/sp-rxpredict-transact-sql.md) system stored procedure in SQL Server for high-performance predictions or scores in forecasting workloads.

Real-time scoring with `sp_rxPredict` is language-agnostic and executes with no dependencies on the R or Python runtimes in [Machine Learning Services](../sql-server-machine-learning-services.md). Using a model created and trained using Microsoft functions and serialized to a binary format in SQL Server, you can use real-time scoring to generate predicted outcomes on new data inputs on SQL Server instances that do not have the R or Python add-on installed.

## How real-time scoring works

Real-time scoring is supported on specific model types based on functions in [RevoScaleR](../r/ref-r-revoscaler.md) or [MicrosoftML](../r/ref-r-microsoftml.md) in R, or [revoscalepy](../python/ref-py-revoscalepy.md) or [microsoftml](../python/ref-py-microsoftml.md) in Python. It uses native C++ libraries to generate scores based on user input provided to a machine learning model stored in a special binary format.

Because a trained model can be used for scoring without having to call an external language runtime in [Machine Learning Services](../sql-server-machine-learning-services.md), the overhead of multiple processes is reduced.

Real-time scoring is a multi-step process:

1. You enable the stored procedure that does scoring on a per-database basis.
2. You load the pre-trained model in binary format.
3. You provide new input data to be scored, either tabular or single rows, as input to the model.
4. To generate scores, call the [sp_rxPredict](../../relational-databases/system-stored-procedures/sp-rxpredict-transact-sql.md) stored procedure.

## Prerequisites

+ [Enable SQL Server CLR integration](../../relational-databases/clr-integration/clr-integration-enabling.md).

+ [Enable real-time scoring](#bkmk_enableRtScoring).

+ The model must be trained in advance using one of the supported **rx** algorithms. For details, see [Supported algorithms](../../relational-databases/system-stored-procedures/sp-rxpredict-transact-sql.md#supported-algorithms) for `sp_rxPredict`.

+ Serialize the model using [rxSerialize](/machine-learning-server/r-reference/revoscaler/rxserializemodel) for R or [rx_serialize_model](/machine-learning-server/python-reference/revoscalepy/rx-serialize-model) for Python. These serialization functions have been optimized to support fast scoring.

+ Save the model to the database engine instance from which you want to call it. This instance is not required to have the R or Python runtime extension.

> [!Note]
> Real-time scoring is currently optimized for fast predictions on smaller data sets, ranging from a few rows to hundreds of thousands of rows. On big datasets, using [rxPredict](/machine-learning-server/r-reference/revoscaler/rxpredict) might be faster.

<a name ="bkmk_enableRtScoring"></a>

## Enable real-time scoring

Enable this feature for each database that you want to use for scoring. The server administrator should run the command-line utility, RegisterRExt.exe, which is included with the RevoScaleR package.

> [!CAUTION]
> In order for real-time scoring to work, SQL CLR functionality needs to be enabled in the instance and the database needs to be marked trustworthy. When you run the script, these actions are performed for you. However, consider carefully the additional security implications before doing this.

1. Open an elevated command prompt, and navigate to the folder where RegisterRExt.exe is located. The following path can be used in a default installation:

    `<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\`

2. Run the following command, substituting the name of your instance and the target database where you want to enable the extended stored procedures:

    `RegisterRExt.exe /installRts [/instance:name] /database:databasename`

    For example, to add the extended stored procedure to the CLRPredict database on the default instance, type:

    `RegisterRExt.exe /installRts /database:CLRPRedict`

    The instance name is optional if the database is on the default instance. If you're using a named instance, specify the instance name.

3. RegisterRExt.exe creates the following objects:

    + Trusted assemblies
    + The stored procedure `sp_rxPredict`
    + A new database role, `rxpredict_users`. The database administrator can use this role to grant permission to users who use the real-time scoring functionality.

4. Add any users who need to run `sp_rxPredict` to the new role.

> [!NOTE]
>
> In SQL Server 2017 and later, additional security measures are in place to prevent problems with CLR integration. These measures impose additional restrictions on the use of this stored procedure as well.

## Disable real-time scoring

To disable real-time scoring functionality, open an elevated command prompt, and run the following command: `RegisterRExt.exe /uninstallrts /database:<database_name> [/instance:name]`

## Example

This example describes the steps required to prepare and save a model for **real-time** prediction, and provides an example in R of how to call the function from T-SQL.

### Step 1. Prepare and save the model

The binary format required by sp\_rxPredict is the same as the format required to use the PREDICT function. Therefore, in your R code, include a call to [rxSerializeModel](/machine-learning-server/r-reference/revoscaler/rxserializemodel), and be sure to specify `realtimeScoringOnly = TRUE`, as in this example:

```R
model <- rxSerializeModel(model.name, realtimeScoringOnly = TRUE)
```

### Step 2. Call sp_rxPredict

You call `sp_rxPredict` as you would any other stored procedure. In the current release, the stored procedure takes only two parameters: _\@model_ for the model in binary format, and _\@inputData_ for the data to use in scoring, defined as a valid SQL query.

Because the binary format is the same as that used by the PREDICT function, you can use the models and data table from the preceding example.

```sql
DECLARE @irismodel varbinary(max)
SELECT @irismodel = [native_model_object] from [ml_models]
WHERE model_name = 'iris.dtree' 
AND model_version = 'v1'

EXEC sp_rxPredict
@model = @irismodel,
@inputData = N'SELECT * FROM iris_rx_data'
```

> [!NOTE]
> The call to `sp_rxPredict` fails if the input data for scoring does not include columns that match the requirements of the model. Currently, only the following .NET data types are supported: double, float, short, ushort, long, ulong and string.
>
> Therefore, you might need to filter out unsupported types in your input data before using it for real-time scoring.
>
> For information about corresponding SQL types, see [SQL-CLR Type Mapping](/dotnet/framework/data/adonet/sql/linq/sql-clr-type-mapping) or [Mapping CLR Parameter Data](../../relational-databases/clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).

## Next steps

+ [Native scoring using the PREDICT T-SQL function with SQL machine learning](native-scoring-predict-transact-sql.md)
+ [sp_rxPredict](../../relational-databases/system-stored-procedures/sp-rxpredict-transact-sql.md)
+ [SQL machine learning](../index.yml)
