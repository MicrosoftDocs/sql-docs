---
title: "SQLServerParameterMetaData Members"
description: "SQLServerParameterMetaData Members"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerParameterMetaData Members
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  The following tables list the members that are exposed by the [SQLServerParameterMetaData](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md) class.  
  
## Constructors  
 None.  
  
## Fields  
 None.  
  
## Inherited Fields  
  
|Name|Description|  
|----------|-----------------|  
|java.sql.ParameterMetaData|parameterModeIn, parameterModeInOut, parameterModeOut, parameterModeUnknown, parameterNoNulls, parameterNullable, parameterNullableUnknown|  
  
## Methods  
  
|Name|Description|  
|----------|-----------------|  
|[getParameterClassName](../../../connect/jdbc/reference/getparameterclassname-method-sqlserverparametermetadata.md)|Retrieves the fully-qualified name of the Java class whose instances should be passed to the [setObject](../../../connect/jdbc/reference/setobject-method-sqlserverpreparedstatement.md) method of the [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) class.|  
|[getParameterCount](../../../connect/jdbc/reference/getparametercount-method-sqlserverparametermetadata.md)|Retrieves the number of parameters in the [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md) object for which this [SQLServerParameterMetaData](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md) object contains information.|  
|[getParameterMode](../../../connect/jdbc/reference/getparametermode-method-sqlserverparametermetadata.md)|Retrieves the mode of the designated parameter.|  
|[getParameterType](../../../connect/jdbc/reference/getparametertype-method-sqlserverparametermetadata.md)|Retrieves the SQL type of the designated parameter.|  
|[getParameterTypeName](../../../connect/jdbc/reference/getparametertypename-method-sqlserverparametermetadata.md)|Retrieves the database-specific type name of the designated parameter.|  
|[getPrecision](../../../connect/jdbc/reference/getprecision-method-sqlserverparametermetadata.md)|Retrieves the number of decimal digits for the designated parameter.|  
|[getScale](../../../connect/jdbc/reference/getscale-method-sqlserverparametermetadata.md)|Retrieves the number of digits to the right of the decimal point for the designated parameter.|  
|[isNullable](../../../connect/jdbc/reference/isnullable-method-sqlserverparametermetadata.md)|Retrieves whether null values are allowed in the designated parameter.|  
|[isSigned](../../../connect/jdbc/reference/issigned-method-sqlserverparametermetadata.md)|Retrieves whether values for the designated parameter can be signed numbers.|  
  
## Inherited Methods  
  
|Class inherited from:|Methods|  
|---------------------------|-------------|  
|java.lang.Object|clone, equals, finalize, getClass, hashCode, notify, notifyAll, toString, wait|  
|java.sql.Wrapper|isWrapperFor, unwrap|  
  
## See Also  
 [SQLServerParameterMetaData Class](../../../connect/jdbc/reference/sqlserverparametermetadata-class.md)  
  
  
