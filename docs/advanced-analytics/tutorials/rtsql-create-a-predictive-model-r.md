---
title: "Create a Predictive Model (R in T-SQL Tutorial) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
  - "SQL"
ms.assetid: 6eb78a80-5791-438f-9ca6-d142ab5d9bb1
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Create an R Model using SQL

In this step, you'll learn how to train a model using R, and then save the model to a table in SQL Server. The model is a simple regression model that predicts the stopping distance of a car based on speed. You'll use the `cars` dataset already included with R, because it is small and easy to understand.

## Create the source data

First, create a table to save the training data.

```sql
CREATE TABLE CarSpeed ([speed] int not null, [distance] int not null)
INSERT INTO CarSpeed
EXEC sp_execute_external_script
        @language = N'R'
        , @script = N'car_speed <- cars;'
        , @input_data_1 = N''
        , @output_data_1_name = N'car_speed'
```

+ Some people like to use temporary tables, but be aware that some R clients disconnect sessions between batches.

+ Many datasets, small and large, are included with the R runtime. To get a list of datasets installed with R,  type `library(help="datasets")` from an R command prompt.

## Create a regression model

The car speed data contains two columns, both numeric, `dist` and`speed`. There are multiple observations of some speeds. From this data, you will create a linear regression model that describes some relationship between car speed and the distance required to stop a car.

The requirements of a linear model are simple:

+ Define a formula that describes the relationship between the dependent variable `speed` and the independent variable `distance`

+ Provide input data to use in training the model

> [!TIP]
> If you need a refresher on linear models, we recommend this tutorial, which describes the process of fitting a model using rxLinMod: [Fitting Linear Models](https://docs.microsoft.com/r-server/r/how-to-revoscaler-linear-model)

To actually build the model, you define the formula inside your R code, and pass the data as an input parameter.

```sql
DROP PROCEDURE IF EXISTS generate_linear_model;
GO
CREATE PROCEDURE generate_linear_model
AS
BEGIN
    EXEC sp_execute_external_script
    @language = N'R'
    , @script = N'lrmodel <- rxLinMod(formula = distance ~ speed, data = CarsData);
        trained_model <- data.frame(payload = as.raw(serialize(lrmodel, connection=NULL)));'
    , @input_data_1 = N'SELECT [speed], [distance] FROM CarSpeed'
    , @input_data_1_name = N'CarsData'
    , @output_data_1_name = N'trained_model'
    WITH RESULT SETS ((model varbinary(max)));
END;
GO
```

+ The first argument to rxLinMod is the *formula* parameter, which defines distance as dependent on speed.
+ The input data is stored in the variable `CarsData`, which is populated by the SQL query. If you don't assign a specific name to your input data, the default variable name would be _InputDataSet_.

## Create a table for storing the model

Next, store the model so you can retrain or use it for prediction. The output of an R package that creates a model is usually a **binary object**. Therefore, the table where you store the model must provide a column of **varbinary** type.

```sql
CREATE TABLE stopping_distance_models (
	model_name varchar(30) not null default('default model') primary key,
	model varbinary(max) not null);
```

## Save the model

To save the model, run the following Transact-SQL statement to call the stored procedure, generate the model, and save it to a table.

```sql
INSERT INTO stopping_distance_models (model)
EXEC generate_linear_model;
```

Note that if you run this code a second time, you get this error:

```
Violation of PRIMARY KEY constraint...Cannot insert duplicate key in object dbo.stopping_distance_models
```

One option for avoiding this error is to update the name for each new model. For example, you could change the name to something more descriptive, and include the model type, the day you created it, and so forth.

```sql
UPDATE stopping_distance_models
SET model_name = 'rxLinMod ' + format(getdate(), 'yyyy.MM.HH.mm', 'en-gb')
WHERE model_name = 'default model'
```

## Output additional variables

Generally, the output of R from the stored procedure [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) is limited to a single data frame. (This limitation might be removed in future.)

However, you can return outputs of other types, such as scalars, in addition to the data frame.

For example, suppose you want to train a model but immediately view a table of coefficients from the model. You could create the table of coefficients as the main result set, and output the trained model in a SQL variable. You could immediately re-use the model by callings variable, or you could save it to a table as shown here.

```sql
DECLARE @model varbinary(max), @modelname varchar(30)
EXEC sp_execute_external_script
    @language = N'R'
    , @script = N'
        speedmodel <- rxLinMod(distance ~ speed, CarsData)
        modelbin <- serialize(speedmodel, NULL)
        OutputDataSet <- data.frame(coefficients(speedmodel));'
    , @input_data_1 = N'SELECT [speed], [distance] FROM CarSpeed'
    , @input_data_1_name = N'CarsData'
    , @params = N'@modelbin varbinary(max) OUTPUT'
    , @modelbin = @model OUTPUT
    WITH RESULT SETS (([Coefficient] float not null));

-- Save the generated model
INSERT INTO [dbo].[stopping_distance_models] (model_name, model)
VALUES (' latest model', @model)
```

**Results**

![rslq_basictut_coefficients](media/rslq-basictut-coefficients.PNG)

### Summary

Remember these rules for working with SQL parameters and R variables in `sp_execute_external_script`:

+ All SQL parameters mapped to R script must be listed by name in the _@params_ argument.
+ To output one of these parameters, add the OUTPUT keyword in the _@params_ list.
+ After listing the mapped parameters, provide the mapping, line by line, of SQL parameters to R variables, immediately after the _@params_ list.

## Next lesson

Now that you have a model, in the final step, you'll learn how to generate predictions from it and plot the results.

[Predict and Plot from Model](/rtsql-predict-and-plot-from-model.md)


