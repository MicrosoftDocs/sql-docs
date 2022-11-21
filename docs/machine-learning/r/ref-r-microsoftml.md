---
title: MicrosoftML R package
description: MicrosoftML is an R package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services and supports high performance on big data, using multicore processing, and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/30/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# MicrosoftML (R package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

**MicrosoftML** is an R package from Microsoft that provides high-performance machine learning algorithms. It includes functions for training and transformations, scoring, text and image analysis, and feature extraction for deriving values from existing data. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md) and supports high performance on big data, using multicore processing, and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.

## Full reference documentation

The **MicrosoftML** package is distributed in multiple Microsoft products, but usage is the same whether you get the package in SQL Server or another product. Because the functions are the same, [documentation for individual RevoScaleR functions](/machine-learning-server/r-reference/revoscaler/revoscaler) is published to just one location under the [R reference](/machine-learning-server/r-reference/introducing-r-server-r-package-reference). Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Versions and platforms

The **MicrosoftML** package is based on R 3.5.2 and available only when you install one of the following Microsoft products or downloads:

+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)
+ [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)
+ [Microsoft R client](set-up-data-science-client.md)

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
|[rxFastTrees](reference/microsoftml/rxfasttrees.md) | An implementation of FastRank, an efficient implementation  of the MART gradient boosting algorithm.  |
|[rxFastForest](reference/microsoftml/rxfastforest.md) | A random forest and Quantile regression forest  implementation using [rxFastTrees](reference/microsoftml/rxfasttrees.md).  |
|[rxLogisticRegression](reference/microsoftml/logisticregression.md) | Logistic regression using L-BFGS.  |
|[rxOneClassSvm](reference/microsoftml/rxoneclasssvm.md) | One class support vector machines.  
|[rxNeuralNet](reference/microsoftml/rxneuralnet.md) | Binary, multi-class, and regression neural net.  |
|[rxFastLinear](reference/microsoftml/rxfastlinear.md) | Stochastic dual coordinate ascent optimization for linear binary classification and regression. |
|[rxEnsemble](reference/microsoftml/rxensemble.md) | Trains a number of models of various kinds to obtain better predictive performance than could be obtained from a single model.|

<a name="ml-transforms"></a>

## 2-Transformation functions

| Function name | Description |
|---------------|-------------|
|[concat](reference/microsoftml/concat.md) | Transformation to create a single vector-valued column from multiple columns.  |
|[categorical](reference/microsoftml/categorical.md) | Create indicator vector using categorical transform with dictionary.  |
|[categoricalHash](reference/microsoftml/categoricalhash.md) | Converts the categorical value into an indicator array by hashing. |
|[featurizeText](reference/microsoftml/featurizetext.md) | Produces a bag of counts of sequences of consecutive words, called n-grams, from a given corpus of text. It offers language detection, tokenization, stopwords removing, text normalization, and feature generation.  |
|[getSentiment](reference/microsoftml/getsentiment.md) | Scores natural language text and creates a column that contains probabilities that the sentiments in the text are positive.|
|[ngram](reference/microsoftml/ngram.md) | allows defining arguments for count-based and hash-based feature extraction.|
|[selectColumns](reference/microsoftml/selectcolumns.md) | Selects a set of columns to retrain, dropping all others. |
|[selectFeatures](reference/microsoftml/selectfeatures.md) | Selects features from the specified variables using a specified mode.|
|[loadImage](reference/microsoftml/loadimage.md) | Loads image data.|
|[resizeImage](reference/microsoftml/resizeimage.md) | Resizes an image to a specified dimension using a specified resizing method.|
|[extractPixels](reference/microsoftml/extractpixels.md) | Extracts the pixel values from an image.|
|[featurizeImage](reference/microsoftml/featurizeimage.md) | Featurizes an image using a pre-trained deep neural network model.|


## 3-Scoring and training functions

| Function name | Description |
|---------------|-------------|
|[rxPredict.mlModel](reference/microsoftml/rxpredict.md) | Runs the scoring library either from SQL Server, using the stored procedure, or from R code enabling real-time scoring to provide much faster prediction performance.|
|[rxFeaturize](reference/microsoftml/rxfeaturize.md) | Transforms data from an input data set to an output data set.|
|[mlModel](reference/microsoftml/mlmodel.md) | Provides a summary of a Microsoft R Machine Learning model.|


## 4-Loss functions for classification and regression

| Function name | Description |
|---------------|-------------|
|[expLoss](reference/microsoftml/loss.md) | Specifications for exponential classification loss function. | 
|[logLoss](reference/microsoftml/loss.md) | Specifications for log classification loss function.  |
|[hingeLoss](reference/microsoftml/loss.md) | Specifications for hinge classification loss function. | 
|[smoothHingeLoss](reference/microsoftml/loss.md) | Specifications for smooth hinge classification loss function.  |
| [poissonLoss](reference/microsoftml/loss.md) | Specifications for poisson regression loss function. | 
|[squaredLoss](reference/microsoftml/loss.md) | Specifications for squared regression loss function.   |   

## 5-Feature selection functions

| Function name | Description |
|---------------|-------------|
|[minCount](reference/microsoftml/mincount.md) | Specification for feature selection in count mode. |
|[mutualInformation](reference/microsoftml/mutualinformation.md) | Specification for feature selection in mutual information mode. |

## 6-Ensemble modeling functions

| Function name | Description |
|---------------|-------------|
|[fastTrees](reference/microsoftml/fasttrees.md) | Creates a list containing the function name and arguments to train a Fast Tree model with [rxEnsemble](reference/microsoftml/rxensemble.md).|
|[fastForest](reference/microsoftml/rxfastforest.md) | Creates a list containing the function name and arguments to train a Fast Forest model with [rxEnsemble](reference/microsoftml/rxensemble.md).|
|[fastLinear](reference/microsoftml/fastlinear.md) | Creates a list containing the function name and arguments to train a Fast Linear model with [rxEnsemble](reference/microsoftml/rxensemble.md).|
|[logisticRegression](reference/microsoftml/logisticregression.md) | Creates a list containing the function name and arguments to train a  Logistic Regression model with [rxEnsemble](reference/microsoftml/rxensemble.md).|
|[oneClassSvm](reference/microsoftml/oneclasssvm.md) | Creates a list containing the function name and arguments to train a OneClassSvm model with [rxEnsemble](reference/microsoftml/rxensemble.md).|

## 7-Neural networking functions

| Function name | Description |
|---------------|-------------|
|[optimizer](reference/microsoftml/optimizer.md) | Specifies optimization algorithms for the [rxNeuralNet](reference/microsoftml/rxneuralnet.md) machine learning algorithm.|


## 8-Package state functions

| Function name | Description |
|---------------|-------------|
|[rxHashEnv](reference/microsoftml/rxhashenv.md) | An environment object used to store package-wide state. |


## How to use MicrosoftML

Functions in **MicrosoftML** are callable in R code encapsulated in stored procedures. Most developers build **MicrosoftML** solutions locally, and then migrate finished R code to stored procedures as a deployment exercise.

The **MicrosoftML** package for R is installed "out-of-the-box" in SQL Server 2017.

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
+ [R reference](/machine-learning-server/r-reference/introducing-r-server-r-package-reference)