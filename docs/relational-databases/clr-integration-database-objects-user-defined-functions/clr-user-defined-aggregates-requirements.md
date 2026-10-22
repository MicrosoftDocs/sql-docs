---
title: "Requirements for CLR User-Defined Aggregates"
description: SQL Server CLR integration allows you to create custom aggregate functions in managed code. They must implement the required aggregation contract.
author: uc-msft
ms.author: umajay
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
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
---
# Requirements for CLR user-defined aggregates

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

A type in a common language runtime (CLR) assembly can be registered as a user-defined aggregate function, as long as it implements the required aggregation contract. This contract consists of the `SqlUserDefinedAggregate` attribute and the aggregation contract methods. The aggregation contract includes the mechanism to save the intermediate state of the aggregation, and the mechanism to accumulate new values, which consist of four methods: `Init`, `Accumulate`, `Merge`, and `Terminate`. Once you meet these requirements, you can take full advantage of user-defined aggregates in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This article provides more information about how to create and work with user-defined aggregates. For an example, see [Invoke CLR user-defined aggregate functions](clr-user-defined-aggregate-invoking-functions.md).

For more information, see [SqlUserDefinedAggregateAttribute](/dotnet/api/microsoft.sqlserver.server.sqluserdefinedaggregateattribute).

## Aggregation methods

The class registered as a user-defined aggregate should support the following instance methods. The query processor uses the following methods to compute the aggregation.

- [Init](#init-method)
- [Accumulate](#accumulate-method)
- [Merge](#merge-method)
- [Terminate](#terminate-method)

### Init method

Syntax:

```csharp
public void Init();
```

The query processor uses this method to initialize the computation of the aggregation. This method is invoked once for each group that the query processor is aggregating. The query processor might choose to reuse the same instance of the aggregate class for computing aggregates of multiple groups. The `Init` method should perform any clean-up as necessary from previous uses of this instance, and enable it to restart a new aggregate computation.

### Accumulate method

Syntax:

```csharp
public void Accumulate(input-type value[, input-type value, ...]);
```

One or more parameters representing the parameters of the function. *input_type* should be the managed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type equivalent to the native [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type specified by *input_sqltype* in the `CREATE AGGREGATE` statement. For more information, see [Map CLR parameter data](../clr-integration-database-objects-types-net-framework/mapping-clr-parameter-data.md).

For user-defined types (UDTs), the input-type is the same as the UDT type. The query processor uses this method to accumulate the aggregate values. This is invoked once for each value in the group that is being aggregated. The query processor always calls this only after calling the `Init` method on the given instance of the aggregate-class. The implementation of this method should update the state of the instance to reflect the accumulation of the argument value being passed in.

### Merge method

Syntax:

```csharp
public void Merge(udagg_class value);
```

This method can be used to merge another instance of this aggregate class with the current instance. The query processor uses this method to merge multiple partial computations of an aggregation.

### Terminate method

Syntax:

```csharp
public return_type Terminate();
```

This method completes the aggregate computation and returns the result of the aggregation. The *return_type* should be a managed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type that is the managed equivalent of *return_sqltype* specified in the `CREATE AGGREGATE` statement. The *return_type* can also be a user-defined type.

## Related content

- [CLR user-defined types](../clr-integration-database-objects-user-defined-types/clr-user-defined-types.md)
- [Invoke CLR user-defined aggregate functions](clr-user-defined-aggregate-invoking-functions.md)
