---
title: "ADO Error Codes"
description: "Capture ADO Error Codes"
author: rothja
ms.author: jroth
ms.date: "02/15/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "errors [ADO], error codes"
---
# Capture ADO Error Codes
In addition to the provider errors returned in the [Error](../../reference/ado-api/error-object.md) objects of the [Errors](../../reference/ado-api/errors-collection-ado.md) collection, ADO itself can return errors to the exception-handling mechanism of your run-time environment. Use the error trapping mechanism your programming language, such as the **On Error** statement in Microsoft® Visual Basic, or the **try-catch** block in Microsoft Visual C++®, to capture ADO errors.

 For the list of ADO error codes, see [ErrorValueEnum](../../reference/ado-api/errorvalueenum.md).

## See Also
 [Error Object](../../reference/ado-api/error-object.md)
 [Errors Collection (ADO)](../../reference/ado-api/errors-collection-ado.md)