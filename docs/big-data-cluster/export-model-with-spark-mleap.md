---
title: Export Spark ML models with MLeap
titleSuffix: SQL Server 2019 big data clusters
description: Learn how to export Spark machine learning models with MLeap.
author: lgongmsft
ms.author: shivprashant
ms.reviewer: jroth
manager: craigg
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Export Spark machine learning models with MLeap

A typical machine learning scenario involves model training on Spark and scoring outside of Spark. Export models in a portable format such that it can be used outside Spark. [MLeap](https://github.com/combust/mleap) is one such model exchange format. It allows Spark machine learning pipelines and models to be exported as portable formats and used in any JVM-based system with the `Mleap` runtime.

This guide demonstrates how you can export your spark models using Mleap. The steps are summarized below and detailed with code in the following section.

1. Start by creating a Spark model. For this use **Training and Creating machine learning model with Spark [here.](train-and-create-machinelearning-models-with-spark.md)**
2. As a next step we'll **Import the training\test data and the Model**
3. **Export the model as `Mleap` bundle**. This exported bundle can now be used to score outside spark.
4. To validate, we'll import the `Mleap` bundle back again and use that to score in Spark.

## Step 1 - Start by creating a Spark model
Run [Training and Creating machine learning model with Spark](train-and-create-machinelearning-models-with-spark.md) to create training/test sets and model, and persist to HDFS storage. The model should be exported as `AdultCensus.mml` under the `spark_ml` directory.

## Step 2 - Import the training\test data and the Model

Step 1 created the `AdultCensus.mml`, which is a spark model. 

In this step, import the spark model.

```python
import mleap.pyspark
from mleap.pyspark.spark_support import SimpleSparkSerializer
from pyspark.ml import PipelineModel

model_name = "AdultCensus.mml"
model_fs = "/spark_ml/" + model_name

print("load pyspark model from hbfs")
model = PipelineModel.load(model_fs)
print("Model is " , model)
print("Model stages", model.stages)
```

## Step 3 - Export the model as `Mleap` bundle

Export the Spark model as a portable `Mleap` model and persist it in local storage. Post this step, the model is available in a portable `Mleap` format and can be used outside Spark.

```python
import os

#Get the train and test datasets

# Write the train and test data sets to intermediate storage

train_data_path = "/spark_ml/AdultCensusIncomeTrain"
test_data_path = "/spark_ml/AdultCensusIncomeTest"

train = spark.read.orc(train_data_path)
test = spark.read.orc(test_data_path)

print("train: ({}, {})".format(train.count(), len(train.columns)))
train.printSchema()

print("test: ({}, {})".format(test.count(), len(test.columns)))
test.printSchema()

# serialize the model to a zip file in JSON format
model_name_export = "adult_census_pipeline.zip"
model_name_path = os.getcwd()
model_file = os.path.join(model_name_path, model_name_export)

# serialize the model to a zip file in JSON format
model_name_export = "adult_census_pipeline.zip"
model_name_path = os.getcwd()
model_file = os.path.join(model_name_path, model_name_export)

# remove an old model file, if needed.
try:
    os.remove(model_file)
except OSError:
    pass

model_file_path = "jar:file:{}".format(model_file)
model.serializeToBundle(model_file_path, model.transform(train))

```

Persist the `Mleap` bundle from local to hdfs

```python
print("persist the mleap bundle from local to hdfs")
from subprocess import Popen, PIPE
proc = Popen(["hadoop", "fs", "-put", "-f", model_file, os.path.join("/spark_ml", model_name_export)], stdout=PIPE, stderr=PIPE)
s_output, s_err = proc.communicate()
if(s_err):
    print("Error when storing to HDFS")
```

## Step 3 - Validate by Importing the `Mleap` bundle and Scoring in Spark
In step 2, we have already exported the model to a portable `Mleap` format that can be used outside Spark. In this step, We import the `Mleap` serialized in Spark and use it in Spark to Score on the test set.
   
```python
model_deserialized = PipelineModel.deserializeFromBundle(model_file_path)
print("The deserialized model is ", model_deserialized)
print("The deserialized model stages are", model_deserialized.stages)

from pyspark.ml.evaluation import BinaryClassificationEvaluator

# make prediction
pred = model_deserialized.transform(test)


# evaluate. note only 2 metrics are supported out of the box by Spark ML.
bce = BinaryClassificationEvaluator(rawPredictionCol='rawPrediction')
au_roc = bce.setMetricName('areaUnderROC').evaluate(pred)
au_prc = bce.setMetricName('areaUnderPR').evaluate(pred)

print("Results of using the model score test set")
print("Area under ROC: {}".format(au_roc))
print("Area Under PR: {}".format(au_prc))
```

## References

* [Getting started with PySpark notebooks](notebooks-guidance.md)
* [Training and Creating machine learning model with Spark](train-and-create-machinelearning-models-with-spark.md)
