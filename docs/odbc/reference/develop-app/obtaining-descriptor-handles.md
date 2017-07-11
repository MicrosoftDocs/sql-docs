---
title: "Obtaining Descriptor Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "descriptors [ODBC], retrieving or setting field values"
ms.assetid: 936f983f-c7e9-43f3-97ea-dd4b1bbf4654
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Obtaining Descriptor Handles
An application obtains the handle of any explicitly allocated descriptor as an output argument of the call to **SQLAllocHandle**. The handle of an implicitly allocated descriptor is obtained by calling **SQLGetStmtAttr**.