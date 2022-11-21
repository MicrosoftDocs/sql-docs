--- 
 
# required metadata 
title: "Batch class"
description: "The Batch class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
keywords: "" 
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
#ms.subservice: "" 
ms.custom: ""
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15"
 
---

# Class Batch


## Batch



```
azureml.deploy.server.service.Batch(service, records=[], parallel_count=10,
    execution_id=None)
```




Manager of a service's batch execution lifecycle.



## api

```python
api
```




Gets the api endpoint.



## execution_id

```python
execution_id
```




Gets this batch's execution identifier if currently started, otherwise
`None`.



## parallel_count

```python
parallel_count
```




Gets this batch's parallel count of threads.



```
records
```




Gets the batch input records.



```
results(show_partial_results=True)
```




Poll for batch results.


### Arguments


### show_partial_results

To get partial execution results or not.
The default is to include partial results.


### Returns

An instance of [`BatchResponse`](batch-response.md).



## start

```python
start()
```




Starts a batch execution for this service.


### Returns

An instance of itself [`Batch`](Batch.md).

## artifacts

```python
artifact(index, file_name)
```

Get the file artifact for this service batch execution *index*.

### Arguments

### index

Batch execution index.

### file_name

Artifact filename

### Returns

A single file artifact.

## cancel

```python
cancel()
```

Cancel this batch execution.

## download



```
download(index, file_name=None, destination=cwd())
```


Download the file artifact to file-system in the *destination*.

### Arguments

### index

Batch execution index.

### file_name

The file artifact name.

### destination

Download location.

### Returns

A *list* of downloaded file-paths.

## list_artifacts

```python
list_artifacts(index)
```

List the file artifact names belonging to this service batch execution
*index*.

### Arguments

### index

Batch execution index.

### Returns

A *list* of file artifact names.

Gets this batch's parallel count of threads.

## records

```python
records
```

Gets the batch input records.

## results

```python
results(show_partial_results=True)
```

Poll batch results.

### Arguments

### show_partial_results

To get partial execution results or not.

### Returns

An execution Self [*BatchResponse*](batch-response.md).
