---
title: "Install pretrained machine learning models on SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/15/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 21456462-e58a-44c3-9d3a-68b4263575d7
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Install pretrained machine learning models on SQL Server

This topic describes how to add pretrained models to an instance of SQL Server that already has R Services or Machine Learning Services installed.

Pretrained models are provided with the update to Microsoft R Server (or the update to Microsoft Machine Learning Server). For information about how to upgrade your instance and get the latest version of Microsoft R, see [Upgrade the R components in an instance of R Services](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

You can install these models only by running the separate Windows-based installer for R Server.
However, there are some additional steps to use when installing the models on SQL Server. This topic describes the process.

## Benefits of using pretrained models

Pre-trained models have been made available to support customers who need to perform tasks such as sentiment analysis or image featurization, but do not have the resources to obtain the large datasets or train a complex model. Using pre-trained models lets you get started on text and image processing most efficiently.

Currently the models that are available are deep neural network (DNN) models for sentiment analysis and image classification. All four pretrained models were trained on CNTK. The configuration of each network was based on the following reference implementations:

+ ResNet-18
+ ResNet-50
+ ResNet-101
+ AlexNet

For more information about deep residual networks and their implementation using CNTK, see these articles:

+ [Microsoft Researchers’ Algorithm Sets ImageNet Challenge Milestone](https://www.microsoft.com/research/blog/microsoft-researchers-algorithm-sets-imagenet-challenge-milestone/)

+ [Microsoft Computational Network Toolkit offers most efficient distributed deep learning computational performance](https://www.microsoft.com/research/blog/microsoft-computational-network-toolkit-offers-most-efficient-distributed-deep-learning-computational-performance/)

## How to install the models on SQL Server

   > [!NOTE]
   > 
   > If you use the separate Windows-based installer to install Microsoft R Server or to upgrade your instance of SQL Server, the pretrained models are available from the installer. See [Install R Server for Windows](https://docs.microsoft.com/en-us/r-server/install/r-server-install-windows).
   > 
   > Some additional steps might be required to use the models with Microsoft R Server. For more information, see [How to install and deploy pre-trained machine learning models with MicrosoftML](https://docs.microsoft.com/r-server/install/microsoftml-install-pretrained-models)

1. The pretrained models are not installed by default when you install SQL Server; you must add them by running a command-line setup utility after SQL Server setup has finished.

2. Open an elevated command prompt, and navigate to the setup bootstrap folder for SQL Server, which also contains the Microsoft R installer.

    In a default instance of SQL Server 2017 RC 1, this would be:
    
    `C:\Program Files\Microsoft SQL Server\140\Setup Bootstrap\SQL2017RC1\x64\`

3. Specify the component to install, and the folder where the pretrained models should be added, by using the following arguments:

  + To use models with **R_SERVICES**

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir <SQL_DB_instance_folder>\R_SERVICES`

    For example, to enable use of the pretrained models using R in a default instance of SQL Server 2017, you would run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.22 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES"`

  + To use models with **PYTHON_SERVICES**

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir <SQL_DB_instance_folder>\PYTHON_SERVICES`

    For example, to enable use of the pretrained models using Python, for a default instance of SQL Server 2017, you would run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.22 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES"`

4. For the version parameter, the following values are supported:

    + Release candidate 0: **9.1.0.0**
    + Release candidate 1: **9.2.0.22**
    + Final version number (not released): **9.2.0.100**

5. If installation is successful, the following models should be added to your R\_SERVICES or PYTHON\_SERVICES folders:

    - AlexNet_Updated.model
    - ImageNet1K_mean.xml
    - pretrained.model
    - ResNet_101_Updated.model
    - ResNet_18_Updated.model
    - ResNet_50_Updated.model

## Examples

After you have installed the models, you can use the models by calling them from R code.

### Image featurization example

The pretrained model for images supports featurization of images that you supply. To use the model, you call the  **featurizeImage** transform.

+ [featurizeImage: Machine Learning Image Featurization Transform](https://docs.microsoft.com/r-server/r-reference/microsoftml/featurizeimage)

In this example, see the second block of code. The image is loaded, resized, and featurized by the pretrained convolutional DNN model. The output of the DNN featurizer is then used to train a linear model for image classification.

The image must be resized to meet the requirements of the trained model: here the images used for training were 224 x 224 px. If you were using an AlexNet model, the image would be resized to 227 x 227 px.

```R
 model <- rxFastLinear(
     Label ~ Features,
     data = train,
     mlTransforms = list(
         loadImage(vars = list(Features = "Path")),
         resizeImage(vars = "Features", width = 224, height = 224), 
         extractPixels(vars = "Features"),
         featurizeImage(var = "Features")
         ),
     mlTransformVars = "Path")
```

> [!NOTE]
> 
> It is not possible to read or modify the pretrained model itself. This particular model is based on [CNTK](https://docs.microsoft.com/cognitive-toolkit/) model but has been compressed using a native format for performance reasons.

### Text analysis example

This sample demonstrates use of the pretrained model for classification:

[Sentiment Analysis using Text Featurizer](https://github.com/Microsoft/microsoft-r/tree/master/microsoft-ml/Samples/101/BinaryClassification/SimpleSentimentAnalysis)
