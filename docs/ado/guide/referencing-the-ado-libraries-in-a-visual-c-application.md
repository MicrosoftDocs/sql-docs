---
title: "Referencing the ADO Libraries In a Visual C++ Application"
description: "Referencing the ADO Libraries In a Visual C++ Application"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "libraries [ADO]"
  - "referencing libraries in a Visual C++ application[ADO]"
  - "ADO, libraries"
dev_langs:
  - "C++"
---
# Referencing the ADO Libraries In a Visual C++ Application
To use the latest version of ADO in a Visual C++ application, use the following `#import` directive:  
  
```cpp
#import "msado15.dll" \  
    no_namespace rename("EOF", "EndOfFile")  
```  
  
 To use ADO MD or ADOX, you must import *msadomd.dll* or *msadox.dll*, by using the syntax above.  
  
## Backward Compatibility  
 To use any earlier version of ADO, replace *msado15.dll* above with one of the following type libraries.  
  
-   *msado27.tlb*, ADO 2.7 Type Library  
  
-   *msado26.tlb*, ADO 2.6 Type Library  
  
-   *msado25.tlb*, ADO 2.5 Type Library  
  
-   *msado21.tlb*, ADO 2.1 Type Library  
  
-   *msado20.tlb*, ADO 2.0 Type Library
