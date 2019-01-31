---
title: "Formatting Decimal Strings and Money Values | Microsoft Docs"
ms.custom: ""
ms.date: "02/11/2019"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "formatting, decimal types, money values"
author: "yitam"
ms.author: "v-yitam"
manager: "mbarwin"
---
# Formatting Decimal Strings and Money Values
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

You can retrieve integers and floats as numbers, but to preserve the accuracy of [decimal or numeric types](https://docs.microsoft.com/sql/t-sql/data-types/decimal-and-numeric-transact-sql), their values are fetched as strings with exact precisions by default. If the values are less than 1, leading zeroes are dropped. The same applies to money and smallmoney fields because they are a subset of decimal fields with fixed precisions and scales.

Beginning with version 5.6.0, you can format the decimal strings to add leading zeroes (if missing) or configure decimal places for money values:

-   [Formatting decimal strings &#40;SQLSRV Driver&#41;](../../connect/php/formatting-decimals-sqlsrv-driver.md)

-   [Formatting decimal strings &#40;PDO_SQLSRV Driver&#41;](../../connect/php/formatting-decimals-pdo-sqlsrv-driver.md)

## See Also
[Retrieving Data](../../connect/php/retrieving-data.md)
