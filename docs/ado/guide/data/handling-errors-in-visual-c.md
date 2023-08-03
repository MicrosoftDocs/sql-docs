---
title: "Handling Errors in Visual C++"
description: "Handling Errors in Visual C++"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "errors [ADO], Visual C++"
  - "Visual C++ error handling [ADO]"
dev_langs:
  - "C++"
---
# Handling Errors in Visual C++
In COM, most operations return an HRESULT return code that indicates whether a function completed successfully. The #import directive generates wrapper code around each "raw" method or property and checks the returned HRESULT. If the HRESULT indicates failure, the wrapper code throws a COM error by calling _com_issue_errorex() with the HRESULT return code as an argument. COM error objects can be caught in a **try-catch** block. (For efficiency's sake, catch a reference to a _com_error object.)  
  
 Remember, these are ADO errors: they result from the ADO operation failing. Errors returned by the underlying provider appear as **Error** objects in the **Connection** object's **Errors** collection.  
  
 The #import directive only creates error-handling routines for methods and properties declared in the ADO .dll. However, you can take advantage of this same error-handling mechanism by writing your own error-checking macro or inline function. See the topic Visual C++® Extensions for examples.
