---
title: "Recordset Object (ADO) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset"
helpviewer_keywords: 
  - "Recordset object [ADO]"
ms.assetid: ede1415f-c3df-4cc5-a05b-2576b2b84b60
author: MightyPen
ms.author: genemi
manager: craigg
---
# Recordset Object (ADO)
Represents the entire set of records from a base table or the results of an executed command. At any time, the **Recordset** object refers to only a single record within the set as the current record.  
  
## Remarks  
 You use **Recordset** objects to manipulate data from a provider. When you use ADO, you manipulate data almost entirely using **Recordset** objects. All **Recordset** objects consist of records (rows) and fields (columns). Depending on the functionality supported by the provider, some **Recordset** methods or properties may not be available.  
  
 ADODB.Recordset is the ProgID that should be used to create a **Recordset** object. Existing applications that reference the outdated ADOR.Recordset ProgID will continue to work without recompiling, but new development should reference ADODB.Recordset.  
  
 There are four different cursor types defined in ADO:  
  
-   **Dynamic cursor** Allows you to view additions, changes, and deletions by other users; allows all types of movement through the **Recordset** that doesn't rely on bookmarks; and allows bookmarks if the provider supports them.  
  
-   **Keyset cursor** Behaves like a dynamic cursor, except that it prevents you from seeing records that other users add, and prevents access to records that other users delete. Data changes by other users will still be visible. It always supports bookmarks and therefore allows all types of movement through the **Recordset**.  
  
-   **Static cursor** Provides a static copy of a set of records for you to use to find data or generate reports; always allows bookmarks and therefore allows all types of movement through the **Recordset**. Additions, changes, or deletions by other users will not be visible. This is the only type of cursor allowed when you open a client-side **Recordset** object.  
  
-   **Forward-only cursor** Allows you to only scroll forward through the **Recordset**. Additions, changes, or deletions by other users will not be visible. This improves performance in situations where you need to make only a single pass through a **Recordset**.  
  
 Set the [CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md) property prior to opening the **Recordset** to choose the cursor type, or pass a *CursorType* argument with the [Open](../../../ado/reference/ado-api/open-method-ado-recordset.md) method. Some providers don't support all cursor types. Check the documentation for the provider. If you don't specify a cursor type, ADO opens a forward-only cursor by default.  
  
 If the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property is set to **adUseClient** to open a **Recordset**, the **UnderlyingValue** property on [Field](../../../ado/reference/ado-api/field-object.md) objects is not available in the returned **Recordset** object. When used with some providers (such as the Microsoft ODBC Provider for OLE DB in conjunction with Microsoft SQL Server), you can create **Recordset** objects independently of a previously defined [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object by passing a connection string with the **Open** method. ADO still creates a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object, but it doesn't assign that object to an object variable. However, if you are opening multiple **Recordset** objects over the same connection, you should explicitly create and open a **Connection** object; this assigns the **Connection** object to an object variable. If you do not use this object variable when opening your **Recordset** objects, ADO creates a new **Connection** object for each new **Recordset**, even if you pass the same connection string.  
  
 You can create as many **Recordset** objects as needed.  
  
 When you open a **Recordset**, the current record is positioned to the first record (if any) and the [BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) and [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) properties are set to **False**. If there are no records, the **BOF** and **EOF** property settings are **True**.  
  
 You can use the [MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md), **MoveLast**, **MoveNext**, and **MovePrevious** methods; the [Move](../../../ado/reference/ado-api/move-method-ado.md) method; and the [AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md), [AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md), and [Filter](../../../ado/reference/ado-api/filter-property.md) properties to reposition the current record, assuming the provider supports the relevant functionality. Forward-only **Recordset** objects support only the [MoveNext](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md) method. When you use the **Move** methods to visit each record (or enumerate the **Recordset**), you can use the **BOF** and **EOF** properties to determine if you've moved beyond the beginning or end of the **Recordset**.  
  
 Before using any functionality of a **Recordset** object, you must call the **Supports** method on the object to verify that the functionality is supported or available. You must not use the functionality when the **Supports** method returns false. For example, you can use the **MovePrevious** method only if `Recordset.Supports(adMovePrevious)` returns **True**. Otherwise, you will get an error, because the **Recordset** object might have been closed and the functionality rendered unavailable on the instance. If a feature you are interested in is not supported, **Supports** will return false as well. In this case, you should avoid calling the corresponding property or method on the **Recordset** object.  
  
 **Recordset** objects can support two types of updating: immediate and batched. In immediate updating, all changes to data are written immediately to the underlying data source once you call the [Update](../../../ado/reference/ado-api/update-method.md) method. You can also pass arrays of values as parameters with the [AddNew](../../../ado/reference/ado-api/addnew-method-ado.md) and **Update** methods and simultaneously update several fields in a record.  
  
 If a provider supports batch updating, you can have the provider cache changes to more than one record and then transmit them in a single call to the database with the [UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md) method. This applies to changes made with the **AddNew**, **Update**, and [Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md) methods. After you call the **UpdateBatch** method, you can use the [Status](../../../ado/reference/ado-api/status-property-ado-recordset.md) property to check for any data conflicts in order to resolve them.  
  
> [!NOTE]
>  To execute a query without using a [Command](../../../ado/reference/ado-api/command-object-ado.md) object, pass a query string to the **Open** method of a **Recordset** object. However, a **Command** object is required when you want to persist the command text and re-execute it, or use query parameters.  
  
 The [Mode](../../../ado/reference/ado-api/mode-property-ado.md) property governs access permissions.  
  
 The **Fields** collection is the default member of the **Recordset** object. As a result, the following two code statements are equivalent.  
  
```  
Debug.Print objRs.Fields.Item(0)  ' Both statements print   
Debug.Print objRs(0)              '  the Value of Item(0).  
```  
  
 When a **Recordset** object is passed across processes, only the **rowset** values are marshalled, and the properties of the **Recordset** object are ignored. During unmarshalling, the **rowset** is unpacked into a newly created **Recordset** object, which also sets its properties to the default values.  
  
 The **Recordset** object is safe for scripting.  
  
 This section contains the following topic.  
  
-   [Recordset Object Properties, Methods, and Events](../../../ado/reference/ado-api/recordset-object-properties-methods-and-events.md)  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Fields Collection (ADO)](../../../ado/reference/ado-api/fields-collection-ado.md)   
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)   
 [Appendix A: Providers](../../../ado/guide/appendixes/appendix-a-providers.md)
