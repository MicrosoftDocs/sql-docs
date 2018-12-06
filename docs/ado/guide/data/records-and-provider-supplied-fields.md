---
title: "Records and Provider-Supplied Fields | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "records-provided fields [ADO]"
  - "provider-supplied fields [ADO]"
ms.assetid: 77f95e0a-0cf2-411a-a792-593f77330fbd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Records and Provider-Supplied Fields
When a [Record](../../../ado/reference/ado-api/record-object-ado.md) object is opened, its source can be the current row of an open [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), an absolute URL, or a relative URL in conjunction with an open [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
 If the **Record** is opened from a **Recordset**, the **Record** object [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection will contain all the fields from the **Recordset**, plus any fields added by the underlying provider.  
  
 The provider may insert additional fields that serve as supplementary characteristics of the **Record**. As a result, a **Record** may have unique fields not in the **Recordset** as a whole or any **Record** derived from another row of the **Recordset**.  
  
 For example, all rows of a **Recordset** derived from an e-mail data source might have columns such as From, To, and Subject. A **Record** derived from that **Recordset** will have the same fields. However, the **Record** may also have other fields unique to the particular message represented by that **Record**, such as Attachment and Cc (carbon copy).  
  
 Although the **Record** object and current row of the **Recordset** have the same fields, they are different because **Record** and **Recordset** objects have different methods and properties.  
  
 A field held in common by the **Record** and **Recordset** can be modified on either object. However, the field cannot be deleted on the **Record** object, although the underlying provider may support setting the field to null.  
  
 After the **Record** is opened, you can programmatically add fields. You can also delete fields that you have added, but you cannot delete fields from the original **Recordset**.  
  
 You can also open the **Record** object directly from a URL. In this case, the fields added to the **Record** depend on the underlying provider. Currently, most providers add a set of fields that describe the entity represented by the **Record**. If the entity consists of a stream of bytes, such as a simple file, a [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object can usually be opened from the **Record**.  
  
## Special Fields for Document Source Providers  
 A special class of providers, called *document source providers*, manages folders and documents. When a **Record** object represents a document or a **Recordset** object represents a folder of documents, the document source provider populates those objects with a unique set of fields that describe characteristics of the document instead of the actual document itself. Typically, one field contains a reference to the **Stream** that represents the document.  
  
 These fields constitute a resource **record** or **recordset** and are listed for the specific providers that support them in [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md).  
  
 Two constants index the **Fields** collection of a resource **Record** or **Recordset** to retrieve a pair of commonly used fields. The **Field** object [Value](../../../ado/reference/ado-api/value-property-ado.md) property returns the desired content.  
  
-   The field accessed with the **adDefaultStream** constant contains a default stream associated with the **Record** or **Recordset** object. The provider assigns a default stream to an object.  
  
-   The field accessed with the **adRecordURL** constant contains the absolute URL that identifies the document.  
  
 A document source provider does not support the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection of **Record** and **Field** objects. The content of the **Properties** collection is null for such objects.  
  
 A document source provider may add a provider-specific property such as **Datasource Type** to identify whether it is a document source provider. For more information about how to determine your type of provider, see your provider documentation.  
  
## Resource Recordset Columns  
 A *resource recordset* consists of the following columns.  
  
|Column name|Type|Description|  
|-----------------|----------|-----------------|  
|RESOURCE_PARSENAME|AdVarWChar|Read-only. Indicates the URL of the resource.|  
|RESOURCE_PARENTNAME|AdVarWChar|Read-only. Indicates the absolute URL of the parent record.|  
|RESOURCE_ABSOLUTEPARSENAME|AdVarWChar|Read-only. Indicates the absolute URL of the resource, which is the concatenation of PARENTNAME and PARSENAME.|  
|RESOURCE_ISHIDDEN|AdBoolean|True if the resource is hidden. No rows will be returned unless the command that creates the rowset explicitly selects rows where RESOURCE_ISHIDDEN is True.|  
|RESOURCE_ISREADONLY|AdBoolean|True if the resource is read-only. Tries to open this resource with DBBINDFLAG_WRITE and will fail with DB_E_READONLY. This property can be edited even when the resource has only been opened for reading.|  
|RESOURCE_CONTENTTYPE|AdVarWChar|Indicates the likely use of the document-for example, a lawyer's brief. This may correspond to the Office template that was used to create the document.|  
|RESOURCE_CONTENTCLASS|AdVarWChar|Indicates the MIME type of the document, indicating the format such as "`text/html`".|  
|RESOURCE_CONTENTLANGUAGE|AdVarWChar|Indicates the language in which the content is stored.|  
|RESOURCE_CREATIONTIME|adFileTime|Read-only. Indicates a FILETIME structure that contains the time the resource was created. The time is reported in Coordinated Universal Time (UTC) format.|  
|RESOURCE_LASTACCESSTIME|AdFileTime|Read-only. Indicates a FILETIME structure that contains the time that the resource was last accessed. The time is in UTC format. The FILETIME members are zero if the provider does not support this time member.|  
|RESOURCE_LASTWRITETIME|AdFileTime|Read-only. Indicates a FILETIME structure that contains the time that the resource was last written. The time is in UTC format. The FILETIME members are zero if the provider does not support this time member.|  
|RESOURCE_STREAMSIZE|asUnsignedBigInt|Read-only. Indicates the size of the resource's default stream, in bytes.|  
|RESOURCE_ISCOLLECTION|AdBoolean|Read-only. True if the resource is a collection, such as a directory. False if the resource is a simple file.|  
|RESOURCE_ISSTRUCTUREDDOCUMENT|AdBoolean|True if the resource is a structured document. False if the resource is not a structured document. It could be a collection or a simple file.|  
|DEFAULT_DOCUMENT|AdVarWChar|Read-only. Indicates that this resource contains a URL to the default simple document of a folder or a structured document. Used when the default stream is requested from a resource. This property is blank for a simple file.|  
|CHAPTERED_CHILDREN|AdChapter|Read-only. Optional. Indicates the chapter of the rowset that contains the children of the resource. (The *OLE DB Provider for Internet Publishing* does not use this column.)|  
|RESOURCE_DISPLAYNAME|AdVarWChar|Read-only. Indicates the display name of the resource.|  
|RESOURCE_ISROOT|AdBoolean|Read-only. True if the resource is the root of a collection or structured document.|  
  
## See Also  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
