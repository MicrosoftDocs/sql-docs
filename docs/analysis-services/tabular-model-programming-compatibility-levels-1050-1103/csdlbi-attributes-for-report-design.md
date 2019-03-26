---
title: "CSDLBI Attributes for Report Design | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# CSDLBI Attributes for Report Design
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  This section describes the attributes in the extensions to CSDL for tabular modeling that affect [!INCLUDE[ssCrescent](../../includes/sscrescent-md.md)] query design.  
  
## Model Attributes  
 These attributes are defined on a sub-element of a CSDL [EntityContainer](http://msdn.microsoft.com/library/bb399169.aspx) element.  
  
|Attribute name|Data type|Description|  
|--------------------|---------------|-----------------|  
|Culture|Text|Indicates the culture used for currency formats. If omitted, EN-US is used.|  
|IsRightToLeft|Boolean|Indicates whether the values of text fields should be read right to left by default|  
  
## Entity Attributes  
 These attributes are defined on a sub-element of either a CSDL EntitySet or EntityType element.  
  
|Attribute name|Data type|Description|  
|--------------------|---------------|-----------------|  
|**ReferenceName**|Text|The identifier used to reference this entity in a DAX query. If omitted, the name is used.|  
|**Caption**|Text|The display name for the entity.|  
|**Documentation**|Text|Descriptive text to help business users understand the meaning of the data.|  
|**Hidden**|Boolean|Indicates whether the entity should be displayed. The default is **false**.|  
|**CollectionCaption**|Text|Plural name for referring to a set of instances of the entity. If omitted, the Caption attribute is used.|  
|**DisplayKey**|MemberRef[]|An ordered list of field(s) used to identify an entity instance to a business user. The references can include instance properties and navigation properties. When a navigation property is referenced, the **DisplayKey** of the target entity is displayed. If the **DisplayKey** value is omitted, the Key field is used.|  
|**DefaultImage**|MemberRef|A reference to the field containing an image used to visually identify an entity instance to a business user. If omitted, the first image field in the entity is used, if any.|  
|**DefaultDetails**|MemberRef[]|An ordered list of fields representing the default set of detail information displayed to a business user about an entity instance.If omitted, the first five (5) fields in the entity are used, excluding those already referenced by **Key**, **DisplayKey**, or **DefaultImage**.|  
|**SortProperties**|MemberRef[]|An ordered list of fields by which the entity instances are typically sorted. When sorting on these fields, the **SortDirection** specified on each field is used. If omitted, the **DisplayKey** fields are used|  
|**DefaultMeasure**|MemberRef|A reference to a measure defined in the model, or to a numeric field with a default aggregate function, to indicate that the measure or field should be used as the default representation for multiple instances of the entity. If omitted, a count of the entity instances is used.|  
|**DefaultLocation**|MemberRef|A reference to a field whose value represents the default location associated with an entity instance. If omitted, the first location field in the entity is used, if any.|  
  
## Field Attributes  
 These attributes are defined on a sub-element of a CSDL Property or [NavigationProperty](http://msdn.microsoft.com/library/bb387104.aspx) element.  
  
|Attribute name|Data type|Description|  
|--------------------|---------------|-----------------|  
|**ReferenceName**|Text|The identifier used to reference this entity in a DAX query. If omitted, the field name is used.|  
|**Caption**|Text|The display name for the entity. If omitted, the field's **ReferenceName** is used.|  
|**Documentation**|Text|Descriptive text to help business users understand the meaning of the field.|  
|**Hidden**|Boolean|Indicates whether the field should be displayed. The default is **false**, meaning the field is displayed.|  
|**DisplayFolder**|Text|The name (full path) of the folder in which this field is displayed. If omitted, the field is displayed at the model root.|  
|**ContextualNameRule**|Enum|A value indicating whether and how the property name should be modified based on the context in which it is used. Possible values are:  **None**,  **Role**,  **Merge**.|  
|**Alignment**|Enum|A value indicating how the field values should be aligned in a tabular presentation. Possible values are **Default**, **Center**, **Left**, **Right**. If omitted, the default determines the alignment based on the field's data type.|  
|**FormatString**|Text|A .NET format string indicating how the field's value should be formatted by default. If omitted, the following format is assumed:<br /><br /> -Datetime fields: regional short date or "d"<br /><br /> -Floating-point fields and integral fields with a default aggregate function: regional number or "n"<br /><br /> -Integers with no default aggregate function: regional decimal number or "d"<br /><br /> For all other types of fields, no format string applies.|  
|**Units**|Text|The symbol that is applied to field values to express units. If omitted, the units are assumed to be unknown.|  
|**Width**|Integer|The preferred width in characters that should be reserved for displaying the field's values in a tabular presentation. If omitted, a default width is based on the field's data type.|  
|**SortDirection**|Enum|A value indicating how the fields values are typically sorted. Possible values are **Default**, **Ascending**, **Descending**. If omitted, the default value assigns a sort direction is based on the field's data type.|  
|**IsRightToLeft**|Boolean|Indicates whether the field contains text that should be read right to left. If omitted, the model setting is assumed.|  
|**OrderBy**|MemberRef|A reference to another field within the model that defines the sort order for this field's values. The values for the two fields must have a 1:1 mapping, or the sort behavior is undefined. If omitted, the field is sorted based on its own value.|  
|**Contents**|Enum|An enumeration describing the subtype or contents of the field. If omitted, no particular subtype is assumed, unless the field's data type is Binary, in which case Image is assumed. For a full list of supported content types, see the AMO documentation.|  
|**DefaultAggregateFunction**|Enum|A value indicating the default function, if any, typically used to aggregate this field. Possible values are **None**, **Sum**, **Average**, **Count**, **Min**, **Max**. If omitted, **Sum** is assumed for numeric fields, **None** for all other fields.|  
|**IsSimpleMeasure**|Boolean|Indicates whether a measure is merely a simple aggregate of a numeric field. Such aggregates can be easily defined in the query as needed and therefore should be omitted from the model definition to improve performance. If omitted, **false** is assumed.|  
|**Kpi**<br /><br /> **KpiGoal**<br /><br /> **KpiStatus**|Subelement|Indicates that the measure element is to be used as a KPI. The KPI subelement uses the KpiGoal and KpiStauts elements to define the associated display image and target ranges.|  
  
  
