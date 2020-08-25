---
description: "ADOX Enumerated Constants"
title: "ADOX Enumerated Constants | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "enumerated constants [ADOX]"
ms.assetid: 9d91f511-d46f-44ef-97ef-77bf93836186
author: rothja
ms.author: jroth
---
# ADOX Enumerated Constants
To assist debugging, the ADOX enumerated constants list a value for each constant. However, this value is purely advisory, and may change from one release of ADOX to another. Your code should only depend on the name, not the actual value, of enumerated constants.  
  
 The following enumerated constants are defined.  
  
|Enumeration|Description|  
|-----------------|-----------------|  
|[ActionEnum](./actionenum.md)|Specifies the type of action to be performed when **SetPermissions** is called.|  
|[AllowNullsEnum](./allownullsenum.md)|Specifies whether records with null values are indexed.|  
|[ColumnAttributesEnum](./columnattributesenum.md)|Specifies characteristics of a **Column**.|  
|[DataTypeEnum](../ado-api/datatypeenum.md)|Specifies the data type of a **Field**, **Parameter**, or **Property**.|  
|[InheritTypeEnum](./inherittypeenum.md)|Specifies how objects will inherit permissions set with **SetPermissions**.|  
|[KeyTypeEnum](./keytypeenum.md)|Specifies the type of **Key**: primary, foreign, or unique.|  
|[ObjectTypeEnum](./objecttypeenum.md)|Specifies the type of database object for which to set permissions or ownership.|  
|[RightsEnum](./rightsenum.md)|Specifies the rights or permissions for a group or user on an object.|  
|[RuleEnum](./ruleenum.md)|Specifies the rule to follow when a **Key** is deleted.|  
|[SortOrderEnum](./sortorderenum.md)|Specifies the sort sequence for an indexed column.|  
  
## See Also  
 [ADOX API Reference](./adox-object-model.md?view=sql-server-ver15)   
 [ADO Extensions for Data Definition Language and Security (ADOX)](../../guide/extensions/ado-extensions-for-data-definition-language-and-security-adox.md)