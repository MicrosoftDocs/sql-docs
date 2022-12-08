---

# required metadata
title: "Deploy & manage Machine Learning Server web services in Python"
description: "This class is for SQL Machine Learning Services and Machine Learning Server for managing web services."
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: 2/16/2018
ms.topic: "reference"
ms.service: sql
ms.subservice: "machine-learning-services"

# optional metadata
#ROBOTS: ""
#audience: ""
#ms.devlang: ""
#ms.reviewer: ""
#ms.suite: ""
#ms.tgt_pltfrm: ""
#ms.subservice: ""
#ms.custom: ""
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
---

# Machine Learning Server: manage web services with azureml-model-management-sdk
 
**Applies to:  Machine Learning Server, SQL Server 2017**

'azureml-model-management-sdk' is a custom Python package developed by Microsoft. This package provides the classes and functions to deploy and interact with analytic web services. These web services are backed by code block and scripts in Python or R.  

This topic is a high-level description of package functionality. These classes and functions can be called directly. For syntax and other details, see the individual function help topics in the table of contents.

<br/>

| Package details | Information |
|--------|-|
| Current version: |  1.0.1b7 |
| Built on: | [Anaconda](https://www.anaconda.com/) distribution of [Python 3.5](https://www.python.org/doc) |
| Package distribution: | [Machine Learning Server 9.x](/machine-learning-server/what-is-machine-learning-server) </br>[SQL Server 2017 Machine Learning Server (Standalone)](../../../r/r-server-standalone.md) |



## How to use this package

The **azureml-model-management-sdk** package is installed as part of Machine Learning Server and SQL Server 2017 Machine Learning Server (Standalone) when you add Python to your installation. It is also [available locally on Windows](/machine-learning-server/install/python-libraries-interpreter).  When you install these products, you get the full collection of proprietary packages plus a Python distribution with its modules and interpreters. 

You can use any Python IDE to write Python scripts that call the classes and functions in **azureml-model-management-sdk**. However, the script must run on a computer having Machine Learning Server or SQL Server 2017 Machine Learning Server (Standalone) with Python.

## Use cases

There are three primary use cases for this release: 

+ Adding authentication logic to your Python script
+ Deploying standard or real-time Python web services
+ Managing and consuming these web services

## Main classes and functions

* [DeployClient](deploy-client.md) 

* [MLServer](mlserver.md) 

* [Operationalization](operationalization.md) 

* [OperationalizationDefinition](operationalization-definition.md) 

* [ServiceDefinition](service-definition.md) 

* [RealtimeDefinition](realtime-definition.md) 

* [Service](service.md) 

* [ServiceResponse](service-response.md) 

* [Batch](batch.md) 

* [BatchResponse](batch-response.md) 




## Next steps

Add both Python modules to your computer by running setup: 

+ Set up [Machine Learning Server](/machine-learning-server/install/machine-learning-server-install) for Python or [Python Machine Learning Services](../../../install/sql-machine-learning-services-windows-install.md).

Next, follow this quickstart to try it yourself:

+ [Quickstart: How to deploy Python model as a service](/machine-learning-server/operationalize/python/quickstart-deploy-python-web-service) 

Or, read this how-to article:
+ [How to publish and manage web services in Python](/machine-learning-server/operationalize/python/how-to-deploy-manage-web-services)


## See also

+ [Library Reference](/machine-learning-server/python-reference/introducing-python-package-reference)

+ [Install Machine Learning Server](/machine-learning-server/what-is-machine-learning-server)

+ [Install the Python interpreter and libraries on Windows](/machine-learning-server/install/python-libraries-interpreter)

+ [How to authenticate in Python with this package](/machine-learning-server/operationalize/python/how-to-authenticate-in-python)

+ [How to list, get, and consume services in Python with this package](/machine-learning-server/operationalize/python/how-to-consume-web-services)
