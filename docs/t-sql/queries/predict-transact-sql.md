---
title: "PREDICT (Transact-SQL)"
ms.custom: ""
ms.date: "06/16/2020"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: machine-learning
ms.topic: "language-reference"
f1_keywords: 
  - "PREDICT"
  - "PREDICT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PREDICT clause"
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=azuresqldb-mi-current||=azure-sqldw-latest||=sqlallproducts-allversions"
---
# PREDICT (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ss2017-asdb-asdw-xxx-md](../../includes/tsql-appliesto-ss2017-asdb-asdw-xxx-md.md)]

Generates a predicted value or scores based on a stored model.

::: moniker range=">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=azuresqldb-mi-current||=sqlallproducts-allversions"
For more information, see [Native scoring using the PREDICT T-SQL function](../../machine-learning/sql-native-scoring.md).
::: moniker-end

## Syntax

```syntaxsql
PREDICT  
(  
  MODEL = @model | model_literal,  
  DATA = object AS <table_alias>
  [, RUNTIME = ONNX ]
)  
WITH ( <result_set_definition> )  

<result_set_definition> ::=  
  {  
    { column_name  
      data_type  
      [ COLLATE collation_name ]  
      [ NULL | NOT NULL ]  
    }  
      [,...n ]  
  }  

MODEL = @model | model_literal  
```

### Arguments

**MODEL**

The `MODEL` parameter is used to specify the model used for scoring or prediction. The model is specified as a variable or a literal or a scalar expression.

::: moniker range=">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=sqlallproducts-allversions"
The model object can be created by using R or Python or another tool.
::: moniker-end

::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
The model object can be created by using R or Python or another tool. [Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) is also a supported model format for Azure SQL Managed Instance.
::: moniker-end

::: moniker range=">=azure-sqldw-latest||=sqlallproducts-allversions"
[Open Neural Network Exchange (ONNX)](https://onnx.ai/get-started.html) is the supported model format for Azure Synapse.
::: moniker-end

**DATA**

The DATA parameter is used to specify the data used for scoring or prediction. Data is specified in the form of a table source in the query. Table source can be a table, table alias, CTE alias, view, or table-valued function.

**RUNTIME = ONNX**

> [!IMPORTANT]
> The `RUNTIME = ONNX` argument is only available in [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/machine-learning-services-overview) and [Azure SQL Edge](/azure/sql-database-edge/onnx-overview).

Indicates the machine learning engine used for model execution. The `RUNTIME` parameter value is always `ONNX`. The parameter is required for Azure SQL Edge. On Azure SQL Managed Instance, the parameter is optional and only used when using ONNX models.

**WITH ( <result_set_definition> )**

The WITH clause is used to specify the schema of the output returned by the `PREDICT` function.

In addition to the columns returned by the `PREDICT` function itself, all the columns that are part of the data input are available for use in the query.

### Return values

No predefined schema is available; the contents of the model is not validated and the returned column values are not validated either.

- The `PREDICT` function passes through columns as input.
- The `PREDICT` function also generates new columns, but the number of columns and their data types depends on the type of model that was used for prediction.

Any error messages related to the data, the model, or the column format are returned by the underlying prediction function associated with the model.

::: moniker range=">=sql-server-2017||>=sql-server-linux-2017||=sqlallproducts-allversions"
## Remarks

The `PREDICT` function is supported in all editions of SQL Server 2017 or later, on Windows and Linux. [Machine Learning Services](../../machine-learning/sql-server-machine-learning-services.md) does not need to be enabled to use `PREDICT`.
::: moniker-end

### Supported algorithms

::: moniker range=">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=sqlallproducts-allversions"
The model that you use must have been created using one of the supported algorithms from the RevoScaleR package. For a list of currently supported models, see [Real-time scoring](../../machine-learning/real-time-scoring.md).
::: moniker-end
::: moniker range="=azure-sqldw-latest||=sqlallproducts-allversions"
Algorithms that can be converted to [ONNX](https://onnx.ai/) model format are supported.
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
Algorithms that can be converted to [ONNX](https://onnx.ai/) model format and models that you have been created using one of the supported algorithms from the RevoScaleR package are supported.
::: moniker-end

### Permissions

No permissions are required for `PREDICT`; however, the user needs `EXECUTE` permission on the database, and permission to query any data that is used as inputs. The user must also be able to read the model from a table, if the model has been stored in a table.

## Examples

The following examples demonstrate the syntax for calling `PREDICT`.

### Using PREDICT in a FROM clause

This example references the `PREDICT` function in the `FROM` clause of a `SELECT` statement:

```sql
SELECT d.*, p.Score
FROM PREDICT(MODEL = @model,
    DATA = dbo.mytable AS d) WITH (Score float) AS p;
```

The alias **d** specified for table source in the `DATA` parameter is used to reference the columns belonging to `dbo.mytable`. The alias **p** specified for the `PREDICT` function is used to reference the columns returned by the `PREDICT` function.

- The model is stored as `varbinary(max)` column in table call **Models**. Additional information such as **ID** and **description** is saved in the table to identify the mode.
- The alias **d** specified for table source in the `DATA` parameter is used to reference the columns belonging to `dbo.mytable`. The input data column names should match the name of inputs for the model.
- The alias **p** specified for the `PREDICT` function is used to reference the predicted column returned by the `PREDICT` function. The column name should have the same name as the output name for the model.
- All input data columns and the predicted columns are available to display in the SELECT statement.

### Combining PREDICT with an INSERT statement

A common use case for prediction is to generate a score for input data, and then insert the predicted values into a table. The following example assumes the calling application uses a stored procedure to insert a row containing the predicted value into a table:

```sql
DECLARE @model varbinary(max) = (SELECT model FROM scoring_model WHERE model_name = 'ScoringModelV1');

INSERT INTO loan_applications (c1, c2, c3, c4, score)
SELECT d.c1, d.c2, d.c3, d.c4, p.score
FROM PREDICT(MODEL = @model, DATA = dbo.mytable AS d) WITH(score float) AS p;
```

- The results of `PREDICT` are stored in a table called PredictionResults. 
- The model is stored as `varbinary(max)` column in table call **Models**. Additional information such as ID and description can be saved in the table to identify the model.
- The alias **d** specified for table source in the `DATA` parameter is used to reference the columns in `dbo.mytable`.The input data column names should match the name of inputs for the model.
- The alias **p** specified for the `PREDICT` function is used to reference the predicted column returned by the `PREDICT` function. The column name should have the same name as the output name for the model.
- All input columns and the predicted column are available to display in the SELECT statement.

## Next steps

::: moniker range=">=sql-server-2017||=azuresqldb-current||>=sql-server-linux-2017||=azuresqldb-mi-current||=sqlallproducts-allversions"
- [Native scoring using the PREDICT T-SQL function](../../machine-learning/sql-native-scoring.md)
::: moniker-end
::: moniker range="=azure-sqldw-latest||=azuresqldb-mi-current||=sqlallproducts-allversions"
-	[Learn more about ONNX models](/azure/machine-learning/concept-onnx#get-onnx-models)
::: moniker-end
