---
title: Install pre-trained models
description: Add pre-trained models for sentiment analysis and image featurization to SQL Server Machine Learning Services (R or Python) or SQL Server R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 05/24/2022
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
- seo-lt-2019
- intro-installation
- event-tier1-build-2022
monikerRange: "=sql-server-2016||=sql-server-2017||=sql-server-ver15||=sql-server-linux-ver15"
---
# Install pre-trained machine learning models on SQL Server

[!INCLUDE [SQL Server 2016 2017 2019](../../includes/applies-to-version/sqlserver2016-2019-only.md)]

This article applies to [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]. 

This article explains how to use PowerShell to add free pre-trained machine learning models for *sentiment analysis* and *image featurization* to a SQL Server instance having R or Python integration. The pre-trained models are built by Microsoft and ready-to-use, added to an instance as a post-install task. For more information about these models, see the [Resources](#bkmk_resources) section of this article.

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], runtimes for R, Python, and Java, are no longer installed with SQL Setup. Instead, install your desired R and/or Python custom runtime(s) and packages. For more information, see [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install-sql-2022.md).

Once installed, the pre-trained models are considered an implementation detail that power specific functions in the MicrosoftML (R) and microsoftml (Python) libraries. You should not (and cannot) view, customize, or retrain the models, nor can you treat them as an independent resource in custom code or paired other functions. 

To use the pretrained models, call the functions listed in the following table.

| R function (MicrosoftML) | Python function (microsoftml) | Usage |
|--------------------------|-------------------------------|-------|
| [getSentiment](../r/reference/microsoftml/getsentiment.md) | [get_sentiment](../python/reference/microsoftml/get-sentiment.md) | Generates  positive-negative sentiment score over text inputs. |
| [featurizeImage](../r/reference/microsoftml/featurizeimage.md) | [featurize_image](../python/reference/microsoftml/featurize-image.md) | Extracts text information from image file inputs. |

## Prerequisites

Machine learning algorithms are computationally intensive. We recommend 16 GB RAM for low-to-moderate workloads, including completion of the tutorial walkthroughs using all of the sample data.

You must have administrator rights on the computer and SQL Server to add pre-trained models.

External scripts must be enabled and SQL Server LaunchPad service must be running. Installation instructions provide the steps for enabling and verifying these capabilities. 

Download and install the latest cumulative update for your version of SQL Server. See the [Latest updates for Microsoft SQL Server](../../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md).

::: moniker range=">=sql-server-2017"
[MicrosoftML R package](../r/ref-r-microsoftml.md) or [microsoftml Python package](../python/ref-py-microsoftml.md) contain the pre-trained models.

[SQL Server Machine Learning Services](sql-machine-learning-services-windows-install.md) includes both language versions of the machine learning library, so this prerequisite is met with no further action on your part. Because the libraries are present, you can use the PowerShell script described in this article to add the pre-trained models to these libraries.
::: moniker-end

::: moniker range="=sql-server-2016"
[MicrosoftML R package](../r/ref-r-microsoftml.md) contain the pre-trained models.

[SQL Server R Services](sql-r-services-windows-install.md), which is R only, does not include [MicrosoftML package](../r/ref-r-microsoftml.md) out of the box. To add MicrosoftML, you must do a [component upgrade](../install/upgrade-r-and-python.md). One advantage of the component upgrade is that you can simultaneously add the pre-trained models, which makes running the PowerShell script unnecessary. However, if you already upgraded but missed adding the pre-trained models the first time around, you can run the PowerShell script as described in this article. It works for both versions of SQL Server. Before you do, confirm that the MicrosoftML library exists at `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library`.
::: moniker-end

<a name="file-location"></a>

## Check whether pre-trained models are installed

The install paths for R and Python models are as follows:

+ For R: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\MicrosoftML\mxLibs\x64`

+ For Python: `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Lib\site-packages\microsoftml\mxLibs`

Model file names are listed below:

+ AlexNet\_Updated.model
+ ImageNet1K\_mean.xml
+ pretrained.model
+ ResNet\_101\_Updated.model
+ ResNet\_18\_Updated.model
+ ResNet\_50\_Updated.model

If the models are already installed, skip ahead to the [validation step](#verify) to confirm availability.

## Download the installation script

Visit [https://aka.ms/mlm4sql](https://aka.ms/mlm4sql) to download the file **Install-MLModels.ps1**.

## Execute with elevated privileges

1. Start PowerShell. On the task bar, right-click the PowerShell program icon and select **Run as administrator**.
2. The recommended execution policy during installation is "RemoteSigned". For more information on setting the PowerShell execution policy, see [Set-ExecutionPolicy](/powershell/module/microsoft.powershell.security/set-executionpolicy). For example:

   ```powershell
   Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
   ```

3. Enter a fully-qualified path to the installation script file and include the instance name. Assuming the Downloads folder and a default instance, the command might look like this:

   ```powershell
   PS C:\WINDOWS\system32> C:\Users\<user-name>\Downloads\Install-MLModels.ps1 MSSQLSERVER
   ```

**Output**

On an internet-connected SQL Server Machine Learning Services default instance with R and Python, you should see messages similar to the following.

   ```powershell
   MSSQL14.MSSQLSERVER
        Verifying R models [9.2.0.24]
        Downloading R models [C:\Users\<user-name>\AppData\Local\Temp]
        Installing R models [C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\]
        Verifying Python models [9.2.0.24]
        Installing Python models [C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\]
   PS C:\WINDOWS\system32>
   ```

<a name="verify"> </a>

## Verify installation

First, check for the new files in the [mxlibs folder](#file-location). Next, run demo code to confirm the models are installed and functional. 

### R verification steps

1. Start **RGUI.EXE** at C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64.

2. Paste in the following R script at the command prompt.

    ```R
    # Create the data
    CustomerReviews <- data.frame(Review = c(
    "I really did not like the taste of it",
    "It was surprisingly quite good!",
    "I will never ever ever go to that place again!!"),
    stringsAsFactors = FALSE)

    # Get the sentiment scores
    sentimentScores <- rxFeaturize(data = CustomerReviews, 
                                    mlTransforms = getSentiment(vars = list(SentimentScore = "Review")))

    # Let's translate the score to something more meaningful
    sentimentScores$PredictedRating <- ifelse(sentimentScores$SentimentScore > 0.6, 
                                            "AWESOMENESS", "BLAH")

    # Let's look at the results
    sentimentScores
    ```

3. Press Enter to view the sentiment scores. Output should be as follows:

    ```R
    > sentimentScores
                                            Review SentimentScore
    1           I really did not like the taste of it      0.4617899
    2                 It was surprisingly quite good!      0.9601924
    3 I will never ever ever go to that place again!!      0.3103435
    PredictedRating
    1            BLAH
    2     AWESOMENESS
    3            BLAH
    ```

### Python verification steps

1. Start **Python.exe** at C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES.

2. Paste in the following Python script at the command prompt

    ```python
    import numpy
    import pandas
    from microsoftml import rx_logistic_regression, rx_featurize, rx_predict, get_sentiment

    # Create the data
    customer_reviews = pandas.DataFrame(data=dict(review=[
                "I really did not like the taste of it",
                "It was surprisingly quite good!",
                "I will never ever ever go to that place again!!"]))
                
    # Get the sentiment scores
    sentiment_scores = rx_featurize(
        data=customer_reviews,
        ml_transforms=[get_sentiment(cols=dict(scores="review"))])
        
    # Let's translate the score to something more meaningful
    sentiment_scores["eval"] = sentiment_scores.scores.apply(
                lambda score: "AWESOMENESS" if score > 0.6 else "BLAH")
    print(sentiment_scores)
    ```

3. Press Enter to print the scores. Output should be as follows:

    ```python
    >>> print(sentiment_scores)
                                                review    scores         eval
    0            I really did not like the taste of it  0.461790         BLAH
    1                  It was surprisingly quite good!  0.960192  AWESOMENESS
    2  I will never ever ever go to that place again!!  0.310344         BLAH
    >>>
    ```

> [!NOTE]
> If demo scripts fail, check the file location first. On systems having multiple instances of SQL Server, or for instances that run side-by-side with standalone versions, it's possible for the installation script to mis-read the environment and place the files in the wrong location. Usually, manually copying the files to the correct mxlib folder fixes the problem.

## Examples using pre-trained models

The following link include example code invoking the pretrained models.

+ [Code sample: Sentiment Analysis using Text Featurizer](https://github.com/Microsoft/microsoft-r/tree/master/microsoft-ml/Samples/101/BinaryClassification/SimpleSentimentAnalysis)

<a name="bkmk_resources"></a> 

## Research and resources

Currently the models that are available are deep neural network (DNN) models for sentiment analysis and image classification. All pre-trained models were trained by using Microsoft's [Computation Network Toolkit](https://cntk.ai/Features/Index.html), or **CNTK**.

The configuration of each network was based on the following reference implementations:

+ ResNet-18
+ ResNet-50
+ ResNet-101
+ AlexNet

For more information about the algorithms used in these deep learning models, and how they are implemented and trained using CNTK, see these articles:

+ [Microsoft Researchers' Algorithm Sets ImageNet Challenge Milestone](https://www.microsoft.com/research/blog/microsoft-researchers-algorithm-sets-imagenet-challenge-milestone/)

+ [Microsoft Computational Network Toolkit offers most efficient distributed deep learning computational performance](https://www.microsoft.com/research/blog/microsoft-computational-network-toolkit-offers-most-efficient-distributed-deep-learning-computational-performance/)

## See also

+ [SQL Server Machine Learning Services](sql-machine-learning-services-windows-install.md)
+ [Upgrade R and Python components in SQL Server instances](../install/upgrade-r-and-python.md)
+ [MicrosoftML package for R](../r/ref-r-microsoftml.md)
+ [microsoftml package for Python](../python/ref-py-microsoftml.md)
