---
title: "SqlServiceAdvancedProperty Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "SqlServiceAdvancedProperty Class"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SqlServiceAdvancedProperty class"
ms.assetid: a5d06bde-6058-464c-a4aa-444d83f2331f
caps.latest.revision: 32
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SqlServiceAdvancedProperty Class
  The [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) represents an advanced property of the service that is referenced by the [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object.  
  
 The [AdvancedProperties Property (SqlService Class)](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/advancedproperties-property-sqlservice-class.md) references an array of [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) objects.  
  
 The [Starting and Stopping Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx) class represents properties that are unique to the service. These properties are not in the list of properties that is associated with the [SqlService Class](http://technet.microsoft.com/library/ms186497.aspx) class. The [SqlServiceAdvancedProperty Class](http://technet.microsoft.com/library/ms182447.aspx) class allows representation of string, numeric, or Boolean properties. You can use this class to view the unique properties of the specified service.  
  
## See Also  
 [Starting, Stopping, and Pausing Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  