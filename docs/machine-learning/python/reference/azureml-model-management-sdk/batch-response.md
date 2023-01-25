--- 
 
# required metadata 
title: "BatchResponse class" 
description: "The BatchResponse class is for SQL Machine Learning Services and Machine Learning Server for managing web services." 
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

# Class BatchResponse


## BatchResponse



```
azureml.deploy.server.service.BatchResponse(api, execution_id, response,
    output_schema)
```




Represents a service's entire batch execution response at a particular state
in time. Using this, a batch execution index can be supplied to the
`execution(index)` function in order to retrieve the service's
[`ServiceResponse`](service-response.md).



## api

```python
api
```




Gets the api endpoint.



## completed_item_count

```python
completed_item_count
```




Returns the number of completed batch results processed thus far.



## execution

```python
execution(index)
```




Extracts the service execution results within the batch at this
execution *index*.


### Arguments


### index

The batch execution index.


### Returns

The execution results [`ServiceResponse`](service-response.md).



## execution_id

```python
execution_id
```




Returns this batch's execution identifier if a batch has been started,
otherwise `None`.



## total_item_count

```python
total_item_count
```




Returns the total number of batch results processed in any state.
