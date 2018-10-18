---
title: Use a Python model in SQL Server for training and predictions | Microsoft Docs
description: Create and train a model using Python and the classic Iris data set. Save the model to SQL Server, and then use it to generate predicted outcomes.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/18/2018  
ms.topic: tutorial
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Use a Python model in SQL Server for training and scoring
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this exercise, learn the basic technique for executing Python in SQL Server through sstored procedures. This exercise creates a Naïve Bayes model to predict an Iris species based on flower characteristics. Be sure you have completed the [previous lesson](wrap-python-in-tsql-stored-procedure.md): preparing the database objects used for training and scoring Iris data in Python.

Tasks in this article include creating two stored procedures to perform the following functions:

+ Generate a model
+ Generate predictions

By completing these tasks, you'll learn a common pattern used to create, train, and save a model using data from a SQL Server database.

+ Design a stored procedure that uses [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to start a Python process.
+ The stored procedure calls a Python machine learning function to specify which algorithm or model to use.
+ The stored procedure needs data from SQL Server to use in training the model.
+ The stored procedure outputs a trained model, serialized as a binary variable. 
+ The stored procedure saves the trained model by inserting the binary variable model into a table. 

## Create the stored procedure and train a Python model

1. Open a new query window in Management Studio, connected to the **sqlpy** database. 

    ```sql
    USE sqlpy
    GO
    ```

2. Run the following code in a new query window to create the stored procedure that builds and trains a model. This code uses pickle to serialize the model and scikit to provide the algorithm. The trained model contains column data from columns 0 through 4 from the **iris_data** table. 

    ```sql
    CREATE PROCEDURE generate_iris_model (@trained_model varbinary(max) OUTPUT)
    AS
    BEGIN
    EXEC sp_execute_external_script @language = N'Python',
    @script = N'
    import pickle
    from sklearn.naive_bayes import GaussianNB
    GNB = GaussianNB()
    trained_model = pickle.dumps(GNB.fit(iris_data[[0,1,2,3]], iris_data[[4]]))
    '
    , @input_data_1 = N'select "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "SpeciesId" from iris_data'
    , @input_data_1_name = N'iris_data'
    , @params = N'@trained_model varbinary(max) OUTPUT'
    , @trained_model = @trained_model OUTPUT;
    END;
    GO
    ```

3. Verify the object is created. If the T-SQL script from the previous step runs without error, a new stored procedure called **generate_iris_model** is created and added to the **sqlpy** database. You can find stored procedures in Management Studio's **Object Explorer**, under **Programmability**.

4. Run the stored procedure to create the model. This particular script deletes an existing model of the same name ("Naive Bayes") so that it can be recreated on subsequent reruns of the same procedure. Without model deletion, an error occurs stating that the object already exists.

    ```sql
    DECLARE @model varbinary(max);
    DECLARE @new_model_name varchar(50)
    SET @new_model_name = 'Naive Bayes '
    SELECT @new_model_name 
    EXEC generate_iris_model @model OUTPUT;
    DELETE iris_models WHERE model_name = @new_model_name;
    INSERT INTO iris_models (model_name, model) values(@new_model_name, @model);
    GO
    ```

    The script includes a SELECT statement showing that the model exists.

    **Results**

    |   | (no column name |
    |---|-----------------|
    | 1 | Naive Bayes     | 


## Generate predictions

In this step, create a stored procedure that generates predictions. You'll do this by calling sp_execute_external_script to start Python, and then pass in Python script that loads a serialized model from the **iris_models** table into a variable, and passes it back to Python to generate scores.

1. Run the following code to create the stored procedure that performs scoring. The procedure loads a binary model, uses columns `[1,2,3,4]` for predictions, and includes columns `[0,5,6]` in output.

    ```sql
    CREATE PROCEDURE predict_species (@model varchar(100))
    AS
    BEGIN
    DECLARE @nb_model varbinary(max) = (SELECT model FROM iris_models WHERE model_name = @model);
    EXEC sp_execute_external_script @language = N'Python', 
    @script = N'
    import pickle
    irismodel = pickle.loads(nb_model)
    species_pred = irismodel.predict(iris_data[[1,2,3,4]])
    iris_data["PredictedSpecies"] = species_pred
    OutputDataSet = iris_data[[0,5,6]] 
    print(OutputDataSet)
    '
    , @input_data_1 = N'select id, "Sepal.Length", "Sepal.Width", "Petal.Length", "Petal.Width", "SpeciesId" from iris_data'
    , @input_data_1_name = N'iris_data'
    , @params = N'@nb_model varbinary(max)'
    , @nb_model = @nb_model
    WITH RESULT SETS ( ("id" int, "SpeciesId" int, "SpeciesId.Predicted" int));
    END;
    GO
    ```

2. Run the following lines to pass the model name "Naive Bayes" to the stored procedure that executes the scoring code. 

    ```sql
    EXEC predict_species 'Naive Bayes';
    GO
    ```

    When you run the stored procedure, it returns a Python data.frame. This line of T-SQL specifies the schema for the returned results: `WITH RESULT SETS ( ("id" int, "SpeciesId" int, "SpeciesId.Predicted" int));`

    ![Result set from running stored procedure](media/train-score-using-python-NB-model-results.png)

    You can insert the results into a new table, or return them to an application.

    This example has been made simple by using the data from the Python iris dataset for scoring. (See the line `iris_data[[1,2,3,4]])`.) However, more typically you would run a SQL query to get the new data, and pass that into Python as `InputDataSet`. 

## Remarks

If you are used to working in Python, you might be accustomed to loading data, creating some summaries and graphs, then training a model and generating some scores all in the same 250 lines of code.

However, if your goal is to operationalize the process (model creation, scoring, and so forth) in SQL Server, it is important to consider ways that you can separate the process into repeatable steps that can be modified using parameters. As much as possible, you want the Python code that you run in a stored procedure to have clearly defined inputs and outputs that map to stored procedure inputs and outputs.

Moreover, you can generally improve performance by separating the data exploration process from the processes of training a model or generating scores. 

Scoring and training processes can often be optimized by leveraging features of SQL Server, such as parallel processing, or by using algorithms in [revoscalepy](../python/what-is-revoscalepy.md) or [MicrosoftML](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) that support streaming and parallel execution, rather than using standard Python libraries. 

## Common mistakes

If you are new to T-SQL or stored procedures, it might help to take a closer look at how stored procedures are called, and what errors you might encounter if you overlook a step or tow.

### Missing an input paramter to a stored procedure

The **generate_iris_model** stored procedure in this exercise requires an input. If you omit the required input and run it as shown below, you might get this error: "Procedure or function 'generate_iris_model' expects parameter '\@trained_model', which was not supplied."

```sql
EXEC generate_iris_model
```

    


## Next steps

Previous tutorials focused on local execution. However, you can also run Python code from a client workstation, using SQL Server as the remote compute context. For more information about setting up a client workstation that connects to SQL Server, see [Set up Python client tools](../python/setup-python-client-tools-sql.md).

+ [Create a revoscalepy model from a Python client](use-python-revoscalepy-to-create-model.md)
