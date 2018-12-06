---
title: "Trace DLL | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "trace DLLs [ODBC]"
  - "tracing options [ODBC], trace DLLs"
ms.assetid: 5ab99bd3-cdc3-4e2c-8827-932d1fcb6e00
author: MightyPen
ms.author: genemi
manager: craigg
---
# Trace DLL
The DLL that performs tracing is one of the ODBC core components. The trace DLL is currently provided as a sample DLL in the ODBC component of the Windows SDK, and was formerly included the Microsoft Data Access Components (MDAC) SDK. Therefore, the registry entry, interface, and sample code for the trace DLL are available. This DLL can be replaced by a trace DLL produced by either an ODBC user or a third-party vendor. A custom trace DLL should be given a different name than the original sample trace DLL. Trace DLLs must be installed in the system directory, or they will fail to load. The connection strings will not be passed to the trace DLL by the Driver Manager.  
  
 The trace DLL traces input arguments, output arguments, deferred arguments, return codes, and SQLSTATEs. When tracing is enabled, the Driver Manager calls the trace DLL at two points: once upon function entry (before argument validation) and again just before the function returns.  
  
 When an application calls a function, the Driver Manager calls a trace function in the trace DLL before calling the function in the driver or processing the call itself. Each ODBC function has a corresponding trace function (prefixed with *Trace*) that is identical to the ODBC function with the exception of the name. When the trace function is called, the trace DLL captures the input arguments and returns a return code. Because the trace DLL is called before the Driver Manager validates arguments, invalid function calls are traced, so state transition errors and invalid arguments are logged.  
  
 After calling the trace function in the trace DLL, the Driver Manager calls the ODBC function in the driver. It then calls **TraceReturn** in the trace DLL. This function takes two arguments: the value returned by the trace DLL for the trace function, and the return code returned by the driver to the Driver Manager for the ODBC function (or the value returned by the Driver Manager itself if it processed the function). The function uses the value returned for the trace function to manipulate captured input argument values. It writes the code returned for the ODBC function to the log file (or displays it dynamically, if that is enabled). It dereferences the output argument pointers and logs the output argument values.
