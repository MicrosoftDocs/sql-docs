---
title: "Mathematical Functions (Transact-SQL)"
description: Mathematical Transact-SQL functions in the SQL Server Database Engine.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/16/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "calculations [SQL Server]"
  - "mathematical functions [SQL Server]"
  - "functions [SQL Server], mathematical"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqledge-current || =azure-sqldw-latest || >=aps-pdw-2016 || =fabric || =fabric-sqldb"
---
# Mathematical functions (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

The following scalar functions perform a calculation, usually based on input values that you provide as arguments, and return a numeric value.

## Numeric magnitude and sign (single-value transforms)

Functions in this category evaluate the magnitude or directional sign of a numeric input. Use them in data validation, normalization, financial modeling, and any scenario where the positivity, negativity, or absolute scale of a value needs to be interpreted or standardized.

| Function | Description |
| --- | --- |
| [ABS](abs-transact-sql.md) | Returns the absolute (positive) value of the numeric expression. |
| [SIGN](sign-transact-sql.md) | Returns +1, 0, or -1 depending on whether the expression is positive, zero, or negative. |

## Rounding and integer boundary

These functions convert floating-point or high-precision values into integers or fixed-precision representations. They support reporting, bucketing, currency formatting, threshold calculations, and any operation where values must align with discrete numeric boundaries.

| Function | Description |
| --- | --- |
| [CEILING](ceiling-transact-sql.md) | Returns smallest integer greater than or equal to the expression. |
| [FLOOR](floor-transact-sql.md) | Returns largest integer less than or equal the expression. |
| [ROUND](round-transact-sql.md) | Rounds a numeric value to the specified precision and length. |

## Trigonometric functions

### Forward functions (input interpreted as radians)

This group provides the elementary trigonometric functions that compute ratios of a right triangle or model periodic behavior. In SQL workloads, these functions typically support geometric computation, spatial transformations, data analysis, and simulation models that require angle-based calculations.

| Function | Description |
| --- | --- |
| [SIN](sin-transact-sql.md) | Sine of the specified angle. |
| [COS](cos-transact-sql.md) | Cosine of the specified angle. |
| [TAN](tan-transact-sql.md) | Tangent of the input expression. |
| [COT](cot-transact-sql.md) | Cotangent of the specified angle. |

### Inverse trigonometry and angle-from-coordinates

Inverse trigonometric functions return the angle that corresponds to a given trigonometric ratio. These functions enable you to recover an angle from coordinate or sensor data. Use them in navigation, geospatial analytics, error-vector calculations, and any scenario where you compute direction or orientation from component values.

| Function | Description |
| --- | --- |
| [ASIN](asin-transact-sql.md) | Angle (in radians) whose sine is the given value (arcsine). |
| [ACOS](acos-transact-sql.md) | Angle (in radians) whose cosine is the given value (arccosine). |
| [ATAN](atan-transact-sql.md) | Angle (in radians) whose tangent is the given value (arctangent). |
| [ATN2](atn2-transact-sql.md) | Angle (in radians) between the positive x-axis and a ray to point `(y, x)`. |

## Angle conversion

These functions convert values between degrees and radians. They serve as utility operations that support interoperability with APIs, libraries, and mathematical formulas that expect a specific angular measurement unit.

| Function | Description |
| --- | --- |
| [DEGREES](degrees-transact-sql.md) | Converts radians to degrees. |
| [RADIANS](radians-transact-sql.md) | Converts degrees to radians. |

## Exponents, logarithms, powers, and roots

This category includes functions that scale values exponentially, compute logarithmic magnitude, raise numbers to arbitrary powers, or extract roots. Typical workloads include financial compounding, scoring models, machine-learning feature engineering, scientific analysis, and any transformation involving nonlinear growth or decay.

| Function | Description |
| --- | --- |
| [EXP](exp-transact-sql.md) | Exponential value of the expression (e raised to the expression). |
| [LOG](log-transact-sql.md) | Natural logarithm by default; optional base supported in SQL Server. |
| [LOG10](log10-transact-sql.md) | Base-10 logarithm. |
| [POWER](power-transact-sql.md) | Raises the expression to the specified power. |
| [SQRT](sqrt-transact-sql.md) | Square root of the specified value. |
| [SQUARE](square-transact-sql.md) | Square of the specified value. |

## Constants and randomness

These functions provide numerical constants and pseudo-random number generation for sampling, stochastic modeling, testing, and procedural computations. Use them for simulation, Monte Carlo analysis, randomized selection, or creating reproducible test scenarios when seeded.

| Function | Description |
| --- | --- |
| [PI](pi-transact-sql.md) | Returns the constant `π` (pi). |
| [RAND](rand-transact-sql.md) | Returns a pseudo-random float between 0 and 1. |

## Remarks

Arithmetic functions, such as `ABS`, `CEILING`, `DEGREES`, `FLOOR`, `POWER`, `RADIANS`, and `SIGN`, return a value with the same data type as the input value. Trigonometric and other functions, including `EXP`, `LOG`, `LOG10`, `SQUARE`, and `SQRT`, cast their input values to **float** and return a **float** value.

All mathematical functions, except for `RAND`, are deterministic functions. This means they return the same results each time they're called with a specific set of input values. `RAND` is deterministic only when you specify a seed parameter. For more information about function determinism, see [Deterministic and nondeterministic functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).

## Related content

- [Arithmetic operators (Transact-SQL)](../language-elements/arithmetic-operators-transact-sql.md)
- [What are the SQL database functions?](functions.md)
