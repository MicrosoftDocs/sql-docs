---
description: "Affected ODBC Components"
title: "Affected ODBC Components | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "upgrading applications [ODBC], affected components"
  - "application upgrades [ODBC], affected components"
  - "compatibility [ODBC], affected components"
  - "ODBC drivers [ODBC], backward compatibility"
  - "backward compatibility [ODBC], affected components"
ms.assetid: 71fa6ea4-007c-4c2b-b5af-2cec6ea79b58
author: David-Engel
ms.author: v-davidengel
---
# Affected ODBC Components
Backward compatibility describes how applications, the Driver Manager, and drivers are affected by the introduction of a new version of the Driver Manager. This affects applications and driver when either or both of them remain in the old version. There are, therefore, three types of backward compatibility to consider, as shown in the following table.  
  
|Type|Version of DM|Version of application|Version of driver|  
|----------|-------------------|----------------------------|-----------------------|  
|Backward Compatibility of Driver Manager|*3.x*|*2.x*|*2.x*|  
|Backward Compatibility of Driver[1]|*3.x*|*2.x*|*3.x*|  
|Backward Compatibility of Application|*3.x*|*3.x*|*2.x*|  
  
 [1]   The backward compatibility of drivers is primarily discussed in Appendix G: Driver Guidelines for Backward Compatibility.  
  
> [!NOTE]
>  A standards-compliant application - for example, an application that has been written in accordance with the Open Group or ISO CLI standards - is guaranteed to work with an ODBC *3.x* driver through the ODBC *3.x* Driver Manager. It is assumed that the functionality that the application is using is available in the driver. It is also assumed that the standards-compliant application has been compiled with the ODBC *3.x* header files.
