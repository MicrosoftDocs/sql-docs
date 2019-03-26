---
title: "Balanced Data Distributor Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.balanceddatadistributor.f1"
ms.assetid: ae0b33dd-f44b-42df-b6f6-69861770ce10
author: janinezhang
ms.author: janinez
manager: craigg
---
# Balanced Data Distributor Transformation
  The Balanced Data Distributor (BDD) transformation takes advantage of concurrent processing capability of modern CPUs. It distributes buffers of incoming rows uniformly across outputs on separate threads. By using separate threads for each output path, the BDD component improves the performance of an SSIS package on multi-core or multi-processor machines.  
  
 The following diagram shows a simple example of using the BDD transformation. In this example, the BDD transformation picks one pipeline buffer at a time from the input data from a flat file source and sends it down one of the three output paths in a round robin fashion. In SQL Server Data Tools, you can check the values of a <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.MainPipeClass.DefaultBufferSize%2A>(default size of the pipeline buffer) and <xref:Microsoft.SqlServer.Dts.Pipeline.Wrapper.MainPipeClass.DefaultBufferMaxRows%2A>(default maximum number of rows in a pipeline buffer) in the **Properties** window displaying properties of a data flow task.  
  
 ![Balanced Data Distributor](../../../integration-services/data-flow/transformations/media/balanceddatadistributor.JPG "Balanced Data Distributor")  
  
 The Balanced Data Distributor transformation helps improve performance of a package in a scenario that satisfies the following conditions:  
  
1.  There is large amount of data coming into the BDD transformation. If the data size is small and only one buffer can hold the data, there is no point in using the BDD transformation. If the data size is large and several buffers are required to hold the data, BDD can efficiently process buffers of data in parallel by using separate threads.  
  
2.  The data can be read faster than the rest of the data flow can process it. In this scenario, the transformations that are performed on the data run slowly compared to the rate at which data is coming. If the bottleneck is at the destination, the destination must be parallelizable though.  
  
3.  The data does not need to be ordered. For example, if the data needs to stay sorted, you should not split the data using the BDD transformation.  
  
 Note that if the bottleneck in an SSIS package is due to the rate at which data can be read from the source, the BDD component does not help improve the performance. If the bottleneck in an SSIS package is because the destination does not support parallelism, the BDD does not help; however, you can perform all the transformations in parallel and use the Union All transformation to combine the output data coming out of different output paths of the BDD transformation before sending the data to the destination.  
  
> [!IMPORTANT]  
>  See the [Balanced Data Distributor video](https://go.microsoft.com/fwlink/?LinkID=226278) on the TechNet Library for a presentation with a demo on using the transformation.  
  
  
