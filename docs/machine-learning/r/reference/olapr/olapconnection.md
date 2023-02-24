---
title: "OlapConnection function (olapR)"
description: "OlapConnection constructs a OlapConnection object."
author: "rothja"
ms.author: "jroth"
ms.date: 07/15/2019
ms.service: sql
ms.subservice: machine-learning
ms.topic: "reference"
ms.custom: ""
keywords: (olapR), OlapConnection
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---


# OlapConnection: olapR OlapConnection Creation 



`OlapConnection` constructs a "OlapConnection" object.



## Usage

```   
  OlapConnection(connectionString="Data Source=localhost; Provider=MSOLAP;")

  is.OlapConnection(ocs)

  print.OlapConnection(ocs)

```


## Arguments



### `connectionString`
 A valid connection string for connecting to Analysis Services 


### `ocs`
 An object of class "OlapConnection" 




## Details

`OlapConnection` validates and holds an Analysis Services connection string. By default, Analysis Services returns the first cube of the first database. To connect to a specific database, use the Initial Catalog parameter.



## Value

`OlapConnection` returns an object of type "OlapConnection". A warning is shown if the connection string is invalid.


## References
  For more information on Analysis Services connection strings, see [Connection string properties](/analysis-services/instances/connection-string-properties-analysis-services).



## See also

[Query](Query.md), [executeMD](ExecuteMD.md), [execute2D](Execute2D.md), [explore](Explore.md)


## Examples

 ```


  # Create the connection string. For a named instance, escape the instance name: localhost\my-other-instance
  cnnstr <- "Data Source=localhost; Provider=MSOLAP; initial catalog=AdventureWorksCube"
  olapCnn <- OlapConnection(cnnstr)
```

