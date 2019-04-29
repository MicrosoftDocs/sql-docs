---
title: "Requirements for CLR User-Defined Aggregates | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "aggregate functions [CLR integration]"
  - "user-defined types [CLR integration], user-defined aggregates"
  - "CREATE AGGREGATE statement"
  - "SqlUserDefinedAggregate attribute"
  - "aggregate methods [CLR integration]"
  - "assemblies [CLR integration], user-defined aggregate functions"
  - "custom aggregates [CLR integration]"
  - "user-defined functions [CLR integration]"
  - "UDTs [CLR integration], user-defined aggregates"
ms.assetid: dbf9eb5a-bd99-42f7-b275-556d0def045d
author: rothja
ms.author: jroth
manager: craigg
---
# Requirements for CLR User-Defined Aggregates
  A type in a common language runtime (CLR) assembly can be registered as a user-defined aggregate function, as long as it implements the required aggregation contract. This contract consists of the `SqlUserDefinedAggregate` attribute and the aggregation contract methods. The aggregation contract includes the mechanism to save the intermediate state of the aggregation, and the mechanism to accumulate new values, which consists of four methods: `Init`, `Accumulate`, `Merge`, and `Terminate`. When you have met these requirements, you will be able to take full advantage of user-defined aggregates in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The following sections of this topic provide additional details about how to create and work with user-defined aggregates. For an example, see [Invoking CLR User-Defined Aggregate Functions](clr-user-defined-aggregate-invoking-functions.md).  
  
## SqlUserDefinedAggregate  
 For more information, see [SqlUserDefinedAggregateAttribute](https://go.microsoft.com/fwlink/?LinkId=124626).  
  
## Aggregation Methods  
 The class registered as a user-defined aggregate should support the following instance methods. These are the methods that the query processor uses to compute the aggregation:  
  
|Method|Syntax|Description|  
|------------|------------|-----------------|  
|`Init`|public void Init();|The query processor uses this method to initialize the computation of the aggregation. This method is invoked once for each group that the query processor is aggregating. The query processor may choose to reuse the same instance of the aggregate class for computing aggregates of multiple groups. The `Init` method should perform any clean-up as necessary from previous uses of this instance, and enable it to re-start a new aggregate computation.|  
|`Accumulate`|public void Accumulate ( input-type value[, input-type value, ...]);|One or more parameters representing the parameters of the function. *input_type* should be the managed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type equivalent to the native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type specified by *input_sqltype* in the `CREATE AGGREGATE` statement. For more information, see [Mapping CLR Parameter Data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).<br /><br /> For user-defined types (UDTs), the input-type is the same as the UDT type. The query processor uses this method to accumulate the aggregate values. This is invoked once for each value in the group that is being aggregated. The query processor always calls this only after calling the `Init` method on the given instance of the aggregate-class. The implementation of this method should update the state of the instance to reflect the accumulation of the argument value being passed in.|  
|`Merge`|public void Merge( udagg_class value);|This method can be used to merge another instance of this aggregate class with the current instance. The query processor uses this method to merge multiple partial computations of an aggregation.|  
|`Terminate`|public return_type Terminate();|This method completes the aggregate computation and returns the result of the aggregation. The *return_type* should be a managed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type that is the managed equivalent of *return_sqltype* specified in the `CREATE AGGREGATE` statement. The *return_type* can also be a user-defined type.|  
  
### Table-Valued Parameters  
 Table-valued parameters (TVPs), user-defined table types that are passed into a procedure or function, provide an efficient way to pass multiple rows of data to the server. TVPs provide similar functionality to parameter arrays, but offer greater flexibility and closer integration with [!INCLUDE[tsql](../../includes/tsql-md.md)]. They also provide the potential for better performance. TVPs also help reduce the number of round trips to the server. Instead of sending multiple requests to the server, such as with a list of scalar parameters, data can be sent to the server as a TVP. A user-defined table type cannot be passed as a table-valued parameter to, or be returned from, a managed stored procedure or function executing in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Also, TVPs cannot be used within the scope of a context connection. However, a TVP can be used with SqlClient in managed stored procedures or functions executing in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process, if it is used in a connection that is not a context connection. The connection can be to the same server that is executing the managed procedure or function. For more information about TVPs, see [Use Table-Valued Parameters &#40;Database Engine&#41;](../tables/use-table-valued-parameters-database-engine.md).  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Updated the description of the `Accumulate` method; it now accepts more than one parameter.|  
  
## See Also  
 [CLR User-Defined Types](../clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)   
 [Invoking CLR User-Defined Aggregate Functions](clr-user-defined-aggregate-invoking-functions.md)  
  
  
