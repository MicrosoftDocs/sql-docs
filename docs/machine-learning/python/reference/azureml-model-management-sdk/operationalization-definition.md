---
title: "OperationalizationDefinition class"
description: "The OperationalizationDefinition class is for SQL Machine Learning Services and Machine Learning Server for managing web services."
author: VanMSFT
ms.author: vanto
ms.date: 07/15/2019
ms.service: sql
ms.subservice: "machine-learning-services"
ms.topic: "reference"
ms.devlang: python
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
---
# Class OperationalizationDefinition


## OperationalizationDefinition



```
azureml.deploy.operationalization.OperationalizationDefinition(name, op,
    defs_extent={})
```




Base abstract class defining a service's properties.

Create a new publish definition.


### Arguments


### name

The web service name.


### op

A reference to the deploy client instance.


### defs_extent

A mixin of subclass specific definitions.

```python
alias(alias)
```




Set the optional service function name alias to use in order to consume
the service.

**Example:**



```
service = client.service('score-service').alias('score').deploy()

# `score()` is the function that will call the `score-service`
result = service.score()
```



### Arguments


### alias

The service function name alias to use in order to consume
the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.



## deploy

```python
deploy()
```




Bundle up the definition properties and publish the service.

To be implemented by subclasses.


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.



## description

```python
description(description)
```




Set the service's optional description.


### Arguments


### description

The description of the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.



## redeploy

```python
redeploy(force=False)
```




Bundle up the definition properties and update the service.

To be implemented by subclasses.


### Returns

A new instance of [`Service`](service.md) representing the
service *deployed*.



## version

```python
version(version)
```




Set the service's optional version.


### Arguments


### version

The version of the service.


### Returns

Self [`OperationalizationDefinition`](operationalization-definition.md) for fluent API.
