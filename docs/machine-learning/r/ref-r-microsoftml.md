---
title: MicrosoftML R package
description: MicrosoftML is an R package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services and supports high performance on big data, using multicore processing, and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 11/06/2019
ms.topic: how-to
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# MicrosoftML (R package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

**MicrosoftML** is an R package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md) and supports high performance on big data, using multicore processing, and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.

## Full reference documentation

The **MicrosoftML** package is distributed in multiple Microsoft products, but usage is the same whether you get the package in SQL Server or another product. Because the functions are the same, [documentation for individual RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) is published to just one location under the [R reference](/machine-learning-server/r-reference/introducing-r-server-r-package-reference) for Microsoft Machine Learning Server. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **MicrosoftML** package is based on R 3.4.3 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)
+ [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft Machine Learning Server 9.2.0 or later](/machine-learning-server/)
+ [Microsoft R client](set-up-a-data-science-client.md)

> [!NOTE]
> Full product release versions are Windows-only in SQL Server 2017. Both Windows and Linux are supported for **MicrosoftML** in [SQL Server 2019](../../linux/sql-server-linux-setup-machine-learning.md).

## Package dependencies

Algorithms in **MicrosoftML** depend on [RevoScaleR](ref-r-revoscaler.md) for:

+ Data source objects. Data consumed by **MicrosoftML** functions are created using **RevoScaleR** functions.
+ Remote computing (shifting function execution to a remote SQL Server instance). The **RevoScaleR** package provides functions for creating and activating a remote compute context for SQL server.

In most cases, you will load the packages together whenever you are using **MicrosoftML**.

## Functions by category

This section lists the functions by category to give you an idea of how each one is used. You can also use the [table of contents](/machine-learning-server/r-reference/introducing-r-server-r-package-reference)  to find functions in alphabetical order.

<a name="ml-algorithms"></a>

## 1-Machine learning algorithms

| Function name | Description |
|---------------|-------------|
|[rxFastTrees](/machine-learning-server/r-reference/microsoftml/rxfasttrees) | An implementation of FastRank, an efficient implementation  of the MART gradient boosting algorithm.  |
|[rxFastForest](/machine-learning-server/r-reference/microsoftml/rxfastforest) | A random forest and Quantile regression forest  implementation using [rxFastTrees](/machine-learning-server/r-reference/microsoftml/rxfasttrees).  |
|[rxLogisticRegression](/machine-learning-server/r-reference/microsoftml/logisticregression) | Logistic regression using L-BFGS.  |
|[rxOneClassSvm](/machine-learning-server/r-reference/microsoftml/rxoneclasssvm) | One class support vector machines.  
|[rxNeuralNet](/machine-learning-server/r-reference/microsoftml/rxneuralnet) | Binary, multi-class, and regression neural net.  |
|[rxFastLinear](/machine-learning-server/r-reference/microsoftml/rxfastlinear) | Stochastic dual coordinate ascent optimization for linear binary classification and regression. |
|[rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble) | Trains a number of models of various kinds to obtain better predictive performance than could be obtained from a single model.|

<a name="ml-transforms"></a>

## 2-Transformation functions

| Function name | Description |
|---------------|-------------|
|[concat](/machine-learning-server/r-reference/microsoftml/concat) | Transformation to create a single vector-valued column from multiple columns.  |
|[categorical](/machine-learning-server/r-reference/microsoftml/categorical) | Create indicator vector using categorical transform with dictionary.  |
|[categoricalHash](/machine-learning-server/r-reference/microsoftml/categoricalhash) | Converts the categorical value into an indicator array by hashing. |
|[featurizeText](/machine-learning-server/r-reference/microsoftml/featurizetext) | Produces a bag of counts of sequences of consecutive words, called n-grams, from a given corpus of text. It offers language detection, tokenization, stopwords removing, text normalization, and feature generation.  |
|[getSentiment](/machine-learning-server/r-reference/microsoftml/getsentiment) | Scores natural language text and creates a column that contains probabilities that the sentiments in the text are positive.|
|[ngram](/machine-learning-server/r-reference/microsoftml/ngram) | allows defining arguments for count-based and hash-based feature extraction.|
|[selectColumns](/machine-learning-server/r-reference/microsoftml/selectcolumns) | Selects a set of columns to retrain, dropping all others. |
|[selectFeatures](/machine-learning-server/r-reference/microsoftml/selectfeatures) | Selects features from the specified variables using a specified mode.|
|[loadImage](/machine-learning-server/r-reference/microsoftml/loadimage) | Loads image data.|
|[resizeImage](/machine-learning-server/r-reference/microsoftml/resizeimage) | Resizes an image to a specified dimension using a specified resizing method.|
|[extractPixels](/machine-learning-server/r-reference/microsoftml/extractpixels) | Extracts the pixel values from an image.|
|[featurizeImage](/machine-learning-server/r-reference/microsoftml/featurizeimage) | Featurizes an image using a pre-trained deep neural network model.|


## 3-Scoring and training functions

| Function name | Description |
|---------------|-------------|
|[rxPredict.mlModel](/machine-learning-server/r-reference/microsoftml/rxpredict) | Runs the scoring library either from SQL Server, using the stored procedure, or from R code enabling real-time scoring to provide much faster prediction performance.|
|[rxFeaturize](/machine-learning-server/r-reference/microsoftml/rxfeaturize) | Transforms data from an input data set to an output data set.|
|[mlModel](/machine-learning-server/r-reference/microsoftml/mlmodel) | Provides a summary of a Microsoft R Machine Learning model.|


## 4-Loss functions for classification and regression

| Function name | Description |
|---------------|-------------|
|[expLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for exponential classification loss function. | 
|[logLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for log classification loss function.  |
|[hingeLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for hinge classification loss function. | 
|[smoothHingeLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for smooth hinge classification loss function.  |
| [poissonLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for poisson regression loss function. | 
|[squaredLoss](/machine-learning-server/r-reference/microsoftml/loss) | Specifications for squared regression loss function.   |   

## 5-Feature selection functions

| Function name | Description |
|---------------|-------------|
|[minCount](/machine-learning-server/r-reference/microsoftml/mincount) | Specification for feature selection in count mode. |
|[mutualInformation](/machine-learning-server/r-reference/microsoftml/mutualinformation) | Specification for feature selection in mutual information mode. |

## 6-Ensemble modeling functions

| Function name | Description |
|---------------|-------------|
|[fastTrees](/machine-learning-server/r-reference/microsoftml/fasttrees) | Creates a list containing the function name and arguments to train a Fast Tree model with [rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[fastForest](/machine-learning-server/r-reference/microsoftml/rxfastforest) | Creates a list containing the function name and arguments to train a Fast Forest model with [rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[fastLinear](/machine-learning-server/r-reference/microsoftml/fastlinear) | Creates a list containing the function name and arguments to train a Fast Linear model with [rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[logisticRegression](/machine-learning-server/r-reference/microsoftml/logisticregression) | Creates a list containing the function name and arguments to train a  Logistic Regression model with [rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble).|
|[oneClassSvm](/machine-learning-server/r-reference/microsoftml/oneclasssvm) | Creates a list containing the function name and arguments to train a OneClassSvm model with [rxEnsemble](/machine-learning-server/r-reference/microsoftml/rxensemble).|

## 7-Neural networking functions

| Function name | Description |
|---------------|-------------|
|[optimizer](/machine-learning-server/r-reference/microsoftml/optimizer) | Specifies optimization algorithms for the [rxNeuralNet](/machine-learning-server/r-reference/microsoftml/rxneuralnet) machine learning algorithm.|


## 8-Package state functions

| Function name | Description |
|---------------|-------------|
|[rxHashEnv](/machine-learning-server/r-reference/microsoftml/rxHashEnv) | An environment object used to store package-wide state. |


## How to use MicrosoftML

Functions in **MicrosoftML** are callable in R code encapsulated in stored procedures. Most developers build **MicrosoftML** solutions locally, and then migrate finished R code to stored procedures as a deployment exercise.

The **MicrosoftML** package for R is installed "out-of-the-box" in SQL Server 2017. It is also available for use with SQL Server 2016 if you upgrade the R components for the instance: [Upgrade an instance of SQL Server using binding](../install/upgrade-r-and-python.md)

The package is not loaded by default. As a first step, load the **MicrosoftML** package, and then load **RevoScaleR** if you need to use remote compute contexts or related connectivity or data source objects. Then, reference the individual functions you need.

```R
library(microsoftml);
library(RevoScaleR);
logisticRegression(args);
```

## See also

+ [R tutorials](../tutorials/r-tutorials.md)
+ [Learn to use compute contexts](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [R for SQL developers: Train and operationalize a model](../tutorials/r-taxi-classification-introduction.md)
+ [Microsoft product samples on GitHub](https://github.com/Microsoft/SQL-Server-R-Services-Samples)
+ [R reference (Microsoft Machine Learning Server)](/machine-learning-server/r-reference/introducing-r-server-r-package-reference)