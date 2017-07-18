---
title: "PREDICT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/17/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "PREDICT"
  - "PREDICT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "PREDICT clause"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# PREDICT (Transact-SQL)  
[!INCLUDE[tsql-appliesto-ssvnxt-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssvnxt-xxxx-xxxx-xxx.md)]  

Generates a predicted value or scores based on a stored model.  

## Syntax

```
PREDICT  
(  
  MODEL = @model | model_literal,  
  DATA = object AS <table_alias>  
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

**model**

The `MODEL` parameter is used to specify the model used for scoring or prediction. The model is specified as a variable or a literal or a scalar expression.

The model object can be created by using R or Python or another tool.

**data**

The DATA parameter is used to specify the data used for scoring or prediction. Data is specified in the form of a table source in the query. Table source can be a table, table alias, CTE alias, view, or table-valued function.

**parameters**

The PARAMETERS parameter is used to specify optional user-defined parameters used for scoring or prediction.

The name of each parameter is specific to the model type. For example, the rxPredict function in RevoScaleR supports the parameter _@computeResiduals bit_ to support computation of residuals when scoring a logistic regression model. YOu could pass that parameter name and it value to the `PREDICT` function.

> [NOTE]
> This option is not supported in the pre-release of SQL Server 2017 and is included for forward-compatibility purposes only.

**WITH ( \<result_set_definition> )**

The WITH clause is used to specify the schema of the output returned by the `PREDICT` function.

In addition to the columns returned by the `PREDICT` function itself, all the columns that are part of the data input are available for use in the query.

### Return values

No predefined schema is available; SQL Server does not validate the contents of the model and does not validate the returned column values.  
- The `PREDICT` function passes through columns as input  
- The `PREDICT` function also generates new columns, but the number of columns and their data types depends on the type of model that was used for prediction.  

Any error messages related to the data, the model, or the column format are returned by the underlying prediction function associated with the model.  
- For RevoScaleR, the equivalent function is [rxPredict](https://docs.microsoft.com/r-server/r-reference/revoscaler/rxpredict)  
- For MicrosoftML, the equivalent function is [rxPredict.mlModel](https://docs.microsoft.com/r-server/r-reference/microsoftml/rxpredict)  

It is not possible to view the internal model structure using `PREDICT`. If you want to understand the contents of the model itself, you must load the model object, deserialize it, and use appropriate R code to parse the model.

## Remarks

The `PREDICT` function is supported in all editions of SQL Server, including Linux.

It is not necessary that R, Python, or another machine learning language be installed on the server to use the `PREDICT` function. You can train the model in another environment and save it to a SQL Server table for use with `PREDICT`, or call the model from another instance of SQL Server that has the saved model.

### Supported algorithms

The model that you use must have been created using one of the supported algorithms from the RevoScaleR package. For a list of currently supported models, see [Real-time scoring](../advanced-analytics/real-time-scoring.md).

### Permissions

No permissions are required for `PREDICT`; however, the user needs `EXECUTE` permission on the database, and permission to query any data that is used as inputs. The user must also be able to read the model from a table, if the model has been stored in a table.

## Examples

The following examples demonstrate the sytax for calling `PREDICT`.

### Call a stored model and use it for prediction

This example calls an existing logistic regression model stored in table [models_table]. It gets the latest trained model, using a SELECT statement, and then passes the binary model to the PREDICT function. The input values represent features; the output represents the classification assigned by the model.

```sql
DECLARE @logit_model varbinary(max) = "SELECT TOP 1 @model from [models_table]";
DECLARE @input_qry = "SELECT ID, [Gender], [Income] from NewCustomers";

SELECT PREDICT [class]
FROM PREDICT( MODEL = @logit_model,  DATA = @input_qry
WITH (class string);
```

### Using PREDICT in a FROM clause

This example references the `PREDICT` function in the `FROM` clause of a `SELECT` statement:

```sql
SELECT d.*, p.Score
FROM PREDICT(MODEL = @logit_model, 
  DATA = dbo.mytable AS d) WITH (Score float) AS p;
```

The alias **d** specified for table source in the _DATA_ parameter is used to reference the columns belonging to dbo.mytable. The alias **p** specified for the **PREDICT** function is used to reference the columns returned by the PREDICT function.

### Combining PREDICT with an INSERT statement

One of the common use cases for prediction is to generate a score for input data, and then insert the predicted values into a table. The following example assumes that the calling application uses a stored procedure to insert a row containing the predicted value into a table:

```sql
CREATE PROCEDURE InsertLoanApplication
(@p1 varchar(100), @p2 varchar(200), @p3 money, @p4 int)
AS
BEGIN
  DECLARE @model varbinary(max) = (select model
  FROM scoring_model
  WHERE model_name = 'ScoringModelV1');
  WITH d as ( SELECT * FROM (values(@p1, @p2, @p3, @p4)) as t(c1, c2, c3, c4) )

  INSERT INTO loan_applications (c1, c2, c3, c4, score)
  SELECT d.c1, d.c2, d.c3, d.c4, p.score
  FROM PREDICT(MODEL = @model, DATA = d) WITH(score float) as p;
END;
```

If the procedure takes multiple rows via a table-valued parameter, then it can be written as follows:

```sql
CREATE PROCEDURE InsertLoanApplications (@new_applications dbo.loan_application_type)
AS
BEGIN
  DECLARE @model varbinary(max) = (SELECT model_bin FROM scoring_models WHERE model_name = 'ScoringModelV1');
  INSERT INTO loan_applications (c1, c2, c3, c4, score)
  SELECT d.c1, d.c2, d.c3, d.c4, p.score
  FROM PREDICT(MODEL = @model, DATA = @new_applications as d)
  WITH (score float) as p;
END;
```

### Creating an R model and generating scores using optional model parameters

> [!NOTE]
> Use of the parameters argument is not supported in Release Candidate 1.

This example assumes that you have created a logistic regression model fitted with a covariance matrix, using a call to RevoScaleR such as this:

```R
logitObj <- rxLogit(Kyphosis ~ Age + Start + Number, data = kyphosis, covCoef = TRUE)
```

If you store the model in SQL Server in binary format, you can use the PREDICT function to generate not just predictions, but additional information supported by the model type, such as error or confidence intervals.

The following code shows the equivalent call from R to rxPredict:

```R
rxPredict(logitObj, data = new_kyphosis_data, computeStdErr = TRUE, interval = "confidence")
```

The equivalent call using the `PREDICT` function also provides the score (predicted value), error, and confidence intervals:

```sql
SELECT d.Age, d.Start, d.Number, p.pred AS Kyphosis_Pred, p.stdErr, p.pred_lower, p.pred_higher
FROM PREDICT( MODEL = @logitObj,  DATA = new_kyphosis_data AS d,
  PARAMETERS = N'computeStdErr bit, interval varchar(30)',
  computeStdErr = 1, interval = 'confidence')
WITH (pred float, stdErr float, pred_lower float, pred_higher float) AS p;
```


