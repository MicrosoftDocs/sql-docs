---
title: Quickstart to create a predictive model using R - SQL Server Machine Learning
description: In this quickstart, learn how to build a model in R using SQL Server data to plot predictions.
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/04/2019
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: Create a predictive model using R in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this quickstart, you'll learn how to train a model using R, and then save the model to a table in SQL Server. The model is a simple generalized linear model (GLM) that predicts probability that a vehicle has been fitted with a manual transmission. You'll use the `mtcars` dataset included with R.

## Prerequisites

A previous quickstart, [Verify R exists in SQL Server](quickstart-r-verify.md), provides information and links for setting up the R environment required for this quickstart.

## Create the source data

First, create a table to save the training data.

```sql
CREATE TABLE dbo.MTCars(
    mpg decimal(10, 1) NOT NULL,
    cyl int NOT NULL,
    disp decimal(10, 1) NOT NULL,
    hp int NOT NULL,
    drat decimal(10, 2) NOT NULL,
    wt decimal(10, 3) NOT NULL,
    qsec decimal(10, 2) NOT NULL,
    vs int NOT NULL,
    am int NOT NULL,
    gear int NOT NULL,
    carb int NOT NULL
);
```

Next, insert the data from the build in dataset `mtcars`.

```SQL
INSERT INTO dbo.MTCars
EXEC sp_execute_external_script
        @language = N'R'
        , @script = N'MTCars <- mtcars;'
        , @input_data_1 = N''
        , @output_data_1_name = N'MTCars';
```

+ Some people like to use temporary tables, but be aware that some R clients disconnect sessions between batches.

+ Many datasets, small and large, are included with the R runtime. To get a list of datasets installed with R,  type `library(help="datasets")` from an R command prompt.

## Create a model

The car speed data contains two columns, both numeric, horsepower (`hp`) and weight (`wt`). From this data, you will create a generalized linear model (GLM) that estimates the probability that a vehicle has been fitted with a manual transmission.

To build the model, you define the formula inside your R code, and pass the data as an input parameter.

```sql
DROP PROCEDURE IF EXISTS generate_GLM;
GO
CREATE PROCEDURE generate_GLM
AS
BEGIN
    EXEC sp_execute_external_script
    @language = N'R'
    , @script = N'carsModel <- glm(formula = am ~ hp + wt, data = MTCarsData, family = binomial);
        trained_model <- data.frame(payload = as.raw(serialize(carsModel, connection=NULL)));'
    , @input_data_1 = N'SELECT hp, wt, am FROM MTCars'
    , @input_data_1_name = N'MTCarsData'
    , @output_data_1_name = N'trained_model'
    WITH RESULT SETS ((model VARBINARY(max)));
END;
GO
```

+ The first argument to `glm` is the *formula* parameter, which defines `am` as dependent on `hp + wt`.
+ The input data is stored in the variable `MTCarsData`, which is populated by the SQL query. If you don't assign a specific name to your input data, the default variable name would be _InputDataSet_.

## Create a table for the model

Next, store the model so you can retrain or use it for prediction. The output of an R package that creates a model is usually a **binary object**. Therefore, the table where you store the model must provide a column of **varbinary(max)** type.

```sql
CREATE TABLE GLM_models (
    model_name varchar(30) not null default('default model') primary key,
    model varbinary(max) not null
);
```

## Save the model

To save the model, run the following Transact-SQL statement to call the stored procedure, generate the model, and save it to a table.

```sql
INSERT INTO GLM_models(model)
EXEC generate_GLM;
```

Note that if you run this code a second time, you get this error:

```sql
Violation of PRIMARY KEY constraint...Cannot insert duplicate key in object dbo.stopping_distance_models
```

One option for avoiding this error is to update the name for each new model. For example, you could change the name to something more descriptive, and include the model type, the day you created it, and so forth.

```sql
UPDATE GLM_models
SET model_name = 'GLM_' + format(getdate(), 'yyyy.MM.HH.mm', 'en-gb')
WHERE model_name = 'default model'
```

## Next steps

Now that you have a model, in the final quickstart, you'll learn how to generate predictions from it and plot the results.

> [!div class="nextstepaction"]
> [Quickstart: Predict and plot from model](quickstart-r-predict-from-model.md)