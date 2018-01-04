---
title: "Install pretrained machine learning models on SQL Server | Microsoft Docs"
ms.date: "11/16/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 21456462-e58a-44c3-9d3a-68b4263575d7
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Install pretrained machine learning models on SQL Server

This article describes how to add pretrained models to an instance of SQL Server that already has R Services or Machine Learning Services installed.

The option to install pretrained models is available when you use the separate Windows installer for Microsoft R Server or Machine Learning Server. You can use this installer to get just the pretrained models, or you can use it to [upgrade the machine learning components](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md) in an instance of SQL Server 2016 or SQL Server 2017.

After you have downloaded the pretrained models by running the installer, there are some additional steps to configure the models for use with SQL Server. This article describes the process.

For an example of how to use the pretrained models with SQL Server data, see this blog by the SQL Server Machine Learning team: 

+ [Sentiment analysis with Python in SQL Server Machine Learning Services](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/11/01/sentiment-analysis-with-python-in-sql-server-machine-learning-services/)

## Benefits of using pretrained models

These pre-trained models were created to help customers who need to perform tasks such as sentiment analysis or image featurization, but who do not have the resources to obtain the large datasets or train a complex model. Using pre-trained models lets you get started on text and image processing efficiently.

Currently the models that are available are deep neural network (DNN) models for sentiment analysis and image classification. All pretrained models were trained by using Microsoft's [Computation Network Toolkit](https://cntk.ai/Features/Index.html), or **CNTK**.

The configuration of each network was based on the following reference implementations:

+ ResNet-18
+ ResNet-50
+ ResNet-101
+ AlexNet

For more information about these models, see [Pre-trained machine learning models for sentiment analysis and image detection](https://docs.microsoft.com/machine-learning-server/install/microsoftml-install-pretrained-models)

For more information about deep learning networks and their implementation using CNTK, see these articles:

+ [Microsoft Researchers’ Algorithm Sets ImageNet Challenge Milestone](https://www.microsoft.com/research/blog/microsoft-researchers-algorithm-sets-imagenet-challenge-milestone/)

+ [Microsoft Computational Network Toolkit offers most efficient distributed deep learning computational performance](https://www.microsoft.com/research/blog/microsoft-computational-network-toolkit-offers-most-efficient-distributed-deep-learning-computational-performance/)

## How to install the models on SQL Server

1. Run the separate Windows-based installer for Machine Learning Server. For download locations, see:

    + [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)
    + [Install R Server 9.1 for Windows](https://docs.microsoft.com/r-server/install/r-server-install-windows)

2. The selection of features to install depends on whether you are getting just the models, or performing other updates using the installer.
 
    + If this is a new installation of Machine Learning Server, and you do not want to make other changes to R or Python components, select **only** the pretrained models option. Accept all other prompts, including license agreements.

    + To upgrade the R or Python components at the same time, select the language (R, or Python, or both) that you want to update, and select the pretrained models option. Select one or more instances to apply these changes to.

    + If you have previously installed Machine Learning Server and updated R or Python components using the binding option, leave all previous selections **as is**, and select the pretrained models options. Do not deselect any previously selected options; if you do so, the installer removes the components.

3. When installation is complete, open a Windows command prompt **as administrator**, and navigate to the setup bootstrap folder for SQL Server, which also contains the Microsoft R installer. In a default instance of SQL Server 2017, the folder would be:
    
    `C:\Program Files\Microsoft SQL Server\140\Setup Bootstrap\SQL2017\x64\`

4. Run RSetup.exe and indicate the component to install, the version, and the folder containing the model source files, using the command-line arguments shown in these examples:

    + To use models with **R_SERVICES**:

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "<SQL_DB_instance_folder>\R_SERVICES\library\MicrosoftML\mxLibs\x64"`

    For example, to enable use of the latest version of the pretrained models for R, in a default instance of SQL Server 2017, you would run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\MicrosoftML\mxLibs\x64"`

    On a named instance, the command would be something like this:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MyInstanceName\R_SERVICES\library\MicrosoftML\mxLibs\x64"`

    + To use pretrained models with R Server (Standalone) or Machine Learning Server (Standalone):

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "~\R_SERVER\library\MicrosoftML\mxLibs\x64"`

    For example, to enable use of the latest version of the pretrained models for R, in a default installation of R Server with SQL Server 2016, you would run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir ‘C:\Program Files\Microsoft SQL Server\130\R_SERVER\library\MicrosoftML\mxLibs\"`
    
    + To use pretrained models with **PYTHON_SERVICES**:

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "<SQL_DB_instance_folder>\PYTHON_SERVICES\library\MicrosoftML\mxLibs\x64"`

    For example, to enable use of the latest version of the pretrained models for Python, in a default instance of SQL Server 2017, you would run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\library\MicrosoftML\mxLibs\x64"`

    On a named instance, the command would be something like this:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MyInstanceName\PYTHON_SERVICES\library\MicrosoftML\mxLibs\x64"`

    + To use Python pretrained models with Machine Learning Server (Standalone):

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "<sql_folder>\PYTHON_SERVER\site-packages\microsoftml\mxLibs"`

    For example, assuming a default installation of Machine Learning Server (Standalone) using SQL Server 2017 setup, run this statement:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER\Lib\site-packages\microsoftml\mxLibs"`

5. The following values are supported for the version parameter:

    + Release candidate 0: **9.1.0.0**
    + Release candidate 1: **9.2.0.22**
    + RTM: **9.2.0.100**
    + Cumulative Update 1: **9.2.0.24**

6. If installation is successful, the following models should be added to your R\_SERVICES or PYTHON\_SERVICES folders:

    - AlexNet\_Updated.model
    - ImageNet1K\_mean.xml
    - pretrained.model
    - ResNet\_101\_Updated.model
    - ResNet\_18\_Updated.model
    - ResNet\_50\_Updated.model


> [!NOTE]
> 
> If the path to the model file is too long, you might get an error when calling the model file from Python code. This is due to a limitation in the current Python implementation. This issue will be fixed in an upcoming service release.

## Examples

After you have installed the models, you can use the models by calling them from your code.

### Image featurization example

The pretrained model for images supports featurization of images that you supply. This particular model was trained by using [CNTK](https://docs.microsoft.com/cognitive-toolkit/). 

To use the model, you call the  **featurizeImage** transform.

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
> It is not possible to read or modify the pretrained models, because they are compressed using a native format, to improve performance.

### Text analysis example

See the following sample for a demonstration of how to use the pretrained text featurization model for text classification:

[Sentiment Analysis using Text Featurizer](https://github.com/Microsoft/microsoft-r/tree/master/microsoft-ml/Samples/101/BinaryClassification/SimpleSentimentAnalysis)
