---
title: "Creating and Terminating Threads"
description: "Creating and Terminating Threads"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "terminating threads [ODBC]"
  - "threads [ODBC]"
  - "multithreaded applications [ODBC]"
---
# Creating and Terminating Threads
Multithread applications that use ODBC should call the Microsoft® Visual C++® Run-Time Library functions **_beginthread** and **_endthread** (or **_beginthreadex** and **_endthreadex**) to create and terminate threads that call the ODBC Driver Manager. If applications call the Microsoft Windows NT® functions **CreateThread** and **EndThread** instead, memory leaks will occur because the Driver Manager and some ODBC drivers call C run-time functions that will not work on a thread created by calling **CreateThread**. For more information, see the Microsoft Windows® documentation.
