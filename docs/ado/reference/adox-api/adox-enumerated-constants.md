---
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
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADOX Enumerated Constants
To assist debugging, the ADOX enumerated constants list a value for each constant. However, this value is purely advisory, and may change from one release of ADOX to another. Your code should only depend on the name, not the actual value, of enumerated constants.  
  
 The following enumerated constants are defined.  
  
|Enumeration|Description|  
|-----------------|-----------------|  
|[ActionEnum](../../../ado/reference/adox-api/actionenum.md)|Specifies the type of action to be performed when **SetPermissions** is called.|  
|[AllowNullsEnum](../../../ado/reference/adox-api/allownullsenum.md)|Specifies whether records with null values are indexed.|  
|[ColumnAttributesEnum](../../../ado/reference/adox-api/columnattributesenum.md)|Specifies characteristics of a **Column**.|  
|[DataTypeEnum](../../../ado/reference/ado-api/datatypeenum.md)|Specifies the data type of a **Field**, **Parameter**, or **Property**.|  
|[InheritTypeEnum](../../../ado/reference/adox-api/inherittypeenum.md)|Specifies how objects will inherit permissions set with **SetPermissions**.|  
|[KeyTypeEnum](../../../ado/reference/adox-api/keytypeenum.md)|Specifies the type of **Key**: primary, foreign, or unique.|  
|[ObjectTypeEnum](../../../ado/reference/adox-api/objecttypeenum.md)|Specifies the type of database object for which to set permissions or ownership.|  
|[RightsEnum](../../../ado/reference/adox-api/rightsenum.md)|Specifies the rights or permissions for a group or user on an object.|  
|[RuleEnum](../../../ado/reference/adox-api/ruleenum.md)|Specifies the rule to follow when a **Key** is deleted.|  
|[SortOrderEnum](../../../ado/reference/adox-api/sortorderenum.md)|Specifies the sort sequence for an indexed column.|  
  
## See Also  
 [ADOX API Reference](../../../ado/reference/adox-api/adox-api-reference.md)   
 [ADO Extensions for Data Definition Language and Security (ADOX)](../../../ado/guide/extensions/ado-extensions-for-data-definition-language-and-security-adox.md)
