---
title: MicrosoftML R function library - SQL Server Machine Learning Services
description: Introduction to the MicrosoftML function library in SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services with R.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# MicrosoftML (R library in SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

**MicrosoftML** is an R function library from Microsoft providing high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data.

The machine learning APIs were developed by Microsoft for internal machine learning applications, and have been refined over the years to support high performance on big data, using multicore processing and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.

## Full reference documentation

The **MicrosoftML** library is distributed in multiple Microsoft products, but usage is the same whether you get the library in SQL Server or another product. Because the functions are the same, [documentation for individual RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) is published to just one location under the [R reference](https://docs.microsoft.com/machine-learning-server/r-reference/introducing-r-server-r-package-reference) for Microsoft Machine Learning Server. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **MicrosoftML** library is based on R 3.4.3 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)
+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft Machine Learning Server 9.2.0 or later](https://docs.microsoft.com/machine-learning-server/)
+ [Microsoft R client](set-up-a-data-science-client.md)

> [!NOTE]
> Full product release versions are Windows-only, starting with SQL Server 2017. Linux support for **MicrosoftML** is new in [SQL Server 2019 Preview](../../linux/sql-server-linux-setup-machine-learning.md).

## Package dependencies

Algorithms in **MicrosoftML** depend on [RevoScaleR](ref-r-revoscaler.md) for:

+ Data source objects. Data consumed by **MicrosoftML** functions are created using **RevoScaleR** functions.
+ Remote computing (shifting function execution to a remote SQL Server instance). The **RevoScaleR** library provides functions for creating and activating a remote compute context for SQL server.

In most cases, you will load the packages together whenever you are using **MicrosoftML**.

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the [table of contents](https://docs.microsoft.com/machine-learning-server/r-reference/introducing-r-server-r-package-reference)  to find functions in alphabetical order.

<a name="ml-algorithms"></a>

## 1-Machine learning algorithms

| Function name | Description |
|---------------|-------------|
|[rxFastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees) | An implementation of FastRank, an efficient implementation  of the MART gradient boosting algorithm.  |
|[rxFastForest](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastforest) | A random forest and Quantile regression forest  implementation using [rxFastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfasttrees).  |
|[rxLogisticRegression](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/logisticregression) | Logistic regression using L-BFGS.  |
|[rxOneClassSvm](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxoneclasssvm) | One class support vector machines.  
|[rxNeuralNet](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet) | Binary, multi-class, and regression neural net.  |
|[rxFastLinear](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastlinear) | Stochastic dual coordinate ascent optimization for linear binary classification and regression. |
|[rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble) | Trains a number of models of various kinds to obtain better predictive performance than could be obtained from a single model.|

<a name="ml-transforms"></a>

## 2-Transformation functions

| Function name | Description |
|---------------|-------------|
|[concat](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/concat) | Transformation to create a single vector-valued column from multiple columns.  |
|[categorical](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categorical) | Create indicator vector using categorical transform with dictionary.  |
|[categoricalHash](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/categoricalhash) | Converts the categorical value into an indicator array by hashing. |
|[featurizeText](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/featurizetext) | Produces a bag of counts of sequences of consecutive words, called n-grams, from a given corpus of text. It offers language detection, tokenization, stopwords removing, text normalization and feature generation.  |
|[getSentiment](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/getsentiment) | Scores natural language text and creates a column that contains probabilities that the sentiments in the text are positive.|
|[ngram](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/ngram) | allows defining arguments for count-based and hash-based feature extraction.|
|[selectColumns](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/selectcolumns) | Selects a set of columns to retrain, dropping all others. |
|[selectFeatures](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/selectfeatures) | Selects features from the specified variables using a specified mode.|
|[loadImage](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loadimage) | Loads image data.|
|[resizeImage](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/resizeimage) | Resizes an image to a specified dimension using a specified resizing method.|
|[extractPixels](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/extractpixels) | Extracts the pixel values from an image.|
|[featurizeImage](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/featurizeimage) | Featurizes an image using a pre-trained deep neural network model.|


## 3-Scoring and training functions

| Function name | Description |
|---------------|-------------|
|[rxPredict.mlModel](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxpredict) | Runs the scoring library either from SQL Server, using the stored procedure, or from R code enabling real-time scoring to provide much faster prediction performance.|
|[rxFeaturize](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfeaturize) | Transforms data from an input data set to an output data set.|
|[mlModel](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/mlmodel) | Provides a summary of a Microsoft R Machine Learning model.|


## 4-Loss functions for classification and regression

| Function name | Description |
|---------------|-------------|
|[expLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for exponential classification loss function. | 
|[logLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for log classification loss function.  |
|[hingeLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for hinge classification loss function. | 
|[smoothHingeLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for smooth hinge classification loss function.  |
| [poissonLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for poisson regression loss function. | 
|[squaredLoss](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/loss) | Specifications for squared regression loss function.   |   

## 5-Feature selection functions

| Function name | Description |
|---------------|-------------|
|[minCount](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/mincount) | Specification for feature selection in count mode. |
|[mutualInformation](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/mutualinformation) | Specification for feature selection in mutual information mode. |

## 6-Ensemble modeling functions

| Function name | Description |
|---------------|-------------|
|[fastTrees](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/fasttrees) | Creates a list containing the function name and arguments to train a Fast Tree model with [rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[fastForest](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxfastforest) | Creates a list containing the function name and arguments to train a Fast Forest model with [rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[fastLinear](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/fastlinear) | Creates a list containing the function name and arguments to train a Fast Linear model with [rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[logisticRegression](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/logisticregression) | Creates a list containing the function name and arguments to train a  Logistic Regression model with [rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[oneClassSvm](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/oneclasssvm) | Creates a list containing the function name and arguments to train a OneClassSvm model with [rxEnsemble](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxensemble).|

## 7-Neural networking functions

| Function name | Description |
|---------------|-------------|
|[optimizer](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/optimizer) | Specifies optimization algorithms for the [rxNeuralNet](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxneuralnet) machine learning algorithm.|


## 8-Package state functions

| Function name | Description |
|---------------|-------------|
|[rxHashEnv](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/rxHashEnv) | An environment object used to store package-wide state. |


## How to use MicrosoftML

Functions in **MicrosoftML** are callable in R code encapsulated in stored procedures. Most developers build **MicrosoftML** solutions locally, and then migrate finished R code to stored procedures as a deployment exercise.

The **MicrosoftML** package for R is installed "out-of-the-box" in SQL Server 2017. It is also available for use with SQL Server 2016 if you upgrade the R components for the instance: [Upgrade an instance of SQL Server using binding](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)

The package is not loaded by default. As a first step, load the **MicrosoftML** package, and then load **RevoScaleR** if you need to use remote compute contexts or related connectivity or data source objects. Then, reference the individual functions you need.

```R
library(microsoftml);
library(RevoScaleR);
logisticRegression(args);
```

## See also

+ [R tutorials](../tutorials/sql-server-r-tutorials.md)
+ [Learn to use compute contexts](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [R for SQL developers: Train and operationalize a model](../tutorials/sqldev-in-database-r-for-sql-developers.md)
+ [Microsoft product samples on GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples)
+ [R reference (Microsoft Machine Learning Server)](https://docs.microsoft.com/machine-learning-server/r-reference/introducing-r-server-r-package-reference) 