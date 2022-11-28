--- 
 
# required metadata 
title: "microsoftml API" 
description: "microsoftml API" 
keywords: "microsoftml API, API" 
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.service: sql
ms.subservice: "machine-learning-services" 
ms.assetid: "" 
 
# optional metadata 
ROBOTS: "" 
audience: "" 
ms.devlang: "Python" 
ms.reviewer: "" 
ms.suite: "" 
ms.tgt_pltfrm: "" 
ms.custom: "" 
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# microsoftml API

## learners


**Applies to: SQL Server 2017 RC2**


### training functions

* [*microsoftml.rx_fast_forest*: Random Forest](rx-fast-forest.md) 

* [*microsoftml.rx_fast_linear*: Linear Model with Stochastic Dual Coordinate Ascent](rx-fast-linear.md) 

  * [loss functions](rx-fast-linear.md) 

    * [*microsoftml.hinge_loss*: Hinge loss function](hinge-loss.md) 

    * [*microsoftml.log_loss*: Log loss function](log-loss.md) 

    * [*microsoftml.smoothed_hinge_loss*: Smoothed hinge loss function](smoothed-hinge-loss.md) 

    * [*microsoftml.squared_loss*: Squared loss function](squared-loss.md) 

* [*microsoftml.rx_fast_trees*: Boosted Trees](rx-fast-trees.md) 

* [*microsoftml.rx_logistic_regression*: Logistic Regression](rx-logistic-regression.md) 

* [*microsoftml.rx_neural_network*: Neural Network](rx-neural-network.md) 

  * [optimizers](rx-neural-network.md) 

    * [*microsoftml.adadelta_optimizer*: Adaptive learing rate method](adadelta-optimizer.md) 

    * [*microsoftml.sgd_optimizer*: Stochastic gradient descent](sgd-optimizer.md) 

  * [math](rx-neural-network.md) 

    * [*microsoftml.avx_math*: Acceleration with AVX instructions](avx-math.md) 

    * [*microsoftml.clr_math*: Acceleration with .NET math](clr-math.md) 

    * [*microsoftml.gpu_math*: Acceleration with NVidia CUDA](gpu-math.md) 

    * [*microsoftml.mkl_math*: Acceleration with Intel MKL](mkl-math.md) 

    * [*microsoftml.sse_math*: Acceleration with SSE instructions](sse-math.md) 

* [*microsoftml.rx_oneclass_svm*: Anomaly Detection](rx-oneclass-svm.md) 


## transforms


### categorical variable handling

* [*microsoftml.categorical*: Converts a text column into categories](categorical.md) 

* [*microsoftml.categorical_hash*: Hashes and converts a text column into categories](categorical-hash.md) 


### schema manipulation

* [*microsoftml.concat*: Concatenates multiple columns into a single vector](concat.md) 

* [*microsoftml.drop_columns*: Drops columns from a dataset](drop-columns.md) 

* [*microsoftml.select_columns*: Retains columns of a dataset](select-columns.md) 


### variable selection

* [*microsoftml.count_select*: Feature selection based on counts](count-select.md) 

* [*microsoftml.mutualinformation_select*: Feature selection based on mutual information](mutualinformation-select.md) 


### text analytics

* [*microsoftml.featurize_text*: Converts text columns into numerical features](featurize-text.md) 

  * [N-grams extractors](featurize-text.md) 

    * [*microsoftml.n_gram*: Converts text into features using n-grams](n-gram.md) 

    * [*microsoftml.n_gram_hash*: Converts text into features using hashed n-grams](n-gram-hash.md) 

  * [Stopwords removers](featurize-text.md) 

    * [*microsoftml.custom*: Removes custom stopwords](custom.md) 

    * [*microsoftml.predefined*: Removes predefined stopwords](predefined.md) 

* [*microsoftml.get_sentiment*: Sentiment analysis](get-sentiment.md) 


### image analytics

* [*microsoftml.load_image*: Loads an image](load-image.md) 

* [*microsoftml.resize_image*: Resizes an Image](resize-image.md) 

* [*microsoftml.extract_pixels*: Extracts pixels form an image](extract-pixels.md) 

* [*microsoftml.featurize_image*: Converts an image into features](featurize-image.md) 


## scorers

* [*microsoftml.rx_predict*: Scores using a Microsoft machine learning model](rx-predict.md) 


## featurizers

* [*microsoftml.rx_featurize*: Data transformation for data sources](rx-featurize.md) 
