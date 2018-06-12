---
title: Install pre-trained machine learning models on SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/31/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Install pre-trained machine learning models on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to add pre-trained machine learning models for sentiment analysis and image featurization to an (In-Database) instance of SQL Server that already has R Services or SQL Server Machine Learning Services installed. 

Pre-trained models exist to help customers who need to perform sentiment analysis or image featurization, but do not have the resources to obtain the large datasets or train a complex model. The Machine Learning Server team created and trained these models to help you get started on text and image processing efficiently. For more information, see the [Resources](#bkmk_resources) section of this article.

For an example of how to use the pre-trained models with SQL Server data, see this blog by the SQL Server Machine Learning team: [Sentiment analysis with Python in SQL Server Machine Learning Services](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2017/11/01/sentiment-analysis-with-python-in-sql-server-machine-learning-services/)

## Pre-trained model availability

Pre-trained models are installed using the installation media of Microsoft Machine Learning Server (or Microsoft R Server, if you are adding models to a SQL Server 2016 installation). You can use the free Developer edition to install the models. There is no extra cost associated with model installation. 

Pre-trained models work with the following products and languages. The Setup program detects language integration, the MicrosoftML or microsoftml library, and then inserts the pre-trained models into the respective library. When model installation is finished, you access the models through library functions.

+ SQL Server 2016 R Services (In-Database) - R only, with the [MicrosoftML library](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package)
+ SQL Server 2016 R Server (Standalone) - R only, with the [MicrosoftML library](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package)
+ SQL Server 2017 Machine Learning Services (In-Database) - R with the [MicrosoftML library](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package), Python with the [microsoftml library](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package)
+ SQL Server 2017 Machine Learning Server (Standalone) - R with the [MicrosoftML library](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package), Python with the [microsoftml library](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package)

The installation process differs slightly depending on your version of SQL Server. See the following sections for instructions for each version.

> [!NOTE]
> It is not possible to read or modify the pre-trained models. They are compressed using a native format to improve performance.

## Obtain files for an offline installation

To install the pre-trained models on a server that does not have internet access, you must download the appropriate installers in advance, and copy the installer to a local folder on the server. 

See this page for the download links for all R Server and Machine Learning Server installers: [Offline install](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-offline)

## Install pre-trained models on SQL Server 2016 R Services

With SQL Server 2016, you can install and use the pre-trained R models only if you first upgrade the machine learning components, in a process called **binding**. 

You do this by running the separate Windows installer for Microsoft R Server or Machine Learning Server on a computer having a SQL Server 2016 R Services installation, and selecting an instance or instances to **bind**. Binding an instance means that the support policy associated with the instance is changed to allow more frequent updates to R. 

1. Launch the separate Windows-based installer for either [R Server](https://docs.microsoft.com/machine-learning-server/rebranding-microsoft-r-server) or [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

2. Select the instance to upgrade, and then select the option to get the pre-trained models.

    For more information, see [Upgrade machine learning components used by SQL Server](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

    If you have previously performed binding to update the R components, and just want to add the pre-trained models, leave all previous selections **as is**, and select the pre-trained models option. 
    
    Do not deselect any previously selected options; if you do so, the installer removes the components.

3. We recommend that you accept default settings for the model locations.

4. Accept all other prompts, including license agreements.

After binding is complete, the R version and libraries associated with the instance are replaced with the newer versions provided in R Server or Machine Learning Server. 

With SQL Server 2016, you must perform some additional steps to register the models with the SQL Server 2016 instance library. Repeat these steps for each instance where you installed the pre-trained models.

1. Open a Windows command prompt **as administrator**.

2. Navigate to the setup bootstrap folder for SQL Server, which also contains the Microsoft R installer. In a default instance of SQL Server 2016, the folder would be:
    
    `C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\SQL2017\x64\`

3. Run `RSetup.exe` and indicate the component to install, the version, and the folder containing the model source files, using this syntax:

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "<SQL_DB_instance_folder>\R_SERVICES\library\MicrosoftML\mxLibs\x64"`. 

    The following values are supported for the version parameter:

    + Release candidate 0: **9.1.0.0**
    + Release candidate 1: **9.2.0.22**
    + RTM: **9.2.0.100**
    + Cumulative Update 1: **9.2.0.24**
    + Cumulative Update 4: **9.3.0**

    > [!NOTE]
    > The corresponding SQL Server releases are listed to give you an idea of the time that the update was published. If you are not sure of the version installed with SQL Server, open the R_SERVICES folder for the instance, and open RGui (`~\R_SERVICES\bin\x64`). The starting screen lists the versions for Microsoft R Open version and Machine Learning Server. 

    For example, to use pre-trained R models with a default instance of **R_SERVICES**, you would run this command:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library\MicrosoftML\mxLibs\x64"`

    On a named instance, the command would be something like this:

    `RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL13.MyInstanceName\R_SERVICES\library\MicrosoftML\mxLibs\x64"`

4. If installation is successful, the following models should be added to your `R_SERVICES` folder:

    + AlexNet\_Updated.model
    + ImageNet1K\_mean.xml
    + pretrained.model
    + ResNet\_101\_Updated.model
    + ResNet\_18\_Updated.model
    + ResNet\_50\_Updated.model

## Install pre-trained models on SQL Server 2017 Machine Learning Services (In-Database)

If you have already installed SQL Server 2017, you can get the pre-trained models in two ways:

+ Install just the pre-trained models
+ Upgrade the Python and R components by using binding, and install the pre-trained models at the same time

### Add pre-trained models only

To add the pre-trained models, you can run RSetup.exe from the command line.

For the R version of the models, install the MLM component to R_SERVICES:

```
RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES"
```

For the Python version of the models, install the MLM component to PYTHON_SERVICES:

```
RSetup.exe /install /component MLM /version 9.2.0.24 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES"
```

### Bind and install pre-trained models

The following instructions describe the process for upgrading the machine learning components and getting the pre-trained models at the same time.

1. Run the Windows-based installer for Machine Learning Server. You can download the installer from the links on this page: [Install machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

2. If you are installing the models to a server that does not have internet access, make sure to also download the installer for the pre-trained models from this page: [Offline install for Machine Learning Server](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-offline).

3. Run the installer.

4. To upgrade the R or Python components at the same time, select the language (R, or Python, or both) that you want to update.

    If you have previously installed Machine Learning Server and updated R or Python components using the binding option, leave all previous selections **as is**, and select the pre-trained models options. Do not deselect any previously selected options; if you do so, the installer removes the components.

5. Accept all other prompts, including license agreements.

With SQL Server 2017, no additional configuration is required.

> [!NOTE]
> 
> For Python models, you might get an error when calling the model file from Python code. This is due to a limitation in the current Python implementation, which limit the length of the path to the model file. This issue has been fixed and will be available in an upcoming service release.

## Install pre-trained models with SQL Server standalone R server

If you installed an early version of R Server (Standalone) using SQL Server 2016 setup, you can add the ability to use the pre-trained models by upgrading R Server using the newer Windows-based installer. 

The pre-trained models first became available as an option with [Microsoft R Server 9.1](https://blogs.technet.microsoft.com/dataplatforminsider/2017/04/19/introducing-microsoft-r-server-9-1-release/), but upgrades have been added with each release. We recommend getting the latest version possible, but previous releases are listed here [R Server releases](https://docs.microsoft.com/machine-learning-server/install/r-server-install)

The following instructions describe how to upgrade the R components, and at the same time add the pre-trained models.

1. Launch the separate Windows-based installer for either [R Server](https://docs.microsoft.com/machine-learning-server/rebranding-microsoft-r-server) or [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install).

2. Select the languages that you wish to update, and select the **Pre-trained Models** option.

    > [!TIP]
    > If you have previously run the installer to update R Server (Standalone), and just want to add the pre-trained models, leave all previous selections **as is**, and select just the Pre**-trained Models** option. **Do not** deselect any previously selected options; if you do so, the installer removes the components.

    We recommend that you accept default settings for the model locations.

3. Click **Continue**. 

4. Accept all other prompts, including license agreements.

After installation is complete, you must perform some additional steps to register the pre-trained models.

1. Open a Windows command prompt **as administrator**.

2. Navigate to the setup bootstrap folder for R Server (Standalone), which also contains the Microsoft R installer. 

3. Run `RSetup.exe` and indicate the component to install, the version, and the folder containing the model source files, using this syntax:

    `RSetup.exe /install /component MLM /version <version> /language 1033 /destdir "~\R_SERVER\library\MicrosoftML\mxLibs\x64"`

    The following values are supported for the version parameter:

    + Release candidate 0: **9.1.0.0**
    + Release candidate 1: **9.2.0.22**
    + RTM: **9.2.0.100**
    + Cumulative Update 1: **9.2.0.24**
    + Cumulative Update 4: **9.3.0**

    For example, to enable use of the latest version of the pre-trained models for R, in a default installation of R Server (Standalone), you would run this statement:

    `RSetup.exe /install /component MLM /version 9.3.0 /language 1033 /destdir "C:\Program Files\Microsoft SQL Server\130\R_SERVER\library\MicrosoftML\mxLibs\"`

## Install pre-trained models with SQL Server 2017 standalone server

If you installed Machine Learning Server using SQL Server 2017 setup, you add the pre-trained models by running the Windows-based installer. You can select the option to upgrade the R or Python components, and at the same time add the pre-trained models. 

No additional configuration is required after installation is complete.

## <a name="bkmk_resources"></a> Research and resources

Currently the models that are available are deep neural network (DNN) models for sentiment analysis and image classification. All pre-trained models were trained by using Microsoft's [Computation Network Toolkit](https://cntk.ai/Features/Index.html), or **CNTK**.

The configuration of each network was based on the following reference implementations:

+ ResNet-18
+ ResNet-50
+ ResNet-101
+ AlexNet

For more information about the algorithms used in these deep learning models, and how they are implemented and trained using CNTK, see these articles:

+ [Microsoft Researchers’ Algorithm Sets ImageNet Challenge Milestone](https://www.microsoft.com/research/blog/microsoft-researchers-algorithm-sets-imagenet-challenge-milestone/)

+ [Microsoft Computational Network Toolkit offers most efficient distributed deep learning computational performance](https://www.microsoft.com/research/blog/microsoft-computational-network-toolkit-offers-most-efficient-distributed-deep-learning-computational-performance/)

## How to use pre-trained models for text analysis

See the following sample for a demonstration of how to use the pre-trained text featurization model for text classification:

[Sentiment Analysis using Text Featurizer](https://github.com/Microsoft/microsoft-r/tree/master/microsoft-ml/Samples/101/BinaryClassification/SimpleSentimentAnalysis)

## How to use pre-trained models for image detection

The pre-trained model for images supports featurization of images that you supply. To use the model, you call the  **featurizeImage** transform. The image is loaded, resized, and featurized by the trained model. The output of the DNN featurizer is then used to train a linear model for image classification.

To use this model, all images must be resized to meet the requirements of the trained model. For example, if you use an AlexNet model, the image should be resized to 227 x 227 px.

For more information, see [Pre-trained models for sentiment analysis and image detection](https://docs.microsoft.com/machine-learning-server/install/microsoftml-install-pretrained-models)
