---
title: "Visual C++ Extensions for ADO"
description: "Visual C++ Extensions for ADO"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, Visual C++"
  - "Visual C++ [ADO], VC++ extensions for ADO"
dev_langs:
  - "C++"
---
# Visual C++ Extensions for ADO
The preferred method of programming ADO with Visual C++ is using the **#import** directive, as discussed in [Microsoft Visual C++ ADO Programming](./visual-c-ado-programming.md). However, earlier versions of ADO shipped with an alternate method of programming using Visual C++: the Visual C++ Extensions. This section documents this feature for those who must maintain Visual C++ Extensions code, but new ADO code should be written using #**import**.

 One of the most tedious jobs Visual C++ programmers face when retrieving data with ADO is converting data returned as a VARIANT data type into a C++ data type, and then storing the converted data in a class or structure. In addition to being cumbersome, retrieving C++ data through a VARIANT data type diminishes performance.

 ADO provides an interface that supports retrieving data into native C/C++ data types without going through a VARIANT, and also provides preprocessor macros that simplify using the interface. The result is a flexible tool that is easier to use and has great performance.

 A common C/C++ client scenario is to bind a record in a [Recordset](../../reference/ado-api/recordset-object-ado.md) to a C/C++ struct or class containing native C/C++ types. When going through VARIANTs, this involves writing conversion code from VARIANT to C/C++ native types. The Visual C++ Extensions for ADO are targeted at making this scenario much easier for the Visual C++ programmer.

 See the following topics to learn more about the Visual C++ Extensions for ADO.

-   [Using Visual C++ Extensions for ADO](./using-visual-c-extensions.md)

-   [Visual C++ Extensions Header](./visual-c-extensions-header.md)

-   [ADO with Visual C++ Extensions Example](./visual-c-extensions-example.md)

## See Also
 [ADO for Visual C++ Syntax Index for COM](../../reference/ado-api/ado-for-visual-c-syntax-index-for-com.md)
 [Visual C++ Extensions Example](./visual-c-extensions-example.md)
 [Using Visual C++ Extensions](./using-visual-c-extensions.md)
 [Visual C++ Extensions Header](./visual-c-extensions-header.md)