---
title: "Open Method (ADO Record) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "_Record::raw_Open"
  - "_Record::Open"
helpviewer_keywords: 
  - "Open method [ADO]"
ms.assetid: ab79a623-88a9-40b6-a017-a658bf19b778
author: MightyPen
ms.author: genemi
manager: craigg
---
# Open Method (ADO Record)
Opens an existing [Record](../../../ado/reference/ado-api/record-object-ado.md) object, or creates a new item represented by the **Record**, such as a file or directory.  
  
## Syntax  
  
```  
  
Open Source, ActiveConnection, Mode, CreateOptions, Options, UserName, Password  
```  
  
#### Parameters  
 *Source*  
 Optional. A **Variant** that may represent the URL of the entity to be represented by this **Record** object, a **Command**, an open [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) or another **Record** object, a string that contains an SQL SELECT statement or a table name.  
  
 *ActiveConnection*  
 Optional. A **Variant** that represents the connect string or open [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object.  
  
 *Mode*  
 Optional. A [ConnectModeEnum](../../../ado/reference/ado-api/connectmodeenum.md) value that specifies the access mode for the resultant **Record** object. Default value is **adModeUnknown**.  
  
 *CreateOptions*  
 Optional. A [RecordCreateOptionsEnum](../../../ado/reference/ado-api/recordcreateoptionsenum.md) value that specifies whether an existing file or directory should be opened, or a new file or directory should be created. Default value is **adFailIfNotExists**. If set to the default value, the access mode is obtained from the [Mode](../../../ado/reference/ado-api/mode-property-ado.md) property. This parameter is ignored when the *Source* parameter does not contain a URL.  
  
 *Options*  
 Optional. A [RecordOpenOptionsEnum](../../../ado/reference/ado-api/recordopenoptionsenum.md) value that specifies options for opening the **Record**. Default value is **adOpenRecordUnspecified**. These values can be combined.  
  
 *UserName*  
 Optional. A **String** value that contains the user ID that, if it is required, authorizes access to *Source*.  
  
 *Password*  
 Optional. A **String** value that contains the password that, if it is required, verifies *UserName*.  
  
## Remarks  
 *Source* may be:  
  
-   A URL. If the protocol for the URL is http, the Internet Provider will be invoked by default. If the URL points to a node that contains an executable script (such as an .ASP page), a **Record** that contains the source instead of the executed contents is opened by default. Use the *Options* argument to modify this behavior.  
  
-   A **Record** object. A **Record** object opened from another **Record** will clone the original **Record** object.  
  
-   A **Command** object. The opened **Record** object represents the single row returned by executing the **Command**. If the results contain more than a single row, the contents of the first row are placed in the record and an error may be added to the **Errors** collection.  
  
-   An SQL SELECT statement. The opened **Record** object represents the single row returned by executing the contents of the string. If the results contain more than a single row, the contents of the first row are placed in the record and an error may be added to the **Errors** collection.  
  
-   A table name.  
  
 If the **Record** object represents an entity that cannot be accessed with a URL (for example, a row of a **Recordset** derived from a database), the values of both the [ParentURL](../../../ado/reference/ado-api/parenturl-property-ado.md) property and the field accessed with the **adRecordURL** constant are null.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
## Applies To  
 [Record Object (ADO)](../../../ado/reference/ado-api/record-object-ado.md)  
  
## See Also  
 [Open Method (ADO Connection)](../../../ado/reference/ado-api/open-method-ado-connection.md)   
 [Open Method (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)   
 [Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)   
 [OpenSchema Method](../../../ado/reference/ado-api/openschema-method.md)
