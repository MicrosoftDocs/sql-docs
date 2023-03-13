---
title: "SqlServiceAdvancedProperty Class"
description: "SqlServiceAdvancedProperty Class"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "SqlServiceAdvancedProperty class"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "SqlServiceAdvancedProperty Class"
apitype: "MOFDef"
---
# SqlServiceAdvancedProperty Class
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  The [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) represents an advanced property of the service that is referenced by the [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object.  
  
 The [AdvancedProperties Property (SqlService Class)](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/advancedproperties-property-sqlservice-class.md) references an array of [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) objects.  
  
 The [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx) class represents properties that are unique to the service. These properties are not in the list of properties that is associated with the [SqlService Class](../sqlservice-class/sqlservice-class.md) class. The `SqlServiceAdvancedProperty Class` class allows representation of string, numeric, or Boolean properties. You can use this class to view the unique properties of the specified service.  
  
## See Also  
 [Starting, Stopping, and Pausing Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
