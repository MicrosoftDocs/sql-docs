---
title: Cluster (DMX)
description: Cluster (DMX)
ms.date: 08/13/2026
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom:
  - dmx
---
# Cluster (DMX)

[!INCLUDE [ssas](../includes/applies-to-version/ssas.md)]

Returns the cluster that is most likely to contain the input case.

## Syntax

```syntaxsql
Cluster()
```

## Applies to

This function can be used only if the underlying data mining model supports clustering.

## Return types

The `Cluster` function doesn't require parameters.

The `Cluster` function returns a scalar value of a cluster name. However, if you use this function as an argument of another function, you must regard it as a `<cluster column reference>`.

## Remarks

`Cluster` can also be used as a `<cluster column reference>` for a `PredictHistogram` function.

## Examples

The following example uses a singleton query with the [PredictHistogram (DMX)](predicthistogram-dmx.md) and `Cluster` functions to return the distance of the individual case from each cluster of the TM Clustering mining model and the probability that the individual case will exist in each cluster.

```sql
SELECT
    PredictHistogram(Cluster())
FROM
    [TM Clustering]
    NATURAL PREDICTION
    JOIN (SELECT 28 AS [Age],
       '2-5 Miles' AS [Commute Distance],
       'Graduate Degree' AS [Education],
       0 AS [Number Cars Owned],
       0 AS [Number Children At Home]
    ) AS t;
```

## Related content

- [ClusterProbability (DMX)](clusterprobability-dmx.md)
- [Data Mining Extensions (DMX) Function Reference](data-mining-extensions-dmx-function-reference.md)
- [Functions (DMX)](functions-dmx.md)
- [General Prediction Functions (DMX)](general-prediction-functions-dmx.md)
