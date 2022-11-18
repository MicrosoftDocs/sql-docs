--- 
 
# required metadata 
title: "Service class"
description: "The Service class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
keywords: "" 
author: WilliamDAssafMSFT
ms.author: wiassaf 
ms.date: "09/20/2017" 
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
#ms.subservice: "" 
ms.custom: ""
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# Class Service


## Service



```
azureml.deploy.server.service.Service(service, http_client)
```




Dynamic object for service consumption and batching based on service
metadata attributes.



## batch

```
batch(records, parallel_count=10)
```




Register a set of input records for batch execution on this service.


### Arguments


### records

The *data.frame* or *list* of
input records to execute.


### parallel_count

Number of threads used to process entries in
the batch. Default value is 10. Please make sure not to use too
high of a number because it might negatively impact performance.


### Returns

The [`Batch`](batch.md) instance to control this service's
batching lifecycle.



## capabilities

```python
capabilities()
```




Provides the following information describing the holdings of this
service:

* *api* -  The API REST endpoint. 

* *name* - The service name. 

* *version* - The service version. 

* *published_by* - The service publishing author. 

* *runtime* - The service runtime context _R|Python_. 

* *description* - The service description. 

* *creation_time* - The service publish timestamp. 

* *snapshot_id* - The snapshot identifier this service is bound with. 

* *inputs* - The input schema name/type definition. 

* *outputs* - The output schema name/type definition. 

* *inputs_encoded* - The input schema name/type encoded to python. 

* *outputs_encoded* - The output schema name/type encoded to python. 

* *artifacts* - The supported generated files. 

* *operation_id* - The function `alias`. 

* *swagger* - The API REST endpoint to this service's *swagger.json* document. 


### Returns

A `dict` of key/values describing the service.



## get_batch

```python
get_batch(execution_id)
```




Retrieves the service batch based on an execution identifier.


### Arguments


### execution_id

The identifier of the batch execution.


### Returns

The [`Batch`](batch.md) instance to control this service's
batching lifecycle.



## list_executions

```python
list_batch_executions()
```




Gets all batch execution identifiers currently queued for this service.


### Returns

A `list` of execution identifiers.



## swagger

```python
swagger()
```




Retrieves the *swagger.json* for this service (see [http://swagger.io/](http://swagger.io/)).


### Returns

The swagger document for this service as a json `str`.
