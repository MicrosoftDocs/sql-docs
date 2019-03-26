---
title: Train/Create ML models with Spark 
titleSuffix: SQL Server 2019 big data clusters
description: Use PySpark to train and create machine learning models with Spark on SQL Server big data clusters (preview).
author: lgongmsft
ms.author: shivprashant
ms.manager: craigg
ms.reviewer: jroth
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Train and Create machine learning models with Spark

Spark in SQL Server big data cluster enables AI and machine learning. The example demonstrates how to training a machine learning model using Python in Spark (PySpark) using data stored in HDFS. 

The example is a step by step guide with code snippets that can be used from an Azure Data Studio Notebook and each cell run one step at a time. For more information on how to connect with Spark from notebook refer [here](notebooks-guidance.md)

In the example:

1. Start with **understanding the data and prediction desired**
2. **Upload the data to HDFS and prepare the data** for creating the model
3. **Select features to use**
4. **Split data as  training and test set**
5. Put together a **ml pipeline and build a model**
6. Use the created model to **make predictions**
7. As a final step, **persist the created model to later use**.

E2E machine learning involves several additional steps, e.g, data exploration, feature selection and principal component analysis, model selection. Many of these steps are ignored here for brevity.

## Step 1 - Understanding the data and prediction desired

This example uses adult census income data from [here]( https://amldockerdatasets.azureedge.net/AdultCensusIncome.csv ). In `AdultCensusIncome.csv`, each row represents income range and other characteristics like age, hours-per-week, education, occupation etc for a given adult. Build a model that can predict if the income range. The model will take age and hours-per-week as features and predict if the income would be >50 K or <50 K. 

## Step 2 - Upload the data to HDFS and basic explorations on data
From Azure Data Studio connect to the HDFS/Spark gateway, and create a directory called `spark_ml` under HDFS. 
Download [AdultCensusIncome.csv]( https://amldockerdatasets.azureedge.net/AdultCensusIncome.csv ) to your local machine and upload to HDFS. Upload `AdultCensusIncome.csv` to the folder you created.


Now, write some code. You can copy the code below and paste it in individual cells of a notebook in Azure Data Studio. 

The code below reads the CSV file to Spark data frame. Further to that it counts the number of rows and columns and display the loaded data.

```python
datafile = "/spark_ml/AdultCensusIncome.csv"

#Read the data to a spark data frame.
data_all = spark.read.format('csv').options(header='true', inferSchema='true', ignoreLeadingWhiteSpace='true', ignoreTrailingWhiteSpace='true').load(datafile)
print("Number of rows: {},  Number of coulumns : {}".format(data_all.count(), len(data_all.columns)))

#Replace "-" with "_" in column names
columns_new = [col.replace("-", "_") for col in data_all.columns]
data_all = data_all.toDF(*columns_new)

#Print Schema and show top 5 row
data_all.printSchema() 
data_all.show(5)
```

## Step 3 - Select features to use

In this step, use the following two terms
1. `Label`    - Refers to value to predict. This is represented as a column in the data.  
2. `Features` - Refers to the characteristics in data to predict. Also referred some time as `predictors` 

In this example `Label`, is the **income** column. For simplicity, choose **age** and **hours_per_week** as `Features`. In reality features are chosen by applying some correlation techniques to understand what best characterize the predicting label.

```python
# Choose feature columns and the label column.
label = "income"
xvars = ["age", "hours_per_week"] #all numeric

print("label = {}".format(label))
print("features = {}".format(xvars))

select_cols = xvars
select_cols.append(label)
data = data_all.select(select_cols)

```

## Step 4 - Split as training and test set

Use 75% of rows to train the model and rest of the 25% to evaluate the model. Additionally, persist the train and test data sets to HDFS storage. The step is not necessary,but shown to demonstrate saving and loading with ORC format. Other formats, for example, `Parquet` may also be used.

Post this step you should see two directories created with the name AdultCensusIncomeTrain and AdultCensusIncomeTest

```python

# Split data into train and test.
train, test = data.randomSplit([0.75, 0.25], seed=123)

print("train ({}, {})".format(train.count(), len(train.columns)))
print("test ({}, {})".format(test.count(), len(test.columns)))

train_data_path = "/spark_ml/AdultCensusIncomeTrain"
test_data_path = "/spark_ml/AdultCensusIncomeTest"

train.write.mode('overwrite').orc(train_data_path)
test.write.mode('overwrite').orc(test_data_path)
print("train and test datasets saved to {} and {}".format(train_data_path, test_data_path))

```

## Step 5 - Put together a pipeline and build a model
[Spark ML pipeline](https://spark.apache.org/docs/2.3.1/ml-pipeline.html) allow to sequence all steps as a workflow and make it easier to experiment with various algorithms and their parameters. The following code first constructs the stages and then puts these stages together in Ml pipeline.  LogisticRegression is used to create the model.

```python
from pyspark.ml import Pipeline, PipelineModel
from pyspark.ml.feature import OneHotEncoder, StringIndexer, VectorAssembler
from pyspark.ml.classification import LogisticRegression

reg = 0.1
print("Using LogisticRegression model with Regularization Rate of {}.".format(reg))

# create a new Logistic Regression model.
lr = LogisticRegression(regParam=reg)

dtypes = dict(train.dtypes)
dtypes.pop(label)

si_xvars = []
ohe_xvars = []
featureCols = []
for idx,key in enumerate(dtypes):
    if dtypes[key] == "string":
        featureCol = "-".join([key, "encoded"])
        featureCols.append(featureCol)
        
        tmpCol = "-".join([key, "tmp"])
        si_xvars.append(StringIndexer(inputCol=key, outputCol=tmpCol, handleInvalid="skip")) #, handleInvalid="keep"
        ohe_xvars.append(OneHotEncoder(inputCol=tmpCol, outputCol=featureCol))
    else:
        featureCols.append(key)

# string-index the label column into a column named "label"
si_label = StringIndexer(inputCol=label, outputCol='label')

# assemble the encoded feature columns in to a column named "features"
assembler = VectorAssembler(inputCols=featureCols, outputCol="features")

```

Now, put together the pipeline. 

```python
# put together the pipeline
stages = []
stages.extend(si_xvars)
stages.extend(ohe_xvars)
stages.append(si_label)
stages.append(assembler)
stages.append(lr)
pipe = Pipeline(stages=stages)
print("Pipeline Created")

```

Now that the pipeline is created, use that to train the model.

```python
# train the model
model = pipe.fit(train)
print("Model Trained")
print("Model is ", model)
print("Model Stages", model.stages)

```

## Step 6 - Predict using the model and Evaluate the model accuracy
The code below uses test data set to predict the outcome using the model created in the step above. It measures accuracy of the model with `areaUnderROC` and `areaUnderPR` metric.

```python
from pyspark.ml.evaluation import BinaryClassificationEvaluator
# make prediction
pred = model.transform(test)

# evaluate. note only 2 metrics are supported out of the box by Spark ML.
bce = BinaryClassificationEvaluator(rawPredictionCol='rawPrediction')
au_roc = bce.setMetricName('areaUnderROC').evaluate(pred)
au_prc = bce.setMetricName('areaUnderPR').evaluate(pred)

print("Area under ROC: {}".format(au_roc))
print("Area Under PR: {}".format(au_prc))
```


## Step 7 - Persist the models to HDFS
Finally, persist the model in HDFS for later use. Post this step the created model get saved as /spark_ml/AdultCensus.mml

```python
##NOTE: by default the model is saved to and loaded from path

model_name = "AdultCensus.mml"
model_fs = "/spark_ml/" + model_name

model.write().overwrite().save(model_fs)
print("saved model to {}".format(model_fs))

# load the model file (from dbfs)
model2 = PipelineModel.load(model_fs)
assert str(model2) == str(model)
print("loaded model from {}".format(model_fs))
```

## Next steps

For more information on how to get started with PySpark notebooks, see [How to use notebooks](notebooks-guidance.md).