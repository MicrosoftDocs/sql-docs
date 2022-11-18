---
title: "Microsoft OLE DB Provider for Microsoft Active Directory Service"
description: "Microsoft OLE DB Provider for Microsoft Active Directory Service"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADSI provider [ADO]"
  - "Active Directory Service Interfaces provider [ADO]"
  - "providers [ADO], OLE DB provider for Active Directory service"
  - "OLE DB provider for Active Directory service [ADO]"
---
# Microsoft OLE DB Provider for Microsoft Active Directory Service
The Active Directory Service Interfaces (ADSI) Provider allows ADO to connect to heterogeneous directory services through ADSI. This gives ADO applications read-only access to the Microsoft Windows NT 4.0 and Microsoft Windows 2000 directory services, in addition to any LDAP-compliant directory service and Novell Directory Services. ADSI itself is based on a provider model, so that if there is a new provider giving access to another directory, the ADO application will be able to access it seamlessly. The ADSI provider is free-threaded and Unicode enabled.  
  
## Connection String Parameters  
 To connect to this provider, set the **Provider** argument of the [ConnectionString](../../reference/ado-api/connectionstring-property-ado.md) property to the following:  
  
```vb
ADSDSOObject  
```  
  
 Reading the [Provider](../../reference/ado-api/provider-property-ado.md) property will return this string as well.  
  
## Typical Connection String  
 A typical connection string for this provider is as follows:  
  
```vb
"Provider=ADSDSOObject;User ID=MyUserID;Password=MyPassword;"  
```  
  
 The string consists of the following keywords.  
  
|Keyword|Description|  
|-------------|-----------------|  
|**Provider**|Specifies the OLE DB Provider for Active Directory Service.|  
|**User ID**|Specifies the user name. If this keyword is omitted, the current logon is used.|  
|**Password**|Specifies the user password. If this keyword is omitted. Then the current logon is used.|  
  
> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.  
  
## Command Text  
 A four-part command text string is recognized by the provider in the following syntax:  
  
```vb
"Root; Filter; Attributes[; Scope]"  
```  
  
|Value|Description|  
|-----------|-----------------|  
|*Root*|Indicates the **ADsPath** object from which to start the search (that is, the root of the search).|  
|*Filter*|Indicates the search filter in the RFC 1960 format.|  
|*Attributes*|Indicates a comma-delimited list of attributes to be returned.|  
|*Scope*|Optional. A **String** that specifies the scope of the search. Can be one of the following:<br /><br /> -   Base - Search only the base object (root of the search).<br />-   OneLevel - Search only one level.<br />-   Subtree - Search the whole subtree.|  
  
 For example:  
  
```vb
"<LDAP://DC=ArcadiaBay,DC=COM>;(objectClass=*);sn, givenName; subtree"  
```  
  
 The provider also supports SQL SELECT for command text. For example:  
  
```vb
"SELECT title, telephoneNumber From 'LDAP://DC=Microsoft, DC=COM' WHERE   
objectClass='user' AND objectCategory='Person'"  
```  
  
## Remarks  
 The provider does not accept stored procedure calls or simple table names (for example, the [CommandType](../../reference/ado-api/commandtype-property-ado.md) property will always be **adCmdText**). See the Active Directory Service Interfaces documentation for a more thorough description of the command text elements.  
  
## Recordset Behavior  
 The following tables list the features available on a [Recordset](../../reference/ado-api/recordset-object-ado.md) object opened by using this provider. Only the static cursor type (**adOpenStatic**) is available.  
  
 For more information about **Recordset** behavior for your provider configuration, run the [Supports](../../reference/ado-api/supports-method.md) method and enumerate the [Properties](../../reference/ado-api/properties-collection-ado.md) collection of the **Recordset** to determine whether provider-specific dynamic properties are present.  
  
 **Availability of standard ADO Recordset properties:**  
  
|Property|Availability|  
|--------------|------------------|  
|[AbsolutePage](../../reference/ado-api/absolutepage-property-ado.md)|read/write|  
|[AbsolutePosition](../../reference/ado-api/absoluteposition-property-ado.md)|read/write|  
|[ActiveConnection](../../reference/ado-api/activeconnection-property-ado.md)|read-only|  
|[BOF](../../reference/ado-api/bof-eof-properties-ado.md)|read-only|  
|[Bookmark](../../reference/ado-api/bookmark-property-ado.md)|read/write|  
|[CacheSize](../../reference/ado-api/cachesize-property-ado.md)|read/write|  
|[CursorLocation](../../reference/ado-api/cursorlocation-property-ado.md)|always **adUseServer**|  
|[CursorType](../../reference/ado-api/cursortype-property-ado.md)|always **adOpenStatic**|  
|[EditMode](../../reference/ado-api/editmode-property.md)|always **adEditNone**|  
|[EOF](../../reference/ado-api/bof-eof-properties-ado.md)|read-only|  
|[Filter](../../reference/ado-api/filter-property.md)|read/write|  
|[LockType](../../reference/ado-api/locktype-property-ado.md)|read/write|  
|[MarshalOptions](../../reference/ado-api/marshaloptions-property-ado.md)|not available|  
|[MaxRecords](../../reference/ado-api/maxrecords-property-ado.md)|read/write|  
|[PageCount](../../reference/ado-api/pagecount-property-ado.md)|read-only|  
|[PageSize](../../reference/ado-api/pagesize-property-ado.md)|read/write|  
|[RecordCount](../../reference/ado-api/recordcount-property-ado.md)|read-only|  
|[Source](../../reference/ado-api/source-property-ado-recordset.md)|read/write|  
|[State](../../reference/ado-api/state-property-ado.md)|read-only|  
|[Status](../../reference/ado-api/status-property-ado-recordset.md)|read-only|  
  
 **Availability of standard ADO Recordset methods:**  
  
|Method|Available?|  
|------------|----------------|  
|[AddNew](../../reference/ado-api/addnew-method-ado.md)|No|  
|[Cancel](../../reference/ado-api/cancel-method-ado.md)|No|  
|[CancelBatch](../../reference/ado-api/cancelbatch-method-ado.md)|No|  
|[CancelUpdate](../../reference/ado-api/cancelupdate-method-ado.md)|No|  
|[Clone](../../reference/ado-api/clone-method-ado.md)|Yes|  
|[Close](../../reference/ado-api/close-method-ado.md)|Yes|  
|[Delete](../../reference/ado-api/delete-method-ado-recordset.md)|No|  
|[GetRows](../../reference/ado-api/getrows-method-ado.md)|Yes|  
|[Move](../../reference/ado-api/move-method-ado.md)|Yes|  
|[MoveFirst](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|  
|[MoveLast](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|  
|[MoveNext](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|  
|[MovePrevious](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|  
|[NextRecordset](../../reference/ado-api/nextrecordset-method-ado.md)|Yes|  
|[Open](../../reference/ado-api/open-method-ado-recordset.md)|Yes|  
|[Requery](../../reference/ado-api/requery-method.md)|Yes|  
|[Resync](../../reference/ado-api/resync-method.md)|Yes|  
|[Supports](../../reference/ado-api/supports-method.md)|Yes|  
|[Update](../../reference/ado-api/update-method.md)|No|  
|[UpdateBatch](../../reference/ado-api/updatebatch-method.md)|No|  
  
 For more information about ADSI and the specifics of the provider, refer to the Active Directory Service Interfaces documentation or visit the ADSI Web page.  
  
## See Also  
 [CommandType Property (ADO)](../../reference/ado-api/commandtype-property-ado.md)   
 [ConnectionString Property (ADO)](../../reference/ado-api/connectionstring-property-ado.md)   
 [Properties Collection (ADO)](../../reference/ado-api/properties-collection-ado.md)   
 [Provider Property (ADO)](../../reference/ado-api/provider-property-ado.md)   
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)   
 [Supports Method](../../reference/ado-api/supports-method.md)