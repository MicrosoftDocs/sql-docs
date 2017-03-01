---
title: "ADO Error Codes | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: "H1Hack27Feb2017"
ms.date: "02/15/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords:
  - "errors [ADO], error codes"
ms.assetid: 3aee61c7-a9b7-4596-b78e-5828a00d0281
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Capture ADO Error Codes
In addition to the provider errors returned in the [Error](../../../ado/reference/ado-api/error-object.md) objects of the [Errors](../../../ado/reference/ado-api/errors-collection-ado.md) collection, ADO itself can return errors to the exception-handling mechanism of your run-time environment. Use the error trapping mechanism your programming language, such as the **On Error** statement in Microsoft® Visual Basic, or the **try-catch** block in Microsoft Visual C++®, to capture ADO errors.

 For the list of ADO error codes, see [ErrorValueEnum](../../../ado/reference/ado-api/errorvalueenum.md).

## See Also
 [Error Object](../../../ado/reference/ado-api/error-object.md)
 [Errors Collection (ADO)](../../../ado/reference/ado-api/errors-collection-ado.md)
