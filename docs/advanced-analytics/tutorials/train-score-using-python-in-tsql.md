---
title: "Use Python model in SQL for training and scoring | Microsoft Docs"
titleSuffix: "SQL Server"
ms.custom: ""
ms.date: "02/28/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2017"
dev_langs: 
  - "Python"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Use Python model in SQL for training and scoring
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

In this lesson, you train a model on the data you've added to SQL Server, and save the model to a SQL Server table.

As you learned in the [previous lesson](running-python-in-sql-stored-procedure.md), to wrap and run Python in a stored procedure might span multiple steps:

+ Create a stored procedure that does something, like creating a model
+ Provide input data as a parameter to the stored procedure, and execute the stored procedure to actually train it
+ Get the output and do something with it, like saving the model into a table, or passing it to another statement

Run this code in SQL Server Management Studio to create the stored procedure that builds a model.

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

In this case, the stored procedure doesn't output any data, just a single variable containing the trained model in binary format. You pass the trained model to SQL Server by mapping the Python variable `trained_model` to the SQL Server variable `@trained_model`:

```sql
@trained_model = @trained_model OUTPUT;
```

Keep in mind these rules for working with SQL and Python variables:

+ All variables in the Python code must have exactly the same names as the corresponding SQL variables. 
+ If you create a SQL parameter to use with Python, you can output that parameter back to SQL by adding the `OUTPUT` or `OUT` keywords.
+ All variables must be explicitly mapped. Suppose you add some new variables: one variable for the model name, another for the code author, and another containing the system date. You would add a line at the end of the stored procedure for each variable mapping:

    ```sql
    , @trained_model = @trained_model OUTPUT;
    , @model_name = @trained_model OUTPUT;
    , @model_owner = @model_owner OUTPUT;
    , @sys_date = @sys_date OUTPUT;

    You need not manipulate these new variables in the Python code; you might generate the system date using a SQL function and get the name of the author from the login context. But any variables that you want to pass through to output must be mapped.

If this command runs without error, a new stored procedure is created and added to the database. You can find stored procedures in Management Studio's **Object Explorer**, under **Programmability**.

### Train and save the model

By now, you should be getting the hang of how to separate Python tasks from SQL tasks. To train a model, you run a stored procedure that calls Python, and pass in data from SQL Server. To save the trained model, you insert the model into a table, using the SQL type **varbinary(max)**.

```sql
DECLARE @model varbinary(max);
EXEC generate_iris_model @model OUTPUT;
INSERT INTO iris_models (model_name, model) values('Naive Bayes', @model);
```

Now, try running the model generation code once more. You should get the error: "Violation of PRIMARY KEY constraint Cannot insert duplicate key in object 'dbo.iris_models'. The duplicate key value is (Naive Bayes)".

That's because the model name was provided by manually typing in "Naive Bayes" as part of the INSERT statement. Assuming you plan to create lots of models, perhaps using different parameters or different algorithms on each run, consider setting up a metadata scheme so that you can automatically name models and more easily identify them.

For example, you could generate a unique name by appending the date:

```sql
DECLARE @model varbinary(max);
DECLARE @new_model_name varchar(50)
SET @new_model_name = 'Naive Bayes ' + CAST(GETDATE()as varchar)
SELECT @new_model_name 
EXEC generate_iris_model @model OUTPUT;
INSERT INTO iris_models (model_name, model) values(@new_model_name, @model);
```

To view the model, run a simple SELECT statement.

```sql
SELECT * FROM iris_models;
GO
```

**Results**

|model_name	| model |
|------|------|
| Naive Bayes | 0x800363736B6C656172... |
| Naive Bayes Jan 01 2018  9:39AM | 0x800363736B6C656172... |
| Naive Bayes Feb 01 2018  10:51AM | 0x800363736B6C656172... |

Finally, let's load this model from the table into a variable, and pass it back to Python to generate scores.

Here's the stored procedure that performs scoring based on the Naïve Bayes model.

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
OutputDataSet = iris_data.query( ''PredictedSpecies != SpeciesId'' )[[0, 5, 6]]
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

+ The stored procedure gets the model from the table using the model name. However, depending on what kind of metadata you are saving with the model, you could also get the most recent model, or the model with the highest accuracy.

+ The input data used for scoring comes from the iris dataset provided as a part of the Python sample datasets: `iris_data[[1,2,3,4]])`. However, more typically you would run a SQL query to get the new data, and pass that into Python as InputDataSet. For an example, see this tutorial: Operationalizing a Python solution in SQL Server.

+ The stored procedure returns a Python data.frame. This line of T-SQL specifies the schema for the returned results: `WITH RESULT SETS ( ("id" int, "SpeciesId" int, "SpeciesId.Predicted" int));`

Run the following lines to pass the model name "Naive Bayes" to the stored procedure that executes the scoring code. You can insert the results into a new table, or return them to an application. 

```sql
EXEC predict_species 'Naive Bayes';
GO
```

### Remarks

If you are used to working in Python, you might be accustomed to loading data, creating some summaries and graphs, then training a model and generating some scores all in the same 250 lines of code.

However, if your goal is to operationalize the process (model creation, scoring, etc.) in SQL Server, it is important to consider ways that you can separate the process into repeatable steps that can be modified using parameters. As much as possible, you want the Python code you embed in a stored procedure to have clearly defined inputs and outputs that map to stored procedure inputs and outputs.

You can also improve performance by separating the data exploration process from the processes of training a model or generating scores, and by optimizing your scoring and training processes to use parallel processing, or by using [revoscalepy](../python/what-is-revoscalepy.md) or [MicrosoftML](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) rather than standard Python libraries. 

## Next steps

These additional Python samples and tutorials demonstrate end-to-end scenarios using more complex data sources, as well as the use of remote compute contexts.

+ [In-Database Python for SQL developers](sqldev-in-database-python-for-sql-developers.md)
+ [Build a predictive model using Python and SQL Server](https://microsoft.github.io/sql-ml-tutorials/python/rentalprediction/)
+ [Create a revoscalepy model from a Python client](use-python-revoscalepy-to-create-model.md)